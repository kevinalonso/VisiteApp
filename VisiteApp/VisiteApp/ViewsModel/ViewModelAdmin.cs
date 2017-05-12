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
        private INavigation _Navigation;
        private string _Commercial;
        #endregion

        #region Properties
        public ICommand ModifierProduit { get { return _ModifierProduit; } }
        public ICommand Synchro { get { return _Synchro; } }
        public ICommand Produit { get { return _Produit; } }
        public ICommand Visite { get { return _Visite; } }
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

        #region Constructor
        public ViewModelAdmin(INavigation nav)
        {
            _Navigation = nav;

            _ModifierProduit = new Command(ModifierExecuted);
            _Synchro = new Command(SynchroExecuted);
            _Produit = new Command(NewProduitExecuted);
            _Visite = new Command(NewVisiteExecuted);
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
            ICollection<Visite> visites = db.getAllNoSynchro();
            WSVisite ws = new WSVisite();
            await ws.postSynchro(visites, callback);
            // Appel du WS pour synchronisation
        }

        private void callback(IRestResponse obj)
        {
            WSVisite ws = new WSVisite();
            ObservableCollection<Visite> visites =ws.JsonToVisite(obj.Content);
            SynchroWStoData(visites);
            MessagingCenter.Send<Admin>(this._Admin, "synchro");
        }

        private void SynchroWStoData(ObservableCollection<Visite> visites)
        {
            DBVisite dbv = new DBVisite();
           
            foreach (Visite v in visites)
            {
                if(dbv.get(v.IdServeur) == null)
                {
                    //insert
                    v.IsSynchro = true;
                    dbv.add(v);                                     
                }else
                {
                    //update
                    v.IsSynchro = true;
                    dbv.update(v);                 
                }
                SynchroProduits(v.Produits);
            }
        }

        private void SynchroProduits(List<Produit> produits)
        {
            DBProduit dbp = new DBProduit();
            foreach (Produit p in produits)
            {
                if (dbp.get(p.IdServeur) == null)
                {
                    // insert
                    dbp.add(p);
                }
                else
                {
                    //update
                    dbp.update(p);
                }
            }
        }

        private void ModifierExecuted(object obj)
        {
            // Vers Page produit
        }


        #endregion
    }
}
