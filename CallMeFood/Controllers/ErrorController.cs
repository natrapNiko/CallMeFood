
namespace CallMeFood.Web.Controllers
{
    using CallMeFood.ViewModels.ErrorViewModels;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult Error404()
        {
            var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            var model = new Error404ViewModel
            {
                RequestedPath = statusCodeReExecuteFeature?.OriginalPath
            };

            return View(model);
        }

        [Route("Error/500")]
        public IActionResult Error500()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var model = new Error500ViewModel
                {
                ErrorMessage = exceptionHandlerFeature?.Error.Message,
                StackTrace = exceptionHandlerFeature?.Error.StackTrace
                };

            return View(model);
        }

        [Route("Home/Error")]
        public IActionResult TriggerError()
        {
            throw new Exception("Test exception for error 500");
        }
    }
}
