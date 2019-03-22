using FlightControl.Events;
using NLog.Common;
using NLog.Targets;
using Prism.Events;

namespace FlightControl
{
    internal class NlogEventAggregatorTarget : TargetWithLayout
    {
        private IEventAggregator eventAggregator;

        public NlogEventAggregatorTarget(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            var msg = this.Layout.Render(logEvent.LogEvent);

            eventAggregator.GetEvent<LogMessageEvent>().Publish(new LogMessageEventArgs(msg));
        }
    }
}