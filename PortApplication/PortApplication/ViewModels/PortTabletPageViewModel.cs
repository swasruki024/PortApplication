using PortApplication.Models;
using PortApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PortApplication.ViewModels
{
    public class PortTabletPageViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;
        private readonly IPortService _portService;

        private List<Port> _ports;

        public List<Port> Ports
        {
            get => _ports ?? (_ports = new List<Port>());
            set
            {
                _ports = value;
                OnPropertyChanged();
                OnPropertyChanged("FilteredList");
            }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                OnPropertyChanged("SearchQuery");
                OnPropertyChanged("FilteredList");
            }
        }

        public List<Port> FilteredList
        {
            get
            {
                if (string.IsNullOrEmpty(SearchQuery))
                    return Ports;

                var filteredList = Ports.Where(x => x.Name.ToLower().Contains(SearchQuery) || x.Country.ToLower().Contains(SearchQuery)
                || x.PortCode.ToLower().Contains(SearchQuery)).ToList();
                return filteredList;
            }
        }

        private bool _detailsIsVisible;
        public bool DetailsIsVisible
        {
            get { return _detailsIsVisible; }
            set { _detailsIsVisible = value; OnPropertyChanged(); }
        }

        private Port _port;
        public Port Port
        {
            get { return _port; }
            set { _port = value; OnPropertyChanged(); }
        }

        public ICommand ShowDetailsCommand { get; set; }

        public ICommand SharePortCommand { get; set; }

        public ICommand OpenMapCommand { get; set; }

        public PortTabletPageViewModel(IPortService portService, IAlertService alertService)
        {
            _alertService = alertService;
            _portService = portService;

            // retrieve all ports
            Ports = _portService.GetAllPorts();

            ShowDetailsCommand = new Command((param) =>
            {
                if (param != null)
                {
                    ShowDetails((int)param);
                }
            });

            SharePortCommand = new Command(async () => await SharePortAsync());

            OpenMapCommand = new Command(async () => await OpenMapAsync());
        }

        private void ShowDetails(int param)
        {
            if (IsBusy) return;
            IsBusy = true;

            DetailsIsVisible = true;
            Port = Ports.FirstOrDefault(x => x.ID == param);

            IsBusy = false;
        }

        private async Task OpenMapAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

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

            IsBusy = false;
        }

        private async Task SharePortAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

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

            IsBusy = false;
        }
    }
}
