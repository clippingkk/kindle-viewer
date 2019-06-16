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
using ClippingKKModel;

namespace kindle_viewer
{
    public class ClipListObservable: ObservableCollection<ClippingItem>, ISupportIncrementalLoading
    {
        private ClippingContext clippingContext;
        private int offset = 0;
        private readonly int take = 20;

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            var dispatcher = Window.Current.Dispatcher;

            return Task.Run(async () =>
            {
                var data = this.LoadMore();

                if (data.Count > 0)
                {
                    await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        foreach (ClippingItem clip in data)
                        {
                            this.Add(clip);
                        }
                    });

                }

                return new LoadMoreItemsResult() { Count = (uint)data.Count };
            }).AsAsyncOperation<LoadMoreItemsResult>();
        }

        public List<ClippingItem> LoadMore()
        {
            if (!this.HasMoreItems)
            {
                return new List<ClippingItem>();
            }

            var data = this.clippingContext.Clippings.Skip(offset).Take(take).ToList();
            offset += take;

            return data;
        }

        public void SetupClips()
        {
            var d = this.LoadMore();
            var c = this.clippingContext.Clippings.Count();
            foreach (ClippingItem clip in d)
            {
                this.Add(clip);
            }

        }

        public ClipListObservable()
        {
            this.clippingContext = new ClippingContext();
        }

        ~ClipListObservable()
        {
            this.clippingContext.Dispose();
        }

        public bool HasMoreItems => offset < this.clippingContext.Clippings.Count();
    }
}
