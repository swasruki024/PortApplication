using PortApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PortApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortTabletPage : ContentPage
    {
        private PortTabletPageViewModel _viewModel;
        public PortTabletPage()
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<PortTabletPageViewModel>();
            _viewModel = (PortTabletPageViewModel)BindingContext;
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SearchQuery = e.NewTextValue.ToLower();
            PortList.ItemsSource = string.IsNullOrEmpty(_viewModel.SearchQuery) ? _viewModel.Ports : _viewModel.FilteredList;
        }
    }
}