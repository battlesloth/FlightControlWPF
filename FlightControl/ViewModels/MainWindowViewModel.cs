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
        private ControlPage activeControlPage;

        public ICommand GetPorts { get; set; }
        public ICommand ConnectToPort { get; set; }
        public ICommand SendData { get; set; }
        public ICommand TestButton { get; set; }


        private ControlSet activeControlSet;


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

        public ObservableCollection<ComPort> ComPorts { get; set; }

        public ObservableCollection<ControlSet> ControlSets { get; set; }

        public ControlSet ActiveControlSet
        {
            get => activeControlSet;
            set => SetProperty(ref activeControlSet, value);
        }

        public ControlPage ActiveControlPage
        {
            get => activeControlPage;
            set => SetProperty(ref activeControlPage, value);
        }


        public MainWindowViewModel()
        {
            ControlSets = new ObservableCollection<ControlSet>();
            ActiveControlSet  = new ControlSet();
            ActiveControlPage = new ControlPage();

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
            if (ControlSets.Count == 0)
            {
                var set1 = new ControlSet(){Name = "Test Set 1"};
                set1.ControlPages.Add(new ControlPage(){Name = "Test 1_1 Page"});
                set1.ControlPages.Add(new ControlPage() { Name = "Test 1_2 Page" });
                set1.ControlPages.Add(new ControlPage() { Name = "Test 1_3 Page" });

                ControlSets.Add(set1);

                var set2 = new ControlSet() { Name = "Test Set 2" };
                set2.ControlPages.Add(new ControlPage() { Name = "Test 2_1 Page" });
                set2.ControlPages.Add(new ControlPage() { Name = "Test 2_2 Page" });
                set2.ControlPages.Add(new ControlPage() { Name = "Test 3_3 Page" });

                ControlSets.Add(set2);

                var set3 = new ControlSet() { Name = "Test Set 3" };
                set3.ControlPages.Add(new ControlPage() { Name = "Test 3_1 Page" });
                set3.ControlPages.Add(new ControlPage() { Name = "Test 3_2 Page" });
                set3.ControlPages.Add(new ControlPage() { Name = "Test 3_3 Page" });

                ControlSets.Add(set3);

                ActiveControlSet = ControlSets[0];
                ActiveControlPage = ActiveControlSet.ControlPages[0];
            }


            var test = ActiveControlPage.Toggle1Up.BottomLabel;
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

            //sb.Append(Line1?.Length.ToString("D2"));
            //sb.Append(Line1);
            //
            //if (sb.Length == 0)
            //{
            //    sb.Append(0);
            //}
            //else if (!string.IsNullOrEmpty(Line2))
            //{
            //    sb.Append(Line2.Length.ToString("D2"));
            //    sb.Append(Line2);
            //}

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
