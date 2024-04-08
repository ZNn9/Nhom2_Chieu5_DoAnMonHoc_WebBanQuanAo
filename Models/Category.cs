using System.ComponentModel.DataAnnotations;

namespace Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
