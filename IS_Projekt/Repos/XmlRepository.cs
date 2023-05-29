using IS_Projekt.Database;
using IS_Projekt.Models;

namespace IS_Projekt.Repos
{
    public class XmlRepository : IXmlRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<XmlRepository> _logger;

        public XmlRepository(ApplicationDbContext context, ILogger<XmlRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<InternetUse>> ImportDataInternetUse(IEnumerable<InternetUse> parsedData)
        {
            _context.InternetUseData.AddRange(parsedData);
            var insertedAmount = await _context.SaveChangesAsync();
            _logger.LogInformation($"Inserted {insertedAmount} rows into database");
            return parsedData;
        }

        public async Task<IEnumerable<InternetUse>> ExportDataInternetUse()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ECommerce>> ImportDataECommerce(IEnumerable<ECommerce> parsedData)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ECommerce>> ExportDataECommerce()
        {
            throw new NotImplementedException();
        }
    }
}
