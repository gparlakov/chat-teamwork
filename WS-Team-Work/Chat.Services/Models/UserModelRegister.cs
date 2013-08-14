using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Chat.Services.Models
{
    public class UserModelRegister : UserModelLogin
    {
        public string Nickname { get; set; }

    }
}