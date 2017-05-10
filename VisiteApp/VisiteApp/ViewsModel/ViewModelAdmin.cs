using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VisiteApp.Core;
using VisiteApp.Views;
using Xamarin.Forms;

namespace VisiteApp.ViewsModel
{
    public class ViewModelAdmin : Observable
    {
        #region Fields
        private Admin _Admin;
        #endregion

        #region Properties

        public Admin Admin
        {
            get { return _Admin; }
            set
            {
                OnPropertyChanging(nameof(Admin));
                _Admin = value;
                OnPropertyChanged(nameof(Admin));

            }
        }

        #endregion

        #region Constructor
        public ViewModelAdmin(INavigation nav)
        {
            
        }

        #endregion

        #region Methods

        

        #endregion
    }
}
