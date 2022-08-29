import axios from "axios";
import React, { useEffect } from "react";
import { useRouter } from "next/router";
import {
    Button,
    ButtonProps,
    DatePicker,
    Form,
    FormItemProps,
    FormProps,
    Input,
    message,
} from "antd";
import styled from "styled-components";
import { Client } from "../../../Models/Client";
import moment from "moment";

interface IClientRegistrationProps {
    client?: Client;
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

const ClientRegistration: React.FC<IClientRegistrationProps> = ({ client }) => {
    const router = useRouter();
    const [form] = Form.useForm();

    useEffect(() => {
        if (client) {
            form.setFields([
                { name: "nome", value: client.nome },
                { name: "cpf", value: client.cpf },
                { name: "dataNascimento", value: moment(client.dataNascimento) },
            ])
        }
    }, [client]);

    const onSaveOrUpdateClient = async () => {
        try {
            const values = await form.validateFields();

            let dataNascimento = new Date(values.dataNascimento).toISOString();

            const data = {
                nome: values.nome,
                cpf: values.cpf,
                dataNascimento: dataNascimento,
            }

            if (client) {
                await axios.put(`${process.env.NEXT_PUBLIC_API_URL}/client/${client.id}`, data)
                    .then(() => {
                        message.success("Cliente atualizado com sucesso!");
                        router.push("/");
                    });
            } else {   
                await axios.post(`${process.env.NEXT_PUBLIC_API_URL}/client`, data)
                    .then(() => {
                        message.success("Cliente criado com sucesso!");
                        router.push("/");
                    });
            }
        } catch (errorInfo) {
            message.error({
                content: 'Erro ao validar cadastro',
                duration: 4
            });

            form.validateFields(["nome"]);
        }
    };

    return (
        <FormStyled form={form} name="dynamic_rule">
            <FormInputStyle
                name="nome"
                label="Nome"
                rules={[
                    { required: true, message: "Por favor informe o Nome do Cliente" },
                ]}
            >
                <Input placeholder="Informe o Nome do Cliente" />
            </FormInputStyle>
            <FormInputStyle
                name="cpf"
                label="CPF"
                rules={[
                    {
                        required: true,
                        message: "Por favor informe o CPF do Cliente",
                    },
                ]}
            >
                <Input
                    placeholder="Informe o CPF do Cliente"
                    maxLength={11}
                />
            </FormInputStyle>
            <FormInputStyle
                name="dataNascimento"
                label="Data de Nascimento"
            >
                <DatePicker />
            </FormInputStyle>
            <FormInputStyle>
                <ButtonStyled type="primary" onClick={onSaveOrUpdateClient}>Cadastrar</ButtonStyled>
                <ButtonStyled htmlType="button" onClick={() => form.resetFields()}>Limpar</ButtonStyled>
            </FormInputStyle>
        </FormStyled>
    );
};

export default ClientRegistration;
