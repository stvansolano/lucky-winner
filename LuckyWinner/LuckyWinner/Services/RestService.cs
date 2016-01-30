namespace Shared
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using ModernHttpClient;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
	using System.Text;

    public abstract class RestService
    {
        protected NetworkService Network { get; set; }
        protected string ApiBaseAddress { get; set; }

        protected RestService(string apiAddress, NetworkService networkService)
        {
            ApiBaseAddress = apiAddress;
            Network = networkService;
        }

        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient(new NativeMessageHandler())
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

		protected async Task<string> Get(string resource)
		{
			using (var httpClient = CreateClient())
			{
				var response = await httpClient.GetAsync(resource).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{
					return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				}
			}
			return string.Empty;
		}

        protected async Task<IEnumerable<T>> Get<T>(string resource) where T: class
        {
            try
            {
                using (var httpClient = CreateClient())
                {
                    var response = await httpClient.GetAsync(resource).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (!string.IsNullOrWhiteSpace(json))
                        {
                            return JsonConvert.DeserializeObject<IEnumerable<T>>(json).ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return Enumerable.Empty<T>();
        }

		protected async Task<HttpResponseMessage> Post<T>(string resource, T instance) where T: class
		{
			try
			{
				using (var httpClient = CreateClient())
				{
					var json = JsonConvert.SerializeObject(instance);
					return await httpClient.PostAsync(resource, new StringContent(json, Encoding.UTF8, "application/json")).ConfigureAwait(false);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			return new HttpResponseMessage{ StatusCode = System.Net.HttpStatusCode.BadRequest };
		}
    }
}