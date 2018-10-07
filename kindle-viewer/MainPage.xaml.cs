using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using Windows.Storage;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using kindle_viewer.pages;
using Windows.UI.Core;
using kindle_viewer.Misc;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace kindle_viewer {

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {

        public MainPage() {
            this.InitializeComponent();
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args) {

            if (args.IsSettingsInvoked) {
                NavView.Header = "Settings";
                ContentFrame.Navigate(typeof(SettingPage));
                return;
            }

            var navItemTag = NavView.MenuItems
                .OfType<NavigationViewItem>()
                .First(i => args.InvokedItem.Equals(i.Content))
                .Tag.ToString();
            var windowFrame = Window.Current.Content as Frame;
            switch (navItemTag) {
                case "clippings":
                    windowFrame.Navigate(typeof(ClipListPage));
                    break;
                case "user":
                    NavView.Header = "Profile";
                    var page = this.getProfilePage();
                    ContentFrame.Navigate(page);
                    break;
                case "square":
                    ContentFrame.Navigate(typeof(Square));
                    NavView.Header = "Square";
                    break;
                case "reupload":
                    ContentFrame.Navigate(typeof(DropText));
                    NavView.Header = "Reupload";
                    break;
                default:
                    break;
            }
        }

        private Type getProfilePage() {
            Windows.Security.Credentials.PasswordCredential credential = null;

            var vault = new Windows.Security.Credentials.PasswordVault();
            try {
                var credentialList = vault.FindAllByResource(AuthViewModel.AUTH_JWT_TOKEN_NS);
                if (credentialList.Count > 0) {
                    if (credentialList.Count == 1) {
                        credential = credentialList[0];
                    } else {
                        credential = vault.Retrieve(AuthViewModel.AUTH_JWT_TOKEN_NS, AuthViewModel.AUTH_USERNAME_NS);
                    }
                }

            } catch {
                // do nothing
            }

            if (credential == null) {
                return typeof(AuthContainer);
            }

            credential.RetrievePassword();
            var jwt = credential.Password;
            Config.JWT = jwt;
            return typeof(Profile);
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e) {
            ContentFrame.Navigate(this.getProfilePage());
            NavView.Header = "Auth";
        }
    }
}
