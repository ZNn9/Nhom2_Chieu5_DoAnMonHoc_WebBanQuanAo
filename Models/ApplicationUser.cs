

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Age { get; set; }
        // NotMapped dùng để không ánh xạ lúc tạo database bằng Endentity
        [NotMapped]
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
