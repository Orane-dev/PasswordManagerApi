using Microsoft.AspNetCore.Mvc;
using PasswordManagerApi.BL.Interfaces;
using PasswordManagerApi.DTO;
using PasswordManagerApi.Entities;
using PasswordManagerApi.Exceptions;
using PasswordManagerApi.Repository.Interfaces;

namespace PasswordManagerApi.BL.Implementation
{
    public class PasswordBL : IPasswordBL
    {
        private readonly IPasswordRepository _passwordRepository;
        private readonly IServiceRepository _serviceRepository;
        public PasswordBL(IPasswordRepository passwordRepository, IServiceRepository serviceRepository)
        {
            _passwordRepository = passwordRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<List<PasswordDTO>?> GetPassword(string telegramId, string service)
        {
            var passwordList = await _passwordRepository.GetPasswordAsync(telegramId, service);

            if (!passwordList.Any())
            {
                throw new NotFoundException("Password not found for the given input data.");
            }

            List<PasswordDTO> passwordDTOs = new List<PasswordDTO>();

            foreach (var password in passwordList)
            {
                passwordDTOs.Add(password.ToPasswordDTO());
            }

            return passwordDTOs;
        }

        public async Task CreatePassword(CreatePasswordDTO passwordDTO)
        {
            var serviceId = await _serviceRepository.GetServiceIdAsync(passwordDTO.ServiceName);

            if (serviceId == 0)
            {
                serviceId = await _serviceRepository.CreateServiceAsync(passwordDTO.ServiceName);
            }

            var password = new Password
            {
                Login = passwordDTO.Login,
                EncryptedPassword = passwordDTO.DecryptedPassword,
                TelegramUserId = passwordDTO.TelegramUserId,
                ServiceId = serviceId,
                CreateTime = DateTime.UtcNow,
            };

            await _passwordRepository.CreatePasswordAsync(password);
        }

        public async Task DeletePassword(string telegramId, string service, string login)
        {

            var deleted = await _passwordRepository.DeletePasswordAsync(telegramId, service, login);
            if (deleted == null)
            {
                throw new NotFoundException("Password not found for the given input data.");
            }
        }

        public async Task PutPassword(CreatePasswordDTO passwordDTO)
        {
            var putPassword = await _passwordRepository.PutPasswordAsync(passwordDTO);
            if (putPassword == null)
            {
                throw new NotFoundException("Password not found for the given input data.");
            }
        }
    }
}
