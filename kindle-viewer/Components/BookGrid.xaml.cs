using kindle_viewer.Model;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace kindle_viewer.Components
{
    public sealed partial class BookGrid : UserControl
    {
        public BookListObservable Books {
            get { return (BookListObservable)GetValue(bookList); }
            set { SetValue(bookList, value); }
        }

        public static readonly DependencyProperty bookList =
            DependencyProperty.Register("books", typeof(BookListObservable), typeof(BookGrid), null);

        public BookGrid()
        {
            this.InitializeComponent();
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
