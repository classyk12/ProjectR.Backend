using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ProjectR.Backend.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        public string ErrorString => "An error occured";

        public BaseController()
        {
        }

        public string TraceId => Activity.Current?.Id ?? HttpContext.TraceIdentifier;

        /// <summary>
        /// ID of the User from the current session
        /// </summary>
        public Guid UserId
        {
            get
            {
                string? claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                try
                {
                    if (claim != null && Guid.TryParse(claim, out Guid result))
                    {
                        return result;
                    }

                    throw new Exception("User does not exist");
                }

                catch
                {
                    throw new Exception("User does not exist");
                }
            }
        }
    }
}
