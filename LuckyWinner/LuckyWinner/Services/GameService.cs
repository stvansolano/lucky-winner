namespace Shared
{
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Net;
    using System.Diagnostics;
    using System.Linq;

    public class GameService : RestService
    {
        private const string PARENT_RESOURCE = "/Games/";
        private const string ApiAddress = "https://lucky-winner.firebaseio.com/";

        public GameService(NetworkService networkService) : base(ApiAddress, networkService)
        {
        }

		public async Task<IEnumerable<Game>> GetUserGames(string userId)
        {
            var result = new List<Game>();

            if (Network.IsConnected == false)
            {
                return result;
            }
				
            try
            {
				var json = await GetContent(PARENT_RESOURCE + userId + "/.json").ConfigureAwait(false);

				// JSON.Net deserialization
				var parsed = JsonConvert.DeserializeObject<Dictionary<object, Game>>(json);

				return parsed.Values.Where(item => item.Owner != null && item.Owner.Id == userId)
                                           .ToArray();
            }
            catch (Exception ex)
            {
				Debug.WriteLine(ex.Message);
            }
            
            return result;
        }

		public async Task<bool> Save(Game instance)
		{
			if (Network.IsConnected == false)
			{
				return false;
			}
				
			try
			{
				var result = await Post(PARENT_RESOURCE + "/.json", instance).ConfigureAwait(false);

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