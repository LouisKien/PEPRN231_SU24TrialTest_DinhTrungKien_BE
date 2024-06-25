using Microsoft.EntityFrameworkCore.Storage;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Models;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Repository.GenericRepository;

namespace PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Style> StyleRepository { get; }
        IGenericRepository<UserAccount> UserAccountRepository { get; }
        IGenericRepository<WatercolorsPainting> WatercolorsPaintingRepository { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task SaveAsync();
    }
}
