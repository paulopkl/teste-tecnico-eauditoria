import { Button, ButtonProps, DatePicker, Form, FormItemProps, FormProps, Input, message, Select } from "antd";
import axios from "axios";
import moment from "moment";
import { useRouter } from "next/router";
import React, { useEffect } from "react";
import styled from "styled-components";
import { Client } from "../../../Models/Client";
import { Movie } from "../../../Models/Movie";
import { Rental } from "../../../Models/Rental";

interface IRentalRegistrationProps {
    location?: Rental;
    clients: Client[];
    movies: Movie[];
}

interface IDataRequest {
    clientId: number;
    movieId: number;
    dataLocacao: string;
    dataDevolucao?: string;
}

const ButtonStyled: React.FunctionComponent<ButtonProps> = styled(Button)`
    margin-right: 16px;
`;

const FormStyled = styled(Form)<FormProps>`
    min-width: 600px;
`;

const FormInputStyle = styled(Form.Item)<FormItemProps>`
    margin: 20px 15px 40px 15px;
    
    &:last-of-type {
        margin: 20px 15px 20px 15px;
    }
`;

const { Option } = Select;

const CreateRental: React.FC<IRentalRegistrationProps> = ({ location, clients, movies }) => {
    const router = useRouter();
    const [form] = Form.useForm();

    useEffect(() => {
        if (location) {
            const clientId = clients.find(client => client.nome == location.cliente)?.id;
            const movieId = movies.find(movie => movie.titulo == location.filme)?.id;

            form.setFields([
                { name: "clientId", value: clientId },
                { name: "movieId", value: movieId },
                { name: "dataLocacao", value: moment(location.dataLocacao) },
            ])
        }
    }, [location]);

    const onSaveOrUpdateRental = async () => {
        try {
            const values = await form.validateFields();

            let dataLocacao = new Date(values.dataLocacao).toISOString();

            const data: IDataRequest = {
                clientId: values.clientId,
                movieId: values.movieId,
                dataLocacao
            }

            if (location) {
                data.dataDevolucao = new Date(values.dataDevolucao).toISOString();
            
                await axios.put(`${process.env.NEXT_PUBLIC_API_URL}/location/${location.id}`, data)
                    .then(() => {
                        message.success("Locação atualizada com sucesso!");
                        router.push("/");
                    });
            } else {   
                await axios.post(`${process.env.NEXT_PUBLIC_API_URL}/location`, data)
                    .then(() => {
                        message.success("Locação de filme realizada com sucesso!");
                        router.push("/");
                    });
            }
        } catch (errorInfo) {
            message.error({
                content: 'Erro ao validar cadastro',
                duration: 4
            });
        }
    };

    return (
        <FormStyled form={form} name="dynamic_rule">
            <FormInputStyle
                name="clientId"
                label="Cliente"
                rules={[
                    { required: true, message: "Por favor informe um Cliente" },
                ]}
            >
                <Select>
                    {clients.map((client) => <Option value={client.id}>{client.nome}</Option>)}
                </Select>
            </FormInputStyle>
            <FormInputStyle
                name="movieId"
                label="Filme"
                rules={[
                    { required: true, message: "Por favor informe um Filme" },
                ]}
            >
                <Select>
                    {movies.map((movie) => <Option value={movie.id}>{movie.titulo}</Option>)}
                </Select>
            </FormInputStyle>
            <FormInputStyle
                name="dataLocacao"
                label="Data de Locação"
                rules={[
                    { required: true, message: "Por favor informe a data de locação" },
                ]}
            >
                <DatePicker />
            </FormInputStyle>
            {location && (
                <FormInputStyle
                    name="dataDevolucao"
                    label="Data de Devolução"
                    rules={[
                        { required: true, message: "Por favor informe a data de devolucao" },
                    ]}
                >
                    <DatePicker />
                </FormInputStyle>
            )}
            <FormInputStyle>
                <ButtonStyled type="primary" onClick={onSaveOrUpdateRental}>Cadastrar</ButtonStyled>
                <ButtonStyled htmlType="button" onClick={() => form.resetFields()}>Limpar</ButtonStyled>
            </FormInputStyle>
        </FormStyled>
    );
}

export default CreateRental;