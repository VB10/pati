using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using dotnetcore.Model;

namespace pati_backend.Controllers
{
	public class Rasperry
	{
		const String rasperyUrl = "http://192.168.0.100:8085/";

		const String firebaseUrl = "https://reacttest-d7f4d.firebaseio.com/pets/0.json";

		private static int maxWeight = 1000;
		public static float getFinalWeight(float currentWeight)
		{
			return (100 * currentWeight)/maxWeight;
		}
		public async Task<PetInfo> getAnimalaDataAsync()
		{
			var request = new HttpRequestMessage(HttpMethod.Get, firebaseUrl);
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