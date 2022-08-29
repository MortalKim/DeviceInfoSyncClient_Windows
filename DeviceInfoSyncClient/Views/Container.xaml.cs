﻿// Project: DeviceInfoSyncClient
// FileName: Container.xaml.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

using DeviceInfoSyncClient.WMI;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Common;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace DeviceInfoSyncClient.Views
{
    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class Container : INavigationWindow
    {
        public ViewModels.ContainerViewModel ViewModel
        {
            get;
        }

        public Container(ViewModels.ContainerViewModel viewModel, IPageService pageService, INavigationService navigationService)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            
            SetPageService(pageService);

            navigationService.SetNavigationControl(RootNavigation);

            //handle closing event
            this.Closing += Window_Closing;

            var bios = new Bios();
            var mem = new RAM();
            Debug.Print("ces" + bios.ToString());
            WindowState = WindowState.Minimized;
        }

        #region INavigationWindow methods

        public Frame GetFrame()
            => RootFrame;

        public INavigation GetNavigation()
            => RootNavigation;

        public bool Navigate(Type pageType)
            => RootNavigation.Navigate(pageType);

        public void SetPageService(IPageService pageService)
            => RootNavigation.PageService = pageService;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();

        #endregion INavigationWindow methods

        /// <summary>
        /// Raises the closed event.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            //base.OnClosed(e);
            this.Hide();
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (MenuItem)sender;
            switch (item.Tag)
            {
                case "home":
                    break;
                case "quit":
                    Application.Current.Shutdown();
                    break;
            }
        }
    }
}