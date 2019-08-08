using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FlightControl.Models;

namespace FlightControl.CustomControls
{
    /// <summary>
    /// Interaction logic for ButtonControl.xaml
    /// </summary>
    public partial class ButtonControl : UserControl
    {
        public static DependencyProperty NameLabelProperty =
            DependencyProperty.Register(
                nameof(NameLabel),
                typeof(string),
                typeof(ButtonControl));

        public string NameLabel
        {
            get => (string)GetValue(NameLabelProperty);
            set => SetValue(NameLabelProperty, value);
        }


        public static DependencyProperty ValuesProperty =
            DependencyProperty.Register(
                nameof(Values),
                typeof(ControlValues),
                typeof(ButtonControl));


        public ControlValues Values
        {
            get => (ControlValues)GetValue(ValuesProperty);
            set => SetValue(ValuesProperty, value);
        }

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
