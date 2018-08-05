using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using kindle_viewer.pages;
using ClippingKKModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kindle_viewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DropText : Page
    {
        public DropText()
        {
            this.InitializeComponent();
        }

        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            var storageItems = await e.DataView.GetStorageItemsAsync();
            StorageFile file = storageItems.First() as StorageFile;
            var fileType = file.FileType.ToString().ToLower();

            if (!fileType.Equals(".txt"))
            {

                ContentDialog fileTypeAlert = new ContentDialog
                {
                    Title = "兄弟，我要的是 txt 文件啊",
                    Content = "你这个文件类型不对，把 kindle 里的 clipings.txt 丢给我啊喂! ",
                    CloseButtonText = "老子知道了！",
                };

                await fileTypeAlert.ShowAsync();

                return;
            }

            Debug.WriteLine(file.FileType.ToString());
            var clipItems = await this.FileParser(file);

            using (var db = new ClippingContext())
            {
                await db.Clippings.AddRangeAsync(clipItems);
                await db.SaveChangesAsync();
            }

            ContentDialog memAlert = new ContentDialog
            {
                Title = "done",
                Content = "done",
                CloseButtonText = "I know it",
            };

            await memAlert.ShowAsync();

            // var f = Window.Current.Content as Frame;
            // f.Navigate(typeof(ClipListPage));
        }


        private async Task<List<ClippingItem>> FileParser(StorageFile file)
        {
            List<ClippingItem> clipItems = new List<ClippingItem>();

            using (var inputStream = await file.OpenReadAsync())
            using (var classicStream = inputStream.AsStreamForRead())
            using (var streamReader = new StreamReader(classicStream))
            {
                int currentLine = 1;
                ClippingItem clipItem = new ClippingItem();
              
                while (streamReader.Peek() >= 0)
                {
                    var line = streamReader.ReadLine();
                    switch (currentLine % 5)
                    {
                        case 1:
                            clipItem = new ClippingItem();
                            var result = line.Split(' ');
                            clipItem.Title = result.First();
                            clipItem.Author = result.Last();
                            break;
                        case 2:
                            var locationRegex = new Regex(@"Location (\d+-\d+)");
                            clipItem.Location = locationRegex.Match(line).Value;
                            var dateTimeRegex = new Regex(@"(20.*) [A|P]M");
                            var thatTimeString = dateTimeRegex.Match(line).Value;
                            Debug.WriteLine(thatTimeString);
                            // clipItem.createdAt = DateTime.Parse(thatTimeString);
                            break;
                        case 4:
                            clipItem.Content = line;
                            break;
                        case 0:
                            clipItems.Add(clipItem);
                            break;
                    }
                    currentLine++;
                }
            }
            clipItems.ForEach(i => i.DataId = Misc.Encrypt.Sha256(i.Content + i.Location));
            return clipItems;
        }



        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
        }

    }
}
