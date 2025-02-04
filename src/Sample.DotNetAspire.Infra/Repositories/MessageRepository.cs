namespace Sample.DotNetAspire.Infra.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly IMongoCollection<Message> _messages;

    public MessageRepository(IMongoClient client)
    {
        _messages = client.GetDatabase("default").GetCollection<Message>("Messages");
    }

    public async Task AddAsync(Message message)
    {
        await _messages.InsertOneAsync(message);
    }

    public async Task<Message?> GetByIdAsync(string id)
    {
        return (await _messages.FindAsync<Message>(
            Builders<Message>.Filter.And(Builders<Message>.Filter.Eq(m => m.MessageId, id)))).FirstOrDefault();
    }

    public async Task UpdateStatusAsync(string id, MessageStatus status)
    {
        var filter = Builders<Message>.Filter.And(Builders<Message>.Filter.Eq(m => m.MessageId, id));
        var updateDefinition = Builders<Message>.Update.AddToSet(m => m.Status, status);

        await _messages.FindOneAndUpdateAsync(filter, updateDefinition);
    }
}
