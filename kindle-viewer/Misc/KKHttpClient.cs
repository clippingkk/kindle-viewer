using kindle_viewer.Repository;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace kindle_viewer.Misc {
    public class KKHttpException : Exception {
        public readonly dynamic response;
        public readonly string url;

        public KKHttpException(string url, KKCommonResponse response) {
            this.response = response;
            this.url = url;
        }

        public override string Message {
            get {
                string info = "Request to " + url + " failed with status code " + response.status + 
                    " content: " + response.msg;
                return info;
            }
        }
    }

    public class KKHttpClient {
        private HttpClient http;
        private JsonSerializer serializer;

        public KKHttpClient() {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Config.JWT);
            serializer = new JsonSerializer();
            http = client;
        }

        private void LogError(string url, HttpResponseMessage response) {
            string info = "Request to " + url + " failed with status code " + response.StatusCode + 
                "content: " + response.Content;
            //Acquire the actual exception
            var ex = new Exception(info);
            info = string.Empty;

            //Log the exception and info message
            SentryLogger.Log(ex);
            throw ex;
        }

        public async Task<T> Post<T>(string url, dynamic body) {
            HttpStringContent content = getJsonContent(body);
            var uri = new Uri(Config.UrlPrefix + url);

            HttpResponseMessage result = await http.PostAsync(uri, content);
            if (!result.IsSuccessStatusCode) {
                LogError(url, result);
            };

            var resultContentString = await result.Content.ReadAsStringAsync();
            KKCommonResponse resultObj = serializer.Deserialize<KKCommonResponse>(resultContentString);
            result.Dispose();
            if (resultObj.status != 200) {
                var e = new KKHttpException(url, resultObj);
                SentryLogger.Log(e);
                return default(T);
            }
            var d = serializer.Deserialize<T>(resultObj.data);
            return d;
        }
        public async Task<T> Get<T>(string url) {
            Uri uri = new Uri(url);
            HttpResponseMessage result = await http.GetAsync(uri);
            if (!result.IsSuccessStatusCode) {
                LogError(url, result);
            };
            var resultContentString = await result.Content.ReadAsStringAsync();
            KKCommonResponse resultObj = serializer.Deserialize<KKCommonResponse>(resultContentString);
            result.Dispose();
            if (resultObj.status != 200) {
                var e = new KKHttpException(url, resultObj);
                SentryLogger.Log(e);
                return default(T);
            }
            var d = serializer.Deserialize<T>(resultObj.data);
            return d;
        }
        
        public async Task<dynamic> Post(string url, dynamic body) {

            HttpStringContent content = getJsonContent(body);
            var uri = new Uri(Config.UrlPrefix + url);

            HttpResponseMessage result = await http.PostAsync(uri, content);
            if (!result.IsSuccessStatusCode) {
                LogError(url, result);
            };

            var resultContentString = await result.Content.ReadAsStringAsync();
            KKCommonResponse resultObj = serializer.Deserialize<KKCommonResponse>(resultContentString);
            result.Dispose();
            if (resultObj.status != 200) {
                var e = new KKHttpException(url, resultObj);
                SentryLogger.Log(e);
            }
            return resultObj.data;
        }


        private HttpStringContent getJsonContent(dynamic body) {
            string jsonData = serializer.Serialize(body);
            var content = new HttpStringContent(jsonData);
            content.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");
            return content;
        }

        private async Task<KKCommonResponse> serializeBodyAsync(dynamic result, string url) {
            if (!result.IsSuccessStatusCode) {
                LogError(url, result);
            };

            var resultContentString = await result.Content.ReadAsStringAsync();
            KKCommonResponse resultObj = serializer.Deserialize<KKCommonResponse>(resultContentString);
            result.Dispose();
            if (resultObj.status != 200) {
                var e = new KKHttpException(url, resultObj);
                SentryLogger.Log(e);
            }
            return resultObj;
        }

        ~KKHttpClient() {
            http.Dispose();
        }
    }

}
