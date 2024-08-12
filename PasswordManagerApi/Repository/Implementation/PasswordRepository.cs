using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PasswordManagerApi.DTO;
using PasswordManagerApi.Entities;
using PasswordManagerApi.Repository.Interfaces;
using PasswordManagerApi.Services;

namespace PasswordManagerApi.Repository.Implementation
{
    public class PasswordRepository : IPasswordRepository
    {
        private ApplicationContext _context;
        private IEncryptionService _encryptionService;
        public PasswordRepository(ApplicationContext applicationContext, IEncryptionService encryptionService)
        {
            _context = applicationContext;
            _encryptionService = encryptionService;
        }

        public async Task<List<Password>> GetPasswordAsync(string telegramId, string service)
        {
            var passwordList = await _context.Passwords
                .Include(p => p.Service)
                .Where(p => p.TelegramUserId == telegramId && p.Service.Name == service)
                .ToListAsync();

            if (passwordList != null)
            {
                passwordList.ForEach(x =>
                    x.EncryptedPassword = _encryptionService.Decrypt(x.EncryptedPassword));
            }

            return passwordList;
        }

        public async Task CreatePasswordAsync(Password password)
        {

            password.EncryptedPassword = _encryptionService.Encrypt(password.EncryptedPassword);
            await _context.Passwords.AddAsync(password);
            await _context.SaveChangesAsync();

        }

        public async Task<Password?> DeletePasswordAsync(string telegramId, string service, string login)
        {
            var deleteItem = await _context.Passwords
                .Include(p => p.Service)
                .Where(p => p.TelegramUserId == telegramId && p.Service.Name == service && p.Login == login)
                .FirstOrDefaultAsync();

            if (deleteItem != null)
            {
                _context.Passwords.Remove(deleteItem);
                await _context.SaveChangesAsync();
            }

            return deleteItem;
        }

        public async Task<Password?> PutPasswordAsync(CreatePasswordDTO passwordDTO)
        {
            var putItem = await _context.Passwords
                .Include(p => p.Service)
                .Where(p =>
                        p.TelegramUserId == passwordDTO.TelegramUserId &&
                        p.Service.Name == passwordDTO.ServiceName &&
                        p.Login == passwordDTO.Login
                ).FirstOrDefaultAsync();

            if (putItem != null)
            {
                putItem.EncryptedPassword = _encryptionService.Encrypt(passwordDTO.DecryptedPassword);
                await _context.SaveChangesAsync();
            }
            return putItem;
        }
    }
}
