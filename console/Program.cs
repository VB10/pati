using System;
using System.Timers;
using System.Net.Http;

using Platform360.Devices.SDK.Client;
using Platform360.Devices.SDK.Client.DeviceManagementService.Internal;
using Platform360.Devices.SDK.Client.EnrollmentService;
using Platform360.Devices.SDK.Client.Options;
using Platform360.Devices.SDK.Communicator;
using Platform360.Devices.SDK.OneM2M.Resources.MgmtResources;

using System.Threading.Tasks;
using console.model;

namespace console
{
	class Program
	{

		private static Timer timer;
		private static User user;
		async static Task Main(string[] args)
		{
			user = new User();
			user.MQTTuserName = "sem5VK";
			user.MQTTpassword = "EOUH2#bW";
			user.CSEid = "/sem-cux_bac8066f-dfd1-49cd-bee4-147e5e0dd267";
			user.clientid = "CAE11ed5b95-5fe3-4f16-921f-ad8a4b837ac9";

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
				.WithMqttOptions("pati", 4040, 1, user.MQTTuserName, user.MQTTpassword).Build();

			IEnrollmentServiceFactory _enrollmentServiceFactory = new EnrollmentServiceFactory();
			IDeviceManagementServiceFactory _deviceManagementServiceFactory = new DeviceManagementServiceFactory();

			IDeviceServiceFactory _deviceServiceFactory = new DeviceServiceFactory();

			var enrollmentService = _enrollmentServiceFactory.CreateEnrollmentService(deviceMqttClientOptions);
			var deviceManagementService = _deviceManagementServiceFactory.CreateDeviceManagementService(deviceMqttClientOptions);
			var deviceService = _deviceServiceFactory.CreateDeviceService(deviceMqttClientOptions);
			await enrollmentService.EnrollDeviceAsync("TestDevice");
			await deviceService.ConnectToPlatformAsync();
			await deviceManagementService.ConnectToPlatformAsync();
			// Sensor Creation
			// Parameters: Sensor Name and isBidirectional
			var sensor = await deviceService.CreateSensorAsync("TestSensor", false);
			var existedSensors = deviceService.GetSensors();

			if (existedSensors.Count > 0)
			{
				// It sends the request to platform to save sensor data.
				await deviceService.PushSensorDataToPlatformAsync(existedSensors[0].Id, data, "pati", "values", DateTime.Now);

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
					// pass360 values
					await platform360InitAsync(data);

				}
			}
		}
	}
}
