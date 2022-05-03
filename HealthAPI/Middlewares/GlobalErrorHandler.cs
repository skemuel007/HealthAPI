using HealthAPI.Models;
using HealthAPI.Repositories.Interfaces;
using HealthAPI.Utils;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace HealthAPI.Middlewares
{
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        /// <summary>
        /// Global error handler request method
        /// </summary>
        /// <param name="next"></param>
        public GlobalErrorHandler(RequestDelegate next,
            ILoggerManager logger)
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
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    _logger.LogError($"Something went wrong: - {e.InnerException}");
                }
                else
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    _logger.LogError($"Something went wrong: - {e}");
                }
                await HandleErrorAsync(context, e);
            }
        }

        public static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                // checks for application error
                case AppException e:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ValidationException e:
                    context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                // not found error
                case KeyNotFoundException e:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case UserNotFoundException e:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;// all unhandled error
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            // create response
            //var response = new { Status = false, Message = exception.InnerException != null ? exception.InnerException.Message : exception.Message, Data = new { } };
            // var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.InnerException != null ? exception.InnerException.Message : exception.Message
            }.ToString());

            // return context.Response.WriteAsync(payload);
        }
    }
}
