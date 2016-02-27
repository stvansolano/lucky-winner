namespace Shared
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Threading.Tasks;
	using Newtonsoft.Json;

    public class UserService : RestService
    {
        private const string ApiAddress = "https://lucky-winner.firebaseio.com/";
        private const string USERS = "/Users";

        public UserService(NetworkService networkService) : base(ApiAddress, networkService)
        {
        }

		private string GetUrlSuffix(params string[] parts)
		{
			var items = new List<string> (parts);
			items.Insert (0, USERS);

			return string.Join ("/", items.ToArray());
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

				if (string.IsNullOrEmpty(json)) {
					return await SaveAsNew(registeringUser);
				}
                // JSON.Net deserialization
                var parsed = JsonConvert.DeserializeObject<Dictionary<object, User>>(json);

				if (parsed == null) {
					return await SaveAsNew(registeringUser);
				}
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