using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using dotnetcore.Model;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcore.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PetController : ControllerBase
	{

		HttpClient client = new HttpClient();
		const String firebaseUrl = "https://reacttest-d7f4d.firebaseio.com/pets/0.json";
		const String rasperyUrl = "http://192.168.0.100:8080/";

		[HttpGet]
		public async Task<ActionResult> GetAsync()
		{
              var data = await getAnimalaDataAsync();
              if (data == null)
              {
                  return NotFound();
              }
              else {
                  return Ok(data);
              }
			// var response = await client.GetAsync(firebaseUrl);
			// if (response.IsSuccessStatusCode)
			// {
			// 	var responseStream = await response.Content.ReadAsStreamAsync();
			// 	var petJson = await JsonSerializer.DeserializeAsync<Pet>(responseStream);
            //     await getAnimalaDataAsync();
			// 	return Ok(petJson);
			// }
			return NotFound();
		}

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