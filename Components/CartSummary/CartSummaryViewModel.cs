using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.Models;

namespace Nhom2_Chieu5_DoAnMonHoc_WebBanQuanAo.Components.CartSummary
{
    public class CartSummaryViewModel
    {
        public int NumberOfItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
