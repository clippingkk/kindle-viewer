using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace kindle_viewer.Components
{
    public sealed partial class ClippingList : UserControl
    {
        public ClipListObservable Clippings {
            get { return (ClipListObservable)GetValue(clippingList); }
            set { SetValue(clippingList, value); }
        }

        public static readonly DependencyProperty clippingList =
            DependencyProperty.Register("list", typeof(ClipListObservable), typeof(ClippingList), null);

        public ClippingList()
        {
            this.InitializeComponent();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var f = Window.Current.Content as Frame;
            var index = (sender as ListView).SelectedIndex;
            f.Navigate(typeof(DetailPage), this.Clippings.ElementAt(index));
        }
    }
}
