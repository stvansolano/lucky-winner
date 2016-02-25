namespace Shared
{
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using System.Net;
    using System.Diagnostics;

    public class ParticipantService : RestService
    {
        private const string ApiAddress = "https://lucky-winner.firebaseio.com/";

        public ParticipantService(NetworkService networkService) : base(ApiAddress, networkService)
        {
        }

		public async Task<IEnumerable<Participant>> Get(string raffle)
        {
            var result = new List<Participant>();

            if (Network.IsConnected == false)
            {
                return result;
            }
				
            try
            {
				var json = await Get("/Raffles/" + raffle + "/.json").ConfigureAwait(false);

				// JSON.Net deserialization
				var parsed = JsonConvert.DeserializeObject<Dictionary<object, Participant>>(json);

				return parsed.Values.ToArray();
            }
            catch (Exception ex)
            {
				Debug.WriteLine(ex.Message);
            }
            
            return result;
        }

		public async Task<bool> Register(string raffle, Participant instance)
		{
			if (Network.IsConnected == false)
			{
				return false;
			}
				
			try
			{
				var result = await Post("/Raffles/" + raffle + "/.json", instance).ConfigureAwait(false);

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