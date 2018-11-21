using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Work;

namespace Cities.Adapter
{
    class CityAdapter : RecyclerView.Adapter
    {
        private IList<City> cities = new List<City>();

        public override int ItemCount => cities.Count;

        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if(holder is CityViewHolder cityViewHolder)
            {
                var city = cities[position];

                cityViewHolder.Label.Text = city.Title;
                ImageService.Instance
                            .LoadUrl(city.Url)
                            .LoadingPlaceholder("@drawable/icon_loading", ImageSource.CompiledResource)
                            .ErrorPlaceholder("@drawable/icon_error", ImageSource.CompiledResource)
                            .Into(cityViewHolder.Image);

            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item, parent, false);
            return new CityViewHolder(itemView,OnClick);
        }

        public void OnClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }

        public void Update(IEnumerable<City> cities)
        {
            this.cities.Clear();

            if (cities != null)
            {
                foreach(var city in cities)
                {
                    this.cities.Add(city);
                }
            }
            NotifyDataSetChanged();
        }
    }
}