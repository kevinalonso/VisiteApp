using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VisiteApp.Core;
using VisiteApp.Data;
using VisiteApp.Entity;
using VisiteApp.Views;
using Xamarin.Forms;

namespace VisiteApp.ViewsModel
{
    public class ViewModelVisite : Observable
    {
        #region Fields
        private VisiteForm _Visite;
        private DateTime _DateVisite = DateTime.Now;
        private ICommand _CreerVisite;
        private INavigation _Navigation;
        private string _NomVisite;
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public ViewModelVisite(INavigation nav)
        {
            _Navigation = nav;
            this._CreerVisite = new Command(CreerVisisteExecuted);
        }

        private void CreerVisisteExecuted(object obj)
        {
            DBVisite db = new DBVisite();
            IEnumerable<Visite> vs = db.getAll();
            if (!String.IsNullOrEmpty(_NomVisite))
            {           
                Visite v = new Visite(_DateVisite,_NomVisite);
                db.add(v);
            }                    
        }

        public VisiteForm Visite
        {
            get
            {
                return _Visite;
            }

            set
            {
                OnPropertyChanging(nameof(Visite));
                _Visite = value;
                OnPropertyChanged(nameof(Visite));
                
            }
        }

        public DateTime DateVisite
        {
            get
            {
                return _DateVisite;
            }

            set
            {
                OnPropertyChanging(nameof(DateVisite));
                _DateVisite = value;
                OnPropertyChanged(nameof(DateVisite));
                
            }
        }

        public ICommand CreerVisite
        {
            get
            {
                return _CreerVisite;
            }

            set
            {
                _CreerVisite = value;
            }
        }

        public string NomVisite
        {
            get
            {
                return _NomVisite;
            }

            set
            {
                _NomVisite = value;
            }
        }

        public INavigation Navigation
        {
            get
            {
                return _Navigation;
            }

            set
            {
                _Navigation = value;
            }
        }

        #endregion

        #region Methods

        #endregion
    }
}
