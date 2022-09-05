using PortApplication.Interfaces;
using PortApplication.Models;
using PortApplication.Services.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PortApplication.ViewModels
{
    public class PortDetailViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;

        private Port _port;
        public Port Port
        {
            get { return _port; }
            set { _port = value; OnPropertyChanged(); }
        }

        private bool _sharePopupVisible;
        public bool SharePopupVisible
        {
            get { return _sharePopupVisible; }
            set { _sharePopupVisible = value; OnPropertyChanged(); }
        }

        public ICommand SharePortCommand { get; set; }

        public ICommand OpenMapCommand { get; set; }

        public PortDetailViewModel(IAlertService alertService)
        {
            _alertService = alertService;

            SharePortCommand = new Command(async () => await SharePortAsync());

            OpenMapCommand = new Command(async () => await OpenMapAsync());
        }

        private async Task OpenMapAsync()
        {
            try
            {
                await Map.OpenAsync(Port.Latitude, Port.Longitude, new MapLaunchOptions
                {
                    Name = Port.Name,
                    NavigationMode = NavigationMode.Default
                });
            }
            catch (Exception ex)
            {
                App.LogCrash(ex, "OpenMapAsync");
                await _alertService.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
            }
        }

        private async Task SharePortAsync()
        {
            try
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Title = "Port Application",
                    Text = $"The application is sharing port {Port.Name} of {Port.Country} country.",
                    Uri = Port.Url
                });
            }
            catch (Exception ex)
            {
                await _alertService.DisplayAlert("Alert", "Sharing Port Failed", "Ok");
                App.LogCrash(ex, "SharePortAsync");
            }
        }
    }
}