using System.Windows.Controls;
using System.Windows.Media;

namespace FlightControl.CustomControls
{
    /// <summary>
    /// Interaction logic for ButtonControl.xaml
    /// </summary>
    public partial class ButtonControl : UserControl
    {
        public ButtonControl()
        {
            InitializeComponent();
        }

        private void SelectedColorChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                ButtonDisplay.Fill = new SolidColorBrush(e.NewValue.Value);
            }
        }
    }
}
