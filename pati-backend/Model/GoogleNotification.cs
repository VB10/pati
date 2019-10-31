
namespace dotnetcore.Model {

    public class GoogleNotification {
        public class Notification {
            // Add your custom properties as needed
            public string title { get; set; }
            public string text { get; set; }
        }

        public string[] registration_ids { get; set; }
        public string priority { get; set; } = "high";
        public Notification notification { get; set; }
        public object data { get; set; }
    }
}