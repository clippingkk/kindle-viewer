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
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace kindle_viewer
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SystemNavigationManager navigationManager;

        private string PageTitle { get; set; } = "Weclome";

        public MainPage()
        {
            this.InitializeComponent();
        }


        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {

            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingPage));
                return;
            }


            var navItemTag = NavView.MenuItems
                .OfType<NavigationViewItem>()
                .First(i => args.InvokedItem.Equals(i.Content))
                .Tag.ToString();
            switch (navItemTag)
            {
                case "drop":
                    ContentFrame.Navigate(typeof(DropText));
                    PageTitle = "DropText";
                    break;
                case "user":
                    ContentFrame.Navigate(typeof(AuthContainer));
                    PageTitle = "Auth";
                    break;
                case "square":
                    ContentFrame.Navigate(typeof(Square));
                    PageTitle = "Square";
                    break;
                default:
                    break;
            }
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(AuthContainer));
            PageTitle = "Auth";
        }
    }
}
