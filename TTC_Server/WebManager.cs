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

        public static List<DefaultUser> GetDefaultUserDataList()
        {
            var res = new List<DefaultUser>();


            return res;
        }

        public static async Task<DefaultUser> GetDefaultUserDto(string _name)
        {

            var _res = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, Constants.DATASERVERIP + $"{userUrl}/getuser/{_name}"));
            string _apiRes = await _res.Content.ReadAsStringAsync();

            var _user = JsonConvert.DeserializeObject<DefaultUser>(_apiRes);


            return _user;
        }

        public static async Task CreateDefaultUser(string _name)
        {
            var _dto = new DefaultUser(_name);
            var _json = JsonConvert.SerializeObject(_dto);

            var _req = new HttpRequestMessage(HttpMethod.Post, Constants.DATASERVERIP + $"{userUrl}")
            {
                Content = new StringContent(_json, Encoding.UTF8, "application/json")
            };

            var _res = await _httpClient.SendAsync(_req);

            return;
        }
    }
}
