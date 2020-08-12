using Casino.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Casino.Middlewares
{
    /// <summary>
    /// Domain exception handler middleware.
    /// </summary>
    public class DomainExceptionHandlerMiddleware
    {
        private const string ProblemJsonMimeType = @"application/problem+json";
        private readonly RequestDelegate next;
        private readonly IJsonHelper jsonHelper;
        private readonly ILogger<DomainExceptionHandlerMiddleware> logger;
        private readonly IWebHostEnvironment environment;

        public DomainExceptionHandlerMiddleware(
            RequestDelegate next,
            IJsonHelper jsonHelper,
            ILogger<DomainExceptionHandlerMiddleware> logger,
            IWebHostEnvironment environment)
        {
            this.next = next;
            this.jsonHelper = jsonHelper;
            this.logger = logger;
            this.environment = environment;
        }

        /// <summary>
        /// Invokes the next middleware.
        /// </summary>
        /// <param name="httpContext">HTTP context.</param>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (DomainException ex)
            {
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = ex.StatusCode;
                httpContext.Response.ContentType = ProblemJsonMimeType;

                var problemDetails = new ValidationProblemDetails
                {
                    Status = ex.StatusCode,
                    Title = ex.Message,
                    Type = ex.GetType().Name,
                    Instance = httpContext.Request.Path
                };

                await using var writer = new StringWriter(new StringBuilder(255));
                jsonHelper.Serialize(problemDetails).WriteTo(writer, HtmlEncoder.Default);
                await httpContext.Response.WriteAsync(writer.ToString());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
