namespace Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Repositories
{
    using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models; // Thay thế bằng namespace thực tế của bạn
    using System.Collections.Generic;
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
