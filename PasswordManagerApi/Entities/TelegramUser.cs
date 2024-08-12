using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PasswordManagerApi.Entities
{
    public class TelegramUser
    {
        [Key]
        public string TelegramId { get; set; }
        public int Role { get; set; }
        public ICollection<Password> Passwords { get; set; }
    }
}
