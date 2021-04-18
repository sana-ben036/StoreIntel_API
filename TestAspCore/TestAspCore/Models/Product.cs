using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TestAspCore.Authentication;

namespace TestAspCore.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Title field is required !")]
        [MinLength(4)]
        public string Title { get; set; }
        [Required(ErrorMessage = "The Price field is required !")]
        public double Price { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "The Stock Quantity field is required !")]
        [Display(Name = "Stock Quantity")]
        public int InStock { get; set; }

        
        [Required(ErrorMessage = "The Category field is required !")]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual IList<Image> Images { get; set; }
        public virtual IList<OrderDetail> OrderLines { get; set; }
        

    }
}
