using SQLite.Net;
using SQLite.Net.Attributes;
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
    public class DBVisite
    {
        private SQLiteConnection _connection;

        public DBVisite()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<Visite>();
        }

        public void close()
        {
            _connection.Close();
        }

        public int add(Visite item)
        {
            return _connection.Insert(item);
        }

        public void delete(int id)
        {
            _connection.Delete<Visite>(id);
        }

        public void deleteAll()
        {
            _connection.DeleteAll<Visite>();
        }

        public void update(Visite visite)
        {
            _connection.Update(visite);
        }

        public Visite get(int id)
        {
            return _connection.Table<Visite>().FirstOrDefault(Visite => Visite.Id == id);
        }


        public IEnumerable<Visite> getAll()
        {
            return (
                from t in _connection.Table<Visite>()
                select t
                    ).ToList();
        }
        public ICollection<Visite> getAllNoSynchro()
        {
            return (
                from t in _connection.Table<Visite>()
                select t
                ).Where(Visite => Visite.IsSynchro == false).ToList();
        }
    }
}
