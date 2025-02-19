using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPC.Models;
using SPC.Services;

namespace SPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyOrderController : ControllerBase
    {
        private readonly PharmacyOrderService _pharmacyOrderService;

        public PharmacyOrderController(PharmacyOrderService pharmacyOrderService)
        {
            _pharmacyOrderService = pharmacyOrderService;
        }

        // Create a new pharmacy order
        [HttpPost("create")]
        public async Task<IActionResult> CreatePharmacyOrder([FromBody] PharmacyOrder order)
        {
            if (order == null) return BadRequest("Invalid data.");
            int result = await _pharmacyOrderService.AddPharmacyOrder(order);
            return result > 0 ? Ok("Pharmacy order created successfully.") : StatusCode(500, "Error creating pharmacy order.");
        }

        // Get all pharmacy orders
        [HttpGet("all")]
        public async Task<ActionResult<List<PharmacyOrder>>> GetAllPharmacyOrders()
        {
            return await _pharmacyOrderService.GetAllPharmacyOrders();
        }

        // Get pharmacy order by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PharmacyOrder>> GetPharmacyOrderById(int id)
        {
            var order = await _pharmacyOrderService.GetPharmacyOrderById(id);
            return order != null ? Ok(order) : NotFound("Pharmacy order not found.");
        }

        // Update status of a pharmacy order
        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdatePharmacyOrderStatus(int id, [FromBody] int status)
        {
            bool updated = await _pharmacyOrderService.UpdatePharmacyOrderStatus(id, status);
            return updated ? Ok("Pharmacy order status updated successfully.") : StatusCode(500, "Error updating pharmacy order status.");
        }

        // Get pharmacy orders by branch ID
        [HttpGet("get-by-branch/{branchId}")]
        public async Task<ActionResult<List<PharmacyOrder>>> GetPharmacyOrdersByBranchID(int branchId)
        {
            return await _pharmacyOrderService.GetPharmacyOrdersByBranchID(branchId);
        }
    }
}
