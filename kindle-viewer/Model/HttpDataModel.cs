using JsonFx.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
