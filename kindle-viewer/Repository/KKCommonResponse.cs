using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Deserializers;

namespace kindle_viewer.Repository {
    class SingleOrArrayConverter<T> : JsonConverter {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<T>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            /*
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }
            */
            return new List<T> { token.ToObject<T>() };
        }

        public override bool CanWrite {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class KKCommonResponse {
        [JsonProperty(PropertyName = "status")]
        public int status { get; set; }
        [JsonProperty(PropertyName = "msg")]
        public string msg { get; set; }
        [JsonProperty(PropertyName = "data")]
        public dynamic data { get; set; }
    }

    public class UserResponse {
        [JsonProperty(PropertyName = "status")]
        public int status { get; set; }
        [JsonProperty(PropertyName = "msg")]
        public string msg { get; set; }
        [JsonProperty(PropertyName = "data")]
        public User data { get; set; }
    }

    public class User {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string email { get; set; }
        [JsonProperty(PropertyName = "avatar")]
        public string avatar { get; set; }
        [JsonProperty(PropertyName = "checked")]
        public bool checkedMark { get; set; }
        public string token { get; set; } = "";
    }

    public class LoginResponse {
        public int status { get; set; }
        public string msg { get; set; }
        public LoginResponseBody data { get; set; }
    }

    public class LoginResponseBody {
        [JsonProperty(PropertyName = "profile")]
        public User profile { get; set; }
        [JsonProperty(PropertyName = "token")]
        public string token { get; set; }
    }
}
