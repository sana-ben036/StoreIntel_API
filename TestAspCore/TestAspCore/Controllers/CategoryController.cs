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
    public class CategoryController : ControllerBase
    {
        private readonly IStoreRepository<Category> _storeRepository;

        public CategoryController(IStoreRepository<Category> storeRepository)
        {
            _storeRepository = storeRepository;
        }



        // GET: CategoryController
        [HttpGet]
        public async Task<IEnumerable<Category>> GetList()
        {
            return await _storeRepository.Get();
        }

        // GET: CategoryController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await _storeRepository.Get(id);

            if (category is null)
                return NotFound();
            return Ok(category);
        }



        // POST: CategoryController/Create
        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] Category category)
        {
            var newcat = await _storeRepository.Create(category);
            return Ok(newcat);
        }

        // PuT: CategoryController/Update
        [HttpPut("{id}")]

        public async Task<ActionResult> Put(Guid id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            await _storeRepository.Update(category);
            return Ok(category);


        }

        // GET: CategoryControllerr/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var category = await _storeRepository.Get(id);

            if (category is null)
            {
                return NotFound();
            }
            await _storeRepository.Delete(category.Id);
            return NoContent();
        }



    }
}
