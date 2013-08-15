using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Chat.Services.Models
{
    public class NotificationModel
    {
        // sessionKey
        [JsonProperty("toUser")]
        public string ToUser { get; set; }

        // nickname
        [JsonProperty("fromUser")]
        public string FromUser { get; set; }
    }
}