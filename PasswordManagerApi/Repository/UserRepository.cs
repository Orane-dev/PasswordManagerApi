using PasswordManagerApi.Entities;
namespace PasswordManagerApi.Repository
{
    public class UserRepository
    {
        ApplicationContext _context;
        public UserRepository(ApplicationContext applicationContext) {
            applicationContext = _context;
        }

        public async Task RegisterUserAsync(string telegramId)
        {
            await _context.TelegramUsers.AddAsync(new TelegramUser { TelegramId = telegramId, Role = (int)ServiceRole.RoleType.User });
            await _context.SaveChangesAsync();
        }
    }
}
