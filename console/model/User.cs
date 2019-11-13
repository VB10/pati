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

		public static User initUser()
		{
			User user = new User();
			user.MQTTuserName = "sem5VK";
			user.MQTTpassword = "EOUH2#bW";
			user.CSEid = "/sem-cux_bac8066f-dfd1-49cd-bee4-147e5e0dd267";
			user.clientid = "CAE20ebb267-ab8c-402f-a859-65a55152c8fa";
			user.MQTTport = 1883;
			user.timeout = 30;
			user.MQTTpointOfAcsesss = "mqtt.kocdigital.com";
			return user;
		}
	}
}