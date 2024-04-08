using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.DataAccess;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models;

namespace Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class EmployeeManangerController : Controller
    {
        // Cách dùng UserManager "UserManager<ApplicationUser>"
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeManangerController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var nonCustomerUsers = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("Customer"))
                {
                    user.Roles = roles;
                    nonCustomerUsers.Add(user);
                }
            }

            return View(nonCustomerUsers);
        }
    }
}
