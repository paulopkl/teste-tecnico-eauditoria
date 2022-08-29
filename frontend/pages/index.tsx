import type { NextPage } from "next";
import Head from "next/head";
import LayoutTemplate from "../Templates/Layout";
import StatisticsCards from "../Components/StatisticsCards";
import axios, { AxiosError } from "axios";
import TitleComponent from "../Components/Title";
import WrapperComponent from "../Components/Wrapper";
import { TbMovie } from "react-icons/tb";

interface IInitialPageProps {
    clientsQuantity: number;
    moviesQuantity: number;
    rentedQuantity: number;
}

const InitialPage: NextPage<IInitialPageProps> = ({ clientsQuantity, moviesQuantity, rentedQuantity }) => {

    return (
        <>
            <Head>
                <title>Rental Company</title>
                <meta
                    name="description"
                    content="a Rental Company for your business"
                />
                <link rel="icon" href="/favicon.ico" />
            </Head>
            <LayoutTemplate>
                <WrapperComponent>
                    <TitleComponent>
                        <TbMovie /> Projeto de Teste Técnico para Vaga de FullStack Developer na E-Auditoria
                    </TitleComponent>
                    <StatisticsCards
                        clientsQuantity={clientsQuantity}
                        moviesQuantity={moviesQuantity}
                        rentedQuantity={rentedQuantity}
                    />
                </WrapperComponent>
            </LayoutTemplate>
        </>
    );
};


InitialPage.getInitialProps = async () => {
    const clientsQuantity = await axios.get(`${process.env.NEXT_PUBLIC_API_URL}/client`)
        .then(res => res.data.length)
        .catch((res: AxiosError) => 0);
    
    const moviesQuantity = await axios.get(`${process.env.NEXT_PUBLIC_API_URL}/movie`)
        .then(res => res.data.length)
        .catch((res: AxiosError) => 0);
    
    const rentedQuantity = await axios.get(`${process.env.NEXT_PUBLIC_API_URL}/location`)
        .then(res => res.data.length)
        .catch((res: AxiosError) => 0);

    return { clientsQuantity, moviesQuantity, rentedQuantity }
}

export default InitialPage;
