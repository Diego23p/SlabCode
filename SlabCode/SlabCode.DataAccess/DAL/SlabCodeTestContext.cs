using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SlabCode.DataAccess
{
    public partial class SlabCodeTestContext : DbContext
    {
        public SlabCodeTestContext()
        {
        }

        public SlabCodeTestContext(DbContextOptions<SlabCodeTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("project_pk");

                entity.ToTable("Project");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.Finishdate)
                    .HasColumnType("date")
                    .HasColumnName("finishdate");

                entity.Property(e => e.Initdate)
                    .HasColumnType("date")
                    .HasColumnName("initdate");

                entity.Property(e => e.State)
                    .HasColumnType("character varying")
                    .HasColumnName("state");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("task_pk");

                entity.ToTable("Task");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.Executiondate)
                    .HasColumnType("date")
                    .HasColumnName("executiondate");

                entity.Property(e => e.Project)
                    .HasColumnType("character varying")
                    .HasColumnName("project");

                entity.Property(e => e.State)
                    .HasColumnType("character varying")
                    .HasColumnName("state");

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.Project)
                    .HasConstraintName("task_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("user_pk");

                entity.ToTable("User");

                entity.Property(e => e.Username)
                    .HasColumnType("character varying")
                    .HasColumnName("username");

                entity.Property(e => e.Email)
                    .HasColumnType("character varying")
                    .HasColumnName("email");

                entity.Property(e => e.Enable).HasColumnName("enable");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasColumnType("character varying")
                    .HasColumnName("role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
