import {
    MenuFoldOutlined,
    MenuUnfoldOutlined
} from "@ant-design/icons";
import { Layout, PageHeaderProps } from "antd";
import { NextPage } from "next";
import Link from "next/link";
import React, { useState } from "react";
import { TbMovie } from "react-icons/tb";
import styled from "styled-components";
import MenuComponent from "../../Components/MenuLeft";

const { Header, Sider } = Layout;

interface ILogo {
    collapsed?: boolean;
}

const Logo = styled.div<ILogo>`
    height: 32px;
    margin: 20px 16px;

    display: flex;
    align-items: center;
    ${({ collapsed }) => collapsed ? "justify-content: center;" : ""}

    cursor: pointer;

    &:hover {
        filter: grayscale(40%);
    }
`;

const HeaderIcon = styled(TbMovie)`
    color: #c7eaff;
    background: rgba(0,0,0,0);
    font-size: 32px;
`;

const Title = styled.h1`
    margin: 0 0 0 12%;
    color: #c7eaff;
    font-size: 1.5rem;
    font-family: "Nunito";
`;

const MenuUnfoldOutlinedStyled = styled(MenuUnfoldOutlined)`
    padding: 0 24px;
    font-size: 18px;
    line-height: 64px;
    cursor: pointer;
    transition: color 0.3s;

    &:hover {
        color: #1890ff;
    }
`;

const MenuFoldOutlinedStyle = styled(MenuFoldOutlined)`
    padding: 0 24px;
    font-size: 18px;
    line-height: 64px;
    cursor: pointer;
    transition: color 0.3s;

    &:hover {
        color: #1890ff;
    }
`;

const SiderStyled = styled(Sider)`
    min-width: ${({ collapsed }) => collapsed ? "50px" : "350px"} !important;
`;

const HeaderStyled: React.FunctionComponent<PageHeaderProps> = styled(Header)`
    padding: 0;
    background: #fff;
`;

const LayoutTemplate: NextPage<any> = (props) => {
    const [collapsed, setCollapsed] = useState(false);

    return (
        <Layout style={{ height: "100vh" }}>
            <SiderStyled trigger={null} collapsible collapsed={collapsed}>
                <Link href="/">
                    <Logo collapsed={collapsed}>
                        <HeaderIcon />
                        {!collapsed && <Title>Gestão de Locação</Title>}
                    </Logo>
                </Link>
                <MenuComponent />
            </SiderStyled>
            <Layout>
                <HeaderStyled>
                    {collapsed ? (
                        <MenuUnfoldOutlinedStyled
                            onClick={() => setCollapsed(!collapsed)}
                        />
                    ) : (
                        <MenuFoldOutlinedStyle
                            onClick={() => setCollapsed(!collapsed)}
                        />
                    )}
                </HeaderStyled>
                {props.children}
            </Layout>
        </Layout>
    );
};

export default LayoutTemplate;
