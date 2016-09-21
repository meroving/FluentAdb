using FluentAdb.Enums;
using FluentAdb.Interfaces;
using JetBrains.Annotations;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FluentAdb.Sample
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IAdb _adb;
        private IAdbTargeted _deviceAdb;
        private string _deviceName;

        public MainWindowViewModel()
        {
            DeviceName = "Not connected";
            Parameters = new ObservableCollection<Parameter>();
            Logcat = new ObservableCollection<string>();
            ConnectCommand = new AsyncCommand(async () => await Connect(), CancellationToken.None);
            InstallCommand = new AsyncCommand(async () => await InstallApp(), CancellationToken.None);
            try
            {
                _adb = Adb.New("..\\..\\..\\adb\\adb.exe");
            }
            catch (AdbException ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(1);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ConnectCommand { get; set; }

        public string DeviceName
        {
            get { return _deviceName; }
            set
            {
                if (value == _deviceName) return;
                _deviceName = value;
                OnPropertyChanged();
            }
        }

        public ICommand InstallCommand { get; set; }

        public ObservableCollection<string> Logcat { get; set; }

        public ObservableCollection<Parameter> Parameters { get; set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task Connect(CancellationToken cancellationToken = default(CancellationToken))
        {
            Logcat.Add(item: await _adb.Version(cancellationToken));
            var devices = await _adb.GetDevices(cancellationToken);
            var deviceInfo = devices.FirstOrDefault(d => d.State == DeviceState.Online);
            if (deviceInfo == null)
            {
                MessageBox.Show("No device with ADB interface is connected");
                return;
            }
            _deviceAdb = _adb.Target(deviceInfo.SerialNumber);
            var model = await _deviceAdb.Shell.GetProperty("ro.product.model", cancellationToken);
            var manufacturer = await _deviceAdb.Shell.GetProperty("ro.product.manufacturer", cancellationToken);
            DeviceName = manufacturer + " " + model;

            var parameters = await _deviceAdb.Shell.GetAllProperties(cancellationToken);

            foreach (var kv in parameters)
            {
                Parameters.Add(new Parameter(kv.Key, kv.Value));
            }

            //_deviceAdb.Logcat()
            //    .Buffer(TimeSpan.FromSeconds(3))
            //    .ObserveOnDispatcher()
            //    .Subscribe(log =>
            //    {
            //        foreach (var l in log)
            //        {
            //            Logcat.Insert(0, l);
            //        }
            //    });
        }

        private async Task InstallApp(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (await _deviceAdb.GetState(cancellationToken) != AdbState.Device)
            {
                MessageBox.Show("Device is disconnected. Can't install app");
            }

            OpenFileDialog ofd = new OpenFileDialog { Filter = "Apk files|*.apk" };

            if (ofd.ShowDialog() == true)
            {
                var apkFilePath = ofd.FileName;
                var installationResult =
                    await _deviceAdb.Install(apkFilePath, InstallOptions.ReinstallKeepingData, cancellationToken: cancellationToken);
                if (installationResult == InstallationResult.Success)
                {
                    MessageBox.Show("Application installed successfully");
                }
                else
                {
                    MessageBox.Show("Application installation failed. Error: " + installationResult);
                }
            }
        }
    }
}