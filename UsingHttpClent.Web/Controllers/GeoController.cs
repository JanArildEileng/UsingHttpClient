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
                Geo = Enumerable.Range(0, 3).Select(index => new Geo
                {
                    Id = Id++,
                    LandNavn = LandNavn[index],
                    Created = DateTime.Now,
                }) 
            .ToList();
        }

        [HttpGet(Name ="GetAll")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<Geo>> GetAll()
        {
            return Ok(Geo);
        }

        [HttpGet("{Id}",Name ="Get")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Geo> Get(int Id)
        {
            var geoObject = Geo.Where(e => e.Id == Id).FirstOrDefault();
            if (geoObject != null)
                return Ok(geoObject);
            else
                return NotFound("Id=" + Id);
        }


        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(204)]
        public ActionResult Post()
        {
            var index = Geo.Count();
         


            if (index < LandNavn.Length)
            {
                var newGeo =new Geo()
                {
                    Id = Id++,
                    Created = DateTime.Now,
                    LandNavn = LandNavn[index]

                };
                Geo.Add(newGeo);
                
               return CreatedAtRoute("Get",new {newGeo.Id}, newGeo);

            } else
            {
                return NoContent();
            }
        }



    }
}
