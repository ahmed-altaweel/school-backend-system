using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.MiddleWare
{
    public class ErrorHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)

        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = ex?.Message };
                switch (ex)
                {

                    case UnauthorizedAccessException e:
                        responseModel.Message = "Unauthorized Access Exception";
                        responseModel.StatusCode = HttpStatusCode.Unauthorized;
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case ValidationException v:
                        responseModel.Message = "Validation Exception";
                        responseModel.Message += v.Message;
                        responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;
                    case DbUpdateException e:
                        responseModel.Message = e.Message;
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        responseModel.Message = "The Requested Element can't be ";
                        responseModel.StatusCode = HttpStatusCode.NotFound;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;


                    case Exception e:
                        responseModel.Message = e.Message;
                        //   responseModel.Message += e.InnerException.ToString();
                        break;
                    default:
                        responseModel.Message = "Please Contact the application adminstrator";
                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
