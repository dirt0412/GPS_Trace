using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return await _dataService.GetData();
        }
        [HttpGet]
        public async Task<Matching> GetMatching()
        {
            return await _dataService.GetMathing();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateData([FromBody]int UnitId, string Latitude, string Longitude)
        {
            if(UnitId == 0 || string.IsNullOrEmpty(Latitude) || string.IsNullOrEmpty(Longitude))
                return BadRequest();
            await _dataService.UpdateData(UnitId, Latitude, Longitude);
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