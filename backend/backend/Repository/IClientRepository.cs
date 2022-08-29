using backend.Model;
using backend.Repository.Generic;

namespace backend.Repository
{
    public interface IClientRepository : IRepository<Client>
    {
        List<Client> FindAll();

        Client FindById(long id);

        Client CreateClient(Client client);

        Client UpdateClient(long id, Client client);

        string? DeleteClient(long id);
    }
}
