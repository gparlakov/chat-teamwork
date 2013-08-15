using AttributeRouting.Web.Http;
using Chat.Models;
using Chat.Notifiers;
using Chat.Repository;
using Chat.Services.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chat.Services.Controllers
{
    public class MessagesController : ApiController
    {
        private Repository.IRepository<Chat.Models.Message> messageRepository;

        public MessagesController(Repository.IRepository<Chat.Models.Message> messageRepository)
        {
            // TODO: Complete member initialization
            this.messageRepository = messageRepository as MessageRepository;
        }
        
        // GET api/messages
        [GET("api/messages/{sessionKey}")]
        public IEnumerable<MessageModel> Get([FromUri]string sessionKey)
        {
            var allMessages = this.messageRepository.All()
                .Where(m => m.ToUser.SessionKey == sessionKey && m.FromUser.SessionKey != null)
                .Select(m => new MessageModel
                {
                    Id = m.Id,
                    Content = m.Content,
                    FromUserId = m.FromUser.Id
                }).ToList();

            return allMessages;
        }

        // GET api/messages/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/messages
        public void Post([FromBody]MessagePostModel message)
        {
            var messageEntity = new Message
            {
                Content = message.Content,
                FromUser = new User { SessionKey = message.FromUser },
                ToUser = new User { Id = int.Parse(message.ToUser) },
            };

            this.messageRepository.Add(messageEntity);

            var notification = new NotificationModel 
            {
                ToUser = messageEntity.FromUser.SessionKey,
                FromUser = messageEntity.ToUser.Nickname
            };

            PubNubNotifier.PublishMessage(JsonConvert.SerializeObject(notification));
        }

        //// PUT api/messages/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/messages/5
        //public void Delete(int id)
        //{
        //}
    }
}
