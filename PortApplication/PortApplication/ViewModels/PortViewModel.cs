using PortApplication.Models;
using PortApplication.Services.Interfaces;
using PortApplication.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PortApplication.ViewModels
{
    public class PortViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
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

        public ICommand GoToCommand { get; set; }

        public PortViewModel(IPortService portService, INavigation navigation)
        {
            _portService = portService;
            _navigation = navigation;

            // retrieve all ports
            Ports = _portService.GetAllPorts();

            GoToCommand = new Command(async (param) =>
            {
                if (param != null)
                {
                    await GoToAsync((int)param);
                }
            });
        }

        private async Task GoToAsync(int portId)
        {
            if (IsBusy) return;
            IsBusy = true;
            await _navigation.PushAsync(new PortDetailPage(Ports.FirstOrDefault(x => x.ID == portId)));          
            IsBusy = false;
        }
    }
}
