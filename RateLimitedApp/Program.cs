using Microsoft.AspNetCore.RateLimiting;
using System.Net;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

const string fixedWindowRateLimitedPolicy = "fixed";
const string concurrencyRateLimitedPolicy = "concurrency";


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Fixed time window
//builder.Services.AddRateLimiter(_ => _
//    .AddFixedWindowLimiter(policyName: fixedWindowRateLimitedPolicy, options =>
//    {
//        options.PermitLimit = 4;
//        options.Window = TimeSpan.FromSeconds(20);
//        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//        options.QueueLimit = 2;
//    }));

//concurrency
//builder.Services.AddRateLimiter(rateLimitedOptions => rateLimitedOptions
//    .AddConcurrencyLimiter(policyName: concurrencyRateLimitedPolicy, options =>
//    {
//        options.PermitLimit = 4;
//        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//        options.QueueLimit = 2;
//    }));


////global
//builder.Services.AddRateLimiter(options =>
//{
//    options.RejectionStatusCode = 429;

//    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//        RateLimitPartition.GetFixedWindowLimiter(
//            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
//            factory: partition => new FixedWindowRateLimiterOptions
//            {
//                AutoReplenishment = true,
//                PermitLimit = 4,
//                QueueLimit = 0,
//                Window = TimeSpan.FromSeconds(20)
//            }));
//});

//Chained
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = 429;
    options.GlobalLimiter = PartitionedRateLimiter.CreateChained<HttpContext>(

        PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 5,
                QueueLimit = 0,
                Window = TimeSpan.FromMinutes(1)
            })),

        PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 1000,
                QueueLimit = 0,
                Window = TimeSpan.FromHours(1)
            })));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireRateLimiting(fixedWindowRateLimitedPolicy);
//app.MapControllers().RequireRateLimiting(concurrencyRateLimitedPolicy);
app.MapControllers();

app.Run();
