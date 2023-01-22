using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.PhotoStock.Services
{
    public interface IFileManager
    {
        Response<PhotoDto> Delete(string photoUrl);
        Task<Response<PhotoDto>> SaveAsync(IFormFile photo, CancellationToken cancellationToken);
    }
}