import { Space, Table, Tag } from "antd";
import { ColumnsType } from "antd/lib/table";
import React from "react";
import { Movie } from "../../../Models/Movie";

interface IMoviesListTableProps {
    movies?: Movie[];
}

const columns: ColumnsType<Movie> = [
    {
        key: "titulo",
        title: "Filme",
        dataIndex: "titulo",
        render: client => client,
    },
    {
        key: "classificacaoIndicativa",
        title: "Classificação Indicativa",
        dataIndex: "classificacaoIndicativa",
        render: (year) => {
            let color: string = "";

            if (year == 18) color = "red"
            else if (year == 16) color = "orange"
            else if (year == 14) color = "yellow"
            else if (year == 12) color = "geekblue"
            else if (year == 10) color = "green"

            return (
                <Tag color={color} key={year}>
                    {year}
                </Tag>
            );
        },
    },
    {
        key: "lancamento",
        title: "Lançamento",
        dataIndex: "lancamento",
        render: lancamento => (
            <Tag color={lancamento == 1 ? "green" : "red"} key={lancamento == 1 ? "yes" : "no"}>
                {lancamento == 1 ? "Sim" : "Não"}
            </Tag>
        )
    }
];

const MoviesListTable: React.FC<IMoviesListTableProps> = ({ movies }) => {
    return <Table columns={columns} dataSource={movies} />;
};

export default MoviesListTable;
