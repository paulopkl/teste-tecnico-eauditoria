using backend.Model;
using backend.Model.Context;
using backend.Repository.Generic;
using System.Linq;

namespace backend.Repository
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(MySQLContext context) : base(context) { }

        public Client CreateClient(Client client)
        {
            try
            {
                var result = _context.Clients.Add(client);
                _context.SaveChanges();
                return result.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Client UpdateClient(long id, Client client)
        {
            var result = _context.Clients.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    client.Id = id;
                    _context.Entry(result).CurrentValues.SetValues(client);
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

        public string? DeleteClient(long id)
        {
            try
            {
                var result = _context.Clients.SingleOrDefault(p => p.Id.Equals(id));

                if (result != null)
                {
                    _context.Clients.Remove(result);
                    _context.SaveChanges();
                } else
                {
                    throw new Exception();
                }

                return null;
            }
            catch (Exception ex)
            {
                return "Error on try delete client";
            }
        }
    }
}
