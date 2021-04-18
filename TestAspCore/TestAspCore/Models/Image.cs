﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestAspCore.Models
{
    public class Image 
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Image field is required !")]
        public string Path { get; set; }
        public  Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        
    }
}
