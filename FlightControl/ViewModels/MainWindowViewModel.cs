using System;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Input;
using FlightControl.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace FlightControl.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private static NLog.Logger logger = NLog.LogManager.GetLogger("MainWindow");

        private ComPort selectedComPort;
        private string connectLabel;  

        private readonly SerialPort serialPort;
        private string line1;
        private string line2;
        private ControlValues buttonValues1_1;

        public ICommand GetPorts { get; set; }
        public ICommand ConnectToPort { get; set; }
        public ICommand SendData { get; set; }
        public ICommand TestButton { get; set; }

        public ObservableCollection<ComPort> ComPorts { get; set; }
        
        public ComPort SelectedComPort
        {
            get => selectedComPort;
            set => SetProperty(ref selectedComPort, value);
        }

        public bool SerialPortOpen => serialPort?.IsOpen ?? false;

        public string ConnectLabel
        {
            get => connectLabel;
            set => SetProperty(ref connectLabel, value);
        }

        public string Line1
        {
            get => line1;
            set => SetProperty(ref line1, value);
        }

        public string Line2
        {
            get => line2;
            set => SetProperty(ref line2, value);
        }

        public ControlValues ButtonValues1_1
        {
            get => buttonValues1_1;
            set => SetProperty(ref buttonValues1_1, value);
        }


        public MainWindowViewModel()
        {
   

            ButtonValues1_1 = new ControlValues();

            ComPorts = new ObservableCollection<ComPort>();
            

            GetPorts = new DelegateCommand(OnGetPorts);
            ConnectToPort = new DelegateCommand(OnChangeConnectionState);
            SendData= new DelegateCommand(OnSendSerialMessage);
            TestButton = new DelegateCommand(OnTestButton);

            ConnectLabel = "Connect";

            serialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);

            serialPort.DataReceived += OnDataReceived;

            logger.Debug("Started!");
        }

        private void OnTestButton()
        {
            var test = ButtonValues1_1.Color;
        }


        private void OnChangeConnectionState()
        {
            if (SelectedComPort == null)
            {
                logger.Warn("No COM port selected");
                return;
            }

            if (serialPort.IsOpen)
            {
                serialPort.Close();
                ConnectLabel = "Connect";
                return;
            }

            try
            {
                serialPort.PortName = SelectedComPort.PortAddress;
                serialPort.Open();
                
                ConnectLabel = "Disconnect";
            }
            catch (Exception e)
            {
                logger.Error(e, $"Error connecting to COM port: ex: {e.Message}");
            }         
        }

        private void OnGetPorts()
        {
            ComPorts.Clear();

            using (var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
            {
                string[] portnames = SerialPort.GetPortNames();
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();

                foreach (var port in ports)
                {
                    ComPorts.Add(new ComPort(port["Caption"].ToString(), port["DeviceId"].ToString()));
                }
            }
        }

        private void OnSendSerialMessage()
        {
            var sb = new StringBuilder();

            sb.Append("P");
            sb.Append("U");

            sb.Append("A");

            //TODO: Add neopixel values

            sb.Append(Line1?.Length.ToString("D2"));
            sb.Append(Line1);

            if (sb.Length == 0)
            {
                sb.Append(0);
            }
            else if (!string.IsNullOrEmpty(Line2))
            {
                sb.Append(Line2.Length.ToString("D2"));
                sb.Append(Line2);
            }

            serialPort.WriteLine(sb.ToString());
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort) sender;

                var s = string.Empty;

                s = sp.ReadLine();

                logger.Debug($"Data received: {s}");
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Exception on data received. ex:{ex.Message}");
            }
        }

    }
}
