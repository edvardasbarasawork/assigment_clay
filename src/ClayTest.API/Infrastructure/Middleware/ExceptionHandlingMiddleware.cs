using ClayTest.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ClayTest.API.Infrastructure.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            _logger.LogError(ex.Message);

            var code = HttpStatusCode.InternalServerError;
            var errors = new List<string>() { ex.Message };

            if (ex is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            else if (ex is BadRequestException)
            {
                code = HttpStatusCode.BadRequest;
            }
            else if (ex is UnprocessableRequestException)
            {
                code = HttpStatusCode.UnprocessableEntity;
            }

            var result = JsonConvert.SerializeObject(new { Code = (int)code, Errors = errors });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
