var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/api/v1/message", async (
    [FromBody] Message message,
    [FromServices] IMessageRepository messageRepository,
    [FromServices] IConnectionMultiplexer connectionMux
    ) =>
{
    if (string.IsNullOrEmpty(message.Payload))
        return Results.BadRequest($"'{nameof(message.Payload)}' cannot be empty");

    await messageRepository.AddAsync(message);

    var db = connectionMux.GetDatabase();
    var messageKey = $"message:{message.Payload}";

    var cachedMessage = await db.StringGetAsync(messageKey);
    if (cachedMessage.HasValue)
    {
        return Results.Conflict("Message already exists in cache.");
    }

    await db.StringSetAsync(messageKey, System.Text.Json.JsonSerializer.Serialize(message), TimeSpan.FromMinutes(1));

    //TODO enviar msg via mensageria

    return Results.NoContent();
});

app.MapDefaultEndpoints();

await app.RunAsync();
