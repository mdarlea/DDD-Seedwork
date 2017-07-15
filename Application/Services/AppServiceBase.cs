using System;
using Swaksoft.Infrastructure.Crosscutting.Logging;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.Seedwork.Services
{
    public abstract class AppServiceBase<T> : IDisposable
    {
        private static readonly Lazy<ILogger> _logger = new Lazy<ILogger>(LoggerLocator.CreateLog<T>);

        protected static ILogger GetLog()
        {
            return _logger.Value;
        }

        protected static TResult Failed<TResult>(string errorMessage, params object[] args)
           where TResult : ActionResult, new()
        {
            return ErrorResult<TResult>(ActionResultCode.Failed, errorMessage, args);
        }

        protected static TResult Error<TResult>(string errorMessage, params object[] args)
           where TResult : ActionResult, new()
        {
            return ErrorResult<TResult>(ActionResultCode.Errored, errorMessage, args);
        }

        protected static TResult ErrorResult<TResult>(ActionResultCode resultCode, string errorMessage, params object[] args)
            where TResult : ActionResult, new()
        {
            var actionResult = new TResult
            {
                Status = resultCode,
                Message = string.Format(errorMessage, args)
            };
            GetLog().LogError(actionResult.Message);
            return actionResult;
        }


        #region dispose
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
        #endregion dispose
    }
}
