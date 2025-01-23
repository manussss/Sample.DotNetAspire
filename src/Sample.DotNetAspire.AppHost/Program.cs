var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

builder.AddProject<Projects.Sample_DotNetAspire_ApiService>("apiservice");

builder.AddProject<Projects.Sample_DotNetAspire_FirstWorker>("sample-dotnetaspire-firstworker");

builder.AddProject<Projects.Sample_DotNetAspire_SecondWorker>("sample-dotnetaspire-secondworker");

builder.Build().Run();
