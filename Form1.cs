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
using System.Security.Cryptography.X509Certificates;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Tankeschön.herreber;

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
            DGVTankstellen.BorderStyle = BorderStyle.None;
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
            string spritsorte = CbSpritsorte.SelectedItem.ToString();
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
            string postleitzahl = TxtOrt1.Text;
            if (string.IsNullOrWhiteSpace(postleitzahl))
            {
                MessageBox.Show("Bitte eine gültige Postleitzahl eingeben.");
                return;
            }

            try
            {
                var (latitude, longitude) = await apiService.GetCoordinatesAsync(postleitzahl);
                var stations = await apiService.GetTankstellenAsync(latitude, longitude, (double)NUDRadius.Value);
                

                if (stations.Count == 0)
                {
                    MessageBox.Show("Keine Tankstelle im Umkreis gefunden.");
                    return;
                }


                string selectedFuel = CbSpritsorte.SelectedItem.ToString();
                if(SharedData.SelectedComboBoxValue == "Diesel")
                {
                    DGVTankstellen.DataSource = null;
                    DGVTankstellen.AutoGenerateColumns = true;

                    DGVTankstellen.DataSource = stations;
                    DGVTankstellen.Columns[2].Visible = false;
                    DGVTankstellen.Columns[3].Visible = false;
                    DGVTankstellen.Columns[5].Visible = false;
                    DGVTankstellen.Columns[6].Visible = false;
                    DGVTankstellen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    DGVTankstellen.BorderStyle = BorderStyle.None;
                    DGVTankstellen.ClearSelection();
                }
                else if(SharedData.SelectedComboBoxValue =="E5")
                {
                    DGVTankstellen.DataSource = null;
                    DGVTankstellen.AutoGenerateColumns = true;
                    
                    DGVTankstellen.DataSource = stations;
                    
                    DGVTankstellen.Columns[2].Visible = false;
                    DGVTankstellen.Columns[3].Visible = false;
                    DGVTankstellen.Columns[4].Visible = false;
                    DGVTankstellen.Columns[6].Visible = false;
                    DGVTankstellen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    DGVTankstellen.BorderStyle = BorderStyle.None;
                    DGVTankstellen.ClearSelection();


                }
                else
                {
                    DGVTankstellen.DataSource = null;
                    DGVTankstellen.AutoGenerateColumns = true;

                    DGVTankstellen.DataSource = stations;
                    DGVTankstellen.Columns[2].Visible = false;
                    DGVTankstellen.Columns[3].Visible = false;
                    DGVTankstellen.Columns[4].Visible = false;
                    DGVTankstellen.Columns[5].Visible = false;
                    DGVTankstellen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    DGVTankstellen.BorderStyle = BorderStyle.None;
                    DGVTankstellen.ClearSelection();
                }


                mapHelper.UpdateMap(latitude, longitude, stations, selectedFuel);
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

        public void CbSpritsorte_SelectedIndexChanged(object sender, EventArgs e)
        {
            SharedData.SelectedComboBoxValue = CbSpritsorte.SelectedItem?.ToString();
        }
    }
}