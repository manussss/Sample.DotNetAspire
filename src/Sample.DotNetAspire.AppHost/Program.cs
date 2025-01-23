var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.Sample_DotNetAspire_ApiService>("apiservice")
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.Sample_DotNetAspire_FirstWorker>("sample-dotnetaspire-firstworker")
    .WithReference(apiService);

builder.AddProject<Projects.Sample_DotNetAspire_SecondWorker>("sample-dotnetaspire-secondworker");

builder.Build().Run();
