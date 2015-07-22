using System;
using Swaksoft.Core;
using Swaksoft.Infrastructure.Crosscutting.Logging;
using System.Linq;
using System.Text;
using Swaksoft.Application.Seedwork.Validation;
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

        protected TResult Call<TResult>(Func<TResult> callback)
            where TResult:ActionResult,new()
        {
            try
            {
                return callback();
            }
            catch (ValidationErrorsException ve)
            {
                var message = new StringBuilder();
                foreach (var err in ve.ValidationErrors)
                {
                    message.AppendLine(err);
                }
                return GetError<TResult>(ve, message.ToString());
            }
            //catch (DbEntityValidationException dberr)
            //{
            //    var errors = dberr.EntityValidationErrors.SelectMany(ve => ve.ValidationErrors);

            //    var message = new StringBuilder(dberr.Message);
            //    foreach (var err in errors)
            //    {
            //        message.AppendLine(err.ErrorMessage);
            //    }

            //    return GetError<TResult>(dberr,message.ToString());
            //}
            catch (Exception ex)
            {
                return GetError<TResult>(ex);
            }
        }

        private static TResult GetError<TResult>(Exception ex)
            where TResult : ActionResult, new()
        {
            return GetError<TResult>(ex, ex.Message);
        }

        private static TResult GetError<TResult>(Exception ex, string message) 
            where TResult : ActionResult, new()
        {
            GetLog().Fatal(message, ex);
            return new TResult
            {
                Status = ActionResultCode.Failed,
                Message = message
            };
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
