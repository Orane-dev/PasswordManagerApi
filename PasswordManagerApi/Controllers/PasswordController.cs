using Microsoft.AspNetCore.Mvc;
using PasswordManagerApi.Entities;
using PasswordManagerApi.Repository;
using PasswordManagerApi.DTO;
using System.ComponentModel.DataAnnotations;

namespace PasswordManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : Controller
    {
        private readonly PasswordRepository _repository;

        public PasswordController(PasswordRepository passwordRepository) 
        {
            _repository = passwordRepository;
        }

        [HttpGet("GetPassword")]
        public async Task<ActionResult<PasswordDTO>> GetPassword(string telegramId, string service)
        {
            try
            {
                var password = await _repository.GetPasswordAsync(telegramId, service);

                if(password == null)
                {
                    return NotFound(new {Message = "Password not found for the given input data."});
                }

                var passwordDTO = new PasswordDTO
                {
                    Id = password.Id,
                    TelegramUserId = password.TelegramUserId,
                    Login = password.Login,
                    PasswordServiceName = password.PasswordServiceName,
                    EncryptedPassword = password.EncryptedPassword,
                    CreateTime = DateTime.UtcNow,

                };
                return Ok(passwordDTO);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new {Message = $"Internal server error while processing get request: {ex.Message}",});
            }
            
        }

        [HttpPost("CreatePassword")]
        public async Task<ActionResult> CreatePassword([FromBody] PasswordDTO passwordDTO)
        {
            try
            {
                var password = new Password
                {
                    Login = passwordDTO.Login,
                    EncryptedPassword = passwordDTO.EncryptedPassword,
                    TelegramUserId = passwordDTO.TelegramUserId,
                    PasswordServiceName = passwordDTO.PasswordServiceName,
                    CreateTime = DateTime.UtcNow,
                };
                await _repository.CreatePasswordAsync(password);
                return CreatedAtAction(nameof(GetPassword), new { telegramId = password.TelegramUserId, service = password.PasswordServiceName }, passwordDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error while processing post request: {ex.Message}",});
            }
            
        }

        [HttpDelete("DeletePassword")]
        public async Task<ActionResult> DeletePassword(string telegramId, string service, string login)
        {
            try
            {
                var deleted = await _repository.DeletePasswordAsync(telegramId, service, login);
                if (deleted == null)
                {
                    return NotFound((new { Message = "Password not found for the given input data." }));
                }
                return NoContent();
            }
            catch (Exception ex) {
                return StatusCode(500, new { Message = $"Internal server error while processing delete request: {ex.Message}", });
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutPassword(int passwordId, PasswordDTO passwordDTO)
        {
            try
            {
                var putPassword = await _repository.PutPasswordAsync(passwordId, passwordDTO);
                if (putPassword == null)
                {
                    return NotFound((new { Message = "Password not found for the given input data." }));
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal server error while processing put request: {ex.Message}", });
            }
        }


        [HttpGet("GetUserServices")]
        public async Task<Dictionary<int, string>> GetUserPasswordServices(string telegramId)
        {
            var userServices = await _repository.GetUserServicesAsync(telegramId);
            return userServices;
        }
        
    }
}
