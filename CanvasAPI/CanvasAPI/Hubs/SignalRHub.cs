using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanvasAPI.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace CanvasAPI.Hubs
{
    public class SignalRHub : Hub
    {
        public SignalRHub(AppDatabase context, IConfiguration config)
        {

        }

        public static class CurrentSession
        {
            public static HashSet<string> ConnectionIds = new HashSet<string>();
        }

        public override async Task OnConnectedAsync()
        {
            string clientIp = Context.GetHttpContext().Connection.RemoteIpAddress.ToString();
            await Clients.Others.SendAsync("NewUserConnected", clientIp);

            CurrentSession.ConnectionIds.Add(Context.ConnectionId);

            //if (CurrentSession.)

            await base.OnConnectedAsync();
        }

    }
}
