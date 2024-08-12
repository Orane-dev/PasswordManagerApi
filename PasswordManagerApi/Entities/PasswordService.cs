namespace PasswordManagerApi.Entities
{
    public class PasswordService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Password> Passwords { get; set; }
    }
}
