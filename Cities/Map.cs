using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.V4.App;

namespace Cities
{
    [Activity(Label = "Map")]
    public class Map : Activity, IOnMapReadyCallback
    {
        private GoogleMap _gMap;
        private double _latitude;
        private double _longitude;
        private string _title;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_map);

            _latitude = Intent.GetDoubleExtra(Constants.LatitudeExtra, 20);
            _longitude = Intent.GetDoubleExtra(Constants.LongitudeExtra, 20);
            _title = Intent.GetStringExtra(Constants.TitleExtra);

            SetupMap();
        }

        private void SetupMap()
        {
            var mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.googleMap);
            mapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            this._gMap = googleMap;

            _gMap.UiSettings.ZoomControlsEnabled = true;
            var latlng = new LatLng(_latitude, _longitude);
            var camera = CameraUpdateFactory.NewLatLngZoom(latlng, 10);
            _gMap.MoveCamera(camera);
            var options = new MarkerOptions().SetPosition(latlng).SetTitle(_title);
            _gMap.AddMarker(options);
        }
    }
}