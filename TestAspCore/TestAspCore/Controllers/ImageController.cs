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
    public class ImageController : ControllerBase
    {
        private readonly IStoreRepository<Image> _storeRepository;

        public ImageController(IStoreRepository<Image> storeRepository)
        {
            _storeRepository = storeRepository;
        }




        // GET: ImageController
        [HttpGet]
        public async Task<IEnumerable<Image>> GetList()
        {
            return await _storeRepository.Get();
        }

        // GET: ImageController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(Guid id)
        {
            var image = await _storeRepository.Get(id);

            if (image is null)
                return NotFound();
            return Ok(image);
        }



        // POST: ImageController/Create
        [HttpPost]
        public async Task<ActionResult<Image>> Post([FromBody] Image image)
        {
            var newimg = await _storeRepository.Create(image);
            return Ok(newimg);
        }

        // PuT: ImageController/Update
        [HttpPut("{id}")]

        public async Task<ActionResult> Put(Guid id, [FromBody] Image image)
        {
            if (id != image.Id)
            {
                return BadRequest();
            }
            await _storeRepository.Update(image);
            return Ok(image);


        }

        // GET: ImageControllerr/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var image = await _storeRepository.Get(id);

            if (image is null)
            {
                return NotFound();
            }
            await _storeRepository.Delete(image.Id);
            return NoContent();
        }




    }
}
