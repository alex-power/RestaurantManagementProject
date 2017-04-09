﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CollegeEventWebsite.Auth
{
    public interface ICustomPrincipal : IPrincipal
    {
        int Id { get; set; }
        string Username { get; set; }
    }
}
