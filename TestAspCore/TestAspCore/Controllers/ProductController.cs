using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAspCore.Authentication;
using TestAspCore.Models;
using TestAspCore.Models.Repositories;

namespace TestAspCore.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IStoreRepository<Product> _storeRepository;

        public ProductController(IStoreRepository<Product> storeRepository)
        {
            _storeRepository = storeRepository;
        }



        // GET: ProductController
        [HttpGet]
        public async Task<IEnumerable<Product>> GetList()
        {
            return await _storeRepository.Get();
        }

        // GET: ProductController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _storeRepository.Get(id);

            if (product is null)
                return NotFound();
            return Ok(product);
        }



        // POST: ProductController/Create
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            var newproduct = await _storeRepository.Create(product);
            return Ok(newproduct);
        }

        // PuT: ProductController/Update
        [HttpPut("{id}")]

        public async Task<ActionResult> Put(Guid id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            await _storeRepository.Update(product);
            //return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            return Ok(product);


        }

        // GET: ProductController/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await _storeRepository.Get(id);

            if (product is null)
            {
                return NotFound();
            }
            await _storeRepository.Delete(product.Id);
            return Ok();
        }










    }
}
