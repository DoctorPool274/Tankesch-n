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
        private string ToolTipText;

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
      
        public void UpdateMap2Orte(double? lat1, double? lng1, double? lat2, double? lng2,
                                   List<Tankstelle> stations, string selectedFuel)
        {
            mapControl.Overlays.Clear();
            var markers = new GMapOverlay("markers");

            if (lat1.HasValue && lng1.HasValue)
            {
                var marker1 = new GMarkerGoogle(new GMap.NET.PointLatLng(lat1.Value, lng1.Value),
                    GMarkerGoogleType.blue_dot);
                {
                    ToolTipText = "Ort 1";
                }
                markers.Markers.Add(marker1);
            }

            if (lat2.HasValue && lng2.HasValue)
            {
                var marker2 = new GMarkerGoogle(new GMap.NET.PointLatLng(lat2.Value, lng2.Value),
                    GMarkerGoogleType.purple);
                {
                    ToolTipText = "Ort 2";
                }
                markers.Markers.Add(marker2);
            }

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
                    var stationMarker = new GMarkerGoogle(
                        new GMap.NET.PointLatLng(station.Latitude, station.Longitude),
                        GMarkerGoogleType.red_dot)
                    {
                        ToolTipText = toolTipText
                    };
                    markers.Markers.Add(stationMarker);
                }
            }

            mapControl.Overlays.Add(markers);

            // Karte auf den Bereich der beiden Orte zentrieren
            if (lat1.HasValue && lng1.HasValue && lat2.HasValue && lng2.HasValue)
            {
                mapControl.SetZoomToFitRect(new GMap.NET.RectLatLng(
                    Math.Min(lat1.Value, lat2.Value),
                    Math.Min(lng1.Value, lng2.Value),
                    Math.Abs(lat1.Value - lat2.Value),
                    Math.Abs(lng1.Value - lng2.Value)));
            }
            else if (lat1.HasValue && lng1.HasValue)
            {
                mapControl.Position = new GMap.NET.PointLatLng(lat1.Value, lng1.Value);
                mapControl.Zoom = 10;
            }
            else if (lat2.HasValue && lng2.HasValue)
            {
                mapControl.Position = new GMap.NET.PointLatLng(lat2.Value, lng2.Value);
                mapControl.Zoom = 10;
            }
        }
    }
}
