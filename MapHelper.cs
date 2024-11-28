using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankeschön
{
    public class MapHelper
    {
        private readonly GMapControl mapControl;

        public MapHelper(GMapControl mapControl)
        {
            this.mapControl = mapControl;
        }

        public void UpdateMap(double latitude, double longitude, List<Tankstelle> stations, string selectedFuel)
        {
            mapControl.Position = new GMap.NET.PointLatLng(latitude, longitude);
            mapControl.Overlays.Clear();

            var markers = new GMapOverlay("markers");

            foreach (var station in stations)
            {
                double fuelPrice = 0.0;
                string toolTipText = $"{station.Name}\n";

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

                if (fuelPrice > 0)
                {
                    var marker = new GMarkerGoogle(
                        new GMap.NET.PointLatLng(station.Latitude, station.Longitude),
                        GMarkerGoogleType.red_dot);

                    marker.ToolTipText = toolTipText;
                    markers.Markers.Add(marker);
                }
            }

            mapControl.Overlays.Add(markers);
        }
    }
}
