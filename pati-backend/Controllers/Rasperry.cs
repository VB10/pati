using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using dotnetcore.Model;

namespace pati_backend.Controllers
{
	public class Rasperry
	{
		const String rasperyUrl = "http://192.168.0.100:8080/";
		public async Task<PetInfo> getAnimalaDataAsync()
		{
			var request = new HttpRequestMessage(HttpMethod.Get, rasperyUrl);
			using (var client = new HttpClient())
			{
				HttpResponseMessage result;
				result = await client.SendAsync(request);
				switch (result.StatusCode)
				{
					case HttpStatusCode.OK:
						var bodyJson = await result.Content.ReadAsStringAsync();
						var model = Newtonsoft.Json.JsonConvert.DeserializeObject<PetInfo>(bodyJson);
						return model;
				}

				return null;
			}

		}
	}
}