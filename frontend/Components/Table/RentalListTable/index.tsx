import React, { useState } from "react";
import { Button, ButtonProps, message, Modal, Space, Table, Tag } from "antd";
import { ColumnsType } from "antd/lib/table";
import { Rental } from "../../../Models/Rental";
import { useRouter } from "next/router";
import styled from "styled-components";
import axios from "axios";

interface IRentalListTableProps {
    locations?: Rental[];
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

const columns: ColumnsType<Rental> = [
    {
        key: "cliente",
        title: "Cliente",
        dataIndex: "cliente",
        render: client => client,
    },
    {
        key: "filme",
        title: "Filme Alugado",
        dataIndex: "filme",
        render: movie => movie
    },
    {
        key: "dataLocacao",
        title: "Data de Locação",
        dataIndex: "dataLocacao",
        render: locationDate => {
            if (!locationDate) return "-";

            let date = new Date(locationDate);
            return date.toLocaleDateString("pt-BR", {
                day: "numeric",
                month: "numeric",
                year: "numeric",
                hour: "numeric",
                minute: "numeric",
                second: "numeric"
            });
        }
    },
    {
        key: "dataDevolucao",
        title: "Data de Devolução",
        dataIndex: "dataDevolucao",
        render: devolutionDate => {
            if (!devolutionDate) {
                return <Tag color={"red"} key={"noDevolution"}>
                    Pendente
                </Tag>;
            }

            let date = new Date(devolutionDate);
            return date.toLocaleDateString("pt-BR", {
                day: "numeric",
                month: "numeric",
                year: "numeric",
                hour: "numeric",
                minute: "numeric",
                second: "numeric"
            });
        }
    },
];

const RentalListTable: React.FC<IRentalListTableProps> = ({ locations }) => {
    const router = useRouter();

    const [isModalVisible, setIsModalVisible] = useState(false);
    const [currentRentalDelete, setCurrentRentalDelete] = useState<Rental>();
    const [locationList, setlocationsList] = useState<Rental[]>(locations!);
    const [loading, setLoading] = useState<boolean>(false);

    const showModal = (location: Rental) => {
        setCurrentRentalDelete(location);
        setIsModalVisible(true);
    };

    const handleOnDelete = async () => {
        setLoading(true);

        await axios.delete(`${process.env.NEXT_PUBLIC_API_URL}/location/${currentRentalDelete?.id}`)
            .then(res => {
                setLoading(false);
                console.log(res);
                setlocationsList(locationList?.filter(c => c.id != res.data));
                setIsModalVisible(false);
                message.success({
                    content: "Locação removida com sucesso!",
                    duration: 4
                });
            })
            .catch(err => {
                message.error({
                    content: "Ocorreu um erro ao tentar remover a locação, por favor tente mais tarde",
                    duration: 4
                });
            });
    };

    const handleCancel = () => {
        setCurrentRentalDelete(undefined);
        setIsModalVisible(false);
    };

    return (
        <>
            <Modal
                title="Remover Locação"
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
                <p>Tem certeza que deseja remover essa locação?</p>
            </Modal>
            <Table
                columns={[
                    ...columns,
                    {
                        key: "action",
                        title: "Ação",
                        render: (_: any, location: Rental) => (
                            <Space size="middle">
                                <Button type="primary" onClick={() => router.push(`/location/update/${location.id}`)}>
                                    Editar Locação
                                </Button>
                                <ButtonRemove onClick={() => showModal(location)}>
                                    Remover Locação
                                </ButtonRemove>
                            </Space>
                        ),
                    },
                ]}
                dataSource={locationList}
            />
        </>
    );
};

export default RentalListTable;
