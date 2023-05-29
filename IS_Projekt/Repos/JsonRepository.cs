using IS_Projekt.Database;
using IS_Projekt.Models;
using Microsoft.EntityFrameworkCore;

namespace IS_Projekt.Repos
{
    public class JsonRepository : IJsonRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<JsonRepository> _logger;

        public JsonRepository(ApplicationDbContext context, ILogger<JsonRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ECommerce>> ImportDataECommerce(IEnumerable<ECommerce> parsedData)
        {
            _context.ECommerceData.RemoveRange(_context.ECommerceData); //usuniecie wszystkich danych z bazy
            await _context.SaveChangesAsync();

            _context.ECommerceData.AddRange(parsedData);
            var insertedAmount = await _context.SaveChangesAsync();
            _logger.LogInformation($"Inserted {insertedAmount} rows into database");
            return parsedData;
        }

        public async Task<IEnumerable<InternetUse>> ImportDataInternetUse(IEnumerable<InternetUse> parsedData)
        {
            _context.InternetUseData.RemoveRange(_context.InternetUseData); //usuniecie wszystkich danych z bazy
            await _context.SaveChangesAsync();

            _context.InternetUseData.AddRange(parsedData);
            var insertedAmount = await _context.SaveChangesAsync();
            _logger.LogInformation($"Inserted {insertedAmount} rows into database");
            return parsedData;
        }

        public async Task<IEnumerable<ECommerce>> ExportDataECommerce()
        {
            return await _context.ECommerceData.ToListAsync();
        }

        public async Task<IEnumerable<InternetUse>> ExportDataInternetUse()
        {
            return await _context.InternetUseData.ToListAsync();
        }


    }
}
