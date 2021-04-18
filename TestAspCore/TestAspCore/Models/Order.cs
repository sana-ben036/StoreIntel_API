using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TestAspCore.Authentication;

namespace TestAspCore.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Image field is required !")]
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public double Total { get; set; }
        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual IList<OrderDetail> OrderLines { get; set; }
    }
}
