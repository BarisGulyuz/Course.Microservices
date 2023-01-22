using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using FreeCourse.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace FreeCourse.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AppUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("getuser")]
        public async Task<IActionResult> GetUser()
        {
            Claim userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null) return BadRequest();

            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user is null) return BadRequest();

            return Ok(
                new ApplicationUserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    City = user.City
                });
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = register.UserName,
                Email = register.Email,
                City = register.City
            };

            IdentityResult result = await _userManager.CreateAsync(applicationUser, register.Password);
            if (result.Succeeded == false)
            {
                return Result(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), (int)HttpStatusCode.BadRequest));
            }

            return NoContent();
        }
    }
}
