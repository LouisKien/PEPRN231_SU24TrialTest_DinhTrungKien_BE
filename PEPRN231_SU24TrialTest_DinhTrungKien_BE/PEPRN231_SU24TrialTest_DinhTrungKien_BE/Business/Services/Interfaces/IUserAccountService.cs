using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.ModelViews.UserAccount;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Models;

namespace PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.Services.Interfaces
{
    public interface IUserAccountService
    {
        Task<UserAccount?> AuthenticateUser(LoginDtoRequest request);

        Task<string> GenerateAccessToken(UserAccount user);
    }
}
