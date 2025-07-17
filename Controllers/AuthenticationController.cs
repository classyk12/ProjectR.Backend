using Microsoft.AspNetCore.Mvc;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationsController : Controller
    {
        private readonly IAuthManager _authManager;

        public AuthenticationsController(IAuthManager authManager)
        {
            _authManager = authManager ?? throw new ArgumentNullException(nameof(authManager));
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<AppThemeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<AppThemeModel>))]
        [HttpPost("ByPhoneNumber")]
        public async Task<IActionResult> AuthenticateWithPhoneNumber([FromBody] LoginWithPhoneNumberModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<PhoneNumberLoginResponseModel> result = await _authManager.AuthenticateWithPhoneNumberAsync(model);
            return result.Status ? Ok(result) : NotFound(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<AppThemeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<AppThemeModel>))]
        [HttpPost("BySocial")]
        public async Task<IActionResult> AuthenticateWithPhoneNumber([FromBody] LoginWithSocialModel  model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<LoginResponseModel> result = await _authManager.AuthenticateWithSocialAsync(model);
            return result.Status ? Ok(result) : NotFound(result);
        }
    }
}
