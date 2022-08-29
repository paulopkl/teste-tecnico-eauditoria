using backend.Model;
using backend.Model.Context;
using ClosedXML.Excel;
using System.Data;

namespace backend.Repository
{
    public class ReportRepository : IReportRepository
    {
        protected MySQLContext _context;

        public ReportRepository(MySQLContext context)
        {
            _context = context;
        }

        public byte[] GetXLSXReport()
        {
            var fiveMoviesMostRentedLastYear = _context.Locations
                .Where(l => l.DataLocacao >= DateTime.Now.AddYears(-1))
                .ToList()
                .GroupBy(l => l.MovieId)
                .Select(l => new {
                    Movie = _context.Movies.First(m => m.Id == l.First().MovieId).Titulo,
                    Count = l.Count() 
                })
                .OrderByDescending(l => l.Count)
                .Take(5)
                .ToList();

            var delayInReturn = _context.Locations
                .Select(l => new
                {
                    Id = l.Id,
                    Cliente = _context.Clients.First(c => c.Id == l.ClientId).Nome,
                    Filme = _context.Movies.First(f => f.Id == l.MovieId).Titulo,
                    DataLocacao = l.DataLocacao,
                    DataDevolucao = l.DataDevolucao,
                    Lancamento = _context.Movies.First(f => f.Id == l.MovieId).Lancamento
                })
                .Where(l => 
                    l.DataDevolucao != null 
                        ? l.Lancamento == 0 && l.DataDevolucao > l.DataLocacao.AddDays(3)
                            || l.Lancamento == 1 && l.DataDevolucao > l.DataLocacao.AddDays(2)
                        : l.Lancamento == 0 && DateTime.Now > l.DataLocacao.AddDays(3)
                            || l.Lancamento == 1 && DateTime.Now > l.DataLocacao.AddDays(2)
                )
                .ToList();

            var moviesNeverRented = _context.Movies
                .Where(m => !_context.Locations.Select(l => l.MovieId).ToList().Contains((int)m.Id))
                .ToList();

            var lastThreeMoviesRentedLastWeek = _context.Locations
                .Where(l => l.DataLocacao >= DateTime.Now.AddDays(-7))
                .ToList()
                .GroupBy(l => l.MovieId)
                .Select(l => new {
                    Movie = _context.Movies.First(m => m.Id == l.First().MovieId).Titulo,
                    Count = l.Count()
                })
                .OrderBy(l => l.Count)
                .Take(3)
                .ToList();

            var secondClientMostRentedMovies = _context.Locations
                .ToList()
                .GroupBy(l => l.ClientId)
                .Select(l => new
                {
                    Client = _context.Clients.First(m => m.Id == l.First().ClientId).Nome,
                    Count = l.Count()
                })
                .OrderByDescending(l => l.Count)
                .Skip(1)
                .Take(1)
                .ToList();

            List<ReportColumns> dataRows = new() { };

            var countNumRows = new List<int> 
            {
                fiveMoviesMostRentedLastYear.Count,
                delayInReturn.Count,
                moviesNeverRented.Count,
                lastThreeMoviesRentedLastWeek.Count,
                secondClientMostRentedMovies.Count
            }.Max();

            for (int i = 0; i < countNumRows; i++)
            {
                dataRows.Add(
                    ReportColumns.makeReportsColumns(
                        delayInReturn.Count > i ? delayInReturn[i].Cliente : "-",
                        moviesNeverRented.Count > i ? moviesNeverRented[i].Titulo : "-",
                        fiveMoviesMostRentedLastYear.Count > i ? fiveMoviesMostRentedLastYear[i].Movie : "-",
                        lastThreeMoviesRentedLastWeek.Count > i ? lastThreeMoviesRentedLastWeek[i].Movie : "-",
                        secondClientMostRentedMovies.Count > i ? secondClientMostRentedMovies[i].Client : "-"
                    )
                );
            }

            DataTable dt = new DataTable("Report");

            dt.Columns.AddRange(
                new DataColumn[5] 
                {
                    new DataColumn("Clientes_com_atraso"),
                    new DataColumn("Filmes_nunca_alugados"),
                    new DataColumn("Cinco_filmes_mais_alugados_do_último_ano"),
                    new DataColumn("Tres_filmes_menos_alugados_da_ultima_semana"),
                    new DataColumn("Segundo_cliente_que_mais_alugou")
                }
            );

            foreach (var row in dataRows)
            {
                dt.Rows.Add(
                    row.Clientes_com_atraso, 
                    row.Filmes_nunca_alugados,
                    row.Cinco_filmes_mais_alugados_do_último_ano,
                    row.Tres_filmes_menos_alugados_da_ultima_semana,
                    row.Segundo_cliente_que_mais_alugou
                );
            }

            using XLWorkbook wb = new XLWorkbook();
            using MemoryStream stream = new MemoryStream();
            wb.Worksheets.Add(dt);
            wb.SaveAs(stream);

            return stream.ToArray();
        }
    }
}
