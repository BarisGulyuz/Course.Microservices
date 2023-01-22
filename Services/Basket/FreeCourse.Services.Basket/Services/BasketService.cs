using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> AddOrUpdateBasketAsync(BasketDto basket)
        {
            bool status = await _redisService.GetDatabase().StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));
            return status == false ? Response<bool>.Fail("Not Found", 404) : Response<bool>.Success(204);
        }

        public async Task<Response<bool>> DeleteAsync(string userId)
        {
            bool status = await _redisService.GetDatabase().KeyDeleteAsync(userId);
            return status == false ? Response<bool>.Fail("Error occured", 500) : Response<bool>.Success(204);
        }

        public async Task<Response<BasketDto>> GetBasketAsync(string userId)
        {
            RedisValue basket = await _redisService.GetDatabase().StringGetAsync(userId);
            if (string.IsNullOrEmpty(basket)) return Response<BasketDto>.Fail("Not Found", 400);

            BasketDto basketToReturn = JsonSerializer.Deserialize<BasketDto>(basket);
            return Response<BasketDto>.Success(basketToReturn, 200);
        }
    }
}
