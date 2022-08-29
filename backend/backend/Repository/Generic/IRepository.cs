using backend.Model.Base;

namespace backend.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T FindById(long id);
        List<T> FindAll();
        //bool Exists(long id);

        //List<T> FindWithPagedSearch(string query);
        //int GetCount(string query);
    }
}
