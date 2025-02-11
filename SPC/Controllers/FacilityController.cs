using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPC.Models;
using SPC.Services;

namespace SPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly FacilityService _facilityService;

        public FacilityController(FacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Facility>>> GetAllFacility()
        {
            var facility = await _facilityService.GetAllFacility();
            return Ok(facility);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Facility>> GetFacilityById(int id)
        {
            var facility = await _facilityService.GetFacilityById(id);
            return facility == null ? NotFound() : Ok(facility);
        }

        [HttpPost]
        public async Task<IActionResult> AddFacility(Facility facility)
        {
            await _facilityService.AddFacility(facility);
            return Ok(new { Message = "Facility added successfully!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFacility(int id, Facility facility)
        {
            if (id != facility.idFacility) return BadRequest();
            await _facilityService.UpdateFacility(facility);
            return Ok(new { Message = "Facility updated successfully!" });
        }
    }
}
