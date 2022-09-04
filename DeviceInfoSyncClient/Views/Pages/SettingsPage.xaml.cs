// Project: DeviceInfoSyncClient
// FileName: SettingsPage.xaml.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

using DeviceInfoSyncClient.Models;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;

namespace DeviceInfoSyncClient.Views.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : INavigableView<ViewModels.SettingsViewModel>
    {
        public ViewModels.SettingsViewModel ViewModel
        {
            get;
        }

        public SettingsPage(ViewModels.SettingsViewModel viewModel)
        {
            DispatcherHelper.Initialize();
            ViewModel = viewModel;

            InitializeComponent();
               
            //Register for message
            Messenger.Default.Register<DialogInfo>(this, Constant.SHOW_DIALOG, ShowReceiveInfo);
            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }

        private void ShowReceiveInfo(DialogInfo msg)
        {
            ViewModel.DialogButtonText = msg.BtnText;
            ViewModel.DialogInfo = msg.Message;
            Task.Run(() => DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                if (msg.Show)
                {
                    RootDialog.Show();
                }
                else
                {
                    RootDialog.Hide();
                }
            }));
            
            
        }

        private void RootDialog_ButtonRightClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.DialogRightButtonClick();
        }

        private void CheckBox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            Debug.WriteLine("checkBox:" + checkBox.IsChecked);
            ViewModel.EnableTransmissionCheck(checkBox.IsChecked);
        }
    }
}