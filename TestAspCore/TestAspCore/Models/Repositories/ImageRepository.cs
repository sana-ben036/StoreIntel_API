using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAspCore.Authentication;

namespace TestAspCore.Models.Repositories
{
    public class ImageRepository : IStoreRepository<Image>
    {

        private readonly AppDbContext _context;
        public ImageRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Image> Create(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task Delete(Guid id)
        {
            var image = await _context.Images.FindAsync(id);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Image>> Get()
        {
            var images = await _context.Images.ToListAsync();

            return images;
        }

        public async Task<Image> Get(Guid id)
        {
            var image = await _context.Images.FindAsync(id);

            return image;
        }

        public async Task Update(Image image)
        {
            _context.Entry(image).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
