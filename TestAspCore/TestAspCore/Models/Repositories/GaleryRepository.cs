using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAspCore.Authentication;

namespace TestAspCore.Models.Repositories
{
    public class GaleryRepository : IStoreRepository<Galery>
    {

        private readonly AppDbContext _context;
        public GaleryRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Galery> Create(Galery image)
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

        public async Task<IEnumerable<Galery>> Get()
        {
            var images = await _context.Images.ToListAsync();

            return images;
        }

        public async Task<Galery> Get(Guid id)
        {
            var image = await _context.Images.FindAsync(id);

            return image;
        }

        public async Task Update(Galery image)
        {
            _context.Entry(image).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
