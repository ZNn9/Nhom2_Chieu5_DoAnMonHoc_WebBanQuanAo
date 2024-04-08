using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.DataAccess;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Repositories;
using System.Diagnostics;
using X.PagedList;
using static Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.DataAccess.ApplicationDbContext;

namespace Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger,
                                IProductRepository productRepository,
                                ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> IndexAsync(int? page)
        {
            var products = await _productRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            PagedList<Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models.Product> lstProducts = new PagedList<Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models.Product>(products, pageNumber, pageSize);
            return View(lstProducts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult SearchProducts(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return BadRequest("Search query is required.");
                }
                var result = _context.Products.Where(
                            p => p.Name.Contains(query) ||
                            (p.Description != null && p.Description.Contains(query))).ToList();
                return View("Index", result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
