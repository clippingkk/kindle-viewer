using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using System.Diagnostics;

namespace kindle_viewer
{
    class ClipListObservable: ObservableCollection<Model.ClippingItem>, ISupportIncrementalLoading
    {

        private List<Model.ClippingItem> clipItems;

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {

            var dispatcher = Window.Current.Dispatcher;

            return Task.Run(async () =>
            {
                var data = this.loadMore();

                if (data != null && data.Count > 0)
                {
                    await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        Debug.WriteLine("load success");
                        foreach (Model.ClippingItem clip in data)
                        {
                            this.Add(clip);
                        }
                    });

                }

                return new LoadMoreItemsResult() { Count = (uint)data.Count };
            }).AsAsyncOperation<LoadMoreItemsResult>();
        }

        public List<Model.ClippingItem> loadMore()
        {
            if (!this.HasMoreItems)
            {
                return null;
            }

            var count = this.clipItems.Count >= 20 ? 20 : this.clipItems.Count;

            var data = this.clipItems.Take(count).ToList();
            this.clipItems.RemoveRange(0, count);

            return data;
        }

        public void setupClips(List<Model.ClippingItem> clips)
        {
            this.clipItems = clips;
            var d = this.loadMore();
            foreach (Model.ClippingItem clip in d)
            {
                this.Add(clip);
            }
        }

        public bool HasMoreItems => this.clipItems.Count > 0;
    }
}
