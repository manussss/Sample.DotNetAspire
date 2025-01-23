var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

builder.AddProject<Projects.Sample_DotNetAspire_ApiService>("apiservice");

builder.Build().Run();
