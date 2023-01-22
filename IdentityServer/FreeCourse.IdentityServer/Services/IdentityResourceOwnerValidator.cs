using FreeCourse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Services
{
    public class IdentityResourceOwnerValidator : IResourceOwnerPasswordValidator
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            bool isErrorExist = true;
            var user = await _userManager.FindByEmailAsync(context.UserName);

            if (user != null)
            {
                bool isPasswordTrue = await _userManager.CheckPasswordAsync(user, context.Password);
                if (isPasswordTrue) isErrorExist = false;
            }

            if (isErrorExist)
            {
                context.Result.CustomResponse = new Dictionary<string, object>();
                context.Result.CustomResponse.Add("erros", "Email Or Password Wrong");
                return;
            }


            context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
