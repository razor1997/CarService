using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        private Stopwatch _stopWatch;
        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
            _stopWatch = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
            await next.Invoke(context);
            _stopWatch.Stop();
            var elapsedMiliSeconds = _stopWatch.Elapsed.TotalMilliseconds;
            if (elapsedMiliSeconds / 1000 > 4)
            {
                var message =
                        $"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedMiliSeconds} ms";
                _logger.LogInformation(message);
            }



        }

        private void StartWatch()
        {
            throw new NotImplementedException();
        }
    }
}
