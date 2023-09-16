namespace MinimalApiChanges
{
    public class SampleEndpointFilter: IEndpointFilter
    {
        protected readonly ILogger Logger;

        protected SampleEndpointFilter(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<SampleEndpointFilter>();
        }

        public virtual async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            Logger.LogInformation($"Request for {context.HttpContext.Request.Path} received at {DateTime.Now.ToLongTimeString()}.");
            var result = await next(context);
            Logger.LogInformation($"Request for {context.HttpContext.Request.Path} handled at {DateTime.Now.ToLongTimeString()}.");
            return result;
        }
    }

    class MyEndpointFilter : SampleEndpointFilter
    {
        public MyEndpointFilter(ILoggerFactory loggerFactory) : base(loggerFactory) { }
    }
}
