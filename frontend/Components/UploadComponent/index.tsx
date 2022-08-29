import React, { useState } from "react";
import { Button, UploadProps } from "antd";
import { message, Upload } from "antd";
import { CloudUploadOutlined, UploadOutlined } from "@ant-design/icons";
import styled from "styled-components";
import { RcFile, UploadChangeParam, UploadFile } from "antd/lib/upload";
import axios from "axios";
import { useRouter } from "next/router";

interface IUploadComponentProps {
    title: string;
    subTitle: string;
}

const { Dragger } = Upload;

const UploadDragIcon = styled.p`
    color: #40a9ff;
    font-size: 48px;
`;

const UploadHint = styled.p`
    color: #a3a3a3;
`;

const UploadComponent: React.FC<IUploadComponentProps> = ({
    title,
    subTitle,
}) => {
    const [file, setFile] = useState<RcFile>();
    const router = useRouter();

    const onChangeFile = async (info: UploadChangeParam<UploadFile<any>>) => {
        const { status } = info.file;

        if (status == "uploading") {
            message.destroy();
            message.loading("Enviando arquivo, aguarde...");
        }

        if (status === "done") {
            message.destroy();
            message.success(`${info.file.name} fimes salvos com sucesso!`);
            router.push("/movie/list");
        } else if (status === "error") {
            message.destroy();
            message.error(`${info.file.response}. Erro ao postar arquivo.`);
        }
    };

    const onDropFile = (e: React.DragEvent<HTMLDivElement>) => {
        console.log("Dropped files", e.dataTransfer.files);
    };

    return (
        <>
            {/* <Upload
                onRemove={file => {
                    // const index = fileList.indexOf(file);
                    // const newFileList = fileList.slice();
                    // newFileList.splice(index, 1);
                    // setFileList(newFileList);
                }}
                beforeUpload={file => {
                    // setFileList([...fileList, file]);
                
                    return false;
                }}
            >
                <Button icon={<UploadOutlined />}>Select File</Button>
            </Upload> */}
            <Dragger
                // action={handleOnSubmit}
                name="file"
                maxCount={1}
                multiple={false}
                action={`${process.env.NEXT_PUBLIC_API_URL}/movie/upload-csv`}
                onChange={onChangeFile}
                onDrop={onDropFile}
            >
                <UploadDragIcon>
                    <CloudUploadOutlined />
                </UploadDragIcon>
                <h1>{title}</h1>
                <UploadHint>{subTitle}</UploadHint>
            </Dragger>
        </>
    );
};

export default UploadComponent;
