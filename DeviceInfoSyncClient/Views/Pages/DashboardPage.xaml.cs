// Project: DeviceInfoSyncClient
// FileName: DashboardPage.xaml.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

using Wpf.Ui.Common.Interfaces;

namespace DeviceInfoSyncClient.Views.Pages
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : INavigableView<ViewModels.DashboardViewModel>
    {
        public ViewModels.DashboardViewModel ViewModel
        {
            get;
        }

        public DashboardPage(ViewModels.DashboardViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
        }
    }
}