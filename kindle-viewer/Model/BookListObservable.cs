using ClippingKKModel;
using kindle_viewer.Misc;
using kindle_viewer.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace kindle_viewer.Model
{
    public class BookListObservable: ObservableCollection<KKBookItem>, ISupportIncrementalLoading
    {
        private int offset = 0;
        private readonly int take = 20;
        private bool hasMore = true;

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            var bookAPI = new Books();
            var dispatcher = Window.Current.Dispatcher;

            return Task.Run(async () => {

                var data = await bookAPI.FetchBooks(Config.uid, offset, take);
                offset += 20;

                if (data.Count == 0) {
                    hasMore = false;
                }

                if (data.Count > 0) {

                    await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        foreach (KKBookItem book in data)
                        {
                            this.Add(book);
                        }
                    });
                }


                return new LoadMoreItemsResult() { Count = (uint)data.Count };
            }).AsAsyncOperation<LoadMoreItemsResult>();
        }

        public bool HasMoreItems => hasMore;
    }
}
