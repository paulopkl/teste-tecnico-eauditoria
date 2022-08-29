import { Button, ButtonProps, message, Modal, Space, Table } from "antd";
import { ColumnsType } from "antd/lib/table";
import axios from "axios";
import { useRouter } from "next/router";
import React, { useEffect, useState } from "react";
import styled from "styled-components";
import { Client } from "../../../Models/Client";

interface IClientsListTableProps {
    clients?: Client[];
}

interface DataType extends Client {
    key: string;
}

const ButtonRemove = styled(Button)<ButtonProps>`
    background-color: #e91515;
    color: #fff;

    &:hover,
    &:focus {
        color: #fff;
        border-color: #e91515;
        background-color: #c61818;
    }
`;

const columns: ColumnsType<Client> = [
    {
        key: "nome",
        title: "Nome",
        dataIndex: "nome",
        render: (client) => client,
    },
    {
        key: "dataNascimento",
        title: "Data de Nascimento",
        dataIndex: "dataNascimento",
        render: (bornDate) => {
            let date = new Date(bornDate);
            return date.toLocaleDateString("pt-BR", {
                day: "numeric",
                month: "numeric",
                year: "numeric",
            });
        },
    },
    {
        key: "cpf",
        title: "CPF",
        dataIndex: "cpf",
        render: (cpf) => {
            let CPF = cpf.replace(/[^\d]/g, ""); // Retira os caracteres indesejados

            return CPF.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4"); // Realizar a formatação
        },
    },
];

const ClientsListTable: React.FC<IClientsListTableProps> = ({ clients }) => {
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [currentClientDelete, setCurrentClientDelete] = useState<Client>();
    const [loading, setLoading] = useState<boolean>(false);
    const [clientsList, setClientsList] = useState<Client[]>(clients!);

    const router = useRouter();

    const showModal = (client: Client) => {
        setCurrentClientDelete(client);
        setIsModalVisible(true);
    };

    const handleOnDelete = async () => {
        setLoading(true);

        await axios.delete(`${process.env.NEXT_PUBLIC_API_URL}/client/${currentClientDelete?.id}`)
            .then(res => {
                setLoading(false);
                setClientsList(clientsList?.filter(c => c.id != res.data));
                setIsModalVisible(false);
                message.success({
                    content: "Cliente removido com sucesso!",
                    duration: 4
                });
            })
            .catch(err => {
                message.error({
                    content: "Ocorreu um erro ao tentar remover o cliente, por favor tente mais tarde",
                    duration: 4
                });
            });
    };

    const handleCancel = () => {
        setCurrentClientDelete(undefined);
        setIsModalVisible(false);
    };

    return (
        <>
            <Modal
                title="Remover Cliente"
                visible={isModalVisible}
                footer={[
                    <Button key="back" type="primary" onClick={handleCancel}>
                        Não
                    </Button>,
                    <ButtonRemove
                        key="submit"
                        loading={loading}
                        onClick={handleOnDelete}
                    >
                        Sim
                    </ButtonRemove>,
                ]}
            >
                <p>
                    Tem certeza que deseja remover o cliente {currentClientDelete?.nome}
                </p>
            </Modal>
            <Table
                columns={[
                    ...columns,
                    {
                        key: "action",
                        title: "Ação",
                        render: (_: any, record: Client) => (
                            <Space size="middle">
                                <Button type="primary" onClick={() => router.push(`/client/update/${record.id}`)}>Editar Cliente</Button>
                                <ButtonRemove onClick={() => showModal(record)}>
                                    Remover Cliente
                                </ButtonRemove>
                            </Space>
                        ),
                    },
                ]}
                dataSource={clientsList}
            />
        </>
    );
};

export default ClientsListTable;
