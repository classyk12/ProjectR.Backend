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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<PhoneNumberLoginResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<PhoneNumberLoginResponseModel>))]
        [HttpPost("WithPhoneNumber")]
        public async Task<IActionResult> AuthenticateWithPhoneNumber([FromBody] LoginWithPhoneNumberModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<PhoneNumberLoginResponseModel> result = await _authManager.AuthenticateWithPhoneNumberAsync(model);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// This endppint is used to authenticate a user that has requested to login with phone number.
        /// It validates the OTP and returns a response model containing the login details.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Successful Authentication Ack</returns>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<LoginResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<LoginResponseModel>))]
        [HttpPost("CompletePhoneNumberLogin")]
        public async Task<IActionResult> CompletePhoneNumberLogin([FromBody] CompleteLoginWithPhoneNumberModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<LoginResponseModel> result = await _authManager.CompletePhoneNumberAuthenticationAsync(model);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<LoginResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<LoginResponseModel>))]
        [HttpPost("WithSocial")]
        public async Task<IActionResult> AuthenticateWithSocialMedia([FromBody] LoginWithSocialModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<LoginResponseModel> result = await _authManager.AuthenticateWithSocialAsync(model);
            return result.Status ? Ok(result) : BadRequest(result);
        }
    }
}
