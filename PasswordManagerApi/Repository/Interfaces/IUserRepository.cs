using PasswordManagerApi.Entities;

namespace PasswordManagerApi.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<TelegramUser> GetTelegramUserAsync(string telegramId);
        Task<TelegramUser> RegisterUserAsync(string telegramId);
        Task<int?> GetUserRoleAsync(string telegramId);
        Task<List<TelegramUser>> GetAllUsersAsync();
    }
}
