using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisiteApp.Entity;

namespace VisiteApp.WS
{
    public class WSVisite
    {
        public async Task postSynchro(IEnumerable<Visite> visites, Action<IRestResponse> callback)
        {
            var client = new RestClient("http://localhost/ReleveLineaire/web/api");
            var request = new RestRequest("/visites", Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddParameter("visites", JsonConvert.SerializeObject(visites));
            try
            {
                IRestResponse response = await client.ExecuteTaskAsync(request);
                Console.WriteLine(response.Content);
                callback(response);
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }
    }
}
