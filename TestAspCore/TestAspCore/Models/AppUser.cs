using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAspCore.Models;

namespace TestAspCore.Authentication
{
    public class AppUser : IdentityUser
    {
        public virtual IList<Order> Orders { get; set; }
    }
}
