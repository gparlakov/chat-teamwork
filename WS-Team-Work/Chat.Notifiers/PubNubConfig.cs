namespace Chat.Notifiers
{
    public class PubNubConfig
    {
        public string PublishKey { get; private set; }
        public string SubscribeKey { get; private set; }
        public string SecretKey { get; private set; }
        public bool Ssl { get; set; }
        public string Channel { get; private set; }

        public PubNubConfig(string pubKey, string subKey, string secretKey, bool ssl)
        {
            this.PublishKey = pubKey;
            this.SecretKey = secretKey;
            this.SubscribeKey = subKey;
            this.Ssl = ssl;
            this.Channel = "chat-michelangelo-channel";
        }
    }
}
