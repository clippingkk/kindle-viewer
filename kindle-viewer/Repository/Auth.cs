using kindle_viewer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Web.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace kindle_viewer.Repository {

    public class ProfileResponse {
        [JsonProperty(PropertyName = "user")]
        public User user { get; set; }
        [JsonProperty(PropertyName = "clippingsCount")]
        public int clippingsCount { get; set; }
        [JsonProperty(PropertyName = "clippings")]
        public List<HTTPClippingItem> Clippings { get; set; }
    }


    public class Auth : KKHttpClient {

        public async Task<User> Login(string email, string pwd) {

            var body = new Model.HttpDataModel.AuthLoginRequest(email, pwd);
            var result = await this.Post<LoginResponseBody>("/auth/login", body);

            var token = result.token;
            var profile = result.profile;
            profile.token = token;
            return profile;
        }

        public async Task<User> Signup(
            string email, string pwd,
            string name, string avatarUrl
        ) {

            var fp = getHardwareID();
            var body = new Model.HttpDataModel.AuthSignupRequest(email, pwd, name, avatarUrl, fp);

            var user = await this.Post<User>("/auth/signup", body);
            return user;
        }


        private string getHardwareID() {
            var hardwareId = Windows.System.Profile.HardwareIdentification.GetPackageSpecificToken(null).Id;
            var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

            byte[] bytes = new byte[hardwareId.Length];
            dataReader.ReadBytes(bytes);
            return BitConverter.ToString(bytes);
        }

        public async Task<ProfileResponse> GetUserBy(string id) {
            ProfileResponse user = await this.Get<ProfileResponse>("/auth/" + id);
            return user;
        }
    }
}
