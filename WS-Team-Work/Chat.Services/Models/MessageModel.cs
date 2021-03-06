﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Services.Models
{
    public class MessageModel
    {
        public string Content { get; set; }

        public int Id { get; set; }

        public int FromUserId { get; set; }

        public string FromUserNickname { get; set; }

        public string FileUrl { get; set; }
    }
}