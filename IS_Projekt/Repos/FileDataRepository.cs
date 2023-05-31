using IS_Projekt.Database;
using IS_Projekt.Extensions;
using IS_Projekt.Models;
using Microsoft.EntityFrameworkCore;

namespace IS_Projekt.Repos
{
    public class FileDataRepository : IFileDataRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FileDataRepository> _logger;

        private IEnumerable<T> ReplaceData<T>(IEnumerable<T> parsedData, DbSet<T> dbSet) where T : DataModel
        {
            var transaction = _context.Database.BeginTransaction();

            dbSet.RemoveRange(dbSet); //preferably we should just filter parsed data to only include new data
            _context.SaveChanges();

            dbSet.AddRange(parsedData);
            var insertedAmount = _context.SaveChanges();

            _logger.LogInformation($"Inserted {insertedAmount} rows into database");
            transaction.Commit();
            return parsedData;
        }

        public FileDataRepository(ApplicationDbContext context, ILogger<FileDataRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> ImportData<T>(IEnumerable<T> parsedData) where T : DataModel
        {
            return ReplaceData(parsedData, _context.Set<T>());
        }

        public async Task<IEnumerable<T>> ExportData<T>() where T : DataModel
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<CountryModel>> GetCountries()
        { 
            return await _context.Countries.ToListAsync();
        }
        public async Task<IEnumerable<YearModel>> GetYears()
        {
            return await _context.Years.ToListAsync();
        }
    }
}
