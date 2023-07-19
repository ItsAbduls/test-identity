using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insig.ApplicationServices.Boundaries
{
    public interface IVoteManager
    {
        Task CastVote(string voteFor);
        Dictionary<string, int> GetCurrentVotes();
    }
}
