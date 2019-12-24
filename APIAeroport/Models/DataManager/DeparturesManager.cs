using APIAeroport.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAeroport.Models.DataManager
{
    public class DeparturesManager : IDataRepository<Departures>
    {
        readonly AeroportContext _context;

        public DeparturesManager(AeroportContext context)
        {
            _context = context;
        }

        public void Add(Departures entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Departures entity)
        {
            throw new NotImplementedException();
        }

        public Departures Get(int id)
        {
            throw new NotImplementedException();
        }

        public Departures Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Departures> GetAll()
        {
            var query = from ced in _context.VolCedules
                        join gen in _context.VolGeneriques on ced.VolGeneriqueId equals gen.VolGeneriqueId
                        join aero in _context.Aeroports on gen.AeroportId equals aero.AeroportId
                        join comp in _context.Compagnies on gen.CompagnieId equals comp.CompagnieId
                        where gen.Direction == false
                        where (DateTime.Compare(DateTime.Now.AddDays(2), ced.DatePrevue) == 1)
                        where (DateTime.Compare(DateTime.Now.AddDays(-1), ced.DatePrevue) == -1)
                        select new { ced.VolCeduleId, ced.DatePrevue, gen.HeurePrevue, ced.DateRevisee, comp.logoUri, gen.VolGeneriqueId, aero.Ville, ced.Statut, ced.Porte };
            List<Departures> departures = new List<Departures>();
            foreach (var queryRes in query)
            {
                DateTime dateComplete;
                if (queryRes.DateRevisee == new DateTime())
                    dateComplete = new DateTime(queryRes.DatePrevue.Year, queryRes.DatePrevue.Month, queryRes.DatePrevue.Day, queryRes.HeurePrevue.Hour, queryRes.HeurePrevue.Minute, 0);
                else
                    dateComplete = new DateTime(queryRes.DatePrevue.Year, queryRes.DatePrevue.Month, queryRes.DatePrevue.Day, queryRes.DateRevisee.Hour, queryRes.DateRevisee.Minute, 0);
                if (DateTime.Compare(dateComplete, DateTime.Now.AddHours(-6)) == 1 && DateTime.Compare(dateComplete, DateTime.Now.AddHours(42)) == -1)
                {
                    Departures departure = new Departures();
                    departure.VolCeduleId = queryRes.VolCeduleId;
                    departure.DatePrevue = queryRes.DatePrevue.ToString("yyyy-MM-dd");
                    departure.HeurePrevue = queryRes.HeurePrevue.ToString("HH:mm");
                    if (queryRes.DateRevisee == new DateTime())
                    {
                        departure.HeureRevisee = "-";
                    }
                    else
                    {
                        departure.HeureRevisee = queryRes.DateRevisee.ToString("HH:mm");
                    }
                    departure.CompLogo = queryRes.logoUri;
                    departure.NumVol = queryRes.VolGeneriqueId;
                    departure.Destination = queryRes.Ville;
                    departure.Statut = queryRes.Statut;
                    departure.Porte = queryRes.Porte;
                    departures.Add(departure);
                }
            }
            return departures.ToList();
        }

        public void Update(Departures dbEntity, Departures entity)
        {
            throw new NotImplementedException();
        }
    }
}
