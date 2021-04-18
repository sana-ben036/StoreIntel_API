using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAspCore.Authentication;

namespace TestAspCore.Models.Repositories
{
    public class OrderRepository : IStoreRepository<Order>
    {

        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task<Order> Create(Order order)
        {
            order.Date = DateTime.Now;
            order.Status = Status.NotConfirmed;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task Delete(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> Get()
        {
            var orders = await _context.Orders.ToListAsync();

            return orders;
        }

        public async Task<Order> Get(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            return order;
        }

        public async Task Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
    
}
