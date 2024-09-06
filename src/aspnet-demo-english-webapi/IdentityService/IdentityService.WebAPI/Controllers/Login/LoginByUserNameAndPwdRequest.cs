using FluentValidation;

namespace IdentityService.WebAPI.Controllers.Login;
public record LoginByUserNameAndPwdRequest(string UserName, string Password);

/// <summary>
/// 登录请求验证
/// </summary>
public class LoginByUserNameAndPwdRequestValidator : AbstractValidator<LoginByUserNameAndPwdRequest>
{
    public LoginByUserNameAndPwdRequestValidator()
    {
        RuleFor(e => e.UserName).NotNull().NotEmpty();
        RuleFor(e => e.Password).NotNull().NotEmpty();
    }
}