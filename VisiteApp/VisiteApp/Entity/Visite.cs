using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisiteApp.Entity
{
    public class Visite
    {
        private int _Id;
        private DateTime _DateVisite;
        private string _NomVisite;
        private string _NomCommercial;
        private bool _IsSynchro;
        private List<Produit> produits;
        private int _IDServeur;

        public Visite()
        {

        }
        public Visite(DateTime dateVisite,string nomVisite)
        {
            _DateVisite = dateVisite;
            _NomVisite = nomVisite; 
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

        public DateTime DateVisite
        {
            get
            {
                return _DateVisite;
            }

            set
            {
                _DateVisite = value;
            }
        }

        public string NomCommercial
        {
            get
            {
                return _NomCommercial;
            }

            set
            {
                _NomCommercial = value;
            }
        }

        public bool IsSynchro
        {
            get
            {
                return _IsSynchro;
            }

            set
            {
                _IsSynchro = value;
            }
        }

        [Ignore]
        public List<Produit> Produits
        {
            get
            {
                return produits;
            }

            set
            {
                produits = value;
            }
        }

        public int IDServeur
        {
            get
            {
                return _IDServeur;
            }

            set
            {
                _IDServeur = value;
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
    }
}
