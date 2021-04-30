using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IStoreRepository<Product> storeRepository, IWebHostEnvironment hostEnvironment)
        {
            _storeRepository = storeRepository;
            _hostEnvironment = hostEnvironment;
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
        public async Task<ActionResult<Product>> Post([FromForm] Product product)
        {
            product.ImageName = await SaveImage(product.ImageFile);
            await _storeRepository.Create(product);
            return StatusCode(201);
            
        }

        // PuT: ProductController/Update
        [HttpPut("{id}")]

        public async Task<ActionResult> Put(Guid id, [FromForm] Product product)
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

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }








    }
}
