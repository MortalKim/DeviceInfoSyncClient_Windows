// Project: DeviceInfoSyncClient
// FileName: DataPage.xaml.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

using Wpf.Ui.Common.Interfaces;

namespace DeviceInfoSyncClient.Views.Pages
{
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class DataPage : INavigableView<ViewModels.DataViewModel>
    {
        public ViewModels.DataViewModel ViewModel
        {
            get;
        }

        public DataPage(ViewModels.DataViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
        }
    }
}
