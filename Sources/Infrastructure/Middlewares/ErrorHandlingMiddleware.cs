using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Mmu.Mls3.WebApi.Infrastructure.Middlewares
{
    internal class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Global error handler")]
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                var tra = httpContext.Request.Method;
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var serverException = ServerException.CreateFromException(exception);
                var serializedServerError = JsonConvert.SerializeObject(serverException);

                await response.WriteAsync(serializedServerError);
            }
        }
    }
}