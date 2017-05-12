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

        private Visite _SelectionVisite;

        private bool _SwitchCheck;

        private string _Commercial;
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

        public Visite SelectionVisite
        {
            get { return _SelectionVisite; }
            set
            {
                OnPropertyChanging(nameof(SelectionVisite));
                _SelectionVisite = value;
                OnPropertyChanged(nameof(SelectionVisite));
            }
        }

        public string Commercial
        {
            get { return _Commercial; }
            set
            {
                OnPropertyChanging(nameof(Commercial));
                _Commercial = value;
                OnPropertyChanged(nameof(Commercial));
            }
        }

        public bool SwitchCheck
        {
            get { return _SwitchCheck; }
            set
            {
                OnPropertyChanging(nameof(SwitchCheck));
                _SwitchCheck = value;
                OnPropertyChanged(nameof(SwitchCheck));
            }
        }

        #endregion

        #region Constructor
        public ViewModelFormulaire(INavigation nav)
        {
            _Navigation = nav;
            Visites = new ObservableCollection<Visite>();
            ProduitEntity = new Produit();
            SelectionVisite = new Visite();

            GetVisite();
           
            //Command
            this._Valider = new Command(ValiderExecuted);
        }



        #endregion

        #region Methods

        private void ValiderExecuted(object obj)
        {
            //Ajouter la vérification
            
            this._SelectionVisite.NomCommercial = this._Commercial;
            ProduitEntity.IdVisite = this._SelectionVisite.Id;
            ProduitEntity.Concurrents = SwitchCheck;

            //Insertion du produit en base
            DBProduit dbp = new DBProduit();
            dbp.add(ProduitEntity);
            IEnumerable<Produit> prd = dbp.getAll();
            //Met à jour la Visite avec le nouveau produit
            DBVisite dbv = new DBVisite();
            this._SelectionVisite.Produits.Add(ProduitEntity);
            
            dbv.update(this._SelectionVisite);

           IEnumerable<Visite> vs = dbv.getAll();

            //Remet à zéro le formulaire
            ProduitEntity.NomProduit = "";
            ProduitEntity.Rayon = "";
            ProduitEntity.Etages = 0;
            ProduitEntity.Prix = 0;
            ProduitEntity.NbVue = 0;

            ProduitEntity = ProduitEntity;

            SwitchCheck = false;
            
        }

        private void GetVisite()
        {
            //Récupération en base de données locale les visite pour le formulaire
            DBVisite db = new DBVisite();
            IEnumerable<Visite> vs = db.getAllNoSynchro();

            foreach(Visite item in vs)
            {
                Visites.Add(item);
            }
        }

        #endregion
    }
}
