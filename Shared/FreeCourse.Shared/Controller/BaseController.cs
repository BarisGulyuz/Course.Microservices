using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Shared
{
    public class BaseController : ControllerBase
    {
        protected IActionResult Result<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
