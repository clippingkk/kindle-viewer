using JsonFx.Json;
using System;

namespace kindle_viewer.Model
{
    class HttpDataModel
    {

        public class AuthLoginRequest
        {
            [JsonName("email")]
            public string Email { get; set; }
            [JsonName("pwd")]
            public string Pwd { get; set; }
        }
        public class AuthSignupRequest : AuthLoginRequest
        {
            [JsonName("name")]
            public string Name { get; set; }
            [JsonName("avatarUrl")]
            public string AvatarUrl { get; set; }

            [JsonName("fp")]
            public string FingerPrint { get; set; }
        }

        public class ClippingItemRequest
        {
            [JsonName("title")]
            public string Title { get; set; }
            [JsonName("content")]
            public string Content { get; set; }
            [JsonName("pageAt")]
            public string Location { get; set; }
            [JsonName("bookId")]
            public string BookID { get; set; }
        }
    }
}
