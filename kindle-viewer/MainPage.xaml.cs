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
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace kindle_viewer
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {


        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            List<ClipItem> clipItems = new List<ClipItem>();
            var storageItems = await e.DataView.GetStorageItemsAsync();
            StorageFile file = storageItems.First() as StorageFile;


            using (var inputStream = await file.OpenReadAsync())
            using (var classicStream = inputStream.AsStreamForRead())
            using (var streamReader = new StreamReader(classicStream))
            {
                int currentLine = 1;
                ClipItem clipItem = new ClipItem();
              
                while (streamReader.Peek() >= 0)
                {
                    var line = streamReader.ReadLine();
                    switch (currentLine % 5)
                    {
                        case 1:
                            clipItem = new ClipItem();
                            var result = line.Split(' ');
                            clipItem.title = result.First();
                            clipItem.author = result.Last();
                            break;
                        case 2:
                            var locationRegex = new Regex(@"Location (\d+-\d+)");
                            clipItem.location = locationRegex.Match(line).Value;
                            var dateTimeRegex = new Regex(@"(20.*) [A|P]M");
                            var thatTimeString = dateTimeRegex.Match(line).Value;
                            Debug.WriteLine(thatTimeString);
                            // clipItem.createdAt = DateTime.Parse(thatTimeString);
                            break;
                        case 4:
                            clipItem.content = line;
                            break;
                        case 0:
                            clipItems.Add(clipItem);
                            break;
                    }
                    currentLine++;
                }
            }

            var item = clipItems.ElementAt(3);
            // Debug.WriteLine(item.content, item.title, item.author, item.location);
            var f = Window.Current.Content as Frame;
            f.Navigate(typeof(ClipListPage), clipItems);
        }

        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
        }

    }
}
