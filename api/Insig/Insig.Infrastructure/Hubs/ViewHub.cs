using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Insig.Infrastructure.Hubs
{
    public class ViewHub: Hub
    {
        public static int ViewCount { get; set; } = 0;
        public static int incrementCount { get; set; } = 0;
        public async Task NotifyWatching() { 
            // notify client 
            await Clients.All.SendAsync("viewCountUpdate", ViewCount);
        }
        public string GetFullName(string firstName, string lastName)
        {
            return $"{firstName} {lastName}";
        }
        public async Task IncrementServerView() { 
            incrementCount++;
            // notify all
            await Clients.All.SendAsync("IncrementView", incrementCount);
        }
        public async override Task OnConnectedAsync()
        {
            ViewCount++;
            // notify client 
            await Clients.All.SendAsync("viewCountUpdate", ViewCount);
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            ViewCount--;
            // notify client 
            await Clients.All.SendAsync("viewCountUpdate", ViewCount);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
