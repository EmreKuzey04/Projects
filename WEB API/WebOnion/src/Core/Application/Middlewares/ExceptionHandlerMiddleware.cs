using Application.Behaviors;
using Application.Exceptions;
using Application.Models.ResponseWrappers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Middlewares
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{

				await HandleExceptionAsync(context,ex,_logger);

            }
        }

		private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ExceptionHandlerMiddleware> logger)
		{
			int statusCode = GetStatusCode(ex);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = statusCode;

			List<string> errorMessages;

			if (ex is ValidationException)
			{
				errorMessages = ((ValidationException)ex).Errors
					.Select(x=> x.ErrorMessage)
					.ToList();
			}

			else 
				errorMessages = new List<string>() { { ex.Message } };

			logger.LogError("ERROR : {ExceptionMessage} | Path : {Path} | TraceId : {TraceId}", 
				string.Join(", ", errorMessages.ToArray()),
				context.Request.Path,
				context.TraceIdentifier);

			var response = new Response<NoData>(errorMessages);

			return context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}

		private static int GetStatusCode(Exception ex)
		{
			if (ex is BadRequestException)
				return StatusCodes.Status400BadRequest;
			else if (ex is NotFoundException)
				return StatusCodes.Status404NotFound;
			else if (ex is ValidationException)
				return StatusCodes.Status422UnprocessableEntity;

			return StatusCodes.Status500InternalServerError;
		}
    }
}
