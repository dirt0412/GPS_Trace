using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model;
using Serilog;

namespace WebApplication1.Repositories
{
    public class DataRepository : IDataRepository
    {
        List<Data> listData = new List<Data>();
        public async Task<string> ReadAllTextAsync(string pathToFile)
        {
            if (File.Exists(pathToFile))
            {
                return await File.ReadAllTextAsync(pathToFile);
            }
            else
                Log.Warning("File {pathToFile} not exist", pathToFile);
            return string.Empty;
        }
        public async Task<List<Data>> GetDataAsync()
        {            
            try
            {
                string pathToFile = "1_Points.json";
                string data = await ReadAllTextAsync(pathToFile);
                listData = JsonConvert.DeserializeObject<List<Data>>(data);
                if (listData.Count > 0)
                {
                    Log.Information(listData.ToString());
                }                    
                else
                {
                    Log.Warning("Wrong data in json file: {pathToFile}", pathToFile);
                    return await Task.FromResult(
                              new List<Data>() { }
                               );
                }
                    
            }
            catch (Exception exception)
            {
                Log.Error("Error! {exception}", exception);
            }
            return listData;
            
        }
        public async Task<Matching> GetMathing()
        {
            string App_ID = Environment.GetEnvironmentVariable("App_ID");
            string App_Code = Environment.GetEnvironmentVariable("App_Code");           

            if (App_ID != null && App_Code != null)
            {
                return await Task.FromResult(
                              new Matching() { 
                                  AppID = App_ID,
                                  AppCode = App_Code,
                                  Routemode = "car",
                                  File = "UEsDBBQAAAAIANmztEQSwaeZzwAAAM8BAAAQAAAAc2FtcGxlLXRyYWNlLmdweIXPTQuCMBwG8HufQnZv%2F605S0k9djEIungdZjpSJ27kPn6%2BRBgYXcYYv2cPzzG2deU8805L1YSIYoLiaHMsWvv9uBlYowOrZYhKY9oAoO973DOsugJ2hFBIz8k1K%2FNabGWjjWiy % 2FJ36ShjVqqITd2lxpmo4XVKgMP6vZaCneKIyYabivzHnr4BhCbb6hoZRpnvMp86L%2BdIapxImRJxiSuh % 2Bj5xq7CWY % 2Bcz1EaypA10qxlfVjvOl8rxVxfzDQrk % 2FFCfLRs7YpOCzA% 2BZd49LoBVBLAQIUABQAAAAIANmztEQSwaeZzwAAAM8BAAAQAAAAAAAAAAEAIAAAAAAAAABzYW1wbGUtdHJhY2UuZ3B4UEsFBgAAAAABAAEAPgAAAP0AAAAAAA%3D%3D"
                              });
            }
            else 
                return await Task.FromResult(
                              new Matching() {}
                               );
        }       

        public async Task<string> GetUrl()
        {
            //https://developer.here.com/documentation/route-match/dev_guide/topics/quick-start-gps-trace-route.html

            string url = string.Empty;
            Matching matching = await GetMathing();
            if(!string.IsNullOrEmpty(matching.AppCode) && !string.IsNullOrEmpty(matching.AppID) && !string.IsNullOrEmpty(matching.Routemode) && listData.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                int i = 0;
                foreach (var data in listData)
                {
                    sb.Append("&waypoint"+i+"=");
                    sb.Append(data.Latitude);
                    sb.Append(",");
                    sb.Append(data.Longitude);
                    i++;
                }
                url = "https://rme.api.here.com/2/calculateroute.json?app_id={" + matching.AppID + "}&app_code={" + matching.AppCode + "}&routeMatch=1&mode=fastest;" + matching.Routemode + ";traffic:disabled"+sb.ToString();
            }
            return await Task.FromResult(url);
        }

        public async Task<Data> UpdateData(int UnitId, string Latitude, string Longitude)
        {
            Data data = new Data();
            double temp = 0;

            double.TryParse(Latitude, out temp);
            data.Latitude = temp;
            temp = 0;
            double.TryParse(Longitude, out temp);
            data.Longitude = temp;

            if (UnitId > 0 && data.Longitude > 0 && data.Latitude > 0)
            {
                listData.Add(data);
            }
            return await Task.FromResult(data);
        }
    }
}
