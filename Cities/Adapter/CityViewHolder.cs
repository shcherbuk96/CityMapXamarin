using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading.Views;

namespace Cities.Adapter
{
    class CityViewHolder : RecyclerView.ViewHolder
    {
        public ImageViewAsync Image { get; set; }
        public TextView Label { get; set; }

        public CityViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            Image = itemView.FindViewById<ImageViewAsync>(Resource.Id.image_view_photo);
            Label = itemView.FindViewById<TextView>(Resource.Id.text_view_name);

            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }
}