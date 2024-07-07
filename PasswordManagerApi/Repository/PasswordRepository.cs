using Microsoft.EntityFrameworkCore;
using PasswordManagerApi.DTO;
using PasswordManagerApi.Entities;
using PasswordManagerApi.Services;
using System.Security.Cryptography.X509Certificates;

namespace PasswordManagerApi.Repository
{
    public class PasswordRepository 
    {
        private ApplicationContext _context;
        private EncryptionService _encryptionService;
        public PasswordRepository(ApplicationContext applicationContext, EncryptionService encryptionService) 
        {
            _context = applicationContext;
            _encryptionService = encryptionService;
        }

        public async Task<Password> GetPasswordAsync(string telegramId, string service)
        {
            var password = await _context.Passwords.Where(x => x.TelegramUserId == telegramId && x.PasswordServiceName == service).FirstOrDefaultAsync();
            if (password != null)
            {
                password.EncryptedPassword = _encryptionService.Decrypt(password.EncryptedPassword);
            }
            return password;
        }

        public async Task CreatePasswordAsync(Password password)
        {
            try
            {
                password.EncryptedPassword = _encryptionService.Encrypt(password.EncryptedPassword);
                await _context.Passwords.AddAsync(password);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { 
                
            }
        }

        public async Task<Dictionary<int, string>> GetUserServicesAsync(string telegramId)
        {
            var dict = new Dictionary<int, string>();
            var services = _context.Passwords.Where(x => x.TelegramUserId == telegramId).Select(x=> x.PasswordServiceName).ToList();
            for(int i = 0; i < services.Count(); i++)
            {
                if (!dict.ContainsKey(i))
                {
                    dict.Add(i, services[i]);
                }
            }
            return dict;
        }

        public async Task<Password?> DeletePasswordAsync(string telegramId, string service, string login)
        {
            var itemToDelete = await _context.Passwords
                .Where(x => x.TelegramUserId == telegramId && x.PasswordServiceName == service && x.Login == login).FirstOrDefaultAsync();
            if (itemToDelete != null) {
                _context.Passwords.Remove(itemToDelete);
                await _context.SaveChangesAsync();
            }
            return itemToDelete;
        }

        public async Task<Password?> PutPasswordAsync(int passwordId, PasswordDTO passwordDTO)
        {
            var putPassword = await _context.Passwords
                .Where(x => x.Id == passwordId)
                .FirstOrDefaultAsync();
            if (putPassword != null) { 
                putPassword.Login = passwordDTO.Login;
                putPassword.PasswordServiceName = passwordDTO.PasswordServiceName;
                putPassword.EncryptedPassword = passwordDTO.EncryptedPassword;
                await _context.SaveChangesAsync();
            }
            return putPassword;
        }
    }
}
