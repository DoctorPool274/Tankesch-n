using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Tankeschön
{
    public partial class Tankeschön : Form
    {
        // Variablen für das Ziehen des Fensters
        private bool isDragging = false;
        private Point dragStartPoint;

        public Tankeschön()
        {
            InitializeComponent();
            // Ereignisse für FlowPanel hinzufügen
            FlowPanel.MouseDown += FlowPanel_MouseDown;
            FlowPanel.MouseMove += FlowPanel_MouseMove;
            FlowPanel.MouseUp += FlowPanel_MouseUp;
        }

        private void Tankeschön_Load(object sender, EventArgs e)
        {
            // Initialisierungen beim Laden der Form
            MapControl.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            MapControl.MinZoom = 5;
            MapControl.MaxZoom = 40;
            MapControl.Zoom = 10;
            MapControl.ShowCenter = false;

            // ComboBox mit den Spritsorten füllen
            CbSpritsorte.Items.Add("Diesel");
            CbSpritsorte.Items.Add("E5");
            CbSpritsorte.Items.Add("E10");

            // Standardwert auswählen
            CbSpritsorte.SelectedIndex = 0;
        }

        private async void BtnSuche_Click(object sender, EventArgs e)
        {
            string postleitzahl = TxtOrt1.Text;
            if (string.IsNullOrWhiteSpace(postleitzahl))
            {
                MessageBox.Show("Bitte eine gültige Postleitzahl eingeben.");
                return;
            }

            string geoApiKey = "d200da1ed61e4a7ba9c558ae6e592ae1";
            string tankerkoenigApiKey = "75b692ad-15c5-3243-0c51-266f96bf1b56";

            string geoUrl = $"https://api.opencagedata.com/geocode/v1/json?q={postleitzahl}&key={geoApiKey}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Geokoordinaten abrufen
                    string geoResponse = await client.GetStringAsync(geoUrl);
                    JObject geoJson = JObject.Parse(geoResponse);

                    double latitude = (double)geoJson["results"][0]["geometry"]["lat"];
                    double longitude = (double)geoJson["results"][0]["geometry"]["lng"];

                    // Tankstellen-Daten abrufen
                    string tankerkoenigUrl = $"https://creativecommons.tankerkoenig.de/json/list.php?lat={latitude}&lng={longitude}&rad={NUDRadius.Value}&type=all&apikey={tankerkoenigApiKey}";
                    string tankerResponse = await client.GetStringAsync(tankerkoenigUrl);
                    Console.WriteLine(tankerResponse);  // Debug-Ausgabe der API-Antwort
                    JObject tankerJson = JObject.Parse(tankerResponse);
                    //MessageBox.Show(tankerkoenigUrl);
                    if (tankerJson["ok"].ToObject<bool>())
                    {
                        if (tankerJson["stations"] == null || !tankerJson["stations"].Any())
                        {
                            MessageBox.Show("Keine Tankstelle im Umkreis");
                            return;
                        }
                        else
                        {
                            // JSON zu Liste von Tankstellen umwandeln
                            var stations = tankerJson["stations"].Select(station => new Tankstelle
                            {
                                Name = station["name"].ToString(),
                                Address = station["street"].ToString(),
                                Latitude = (double)station["lat"],
                                Longitude = (double)station["lng"],
                                Diesel = station["diesel"]?.Type == JTokenType.Null ? 0.0 : (double)station["diesel"],
                                E5 = station["e5"]?.Type == JTokenType.Null ? 0.0 : (double)station["e5"],
                                E10 = station["e10"]?.Type == JTokenType.Null ? 0.0 : (double)station["e10"]
                            }).ToList();

                            // DEBUG: Überprüfe die API-Daten
                            foreach (var station in stations)
                            {
                                Console.WriteLine($"Name: {station.Name}, Latitude: {station.Latitude}, Longitude: {station.Longitude}, Diesel: {station.Diesel}, E5: {station.E5}, E10: {station.E10}");
                            }

                            // Tankstellen in DataGridView anzeigen
                            DGVTankstellen.DataSource = null;
                            DGVTankstellen.AutoGenerateColumns = true;
                            DGVTankstellen.DataSource = stations;

                            // Karte aktualisieren
                            UpdateMap(latitude, longitude, stations);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fehler beim Abrufen der Tankstellen-Daten.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler: {ex.Message}");
                }
            }
        }

        private void UpdateMap(double latitude, double longitude, List<Tankstelle> stations)
        {
            MapControl.Position = new GMap.NET.PointLatLng(latitude, longitude);
            MapControl.Overlays.Clear();

            var markers = new GMapOverlay("markers");

            // Holen der ausgewählten Spritsorte
            string selectedFuel = CbSpritsorte.SelectedItem.ToString();

            // Tankstellen nur mit der ausgewählten Spritsorte anzeigen
            foreach (var station in stations)
            {
                double fuelPrice = 0.0;
                string toolTipText = $"{station.Name}\n";

                // Abhängig von der ausgewählten Spritsorte den Preis zuweisen
                switch (selectedFuel)
                {
                    case "Diesel":
                        fuelPrice = station.Diesel;
                        toolTipText += $"Diesel: {fuelPrice:F2} €/L";
                        break;
                    case "E5":
                        fuelPrice = station.E5;
                        toolTipText += $"E5: {fuelPrice:F2} €/L";
                        break;
                    case "E10":
                        fuelPrice = station.E10;
                        toolTipText += $"E10: {fuelPrice:F2} €/L";
                        break;
                }

                // Wenn der Preis > 0 ist, Marker hinzufügen
                if (fuelPrice > 0)
                {
                    var marker = new GMarkerGoogle(
                        new GMap.NET.PointLatLng(station.Latitude, station.Longitude),
                        GMarkerGoogleType.red_dot);

                    marker.ToolTipText = toolTipText;
                    markers.Markers.Add(marker);
                }
            }

            MapControl.Overlays.Add(markers);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Methoden für das Ziehen des Fensters
        private void FlowPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void FlowPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(
                    currentScreenPos.X - dragStartPoint.X,
                    currentScreenPos.Y - dragStartPoint.Y);
            }
        }

        private void FlowPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void NUDRadius_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}