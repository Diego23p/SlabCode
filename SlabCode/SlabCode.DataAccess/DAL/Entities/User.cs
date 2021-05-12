using System;
using System.Collections.Generic;

#nullable disable

namespace SlabCode.DataAccess
{
    public partial class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool? Enable { get; set; }
        public string Email { get; set; }
    }
}
