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
            DbSet<DataModel> dbSet = dataType switch
            {
                "InternetUse" => _context.InternetUseData,
                "ECommerce" => _context.ECommerceData,
                _ => throw new ArgumentException($"Unknown data type: {dataType}")
            };

            dbSet.RemoveRange(dbSet); //usuniecie wszystkich danych z bazy
            await _context.SaveChangesAsync();

            dbSet.AddRange(parsedData);
            var insertedAmount = await _context.SaveChangesAsync();
            _logger.LogInformation($"Inserted {insertedAmount} rows into database");
            return parsedData;

        }

        public async Task<IEnumerable<DataModel>> ExportData(DataTypes dataType)
        {
            DbSet<DataModel> dbSet = dataType switch
            {
                DataTypes.InternetUse => _context.InternetUseData,
                DataTypes.ECommerce => _context.ECommerceData,
                _ => throw new ArgumentException($"Unknown data type: {dataType}")
            };

            return await dbSet.ToListAsync();
        }
    }
}
