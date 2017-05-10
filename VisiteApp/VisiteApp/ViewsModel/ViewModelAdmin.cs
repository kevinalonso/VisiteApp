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

        private ICommand _ModifierProduit;
        private ICommand _Synchro;
        private ICommand _NewProduit;
        private ICommand _NewVisite;
        #endregion

        #region Properties
        public ICommand ModifierProduit { get { return _ModifierProduit; } }
        public ICommand Synchro { get { return _Synchro; } }
        public ICommand NewProduit { get { return _NewProduit; } }
        public ICommand NewVisite { get { return _ModifierProduit; } }

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
            this._ModifierProduit = new Command(ModifierExecuted);
            this._Synchro = new Command(SynchroExecuted);
            this._NewProduit = new Command(NewProduitExecuted);
            this._NewVisite = new Command(NewVisiteExecuted);
        }

        private void NewVisiteExecuted(object obj)
        {
           // Vers page Visite
        }

        private void NewProduitExecuted(object obj)
        {
           // Vers page Produit
        }

        private void SynchroExecuted(object obj)
        {
           // Récupération des visites non synchros

          // Appel du WS pour synchronisation
        }

        private void ModifierExecuted(object obj)
        {
            // Vers Page produit
        }

        #endregion

        #region Methods



        #endregion
    }
}
