using System.Data;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
{
    public class TransactionPerRequestActionFilterAttribute : IActionFilter
    {
        private readonly IsolationLevel _isolationLevel;
        private readonly UnitOfWork _unitOfWork;

        public TransactionPerRequestActionFilterAttribute(UnitOfWork unitOfWork, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _isolationLevel = isolationLevel;
            _unitOfWork = unitOfWork;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _unitOfWork.BeginTransaction(_isolationLevel);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                _unitOfWork.Complete();
            }
            _unitOfWork.Close();
        }
    }
}