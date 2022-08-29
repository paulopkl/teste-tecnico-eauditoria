namespace backend.Model
{
    public class ReportColumns
    {
        public string Clientes_com_atraso { get; set; }

        public string Filmes_nunca_alugados { get; set; }

        public string Cinco_filmes_mais_alugados_do_último_ano { get; set; }

        public string Tres_filmes_menos_alugados_da_ultima_semana { get; set; }

        public string Segundo_cliente_que_mais_alugou { get; set; }

        public static ReportColumns makeReportsColumns(string p1, string p2, string p3, string p4, string p5)
        {
            return new ReportColumns()
            {
                Clientes_com_atraso = p1,
                Filmes_nunca_alugados = p2,
                Cinco_filmes_mais_alugados_do_último_ano = p3,
                Tres_filmes_menos_alugados_da_ultima_semana = p4,
                Segundo_cliente_que_mais_alugou = p5
            };
        }
    }
}
