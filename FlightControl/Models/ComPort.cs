namespace FlightControl.Models
{
    public class ComPort
    {
        public string Name { get; set; }

        public string PortAddress { get; set; }

        public ComPort(string name, string portAddress)
        {
            Name = name;
            PortAddress = portAddress;
        }
 
    }
}
