import { Content } from 'antd/lib/layout/layout';
import React from 'react';
import styled from 'styled-components';

interface IContentCardProps {
    children: JSX.Element | JSX.Element[];
    responsive?: boolean;
}

interface ContentStyledProps {
    responsive?: boolean;
}

const ContentStyled = styled(Content)<ContentStyledProps>`
    background: #fff;
    margin: 24px 16px;
    padding: 24px;
    min-height: 280px;

    ${({ responsive }) => responsive ? "width: fit-content;" : ""}
`;

const ContentCard: React.FC<IContentCardProps> = ({ children, responsive }) => {
    return (
        <ContentStyled responsive={responsive}>
            {children}
        </ContentStyled>
    );
}

export default ContentCard;