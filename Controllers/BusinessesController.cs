using Microsoft.AspNetCore.Mvc;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Controllers
{
    [Route("api/[controller]")]
    public class BusinessesController : Controller
    {
        private readonly IBusinessManager _businessManager;

        public BusinessesController(IBusinessManager businessManager)
        { 
            _businessManager = businessManager;
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BusinessModel[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BusinessModel[]))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            BusinessModel[] result = await _businessManager.GetAllAsync();
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<BusinessModel>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BusinessModel>))]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            ResponseModel<BusinessModel> result = await _businessManager.GetByIdAsync(id);
            return result.Status ? Ok(result) : BadRequest();
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<BusinessModel>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<BusinessModel>))]
        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            ResponseModel<BusinessModel> result = await _businessManager.GetBySlugAsync(slug);
            return result.Status ? Ok(result) : BadRequest();
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<BusinessModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<BusinessModel>))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddBusinessModel business)
        {
            if (business == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<BusinessModel> result = await _businessManager.AddAsync(business);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<BusinessModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<BusinessModel>))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BusinessModel business)
        {
            if (business == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<BusinessModel> result = await _businessManager.UpdateAsync(business);
            return result.Status ? Ok(result) : BadRequest(result);

        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<BusinessModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<BusinessModel>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            BaseResponseModel result = await _businessManager.DeleteAsync(id);
            return result.Status ? Ok(result) : BadRequest(result);

        }
    }
}
