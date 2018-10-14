using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entity
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Profile { get; set; }
        public int Role { get; set; }
    }
}
