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
using Windows.Data.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kindle_viewer
{
    class DBBookInfo
    {
        public string rating { get; set; }
        public string image { get; set; }
        public string ebookUrl { get; set; }
        public string author { get; set; }

        public DBBookInfo(
            string rating,
            string image,
            string ebookUrl,
            string author
            )
        {
            this.rating = rating;
            this.image = image;
            this.ebookUrl = ebookUrl;
            this.author = author;
        }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailPage : Page
    {

        private ClipItem clipItem;
        private DBBookInfo book;

        public DetailPage()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var clipItem = (ClipItem)e.Parameter;
            this.clipItem = clipItem;

            LoadBookInfo();
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

            var d = JsonObject.Parse(jsonString).GetObject();
            var firstBook = d.GetNamedArray("books").First().GetObject();
            var rating = firstBook.GetNamedObject("rating").GetNamedString("average");
            var image = firstBook.GetNamedString("image");
            var ebookUrl = firstBook.GetNamedString("ebook_url");
            var authors = firstBook.GetNamedArray("author");

            DBBookInfo bookInfo = new DBBookInfo(
                rating, image, ebookUrl, string.Join(", ", authors)
                );
            this.book = bookInfo;

            Debug.WriteLine(jsonString);
            return jsonString;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var f = Window.Current.Content as Frame;

            if (f.CanGoBack)
            {
                f.GoBack();
            }
        }
    }
}
