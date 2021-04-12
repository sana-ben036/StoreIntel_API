﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAspCore.Authentication
{
    public class EditUserModel : ApplicationUser
    {
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
