using Microsoft.AspNetCore.Mvc;
using PasswordManagerApi.BL.Interfaces;
using PasswordManagerApi.Exceptions;
using PasswordManagerApi.Repository.Implementation;
using PasswordManagerApi.Repository.Interfaces;

namespace PasswordManagerApi.BL.Implementation
{
    public class ServiceBL : IServiceBL
    {
        private IServiceRepository _userRepository;
        public ServiceBL(IServiceRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task<Dictionary<int, string>> GetUserServices(string telegramId)
        {
            var services = await _userRepository.GetUserServicesAsync(telegramId);
            if (!services.Any())
            {
                throw new NotFoundException("User services not found");
                
            }

            return services;

        }
    }
}
