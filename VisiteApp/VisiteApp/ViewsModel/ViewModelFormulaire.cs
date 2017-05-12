using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using VisiteApp.Core;
using VisiteApp.Data;
using VisiteApp.Entity;
using VisiteApp.Views;
using Xamarin.Forms;

namespace VisiteApp.ViewsModel
{
    public class ViewModelFormulaire : Observable
    {
        #region Fields
        private INavigation _Navigation;
        private Formulaire _Formulaire;

        private Produit _Produit;

        private ICommand _Valider;

        private ObservableCollection<Visite> _Visites;
        #endregion

        #region Properties
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

        public Formulaire Formulaire
        {
            get
            {
                return _Formulaire;
            }

            set
            {
                _Formulaire = value;
            }
        }

        public ICommand Valider { get { return _Valider; } }

        public ObservableCollection<Visite> Visites
        {
            get { return _Visites; }
            private set
            {
                OnPropertyChanging(nameof(Visites));
                _Visites = value;
                OnPropertyChanged(nameof(Visites));
            }
        }

        public Produit ProduitEntity
        {
            get { return _Produit; }
            set
            {
                OnPropertyChanging(nameof(ProduitEntity));
                _Produit = value;
                OnPropertyChanged(nameof(ProduitEntity));
            }
        }

        #endregion

        #region Constructor
        public ViewModelFormulaire(INavigation nav)
        {
            _Navigation = nav;
            Visites = new ObservableCollection<Visite>();
            GetVisite();
           
            //Command
            this._Valider = new Command(ValiderExecuted);
        }



        #endregion

        #region Methods

        private void ValiderExecuted(object obj)
        {

        }

        private void GetVisite()
        {
            //Récupération en base de données locale les visite pour le formulaire
            DBVisite db = new DBVisite();
            IEnumerable<Visite> vs = db.getAll();

            foreach(Visite item in vs)
            {
                Visites.Add(item);
            }
        }

        #endregion
    }
}
