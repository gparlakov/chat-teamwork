using Chat.Models;
using Chat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chat.Services.Controllers
{
    public class ValuesController : ApiController
    {
        private MessageRepository messageRepository;


        public ValuesController()
        {
            this.messageRepository = new MessageRepository(new Chat.Data.ChatContext());
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]Message newMessage)
        {
            this.messageRepository.Add(newMessage);            
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}