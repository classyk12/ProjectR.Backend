using Microsoft.AspNetCore.Mvc;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserModel[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UserModel[]))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            UserModel[] result = await _userManager.GetAllAsync();
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<UserModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<UserModel>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            ResponseModel<UserModel> result = await _userManager.GetByIdAsync(id: id);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<UserModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<UserModel>))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserModel userModel)
        {
            if (userModel == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<UserModel> result = await _userManager.AddAsync(userModel);
            return result.Status ? Ok(result) : BadRequest();
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<UserModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<UserModel>))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserModel userModel)
        {
            if (userModel == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<UserModel> result = await _userManager.UpdateAsync(userModel);
            return result.Status ? Ok(result) : BadRequest();
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<UserModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<UserModel>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            BaseResponseModel? result = await _userManager.DeleteAsync(id);
            return result!.Status ? Ok(result) : BadRequest();
        }
    }
}