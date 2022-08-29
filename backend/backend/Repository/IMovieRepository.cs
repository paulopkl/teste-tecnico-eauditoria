using backend.Model;
using backend.Repository.Generic;

namespace backend.Repository
{
    public interface IMovieRepository : IRepository<Movie>
    {
        public string? UploadFileCsv(IFormFile file);
    }
}
