using System;
using System.Text.Json;
using System.Threading.Tasks;
using EvaluationSystem.Application.Exceptions;
using EvaluationSystem.Application.Models.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace EvaluationSystem.Application.Middlewares
{
    public class ErrorHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {

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
