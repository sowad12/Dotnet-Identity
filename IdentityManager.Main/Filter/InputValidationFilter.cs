using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WC.Addon.ClubeezWF.Main.Filter
{
    public class InputValidationFilter : IActionFilter
    {

        private readonly ILogger _logger;


        public InputValidationFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<InputValidationFilter>();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            //    context.Result = new BadRequestObjectResult(new
            //    {
            //        Data = from kvp in context.ModelState
            //               from err in kvp.Value.Errors
            //               let k = kvp.Key
            //               select new ValidationErrorResponseViewModel(ValidationErrorResponseViewModel.ErrorType.Input, (HttpStatusCode)422, k, string.IsNullOrEmpty(err.ErrorMessage) ? "Invalid Input" : err.ErrorMessage),
            //        Status = new
            //        {
            //            Code = 422,
            //            Message = "Validation Error."
            //        }
            //    });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // This filter doesn't do anything post action.
        }
    }
}
