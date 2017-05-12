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
    public class ViewModelFormulaireMAJ : Observable
    {
        #region Fields
        private INavigation _Navigation;

        private Formulaire _Formulaire;

        private Produit _Produit;

        private ICommand _Valider;

        private ObservableCollection<Produit> _Produits;

        //private Visite _SelectionVisite;

        private bool _SwitchCheck;

        private string _Commercial;

        private Visite _Visite;

        private Produit _SelectionProduit;

        private bool _DesynchroVisite;
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

        public ObservableCollection<Produit> Produits
        {
            get { return _Produits; }
            set
            {
                OnPropertyChanging(nameof(Produits));
                _Produits = value;
                OnPropertyChanged(nameof(Produits));
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

        public Visite VisiteEntity
        {
            get { return _Visite; }
            set
            {
                OnPropertyChanging(nameof(VisiteEntity));
                _Visite = value;
                OnPropertyChanged(nameof(VisiteEntity));
            }
        }

        public Produit SelectionProduit
        {
            get { return _SelectionProduit; }
            set
            {
                OnPropertyChanging(nameof(SelectionProduit));
                _SelectionProduit = value;
                OnPropertyChanged(nameof(SelectionProduit));

                GetSelectionFromPicker();
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

        public bool DesynchroVisite
        {
            get { return _DesynchroVisite; }
            set
            {
                OnPropertyChanging(nameof(DesynchroVisite));
                _DesynchroVisite = value;
                OnPropertyChanged(nameof(DesynchroVisite));
            }
        }

        #endregion

        #region Constructor

        public ViewModelFormulaireMAJ(INavigation nav)
        {
            _Navigation = nav;
            //Produits = new ObservableCollection<Produit>();
            ProduitEntity = new Produit();
            SelectionProduit = new Produit();

            //Command
            this._Valider = new Command(ValiderExecuted);
        }

        #endregion

        #region Methods

        private void GetSelectionFromPicker()
        {
            ProduitEntity.NomProduit = SelectionProduit.NomProduit;
            ProduitEntity.Rayon = SelectionProduit.Rayon;
            ProduitEntity.Etages = SelectionProduit.Etages;
            ProduitEntity.Prix = SelectionProduit.Prix;
            ProduitEntity.NbVue = SelectionProduit.NbVue;

            ProduitEntity = ProduitEntity;

            SwitchCheck = SelectionProduit.Concurrents;
        }

        private void ValiderExecuted(object obj)
        {
            //Ajouter la vérification
            

            ProduitEntity.IdVisite = VisiteEntity.Id;
            ProduitEntity.Id = SelectionProduit.Id;
            ProduitEntity.Concurrents = SwitchCheck;

            if(_DesynchroVisite = true)
            {
                VisiteEntity.IsSynchro = false;
            }
            
            //Met à jour la Visite avec le nouveau produit
            DBProduit dbp = new DBProduit();
            dbp.update(ProduitEntity);

            //Met à jour la Visite avec le nouveau produit
            DBVisite dbv = new DBVisite();
            VisiteEntity.NomCommercial = _Commercial;
            VisiteEntity.Produits.Add(ProduitEntity);
            dbv.update(VisiteEntity);

            //Remet à zéro le formulaire
            ProduitEntity.NomProduit = "";
            ProduitEntity.Rayon = "";
            ProduitEntity.Etages = 0;
            ProduitEntity.Prix = 0;
            ProduitEntity.NbVue = 0;

            ProduitEntity = ProduitEntity;

            SwitchCheck = false;

        }

        #endregion
    }
}
