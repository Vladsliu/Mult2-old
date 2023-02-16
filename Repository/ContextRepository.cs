using Microsoft.EntityFrameworkCore;
using Mult2.Data;
using Mult2.Interfaces;
using Mult2.Models;

namespace Mult2.Repository
{
    public class ContextRepository : IContextRepository
    {
        private readonly ApplicationDbContext _context;
        public ContextRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Category category)
        {
          _context.Add(category);
            return Save();
        }

        public bool Delete(Category category)
        {
           _context.Remove(category);
            return Save();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
           
            return await _context.Categories.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Category> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
         
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
