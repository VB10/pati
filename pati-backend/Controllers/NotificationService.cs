using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using dotnetcore.Model;

namespace pati_backend.Controllers
{
	public class NotificationService
	{
		const String firebaseCloudUrl = "https://fcm.googleapis.com/fcm/send";
		public const String firebaseUrl = "https://reacttest-d7f4d.firebaseio.com/pets/0.json";

		const String serverKey = "AAAAiyr9Ngg:APA91bExV7dZ2HoNXPOfw6-cvp-Rn__iV66eyVfuDtmeUkKMnUTHL4DropyLSRv_PVsl60QQc7LIdsaTpnLgUMzEXhILfZPh0IEUZG0xbexLw5l4_l-L4f3auxHfSveD9kCI5cvIoBby";

		public async Task postNotifiactionAsync(String title, String body, Object data, String[] deviceTokens)
		{

			var messageInformation = new GoogleNotification();
			messageInformation.notification = new NotificationBody();
			messageInformation.notification.text = body;
			messageInformation.notification.title = title;
			messageInformation.data = data;
			messageInformation.registration_ids = deviceTokens;
			messageInformation.priority = "high";

			var request = new HttpRequestMessage(HttpMethod.Post, firebaseCloudUrl);
			string jsonMessage = Newtonsoft.Json.JsonConvert.SerializeObject(messageInformation);
			request.Headers.TryAddWithoutValidation("Authorization", "key = " + serverKey);
			request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
			HttpResponseMessage result;
			using (var client = new HttpClient())
			{
				result = await client.SendAsync(request);
				System.Console.WriteLine(result.IsSuccessStatusCode);
				if (result.IsSuccessStatusCode)
				{
				    
				}else {
					var resultBody = result.Content;
				}
			}
		}
	}
}