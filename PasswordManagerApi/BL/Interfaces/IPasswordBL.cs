using Microsoft.AspNetCore.Mvc;
using PasswordManagerApi.DTO;

namespace PasswordManagerApi.BL.Interfaces
{
    public interface IPasswordBL
    {
        Task<List<PasswordDTO>?> GetPassword(string telegramId, string service);
        Task CreatePassword(CreatePasswordDTO passwordDTO);
        Task DeletePassword(string telegramId, string service, string login);
        Task PutPassword(CreatePasswordDTO passwordDTO);
    }
}
