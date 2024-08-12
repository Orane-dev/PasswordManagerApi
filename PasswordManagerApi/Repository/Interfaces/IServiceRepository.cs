namespace PasswordManagerApi.Repository.Interfaces
{
    public interface IServiceRepository
    {
        Task<Dictionary<int, string>> GetUserServicesAsync(string telegramId);
        Task<int> CreateServiceAsync(string serviceName);
        Task<int> GetServiceIdAsync(string serviceName);
    }
}
