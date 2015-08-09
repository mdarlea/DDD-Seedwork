using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Swaksoft.Core.Dto
{
    public class ActionResult
    {
        public ActionResultCode Status { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public IEnumerable<string> Errors { get; set; } 

        public override string ToString()
        {
            return string.Format("{0}: {1}", Status, Message);
        }
    }

    public static class ActionResultExtensions
    {
        public static TResult ToResult<TResult>(this ActionResult result) where TResult:ActionResult,new()
        {
            return new TResult
            {
               Status = result.Status,
               Code = result.Code,
               Message = result.Message
            };
        }

        public static ActionResult ToResult(this IEnumerable<ValidationResult> validationResults)
        {
            return validationResults.ToResult<ActionResult>();
        }

        public static TResult ToResult<TResult>(this IEnumerable<ValidationResult> validationResults) where TResult:ActionResult,new()
        {
            var results = new StringBuilder();
            foreach (var validationResult in validationResults)
            {
                results.Append(validationResult.ErrorMessage);
                results.Append(",");
            }
            return (results.Length > 0)
                ? new TResult {Status = ActionResultCode.Errored, Message = results.ToString()}
                : new TResult {Status = ActionResultCode.Success};
        }
    }
}
