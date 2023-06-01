using IS_Projekt.Database;
using IS_Projekt.Extensions;
using IS_Projekt.Models;

namespace IS_Projekt.Repos {
    public class DataRepository: IDataRepository {

        private readonly ApplicationDbContext _context;

        public DataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataModel> GetDataById(int id) {
            //return await _context.
            throw new NotImplementedException();
        }


        public Task<DataModel> AddData(DataModel data) {
            throw new NotImplementedException();
        }

        public Task<DataModel> DeleteData(int id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DataModel>> GetAllData() {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DataModel>> GetDataByCountry(string country) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DataModel>> GetDataByCountryAndYear(string country, int year) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DataModel>> GetDataByIndividualCriteria(string individualCriteria) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DataModel>> GetDataByUnitOfMeasure(string unitOfMeasure) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DataModel>> GetDataByYear(int year) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DataModel>> GetFilteredData(FilterCriteria filter) {
            throw new NotImplementedException();
        }

        public Task<DataModel> UpdateData(DataModel data) {
            throw new NotImplementedException();
        }
    }
}
