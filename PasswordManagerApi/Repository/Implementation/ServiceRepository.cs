using Microsoft.EntityFrameworkCore;
using PasswordManagerApi.Entities;
using PasswordManagerApi.Repository.Interfaces;

namespace PasswordManagerApi.Repository.Implementation
{
    public class ServiceRepository : IServiceRepository
    {
        public ApplicationContext _context;
        public ServiceRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<int, string>> GetUserServicesAsync(string telegramId)
        {
            var dict = new Dictionary<int, string>();

            var services = await _context.Passwords
                .Include(p => p.Service)
                .Where(p => p.TelegramUserId == telegramId)
                .Select(p => p.Service.Name)
                .Distinct()
                .ToListAsync();

            for (int i = 0; i < services.Count(); i++)
            {
                if (!dict.ContainsKey(i))
                {
                    dict.Add(i, services[i]);
                }
            }
            return dict;
        }

        public async Task<int> CreateServiceAsync(string serviceName)
        {
            var serivce = new PasswordService
            {
                Name = serviceName,
            };

            await _context.PasswordServices.AddAsync(serivce);

            await _context.SaveChangesAsync();
            return serivce.Id;
        }

        public async Task<int> GetServiceIdAsync(string serviceName)
        {
            var serviceId = await _context.PasswordServices
                .Where(p => p.Name == serviceName)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();

            return serviceId;
        }
    }
}
