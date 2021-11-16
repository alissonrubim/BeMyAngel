using BeMyAngel.Api.Presentations.ChatEventController;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeMyAngel.Api.Hubs
{
    public class ChatHub: Hub, IChatHub
    {
        public override Task OnConnectedAsync()
        {
            
            var context = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public void ReceiveMessage(HubEventResponse hubEvent)
        {
            
        }
    }
}
