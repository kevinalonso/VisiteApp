using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiteApp.Entity
{
    public class Produit
    {
        private int _Id;
        private string _Rayon;
        private string _NomProduit;
        private int _Etages;
        private int _Prix;
        private int _NbVue;
        private bool _Concurrents;
        private int _IdVisite;
        private int _IdServeur;

        public Produit()
        {

        }
        public Produit(string nomprod,string rayon,int etages,int prix,int nbvue,bool conc,int idvisite)
        {
            _NomProduit = nomprod;
            _Rayon = rayon;
            _Etages = etages;
            _Prix = prix;
            _NbVue = nbvue;
            _Concurrents = conc;
            _IdVisite = idvisite;
        }

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public string Rayon
        {
            get
            {
                return _Rayon;
            }

            set
            {
                _Rayon = value;
            }
        }

        public int Etages
        {
            get
            {
                return _Etages;
            }

            set
            {
                _Etages = value;
            }
        }

        public int Prix
        {
            get
            {
                return _Prix;
            }

            set
            {
                _Prix = value;
            }
        }

        public int NbVue
        {
            get
            {
                return _NbVue;
            }

            set
            {
                _NbVue = value;
            }
        }

        public bool Concurrents
        {
            get
            {
                return _Concurrents;
            }

            set
            {
                _Concurrents = value;
            }
        }

        public int IdServeur
        {
            get
            {
                return _IdServeur;
            }

            set
            {
                _IdServeur = value;
            }
        }

        public string NomProduit
        {
            get
            {
                return _NomProduit;
            }

            set
            {
                _NomProduit = value;
            }
        }

        public int IdVisite
        {
            get
            {
                return _IdVisite;
            }

            set
            {
                _IdVisite = value;
            }
        }
    }
}
