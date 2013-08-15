using AttributeRouting.Web.Http;
using Chat.FileExtensions;
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
using System.Web;
using System.Web.Http;

namespace Chat.Services.Controllers
{
    public class MessagesController : ApiController
    {
        private MessageRepository messageRepository;

        public MessagesController(Repository.IRepository<Chat.Models.Message> messageRepository)
        {
            // TODO: Complete member initialization
            this.messageRepository = messageRepository as MessageRepository;
        }
        
        // GET api/messages
        [GET("api/messages/{sessionKey}/idOfSender/{idOfSender}")]
        public IEnumerable<MessageModel> Get([FromUri]string sessionKey, [FromUri]string idOfSender)
        {
            var senderId = int.Parse(idOfSender);

            var allMessages = this.messageRepository.All()
                .Where(m => m.ToUser.SessionKey == sessionKey && m.FromUser.Id == senderId || 
                    m.FromUser.SessionKey == sessionKey && m.ToUser.Id == senderId)
                .Select(m => new MessageModel
                {
                    Id = m.Id,
                    Content = m.Content,
                    FileUrl = m.FileUrl,
                    FromUserId = m.FromUser.Id,
                    FromUserNickname = m.FromUser.Nickname
                }).ToList();

            foreach (var message in allMessages)
            {
                message.Content = message.Content ?? message.FileUrl;
            }

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
                ToUser = messageEntity.ToUser.Nickname,
                FromUser = messageEntity.FromUser.Nickname
            };

            PubNubNotifier.PublishMessage(JsonConvert.SerializeObject(notification));
        }

        [POST("api/messages/{sessionKey}/{id}")]
        public HttpResponseMessage PostFiles(string sessionKey, int id)
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
                string imageUrl = string.Empty;

                var postedFile = httpRequest.Files[0];

                imageUrl = DropboxUploader.DropboxShareFile(postedFile.InputStream, postedFile.FileName);

                var postedFileMessage = this.messageRepository.AddFileMessage(sessionKey, id, imageUrl);

                result = Request.CreateResponse(HttpStatusCode.OK);

                Notifiers.PubNubNotifier.PublishMessage(JsonConvert.SerializeObject(new {
                    FromUser = postedFileMessage.FromUser.Nickname,
                    ToUser = postedFileMessage.ToUser.Nickname                    
                }));
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;
        }
    }
}
