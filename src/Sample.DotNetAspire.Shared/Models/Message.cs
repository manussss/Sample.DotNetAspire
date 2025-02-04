namespace Sample.DotNetAspire.Shared.Models;

public class Message
{
    public string MessageId { get; set; } 
    public string CorrelationId { get; set; }
    public string Payload { get; set; }
    public DateTime CreatedAt { get; set; }

    public Message(string message)
    {
        MessageId = Guid.NewGuid().ToString();
        CorrelationId = Guid.NewGuid().ToString();
        Payload = message;
        CreatedAt = DateTime.Now;
    }
}
