using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApplication1.Model;
using WebApplication1.Repositories;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{    
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet]
        public async Task<List<Data>> Get()
        {
            List<Data> listData = await _dataService.GetData();
            Log.Information("Get: {listData}", listData);
            return listData;
        }
        [HttpGet]
        public async Task<Matching> GetMatching()
        {
            Matching matching = await _dataService.GetMathing();
            Log.Information("GetMatching: {matching}", matching);
            return matching;
        }
        [HttpPost]
        public async Task<IActionResult> UpdateData([FromBody]int UnitId, string Latitude, string Longitude)
        {
            if(UnitId == 0 || string.IsNullOrEmpty(Latitude) || string.IsNullOrEmpty(Longitude))
            {
                Log.Warning("Error UpdateData: UnitId:{UnitId}, Latitude:{Latitude}, Longitude:{Longitude}", UnitId, Latitude, Longitude);
                return BadRequest();
            }

            await _dataService.UpdateData(UnitId, Latitude, Longitude);
            Log.Information("UpdateData: UnitId:{UnitId}, Latitude:{Latitude}, Longitude:{Longitude}", UnitId, Latitude, Longitude);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> GetUrl()
        {
            string url = await _dataService.GetUrl();
            return Ok();
        }
    }
}