import styles from './MainContainer.module.css';
import { StateEnum } from "../enums/state-enum";
import TableSensorCustom from "../table-custom/TableSensorCustom";
import { RowModel } from "../models/row-model";
import { useEffect, useRef, useState } from "react";
import SensorDataService from "../services/sensor-data-service";
import { RowData } from "@tanstack/react-table";
import { SensorData } from "../models/sensor-data";
import DateFormatService from "../services/date-format-service";
import { SummaryModel } from "../models/summary-model";
import MainProcessingService from '../services/main-processing-service';
import TableControlCustom from '../table-custom/table-controls-custom/TableControlCustom';
import ConnectionState from '../connection-state/ConnectionState';
import { ConnectionEnum } from '../enums/connection-enum';

const MainContainer = () => {

  const timeInterval = 5000;
  const periodInSeconds = 60; // данные датчика за последние 60 секунд

  const [counter, setCounter] = useState<number>(0);

  const [data, setData] = useState<RowModel[]>([]);

  let [periodBegin, setPeriodBegin] = useState<Date | null | undefined>(DateFormatService.getDataWithAddedMinutes(new Date(), -periodInSeconds));
  let [periodEnd, setPeriodEnd] = useState<Date | null | undefined>(new Date);

  let [isLive, setIsLive] = useState<boolean>(true);

  let [connectionState, setConnectionState] = useState<ConnectionEnum>(ConnectionEnum.Online);
  let [connectionAttempts, setConnectionAttempts] = useState<number>(0);

  const loadSensorData = async () => {

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
      (err) => {
        console.error("ERRRORRS!");
        console.error(err);
        setConnectionAttempts(connectionAttempts + 1);
        if (connectionAttempts <= 3) {
          setConnectionState(ConnectionEnum.Reconnecting);
        } else {
          setConnectionState(ConnectionEnum.Offline);
        }
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
          isLive ? <ConnectionState state={connectionState} /> : <div>Вы находитесь в режиме оффлайн</div>
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

    </div>
  );
}

export default MainContainer;