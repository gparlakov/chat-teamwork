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
    }
}
