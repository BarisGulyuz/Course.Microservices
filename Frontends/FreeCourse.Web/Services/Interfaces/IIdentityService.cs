using FreeCourse.Shared;
using FreeCourse.Web.Models.Inputs;
using IdentityModel.Client;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SigninInput input);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
