using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlTypes;


namespace Tankeschön
{
    public class ApiService
    {
        private readonly string geoApiKey = "d200da1ed61e4a7ba9c558ae6e592ae1";
        private readonly string tankerkoenigApiKey = "75b692ad-15c5-3243-0c51-266f96bf1b56";


        // Methode zur Abfrage von Koordinaten basierend auf der Postleitzahl

        public async Task<(double latitude, double longitude)> GetCoordinatesAsync(string postleitzahl)
        {
            string geoUrl = $"https://api.opencagedata.com/geocode/v1/json?q={postleitzahl}&key={geoApiKey}";

            using (HttpClient client = new HttpClient())
            {
                string geoResponse = await client.GetStringAsync(geoUrl);
                JObject geoJson = JObject.Parse(geoResponse);


                if (geoJson["results"] == null || !geoJson["results"].Any())
                    throw new Exception("Keine gültigen Geodaten erhalten.");


                double latitude = (double)geoJson["results"][0]["geometry"]["lat"];
                double longitude = (double)geoJson["results"][0]["geometry"]["lng"];
                return (latitude, longitude);
            }
        }


        // Methode zur Abfrage der Tankstellen

        public async Task<List<Tankstelle>> GetTankstellenAsync(double latitude, double longitude, double radius)
        {
            string tankerkoenigUrl = $"https://creativecommons.tankerkoenig.de/json/list.php?lat={latitude}&lng={longitude}&rad={radius}&type=all&apikey={tankerkoenigApiKey}";

            using (HttpClient client = new HttpClient())
            {
                string tankerResponse = await client.GetStringAsync(tankerkoenigUrl);


                // JSON speichern und in CSV umwandeln
                await SaveJsonAndConvertToCsvAsync(tankerResponse);


                JObject tankerJson = JObject.Parse(tankerResponse);

                if (tankerJson["ok"].ToObject<bool>())
                {
                    var stations = tankerJson["stations"].Select(station => new Tankstelle
                    {

                        Name = station["name"]?.ToString() ?? "Unbekannt",
                        Address = station["street"]?.ToString() ?? "Keine Adresse",
                        Latitude = (double?)station["lat"] ?? 0,
                        Longitude = (double?)station["lng"] ?? 0,

                        Diesel = station["diesel"]?.Type == JTokenType.Null ? 0.0 : (double)station["diesel"],
                        E5 = station["e5"]?.Type == JTokenType.Null ? 0.0 : (double)station["e5"],
                        E10 = station["e10"]?.Type == JTokenType.Null ? 0.0 : (double)station["e10"]
                    }).ToList();

                    return stations;
                }

                return new List<Tankstelle>();
            }
        }


        // Methode zum Speichern der JSON-Antwort und Erstellen einer CSV-Datei
        private async Task SaveJsonAndConvertToCsvAsync(string jsonResponse)
        {
            // Erstelle einen Zeitstempel für den Dateinamen
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            // Bestimme den Pfad des aktuellen Verzeichnisses der Anwendung
            string baseFolder = AppDomain.CurrentDomain.BaseDirectory;

            // Füge den Ordner "DatenTankstellen" hinzu
            string dataFolder = Path.Combine(baseFolder, "DatenTankstellen");

            // Überprüfe, ob der Ordner existiert, und erstelle ihn, wenn nicht
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            // Bestimme den vollständigen Dateipfad für die JSON-Datei
            string jsonFilePath = Path.Combine(dataFolder, $"tankerkönig_response_{timestamp}.json");

            // Speichern der JSON-Datei im angegebenen Ordner mit StreamWriter
            using (var writer = new StreamWriter(jsonFilePath, false, Encoding.UTF8))
            {
                await writer.WriteAsync(jsonResponse);
            }

            // JSON zu CSV umwandeln und im selben Ordner speichern
            ConvertJsonToCsv(jsonResponse, dataFolder, timestamp);
        }

        // Methode zum Umwandeln der JSON-Daten in eine CSV-Datei
        private void ConvertJsonToCsv(string jsonResponse, string specialFolder, string timestamp)
        {
            // JSON in ein JObject parsen
            JObject jsonObject;
            try
            {
                jsonObject = JObject.Parse(jsonResponse);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Parsen der JSON-Daten: {ex.Message}");
                return;
            }

            // Prüfen, ob die Datei den Schlüssel "stations" enthält
            if (jsonObject["stations"] == null)
            {
                MessageBox.Show("Keine 'stations'-Daten in der JSON-Datei gefunden.");
                return;
            }

            // Stationen aus der JSON extrahieren
            var stations = jsonObject["stations"];

            // Bestimme den vollständigen Dateipfad für die CSV-Datei
            string csvFilePath = Path.Combine(specialFolder, $"tankerkönig_data{timestamp}.csv");

            // CSV-Datei erstellen und mit den Daten füllen
            using (var writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                // Kopfzeile der CSV-Datei
                writer.WriteLine("Name,Address,Latitude,Longitude,Diesel,E5,E10");

                // Datenzeilen für jede Tankstelle
                foreach (var station in stations)
                {
                    string name = station["name"]?.ToString() ?? "Unbekannt";
                    string address = station["street"]?.ToString() ?? "Keine Adresse";
                    string latitude = station["lat"]?.ToString() ?? "0";
                    string longitude = station["lng"]?.ToString() ?? "0";
                    string diesel = station["diesel"]?.ToString() ?? "0";
                    string e5 = station["e5"]?.ToString() ?? "0";
                    string e10 = station["e10"]?.ToString() ?? "0";

                    // Daten als CSV-Zeile schreiben
                    writer.WriteLine($"{name},{address},{latitude},{longitude},{diesel},{e5},{e10}");
                }
            }

            // Erfolgsnachricht anzeigen
            MessageBox.Show($"JSON- und CSV-Dateien wurden erfolgreich gespeichert im Ordner: {specialFolder}");
        }
    }
}
