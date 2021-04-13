using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestAspCore.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Title field is required !")]
        [MinLength(4)]
        public string Title { get; set; }
        public SerializableAttribute Image { get; set; }

        public virtual IList<Product> Products { get; set; }
}
}
