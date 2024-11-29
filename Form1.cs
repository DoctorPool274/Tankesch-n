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
            //Platzhaltertext 1
            TxtOrt1.Enter += TxtOrt1_Enter;
            TxtOrt1.Leave += TxtOrt1_Leave;
            //Platzhaltertext 2
            TxtOrt2.Enter += TxtOrt2_Enter;
            TxtOrt2.Leave += TxtOrt2_Leave;
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

            //Platzhaltertext 1
            TxtOrt1.Text = "Ort/PLZ eingeben";
            TxtOrt1.ForeColor = Color.Gray; // Setze eine graue Farbe für den Hinweistext
            // Stellt sicher, dass der Fokus nicht sofort auf das Textfeld geht
            this.ActiveControl = null;

            //Platzhaltertext 2
            TxtOrt2.Text = "2.Ort/PLZ";
            TxtOrt2.ForeColor = Color.Gray; // Setze eine graue Farbe für den Hinweistext
            // Stellt sicher, dass der Fokus nicht sofort auf das Textfeld geht
            this.ActiveControl = null;

        }

        // Eingabe Ort/PLZ 1
        private void TxtOrt1_Enter(object sender, EventArgs e)
        {
            if (TxtOrt1.Text == "Ort/PLZ eingeben")
            {
                TxtOrt1.Text = "";
                TxtOrt1.ForeColor = Color.Black; // Setze die Farbe auf Schwarz für Benutzereingaben
            }
        }

        // Verlassen Ort/PLZ1
        private void TxtOrt1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtOrt1.Text))
            {
                TxtOrt1.Text = "Ort/PLZ eingeben";
                TxtOrt1.ForeColor = Color.Gray; // Textfarbe auf Grau ändern
            }
        }

        // Eingabe Ort/PLZ 2
        private void TxtOrt2_Enter(object sender, EventArgs e)
        {
            if (TxtOrt2.Text == "2.Ort/PLZ")
            {
                TxtOrt2.Text = "";
                TxtOrt2.ForeColor = Color.Black; // Setze die Farbe auf Schwarz für Benutzereingaben
            }
        }

        // Verlassen Ort/PLZ 2
        private void TxtOrt2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtOrt2.Text))
            {
                TxtOrt2.Text = "2.Ort/PLZ";
                TxtOrt2.ForeColor = Color.Gray; // Textfarbe auf Grau ändern
            }
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

                DGVTankstellen.DataSource = null;
                DGVTankstellen.AutoGenerateColumns = true;
                DGVTankstellen.DataSource = stations;

                string selectedFuel = CbSpritsorte.SelectedItem.ToString();
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

        private void TxtOrt1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}