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
    [FromServices] IMessageRepository messageRepository
    ) =>
{
    if (string.IsNullOrEmpty(message.Payload))
        return Results.BadRequest($"'{nameof(message.Payload)}' cannot be empty");

    await messageRepository.AddAsync(message);

    //TODO enviar msg via mensageria

    return Results.NoContent();
});

app.MapDefaultEndpoints();

await app.RunAsync();
