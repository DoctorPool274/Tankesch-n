using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tankeschön
{
    public partial class Tankeschön : Form
    {
        private readonly ApiService apiService;
        private readonly MapHelper mapHelper;
        // Variablen für das Ziehen des Fensters
        private bool isDragging = false;
        private Point dragStartPoint;
        public Tankeschön()
        {
            InitializeComponent();
            apiService = new ApiService();
            mapHelper = new MapHelper(MapControl);

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

        private async void BtnSuche_Click(object sender, EventArgs e)
        {
            string ort1 = TxtOrt1.Text == "Ort/PLZ" ? null : TxtOrt1.Text;
            string ort2 = txtOrt2.Text == "Ort/PLZ" ? null : txtOrt2.Text;

            if (string.IsNullOrWhiteSpace(ort1) && string.IsNullOrWhiteSpace(ort2))
            {
                MessageBox.Show("Mindestens ein Feld muss Ort oder PLZ beinhalten");
                return;
            }

            try
            {
                List<Tankstelle> allStations = new List<Tankstelle>();
                double? lat1 = null, lng1 = null, lat2 = null, lng2 = null;
                // Ergebnisse für Ort 1
                if (!string.IsNullOrWhiteSpace(ort1))
                {
                    (lat1, lng1) = await apiService.GetCoordinatesAsync(ort1);
                    var stationsOrt1 = await apiService.GetTankstellenAsync(lat1.Value, lng1.Value, (double)NUDRadius.Value);

                    // Ergebnisse für Ort 1 anzeigen
                    if (stationsOrt1.Count > 0)
                    {
                        allStations.AddRange(stationsOrt1);
                    }
                    else
                    {
                        MessageBox.Show("Keine Tankstellen im Umkreis von Ort 1 gefunden.");
                    }
                }

                // Ergebnisse für Ort 2
                if (!string.IsNullOrWhiteSpace(ort2))
                {
                    (lat2, lng2) = await apiService.GetCoordinatesAsync(ort2);
                    var stationsOrt2 = await apiService.GetTankstellenAsync(lat2.Value, lng2.Value, (double)NUDRadius.Value);

                    // Ergebnisse für Ort 2 anzeigen
                    if (stationsOrt2.Count > 0)
                    {
                        allStations.AddRange(stationsOrt2);
                    }
                    else
                    {
                        MessageBox.Show("Keine Tankstellen im Umkreis von Ort 2 gefunden.");
                    }
                }

                if (allStations.Count > 0)
                {
                    DGVTankstellen.DataSource = null;
                    DGVTankstellen.AutoGenerateColumns = true;
                    DGVTankstellen.DataSource = allStations;

                    string selectedFuel = CbSpritsorte.SelectedItem.ToString();
                    mapHelper.UpdateMap2Orte(lat1, lng1, lat2, lng2, allStations, selectedFuel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler: {ex.Message}");
            }
        }
        private void BtnSchließen_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void NUDRadius_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}