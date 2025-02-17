using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPC.Models;
using SPC.Services;

namespace SPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierOrderController : ControllerBase
    {
        private readonly SupplierOrderService _supplierOrderService;

        public SupplierOrderController(SupplierOrderService supplierOrderService)
        {
            _supplierOrderService = supplierOrderService;
        }

        // Add Supplier Order
        [HttpPost("add")]
        public async Task<IActionResult> AddSupplierOrder([FromBody] SupplierOrder order)
        {
            var result = await _supplierOrderService.AddSupplierOrder(order);
            return result > 0 ? Ok("Order Added Successfully") : BadRequest("Failed to Add Order");
        }

        // Get All Supplier Orders
        [HttpGet("all")]
        public async Task<IActionResult> GetAllSupplierOrders()
        {
            var result = await _supplierOrderService.GetAllSupplierOrders();
            return Ok(result);
        }

        // Get Supplier Order by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierOrderById(int id)
        {
            var result = await _supplierOrderService.GetSupplierOrderById(id);
            return result != null ? Ok(result) : NotFound("Order Not Found");
        }

        // Update Supplier Order Status
        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateSupplierOrderStatus(int id, [FromBody] int status)
        {
            var result = await _supplierOrderService.UpdateSupplierOrderStatus(id, status);
            return result ? Ok("Status Updated") : BadRequest("Failed to Update Status");
        }

        // Get Supplier Orders by Supplier ID
        [HttpGet("supplierOrders/{supplierId}")]
        public IActionResult GetSupplierOrdersBySupplierID(int supplierId)
        {
            List<SupplierOrder> orders = _supplierOrderService.GetSupplierOrdersBySupplierID(supplierId);

            if (orders == null || orders.Count == 0)
                return NotFound("No supplier orders found for the given supplier ID.");

            return Ok(orders);
        }

    }
}
