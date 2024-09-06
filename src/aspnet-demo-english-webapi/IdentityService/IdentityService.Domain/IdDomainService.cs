using IdentityService.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Zack.JWT;

namespace IdentityService.Infrastructure
{
    /// <summary>
    /// 用户领域服务
    /// </summary>
    public class IdDomainService
    {
        private readonly IIdRepository repository;
        private readonly ITokenService tokenService;
        private readonly IOptions<JWTOptions> optJWT;

        public IdDomainService(IIdRepository repository,
             ITokenService tokenService, IOptions<JWTOptions> optJWT)
        {
            this.repository = repository;
            this.tokenService = tokenService;
            this.optJWT = optJWT;
        }

        /// <summary>
        /// 检查用户名和密码是否正确
        /// </summary>
        /// <returns></returns>
        private async Task<SignInResult> CheckUserNameAndPwdAsync(string userName, string password)
        {
            var user = await repository.FindByNameAsync(userName);
            if (user == null)
            {
                return SignInResult.Failed;
            }
            //CheckPasswordSignInAsync会对于多次重复失败进行账号禁用
            var result = await repository.CheckForSignInAsync(user, password, true);
            return result;
        }

        /// <summary>
        /// 检查手机号与密码是否正确
        /// </summary>
        /// <returns></returns>
        private async Task<SignInResult> CheckPhoneNumAndPwdAsync(string phoneNum, string password)
        {
            var user = await repository.FindByPhoneNumberAsync(phoneNum);
            if (user == null)
            {
                return SignInResult.Failed;
            }
            var result = await repository.CheckForSignInAsync(user, password, true);
            return result;
        }

        /// <summary>
        /// 使用手机号与密码登录
        /// </summary>
        /// <remarks>
        /// (SignInResult Result, string? Token)  元组的语法
        /// </remarks>
        /// <returns></returns>
        public async Task<(SignInResult Result, string? Token)> LoginByPhoneAndPwdAsync(string phoneNum, string password)
        {
            var checkResult = await CheckPhoneNumAndPwdAsync(phoneNum, password);
            if (checkResult.Succeeded)
            {
                var user = await repository.FindByPhoneNumberAsync(phoneNum);
                string token = await BuildTokenAsync(user);
                return (SignInResult.Success, token);
            }
            else
            {
                return (checkResult, null);
            }
        }

        /// <summary>
        /// 使用用户名与密码登录
        /// </summary>
        /// <returns></returns>
        public async Task<(SignInResult Result, string? Token)> LoginByUserNameAndPwdAsync(string userName, string password)
        {
            var checkResult = await CheckUserNameAndPwdAsync(userName, password);
            if (checkResult.Succeeded)
            {
                var user = await repository.FindByNameAsync(userName);
                string token = await BuildTokenAsync(user);
                return (SignInResult.Success, token);
            }
            else
            {
                return (checkResult, null);
            }
        }

        private async Task<string> BuildTokenAsync(User user)
        {
            var roles = await repository.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return tokenService.BuildToken(claims, optJWT.Value);
        }
    }
}
