using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IAccountsService
    {
        Task Register(RegisterModel model);
        Task<LoginResponseDto> Login(LoginModel model);
        Task Logout(string refreshToken);
        Task<UserTokens> RefreshTokens(UserTokens tokens);

        //Task<ResetToken> GenerageResetToken(string email);
        //Task ResetPassword(ResetPasswordModel model);
    }

    // TODO: create endpoints for resetting user password 
    public class ResetToken
    {
        public string Token { get; set; }
    }
    public class ResetPasswordModel
    {
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}
