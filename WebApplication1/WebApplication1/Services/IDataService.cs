using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Services
{
    public interface IDataService
    {
        Task<List<Data>> GetData();
        Task<Matching> GetMathing();
        Task<Data> UpdateData(int UnitId, string Latitude, string Longitude);
        Task<string> GetUrl();
    }
}
