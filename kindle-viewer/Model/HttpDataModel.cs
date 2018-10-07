using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;

namespace kindle_viewer.Model
{
    class HttpDataModel
    {

        public class AuthLoginRequest
        {
            [JsonProperty(PropertyName = "email")]
            public string Email { get; set; }
            [JsonProperty(PropertyName = "pwd")]
            public string Pwd { get; set; }

            public AuthLoginRequest(string email, string pwd) {
                this.Email = email;
                this.Pwd = pwd;

            }
        }
        public class AuthSignupRequest : AuthLoginRequest
        {
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }
            [JsonProperty(PropertyName = "avatarUrl")]
            public string AvatarUrl { get; set; }
            [JsonProperty(PropertyName = "fp")]
            public string fp { get; set; }

            public AuthSignupRequest(
                string email, string pwd,
                string name, string avatarUrl,
                string fp
            ): base(email, pwd) {
                this.Name = name;
                this.AvatarUrl = avatarUrl;
                this.fp = fp;
            }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class ClippingItemRequest
        {
            [JsonProperty(PropertyName = "title")]
            public string Title { get; set; }
            [JsonProperty(PropertyName = "content")]
            public string Content { get; set; }
            [JsonProperty(PropertyName = "pageAt")]
            public string Location { get; set; }
            [JsonProperty(PropertyName = "bookId")]
            public string BookID { get; set; }
        }

        public class ClippingsRequest {
            [JsonProperty(PropertyName = "clippings")]
            public List<ClippingItemRequest> clippings { get; set; }
        }
    }
    
}
