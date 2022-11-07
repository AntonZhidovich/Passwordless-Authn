namespace Passwordless_Authn.Models
{
    public class LoginModel
    {
        public string Email { get; init; }
        public string Password { get; private set; }

        public LoginModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
