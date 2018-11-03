using kindle_viewer.Common;
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
using System.Diagnostics;
using kindle_viewer.Repository;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kindle_viewer.pages {
    class AuthViewModel : BindableBase {
        static public string AUTH_JWT_TOKEN_NS = "clippingkk.jwt";
        static public string AUTH_USERNAME_NS = "username";

        private bool hasError;
        private bool isSignupMode;
        public bool HasError {
            get { return this.hasError; }
            set {
                this.SetProperty(ref this.hasError, value);
            }
        }

        public string PageH1Title {
            get {
                return this.IsSignupMode ? "Signup" : "Login";
            }
        }

        public string PageHasAccountTip {
            get {
                
                return this.IsSignupMode ? "I had an account" : "I need an account";
            }
        }


        public bool IsSignupMode {
            get { return this.isSignupMode; }
            set {
                this.SetProperty(ref this.isSignupMode, value);
                this.OnPropertyChanged("PageH1Title");
                this.OnPropertyChanged("PageHasAccountTip");
            }
        }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AuthContainer : Page {
        private string Email { get; set; }
        private string Pwd { get; set; }
        private string AvatarUrl { get; set; }
        private string Username { get; set; }
        private AuthViewModel authViewModel { get; set; }

        public AuthContainer() {
            this.InitializeComponent();
            this.authViewModel = new AuthViewModel();
        }

        private async void DoAuth(object sender, RoutedEventArgs e) {
            if (!await this.isFormatValidAsync()) {
                return;
            }

            try {
                Auth auth = new Auth();
                var res = await (authViewModel.IsSignupMode ?
                    auth.Signup(Email, Pwd, Username, AvatarUrl) :
                    auth.Login(Email, Pwd));

                if (this.authViewModel.IsSignupMode) {
                    await (new Dialogs.SignupSuccess()).ShowAsync();
                    this.authViewModel.IsSignupMode = false;
                    return;
                }
                this.authViewModel.IsSignupMode = false;

                Config.JWT = res.token;
                Config.uid = res.id;
                this.setToCredentials(res.id, res.token);
                this.toProfilePage();
            } catch (Exception err) {
                this.authViewModel.HasError = true;
            }
            
        }

        private async System.Threading.Tasks.Task<bool> isFormatValidAsync() {
            var emailValid = false;

            try {
                var addr = new System.Net.Mail.MailAddress(Email);
                if (addr.Address == Email) {
                    emailValid = true;
                }
            } catch {
                emailValid = false;
            }

            if (!emailValid) {
                // ui alert
                var d = new Dialogs.FormValidFail("email");
                await d.ShowAsync();
                return false;
            }

            if (Pwd.Length < 6) {
                var d = new Dialogs.FormValidFail("password");
                await d.ShowAsync();
                return false;
            }

            if (!this.authViewModel.IsSignupMode) {
                return true;
            }

            var isAvatarUrl = Uri.IsWellFormedUriString(AvatarUrl, UriKind.RelativeOrAbsolute);
            if (!isAvatarUrl) {
                var d = new Dialogs.FormValidFail("avatar url");
                await d.ShowAsync();
                return false;
            }

            return true;
        }

        private void setToCredentials(int uid, string token) {

            var data = uid.ToString() + "|" + token;

            var vault = new Windows.Security.Credentials.PasswordVault();
            vault.Add(new Windows.Security.Credentials.PasswordCredential(
                AuthViewModel.AUTH_JWT_TOKEN_NS, AuthViewModel.AUTH_USERNAME_NS, data
            ));
        }

        private void toProfilePage() {
            var f = this.Frame;
            f.Navigate(typeof(Profile));
        }

        private void ToggleMode(object sender, RoutedEventArgs e) {
            this.authViewModel.IsSignupMode = !this.authViewModel.IsSignupMode;
        }
    }
}
