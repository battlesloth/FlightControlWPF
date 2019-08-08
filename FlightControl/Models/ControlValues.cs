using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using FlightControl.Annotations;

namespace FlightControl.Models
{
    public class ControlValues : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Color color;
        private string topLabel;
        private string bottomLabel;

        public Color Color
        {
            get => color;
            set => SetValue(nameof(Color), value, ref color);
        }

        public string TopLabel
        {
            get => topLabel;
            set => SetValue(nameof(TopLabel), value, ref topLabel);
        }

        public string BottomLabel
        {
            get => bottomLabel;
            set => SetValue(nameof(BottomLabel),  value, ref bottomLabel);
        }

        public string KeyCommandsLabel => ConstructLabel();

        private string ConstructLabel()
        {
            if (IsMacro)
            {
                if (KeyCommands.Count == 0)
                {
                    return string.Empty;
                }

                return string.Join(" + ", $"({KeyCommands})");
            }

            if (KeyCommands.Count == 0)
            {
                return string.Empty;
            }

            return KeyCommands[0].ToString();

        }

        public bool IsMacro { get; set; }

        public List<KeyCommand> KeyCommands { get; set; }

        public ControlValues()
        {
            KeyCommands = new List<KeyCommand>();
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
