using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Cities
{
    class Constants
    {
        public const string TitleExtra= "TitleExtra";
        public const string DescriptionExtra= "DescriptionExtra";
        public const string UrlExtra = "UrlExtra";
        public const string LatitudeExtra = "LatitudeExtra";
        public const string LongitudeExtra = "LongitudeExtra";

        public const string NameDataBase = "DataBase";
        public const string KEY = "MyKey";

        public const string MAPS_KEY = "AIzaSyDJ6Wg1CvQMZiwmBkvHOO6pvF7us5PCd0M";
        public const string URL = "https://api.myjson.com/bins/7ybe5";
    }
}