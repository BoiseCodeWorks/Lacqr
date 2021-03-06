﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Messages.API.Models;
using Messages.API.Services.Web;
using Messages.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Accounts.API.Services.Web;

namespace Lacqr.Controllers
{
    // [Produces("application/json")]
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private MessagesManagerWeb _manager;
        private AccountsManagerWeb _auth;

        public MessagesController(MessagesManagerWeb m, AccountsManagerWeb a)
        {
            _manager = m;
            _auth = a;
        }
        // GET: api/Messages
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Messages/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Messages
        [Authorize]
        [HttpPost]
        public IWebMessage Post([FromBody]NewMessage msg)
        {
            msg.UserId = _auth.Authenticate(HttpContext).Id;
            if (ModelState.IsValid)
            {
                //Message object is good:
                IWebMessage message = _manager.Create(msg);
                if (message != null)
                {
                    return message;
                }
            }
            throw new Exception("Invalid Message Data - ARGH");
        }
        
        // PUT: api/Messages/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
