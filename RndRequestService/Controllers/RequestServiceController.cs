using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RndRequestService.Policies;

namespace RndRequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestServiceController : ControllerBase
    {
        private readonly ClientPolicy _clientPolicy;
        public RequestServiceController(ClientPolicy clientPolicy)
        {
            _clientPolicy=clientPolicy;
        }
        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
             var client= new HttpClient();
            //var respone = await client.GetAsync("https://localhost:7152/api/ResponseService/1");

            //var respone = await _clientPolicy.ImmediateHttpRetry.ExecuteAsync(()=>
            // client.GetAsync("https://localhost:7152/api/ResponseService/10"));

            var respone = await _clientPolicy.LinearHttpRetry.ExecuteAsync(() =>
           client.GetAsync("https://localhost:7152/api/ResponseService/1"));
            if (respone.IsSuccessStatusCode)
            {
                Console.WriteLine("--->Request Service Success--- code 200");
                return Ok();
            }
            Console.WriteLine("--->Request Service failure--- code 500");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
