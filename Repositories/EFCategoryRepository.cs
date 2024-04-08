using Microsoft.EntityFrameworkCore;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.DataAccess;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models;

namespace Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Repositories
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
        public async Task AddAsync(Category catogory)
        {
            _context.Categories.Add(catogory);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Category catogory)
        {
            _context.Categories.Update(catogory);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var catogory = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(catogory);
            await _context.SaveChangesAsync();
        }
    }
}
