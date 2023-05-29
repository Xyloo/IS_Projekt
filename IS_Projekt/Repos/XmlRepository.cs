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

        public async Task<IEnumerable<InternetUse>> ImportInternetUseDataFromXmlFile(IEnumerable<InternetUse> parsedXml)
        {
            _context.InternetUseData.AddRange(parsedXml);
            var insertedAmount = await _context.SaveChangesAsync();
            _logger.LogInformation($"Inserted {insertedAmount} rows into database");
            return parsedXml;
        }
    }
}
