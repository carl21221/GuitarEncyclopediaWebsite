using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuitarStock.Security
{
    public class ApplicationIdentityRole : IdentityRole
    {
        public string RoleDescription { get; set; }
    }
}
