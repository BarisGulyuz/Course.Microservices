using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<List<CourseDto>>> GetAllByUserId(string userId);
        Task<Response<CourseDto>> GetByIdAsync(string courseId);
        Task<Response<string>> InsertAsync(CourseCreateDto courseCreateDto);
        Task<Response<string>> UpdateAsync(CourseUpdateDto courseUpdateDto);
    }
}