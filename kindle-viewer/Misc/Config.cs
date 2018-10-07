using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using RestSharp;

namespace kindle_viewer.Misc
{
    class Config
    {
        public static string UrlPrefix {
            get {
#if DEBUG
                // return "https://api.clippingkk.annatarhe.com/api";
                return "http://localhost:9654/api";
#else
                return "https://api.clippingkk.annatarhe.com/api";
#endif
            }
        }

        public static string JWT = "";
    }
}
