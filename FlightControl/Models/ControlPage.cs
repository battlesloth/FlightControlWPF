using System.ComponentModel;
using System.Runtime.CompilerServices;
using FlightControl.Annotations;

namespace FlightControl.Models
{
    public class ControlPage :INotifyPropertyChanged
    {
        private ControlValues toggle1;
        private ControlValues toggle2;
        private ControlValues toggle3;
        private ControlValues button1;
        private ControlValues button2;
        private ControlValues button3;
        private ControlValues button4;
        private ControlValues button5;
        private ControlValues button6;

        public ControlPage()
        {
            toggle1 = new ControlValues();
            toggle2 = new ControlValues();
            toggle3 = new ControlValues();
            button1 = new ControlValues();
            button2 = new ControlValues();
            button3 = new ControlValues();
            button4 = new ControlValues();
            button5 = new ControlValues();
            button6 = new ControlValues();
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public string Name { get; set; }


        public ControlValues Toggle1
        {
            get => toggle1;
            set => SetValue(nameof(Toggle1), value, ref toggle1);
        }

        public ControlValues Toggle2
        {
            get => toggle2;
            set => SetValue(nameof(Toggle2), value, ref toggle2);
        }

        public ControlValues Toggle3
        {
            get => toggle3;
            set => SetValue(nameof(Toggle3), value, ref toggle3);
        }

        public ControlValues Button1
        {
            get => button1;
            set => SetValue(nameof(Button1), value, ref button1);
        }

        public ControlValues Button2
        {
            get => button2;
            set => SetValue(nameof(Button2), value, ref button2);
        }

        public ControlValues Button3
        {
            get => button3;
            set => SetValue(nameof(Button3), value, ref button3);
        }

        public ControlValues Button4
        {
            get => button4;
            set => SetValue(nameof(Button4), value, ref button4);
        }

        public ControlValues Button5
        {
            get => button5;
            set => SetValue(nameof(Button5), value, ref button5);
        }

        public ControlValues Button6
        {
            get => button6;
            set => SetValue(nameof(Button6), value, ref button6);
        }


        private void SetValue<T>(string property, T value, ref T field)
        {
            if (value.Equals(field)) return;
            field = value;
            OnPropertyChanged(property);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
