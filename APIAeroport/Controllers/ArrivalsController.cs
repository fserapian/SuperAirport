using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIAeroport.Models;
using APIAeroport.Models.Repository;


namespace APIAeroport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArrivalsController : ControllerBase
    {
        private readonly IDataRepository<Arrivals> _arrivalsRepository;

        public ArrivalsController(IDataRepository<Arrivals> arrivalsRepository)
        {
            _arrivalsRepository = arrivalsRepository;
        }

        // GET: api/Arrivals
        [HttpGet]
        public IEnumerable<Arrivals> Get()
        {
            IEnumerable<Arrivals> arrivals = _arrivalsRepository.GetAll();
            return (arrivals);
        }

        //// GET: api/Arrivals/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value: {id}";
        //}

        // POST: api/Arrivals
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Arrivals/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
