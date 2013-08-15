namespace Chat.Notifiers
{
    public class PubNubNotifier
    {
        private const string PublishKey = "pub-c-01a2bb9b-25f7-4385-8cd3-7c10036a6b06";
        private const string SubscribeKey = "sub-c-484fa9d0-057c-11e3-991c-02ee2ddab7fe ";
        private const string SecretKey = "sec-c-NjljMTQ3OGItYWFjZC00MDVlLTgxMTEtNGRmMzFhMjE5YWJi";
        private const bool Ssl = true;

        public static PubNubConfig Config 
        {
            get
            {
                return pubnubConfig;
            }
        }

        private static PubNubConfig pubnubConfig = new PubNubConfig(PublishKey, SubscribeKey, SecretKey, Ssl);

		private static PubNubAPI pubnub = new PubNubAPI(
            pubnubConfig.PublishKey,               // PUBLISH_KEY
            pubnubConfig.SubscribeKey,               // SUBSCRIBE_KEY
            pubnubConfig.SecretKey,   // SECRET_KEY
            pubnubConfig.Ssl             // SSL_ON?
		);

		// Publish a sample message to Pubnub
        public static void PublishMessage(string message)
        {
            pubnub.Publish(pubnubConfig.Channel, message);
        }	
    }
}
