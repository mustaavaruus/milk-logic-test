import { Form } from "react-bootstrap";
import styles from './TableControlCustom.module.css';
import { TablePropsModel } from "../../../models/table-props-model";
import DatePicker from "react-datepicker";
import 'react-datepicker/dist/react-datepicker.css';
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import NotifyService from "../../../services/notify-service";


const TableControlCustom = (data: TablePropsModel) => {

    let [periodBegin, setPeriodBegin] = useState<Date | null | undefined>(data.periodBegin);
    let [periodEnd, setPeriodEnd] = useState<Date | null | undefined>(data.periodEnd);

    let [isLive, setIsLive] = useState<boolean>(true);
    let [isHistorical, setIsHistorical] = useState<boolean>(false);

     useEffect(() => {

        if (new Date(periodBegin!) > new Date(periodEnd!)) {
            NotifyService.notifyWarning("Дата начала больше даты конца периода");
            return;
        }

        data.onPeriodBeginChange(new Date(periodBegin!));
        data.onPeriodEndChange(new Date(periodEnd!));

     }, [periodBegin, periodEnd]);


     useEffect(() => {

        NotifyService.notifyInfo("In development");

     }, [isHistorical]);

    return (
        <div className={styles.wrapper}>
            <div className={styles.controlBlock}>
                 <DatePicker
                    selected={periodBegin}
                    onChange={setPeriodBegin}
                    showTimeSelect
                    dateFormat="dd.MM.yyyy HH:mm:ss"
                    
                />
            </div>

            <div className={styles.controlBlock}>
                <DatePicker
                    selected={periodEnd}
                    onChange={setPeriodEnd}
                    showTimeSelect
                    dateFormat="dd.MM.yyyy HH:mm:ss"
                    disabled={isLive}
                />
            </div>
            <div className={styles.controlBlock}>
                    <Form.Check 
                        checked={isLive}
                        onChange={() => {
                            setIsLive(!isLive); 
                            data.onIsLiveChange(!isLive);
                        }}
                        type="switch"
                        id="custom-switch"
                        label="Live"
                    />

            </div>
            <div className={styles.controlBlock}>
                    <Form.Check 
                        checked={isHistorical}
                        onChange={() => {setIsHistorical(!isHistorical)}}
                        type="switch"
                        id="custom-switch2"
                        label="Historical"
                    />
                </div>
        </div>
    );

}

export default TableControlCustom;