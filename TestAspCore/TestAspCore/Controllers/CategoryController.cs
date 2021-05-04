using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IWebHostEnvironment _hostEnvironment;

        public CategoryController(IStoreRepository<Category> storeRepository, IWebHostEnvironment hostEnvironment) 
        {
            _storeRepository = storeRepository;
            _hostEnvironment = hostEnvironment;
        }



        // GET: CategoryController
        [HttpGet]
        public async Task<IEnumerable<Category>> GetList()
        {
           var categories = await _storeRepository.Get();
            return categories.Select(x => new Category()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageName = x.ImageName,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}",Request.Scheme,Request.Host,Request.PathBase,x.ImageName)
            });
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
        public async Task<ActionResult<Category>> Post([FromForm] Category category)
        {
            category.ImageName = await SaveImage(category.ImageFile);
            await _storeRepository.Create(category);
            return StatusCode(201);
        }

        // PuT: CategoryController/Update
        [HttpPut("{id}")]

        public async Task<ActionResult> Put(Guid id, [FromForm] Category category)
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
            return Ok();
        }

        [NonAction]
        public async Task<string> SaveImage (IFormFile imageFile)
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
