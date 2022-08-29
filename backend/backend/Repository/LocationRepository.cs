using backend.Model;
using backend.Model.Context;
using backend.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace backend.Repository
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(MySQLContext context) : base(context) { }

        public List<LocationFormatted> FindAllJoin()
        {
            var result = _context.Locations
                .Select(l => new LocationFormatted
                {
                    Id = l.Id,
                    Cliente = _context.Clients.First(c => c.Id == l.ClientId).Nome,
                    Filme = _context.Movies.First(f => f.Id == l.MovieId).Titulo,
                    DataLocacao = l.DataLocacao,
                    DataDevolucao = l.DataDevolucao
                })
                .ToList();

            return result;
        }

        public LocationFormatted FindbyIdJoin(long id)
        {
            List<LocationFormatted> result = _context.Locations
                .Where(x => x.Id == id)
                .Select(l => new LocationFormatted
                {
                    Id = l.Id,
                    Cliente = _context.Clients.First(c => c.Id == l.ClientId).Nome,
                    Filme = _context.Movies.First(f => f.Id == l.MovieId).Titulo,
                    DataLocacao = l.DataLocacao,
                    DataDevolucao = l.DataDevolucao
                })
                .ToList();

            return result[0];
        }

        public Location CreateLocation(Location location)
        {
            try
            {
                var result = _context.Locations.Add(location);
                _context.SaveChanges();
                return result.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Location UpdateLocation(long id, Location location)
        {
            var result = _context.Locations.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    location.Id = id;
                    _context.Entry(result).CurrentValues.SetValues(location);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        public string? DeleteLocation(long id)
        {
            var result = _context.Locations.SingleOrDefault(p => p.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    _context.Locations.Remove(result);
                    _context.SaveChanges();

                    return null;
                }

                throw new Exception();
            }
            catch (Exception ex)
            {
                return "Error on delete Location";
            }
        }
    }
}
