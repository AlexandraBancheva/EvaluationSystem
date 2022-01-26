using System;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using EvaluationSystem.Application.Exceptions;
using EvaluationSystem.Application.Models.Errors;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Application.Middlewares
{
    public class ErrorHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandleMiddleware(RequestDelegate next, ILogger<ErrorHandleMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork)
        {
            try
            {
                unitOfWork.Begin();
                await _next.Invoke(context);
                unitOfWork.Commit();
            }
            catch (Exception e)
            {
                unitOfWork.Rollback();
                _logger.LogError(e.ToString());
                await HandleException(e, context);
            }
           
        }

        private async Task HandleException(Exception e, HttpContext context)
        {
            var error = new ErrorDto();

            switch (e)
            {
                case HttpException httpException:
                    error.Code = (int)httpException.StatusCode;
                    error.Message = httpException.Message;
                    break;

                case ValidationException validationException:
                    error.Code = 400;
                    error.Message = validationException.Message;
                    break;

                default:
                    error.Code = 500;
                    error.Message = e.Message;
                    break;
            }


            var responseError = JsonSerializer.Serialize(error);
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = error.Code;
            await context.Response.WriteAsync(responseError);
        }
    }
}
