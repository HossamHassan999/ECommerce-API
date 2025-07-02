using System.Diagnostics;

namespace WebApplication_API.Mddlewares
{
    public class ProfilingMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddelware> _logger;
        public ProfilingMiddelware(RequestDelegate next , ILogger<ProfilingMiddelware> logger)
        {
            _next = next;

            _logger= logger;

        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await  _next(context);
            stopwatch.Stop();
            _logger.LogInformation($"req {context.Request.Path} took {stopwatch.ElapsedMilliseconds} ms excuted  {context.Connection.RemoteIpAddress?.ToString()}");
        }
    }
}
