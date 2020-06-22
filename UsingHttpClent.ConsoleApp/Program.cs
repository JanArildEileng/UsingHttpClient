using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
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

                Console.WriteLine("TestPost---------- ");

                await TestPost(httpClient);
                Console.WriteLine("TestPost Ok ");

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

        private static async Task TestPost(HttpClient httpClient)
        {
            StringContent stingContent = LagGeoContent("TestNavn1");

            //alternativ 1
            var response = await httpClient.PostAsync("Geo", stingContent);
            response.EnsureSuccessStatusCode();

            //alternativ 2
            stingContent = LagGeoContent("TestNavn2");

            var request = new HttpRequestMessage(HttpMethod.Post, "Geo");
            request.Content = stingContent;
            response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();


        }

        private static StringContent LagGeoContent(string landnavn)
        {
            var geo = new Geo()
            {
                LandNavn = landnavn,
                Created = DateTime.Now
            };

            var content = JsonConvert.SerializeObject(geo);
            var stingContent = new StringContent(content, Encoding.UTF8, "application/json");
            return stingContent;
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
