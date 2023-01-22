using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services;
using FreeCourse.Shared;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;
        private readonly IIdentityService _identityService;

        public BasketController(IBasketService basketService, IIdentityService identityService)
        {
            _basketService = basketService;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _basketService.GetBasketAsync(_identityService.UserId);
            return Result(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(BasketDto basket)
        {
            basket.UserId = _identityService.UserId;
            var response = await _basketService.AddOrUpdateBasketAsync(basket);
            return Result(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var response = await _basketService.DeleteAsync(_identityService.UserId);
            return Result(response);
        }

    }
}
