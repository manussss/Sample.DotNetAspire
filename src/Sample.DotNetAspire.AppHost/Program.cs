var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("redis")
    .WithRedisInsight();

var mongo = builder.AddMongoDB("mongo");
var mongoDb = mongo.AddDatabase("mongodb");

var apiService = builder.AddProject<Projects.Sample_DotNetAspire_ApiService>("apiservice")
    .WithExternalHttpEndpoints();

apiService
    .WithReference(mongoDb)
    .WaitFor(mongoDb);

apiService
    .WithReference(cache)
    .WaitFor(cache);

builder.Build().Run();
