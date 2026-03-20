import styles from './MainContainer.module.css';
import { StateEnum } from "../../enums/state-enum";
import TableSensorCustom from "../table-custom/TableSensorCustom";
import { RowModel } from "../../models/row-model";
import { useEffect, useRef, useState } from "react";
import SensorDataService from "../../services/sensor-data-service";
import { RowData } from "@tanstack/react-table";
import { SensorData } from "../../models/sensor-data";
import DateFormatService from "../../services/date-format-service";
import { SummaryModel } from "../../models/summary-model";
import MainProcessingService from '../../services/main-processing-service';
import TableControlCustom from '../table-custom/table-controls-custom/TableControlCustom';
import ConnectionState from '../connection-state/ConnectionState';
import { ConnectionEnum } from '../../enums/connection-enum';
import { toast } from 'react-toastify';
import XmlUploader from '../xml-uploader/XmlUploader';
import NotifyService from '../../services/notify-service';

const MainContainer = () => {

  const timeInterval = 5000;
  const periodInSeconds = 60; // данные датчика за последние 60 секунд
  const connectionsAttemptMax = 3;

  const [counter, setCounter] = useState<number>(0);

  const [data, setData] = useState<RowModel[]>([]);

  let [periodBegin, setPeriodBegin] = useState<Date | null | undefined>(DateFormatService.getDataWithAddedMinutes(new Date(), -periodInSeconds));
  let [periodEnd, setPeriodEnd] = useState<Date | null | undefined>(new Date);

  let [isLive, setIsLive] = useState<boolean>(true);
  let [isOffline, setIsOffline] = useState<boolean>(false);

  let [connectionState, setConnectionState] = useState<ConnectionEnum>(ConnectionEnum.Online);
  let [connectionAttempts, setConnectionAttempts] = useState<number>(0);

  const loadSensorData = async () => {

    if (isOffline == true) {
      return;
    }

    await SensorDataService
      .getDataByPeriod(periodBegin, periodEnd).then(async (sensorData) => {
        let summary = await SensorDataService
          .getSummaryByPeriod(periodBegin, periodEnd).then();

        let result: RowModel[] = [];

        result = MainProcessingService.getProcessedData(sensorData, summary);

        setData(result);
        setConnectionState(ConnectionEnum.Online);
        setConnectionAttempts(0);
      },
        (error) => {
          if (error.response) {
            console.log(error.response.data);
            console.log(error.response.status);
            console.log(error.response.headers);
            NotifyService.notifyError(`Произошла ошибка: статус: ${error.response.data}, текст: ${error.response.data}`);
          } else if (error.request) {
            console.log(error.request);
            setConnectionAttempts(connectionAttempts + 1);
            if (connectionAttempts < connectionsAttemptMax) {
              setConnectionState(ConnectionEnum.Reconnecting);
            } else {
              setConnectionState(ConnectionEnum.Offline);
              setIsOffline(true);
              NotifyService.notifyError("Произошла ошибка: данные больше не обновляются");
            }
          } else {
            console.error('Error', error.message);
            NotifyService.notifyError(`Произошла ошибка: ${error.message}}`);
          }
          console.error(error);

        });
  }

  const updatePeriods = (): void => {
    setPeriodBegin(DateFormatService.getDataWithAddedMinutes(new Date(), -periodInSeconds));
    setPeriodEnd(new Date())
  }

  useEffect(() => {

    if (!isLive) {
      return;
    }
    updatePeriods();
    loadSensorData();
  },
    [counter]);

  useEffect(() => {

    if (isLive) {
      return;
    }
    loadSensorData();
    console.log(periodBegin, periodEnd);
  },
    [periodBegin, periodEnd]);

  useEffect(() => {

    if (!isLive) {
      return;
    }

    loadSensorData();
  },
    [isLive]);

  useEffect(() => {
    loadSensorData();
    setInterval(() => {
      setCounter((counter) => counter < 10 ? counter + 1 : 0);
    }, timeInterval);
  },
    []);

  return (
    <div>
      <div className={styles.title}>
        <h2>Разработка сервиса мониторинга данных с эмуляцией реального времени</h2>
        <div>
          <p>Роман Рыбаков</p>
          <a href="mailto:rybakov.roman.gennadievich@gmail.com">rybakov.roman.gennadievich@gmail.com</a>
        </div>
        <br />
        {
          isLive ? <ConnectionState 
                      state={connectionState} 
                      attempts={connectionAttempts} 
                      attemptsMax={connectionsAttemptMax} /> 
                : <div>Вы находитесь в режиме оффлайн</div>
        }
      </div>
      <br />
      <div className={styles.tableWrapper}>
        <TableControlCustom
          periodBegin={periodBegin}
          periodEnd={periodEnd}
          onPeriodBeginChange={setPeriodBegin}
          onPeriodEndChange={setPeriodEnd}
          onIsLiveChange={setIsLive}
        />
        <TableSensorCustom data={data} />

      </div>
      <div className={styles.uloaderWrapper}>
        <XmlUploader />
      </div>

    </div>
  );
}

export default MainContainer;