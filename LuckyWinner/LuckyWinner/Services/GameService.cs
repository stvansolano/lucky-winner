namespace Shared
{
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Net;
    using System.Diagnostics;
    using System.Linq;
	using System.Net.Http;

    public class GameService : RestService
    {
		private const string GAMES = "/Games";
        private const string ApiAddress = "https://lucky-winner.firebaseio.com/";

        public GameService(NetworkService networkService) : base(ApiAddress, networkService)
        {
        }

		private string GetUrlSuffix(params string[] parts)
		{
			return string.Join ("/", GAMES, parts);
		}

		public async Task<IEnumerable<Game>> GetUserGamesAsync(string userId)
        {
            var result = new List<Game>();

            if (Network.IsConnected == false)
            {
                return result;
            }
				
            try
            {
				var resource = GetUrlSuffix(userId, ".json");

				var json = await GetContent(resource).ConfigureAwait(false);

				// JSON.Net deserialization
				var parsed = JsonConvert.DeserializeObject<Dictionary<object, Game>>(json);

				return parsed.Values.Where(item => item.Owner != null && item.Owner.Id == userId).ToArray();
            }
            catch (Exception ex)
            {
				Debug.WriteLine(ex.Message);
            }
            
            return result;
        }

		public async Task<bool> SaveAsync(Game instance)
		{
			if (Network.IsConnected == false)
			{
				return false;
			}
				
			try
			{
				HttpResponseMessage result = null;

				if (string.IsNullOrEmpty(instance.Id)) 
				{
					var response = await Post(GetUrlSuffix(".json"), instance).ConfigureAwait(false);

					instance.Id = await DeserializeId(response).ConfigureAwait(false);
				}
				else 
				{
					result = await Put(GetUrlSuffix(instance.Id, ".json"), instance).ConfigureAwait(false);
				}

				return result != null && result.StatusCode == HttpStatusCode.OK;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return false;
		}

		public async Task<bool> SaveParticipantsAsync(Game instance)
		{
			if (Network.IsConnected == false)
			{
				return false;
			}

			try
			{
				var resource = GetUrlSuffix(instance.Id, "Participants", ".json");

				var result = await Put(resource, instance.Participants).ConfigureAwait(false);

				return result != null && result.StatusCode == HttpStatusCode.OK;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return false;
		}

		public async Task<bool> SaveHistoryAsync (Game instance)
		{
			if (Network.IsConnected == false)
			{
				return false;
			}

			try
			{
				var result = await Put(GetUrlSuffix(instance.Id, "History", ".json"), instance.History).ConfigureAwait(false);

				return result != null && result.StatusCode == HttpStatusCode.OK;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return false;
		}
    }
}