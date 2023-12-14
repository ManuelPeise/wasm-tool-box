using Microsoft.AspNetCore.Mvc;

namespace Service.Shared
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class AApiController: ControllerBase
    {
        public AApiController()
        {
            
        }


    }
}
