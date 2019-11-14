using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Repositories
{
    public interface IDataRepository
    {
        Task<string> ReadAllTextAsync(string pathToFile);
        Task<List<Data>> GetDataAsync();
        Task<Matching> GetMathing();
        Task<Data> UpdateData(int UnitId, string Latitude, string Longitude);
        Task<string> GetUrl();
    }
}
