using Microsoft.AspNetCore.Mvc;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Controllers
{
    [Route("api/[controller]")]
    public class AppThemesController : Controller
    {
        private readonly IAppThemeManager _appThemeManager;

        public AppThemesController(IAppThemeManager appThemeManager)
        {
            _appThemeManager = appThemeManager;
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AppThemeModel[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AppThemeModel[]))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            AppThemeModel[] result = await _appThemeManager.GetAllAsync();
            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<AppThemeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<AppThemeModel>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            ResponseModel<AppThemeModel> result = await _appThemeManager.GetByIdAsync(id: id);
            return result.Status ? Ok(result) : NotFound(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<AppThemeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<AppThemeModel>))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddAppThemeModel appTheme)
        {
            if (appTheme == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<AppThemeModel> result = await _appThemeManager.AddAsync(appTheme);
            return result.Status ? Ok(result) : NotFound(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<AppThemeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<AppThemeModel>))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AppThemeModel[] appThemes)
        {
            if (appThemes.Length == 0 || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<AppThemeModel[]> result = await _appThemeManager.UpdateAsync(appThemes);
            return result.Status ? Ok(result) : NotFound(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseModel<AppThemeModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel<AppThemeModel>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            BaseResponseModel result = await _appThemeManager.DeleteAsync(id);
            return result.Status ? Ok(result) : NotFound(result);
        }
    }
}
