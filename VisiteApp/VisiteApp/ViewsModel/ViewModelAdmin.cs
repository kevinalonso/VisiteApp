using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using RestSharp;
using VisiteApp.Core;
using VisiteApp.Data;
using VisiteApp.Entity;
using VisiteApp.Views;
using VisiteApp.WS;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace VisiteApp.ViewsModel
{
    public class ViewModelAdmin : Observable
    {
        #region Fields

        private ICommand _ModifierProduit;
        private ICommand _Synchro;
        private ICommand _Produit;
        private ICommand _Visite;
        private ICommand _ModifierProduitDesynchro;
        private INavigation _Navigation;
        private string _Commercial;
        private Visite _SelectionVisite;
        private Admin _Admin;
        #endregion

        #region Properties
        public ICommand ModifierProduit { get { return _ModifierProduit; } }
        public ICommand Synchro { get { return _Synchro; } }
        public ICommand Produit { get { return _Produit; } }
        public ICommand Visite { get { return _Visite; } }
        public ICommand ModifierProduitDesynchro { get { return _ModifierProduitDesynchro; } }
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

        #endregion

        #region Constructor
        public ViewModelAdmin(INavigation nav)
        {
            _Navigation = nav;
            SelectionVisite = new Visite();
            //Command
            _ModifierProduit = new Command(ModifierExecuted);
            _Synchro = new Command(SynchroExecuted);
            _Produit = new Command(NewProduitExecuted);
            _Visite = new Command(NewVisiteExecuted);
            _ModifierProduitDesynchro = new Command(ModifierDesynchroExecuted);
        }

        #endregion

        #region Methods

        private void NewVisiteExecuted(object obj)
        {
            // Vers page Visite
            VisiteForm pg = new VisiteForm();
            ViewModelVisite vm = new ViewModelVisite(pg.Navigation,_Admin);
            vm.Visite = pg;
            pg.BindingContext = vm;
            this._Navigation.PushAsync(pg).ConfigureAwait(false);
        }

        private void NewProduitExecuted(object obj)
        {
            // Vers page Produit
            Formulaire pg = new Formulaire();
            ViewModelFormulaire vm = new ViewModelFormulaire(pg.Navigation);
            vm.Formulaire = pg;
            vm.Commercial = this._Commercial;
            pg.BindingContext = vm;
            this._Navigation.PushAsync(pg).ConfigureAwait(false);
        }

        private async void SynchroExecuted(object obj)
        {
            // Récupération des visites non synchros
            DBVisite db = new DBVisite();
            DBProduit dbp = new DBProduit();
            ICollection<Visite> visites = db.getAllNoSynchro();
            foreach (Visite v in visites)
            {
                v.Produits = dbp.getAllByVisite(v.Id);
            }
            WSVisite ws = new WSVisite();
            await ws.postSynchro(visites, callback);
            // Appel du WS pour synchronisation
        }

        private void callback(IRestResponse obj, IEnumerable<Visite> vs)
        {
            WSVisite ws = new WSVisite();
            DBVisite dbv = new DBVisite();
            ObservableCollection<Visite> visites =ws.JsonToVisite(obj.Content);
            SynchroWStoData(visites);
            foreach (Visite v in vs)
            {
                dbv.delete(v.Id);
            }
            MessagingCenter.Send<Admin>(this._Admin, "synchro");
        }

        private void SynchroWStoData(ObservableCollection<Visite> visites)
        {
            DBVisite dbv = new DBVisite();
           
            foreach (Visite v in visites)
            {
                if(dbv.getByIdServeur(v.IdServeur) == null)
                {
                    //insert
                    v.IsSynchro = true;
                    dbv.add(v);                                     
                }else
                {
                    //update
                    v.IsSynchro = true;
                    dbv.updateByIdServeur(v);                 
                }
                SynchroProduits(v.Produits);
            }
        }

        private void SynchroProduits(List<Produit> produits)
        {
            DBProduit dbp = new DBProduit();
            foreach (Produit p in produits)
            {
                if (dbp.getByIdServeur(p.IdServeur) == null)
                {
                    // insert
                    dbp.add(p);
                }
                else
                {
                    //update
                    dbp.updateByIdServeur(p);
                }
            }
        }

        //Modifier produit d'une viste non synchro
        private void ModifierExecuted(object obj)
        {
            FormulaireMAJ pg = new FormulaireMAJ();
            ViewModelFormulaireMAJ vm = new ViewModelFormulaireMAJ(pg.Navigation);
            vm.VisiteEntity = SelectionVisite;
            vm.Commercial = _Commercial;
            vm.DesynchroVisite = false;
            DBProduit db = new DBProduit();
            vm.Produits = new ObservableCollection<Produit>(db.getAllByVisite(SelectionVisite.Id));
            pg.BindingContext = vm;
            this._Navigation.PushAsync(pg).ConfigureAwait(false);
        }

        private void ModifierDesynchroExecuted(object obj)
        {
            FormulaireMAJ pg = new FormulaireMAJ();
            ViewModelFormulaireMAJ vm = new ViewModelFormulaireMAJ(pg.Navigation);
            vm.VisiteEntity = SelectionVisite;
            vm.Commercial = _Commercial;
            vm.DesynchroVisite = true;
            DBProduit db = new DBProduit();
            vm.Produits = new ObservableCollection<Produit>(db.getAllByVisite(SelectionVisite.Id));
            pg.BindingContext = vm;
            this._Navigation.PushAsync(pg).ConfigureAwait(false);
        }


        #endregion
    }
}
