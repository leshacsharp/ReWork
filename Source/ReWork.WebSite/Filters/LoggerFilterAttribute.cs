using log4net;
using System.Web.Mvc;

namespace ReWork.WebSite.Filters
{
    public class LoggerFilterAttribute : FilterAttribute, IExceptionFilter
    {
        private readonly ILog _logger;

        public LoggerFilterAttribute()
        {
            _logger = LogManager.GetLogger("FileLogger");
        }

        public void OnException(ExceptionContext filterContext)
        {
            _logger.Error(filterContext.Exception.Message, filterContext.Exception);
        }
    }
}