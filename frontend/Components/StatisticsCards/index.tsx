import { ContainerOutlined } from "@ant-design/icons";
import { Card, CardProps, Col, Row, Statistic } from "antd";
import axios, { AxiosError } from "axios";
import { NextPage } from "next";
import Link from "next/link";
import React from "react";
import { BiCameraMovie } from "react-icons/bi";
import { FiUsers } from "react-icons/fi";
import styled from "styled-components";

const CardStyled: React.FunctionComponent<CardProps> = styled(Card)`
    margin: 1vh 0;
`;

interface IStatisticCardProps {
    clientsQuantity: number;
    moviesQuantity: number;
    rentedQuantity: number;
}

const StatisticCard: NextPage<IStatisticCardProps> = ({ clientsQuantity, moviesQuantity, rentedQuantity }) => {
    
    return (
        <div>
            <Row gutter={16}>
                <Col span={12}>
                    <Link href="/client/list">
                        <CardStyled hoverable>
                            <Statistic
                                title="Numero de Clientes Cadastrados"
                                value={clientsQuantity}
                                // precision={2}
                                valueStyle={{ color: "#3f8600" }}
                                prefix={<FiUsers />}
                                // suffix="%"
                            />
                        </CardStyled>
                    </Link>
                </Col>
                <Col span={12}>
                    <Link href="/movie/list">
                        <CardStyled hoverable>
                            <Statistic
                                title="Quantidade de Filmes Cadastrados"
                                value={moviesQuantity}
                                // precision={2}
                                valueStyle={{ color: "#2f8df8" }}
                                prefix={<BiCameraMovie />}
                                // suffix="%"
                            />
                        </CardStyled>
                    </Link>
                </Col>
            </Row>
            <Row gutter={16}>
                <Col span={12}>
                    <Link href="/location/list">
                        <CardStyled hoverable>
                            <Statistic
                                title="Quantidade de Locações Realizadas"
                                value={rentedQuantity}
                                // precision={2}
                                valueStyle={{ color: "#8b0606" }}
                                prefix={<ContainerOutlined />}
                                // suffix="%"
                            />
                        </CardStyled>
                    </Link>
                </Col>
            </Row>
        </div>
    );
}

export default StatisticCard;