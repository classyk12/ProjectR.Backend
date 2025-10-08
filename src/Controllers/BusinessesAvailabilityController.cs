using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class BusinessAvailabilityController : BaseController
    {
        private readonly IBusinessAvailabilityManager _manager;

        public BusinessAvailabilityController(IBusinessAvailabilityManager manager)
        {
            _manager = manager;
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusinessAvailabilityModel[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessAvailabilityModel[]))]
        [HttpGet("GetByBusiness/{Id:guid}")]
        public async Task<IActionResult> GetByBusiness(Guid Id, [FromQuery] bool includeAll = false)
        {
            BusinessAvailabilityModel[] result = await _manager.GetByBusinessId(Id, includeAll);
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<BusinessAvailabilityModel>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BusinessAvailabilityModel>))]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            ResponseModel<BusinessAvailabilityModel> result = await _manager.GetByIdAsync(id);
            return result.Status ? Ok(result) : BadRequest();
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<BusinessAvailabilityModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<BusinessAvailabilityModel>))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddBusinessAvailabilityModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            ResponseModel<BusinessAvailabilityModel> result = await _manager.AddAsync(model);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<BusinessAvailabilityModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<BusinessAvailabilityModel>))]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBusinessAvailabilityModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<BusinessAvailabilityModel> result = await _manager.UpdateAsync(id, model);
            return result.Status ? Ok(result) : BadRequest(result);
        }
    }
}
