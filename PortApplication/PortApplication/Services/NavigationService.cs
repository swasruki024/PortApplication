using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PortApplication.Services
{
    public class NavigationService : INavigation
    {
        public IReadOnlyList<Page> ModalStack => App.Current.MainPage.Navigation.ModalStack;

        public IReadOnlyList<Page> NavigationStack => App.Current.MainPage.Navigation.NavigationStack;

        public void InsertPageBefore(Page page, Page before)
        {
            App.Current.MainPage.Navigation.InsertPageBefore(page, before);
        }

        public Task<Page> PopAsync()
        {
            return App.Current.MainPage.Navigation.PopAsync();
        }

        public Task<Page> PopAsync(bool animated)
        {
            return App.Current.MainPage.Navigation.PopAsync(animated);
        }

        public Task<Page> PopModalAsync()
        {
            return App.Current.MainPage.Navigation.PopModalAsync();
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            return App.Current.MainPage.Navigation.PopModalAsync(animated);
        }

        public Task PopToRootAsync()
        {
            return App.Current.MainPage.Navigation.PopToRootAsync();
        }

        public Task PopToRootAsync(bool animated)
        {
            return App.Current.MainPage.Navigation.PopToRootAsync(animated);
        }

        public Task PushAsync(Page page)
        {
            return App.Current.MainPage.Navigation.PushAsync(page);
        }

        public Task PushAsync(Page page, bool animated)
        {
            return App.Current.MainPage.Navigation.PushAsync(page, animated);
        }

        public Task PushModalAsync(Page page)
        {
            return App.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public Task PushModalAsync(Page page, bool animated)
        {
            return App.Current.MainPage.Navigation.PushModalAsync(page, animated);
        }

        public void RemovePage(Page page)
        {
            App.Current.MainPage.Navigation.RemovePage(page);
        }
    }
}
