using Microsoft.AspNetCore.Mvc;
using PasswordManagerApi.BL.Interfaces;
using PasswordManagerApi.DTO;
using PasswordManagerApi.Exceptions;
using PasswordManagerApi.Repository.Implementation;

namespace PasswordManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : Controller
    {
        private IPasswordBL _passwordBL;

        public PasswordController(IPasswordBL passwordBL)
        {
            _passwordBL = passwordBL;
        }

        [HttpGet]
        public async Task<ActionResult<List<PasswordDTO>>> GetPassword(string telegramId, string service)
        {
            try
            {
                var passwords = await _passwordBL.GetPassword(telegramId, service);
                return Ok(passwords);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error: " + ex.Message });
            }

        }

        [HttpPost]
        public async Task<ActionResult> CreatePassword([FromBody] CreatePasswordDTO passwordDTO)
        {
            try
            {
                await _passwordBL.CreatePassword(passwordDTO);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error: " + ex.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePassword(string telegramId, string service, string login)
        {
            try
            {
                await _passwordBL.DeletePassword(telegramId, service, login);
                return NoContent();
            }
            catch (NotFoundException ex) 
            { 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex);
            }
        }

        [HttpPut]
            public async Task<ActionResult> PutPassword(CreatePasswordDTO passwordDTO)
            {
                try
                {
                    await _passwordBL.PutPassword(passwordDTO);
                    return NoContent();
                }
                catch (NotFoundException ex) 
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex) 
                { 
                    return StatusCode(500, "Internal server error: " + ex.Message);
                }
            }

            //TO DO 
            // Упаковка паролей 

            // Импорт паролей

        }
    }

