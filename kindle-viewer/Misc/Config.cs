using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kindle_viewer.Misc
{
    class Config
    {
        public static string UrlPrefix {
            get {
#if DEBUG
                // return "http://api.clippingkk.annatarhe.com/api";
                return "http://localhost:9654/api";
#else
                return "https://api.clippingkk.annatarhe.com/api";
#endif
            }
        }

        public static string JWT { get; set; }

        public static EasyHttp.Http.HttpClient GetHttpClient() {
            EasyHttp.Http.HttpClient http = new EasyHttp.Http.HttpClient(Config.UrlPrefix);
            http.Request.AddExtraHeader("jwt-token", Config.JWT);
            return http;
        }
    }
}
