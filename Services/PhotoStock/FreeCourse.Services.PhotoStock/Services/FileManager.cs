using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.PhotoStock.Services
{
    public class FileManager : IFileManager
    {
        public async Task<Response<PhotoDto>> SaveAsync(IFormFile photo, CancellationToken cancellationToken)
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos", photo.FileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await photo.CopyToAsync(stream, cancellationToken);
            }

            string pathToRetun = $"photos/{photo.FileName}";
            PhotoDto photoInfo = new PhotoDto(pathToRetun, PhotoStatus.Created);

            return Response<PhotoDto>.Success(photoInfo, (int)HttpStatusCode.OK);

        }

        public Response<PhotoDto> Delete(string photoUrl)
        {
            bool hasError = false;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", photoUrl);

            if (!File.Exists(path)) hasError = true;

            if (!hasError)
                try { File.Delete(path); }
                catch (Exception ex) { hasError = true; } //log error 

            return hasError == true ? Response<PhotoDto>.Fail("Photo Not Deleted", 400) : Response<PhotoDto>.Success(new PhotoDto(photoUrl, PhotoStatus.Deleted), 200, "Deleted");

        }
    }
}
