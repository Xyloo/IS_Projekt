using IS_Projekt.Database;
using IS_Projekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace IS_Projekt.Services
{
    public class SqlService : ISqlService
    {
        private readonly ApplicationDbContext _dbcontext;

        public SqlService(ApplicationDbContext db)
        {
            _dbcontext = db;
        }

        public async Task ExportECommerceDataToFile(string path)
        {
            var data = await _dbcontext.ECommerceData.ToListAsync();
            StringBuilder sb = new StringBuilder();
            //insert into ECommerceData(IndividualCriteria, Country, UnitOfMeasure, Year, Value) values ( 'test', 'test', 'test', -1, -1)
            foreach (var item in data)
            {
                sb.AppendLine($"INSERT INTO ECommerceData(IndividualCriteria, Country, UnitOfMeasure, Year, Value)" +
                $" VALUES({item.IndividualCriteria}, {item.Country}, {item.UnitOfMeasure}, {item.Year}, {item.Value});");
            }
            await File.WriteAllTextAsync(path, sb.ToString());
        }

        public async Task ExportInternetUseDataToFile(string path)
        {
            //insert into InternetUseData(IndividualCriteria, Country, Year, Value) values ( 'test', 'test', -1, -1)
            var data = await _dbcontext.InternetUseData.ToListAsync();
            StringBuilder sb = new StringBuilder();
            foreach (var item in data)
            {
                sb.AppendLine($"INSERT INTO InternetUseData(IndividualCriteria, Country, Year, Value)" +
                $" VALUES({item.IndividualCriteria}, {item.Country}, {item.Year}, {item.Value});");
            }
            await File.WriteAllTextAsync(path, sb.ToString());
        }

        public async Task ImportECommerceDataFromFile(string path)
        {
            var sqlCommands = await File.ReadAllTextAsync(path);
            await _dbcontext.Database.ExecuteSqlRawAsync(sqlCommands);
        }

        public async Task ImportInternetUseDataFromFile(string path)
        {
            var sqlCommands = await File.ReadAllTextAsync(path);
            await _dbcontext.Database.ExecuteSqlRawAsync(sqlCommands);
        }
    }
}
