using Microsoft.AspNetCore.Mvc;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;
using ProjectR.Backend.Infrastructure.Managers;

namespace ProjectR.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndustryController : BaseController
    {
        private readonly IIndustryManager _industryManager;

        public IndustryController(IIndustryManager industryManager)
        {
            _industryManager = industryManager;
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<IndustryModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<IndustryModel>))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddIndustryModel industry)
        {

            if (industry == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<IndustryModel> result = await _industryManager.AddAsync(industry);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<IndustryModel>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<IndustryModel>))]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            ResponseModel<IndustryModel> result = await _industryManager.GetByIdAsync(id);
            return result.Status ? Ok(result) : BadRequest();
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<IndustryModel[]>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<IndustryModel[]>))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IndustryModel[] result = await _industryManager.GetAllAsync();
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<IndustryModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<IndustryModel>))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] IndustryModel industry)
        {
            if (industry == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<IndustryModel> result = await _industryManager.UpdateAsync(industry);
            return result.Status ? Ok(result) : BadRequest(result);

        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<IndustryModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<IndustryModel>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            BaseResponseModel result = await _industryManager.DeleteAsync(id);
            return result.Status ? Ok(result) : BadRequest(result);
        }
    }
}
