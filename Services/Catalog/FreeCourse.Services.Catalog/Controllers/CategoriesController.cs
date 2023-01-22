using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            return Result(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return Result(response);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CategoryDto category)
        {
            var response = await _categoryService.InsertAsync(category);
            return Result(response);
        }
    }
}
