using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : BaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();
            return Result(response);
        }

        [HttpGet("getAllByUser")]
        public async Task<IActionResult> GetAllByUser(string userId)
        {
            var response = await _courseService.GetAllByUserId(userId);
            return Result(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            return Result(response);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CourseCreateDto createDto)
        {
            var response = await _courseService.InsertAsync(createDto);
            return Result(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto updateDto)
        {
            var response = await _courseService.UpdateAsync(updateDto);
            return Result(response);
        }

    }
}
