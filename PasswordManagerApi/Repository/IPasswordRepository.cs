using PasswordManagerApi.Entities;

namespace PasswordManagerApi.Repository
{
    public interface IPasswordRepository
    {
        Task<Password> GetPasswordAsync(string telegramId, string service);
        Task CreatePasswordAsync(string telegramId, string service, string encryptedPassword);
        Task DeletePasswordAsync(string telegramId, string passwordId);
        Task UpdatePasswordAsync(string telegram, Password password);
    }
}
