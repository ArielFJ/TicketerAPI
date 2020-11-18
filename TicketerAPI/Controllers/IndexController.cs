using Microsoft.AspNetCore.Mvc;

namespace TicketerAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Ok(new {
                apiName = "Ticketer",
                authors = new [] {

                    new {
                        name = "Ariel Fermín",
                        github = "https://github.com/ArielFJ"
                    },
                    new {
                        name = "Rayni Núñez",
                        github = "https://github.com/RayniNE"
                    }
                },
                apiDocumentation = "https://github.com/ArielFJ/TicketerAPI"
            });
        }
    }
}