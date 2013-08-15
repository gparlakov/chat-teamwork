using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Chat.Services.Models
{
    [DataContract]
    public class UserModelLogged
    {
        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }
        [DataMember(Name = "sessionKey")]
        public string SessionKey { get; set; }
        [DataMember(Name = "imageUrl")]
        public string ImageUrl { get; set; }
    }
}