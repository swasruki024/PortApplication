using PortApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PortApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortPage : ContentPage
    {
        private PortViewModel _viewModel;

        public PortPage()
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<PortViewModel>();
            _viewModel = (PortViewModel)BindingContext;
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SearchQuery = e.NewTextValue.ToLower();
            PortList.ItemsSource = string.IsNullOrEmpty(_viewModel.SearchQuery) ? _viewModel.Ports : _viewModel.FilteredList;
        }
    }
}