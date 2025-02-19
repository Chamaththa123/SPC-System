using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPC.Models;
using SPC.Services;

namespace SPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly StockService _stockService;

        public StockController(StockService stockService)
        {
            _stockService = stockService;
        }

        // Create New Stock Entry
        [HttpPost("create")]
        public async Task<IActionResult> CreateStock([FromBody] Stock stock)
        {
            if (stock == null) return BadRequest("Invalid data.");
            int result = await _stockService.CreateStock(stock);
            return result > 0 ? Ok("Stock created successfully.") : StatusCode(500, "Error creating stock.");
        }


        //  Get Stock by Branch ID
        [HttpGet("get-by-branch/{branchId}")]
        public async Task<ActionResult<List<Stock>>> GetStockByBranch(int branchId)
        {
            return await _stockService.GetStockByBranch(branchId);
        }

        [HttpPut("update-instock/{stockId}")]
        public async Task<IActionResult> UpdateStockInStock(int stockId, [FromBody] int inStock)
        {
            int result = await _stockService.UpdateStockInStock(stockId, inStock);
            return result > 0 ? Ok("Stock updated successfully.") : StatusCode(500, "Error updating stock.");
        }

        [HttpGet("get-by-id/{stockId}")]
        public async Task<ActionResult<Stock>> GetStockById(int stockId)
        {
            return await _stockService.GetStockById(stockId);
        }
    }
}
