using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisiteApp.ViewsModel;
using Xamarin.Forms;

namespace VisiteApp.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            this.BindingContext = new ViewModelMain();
		}
	}
}
