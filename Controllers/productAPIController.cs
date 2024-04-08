using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nhom2_Chieu5_DoAnCuoiMon_WebBanQuanAo.DataAccess;
using System;

namespace Nhom2_Chieu5_DoAnMonHoc_WebBanQuanAo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productAPIController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        public productAPIController(ApplicationDbContext _db)
        {
            db = _db;
        }
        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var productTitle = db.Products.Where(p => p.Name.Contains(term))
                                               .Select(p => p.Name).ToList();
                return Ok(productTitle);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
