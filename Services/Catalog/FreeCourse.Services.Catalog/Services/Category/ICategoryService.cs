using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<string>> InsertAsync(CategoryDto categoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string categoryId);
    }
}