using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using FlightControl.Models;

namespace FlightControl.CustomControls
{
    /// <summary>
    /// Interaction logic for ToggleControl.xaml
    /// </summary>
    public partial class ToggleControl : UserControl
    {
        public static DependencyProperty NameLabelProperty =
            DependencyProperty.Register(
                nameof(NameLabel),
                typeof(string),
                typeof(ToggleControl));


        public string NameLabel
        {
            get => (string)GetValue(NameLabelProperty);
            set => SetValue(NameLabelProperty, value);
        }

        public static DependencyProperty UpValuesProperty =
            DependencyProperty.Register(
                nameof(UpValues),
                typeof(ControlValues),
                typeof(ToggleControl));


        public ControlValues UpValues
        {
            get => (ControlValues)GetValue(UpValuesProperty);
            set => SetValue(UpValuesProperty, value);
        }

        public static DependencyProperty DownValuesProperty =
            DependencyProperty.Register(
                nameof(DownValues),
                typeof(ControlValues),
                typeof(ToggleControl));


        public ControlValues DownValues
        {
            get => (ControlValues)GetValue(DownValuesProperty);
            set => SetValue(DownValuesProperty, value);
        }

        public ToggleControl()
        {
            InitializeComponent();
        }
    }
}
