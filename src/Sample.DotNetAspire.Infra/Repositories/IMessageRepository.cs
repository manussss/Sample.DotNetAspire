namespace Sample.DotNetAspire.Infra.Repositories;

public interface IMessageRepository
{
    Task AddAsync(Message message);
    Task<Message?> GetByIdAsync(string id);
    Task UpdateStatusAsync(string id, MessageStatus status);
}
