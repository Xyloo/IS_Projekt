using IS_Projekt.Database;
using IS_Projekt.Dtos;
using IS_Projekt.Extensions;
using IS_Projekt.Models;
using Microsoft.EntityFrameworkCore;

namespace IS_Projekt.Repos {
    public class DataRepository: IDataRepository {

        private readonly ApplicationDbContext _context;

        public DataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DataModelDto>> GetAllData<T>() where T : DataModel
        {
            return await _context.Set<T>()
                        .Include(x => x.Country)
                        .Include(x => x.Year)
                        .Select(data => new DataModelDto
                        {
                            Id = data.Id,
                            Country = data.Country.CountryName,
                            Year = data.Year.Year,
                            IndividualCriteria = data.IndividualCriteria,
                            UnitOfMeasure = data.UnitOfMeasure,
                            Value = data.Value
                        })
                        .ToListAsync();
        }
        public async Task<DataModelDto?> GetDataById<T>(int id) where T : DataModel{
            return await _context.Set<T>()
                        .Include(x => x.Country)
                        .Include(x => x.Year)
                        .Select(data => new DataModelDto
                        {
                            Id = data.Id,
                            Country = data.Country.CountryName,
                            Year = data.Year.Year,
                            IndividualCriteria = data.IndividualCriteria,
                            UnitOfMeasure = data.UnitOfMeasure,
                            Value = data.Value
                        })
                        .FirstOrDefaultAsync(data => data.Id == id);
        }

        public async Task<T> AddData<T>(DataModelDto dataDto) where T : DataModel, new(){

            if (dataDto.IndividualCriteria == null || dataDto.UnitOfMeasure == null || dataDto.Country == null || dataDto.Year == null || dataDto.Value == null)
                throw new Exception("Data is incomplete. Missing data: " + (dataDto.IndividualCriteria == null ? "IndividualCriteria " : "") + (dataDto.UnitOfMeasure == null ? "UnitOfMeasure " : "") + (dataDto.Country == null ? "Country " : "") + (dataDto.Year == null ? "Year " : "") + (dataDto.Value == null ? "Value " : ""));

            var country = await _context.Countries.FirstOrDefaultAsync(x => x.CountryName == dataDto.Country);
            if (country == null) throw new Exception("Country not found");

            var year = await _context.Years.FirstOrDefaultAsync(y => y.Year == dataDto.Year);

            // If the year doesn't exist, create a new year
            if (year == null)
            {
                year = new YearModel { Year = (int)dataDto.Year };
                _context.Years.Add(year);
                await _context.SaveChangesAsync();
            }

            var id = _context.Set<T>().Max(x => x.Id) + 1;

            var dataModel = new T
            {
                Country = country,
                Year = year,
                IndividualCriteria = dataDto.IndividualCriteria,
                UnitOfMeasure = dataDto.UnitOfMeasure,
                Value = (double)dataDto.Value
            };

            _context.Set<T>().Add(dataModel);
            await _context.SaveChangesAsync();
            dataModel.Id = id;

            return dataModel;

        }

        public async Task<T> DeleteData<T>(int id) where T: DataModel{ 
            
            var data = await _context.Set<T>().FindAsync(id);
            if(data == null)
                throw new Exception("Data not found");
            _context.Set<T>().Remove(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<T> UpdateData<T>(DataModelDto dataDto, int id) where T: DataModel{

            if (dataDto.IndividualCriteria == null || dataDto.UnitOfMeasure == null || dataDto.Country == null || dataDto.Year == null || dataDto.Value == null)
                throw new Exception("Data is incomplete. Missing data: " + (dataDto.IndividualCriteria == null ? "IndividualCriteria " : "") + (dataDto.UnitOfMeasure == null ? "UnitOfMeasure " : "") + (dataDto.Country == null ? "Country " : "") + (dataDto.Year == null ? "Year " : "") + (dataDto.Value == null ? "Value " : ""));

            var data = await _context.Set<T>().FindAsync(id);
            if(data == null) throw new Exception("Data not found");

            var country = await _context.Countries.FirstOrDefaultAsync(x => x.CountryName == dataDto.Country);
            if (country == null) throw new Exception("Country not found");

            var year = await _context.Years.FirstOrDefaultAsync(y => y.Year == dataDto.Year);
            if (year == null) throw new Exception("Year not found");

            data.Country = country;
            data.Year = year;
            data.IndividualCriteria = dataDto.IndividualCriteria;
            data.UnitOfMeasure = dataDto.UnitOfMeasure;
            data.Value = (double)dataDto.Value;

            await _context.SaveChangesAsync();
            return data;

        }

        public async Task<IEnumerable<DataModelDto>> GetFilteredData<T>(FilterCriteria filter) where T: DataModel 
        {
            // Start with all the data
            IQueryable<T> query = _context.Set<T>();

            // If Years list is not null or empty, filter by years
            if (filter.Years != null && filter.Years.Any())
            {
                query = query.Where(data => filter.Years.Contains(data.Year.Year));
            }

            // If Countries list is not null or empty, filter by countries
            if (filter.Countries != null && filter.Countries.Any())
            {
                query = query.Where(data => filter.Countries.Contains(data.Country.CountryName));
            }

            // If Criteria list is not null or empty, filter by criteria
            if (filter.Criteria != null && filter.Criteria.Any())
            {
                query = query.Where(data => filter.Criteria.Contains(data.IndividualCriteria));
            }

            // Execute the query and return the results
            return await query.Include(x => x.Country)
                                  .Include(x => x.Year)
                                  .Select(data => new DataModelDto
                                  {
                                      Id = data.Id,
                                      Country = data.Country.CountryName,
                                      Year = data.Year.Year,
                                      IndividualCriteria = data.IndividualCriteria,
                                      UnitOfMeasure = data.UnitOfMeasure,
                                      Value = data.Value
                                  })
                                  .ToListAsync();
        }

        public async Task<DataInfoDto> GetDataInfo()
        {
           var countries = await _context.Countries.Select(x => x.CountryName).Distinct().ToListAsync();
           var years = await _context.Years.Select(x => x.Year).Distinct().ToListAsync();

           var IC_ECommerce = await _context.ECommerceData
                                             .Select(x => x.IndividualCriteria)
                                             .Distinct()
                                             .ToListAsync();
           var IC_InternetUse = await _context.InternetUseData
                                             .Select(x => x.IndividualCriteria)
                                             .Distinct()
                                             .ToListAsync();
            var dataInfo = new DataInfoDto
            {
                Years = years,
                Countries = countries,
                IC_ECommerce = IC_ECommerce,
                IC_InternetUse = IC_InternetUse
            };

            return dataInfo;
        }
    }
}
