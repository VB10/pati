using System;
using System.Timers;
using System.Net.Http;
using Platform360.Devices.SDK.Client.EnrollmentService;
using Platform360.Devices.SDK.Client.Options;
using Platform360.Devices.SDK.Communicator;

using System.Threading.Tasks;
using console.model;
using Platform360.Devices.SDK.Client;

namespace console
{
	class Program
	{

		private static Timer timer;
		private static User user;
		private static IDeviceService deviceService;
		async static Task Main(string[] args)
		{
			user = new User();
			user.MQTTuserName = "sem5VK";
			user.MQTTpassword = "EOUH2#bW";
			user.CSEid = "/sem-cux_bac8066f-dfd1-49cd-bee4-147e5e0dd267";
			user.clientid = "CAE590914ba-2358-4634-b500-58232fbc4d39";
			user.MQTTport = 1445;
			user.timeout = 30;
			user.MQTTpointOfAcsesss = "mqtt.kocdigital.com";

			await platform360InitAsync("veli");
			startTimer();
			Console.WriteLine("Press the Enter key to exit anytime... ");
			Console.ReadLine();

		}
		public async static Task platform360InitAsync(String data)
		{
			// Mqtt Client Options
			var deviceMqttClientOptions = new DeviceClientOptionsBuilder()
				   .WithClientId(user.clientid)
				.WithCSEId(user.CSEid)
				.WithMqttOptions(user.MQTTpointOfAcsesss, user.MQTTport, user.timeout, user.MQTTuserName, user.MQTTpassword).Build();
			IDeviceServiceFactory _deviceServiceFactory = new DeviceServiceFactory();
			var _deviceService = _deviceServiceFactory.CreateDeviceService(deviceMqttClientOptions);
			await _deviceService.ConnectToPlatformAsync();
			var sensor = _deviceService.GetSensors();

			var existedSensors = deviceService.GetSensors();
			// Notification event when any sensor value change request is sent.

			if (existedSensors.Count > 0)
			{
				// It sends the request to platform to save sensor data.
				await deviceService.PushSensorDataToPlatformAsync(existedSensors[0].Id, data, "on", "off", DateTime.Now);
			}

		}

		public static void startTimer()
		{
			timer = new System.Timers.Timer();
			timer.Interval = 3000;
			timer.Elapsed += OnTimedEvent;
			timer.AutoReset = true;
			timer.Enabled = true;
		}

		private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
		{
			GetData();
		}

		static async void GetData()
		{
			//We will make a GET request to a really cool website...

			string baseUrl = "http://localhost:5002/pet";
			//The 'using' will help to prevent memory leaks.
			//Create a new instance of HttpClient

			using (HttpClient client = new HttpClient())
			using (HttpResponseMessage res = await client.GetAsync(baseUrl))
			using (HttpContent content = res.Content)
			{
				string data = await content.ReadAsStringAsync();
				if (data != null)
				{
					Console.WriteLine(data);
					await platform360InitAsync(data);
				}
			}
		}
	}
}
