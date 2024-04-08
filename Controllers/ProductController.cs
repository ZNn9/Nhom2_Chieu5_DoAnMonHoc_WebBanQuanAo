using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Repositories;

public class ProductController : Controller
{

    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    public ProductController(IProductRepository productRepository,
                            ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }
    // Hiển thị danh sách sản phẩm
    public async Task<IActionResult> Index(string productTitle)
    {

        var products = await _productRepository.GetAllAsync();
        var categories = await _categoryRepository.GetAllAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        if (productTitle != null)
        {
            var productdata = products.Where(p => p.Name.Contains(productTitle));
            return View(productdata);
        }
        else
        {
            return View(products);
        }
    }

    // Hiển thị thông tin chi tiết sản phẩm
    public async Task<IActionResult> Display(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
}