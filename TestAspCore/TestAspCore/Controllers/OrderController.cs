using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAspCore.Models;
using TestAspCore.Models.Repositories;

namespace TestAspCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IStoreRepository<Order> _storeRepository;

        public OrderController(IStoreRepository<Order> storeRepository)
        {
            _storeRepository = storeRepository;
        }



        // GET: OrderController
        [HttpGet]
        public async Task<IEnumerable<Order>> GetList()
        {
            return await _storeRepository.Get();
        }

        // GET: CategoryController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _storeRepository.Get(id);

            if (order is null)
                return NotFound();
            return Ok(order);
        }



        // POST: OrderController/Create
        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] Order order)
        {
            var neworder = await _storeRepository.Create(order);
            return Ok(neworder);
        }

        // PuT: OrderController/Update
        [HttpPut("{id}")]

        public async Task<ActionResult> Put(Guid id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }
            await _storeRepository.Update(order);
            return Ok(order);


        }

        // GET: OrderController/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var order = await _storeRepository.Get(id);

            if (order is null)
            {
                return NotFound();
            }
            await _storeRepository.Delete(order.Id);
            return Ok();
        }
    }
}
