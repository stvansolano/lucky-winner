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
        private const string USERS = "Users";

        public UserService(NetworkService networkService) : base(ApiAddress, networkService)
        {
        }


		private string GetUrlSuffix(params string[] parts)
		{
			return string.Join ("/", USERS, parts);
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
				return await SaveAsNew(registeringUser);
            }

            try
            {
				var json = await GetContent(GetUrlSuffix(".json")).ConfigureAwait(false);

                // JSON.Net deserialization
                var parsed = JsonConvert.DeserializeObject<Dictionary<object, User>>(json);

                var match = parsed.Values.FirstOrDefault(item => item.RegistrationKey == registrationKey);

				if (match == null) {
					return await SaveAsNew(registeringUser);
				}
				return match;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return registeringUser;
        }

		private async Task<User> SaveAsNew (User registeringUser)
		{
			if (Network.IsConnected == false)
			{
				return registeringUser;
			}

			var response = await Post(GetUrlSuffix(".json"), registeringUser);
			registeringUser.Id = await DeserializeId(response);

			return registeringUser;
		}
    }
}