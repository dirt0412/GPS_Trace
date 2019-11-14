using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class DataService : IDataService
    {
        private IDataRepository dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }
        public async Task<List<Data>> GetData()
        {
            return await dataRepository.GetDataAsync();
        }
        public async Task<Matching> GetMathing()
        {
            return await dataRepository.GetMathing();
        }
        public async Task<Data> UpdateData(int UnitId, string Latitude, string Longitude)
        {
            return await dataRepository.UpdateData(UnitId, Latitude, Longitude);
        }
        public async Task<string> GetUrl()
        {
            return await dataRepository.GetUrl();
        }
    }
}
