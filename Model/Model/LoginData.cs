﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool Validate()
        {
            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return false;
            }

            return true;
        }
    }
}
