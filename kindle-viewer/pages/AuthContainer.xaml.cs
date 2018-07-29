using kindle_viewer.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private bool IsSignupMode { get; set; }

        public AuthContainer()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void DoAuth(object sender, RoutedEventArgs e)
        {
            var signupRequestData = new Model.HttpDataModel.AuthSignupRequest
            {
                Email = Email,
                Pwd = Pwd,
                AvatarUrl = "av",
                Name = "name"
            };
            EasyHttp.Http.HttpClient http = new EasyHttp.Http.HttpClient(Config.UrlPrefix);
            var url = String.Format("/auth/{0}", IsSignupMode ? "signup" : "login");
            try
            {
                var res = http.Post(url, signupRequestData, EasyHttp.Http.HttpContentTypes.ApplicationJson);
                var result = res.DynamicBody;

                if (result.status != 200)
                {
                    this.hasError = true;
                    var err = new Exception(result.msg);
                    throw err;
                } else
                {
                    var token = result.data.token;
                    Config.JWT = token;
                }
            }
            catch (Exception err)
            {
                this.hasError = true;
                SentryLogger.Log(err);

            }

        }

        private void ToLogin(object sender, RoutedEventArgs e)
        {
            IsSignupMode = false;
        }
    }
}
