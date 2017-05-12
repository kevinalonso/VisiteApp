using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisiteApp.Data;
using VisiteApp.Entity;
using Xamarin.Forms;

namespace VisiteApp.Views
{
	public partial class Admin : ContentPage
	{
		public Admin ()
		{
			InitializeComponent ();
            getVisitesNonSynchro();
            getVisitesSynchro();
            MessagingCenter.Subscribe<Admin>(this, "new", (sender) => {
                Device.BeginInvokeOnMainThread(() => getVisitesNonSynchro());
            });
            MessagingCenter.Subscribe<Admin>(this, "synchro", (sender) => {
                Device.BeginInvokeOnMainThread(() => getVisitesNonSynchro());
            });
        }

        private void getVisitesSynchro()
        {
            DBVisite dbv = new DBVisite();
            IEnumerable<Visite> visites = dbv.getAllSynchro();
            ListVisitesSynchros.ItemsSource = visites;
        }

        private void getVisitesNonSynchro()
        {
            DBVisite dbv = new DBVisite();
           IEnumerable<Visite> visites = dbv.getAllNoSynchro();
            ListVisitesNonSynchros.ItemsSource = visites;
        }
    }
}
