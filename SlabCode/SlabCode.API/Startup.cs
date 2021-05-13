using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SlabCode.Core.ProjectServices.Contract;
using SlabCode.Core.ProjectServices.Implementation;
using SlabCode.DataAccess;
using SlabCode.DataAccess.DBOperations.Contract;
using SlabCode.DataAccess.DBOperations.Implementation;
using System.Text;

namespace SlabCode.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers();

            AddSwagger(services);

            AddBusinessServices(services);
            AddRepositories(services);
            AddDomainServices(services);

            AddObjectConfigurations(services);
        }

        public void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"SlabCode {groupName}",
                    Version = groupName,
                    Description = "SlabCode Technical Test"
                });
            });
        }

        private void AddBusinessServices(IServiceCollection services)
        {
            services.AddScoped<IProjectManagement, SlabCodeProjectManagement>();
        }

        private void AddRepositories(IServiceCollection services)
        {
            //services.Configure<MongoConfiguration>(Configuration.GetSection("Mongo"));
            services.AddScoped<UserDbOperations, UserDbOperations>();
            services.AddScoped<ProjectDbOperations, ProjectDbOperations>();

            services.AddEntityFrameworkNpgsql()
            .AddDbContext<SlabCodeTestContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        private void AddDomainServices(IServiceCollection services)
        {
            
        }

        private void AddObjectConfigurations(IServiceCollection services)
        {
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SlabCode V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
