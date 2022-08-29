import React from "react";
import styled from "styled-components";
import { Menu, MenuProps } from "antd";
import { ContainerOutlined } from "@ant-design/icons";
import { BiCameraMovie } from "react-icons/bi";
import { FiUsers, FiUserPlus } from "react-icons/fi";
import { SiMicrosoftexchange } from "react-icons/si";
import { HiOutlineClipboardList } from "react-icons/hi";
import { AiOutlineCloudUpload } from "react-icons/ai";
import { MdOutlineAssignmentReturned, MdOutlineAssignmentTurnedIn } from "react-icons/md";
import { TbFileDownload } from "react-icons/tb";
import { NextRouter, withRouter } from "next/router";
import { NextPage } from "next";

interface WithRouterProps {
    router: NextRouter;
}

const MenuStyled: React.FunctionComponent<MenuProps> = styled(Menu)`
    width: 100%;
    font-family: "Nunito";
`;

type MenuItem = Required<MenuProps>["items"][number];

const getItem = (
    label: React.ReactNode,
    key: React.Key,
    icon?: React.ReactNode,
    children?: MenuItem[],
    type?: "group"
): MenuItem => ({ key, icon, children, label, type } as MenuItem);

const items: MenuItem[] = [
    getItem("Clientes", "1", <FiUsers />, [
        getItem(
            "Ver cadastro de todos os Clientes",
            "2",
            <HiOutlineClipboardList />
        ),
        getItem("Cadastrar um novo Cliente", "3", <FiUserPlus />),
    ]),
    getItem("Filmes", "4", <BiCameraMovie />, [
        getItem(
            "Ver cadastro de todos os Filmes",
            "5",
            <HiOutlineClipboardList />
        ),
        getItem(
            "Fazer Upload de Filmes via arquivo .xlsx",
            "6",
            <AiOutlineCloudUpload />
        ),
    ]),
    getItem("Locações", "7", <ContainerOutlined />, [
        getItem(
            "Ver cadastro de todas as Locações",
            "8",
            <HiOutlineClipboardList />
        ),
        getItem(
            "Realizar uma nova Locação",
            "9",
            <MdOutlineAssignmentReturned />
        )
    ]),
    getItem("Relatório", "10", <SiMicrosoftexchange />, [
        getItem("Exportar Dados de Locações", "11", <TbFileDownload />),
    ]),
];

const MenuLeftComponent: NextPage<WithRouterProps> = ({ router }) => {

    const handleOnClick: MenuProps["onClick"] = (e) => {
        switch (e.key) {
            case "2": router.push("/client/list"); break;
            case "3": router.push("/client/create"); break;
            case "5": router.push("/movie/list"); break;
            case "6": router.push("/movie/upload-list"); break;
            case "8": router.push("/location/list"); break;
            case "9": router.push("/location/create"); break;
            case "11": router.push("/export-report-data"); break;
            default: break;
        }
    };

    const getDefaultKeys = (): string[] => {
        switch (router.pathname) {
            case "/client/list": return ["1", "2"];
            case "/client/create": return ["1", "3"];
            case "/movie/list": return ["4", "5"];
            case "/movie/upload-list": return ["4", "6"];
            case "/location/list": return ["7", "8"];
            case "/location/create": return ["7", "9"];
            case "/export-report-data": return ["10", "11"];
            default: return ["1", "2"];
        }
    };

    return (
        <MenuStyled
            onClick={handleOnClick}
            defaultOpenKeys={[getDefaultKeys()[0]]}
            defaultSelectedKeys={[getDefaultKeys()[1]]}
            mode="inline"
            theme="dark"
            inlineCollapsed={false}
            items={items}
        />
    );
};

export default withRouter(MenuLeftComponent);
