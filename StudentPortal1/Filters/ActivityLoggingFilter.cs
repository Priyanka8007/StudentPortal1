using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using StudentPortal1.Data;
using StudentPortal1.Models;
using System;

namespace StudentPortal1.Filters
{
    public class ActivityLoggingFilter : ActionFilterAttribute
    {
        private readonly ILogger<ActivityLoggingFilter> _logger;
        private readonly StudentAuthDbContext _context;

        public ActivityLoggingFilter(StudentAuthDbContext context, ILogger<ActivityLoggingFilter> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = context.RouteData.Values["controller"]?.ToString();
            var actionName = context.RouteData.Values["action"]?.ToString();
            var userName = context.HttpContext.User.Identity.Name;
            var timeStamp = DateTime.UtcNow;

            _logger.LogInformation($"User '{userName}' is executing '{actionName}' on '{controllerName}' at {timeStamp}.");

            // Log the user activity to the database
            LogUserActivity(userName, controllerName, actionName, timeStamp);

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controllerName = context.RouteData.Values["controller"]?.ToString();
            var actionName = context.RouteData.Values["action"]?.ToString();
            var userName = context.HttpContext.User.Identity.Name;
            var timeStamp = DateTime.UtcNow;

            _logger.LogInformation($"User '{userName}' executed '{actionName}' on '{controllerName}' at {timeStamp}.");

            // Log the user activity to the database
            LogUserActivity(userName, controllerName, actionName, timeStamp);

            base.OnActionExecuted(context);
        }

        private void LogUserActivity(string userName, string controllerName, string actionName, DateTime timeStamp)
        {
            var userActivity = new UserActivity
            {
                UserName = userName,
                ControllerName = controllerName,
                ActionName = actionName,
                Timestamp = timeStamp
            };

            _context.UserActivities.Add(userActivity);
            _context.SaveChanges(); // Synchronous save
        }
    }
}
