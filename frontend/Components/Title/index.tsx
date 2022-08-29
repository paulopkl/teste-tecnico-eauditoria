import React from 'react'
import styled from 'styled-components';

const Title = styled.h1`
    font-size: 2rem;
    font-weight: 700;
    margin: 24px 16px;
    font-family: "Nunito";

    display: flex;
    align-items: center;
    gap: 12px;
`;

interface ITitleProps {
    children: any
}

const TitleComponent: React.FC<ITitleProps> = (props) => <Title>{props.children}</Title>;

export default TitleComponent;