namespace PasswordManagerApi.Entities
{
    public class Password
    {
        public int Id { get; set; }
        public string TelegramUserId { get; set; }
        public TelegramUser TelegramUser { get; set; }
        public string PasswordServiceName { get; set; }
        public string Login {  get; set; }
        public string EncryptedPassword { get; set; }
        public DateTime CreateTime {  get; set; } 
    }
}
