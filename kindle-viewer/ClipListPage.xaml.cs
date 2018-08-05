using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Core;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kindle_viewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClipListPage : Page
    {
        private SystemNavigationManager navigationManager;
        private ClipListObservable clipList = new ClipListObservable();

        public ClipListPage()
        {
            this.InitializeComponent();
        }

        private void onAppBarBackRequested(Object sender, BackRequestedEventArgs e)
        {
            var f = Window.Current.Content as Frame;
            if (f.CanGoBack)
            {
                f.GoBack();
            }
            if (f.BackStackDepth > 0)
            {
                return;
            }
            this.navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            this.navigationManager.BackRequested -= this.onAppBarBackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            this.clipList.SetupClips();
            if (e.NavigationMode == NavigationMode.New)
            {
                this.navigationManager = SystemNavigationManager.GetForCurrentView();
                this.navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                this.navigationManager.BackRequested += this.onAppBarBackRequested;
            }
        }

        private async void memoryAlert(int count)
        {
            if (count < 30000)
            {
                return;
            }

            ContentDialog memAlert = new ContentDialog
            {
                Title = "Memory Alert",
                Content = "App might be crash after you scroll to very bottom, because your clips file so large",
                CloseButtonText = "I know it",
            };

            await memAlert.ShowAsync();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var f = Window.Current.Content as Frame;
            var index = (sender as ListView).SelectedIndex;
            f.Navigate(typeof(DetailPage), this.clipList.ElementAt(index), new DrillInNavigationTransitionInfo());
        }
    }

    
}
