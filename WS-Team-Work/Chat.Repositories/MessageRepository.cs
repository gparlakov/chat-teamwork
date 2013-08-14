using Chat.Models;
using System;
using System.Data.Entity;
using System.Linq;
using Chat.Repository;

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

        public IQueryable<MessageModel> All()
        {
            var messages = this.messageSet.Select(m => new MessageModel             
            {
                Id = m.Id,
                Content = m.Content
            });

            return messages;
        }
    }
}
