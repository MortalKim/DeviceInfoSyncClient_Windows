// Project: DeviceInfoSyncClient
// FileName: SettingsViewModel.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

using DeviceInfoSyncClient.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Input;
using Wpf.Ui.Common.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using DeviceInfoSyncClient.Models;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Threading;

namespace DeviceInfoSyncClient.ViewModels
{
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {
        private TransmissionHelpers? t;
        private Thread? waitingThread;
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _appVersion = String.Empty;

        [ObservableProperty]
        private Wpf.Ui.Appearance.ThemeType _currentTheme = Wpf.Ui.Appearance.ThemeType.Unknown;

        [ObservableProperty]
        private string _ip = String.Empty;
        [ObservableProperty]
        private int _port = 1024;
        [ObservableProperty]
        private string _dialogButtonText = "Cancel";
        [ObservableProperty]
        private string _dialogInfo = "Testing....";

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom()
        {
        }

        private void InitializeViewModel()
        {
            CurrentTheme = Wpf.Ui.Appearance.Theme.GetAppTheme();
            AppVersion = $"DeviceInfoSync - {GetAssemblyVersion()}";

            _isInitialized = true;
        }

        private string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? String.Empty;
        }

        /// <summary>
        /// TestConnection Command
        /// </summary>
        [ICommand]
        private void TestConnectionButtonClick()
        {
            IPAddress? iPAddress;
            //check it a ip address
            var isIP = System.Net.IPAddress.TryParse(Ip,out iPAddress);
            if (isIP)
            {
                //show waiting dialog
                Messenger.Default.Send<DialogInfo>(new Models.DialogInfo(true, (string)Application.Current.FindResource("Testing"), "Cancel"), "ViewAlert");

                t = new TransmissionHelpers(Ip, Port, 1234);

                var success = false;

                t.StartReceive((object ar) =>
                {
                    string res = (string)ar;
                    Debug.Print("receive form server: " + (string)ar);
                    if (res.Equals("Yes"))
                    {
                        success = true;
                        Messenger.Default.Send<DialogInfo>(new Models.DialogInfo(true, (string)Application.Current.FindResource("TargetAvailable"), "OK"), "ViewAlert");
                    }
                    else
                    {
                        Messenger.Default.Send<DialogInfo>(new Models.DialogInfo(true, (string)Application.Current.FindResource("WrongTarget"), "OK"), "ViewAlert");
                    }
                });
                
                t.SendMsg("Is any one here?");
                //wait for 5s. if no response, show info.
                waitingThread = new Thread(() =>
                {
                    try
                    {
                        Thread.Sleep(5000);
                        // if failed, no respose
                        if (!success)
                        {
                            Messenger.Default.Send<DialogInfo>(new Models.DialogInfo(true, (string)Application.Current.FindResource("NoResponse"), "OK"), "ViewAlert");
                        }
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Debug.Print("Thread Quit");
                    }
                });
                waitingThread.Start();
            }
            else
            {
                Messenger.Default.Send<DialogInfo>(new Models.DialogInfo(true, (string)Application.Current.FindResource("NotIp"), "OK"), "ViewAlert");
            }
        }


        public void DialogRightButtonClick()
        {
            //Close Test Connection
            t?.Close();
            waitingThread?.Interrupt();
            Messenger.Default.Send<DialogInfo>(new Models.DialogInfo(false, "", ""), "ViewAlert");
        }

        [ICommand]
        private void OnChangeTheme(string parameter)
        {
            switch (parameter)
            {
                case "theme_light":
                    if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Light)
                        break;

                    Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light);
                    CurrentTheme = Wpf.Ui.Appearance.ThemeType.Light;

                    break;

                default:
                    if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Dark)
                        break;

                    Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark);
                    CurrentTheme = Wpf.Ui.Appearance.ThemeType.Dark;

                    break;
            }
        }
    }
}
