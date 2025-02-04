namespace Sample.DotNetAspire.Shared.Models;

public class Message
{
    public string MessageId { get; set; } 
    public string CorrelationId { get; set; }
    public string Payload { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<MessageStatus> Status { get; set; }

    public Message(string message)
    {
        MessageId = Guid.NewGuid().ToString();
        CorrelationId = Guid.NewGuid().ToString();
        Payload = message;
        CreatedAt = DateTime.Now;
    }

    public Message()
    {
        
    }
}

public class MessageStatus
{
    public EStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public MessageStatus(EStatus status)
    {
        Status = status;
        CreatedAt = DateTime.Now;
    }

    public MessageStatus()
    {
        
    }
}

public enum EStatus
{
    Processing = 0,
    Processed = 1,
    Error = 2
}
