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
			user.clientid = "CAE11ed5b95-5fe3-4f16-921f-ad8a4b837ac9";
			user.MQTTport = 1883;
			user.timeout = 120;
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

			IEnrollmentServiceFactory _enrollmentServiceFactory = new EnrollmentServiceFactory();
			IDeviceManagementServiceFactory _deviceManagementServiceFactory = new DeviceManagementServiceFactory();
			IDeviceServiceFactory _deviceServiceFactory = new DeviceServiceFactory();

			var enrollmentService = _enrollmentServiceFactory.CreateEnrollmentService(deviceMqttClientOptions);
			var deviceManagementService = _deviceManagementServiceFactory.CreateDeviceManagementService(deviceMqttClientOptions);
			var deviceService = _deviceServiceFactory.CreateDeviceService(deviceMqttClientOptions);

			await enrollmentService.EnrollDeviceAsync("VB Devices");
			await deviceService.ConnectToPlatformAsync();
			await deviceManagementService.ConnectToPlatformAsync();
			// Sensor Creation
			// Parameters: Sensor Name and isBidirectional
			var sensor = await deviceService.CreateSensorAsync("TestSensor", false);
			System.Console.WriteLine("VB SENSOR : " + sensor);

			deviceService.UseSensorValueChangeRequestHandler(e =>
			{
				System.Console.WriteLine(e.SensorId);
			});

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


				}
			}
		}
	}
}
