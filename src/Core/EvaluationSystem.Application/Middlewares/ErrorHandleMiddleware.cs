using System;
using System.Text.Json;
using System.Threading.Tasks;
using EvaluationSystem.Application.Exceptions;
using EvaluationSystem.Application.Models.Errors;
using EvaluationSystem.Application.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EvaluationSystem.Application.Middlewares
{
    public class ErrorHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ErrorHandleMiddleware> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ErrorHandleMiddleware(RequestDelegate next, ILogger<ErrorHandleMiddleware> logger, IUnitOfWork unitOfWork)
        {
            _next = next;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _unitOfWork.Begin();
                await _next.Invoke(context);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
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
