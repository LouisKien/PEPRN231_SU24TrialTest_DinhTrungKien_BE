using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Models;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Repository.GenericRepository;

namespace PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private WatercolorsPainting2024DBContext _context = new WatercolorsPainting2024DBContext();
        private IGenericRepository<Style> _styleRepository;
        private IGenericRepository<UserAccount> _userAccountRepository;
        private IGenericRepository<WatercolorsPainting> _watercolorsPaintingRepository;

        public UnitOfWork(WatercolorsPainting2024DBContext context)
        {
            _context = context;
        }

        public IGenericRepository<Style> StyleRepository => _styleRepository ??= new GenericRepository<Style>(_context);

        public IGenericRepository<UserAccount> UserAccountRepository => _userAccountRepository ??= new GenericRepository<UserAccount>(_context);

        public IGenericRepository<WatercolorsPainting> WatercolorsPaintingRepository => _watercolorsPaintingRepository ??= new GenericRepository<WatercolorsPainting>(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
