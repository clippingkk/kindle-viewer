﻿using kindle_viewer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Web.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace kindle_viewer.Repository {
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

        public async Task<User> GetUserBy(string id) {
            User user = await this.Get<User>("/auth/" + id);
            return user
        }
    }
}
