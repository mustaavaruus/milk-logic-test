import { format } from "date-fns/format";

const DateFormatService = {
    
    // Возвращает данные за заданный период времени.
    getDataByPeriod: (date: Date | null | undefined, myFormat: 'HH:mm:ss' | 'dd.MM.yyyy' = 'HH:mm:ss'): string => {

        if (date == null) {
            return "";
        }
        let result = format(date!, myFormat);
        return result;
    },

    // Добавляет минуты к дате-времени.
    getDataWithAddedMinutes: (date: Date | null | undefined, seconds : number): Date | null => {

        if (date == null) {
            return null;
        }

        let newDate = new Date(date);
        newDate.setTime(newDate.getTime() + seconds * 1000);
        return newDate;
    },

};

export default DateFormatService;