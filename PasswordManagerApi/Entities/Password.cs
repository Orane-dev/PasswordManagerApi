using PasswordManagerApi.DTO;

namespace PasswordManagerApi.Entities
{
    public class Password
    {
        public int Id { get; set; }
        public string TelegramUserId { get; set; }
        public TelegramUser TelegramUser { get; set; }
        public int ServiceId { get; set; }
        public PasswordService Service { get; set; }
        public string Login {  get; set; }
        public string EncryptedPassword { get; set; }
        public DateTime CreateTime {  get; set; }
        
        public PasswordDTO ToPasswordDTO()
        {
            return new PasswordDTO
            {
                Id = this.Id,
                TelegramUserId = this.TelegramUserId,
                ServiceId = this.ServiceId,
                Login = this.Login,
                DecryptedPassword = this.EncryptedPassword,
                CreateTime = this.CreateTime
            };
        }
    }
}
