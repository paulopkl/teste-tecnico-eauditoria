using backend.Model;
using backend.Repository.Generic;

namespace backend.Repository
{
    public interface ILocationRepository : IRepository<Location>
    {
        //List<Location> FindAll();

        //Location FindById(long id);

        List<LocationFormatted> FindAllJoin();

        LocationFormatted FindbyIdJoin(long id);

        Location CreateLocation(Location location);

        Location UpdateLocation(long id, Location location);

        string? DeleteLocation(long id);
    }
}
