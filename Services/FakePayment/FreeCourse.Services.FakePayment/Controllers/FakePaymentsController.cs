using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Pay()
        {
            return Ok();
        }
    }
}
