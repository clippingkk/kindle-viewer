using kindle_viewer.Misc;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kindle_viewer.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
        }

        private void onLogout(object sender, RoutedEventArgs e) {
            if (Config.JWT == "") {
                return;
            }
            var vault = new Windows.Security.Credentials.PasswordVault();
            vault.Remove(
                new Windows.Security.Credentials.PasswordCredential(
                    AuthViewModel.AUTH_JWT_TOKEN_NS, AuthViewModel.AUTH_USERNAME_NS, Config.JWT
                )
            );
            Config.JWT = "";
        }
    }
}
