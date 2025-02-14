namespace Sample.DotNetAspire.IntegrationTests.Api;

public class MessageTests : IAsyncLifetime
{
    private DistributedApplication _application;
    private IDistributedApplicationTestingBuilder _appHost;

    public async Task InitializeAsync()
    {
        _appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.Sample_DotNetAspire_AppHost>();
        
        _application = await _appHost.BuildAsync();
        await _application.StartAsync();
    }

    public async Task DisposeAsync() => await _application.DisposeAsync();

    [Fact]
    public async Task GivenRequest_WhenPayloadInvalid_ThenReturnBadRequest()
    {
        //Arrange
        var httpClient = _application.CreateHttpClient("apiservice");

        // Act
        var response = await httpClient.PostAsJsonAsync("/api/v1/message", new Message());

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GivenRequest_WhenMessageExistsInCache_ThenReturnConflict()
    {
        //Arrange
        var httpClient = _application.CreateHttpClient("apiservice");

        // Act
        var validResponse = await httpClient.PostAsJsonAsync("/api/v1/message", new Message("test"));
        var conflictResponse = await httpClient.PostAsJsonAsync("/api/v1/message", new Message("test"));

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, validResponse.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, conflictResponse.StatusCode);
    }

    [Fact]
    public async Task GivenRequest_WhenValid_ThenSaveToDatabase()
    {
        //Arrange
        var httpClient = _application.CreateHttpClient("apiservice");

        // Act
        var response = await httpClient.PostAsJsonAsync("/api/v1/message", new Message("test message"));

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
