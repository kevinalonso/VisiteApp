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
        private int _Etages;
        private int _Prix;
        private int _NbVue;
        private bool _Concurrents;
        private Visite _Visite;
        private int _IdServeur;

        public Produit()
        {

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

        public Visite Visite
        {
            get
            {
                return _Visite;
            }

            set
            {
                _Visite = value;
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
    }
}
