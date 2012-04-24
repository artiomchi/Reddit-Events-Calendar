using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsCalendar.Repository
{
    [DebuggerDisplay("Status: {Status}")]
    public class OperationStatus
    {
        public bool Status { get; set; }
        public int RecordsAffected { get; set; }
        public string Message { get; set; }
        public object OperationID { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string ExceptionBaseMessage { get; set; }
        public string ExceptionBaseStackTrace { get; set; }

        public static OperationStatus CreateFromException(string message, Exception ex)
        {
            var status = new OperationStatus
            {
                Status = false,
                Message = message,
            };
            if (ex != null)
            {
                status.ExceptionMessage = ex.Message;
                status.ExceptionStackTrace = ex.StackTrace;
                var baseEx = ex.GetBaseException();
                if (baseEx != null && baseEx != ex)
                {
                    status.ExceptionBaseMessage = baseEx.Message;
                    status.ExceptionBaseStackTrace = baseEx.StackTrace;
                }
            }

            return status;
        }
    }
}
