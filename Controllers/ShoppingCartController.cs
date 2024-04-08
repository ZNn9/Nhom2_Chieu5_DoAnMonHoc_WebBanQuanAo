using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.DataAccess;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Repositories;
using Nhom2_Chieu5_DoAnMonHoc_WebBanQuanAo.Extensions;

namespace Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Controllers
{
	/*    [Authorize]*/

	// Theo hướng dẫn của thầy Cường Đẹp Trai
	/*public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        *//*[HttpGet]*/
	/*public async Task<IActionResult> AddToCart(int productId, int quantity)
    {
        // Giả sử bạn có phương thức lấy thông tin sản phẩm từ productId
        var product = GetProductFromDatabase(productId);
        var cartItem = new CartItem
        {
            ProductId = productId,
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            Quantity = quantity
        };
        var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
        cart.AddItem(cartItem);
        HttpContext.Session.SetObjectAsJson("Cart", cart);

        return RedirectToAction("Index");
    }*//*

    [HttpGet]
    public ActionResult AddToCart(int id)
    {
        Product? itemProduct = _context.Products.FirstOrDefault(p => p.Id == id);
        if (itemProduct == null)
            return BadRequest("Sản phẩm không tồn tại");
        var carts = GetCartItems();
        var findCartItem = carts.FirstOrDefault(p => p.ProductId.Equals(id));
        if (findCartItem == null)
        {
            //Th thêm mới vào giỏ hàng
            findCartItem = new CartItem()
            {
                ProductId = itemProduct.Id,
                Name = itemProduct.Name,
                ImageUrl = itemProduct.ImageUrl,
                Price = itemProduct.Price,
                Quantity = 1
            };
            carts.Add(findCartItem);
        }
        else
            findCartItem.Quantity++;
        SaveCartSession(carts);
        return RedirectToAction("Index");
    }
    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

        // Giả sử cart.Items là collection của CartItem trong ShoppingCart
        if (cart.Items != null)
        {
            ViewBag.TongTien = cart.Items.Sum(p => p.Price * p.Quantity);
            ViewBag.TongSoLuong = cart.Items.Sum(p => p.Quantity);
        }
        else
        {
            ViewBag.TongTien = 0;
            ViewBag.TongSoLuong = 0;
        }

        return View(cart);
    }
    // Các actions khác...
    List<CartItem>? GetCartItems()
    {
        string jsoncart = HttpContext.Session.GetString("cart");
        if (jsoncart != null)
        {
            return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
        }
        return new List<CartItem>();
    }
    void SaveCartSession(List<CartItem> ls)
    {
        string jsoncart = JsonConvert.SerializeObject(ls);
        HttpContext.Session.SetString("cart", jsoncart);
    }
    private Product GetProductFromDatabase(int productId)
    {
        // Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm
        // để null cho đỡ hiện lỗi sử lí sau
        return null;
    }
    public ShoppingCartController(ApplicationDbContext context,
                                    UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public IActionResult Checkout()
    {
        return View(new Order());
    }
    [HttpPost]
    public async Task<IActionResult> Checkout(Order order)
    {
        var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
        if (cart == null || !cart.Items.Any())
        {
            // Xử lý giỏ hàng trống...
            return RedirectToAction("Index");
        }
        var user = await _userManager.GetUserAsync(User);
        order.UserId = user.Id;
        order.OrderDate = DateTime.UtcNow;
        order.TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity);
        order.OrderDetails = cart.Items.Select(i => new OrderDetail
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity,
            Price = i.Price
        }).ToList();
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        HttpContext.Session.Remove("Cart");
        return View("OrderCompleted", order.Id); // Trang xác nhận hoàn thành đơn hàng
    }
    [HttpGet]
    public async Task<ActionResult> UpdateCart(int id, int quantity)
    {
        var carts = GetCartItems();
        var findCartItem = carts.FirstOrDefault(p => p.ProductId == id);
        if (findCartItem != null)
        {
            findCartItem.Quantity = quantity;
            SaveCartSession(carts);
        }
        return RedirectToAction("Index");
    }
    public ActionResult DeleteCart(int id)
    {
        var carts = GetCartItems();
        var findCartItem = carts.FirstOrDefault(p => p.ProductId == id);
        if (findCartItem != null)
        {
            carts.Remove(findCartItem);
            SaveCartSession(carts);
        }
        return RedirectToAction("Index");
    }*/



