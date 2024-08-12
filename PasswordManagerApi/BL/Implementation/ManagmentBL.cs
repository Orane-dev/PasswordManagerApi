using Microsoft.AspNetCore.Mvc;
using PasswordManagerApi.BL.Interfaces;
using PasswordManagerApi.Entities;
using PasswordManagerApi.Exceptions;
using PasswordManagerApi.Repository.Interfaces;

namespace PasswordManagerApi.BL.Implementation
{
    public class ManagmentBL : IManagmentBL
    {
        private IUserRepository _userRepository;
        public ManagmentBL(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }
        public async Task<TelegramUser> RegisterUser(string requestTelegramId, string telegramId)
        {
            var telegramUser = await _userRepository.GetTelegramUserAsync(requestTelegramId);
            if (telegramUser.Role == (int)ServiceRole.RoleType.Admin)
            {
                var newUser = await _userRepository.RegisterUserAsync(telegramId);
                return telegramUser;
            }
            else
            {
                throw new InvalidPermissionException("You dont have permission for this action");
            }
            
        }

        public async Task<TelegramUser> GetTelegramUser(string telegramId)
        {
            var telegramUser = await _userRepository.GetTelegramUserAsync(telegramId);
            if (telegramUser != null)
            {
                return telegramUser;
            }
            throw new NotFoundException("User dont found");
        }

        public async Task<List<TelegramUser>> GetAllUsers()
        {
            var serviceUsers = await _userRepository.GetAllUsersAsync();
            if (serviceUsers.Any())
            {
                return serviceUsers;
            }
            else
            {
                throw new NotFoundException("User database is empty");
            }
        }
    }
}
