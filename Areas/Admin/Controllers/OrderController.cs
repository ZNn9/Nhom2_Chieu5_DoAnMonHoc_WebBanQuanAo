using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    [Area("Admin")]

    public class OrderController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
