﻿using System;
using System.Net;

namespace EvaluationSystem.Application.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public HttpException(string message, HttpStatusCode statusCode) 
            :base(message)
        {
            StatusCode = statusCode;
        }
    }
}
