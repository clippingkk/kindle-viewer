using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using kindle_viewer.Common;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace kindle_viewer.Model
{
    class DBBookInfo : BindableBase
    {

        static private string placeholderImageUrl = "https://via.placeholder.com/270x328";

        private string rating;
        private ImageSource image;
        private string ebookUrl;
        private string author;

        public DBBookInfo(
            )
        {
            this.rating = "0.0";
            this.image = new BitmapImage(new Uri(DBBookInfo.placeholderImageUrl));
            this.ebookUrl = "https://AnnatarHe.com";
            this.author = "AnnatarHe";
        }

        public string Rating
        {
            get { return this.rating; }
            set
            {
                this.SetProperty(ref this.rating, value);
            }
        }
        public ImageSource Image
        {
            get { return this.image; }
            set
            {
                this.SetProperty(ref this.image, value);
            }
        }
        public string EbookUrl
        {
            get { return this.ebookUrl; }
            set
            {
                this.SetProperty(ref this.ebookUrl, value);
            }
        }
        public string Author
        {
            get { return this.author; }
            set
            {
                this.SetProperty(ref this.author, value);
            }
        }
    }

}
