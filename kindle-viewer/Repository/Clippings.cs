﻿using kindle_viewer.Misc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kindle_viewer.Model;
using Newtonsoft.Json;

namespace kindle_viewer.Repository {


    public class HTTPClippingItem
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
        [JsonProperty(PropertyName = "bookId")]
        public string BookId { get; set; }
        [JsonProperty(PropertyName = "pageAt")]
        public string Location { get; set; }
        [JsonProperty(PropertyName = "seq")]
        public string Sequence { get; set; }
    }

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
