using FreeCourse.Services.PhotoStock.Services;
using FreeCourse.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotosController : BaseController
    {
        private readonly IFileManager _fileManager;

        public PhotosController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpPost]
        public async Task<IActionResult> Save(IFormFile photo, CancellationToken cancellationToken)
        {
            var result = await _fileManager.SaveAsync(photo, cancellationToken);
            return Result(result);
        }

        [HttpDelete]
        public IActionResult Delete(string photoUrl)
        {
            var result = _fileManager.Delete(photoUrl);
            return Result(result);
        }
    }
}
