using System.Collections.Generic;

namespace FlightControl.Models
{
    public class ControlValues
    {
       public int Blue { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Intensity { get; set; }

        public string TopLabel { get; set; }
        public string BottomLabel { get; set; }

        public bool IsMacro { get; set; }

        public List<KeyCommand> KeyCommands { get; set; }

        public ControlValues()
        {
            KeyCommands = new List<KeyCommand>();
        }
    }
}
