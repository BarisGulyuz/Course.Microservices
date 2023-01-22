

using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasketAsync(string userId);
        Task<Response<bool>> AddOrUpdateBasketAsync(BasketDto basket);
        Task<Response<bool>> DeleteAsync(string userId);
    }
}
