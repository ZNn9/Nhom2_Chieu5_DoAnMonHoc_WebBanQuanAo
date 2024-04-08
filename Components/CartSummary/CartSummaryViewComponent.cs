using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models;

namespace Nhom2_Chieu5_DoAnMonHoc_WebBanQuanAo.Components.CartSummary
{
    /*	[ViewComponent]*/
    public class CartSummaryViewComponent : ViewComponent
    {
        public CartSummaryViewComponent() { }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CartItem> cart = GetCartItems();
            CartSummaryViewModel viewModel = new CartSummaryViewModel();
            viewModel.NumberOfItems = cart.Count;
            viewModel.TotalPrice = cart.Sum(item => item.Price * item.Quantity);
            return View(viewModel);
        }
        List<CartItem> GetCartItems()
        {
            var sessionCart = HttpContext.Session.GetString("cart");
            if (sessionCart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);
            }
            return new List<CartItem>();
        }

    }
}
