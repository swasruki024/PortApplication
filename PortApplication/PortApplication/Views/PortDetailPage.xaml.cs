using PortApplication.Interfaces;
using PortApplication.Models;
using PortApplication.ViewModels;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PortApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortDetailPage : ContentPage
    {        
        public PortDetailViewModel _viewModel;
        public PortDetailPage(Port port)
        {
            InitializeComponent();            
            _viewModel = (PortDetailViewModel)(BindingContext = App.GetViewModel<PortDetailViewModel>());
            _viewModel.Port = port;
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            (sender as Button).IsEnabled = false;

            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                image.Source = ImageSource.FromStream(() => stream);
            }
            
            (sender as Button).IsEnabled = true;
        }
    }
}