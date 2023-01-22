using Microsoft.AspNetCore.Http;
using System.Linq;

namespace FreeCourse.Shared.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string UserId => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub").Value; //NameIdentifier

    }
}
