using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using TTC_Server.Models;

namespace TTC_Server
{
    class WebManager
    {

        private static HttpClient _httpClient = new HttpClient();

        private const string userUrl = "api/users";

        #region UserProfile CRUD
        public static async Task<List<UserProfile>> GetAllUserProfileList()
        {
            var _res = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, Constants.DATASERVERIP + $"{userUrl}"));
            string _apiRes = await _res.Content.ReadAsStringAsync();

            var _users = JsonConvert.DeserializeObject<List<UserProfile>>(_apiRes);

            return _users;
        }

        public static async Task<UserProfile> GetUserProfile(string _name)
        {

            var _res = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, Constants.DATASERVERIP + $"{userUrl}/getuser/{_name}"));
            string _apiRes = await _res.Content.ReadAsStringAsync();

            var _user = JsonConvert.DeserializeObject<UserProfile>(_apiRes);


            return _user;
        }

        public static async Task CreateUserProfile(string _name)
        {
            var _dto = new UserProfile(_name);
            var _json = JsonConvert.SerializeObject(_dto);

            var _req = new HttpRequestMessage(HttpMethod.Post, Constants.DATASERVERIP + $"{userUrl}")
            {
                Content = new StringContent(_json, Encoding.UTF8, "application/json")
            };

            var _res = await _httpClient.SendAsync(_req);
            Console.WriteLine(_res);

            return;
        }

        public static async Task UpdateUserProfile(UserProfile _user)
        {

            var _json = JsonConvert.SerializeObject(_user);
            var _req = new HttpRequestMessage(HttpMethod.Put, Constants.DATASERVERIP + $"{userUrl}/{_user.Id}")
            {
                Content = new StringContent(_json, Encoding.UTF8, "application/json")
            };
            var _res = await _httpClient.SendAsync(_req);

            return;
        }

        public static async Task DeleteUserProfile(string _id)
        {
            var _res = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, Constants.DATASERVERIP + $"{userUrl}/{_id}"));
        }
        #endregion
    }
}
