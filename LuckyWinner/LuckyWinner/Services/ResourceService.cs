namespace Shared
{
	using System;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using Newtonsoft.Json;

	public abstract class ResourceService<T> : RestService
		where T : class, new()
	{
		protected Action<T, string> KeySetter { get; set; }

		public ResourceService (string apiAddress, NetworkService networkService, Action<T, string> keySetter) : base(apiAddress, networkService) {
			KeySetter = keySetter;
		}

		protected async Task<IEnumerable<T>> FetchCollection(string resourceUrl, Action<T, string> uniqueKeySetter)
		{
			var empty = new List<T>();

			var json = await GetContent(resourceUrl).ConfigureAwait(false);

			if (string.IsNullOrEmpty(json)) {
				return empty;
			}
			// JSON.Net deserialization
			var parsed = JsonConvert.DeserializeObject<Dictionary<string, T>>(json);

			if (parsed == null) {
				return empty;
			}

			foreach (var result in parsed) {
				uniqueKeySetter(result.Value, result.Key);
			}

			return parsed.Values;
		}
	}
}