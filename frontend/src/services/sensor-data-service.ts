import axios from 'axios';
import { SensorData } from '../models/sensor-data';
import { SummaryModel } from '../models/summary-model';

const BASE_URL = 'http://localhost:5100'; // TODO в ENV

const SensorDataService = {
    
    // Возвращает данные за заданный период времени.
    getDataByPeriod: async (periodBegin: Date | null | undefined, periodEnd: Date | null | undefined): Promise<SensorData[]> => {

        const params = { 
            PeriodBegin: periodBegin, 
            PeriodEnd: periodEnd
        };

        const response = await axios.get<SensorData[]>(`${BASE_URL}/api/data`, { params });
        return response.data;
    },

    // Возвращает агрегированные данные (среднее, максимум, минимум) за указанный период.
    getSummaryByPeriod: async (periodBegin: Date | null | undefined, periodEnd: Date | null | undefined): Promise<SummaryModel[]> => {

        const params = { 
            PeriodBegin: periodBegin, 
            PeriodEnd: periodEnd
        };

        const response = await axios.get<SummaryModel[]>(`${BASE_URL}/api/sensors/summary`, { params });
        return response.data;
    },

    // Функционал для валидации данных, отправляемых с фронтенда в формате XML.
    validateXMLData: async (): Promise<SensorData[]> => {
        const response = await axios.get<SensorData[]>(`${BASE_URL}/api/data`);
        return response.data;
    },

};

export default SensorDataService;