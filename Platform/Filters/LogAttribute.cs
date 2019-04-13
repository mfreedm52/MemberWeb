using System.Collections.Generic;
using System.Web.Mvc;
using Platform.Domain;
using Platform.Infrastructure;
using Microsoft.AspNet.Identity;

namespace Platform.Filters
{
	public class LogAttribute : ActionFilterAttribute
	{
		private IDictionary<string, object> _parameters;
		public ICurrentUser CurrentUser { get; set; }

		public string Description { get; set; }

		public LogAttribute(string description)
		{
			Description = description;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			_parameters = filterContext.ActionParameters;
			base.OnActionExecuting(filterContext);
		}
		
		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
            var description = Description;

            foreach (var kvp in _parameters)
            {
                description = description.Replace("{" + kvp.Key + "}", kvp.Value.ToString());
            }

            //var projectName = HttpContext.Current.ApplicationInstance.GetType().Assembly.GetName().Name;

            //TODO find out how to get assembly exact to use in Logging.log function 
            Utility.Logging.Log(description, "Platform");

        }
	}
}