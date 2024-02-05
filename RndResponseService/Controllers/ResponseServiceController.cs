using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RndResponseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseServiceController : ControllerBase
    {
        [Route("{id:int}")]
        [HttpGet]
        public ActionResult Getresponse(int id)
        {
            Random random = new Random();
            var rndintager = random.Next(1, 101);
            if (rndintager>=id)
            {
                Console.WriteLine("--->Response Service failure--- code 500");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Console.WriteLine("--->Response Service Success--- code 200");
            return Ok();
        }
    }
}
