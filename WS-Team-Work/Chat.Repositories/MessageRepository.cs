using Chat.Models;
using Chat.Repositories;
using System;
using System.Data.Entity;
using System.Linq;

namespace Chat.Repository
{
    public class MessageRepository : IRepository<Message>
    {
        private DbContext messageContext;
        private DbSet<Message> messageSet;

        public MessageRepository(DbContext context)
        {
            this.messageContext = context;
            this.messageSet = messageContext.Set<Message>();
        }

        public Message Add(Message message)
        {
            var userFrom = messageContext.Set<User>().FirstOrDefault(u => u.SessionKey == message.FromUser.SessionKey);
            var userTo = messageContext.Set<User>().FirstOrDefault(u => u.Id == message.ToUser.Id);

            message.FromUser = userFrom;
            message.ToUser = userTo;

            messageSet.Add(message);
            messageContext.SaveChanges();

            return message;
        }

        public Message Update(int id, Message entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Message Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Message> All()
        {
            var messages = this.messageSet.Include("Users").AsQueryable();

            return messages;
        }

        public Message AddFileMessage(string sessionKey, int id, string url)
        {
            var fromUser = messageContext.Set<User>().Where(u => u.SessionKey == sessionKey).FirstOrDefault();
            var toUser = messageContext.Set<User>().Where(u => u.Id == id).FirstOrDefault();

            if (fromUser == null || toUser == null)
            {
                throw new ServerErrorException("Username or Password Invalid", "username/password");
            }

            Message msg = new Message()
            {
                FromUser = fromUser,
                ToUser = toUser,
                FileUrl = url
            };

            this.messageSet.Add(msg);
            this.messageContext.SaveChanges();

            return msg;
        }
    }
}
