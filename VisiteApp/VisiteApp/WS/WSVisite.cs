using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VisiteApp.Entity;

namespace VisiteApp.WS
{
    public class WSVisite
    {
        public async Task postSynchro(IEnumerable<Visite> visites, Action<IRestResponse,IEnumerable<Visite>> callback)
        {
            //DEBUG
            /*   List<Visite> vs = new List<Visite>();
               Visite v1 = new Visite();
               v1.DateVisite = DateTime.Now;
               v1.NomVisite = "visite 1";
               v1.NomCommercial = "commercial 1";
               Produit p1 = new Produit("tomate", "legumes", 3, 10, 2, true, 2);
               Produit p2 = new Produit("chips", "grignotage", 2, 2, 3, true,2);
               List<Produit> pds = new List<Produit>();
               pds.Add(p1);
               pds.Add(p2);
               v1.Produits = pds;
               Visite v2 = new Visite();
               v2.DateVisite = DateTime.Now;
               v2.NomVisite = "visite 2";
               v2.NomCommercial = "commercial 2";
               Produit p3 = new Produit("velo", "transport", 1, 1000, 5, true, 3);
               Produit p4 = new Produit("ordinateur", "multimedia", 2, 800, 4, false, 3);
               List<Produit> pds2 = new List<Produit>();
               pds2.Add(p3);
               pds2.Add(p4);
               v2.Produits = pds2;

               vs.Add(v1);
               vs.Add(v2);
               foreach(Visite v in vs)
               {
                   string d = v.DateVisite.ToString("yyyy-MM-ddTHH:mm:ss");
                   v.DateVisite = DateTime.Parse(d);
               }*/

            foreach (Visite v in visites)
            {
                string d = v.DateVisite.ToString("yyyy-MM-ddTHH:mm:ss");
                v.DateVisite = DateTime.Parse(d);
            }


            var client = new RestClient("http://192.168.100.217/ReleveLineaire/web/app_dev.php/api");
            var request = new RestRequest("/visites", Method.POST);
            request.AddHeader("Content-type", "application/json");
            string json = JsonConvert.SerializeObject(visites);
            request.AddParameter("visites",json );
            try
            {
                IRestResponse response = await client.ExecuteTaskAsync(request);
                Console.WriteLine(response.Content);
                callback(response,visites);
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }
        public ObservableCollection<Visite> JsonToVisite(string json)
        {
            ObservableCollection<Visite> visites = JsonConvert.DeserializeObject<ObservableCollection<Visite>>(json);
            return visites;
        }
    }
}
