namespace API.Service.Interface
{
    public interface IEncryptionService
    {
        string Encrypt<T>(T data);
        T Decrypt<T>(string protectedDataString);
    }
}