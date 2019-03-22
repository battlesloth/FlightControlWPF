using FlightControl.ViewModels;
using FlightControl.Views;
using Prism.Events;
using System.Windows;

namespace FlightControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        IEventAggregator eventAggregator;

        public MainWindowView(IEventAggregator aggregator)
        {
            eventAggregator = aggregator;
            InitializeComponent();
        }

        public void OpenLogWindow(object sender, RoutedEventArgs e)
        {
            var logWindow = new LogView();
            var model = new LogViewModel(eventAggregator);
            logWindow.DataContext = model;
            logWindow.Show();
        }
    }
}
