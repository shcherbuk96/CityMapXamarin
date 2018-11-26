using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V4.App;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Cities
{
    [Activity(Label = "Map")]
    public class Map : Activity, IOnMapReadyCallback
    {
        private GoogleMap gMap;
        private double Latitude;
        private double Longitude;
        private string Title;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_map);

            Latitude = Intent.GetDoubleExtra(Constants.LatitudeExtra, 20);
            Longitude = Intent.GetDoubleExtra(Constants.LongitudeExtra, 20);
            Title = Intent.GetStringExtra(Constants.TitleExtra);

            SetupMap();
        }

        private void SetupMap()
        {
            var mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.googleMap);
            mapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            this.gMap = googleMap;

            gMap.UiSettings.ZoomControlsEnabled = true;
            LatLng latlng = new LatLng(Latitude, Longitude);
            //LatLng latlng = new LatLng(Convert.ToDouble(13.0291), Convert.ToDouble(80.2083));
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 10);
            gMap.MoveCamera(camera);
            MarkerOptions options = new MarkerOptions().SetPosition(latlng).SetTitle(Title);
            gMap.AddMarker(options);
        }
    }
}