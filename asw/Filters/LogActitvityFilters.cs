

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace asw.Filters
{
    public class LogActitvityFilters : IActionFilter
    {
        private readonly ILogger<LogActitvityFilters> _logger;

        public LogActitvityFilters(ILogger<LogActitvityFilters> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($" Executing action{context.ActionDescriptor.DisplayName} on  Controller{context.Controller} ");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"  action{context.ActionDescriptor.DisplayName} finished executing on  Controller{context.Controller} ");

        }


    }
}
