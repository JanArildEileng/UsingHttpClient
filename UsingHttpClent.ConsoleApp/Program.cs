using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UsingHttpClent.Common;

namespace UsingHttpClent.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient httpClient = InitHttpClient();

            Console.WriteLine("Hello World!");

            try
            {
                IEnumerable<Geo> geos =
                    await TestGet(httpClient);
                Console.WriteLine("TestGet Ok ");
                geos.Print();

            }
            catch (Exception exp)
            {
                Console.WriteLine("Exception " + exp.Message);
            }
        }

        private static HttpClient InitHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44355");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        private static async Task<IEnumerable<Geo>> TestGet(HttpClient httpClient)
        {
            var response=await httpClient.GetAsync("Geo");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var geos = JsonConvert.DeserializeObject<IEnumerable<Geo>>(result);
            return geos;
        }
    }


    static public class Extender
    {
        static public void Print(this IEnumerable<Geo> geos)
        {
            foreach(var geo in geos)
            {
                Console.WriteLine($" {geo.LandNavn}");
            }
        } 


    }

}
