using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BuberDinner.API.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var problemDetails = new ProblemDetails()
            {
                Title = "An error occured while processing you request.",
                Instance = context.HttpContext.Request.Path,
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = exception.Message,
                Type = "https://tools.ietf.org/html/rfc7131#section-6.6.1"
            };

            //var errorResult = new { error = "An error occured while processing you request." };

            context.Result = new ObjectResult(problemDetails);            

            context.ExceptionHandled = true;
        }
    }
}