using Microsoft.AspNetCore.Mvc;
using PasswordManagerApi.Entities;

namespace PasswordManagerApi.BL.Interfaces
{
    public interface IManagmentBL
    {
        Task<TelegramUser> RegisterUser(string requestTelegramId, string telegramId);
        Task<TelegramUser> GetTelegramUser(string telegramId);
        Task<List<TelegramUser>> GetAllUsers();
    }
}
