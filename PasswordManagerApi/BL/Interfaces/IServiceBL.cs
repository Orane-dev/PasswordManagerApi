namespace PasswordManagerApi.BL.Interfaces
{
    public interface IServiceBL
    {
        Task<Dictionary<int, string>> GetUserServices(string telegramId);
    }
}
