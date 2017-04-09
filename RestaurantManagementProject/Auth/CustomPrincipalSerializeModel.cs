﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeEventWebsite.Auth
{
    public class CustomPrincipalSerializeModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
    }
}