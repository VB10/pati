using System;
using System.Timers;
using System.Net.Http;


namespace console
{
	class Program
	{

		private static Timer timer;
		static void Main(string[] args)
		{

			timer = new System.Timers.Timer();
			timer.Interval = 3000;
			timer.Elapsed += OnTimedEvent;
			timer.AutoReset = true;
			timer.Enabled = true;


			Console.WriteLine("Press the Enter key to exit anytime... ");
			Console.ReadLine();

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
