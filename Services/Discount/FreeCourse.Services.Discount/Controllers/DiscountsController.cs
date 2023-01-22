using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : BaseController
    {
        private readonly IDiscountService _discountService;
        private readonly IIdentityService _identityService;


        public DiscountsController(IDiscountService discountService, IIdentityService identityService)
        {
            _discountService = discountService;
            _identityService = identityService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _discountService.GetAllAsync();
            return Result(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var response = await _discountService.GetByIdAsync(id);
            return Result(response);
        }

        [HttpGet("getByCode/{code}")]
        public async Task<IActionResult> GetAll(string code)
        {
            var response = await _discountService.GetByCodeAndUserIdAsync(code, _identityService.UserId);
            return Result(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Models.Discount discount)
        {
            var response = await _discountService.AddAsync(discount);
            return Result(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Models.Discount discount)
        {
            var response = await _discountService.UpdateAsync(discount);
            return Result(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _discountService.DeleteAsync(id);
            return Result(response);
        }
    }
}
