using Microsoft.AspNetCore.Mvc;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Controllers
{
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
            AppThemeModel[] appThemes = await _appThemeManager.GetAllAsync();
            return Ok(appThemes);
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            AppThemeModel[] appThemes = await _appThemeManager.GetAllAsync();
            return Ok(appThemes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Add(Guid id)
        {
            AppThemeModel appThemes = await _appThemeManager.GetByIdAsync(id: id);
            return Ok(appThemes);
        }
    }
}
