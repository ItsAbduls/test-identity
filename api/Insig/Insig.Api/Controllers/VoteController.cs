using Insig.ApplicationServices.Boundaries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Insig.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteManager _voteManager;

        public VoteController(IVoteManager voteManager)
        {
            this._voteManager = voteManager;
        }
        [HttpPost]
        [Route("pie")]
        public async Task<IActionResult> VotePie()
        {
            // save vote
            await _voteManager.CastVote("Pie");
            return Ok();
        }
        [HttpPost]
        [Route("becon")]
        public async Task<IActionResult> VoteBecon()
        {
            // save vote
            await _voteManager.CastVote("Becon");
            return Ok();
        }

    }
}
