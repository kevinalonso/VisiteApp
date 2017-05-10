using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisiteApp.Entity;
using VisiteApp.Interfaces;
using Xamarin.Forms;

namespace VisiteApp.Data
{
    public class DBProduit
    {
        private SQLiteConnection _connection;

        public DBProduit()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<Produit>();
        }

        public void close()
        {
            _connection.Close();
        }

        public int add(Produit item)
        {
            return _connection.Insert(item);
        }

        public void delete(int id)
        {
            _connection.Delete<Produit>(id);
        }

        public void deleteAll()
        {
            _connection.DeleteAll<Produit>();
        }

        public Produit get(int id)
        {
            return _connection.Table<Produit>().FirstOrDefault(Produit => Produit.Id == id);
        }


        public IEnumerable<Produit> getAll()
        {
            return (
                from t in _connection.Table<Produit>()
                select t
                    ).ToList();
        }
      /*  public ICollection<Produit> getAllNoSynchro()
        {
            return (
                from t in _connection.Table<Produit>()
                select t
                ).Where(Produit => Produit.IsSynchro == false).ToList();
        }*/
    }
}
