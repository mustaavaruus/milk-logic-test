import { StateEnum } from "../enums/state-enum";
import { RowModel } from "../models/row-model";
import { SensorData } from "../models/sensor-data";
import { SummaryModel } from "../models/summary-model";

const MainProcessingService = {

    getTrand(data: SensorData[] | undefined): number[] | null | undefined {

        if (!data) {
            return null;
        }

        let trand: number[] = [];

        for (let i = 0; i < data.length; i++) {
            trand.push(data[i].value);
        }

        return trand;
    },

    getStateByValue(data: number): StateEnum {

        if (data >= 90) {
            return StateEnum.Error;
        } else if (data >= 70 && data < 90) {
            return StateEnum.Warning;
        } else if (data < 70) {
            return StateEnum.OK;
        }

        return StateEnum.Error;
    },

    getMappedData(data: SensorData | undefined, summary: SummaryModel | null | undefined, trand: number[] | null | undefined): RowModel {
        let rowModel: RowModel = {
            id: data!.sensorId,
            lastValue: data!.value,
            trand: trand!,
            min: summary?.minimum!,
            max: summary?.maximum!,
            avg: summary?.average!,
            state: this.getStateByValue(data!.value),
            lastRefresh: data!.timestamp,
        }

        return rowModel;
    },

    getLastBySensorId(data: SensorData[], sensorId: Number): SensorData | null {

        for (let i = data.length - 1; i >= 0; i--) {
            if (data[i].sensorId == sensorId) {
                return data[i];
            }
        }

        return null;
    },

    getProcessedData(data: SensorData[], summary: SummaryModel[]): RowModel[] {

        let sensor1Data = MainProcessingService.getLastBySensorId(data, 1)!;
        let sensor2Data = MainProcessingService.getLastBySensorId(data, 2)!;
        let sensor3Data = MainProcessingService.getLastBySensorId(data, 3)!;

        let trand1 = MainProcessingService.getTrand(data.filter(x => x.sensorId == 1));
        let trand2 = MainProcessingService.getTrand(data.filter(x => x.sensorId == 2));
        let trand3 = MainProcessingService.getTrand(data.filter(x => x.sensorId == 3));


        let summaryDataSensor1 = summary.find(s => s.sensorId == 1);
        let summaryDataSensor2 = summary.find(s => s.sensorId == 2);
        let summaryDataSensor3 = summary.find(s => s.sensorId == 3);

        let result: RowModel[] = [];

        result.push(MainProcessingService.getMappedData(sensor1Data, summaryDataSensor1, trand1));
        result.push(MainProcessingService.getMappedData(sensor2Data, summaryDataSensor2, trand2));
        result.push(MainProcessingService.getMappedData(sensor3Data, summaryDataSensor3, trand3));

        return result;
    }
}

export default MainProcessingService;