using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using Tcc.Core.Constants;
using Tcc.Core.Exceptions;

namespace Tcc.Core.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger
            )
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger logger)
        {

            _logger.LogError(ex.ToString());
            var code = (int)HttpStatusCode.InternalServerError;
            if (ex is NotFoundException) code = (int)HttpStatusCode.NotFound;
            else if (ex is BadRequestException) code = (int)HttpStatusCode.BadRequest;
            else if (ex is ForbiddenException) code = (int)HttpStatusCode.Forbidden;
            else if (ex is UnAuthorizeException) code = (int)HttpStatusCode.Unauthorized;
            else throw new ApiException(ErrorMessages.InternalServerError, code);

            throw new ApiException(ex.Message, code);
        }
    }
}
