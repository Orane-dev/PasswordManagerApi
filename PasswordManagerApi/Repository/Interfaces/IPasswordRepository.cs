using PasswordManagerApi.DTO;
using PasswordManagerApi.Entities;

namespace PasswordManagerApi.Repository.Interfaces
{
    public interface IPasswordRepository
    {
        Task<List<Password>> GetPasswordAsync(string telegramId, string service);
        Task CreatePasswordAsync(Password password);
        Task<Password?> DeletePasswordAsync(string telegramId, string service, string login);
        Task<Password?> PutPasswordAsync(CreatePasswordDTO passwordDTO);
    }
}
