using Insig.ApplicationServices.Boundaries;
using Insig.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insig.Infrastructure.Domain
{
    public class VoteManager : IVoteManager
    {
        private readonly IHubContext<VoteHub> hubContext;
        Dictionary<string, int> votes = new Dictionary<string, int>();
        public VoteManager(IHubContext<VoteHub> hubContext)
        {
            this.hubContext = hubContext;
            
        }
        async Task IVoteManager.CastVote(string voteFor)
        {
            if (!votes.ContainsKey(voteFor))
                votes.Add(voteFor, 0);
            else
            {
                votes[voteFor]++;
            }
            // notify
            await hubContext.Clients.All.SendAsync("updateVotes", votes);
        }

        Dictionary<string, int> IVoteManager.GetCurrentVotes()
        {
            return votes;
        }
    }
}
