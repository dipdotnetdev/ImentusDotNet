using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreWebAPIPract.Fileters
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Started");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Completed");
        }
    } 

    public class LoggingActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}