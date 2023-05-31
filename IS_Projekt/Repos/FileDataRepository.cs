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

        public FileDataRepository(ApplicationDbContext context, ILogger<FileDataRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<DataModel>> ImportData(IEnumerable<DataModel> parsedData)
        {
            var dataType = parsedData.First().DataType; //InternetUse lub ECommerce
            if (dataType == "ECommerce")
            {
                var dbSet = _context.ECommerceData;
                dbSet.RemoveRange(dbSet); //usuniecie wszystkich danych z bazy
                await _context.SaveChangesAsync();

                dbSet.AddRange((ECommerce)parsedData);
                var insertedAmount = await _context.SaveChangesAsync();
                _logger.LogInformation($"Inserted {insertedAmount} rows into database");
                return parsedData;
            }
            else if (dataType == "InternetUse")
            {
                var dbSet = _context.InternetUseData;
                dbSet.RemoveRange(dbSet); //usuniecie wszystkich danych z bazy
                await _context.SaveChangesAsync();
                dbSet.AddRange((InternetUse)parsedData);
                var insertedAmount = await _context.SaveChangesAsync();
                _logger.LogInformation($"Inserted {insertedAmount} rows into database");
                return parsedData;
            }
            else
            {
                throw new ArgumentException($"Unknown data type: {dataType}");
            }

        }

        public async Task<IEnumerable<DataModel>> ExportData(DataTypes dataType)
        {
            if (dataType == DataTypes.ECommerce)
            {
                return await _context.ECommerceData.ToListAsync();
            }
            else if (dataType == DataTypes.InternetUse)
            {
                return await _context.InternetUseData.ToListAsync();
            }
            else { throw new ArgumentException($"Unknown data type: {dataType}"); }
        }
    }
}
