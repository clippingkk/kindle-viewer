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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kindle_viewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClipListPage : Page
    {

        private ClipListObservable clipList = new ClipListObservable();

        public ClipListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var list = (List<Model.ClippingItem>)e.Parameter;

            this.clipList.setupClips(list);

            this.memoryAlert(list.Count);
        }

        private void memoryAlert(int count)
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

            memAlert.ShowAsync();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var f = Window.Current.Content as Frame;
            var index = (sender as ListView).SelectedIndex;
            f.Navigate(typeof(DetailPage), this.clipList.ElementAt(index), new DrillInNavigationTransitionInfo());
        }
    }

    
}
