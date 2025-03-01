using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            return Ok(await _saleService.GetAllSalesAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleById(int id)
        {
            return Ok(await _saleService.GetSaleByIdAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult> AddSale(Sale sale)
        {
            return Ok(await _saleService.AddSaleAsync(sale));
        }
    }
}
