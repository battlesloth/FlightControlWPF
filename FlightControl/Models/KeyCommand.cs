using System.Collections.Generic;

namespace FlightControl.Models
{
    public class KeyCommand
    {
        public bool IsDelay { get; set; }

        public int DelayMs { get; set; }

        public List<string> KeyPresses { get; set; }

        public override string ToString()
        {
            if (IsDelay)
            {
                return $"{DelayMs} ms";
            }

            if (KeyPresses.Count == 0)
            {
                return string.Empty;
            }

            return string.Join(" + ", KeyPresses);
        }
    

        public KeyCommand()
        {
            KeyPresses = new List<string>();
        }
    }
}
