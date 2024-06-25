using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Models;

namespace PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.Services.Interfaces
{
    public interface IWatercolorsPaintingService
    {
        Task<List<WatercolorsPainting>> Get();

        Task<WatercolorsPainting?> Get (string key);

        Task Post(WatercolorsPainting watercolorsPainting);

        Task Put(WatercolorsPainting watercolorsPainting);

        Task Delete(WatercolorsPainting painting);

        Task <List<WatercolorsPainting>> Search(string author, int? year);
    }
}
