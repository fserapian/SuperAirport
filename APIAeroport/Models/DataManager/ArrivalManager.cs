using APIAeroport.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAeroport.Models.DataManager
{
    public class ArrivalManager : IDataRepository<Arrivals>
    {
        readonly AeroportContext _context;

        public ArrivalManager(AeroportContext context)
        {
            _context = context;
        }

        public void Add(Arrivals entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Arrivals entity)
        {
            throw new NotImplementedException();
        }

        public Arrivals Get(int id)
        {
            throw new NotImplementedException();
        }

        public Arrivals Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Arrivals> GetAll()
        {
            var query = from ced in _context.VolCedules
                            join gen in _context.VolGeneriques on ced.VolGeneriqueId equals gen.VolGeneriqueId
                            join aero in _context.Aeroports on gen.AeroportId equals aero.AeroportId
                            join comp in _context.Compagnies on gen.CompagnieId equals comp.CompagnieId
                            where gen.Direction == true
                            where (DateTime.Compare(DateTime.Now.AddDays(2), ced.DatePrevue) == 1)
                            where (DateTime.Compare(DateTime.Now.AddDays(-1), ced.DatePrevue) == -1)
                        select new { ced.VolCeduleId, ced.DatePrevue, gen.HeurePrevue, ced.DateRevisee, comp.logoUri, gen.VolGeneriqueId, aero.Ville, ced.Statut };
            List<Arrivals> arrivals = new List<Arrivals>();
            foreach (var queryRes in query)
            {
                DateTime dateComplete;
                if (queryRes.DateRevisee == new DateTime())
                    dateComplete = new DateTime(queryRes.DatePrevue.Year, queryRes.DatePrevue.Month, queryRes.DatePrevue.Day, queryRes.HeurePrevue.Hour, queryRes.HeurePrevue.Minute, 0);
                else
                    dateComplete = new DateTime(queryRes.DatePrevue.Year, queryRes.DatePrevue.Month, queryRes.DatePrevue.Day, queryRes.DateRevisee.Hour, queryRes.DateRevisee.Minute, 0);
                if (DateTime.Compare(dateComplete, DateTime.Now.AddHours(-6)) == 1 && DateTime.Compare(dateComplete, DateTime.Now.AddHours(42)) == -1)
                {
                    Arrivals arrival = new Arrivals();
                    arrival.VolCeduleId = queryRes.VolCeduleId;
                    arrival.DatePrevue = queryRes.DatePrevue.ToString("yyyy-MM-dd");
                    arrival.HeurePrevue = queryRes.HeurePrevue.ToString("HH:mm");
                    if (queryRes.DateRevisee == new DateTime())
                    {
                        arrival.HeureRevisee = "-";
                    }
                    else
                    {
                        arrival.HeureRevisee = queryRes.DateRevisee.ToString("HH:mm");
                    }
                    arrival.CompLogo = queryRes.logoUri;
                    arrival.NumVol = queryRes.VolGeneriqueId;
                    arrival.Provenance = queryRes.Ville;
                    arrival.Statut = queryRes.Statut;
                    arrivals.Add(arrival);
                }
            }
            return arrivals.ToList();
        }

        public void Update(Arrivals dbEntity, Arrivals entity)
        {
            throw new NotImplementedException();
        }
    }
}
