using GodfatherTips.Data.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GodfatherTips.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Post message) =>
            await Clients.All.SendAsync("receiveMessage", message);
    }
}
