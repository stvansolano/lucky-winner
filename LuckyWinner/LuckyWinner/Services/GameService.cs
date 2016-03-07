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

	public class GameService : ResourceService<Game>
    {
        private const string ApiAddress = "https://lucky-winner.firebaseio.com/";
		private const string GAMES = "/Games";

		public GameService(NetworkService networkService) 
			: base(ApiAddress, networkService, (item, key) => item.Id = key)
        {}

		private string GetUrlSuffix(params string[] parts)
		{
			var items = new List<string> (parts);
			items.Insert(0, GAMES);

			return string.Join ("/", items.ToArray());
		}

		public async Task<IEnumerable<Game>> GetUserGamesAsync(string userId)
        {
            var empty = new List<Game>();

            if (Network.IsConnected == false)
            {
                return empty;
            }
				
            try
            {
				var query = await FetchCollection(GetUrlSuffix(".json"), KeySetter);

				return query.Where(item => item.Owner != null && item.Owner == userId);
            }
            catch (Exception ex)
            {
				Debug.WriteLine(ex.Message);
            }
            
            return empty;
        }

		public async Task<Game> GetGameAsync (string gameId)
		{
			var empty = new Game();

			try
			{
				var query = await FetchCollection(GetUrlSuffix(gameId, ".json"), KeySetter);

				return query.FirstOrDefault() ?? empty;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			return empty;
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

        public async Task<Game[]> LoadGamesAsync(string userId)
        {
            var empty = new Game[0];

            try
            {
				var query = await FetchCollection(GetUrlSuffix(".json"), KeySetter);

                return query.Where(item => item.Owner == userId).ToArray();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return empty;
        }
    }
}