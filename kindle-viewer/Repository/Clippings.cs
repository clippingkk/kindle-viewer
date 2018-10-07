using kindle_viewer.Misc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kindle_viewer.Model;

namespace kindle_viewer.Repository {
    class Clippings : KKHttpClient {

        public async void UplodClippings(List<HttpDataModel.ClippingItemRequest> clippingItems) {
            var reqBody = new HttpDataModel.ClippingsRequest() {
                clippings = clippingItems,
            };

            var result = await Post("/clippings/multip/create", reqBody);

            Console.WriteLine(result ?? "need auth");
        }
    }
}
