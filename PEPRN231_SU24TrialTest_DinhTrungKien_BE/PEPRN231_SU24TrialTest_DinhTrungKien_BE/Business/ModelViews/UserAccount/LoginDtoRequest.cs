using System.ComponentModel.DataAnnotations;

namespace PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.ModelViews.UserAccount
{
    public class LoginDtoRequest
    {
        public string? UserEmail { get; set; }

        public string? UserPassword { get; set; }
    }
}
