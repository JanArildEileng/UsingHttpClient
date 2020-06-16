using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UsingHttpClent.Common;

namespace UsingHttpClent.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeoController : ControllerBase
    {
        private static readonly string[] LandNavn = new[]
        {
            "Norge", "Sverige", "Danmark", "Finland", "Island", "England", "Tyskland", "Nederland", "Spania", "Italia"
        };

        private static int Id = 1;


        private static List<Geo> Geo=null;

        private readonly ILogger<GeoController> _logger;

        public GeoController(ILogger<GeoController> logger)
        {
            _logger = logger;
            var rng = new Random();

            if (Geo == null)
                Geo = Enumerable.Range(0, 5).Select(index => new Geo
                {
                    Id = Id++,
                    LandNavn = LandNavn[index]
                    //Date = DateTime.Now.AddDays(index),
                    //TemperatureC = rng.Next(-20, 55),
                    //Summary = Summaries[rng.Next(Summaries.Length)] 
                }) 
            .ToList();
        }

        [HttpGet]
        public IEnumerable<Geo> Get()
        {
            //var index = Geo.Count();

            //if ( index< LandNavn.Length)
            //    Geo.Add(new Geo()
            //    {
            //        LandNavn = LandNavn[index]

            //    });
            return Geo;
        }

        [HttpPost]
        public IEnumerable<Geo> Post()
        {
            var index = Geo.Count();

            if (index < LandNavn.Length)
                Geo.Add(new Geo()
                {
                    Id = Id++,
                    LandNavn = LandNavn[index]

                });
            return Geo;
        }



    }
}
