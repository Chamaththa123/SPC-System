using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPC.Models;
using SPC.Services;

namespace SPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
        private readonly TenderService _tenderService;

        public TenderController(TenderService tenderService)
        {
            _tenderService = tenderService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTender([FromBody] Tender tender)
        {
            var result = await _tenderService.AddTender(tender);
            return result > 0 ? Ok("Tender added successfully") : BadRequest("Failed to add tender");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTenders()
        {
            var tenders = await _tenderService.GetAllTenders();
            return Ok(tenders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTenderById(int id)
        {
            var tender = await _tenderService.GetTenderById(id);
            return tender != null ? Ok(tender) : NotFound("Tender not found");
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateTender([FromBody] Tender tender)
        {
            var result = await _tenderService.UpdateTender(tender);
            return result > 0 ? Ok("Tender updated successfully") : BadRequest("Failed to update tender");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTender(int id)
        {
            var result = await _tenderService.DeleteTender(id);
            return result > 0 ? Ok("Tender deleted successfully") : BadRequest("Failed to delete tender");
        }

        [HttpPut("toggle-status/{id}")]
        public async Task<IActionResult> ToggleTenderStatus(int id)
        {
            var result = await _tenderService.ToggleTenderStatus(id);
            return result > 0 ? Ok("Tender status updated") : BadRequest("Failed to update status");
        }
    }
}
