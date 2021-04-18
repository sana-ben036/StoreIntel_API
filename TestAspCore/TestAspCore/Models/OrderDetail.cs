using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAspCore.Models
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
