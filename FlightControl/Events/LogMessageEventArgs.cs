

using NLog;

namespace FlightControl.Events
{
    internal class LogMessageEventArgs
    {
        public string Message { get; set; }

        public LogMessageEventArgs(string msg)
        {
            Message = msg;
        }
    }
}