using System.Net;
using Newtonsoft.Json.Linq;

namespace Shared
{
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Collections.ObjectModel;
	using System.Diagnostics;

    public class FeedService : RestService
    {
        private const string ApiAddress = "https://xamarin-workshop.firebaseio.com/";

        public FeedService(NetworkService networkService) : base(ApiAddress, networkService)
        {
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var result = new List<Category>();

            if (Network.IsConnected == false)
            {
                return result;
            }
				
            try
            {
				var json = await Get("/Categories.json").ConfigureAwait(false);

				// JSON.Net deserialization
				var parsed = JsonConvert.DeserializeObject<Dictionary<object, Category>>(json);

				return parsed.Values.ToArray();
            }
            catch (Exception ex)
            {
				Debug.WriteLine(ex.Message);
            }
            
            return result;
        }

		public async Task<bool> Post(Category instance)
		{
			if (Network.IsConnected == false)
			{
				return false;
			}
				
			try
			{
				var result = await Post("/Categories.json", instance).ConfigureAwait(false);

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