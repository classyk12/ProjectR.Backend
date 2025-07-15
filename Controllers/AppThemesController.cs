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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            AppThemeModel[] result = await _appThemeManager.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            ResponseModel<AppThemeModel> result = await _appThemeManager.GetByIdAsync(id: id);
            return result.Status ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AppThemeModel appTheme)
        {
            if (appTheme == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<AppThemeModel> result = await _appThemeManager.AddAsync(appTheme);
            return result.Status ? Ok(result) : NotFound(result);
        }

        [HttpPost("AddMany")]
        public async Task<IActionResult> AddMany([FromBody] AppThemeModel[] appThemes)
        {
            if (appThemes.Length == 0 || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseModel<AppThemeModel[]> result = await _appThemeManager.AddAsync(appThemes);
            return result.Status ? Ok(result) : NotFound(result);
        }
    }
}
