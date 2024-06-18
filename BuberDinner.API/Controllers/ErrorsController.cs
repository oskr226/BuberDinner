using BuberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.API.Controllers
{
    //[Route("error")]
    //[ApiController]
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        [HttpPost]
        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features?.Get<IExceptionHandlerFeature>()?.Error;

            var (statusCode, message) = exception switch
            {
                IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            };

            //return Problem(title: exception?.Message, statusCode: 500);
            return Problem(statusCode: statusCode, title: message);
        }
    }
}
