using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// using Windows.Web.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kindle_viewer.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AuthContainer : Page
    {
        private string Email { get; set; }
        private string Pwd { get; set; }
        private string AvatarUrl { get; set; }

        private SystemNavigationManager navigationManager;
        public AuthContainer()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationManager = SystemNavigationManager.GetForCurrentView();
            this.navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            this.navigationManager.BackRequested += this.onAppBarBackRequested;
        }

        private void onAppBarBackRequested(Object sender, BackRequestedEventArgs e)
        {
            var f = Window.Current.Content as Frame;
            if (f.CanGoBack)
            {
                f.GoBack();
            }
            this.navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            this.navigationManager.BackRequested -= this.onAppBarBackRequested;
        }

        private async void DoSignup(object sender, RoutedEventArgs e)
        {

            HttpClient http = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(Email), "email");
            form.Add(new StringContent(Pwd), "pwd");
            form.Add(new StringContent(AvatarUrl), "avatar");

            HttpResponseMessage res = await http.PostAsync(Misc.Config.UrlPrefix + "/auth/signup", form);
            res.EnsureSuccessStatusCode();
            http.Dispose();
            string result = res.Content.ReadAsStringAsync().Result;
        }

        private void ToLogin(object sender, RoutedEventArgs e)
        {

        }
    }
}
