namespace console.model
{
	public class User
	{
		public string MQTTuserName { get; set; }
		public string MQTTpassword { get; set; }
		public string CSEid { get; set; }
		public string clientid { get; set; }
		public string MQTTpointOfAcsesss { get; set; }
		public int MQTTport { get; set; }
		public int timeout { get; set; }
	}
}