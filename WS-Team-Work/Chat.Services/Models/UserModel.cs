using System.Runtime.Serialization;

namespace Chat.Services.Models
{
    [DataContract]
    public class UserModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }
    }
}