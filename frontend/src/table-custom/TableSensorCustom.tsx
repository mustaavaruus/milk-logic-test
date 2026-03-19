import "bootstrap/dist/css/bootstrap.css";
import styles from './TableSensorCustom.module.css';
import SparklineSensorChart from '../sparklines-custom/SparklineCustom';
import { RowModel } from '../models/row-model';
import { RowContainer } from '../models/row-container';
import StateContainer from '../state-container/StateContainer';
import { StateEnum } from '../enums/state-enum';
import DateFormatService from '../services/date-format-service';
import { useEffect, useState } from 'react';
import Table from 'react-bootstrap/esm/Table';
import { Form } from "react-bootstrap";
import TableControlCustom from "./table-controls-custom/TableControlCustom";
import TableCellCustom from "./table-cell-custom/TableCellCustom";

const TableSensorCustom: React.FC<RowContainer> = (data: RowContainer) => {

    const [sensor1, setSensor1] = useState<RowModel>(data.data.find(d => d.id == 1)!);
    const [sensor2, setSensor2] = useState<RowModel>(data.data.find(d => d.id == 2)!);
    const [sensor3, setSensor3] = useState<RowModel>(data.data.find(d => d.id == 3)!);

    useEffect(() => {

        setSensor1(data.data.find(d => d.id == 1)!);
        setSensor2(data.data.find(d => d.id == 2)!);
        setSensor3(data.data.find(d => d.id == 3)!);
    }, [data]);

    return (
        <div className={styles.tableWrapper}>
            <Table striped bordered hover variant="dark">
                <thead>
                    <tr>
                        <th style={{ width: '12%' }}># Sensor</th>
                        <th style={{ width: '12%' }}>Последнее значение</th>
                        <th style={{ width: '18%' }}>Тренд (последние 60 сек)</th>
                        <th style={{ width: '12%' }}>Max</th>
                        <th style={{ width: '12%' }}>Min</th>
                        <th style={{ width: '12%' }}>Avg</th>
                        <th style={{ width: '12%' }}>Status</th>
                        <th style={{ width: '12%' }}>Обновлено</th>
                    </tr>
                </thead>
                <tbody>
                    <tr key={1}>
                        <td>1</td>
                        <td><TableCellCustom data={`${sensor1?.lastValue ?? ""}`} value={sensor1?.lastValue} /></td>
                        <td><SparklineSensorChart data={sensor1?.trand ?? []} /></td>
                        <td><TableCellCustom data={`${sensor1?.max ?? ""}`} value={sensor1?.max}/></td>
                        <td><TableCellCustom data={`${sensor1?.min ?? ""}`} value={sensor1?.min}/></td>
                        <td><TableCellCustom data={`${sensor1?.avg ?? ""}`} value={sensor1?.avg}/></td>
                        <td><TableCellCustom data={<StateContainer state={sensor1?.state} />} value={sensor1?.state} /></td>
                        <td>{`${DateFormatService.getDataByPeriod(sensor2?.lastRefresh)}`}</td>
                    </tr>
                    <tr key={2}>
                        <td>2</td>
                        <td><TableCellCustom data={`${sensor2?.lastValue ?? ""}`} value={sensor2?.lastValue} /></td>
                        <td><SparklineSensorChart data={sensor2?.trand ?? []} /></td>
                        <td><TableCellCustom data={`${sensor2?.max ?? ""}`} value={sensor2?.max}/></td>
                        <td><TableCellCustom data={`${sensor2?.min ?? ""}`} value={sensor2?.min}/></td>
                        <td><TableCellCustom data={`${sensor2?.avg ?? ""}`} value={sensor2?.avg}/></td>
                        <td><TableCellCustom data={<StateContainer state={sensor2?.state} />} value={sensor2?.state} /></td>
                        <td>{`${DateFormatService.getDataByPeriod(sensor2?.lastRefresh)}`}</td>
                    </tr>
                    <tr key={3}>
                        <td>3</td>
                        <td><TableCellCustom data={`${sensor3?.lastValue ?? ""}`} value={sensor3?.lastValue} /></td>
                        <td><SparklineSensorChart data={sensor3?.trand ?? []} /></td>
                        <td><TableCellCustom data={`${sensor3?.max ?? ""}`} value={sensor3?.max}/></td>
                        <td><TableCellCustom data={`${sensor3?.min ?? ""}`} value={sensor3?.min}/></td>
                        <td><TableCellCustom data={`${sensor3?.avg ?? ""}`} value={sensor3?.avg}/></td>
                        <td><TableCellCustom data={<StateContainer state={sensor3?.state} />} value={sensor3?.state} /></td>
                        <td>{`${DateFormatService.getDataByPeriod(sensor3?.lastRefresh)}`}</td>
                    </tr>
                </tbody>
            </Table>
        </div>
    );
};

export default TableSensorCustom;