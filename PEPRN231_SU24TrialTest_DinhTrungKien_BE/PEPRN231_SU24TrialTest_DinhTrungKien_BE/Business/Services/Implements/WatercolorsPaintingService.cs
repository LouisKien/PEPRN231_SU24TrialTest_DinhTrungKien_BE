using AutoMapper;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.Services.Interfaces;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Models;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Repository.UnitOfWork;

namespace PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.Services.Implements
{
    public class WatercolorsPaintingService : IWatercolorsPaintingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WatercolorsPaintingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<WatercolorsPainting>> Get()
        {
            try
            {
                var paintings = (await _unitOfWork.WatercolorsPaintingRepository.GetAsync(includeProperties: "Style")).ToList();
                return paintings;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<WatercolorsPainting?> Get(string key)
        {
            try
            {
                var painting = (await _unitOfWork.WatercolorsPaintingRepository.GetAsync(filter: p => p.PaintingId == key, includeProperties: "Style")).FirstOrDefault();
                if (painting == null)
                {
                    return null;
                } else
                {
                    return painting;
                }    
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Post(WatercolorsPainting watercolorsPainting)
        {
            try
            {
                await _unitOfWork.WatercolorsPaintingRepository.InsertAsync(watercolorsPainting);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
        }

        public async Task Put(WatercolorsPainting watercolorsPainting)
        {
            try
            {
                await _unitOfWork.WatercolorsPaintingRepository.UpdateAsync(watercolorsPainting);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(WatercolorsPainting painting)
        {
            try
            {
                await _unitOfWork.WatercolorsPaintingRepository.DeleteAsync(painting);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex) {
                throw new Exception();
            }
        }

        public async Task<List<WatercolorsPainting>> Search(string author, int? year)
        {
            try
            {
                var paintings = (await _unitOfWork.WatercolorsPaintingRepository.FindAsync(p =>
                (author != null && p.PaintingAuthor.Contains(author)) || (year.HasValue && p.PublishYear == year.Value))).ToList();
                return paintings;
            }
            catch (Exception ex) {
                throw new Exception();
            }
        }
    }
}
