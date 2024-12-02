using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tankeschön
{
    public class ApiService:herreber
    {
        
        private readonly string geoApiKey = "d200da1ed61e4a7ba9c558ae6e592ae1";
        private readonly string tankerkoenigApiKey = "75b692ad-15c5-3243-0c51-266f96bf1b56";

        public async Task<(double latitude, double longitude)> GetCoordinatesAsync(string postleitzahl)
        {
            string geoUrl = $"https://api.opencagedata.com/geocode/v1/json?q={postleitzahl}&key={geoApiKey}";

            using (HttpClient client = new HttpClient())
            {
                string geoResponse = await client.GetStringAsync(geoUrl);
                JObject geoJson = JObject.Parse(geoResponse);

                double latitude = (double)geoJson["results"][0]["geometry"]["lat"];
                double longitude = (double)geoJson["results"][0]["geometry"]["lng"];
                return (latitude, longitude);
            }
        }

        public async Task<List<Tankstelle>> GetTankstellenAsync(double latitude, double longitude, double radius)
        {
            
            string tankerkoenigUrl = $"https://creativecommons.tankerkoenig.de/json/list.php?lat={latitude}&lng={longitude}&rad={radius}&type=all&apikey={tankerkoenigApiKey}";

            using (HttpClient client = new HttpClient())
            {
                 
                string tankerResponse = await client.GetStringAsync(tankerkoenigUrl);
                JObject tankerJson = JObject.Parse(tankerResponse);

                if (tankerJson["ok"].ToObject<bool>())
                {
                    var stations = tankerJson["stations"].Select(station => new Tankstelle
                    {
                        Name = station["name"].ToString(),
                        Address = station["street"].ToString(),
                        Latitude = (double)station["lat"],
                        Longitude = (double)station["lng"],
                        Diesel = station["diesel"]?.Type == JTokenType.Null ? 99.99 : (double)station["diesel"],
                        E5 = station["e5"]?.Type == JTokenType.Null ? 99.99 : (double)station["e5"],
                        E10 = station["e10"]?.Type == JTokenType.Null ? 99.99 : (double)station["e10"]
                    }).ToList();



                    if (SharedData.SelectedComboBoxValue == "Diesel")
                    {
                        var stations1 = (stations.OrderBy(station => station.Diesel).ToList());
                        return stations1;
                    }
                    else if(SharedData.SelectedComboBoxValue == "E5")
                    {
                        var stations1 = (stations.OrderBy(station => station.E5).ToList());
                        return stations1;
                        
                    }
                    else
                    {
                        var stations1 = (stations.OrderBy(station => station.E10).ToList());
                        return stations1;
                    }
                    


                    
                }

                return new List<Tankstelle>().ToList();
                
            }
        }
    }
}
