using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Services.Models
{
    public class MessagePostModel
    {
        //session key
        public string FromUser { get; set; }

        // id of recipient user
        public string ToUser { get; set; }

        public string Content { get; set; }

        public string FileUrl { get; set; }
    }
}