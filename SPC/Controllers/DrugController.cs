using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPC.Models;
using SPC.Services;

namespace SPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        private readonly DrugService _drugService;

        public DrugController(DrugService drugService)
        {
            _drugService = drugService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDrug([FromBody] Drug drug)
        {
            var result = await _drugService.AddDrug(drug);
            return result > 0 ? Ok("Drug added successfully") : BadRequest("Failed to add drug");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllDrugs() => Ok(await _drugService.GetAllDrugs());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrugById(int id)
        {
            var drug = await _drugService.GetDrugById(id);
            return drug != null ? Ok(drug) : NotFound("Drug not found");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateDrug([FromBody] Drug drug)
        {
            return await _drugService.UpdateDrug(drug) ? Ok("Drug updated") : BadRequest("Update failed");
        }

        [HttpPut("update-stockin/{id}")]
        public async Task<IActionResult> UpdateStockIn(int id, [FromBody] int newStockIn)
        {
            var result = await _drugService.UpdateStockIn(id, newStockIn);
            return result ? Ok("Stock updated successfully") : BadRequest("Failed to update stock");
        }

    }
}
