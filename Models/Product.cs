using System.ComponentModel.DataAnnotations;
namespace Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models
{
    public class Product
    {
        //Thiếu thuộc tính ... vd: slton
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Range(0, 100000000)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public List<ProductImage>? Images { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [Range(0, 100000000)]
        public int Quantity { get; set; }
        public int Like { get; set; }
        
    }

}
