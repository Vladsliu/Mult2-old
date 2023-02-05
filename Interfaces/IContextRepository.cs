using CloudinaryDotNet.Actions;
using Mult2.Models;

namespace Mult2.Interfaces
{
    public interface IContextRepository//????
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetByIdAsync(int id);
        Task<Category> GetByIdAsyncNoTracking(int id);
        bool Add(Category category);
        bool Update(Category category);
        bool Delete(Category category);
        bool Save();
    }
}
