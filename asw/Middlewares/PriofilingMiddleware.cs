using System.Diagnostics;

namespace asw.Middlewares
{
    public class PriofilingMiddleware 
    {
        private readonly RequestDelegate _request;
        private readonly ILogger _logger;

        public PriofilingMiddleware(RequestDelegate next ,ILogger<PriofilingMiddleware> logger)
        {
            _request = next;
            _logger = logger;
        }

        public async Task Invoke( HttpContext context) 
        {
          var stopwatch = new Stopwatch();
          stopwatch.Start();
        await    _request(context);
            stopwatch.Stop();

            _logger.LogInformation($"Request: {context.Request.Path}' took '{stopwatch.ElapsedMilliseconds}ms, to execuet");


        }
    }
}
