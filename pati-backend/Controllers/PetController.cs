using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using dotnetcore.Model;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcore.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class PetController : ControllerBase {

        HttpClient client = new HttpClient ();
        const String firebaseUrl = "https://reacttest-d7f4d.firebaseio.com/pets/0.json";
        const String firebaseCloudUrl = "https://fcm.googleapis.com/fcm/send";

        [HttpGet]
        public async Task<ActionResult> GetAsync () {
            var response = await client.GetAsync (firebaseUrl);
            if (response.IsSuccessStatusCode) {
                var responseStream = await response.Content.ReadAsStreamAsync ();
                var petJson = await JsonSerializer.DeserializeAsync<Pet> (responseStream);
                return Ok (petJson);
            }

            return NotFound ();
        }

        public async Task postNotifiactionAsync (String title, String body, Object data, String[] deviceTokens) {

            var messageInformation = new GoogleNotification();
            messageInformation.notification.text = body;
            messageInformation.notification.title = title;
            messageInformation.data = data;
            messageInformation.registration_ids = deviceTokens;
            messageInformation.priority = "high";

            var request = new HttpRequestMessage (HttpMethod.Post, firebaseCloudUrl);
            string jsonMessage = Newtonsoft.Json.JsonConvert.SerializeObject (messageInformation);
            request.Headers.TryAddWithoutValidation ("Authorization", "key = " + "ServerKey");
            request.Content = new StringContent (jsonMessage, Encoding.UTF8, "application / json");
            HttpResponseMessage result;
            using (var client = new HttpClient ()) {
                result = await client.SendAsync (request);
            }
        }
    }

}