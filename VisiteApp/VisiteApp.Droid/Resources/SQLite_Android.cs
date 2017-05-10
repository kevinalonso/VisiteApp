using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using System;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using VisiteApp.Interfaces;
using SQLite.Net;
using System.IO;

namespace VisiteApp.Droid.Resources
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android()
        {

        }

        public SQLiteConnection GetConnection()
        {
            var fileName = "visites.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);

            var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var connection = new SQLite.Net.SQLiteConnection(platform, path, false);

            return connection;
        }
    }
}