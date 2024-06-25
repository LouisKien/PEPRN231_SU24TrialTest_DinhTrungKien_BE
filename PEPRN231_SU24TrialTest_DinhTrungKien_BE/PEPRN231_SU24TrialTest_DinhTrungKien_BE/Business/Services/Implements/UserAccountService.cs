using Microsoft.IdentityModel.Tokens;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.ModelViews.UserAccount;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.Services.Interfaces;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Models;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Repository.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.Services.Implements
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public UserAccountService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<UserAccount?> AuthenticateUser(LoginDtoRequest request)
        {
            try
            {
                var user = (await _unitOfWork.UserAccountRepository.GetAsync(u => u.UserEmail == request.UserEmail && u.UserPassword == request.UserPassword)).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                else
                {
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> GenerateAccessToken(UserAccount user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var accessClaims = new List<Claim>
                {
                    new Claim("UserAccountId", user.UserAccountId.ToString()),
                    new Claim("Role", user.Role.ToString())
                };
                var accessExpiration = DateTime.Now.AddMinutes(30);
                var accessJwt = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], accessClaims, expires: accessExpiration, signingCredentials: credentials);
                var accessToken = new JwtSecurityTokenHandler().WriteToken(accessJwt);
                return accessToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
