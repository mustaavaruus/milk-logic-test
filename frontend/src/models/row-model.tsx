import { StateEnum } from "../enums/state-enum";

export interface RowModel {
    id: Number,
    lastValue: Number,
    trand: number[],
    min: Number,
    max: Number,
    avg: Number,
    state: StateEnum,
    lastRefresh: Date,
}