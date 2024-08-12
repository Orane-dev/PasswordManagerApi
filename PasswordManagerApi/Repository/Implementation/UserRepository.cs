using Microsoft.EntityFrameworkCore;
using PasswordManagerApi.Entities;
using PasswordManagerApi.Repository.Interfaces;
namespace PasswordManagerApi.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        ApplicationContext _context;
        public UserRepository(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<TelegramUser> GetTelegramUserAsync(string telegramId)
        {
            var telegramUser = await _context.TelegramUsers.FirstOrDefaultAsync(x => x.TelegramId == telegramId);
            if (telegramUser != null)
            {
                return telegramUser;
            }
            return null;
        }

        public async Task<TelegramUser> RegisterUserAsync(string telegramId)
        {
            var newUser = new TelegramUser
            {
                TelegramId = telegramId,
                Role = (int)ServiceRole.RoleType.User

            };
            await _context.TelegramUsers.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<int?> GetUserRoleAsync(string telegramId)
        {
            var telegramUser = await _context.TelegramUsers.FirstOrDefaultAsync(x => x.TelegramId == telegramId);
            if (telegramUser != null)
            {
                return telegramUser.Role;
            }
            return null;
        }

        public async Task<List<TelegramUser>> GetAllUsersAsync()
        {
            var telegramUsers = await _context.TelegramUsers.ToListAsync();
            return telegramUsers;
        }
    }
}
