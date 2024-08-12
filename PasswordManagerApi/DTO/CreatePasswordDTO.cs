using System.ComponentModel.DataAnnotations;

namespace PasswordManagerApi.DTO
{
    public class CreatePasswordDTO
    {
        public int Id { get; set; }
        [Required]
        public string TelegramUserId { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string DecryptedPassword { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
