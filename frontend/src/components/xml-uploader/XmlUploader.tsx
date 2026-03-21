import React, { useState, ChangeEvent } from 'react';
import styles from './XmlUploader.module.css';
import { toast } from 'react-toastify';
import NotifyService from '../../services/notify-service';
import { Card, Container, Form } from 'react-bootstrap';

const XmlValidator: React.FC = () => {

    const baseUrl = process.env.REACT_APP_API_BASE_URL; // TODO: env

    const [file, setFile] = useState<File | null>(null);
    const [status, setStatus] = useState<string>('');

    const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {

        setStatus('');
        if (e.target.files && e.target.files[0]) {
            setFile(e.target.files[0]);
        }
    };

    const handleValidate = async () => {
        if (!file) return;

        setStatus('');

        try {
            const fileContent = await file.text();
            const response = await fetch(`${baseUrl}/api/validate-xml-data`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/xml',
                },
                body: fileContent,
            });

            if (response.ok) {
                const result = await response.json();
                setStatus('Успешно: ' + JSON.stringify(result));
                NotifyService.notifyInfo("Успех!");

            } else {
                setStatus(`Ошибка сервера: ${response.status}`);
                NotifyService.notifyError('Ошибка');
            }
        } catch (error) {
            console.error('Ошибка:', error);
            setStatus('Ошибка при отправке данных');
            NotifyService.notifyError(status);
        }
    };

    return (

        <div style={{ padding: '20px' }}>
            <Card body data-bs-theme="dark">
                <h4>Валидация файла</h4>
                <input type="file" accept=".xml" onChange={handleFileChange} />
                <button
                    onClick={handleValidate}
                    disabled={!file}
                    style={{ marginLeft: '10px' }}
                >
                    Валидировать XML
                </button>
                <br/>
                <Form.Text>Результат проверки файла: {status}</Form.Text>
            </Card>
        </div>

    );
};

export default XmlValidator;