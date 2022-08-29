using backend.Model;
using backend.Model.Context;
using backend.Repository.Generic;
using System.Data;

namespace backend.Repository
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MySQLContext context) : base(context) { }

        public string? UploadFileCsv(IFormFile file)
        {
            try
            {
                string[] nameSplitted = file.FileName.Split('.');
                if (nameSplitted.Last() != "csv")
                {
                    throw new Exception("Wrong Format file, please try insert just .csv files");
                }

                var stream = file.OpenReadStream();
                var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(stream);
                parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                parser.SetDelimiters(new string[] { ";" });

                parser.HasFieldsEnclosedInQuotes = true;

                string[] colFields = parser.ReadFields().ToArray();

                var (
                    indexId, indexTitulo, indexClassificacaoIndicativa, indexLancamento
                ) = GetIndexesFromColumns(colFields);

                var listMovies = new List<Movie>();
                var lastId = GetLastIdDB();

                while (!parser.EndOfData)
                {
                    string[] row = parser.ReadFields();

                    var id = long.Parse(row[indexId]);
                    var title = row[indexTitulo];
                    var parentalRating = int.Parse(row[indexClassificacaoIndicativa]);
                    var release = int.Parse(row[indexLancamento]);

                    if (id <= lastId)
                    {
                        throw new Exception("Error on insert ID that already exists on DB"); 
                    }

                    var movie = new Movie
                    {
                        Id = id,
                        Titulo = title,
                        ClassificacaoIndicativa = parentalRating,
                        Lancamento = release
                    };

                    listMovies.Add(movie);
                }

                _context.Movies.AddRange(listMovies);
                _context.SaveChanges();

                return null;
            }  catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private (int indexId, int indexTitulo, int indexClassificacaoIndicativa, int indexLancamento)
            GetIndexesFromColumns(string[] colsFields)
        {
            int indexId = Array.IndexOf(colsFields, "Id");
            int indexTitulo = Array.IndexOf(colsFields, "Titulo");
            int indexClassificacaoIndicativa = Array.IndexOf(colsFields, "ClassificacaoIndicativa");
            int indexLancamento = Array.IndexOf(colsFields, "Lancamento");

            return (indexId, indexTitulo, indexClassificacaoIndicativa, indexLancamento);
        }

        private long GetLastIdDB()
        {
            List<Movie> list = _context.Movies.ToList();
            long lastId = (long)(list.Count > 0 ? _context.Movies.ToList()?.Max(x => x.Id) : 0);

            return lastId;
        }
    }
}
