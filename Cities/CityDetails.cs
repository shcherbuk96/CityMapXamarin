using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FFImageLoading.Views;
using FFImageLoading;
using FFImageLoading.Work;

namespace Cities
{
    [Activity(Label = "City", Theme = "@style/AppTheme")]
    public class CityDetails : Activity
    {
        TextView titileTextView;
        TextView descriptionTextView;
        ImageViewAsync photoImageViewAsync; 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.city_details);

            descriptionTextView = FindViewById<TextView>(Resource.Id.text_view_description);
            photoImageViewAsync = FindViewById<ImageViewAsync>(Resource.Id.image_view_photo);
            
            var title = Intent.GetStringExtra(Constants.TitleExtra);
            var description = Intent.GetStringExtra(Constants.DescriptionExtra);
            var url = Intent.GetStringExtra(Constants.UrlExtra);

            descriptionTextView.Text = description;
            ImageService.Instance
            .LoadUrl(url)
            .LoadingPlaceholder("@drawable/icon_loading", ImageSource.CompiledResource)
            .ErrorPlaceholder("@drawable/icon_error", ImageSource.CompiledResource)
            .Into(photoImageViewAsync);
        }
    }
}