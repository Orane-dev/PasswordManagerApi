using Microsoft.AspNetCore.Mvc;
using PasswordManagerApi.BL.Interfaces;
using PasswordManagerApi.Entities;
using PasswordManagerApi.Exceptions;


namespace PasswordManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagmentController : Controller
    {
        private IManagmentBL _managmentBL;
        public ManagmentController(IManagmentBL managmentBL) 
        {
            _managmentBL = managmentBL;
        }
            
        [HttpPost]
        public async Task<ActionResult<TelegramUser>> RegisterUser([FromForm] string requestTelegramId, [FromForm]string telegramId)
        {
            try
            {
                var telegramUser = await _managmentBL.RegisterUser(requestTelegramId, telegramId);

                return CreatedAtAction(nameof(GetTelegramUser), new { telegramId = telegramUser.TelegramId }, telegramUser);

            }
            catch (InvalidPermissionException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { Message = "Internal server error:" + ex.Message});
            }
            
        }
        [HttpGet("GetTelegramUser")]
        public async Task<ActionResult<TelegramUser>> GetTelegramUser(string telegramId)
        {
            try
            {
                var telegramUser = await _managmentBL.GetTelegramUser(telegramId);
                return Ok(telegramUser);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error:" + ex.Message });
            }
            
        }
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<TelegramUser>>> GetAllUsers()
        {
            try
            {
                var telegramUsers = await _managmentBL.GetAllUsers();
                return Ok(telegramUsers);
            }
            catch (NotFoundException ex) 
            {
                return NotFound(new { Message = "User database is empty" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error:" + ex.Message);
            }
        }

        //TO DO
        // Удаление пользователя
    }
}
