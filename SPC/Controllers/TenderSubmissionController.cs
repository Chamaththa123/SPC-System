using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPC.Models;
using SPC.Services;

namespace SPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenderSubmissionController : ControllerBase
    {
        private readonly TenderSubmissionService _tenderSubmissionService;

        public TenderSubmissionController(TenderSubmissionService tenderSubmissionService)
        {
            _tenderSubmissionService = tenderSubmissionService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTenderSubmission([FromBody] TenderSubmission submission)
        {
            var result = await _tenderSubmissionService.AddTenderSubmission(submission);
            return result > 0 ? Ok("Added Successfully") : BadRequest("Failed to Add");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTenderSubmissions()  
        {
            var result = await _tenderSubmissionService.GetAllTenderSubmissions();
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTenderSubmission([FromBody] TenderSubmission submission)
        {
            var result = await _tenderSubmissionService.UpdateTenderSubmission(submission);
            return result ? Ok("Updated Successfully") : BadRequest("Failed to Update");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTenderSubmission(int id)
        {
            var result = await _tenderSubmissionService.DeleteTenderSubmission(id);
            return result ? Ok("Deleted Successfully") : BadRequest("Failed to Delete");
        }

        [HttpPut("status/{id}/{status}")]
        public async Task<IActionResult> UpdateTenderSubmissionStatus(int id, int status)
        {
            var result = await _tenderSubmissionService.UpdateTenderSubmissionStatus(id, status);
            return result ? Ok("Status Updated") : BadRequest("Failed to Update Status");
        }

        [HttpGet("by-supplier/{supplierId}")]
        public async Task<IActionResult> GetTenderSubmissionsBySupplierId(int supplierId)
        {
            var result = await _tenderSubmissionService.GetTenderSubmissionsBySupplierId(supplierId);
            return result.Any() ? Ok(result) : NotFound("No submissions found for this supplier");
        }

        [HttpGet("by-tender/{tenderId}")]
        public async Task<IActionResult> GetTenderSubmissionsByTenderId(int tenderId)
        {
            var result = await _tenderSubmissionService.GetTenderSubmissionsByTenderId(tenderId);
            return result.Any() ? Ok(result) : NotFound("No submissions found for this tender");
        }

        [HttpGet("active-by-drug/{drugId}")]
        public async Task<IActionResult> GetActiveTenderSubmissionsByDrugId(int drugId)
        {
            var result = await _tenderSubmissionService.GetActiveTenderSubmissionsByDrugId(drugId);
            return result.Any() ? Ok(result) : NotFound("No active submissions found for this drug");
        }



    }
}
