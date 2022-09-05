using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.DependencyInjection;
using PortApplication.Services;
using PortApplication.Services.Interfaces;
using PortApplication.ViewModels;
using PortApplication.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PortApplication
{
    public partial class App : Application
    {
        protected static IServiceProvider ServiceProvider { get; set; }

        public App(Action<IServiceProvider> addPlatformServices = null)
        {
            InitializeComponent();
            SetupServices(addPlatformServices);

            if (Device.Idiom == TargetIdiom.Phone)
            {
                MainPage = new NavigationPage(new PortPage());
            }
            else if (Device.Idiom == TargetIdiom.Tablet)
            {
                MainPage = new NavigationPage(new PortTabletPage());
            }            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void SetupServices(Action<IServiceProvider> addPlatformServices = null)
        {
            var services = new ServiceCollection();

            // Add platform specific services
            addPlatformServices?.Invoke((IServiceProvider)services);

            services.AddSingleton<INavigation, NavigationService>();

            // Add viewmodels
            services.AddTransient<PortViewModel>();
            services.AddTransient<PortDetailViewModel>();
            services.AddTransient<PortTabletPageViewModel>();

            // Register services
            services.AddSingleton<IPortService, PortService>();
            services.AddSingleton<IAlertService, AlertService>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public static BaseViewModel GetViewModel<TViewModel>() where TViewModel : BaseViewModel
        {
            return ServiceProviderServiceExtensions.GetService<TViewModel>(ServiceProvider);
        }

        public static void LogCrash(Exception ex, string location)
        {
            var log = new Dictionary<string, string>
            {
                { "Location" , location},
                { "Message", ex?. Message},
                { "StackTrace", ex?.StackTrace},
            };
            Crashes.TrackError(ex, log);
        }
    }
}
