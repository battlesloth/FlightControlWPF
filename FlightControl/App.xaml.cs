using System;
using System.IO;
using System.Windows;
using FlightControl.ViewModels;
using NLog;
using NLog.Targets;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace FlightControl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override Window CreateShell()
        {
            string sDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BS.FlightControl");
            
            if (!Directory.Exists(sDirectory))
            {
                Directory.CreateDirectory(sDirectory);
            }
            
            //MasterDatasource.Instance.Initialize(Path.Combine(sDirectory, "PercepcoConnect.sqlite3"))
            //    .Wait();
            //
            //var dbSettings = MasterDatasource.Instance.SettingsDal.GetSettings().Result;
            //
            //Configuration.AddSettingsToRepository(dbSettings);

            var config = new NLog.Config.LoggingConfiguration();
            
            var logfile = new FileTarget("logfile")
            {
                FileName = Path.Combine(sDirectory, "logs/FlightControl.log"),
                ArchiveAboveSize = 134217728,
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveOldFileOnStartup = true,
                ArchiveNumbering = ArchiveNumberingMode.DateAndSequence,
                ArchiveDateFormat = "yyyyMMdd",
                ArchiveFileName = Path.Combine(sDirectory, "logs/archive/FlightControl.{#}.zip"),
                EnableArchiveFileCompression = true,
                EnableFileDelete = true,
                MaxArchiveFiles = 15
            };
            
            var logConsole = new ConsoleTarget("logconsole");

            var logPrismAggregator = new NlogEventAggregatorTarget((IEventAggregator)Container.Resolve(typeof(IEventAggregator)));

            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logConsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logPrismAggregator);
          
            LogManager.Configuration = config;
            
            var logger = LogManager.GetLogger("App Startup");
            
            logger.Debug("==============================================");
            
            logger.Debug("Starting app...");
            //Configuration.LoadLocalConfiguration("ConnectDesktop.ini");
            //Configuration.LogOutSettings();
            
            logger.Debug("Startup complete, loading main window");
            logger.Debug("==============================================");
            return Container.Resolve<MainWindowView>();
        }

        protected override void InitializeShell(Window shell)
        {
            IRegionManager regionManager = Container.Resolve<IRegionManager>();

            var mainWindowViewModel = Container.Resolve<MainWindowViewModel>();
            if (Current.MainWindow != null)
            {
                Current.MainWindow.DataContext = mainWindowViewModel;
                Current.MainWindow.Show();
            }
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {

        }
    }
}