	// Hướng dẫn trong sách thực hành web
	[Authorize]
	public class ShoppingCartController : Controller
    {
		private readonly IProductRepository _productRepository;
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
        // Construction
        public ShoppingCartController(ApplicationDbContext context,
	                                    UserManager<ApplicationUser> userManager, 
                                        IProductRepository productRepository)
		{
			_productRepository = productRepository;
			_context = context;
			_userManager = userManager;
		}
        // Lấy list thông tin cart đang chọn mua, bằng cấu trúc json "Session"
        List<CartItem>? GetCartItems()
        {
            string jsoncart = HttpContext.Session.GetString("cart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }
        void SaveCartSession(List<CartItem> ls)
        {
            string jsoncart = JsonConvert.SerializeObject(ls);
            HttpContext.Session.SetString("cart", jsoncart);
        }
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            // Giả sử bạn có phương thức lấy thông tin sản phẩm từ productId
            var product = await GetProductFromDatabase(productId);
            var cartItem = new CartItem
            {
                ProductId = productId,
                Name = product.Name,
                Price = product.Price,
                Quantity = quantity
            };
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            cart.AddItem(cartItem);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

			// Giả sử cart.Items là collection của CartItem trong ShoppingCart
			ViewBag.TongTien = cart.Items.Sum(p => p.Price * p.Quantity);
			ViewBag.TongSoLuong = cart.Items.Sum(p => p.Quantity);
			return View(cart);
        }

        //
        // Các actions khác...
        //

        private async Task<Product> GetProductFromDatabase(int productId)
        {
            // Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm
            var product = await _productRepository.GetByIdAsync(productId);
            return product;
        }

		[HttpPost]
		public async Task<IActionResult> Checkout(Order order)
		{
			var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
			if (cart == null || !cart.Items.Any())
			{
				// Xử lý giỏ hàng trống...
				return RedirectToAction("Index");
			}
			var user = await _userManager.GetUserAsync(User);
			order.UserId = user.Id;
			order.OrderDate = DateTime.UtcNow;
			order.TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity);
			order.OrderDetails = cart.Items.Select(i => new OrderDetail
			{
				ProductId = i.ProductId,
				Quantity = i.Quantity,
				Price = i.Price
			}).ToList();
			_context.Orders.Add(order);
			await _context.SaveChangesAsync();
			HttpContext.Session.Remove("Cart");
			return View("OrderCompleted", order.Id); // Trang xác nhận hoàn thành đơn hàng
        }

		public async Task<IActionResult> OrderCompleted()
		{
			try
			{
				var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
				var user = await _userManager.GetUserAsync(User);
				var order = new Order
				{
					UserId = user != null ? user.Id : null,
					OrderDate = DateTime.UtcNow,
					TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity),
					OrderDetails = cart.Items.Select(item => new OrderDetail
					{
						ProductId = item.ProductId,
						Quantity = item.Quantity,
						Price = item.Price
					}).ToList()
				};

				_context.Orders.Add(order);
				await _context.SaveChangesAsync();
				//xóa session giỏ hàng
				HttpContext.Session.Remove("Cart");
				return View("OrderCompleted", order);
			}
			catch (Exception ex)
			{
				throw;
            }
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart is not null)
            {
                cart.RemoveItem(productId);
                // Lưu lại giỏ hàng vào Session sau khi đã xóa mục
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        // Update cart trong trang giỏ hàng
        public ActionResult UpdateCart(int id, int quantity)
        {
            var carts = GetCartItems();
            var findCartItem = carts.FirstOrDefault(p => p.ProductId == id);
            if (findCartItem != null)
            {
                findCartItem.Quantity = quantity;
                SaveCartSession(carts);
            }
            return RedirectToAction("Index");
        }
    }
}