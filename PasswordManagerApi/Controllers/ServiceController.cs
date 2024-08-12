using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PasswordManagerApi.BL.Interfaces;
using PasswordManagerApi.Exceptions;
using PasswordManagerApi.Repository.Implementation;
using PasswordManagerApi.Repository.Interfaces;

namespace PasswordManagerApi.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {
        private IServiceBL _serviceBL;
        public ServiceController(IServiceBL serviceBL) 
        {
            _serviceBL = serviceBL;
        }
        [HttpGet]
        public async Task<ActionResult<Dictionary<int,string>>> GetUserServices(string telegramId)
        {
            try
            {
                var services = await _serviceBL.GetUserServices(telegramId);
                return Ok(services);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new {Message = ex.Message});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

        }
    }
}
