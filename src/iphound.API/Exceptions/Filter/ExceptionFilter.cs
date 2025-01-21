using iphound.API.Models.HttpModels.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace iphound.API.Exceptions.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AppBaseException)
                HandleProjectException(context);
            else
                HandleUnknownException(context);
        }

        private void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is InvalidIpException invalidIp)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(new ErrorResponse(invalidIp.Message));
            }
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ErrorResponse("Unknown Error"));
        }
    }
}
