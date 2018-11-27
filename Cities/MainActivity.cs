using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using System;
using Android.Content;
using System.Linq;
using System.Net.Http;
using System.ComponentModel;
using Newtonsoft.Json;
using Android.Support.V7.Widget;
using Cities.Adapter;
using Plugin.Connectivity;
using MonkeyCache;
using MonkeyCache.LiteDB;
using System.Threading.Tasks;
using Cities.Data;

namespace Cities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        private ILoadData _iLoadData = new LoadData();
        private IEnumerable<City> _cities;
        private ProgressDialog _progress;
        private CityAdapter _adapter;
        private RecyclerView _recyclerView;
        private RecyclerView.LayoutManager _layoutManager;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            _progress = new ProgressDialog(this);
            
            ShowPD();
            InitialRecyclerView();
            _cities = await _iLoadData.GetDataAsync(); //TODO???

            if (_cities != null)
            {
                _adapter.Update(_cities);
            }

            DismissPD();
            
        }

        private void InitialRecyclerView()
        {
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            _layoutManager = new GridLayoutManager(ApplicationContext, 3);
            _adapter = new CityAdapter();
            _adapter.ItemClick += AdapterClick;
            _recyclerView.SetLayoutManager(_layoutManager);
            _recyclerView.SetAdapter(_adapter);
        }

        public void AdapterClick(object sender, int position)
        {
            var city = _cities.ToArray()[position];

            var intent = new Intent(this, typeof(CityDetails));
            intent.PutExtra(Constants.TitleExtra,city.Title);
            intent.PutExtra(Constants.DescriptionExtra, city.Description);
            intent.PutExtra(Constants.UrlExtra, city.Url);
            intent.PutExtra(Constants.LatitudeExtra, city.Latitude);
            intent.PutExtra(Constants.LongitudeExtra, city.Longitude);

            StartActivity(intent);
        }

        public void ShowPD()
        {
            _progress.SetMessage(Resources.GetString(Resource.String.progress_dialog_message));
            _progress.Show();
        }

        public void DismissPD()
        {
            _progress.Dismiss();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _adapter.ItemClick -= AdapterClick;
            }

            base.Dispose(disposing);
        }

    }
}