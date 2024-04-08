namespace Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Repositories
{
    using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models;
    using System.Collections.Generic;
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
