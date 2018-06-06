﻿using System.Collections.Generic;
using Accounts.API.Services.Web;
using Channels.API.Models;
using Channels.API.Services;
using Channels.Data.Interfaces;
using Lacqr.Interfaces;
using Lacqr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lacqr.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Channels")]
    public class ChannelsController : Controller
    {

        private AccountsManagerWeb _am;
        private ChannelsManager _cm;

        public ChannelsController(AccountsManagerWeb am, ChannelsManager cm)
        {
            _am = am;
            _cm = cm;
        }

        // GET: api/Channels
        [HttpGet]
        public IEnumerable<IChannel> Get()
        {
            var userId = _am.Authenticate(HttpContext).Id;
            return _cm.GetSubscribedChannels(userId);
        }

        [HttpGet("{id}")]
        public IChannelDetails GetChannel(string id)
        {
            var userId = _am.Authenticate(HttpContext).Id;

            var channel = _cm.GetChannel(new Subscriber(id, userId));
            var subscribers = _am.GetSubscribers(channel.Subscribers);

            return new ChannelDetail(channel, subscribers);
        }

        [HttpPost]
        public IChannel CreateChannel([FromBody]NewChannel channel)
        {
            channel.OwnerId = _am.Authenticate(HttpContext).Id;
            return _cm.CreateChannel(channel);
        }

        [HttpPost("{id}/subscribe")]
        public string SubscribeChannel(string id)
        {
            var sub = new Subscriber(id, _am.Authenticate(HttpContext).Id);
            _cm.SubscribeToChannel(sub);
            return "Success";
        }

        [HttpDelete("{id}/subscribe")]
        public string UnsubscribeChannel(string id)
        {
            var sub = new Subscriber(id, _am.Authenticate(HttpContext).Id);
            _cm.UnsubscribeFromChannel(sub);
            return "Success";
        }


    }
}