using FlightControl.Events;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightControl.ViewModels
{
    public class LogViewModel : BindableBase
    {
        IEventAggregator eventAggregator;
        private readonly Stack<string> logMessges;
        private string logView;

        public string LogView
        {
            get => logView;
            set => SetProperty(ref logView, value);
        }

        public LogViewModel(IEventAggregator aggregator)
        {
            logMessges = new Stack<string>(1000);

            eventAggregator = aggregator;
            eventAggregator.GetEvent<LogMessageEvent>().Subscribe(Log);
        }


        private void Log(LogMessageEventArgs args)
        {
            logMessges.Push(args.Message);

            var sb = new StringBuilder();
            foreach (var val in logMessges)
            {
                sb.Append(val);
                sb.Append(Environment.NewLine);
            }

            LogView = sb.ToString();

        }
    }
}
