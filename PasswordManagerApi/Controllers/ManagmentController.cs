using Microsoft.AspNetCore.Mvc;
using PasswordManagerApi.Repository;

namespace PasswordManagerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagmentController : Controller
    {
        private UserRepository _userRepository;
        public ManagmentController(UserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        [HttpGet("RegisterUser")]
        public async Task<IActionResult> RegisterUser(string telegramId)
        {
            await _userRepository.RegisterUserAsync(telegramId);
            return Ok();
        }
    }
}
