using Microsoft.AspNetCore.Mvc;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndustryController : ControllerBase
    {
        private readonly IIndustryManager _manager;

        public IndustryController(IIndustryManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IndustryModel industry)
        {
            var id = await _manager.CreateIndustryAsync(industry.Name, industry.Description);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var industry = await _manager.GetIndustryByIdAsync(id);
            if (industry == null)
            {
                return NotFound();
            }
            return Ok(industry);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var industries = await _manager.GetAllIndustriesAsync();
            return Ok(industries);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] IndustryModel industry)
        {
            await _manager.UpdateIndustryAsync(id, industry.Name, industry.Description);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _manager.DeleteIndustryAsync(id);
            return NoContent();
        }
    }
}
