using Insig.Common.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Insig.Infrastructure.Hubs
{
    public class ColorHub: Hub //Hub<IColorHub>
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
       // [Authorize(Policies.Consumer)]
        public async Task TriggerGroup(string groupName)
        {
            await Clients.Group(groupName).SendAsync("TriggerColor", groupName);
            //await Clients.Group(groupName).TriggerColor(groupName);
        }
        public async Task ColorChange(string color)
        {
            await Clients.All.SendAsync("ColorChange", color);
        }
    }
    // we can also use strogly type interface to replace hardcoded "TriggerColor"
    public interface IColorHub
    {
        Task TriggerColor(string groupName);
    }
}
