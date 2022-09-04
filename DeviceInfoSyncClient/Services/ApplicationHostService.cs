// Project: DeviceInfoSyncClient
// FileName: ApplicationHostService.cs
// Copyright (c) 2022 MortalKim
// Date: 2022-8-29

using DeviceInfoSyncClient.Helpers;
using DeviceInfoSyncClient.Views;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui.Mvvm.Contracts;
using static DeviceInfoSyncClient.Helpers.SystemInfoHelper;

namespace DeviceInfoSyncClient.Services
{
    /// <summary>
    /// Managed host of the application.
    /// </summary>
    public class ApplicationHostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private INavigationWindow _navigationWindow;

        TransmissionHelpers transmissionHelpers;

        public ApplicationHostService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            SystemInfoHelper.Instance.Start(3, SystemInfoDelegate);
            transmissionHelpers = new TransmissionHelpers("127.0.0.1", 777);

        }

        public void SystemInfoDelegate()
        {
            var a = Newtonsoft.Json.JsonConvert.SerializeObject(SystemInfoHelper.Instance);
            transmissionHelpers?.SendMsg(a);
            Debug.Print(a);
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await HandleActivationAsync();
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Creates main window during activation.
        /// </summary>
        private async Task HandleActivationAsync()
        {
            await Task.CompletedTask;

            if (!Application.Current.Windows.OfType<Container>().Any())
            {
                _navigationWindow = (_serviceProvider.GetService(typeof(INavigationWindow)) as INavigationWindow)!;
                _navigationWindow!.ShowWindow();

                _navigationWindow.Navigate(typeof(Views.Pages.DashboardPage));
            }

            await Task.CompletedTask;
        }
    }
}
