using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shared
{
    public class UserService : RestService
    {
        private const string ApiAddress = "https://lucky-winner.firebaseio.com/";
        private const string PARENT_RESOURCE = "Users";

        public UserService(NetworkService networkService) : base(ApiAddress, networkService)
        {
        }

        private User GetUnregisteredUser()
        {
            return new User { RegistrationKey = Guid.NewGuid().ToString() };
        }

        public async Task<User> Checkin(string registrationKey)
        {
            var registeringUser = GetUnregisteredUser();

            if (string.IsNullOrEmpty(registrationKey))
            {
                if (Network.IsConnected == false)
                {
                    return registeringUser;
                }

                var response = await Post(PARENT_RESOURCE + "/.json", registeringUser);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
						registeringUser.Id = JsonConvert.DeserializeXNode(json).Descendants("name").First().Value;
                    }
                }
                return registeringUser;
            }

            try
            {
                var json = await GetContent(PARENT_RESOURCE + "/.json").ConfigureAwait(false);

                // JSON.Net deserialization
                var parsed = JsonConvert.DeserializeObject<Dictionary<object, User>>(json);

                return parsed.Values.FirstOrDefault(item => item.RegistrationKey == registrationKey) ?? registeringUser;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return registeringUser;
        }
    }
}