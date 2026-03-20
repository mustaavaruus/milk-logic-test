import { ConnectionEnum } from "../enums/connection-enum";

export interface ConnectionPropsModel {
    state: ConnectionEnum,
    attempts: number,
    attemptsMax: number,
}