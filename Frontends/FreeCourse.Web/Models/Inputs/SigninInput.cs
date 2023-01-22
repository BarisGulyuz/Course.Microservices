using System.ComponentModel;

namespace FreeCourse.Web.Models.Inputs
{
    public class SigninInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}
