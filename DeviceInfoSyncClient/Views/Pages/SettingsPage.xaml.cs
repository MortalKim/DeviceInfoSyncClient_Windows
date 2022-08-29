// Project: DeviceInfoSyncClient
// FileName: SettingsPage.xaml.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

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
            ViewModel = viewModel;

            InitializeComponent();
        }
    }
}