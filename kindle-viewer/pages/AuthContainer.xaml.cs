using kindle_viewer.Misc;
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
        private bool hasError { get; set; }
        private bool AvatarUrlVisiblity { get; set; }

        public AuthContainer()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void DoSignup(object sender, RoutedEventArgs e)
        {

            var signupRequestData = new Model.HttpDataModel.AuthRequest
            {
                Email = Email,
                Pwd = Pwd,
                AvatarUrl = AvatarUrl
            };
            EasyHttp.Http.HttpClient http = new EasyHttp.Http.HttpClient();
            http.Request.Accept = EasyHttp.Http.HttpContentTypes.ApplicationJson;
            try
            {
                var res = http.Post(Misc.Config.UrlPrefix + "/auth/signup", signupRequestData, EasyHttp.Http.HttpContentTypes.ApplicationJson);
                var result = res.DynamicBody;

                if (result.status != 200)
                {
                    this.hasError = true;
                }

                var token = result.data.token;
                Config.JWT = token;
            }
            catch (Exception err)
            {
                this.hasError = true;
                SentryLogger.Log(err);

            }

        }

        private void ToLogin(object sender, RoutedEventArgs e)
        {
            AvatarUrlVisiblity = false;
        }
    }
}
