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
using System.Diagnostics;
using Windows.UI.Xaml.Media.Imaging;
using kindle_viewer.Model;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kindle_viewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailPage : Page
    {

        private ClipItem clipItem;
        private SystemNavigationManager navigationManager;
        private DBBookInfo book { get; set; } = new DBBookInfo();

        public DetailPage()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var clipItem = (ClipItem)e.Parameter;
            this.clipItem = clipItem;

            this.navigationManager = SystemNavigationManager.GetForCurrentView();
            this.navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            this.navigationManager.BackRequested += this.onAppBarBackRequested;
            LoadBookInfo();
        }

        private void onAppBarBackRequested(Object sender, BackRequestedEventArgs e)
        {
            var f = Window.Current.Content as Frame;
            if (f.CanGoBack)
            {
                f.GoBack();
            }
            this.navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            this.navigationManager.BackRequested -= this.onAppBarBackRequested;
        }



        private async Task<String> LoadBookInfo()
        {
            String jsonString;
            var title = this.clipItem.title;
            if (title.Contains("(") && title.Contains(")"))
            {
                title = title.Split('(')[0];
            }

            String url = String.Format("https://api.douban.com/v2/book/search?q={0}", title);
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var stream = await httpClient.GetStreamAsync(url);
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
            }
            var bookInfoData = Newtonsoft.Json.Linq.JObject.Parse(jsonString)["books"].Children().ToList().First();
            var rating = bookInfoData["rating"]["average"].ToString();
            var image = bookInfoData["image"].ToString();
            var ebookUrl = bookInfoData["ebook_url"] == null ? "https://AnnatarHe.com" : bookInfoData["ebook_url"].ToString();


            // var authors = bookInfoData["authors"].Children().ToArray().ToString();
            var authorsInData = bookInfoData["authors"];

            var authors = authorsInData == null ? "" : authorsInData.Children().ToArray().ToString();

            this.book.Rating = rating;
            this.book.Author = authors;
            this.book.EbookUrl = ebookUrl;
            this.book.Image = new BitmapImage(new Uri(image));

            return jsonString;
        }
    }
}
