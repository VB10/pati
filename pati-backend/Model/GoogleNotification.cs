
namespace dotnetcore.Model
{

	public class GoogleNotification
	{
		public string[] registration_ids { get; set; }
		public string priority { get; set; } = "high";
		public NotificationBody notification { get; set; }
		public object data { get; set; }
	}
	public class NotificationBody
	{
		// Add your custom properties as needed
		public string title { get; set; }
		public string text { get; set; }
	}
}