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

namespace Cities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        private const string CityNameAttribute = "CityName";
        private const string CountryNameAttribute = "CountryName";

        IEnumerable<City> cities;
        ProgressDialog progress;
        CityAdapter adapter;
        RecyclerView recyclerView;
        RecyclerView.LayoutManager layoutManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            progress = new ProgressDialog(this);


            ShowPD();
            InitialRecyclerView();
            LoadData();
        }

        private void InitialRecyclerView()
        {
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            layoutManager = new GridLayoutManager(ApplicationContext, 3);
            adapter = new CityAdapter();
            adapter.ItemClick += OnClick;
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetAdapter(adapter);
        }

        public async void LoadData()
        {

            Barrel.ApplicationId = Constants.NameDataBase;
            Barrel.EncryptionKey = Constants.KEY;

            if (!CrossConnectivity.Current.IsConnected && !Barrel.Current.IsExpired(key: Constants.URL))
            {
                //You are offline, notify the user
                Toast.MakeText(this, GetString(Resource.String.internet_connection_disable), ToastLength.Short).Show();
                cities=Barrel.Current.Get<IEnumerable<City>>(key: Constants.URL);
                adapter.Update(cities);
                DismissPD();
            }

            if(Barrel.Current.IsExpired(key: Constants.URL))
            {
                Toast.MakeText(this, "You don't have db", ToastLength.Short).Show();
                DismissPD();
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                Toast.MakeText(this, "You have Internet", ToastLength.Short).Show();
                //You are online, notify the user
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Constants.URL);
                var jsonResponce = await client.GetStringAsync(client.BaseAddress);
                var responce = JsonConvert.DeserializeObject<ListCities>(jsonResponce);

                if (responce != null)
                {
                    Console.WriteLine("true");
                    cities = responce.Photos;
                    Barrel.Current.Add(key: Constants.URL, data: cities, expireIn: TimeSpan.FromDays(1));
                    adapter.Update(cities);
                                   
                }

                DismissPD();

            }
            
        }

        public void OnClick(object sender, int position)
        {
            var city = cities.ToArray()[position];
            // Display a toast that briefly shows the enumeration of the selected photo:
            var intent = new Intent(this, typeof(CityDetails));
            intent.PutExtra(Constants.TitleExtra,city.Title);
            intent.PutExtra(Constants.DescriptionExtra, city.Description);
            intent.PutExtra(Constants.UrlExtra, city.Url);
            StartActivity(intent);
        }

        public void ShowPD()
        {
            progress.SetMessage(Resources.GetString(Resource.String.progress_dialog_message));
            progress.Show();
        }

        public void DismissPD()
        {
            progress.Dismiss();
        }
  
    }
}