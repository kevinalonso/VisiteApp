using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VisiteApp.Core;
using Xamarin.Forms;

namespace VisiteApp.ViewsModel
{
    public class ViewModelAdmin : Observable
    {
        #region Fields
        private string _Login;

        private ICommand _Connexion;

        private INavigation _Navigation;
        #endregion

        #region Properties
        public string Login
        {
            get { return _Login; }
            set
            {
                OnPropertyChanging(nameof(Login));
                _Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }


        public ICommand Connexion { get { return _Connexion; } }
        #endregion

        #region Constructor
        public ViewModelAdmin(INavigation nav)
        {
            this._Navigation = nav;

            this._Connexion = new Command(ConnexionExecuted);
        }

        #endregion

        #region Methods

        private void ConnexionExecuted(object obj)
        {

        }

        #endregion
    }
}
