using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FlightControl.Annotations;

namespace FlightControl.Models
{
    public class ControlPage :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ControlValues toggle1Up;
        private ControlValues toggle2Up;
        private ControlValues toggle3Up;
        private ControlValues toggle1Down;
        private ControlValues toggle2Down;
        private ControlValues toggle3Down;
        private ControlValues button1;
        private ControlValues button2;
        private ControlValues button3;
        private ControlValues button4;
        private ControlValues button5;
        private ControlValues button6;

        public string Name { get; set; }

        public Guid Id { get; set; }


        public ControlValues Toggle1Up
        {
            get => toggle1Up;
            set => SetValue(nameof(Toggle1Up), value, ref toggle1Up);
        }

        public ControlValues Toggle2Up
        {
            get => toggle2Up;
            set => SetValue(nameof(Toggle2Up), value, ref toggle2Up);
        }

        public ControlValues Toggle3Up
        {
            get => toggle3Up;
            set => SetValue(nameof(Toggle3Up), value, ref toggle3Up);
        }

        public ControlValues Toggle1Down
        {
            get => toggle1Down;
            set => SetValue(nameof(Toggle1Down), value, ref toggle1Down);
        }

        public ControlValues Toggle2Down
        {
            get => toggle2Down;
            set => SetValue(nameof(Toggle2Down), value, ref toggle2Down);
        }

        public ControlValues Toggle3Down
        {
            get => toggle3Down;
            set => SetValue(nameof(Toggle3Down), value, ref toggle3Down);
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

        public ControlPage()
        {
            toggle1Up = new ControlValues();
            toggle2Up = new ControlValues();
            toggle3Up = new ControlValues();
            toggle1Down = new ControlValues();
            toggle2Down = new ControlValues();
            toggle3Down = new ControlValues();
            button1 = new ControlValues();
            button2 = new ControlValues();
            button3 = new ControlValues();
            button4 = new ControlValues();
            button5 = new ControlValues();
            button6 = new ControlValues();
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
