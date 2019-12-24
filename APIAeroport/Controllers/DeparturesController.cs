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
    public class DeparturesController : ControllerBase
    {
        private readonly IDataRepository<Departures> _departuresRepository;

        public DeparturesController(IDataRepository<Departures> departuresRepository)
        {
            _departuresRepository = departuresRepository;
        }

        // GET: api/Departures
        [HttpGet]
        public IEnumerable<Departures> Get()
        {
            IEnumerable<Departures> departures = _departuresRepository.GetAll();
            return (departures);
        }

        // GET: api/Departures/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value: {id}";
        //}

        // POST: api/Departures
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Departures/5
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
