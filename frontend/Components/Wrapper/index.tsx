import React from "react";
import styled from "styled-components";

interface IWrapperComponentProps {
    children?: any;
}

const Wrapper = styled.div`
    padding: 24px;
`;

const WapperComponent: React.FC<IWrapperComponentProps> = ({ children }) => <Wrapper>{children}</Wrapper>;

export default WapperComponent;
