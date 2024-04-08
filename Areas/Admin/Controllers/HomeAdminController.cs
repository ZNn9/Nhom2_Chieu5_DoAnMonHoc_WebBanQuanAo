using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nhom2_Chieu5_DoAnMonHoc_WebBanQuanAo.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        [Authorize(Roles = "Admin, Employee")]
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
