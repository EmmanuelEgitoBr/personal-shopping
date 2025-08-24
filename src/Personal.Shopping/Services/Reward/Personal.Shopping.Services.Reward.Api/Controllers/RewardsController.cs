using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Personal.Shopping.Services.Reward.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardsController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(id);
        }
    }
}
