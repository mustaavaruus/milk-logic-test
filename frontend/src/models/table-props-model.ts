export interface TablePropsModel {
    periodBegin: Date | null | undefined,
    periodEnd: Date | null | undefined,

    onPeriodBeginChange: (data: Date | null | undefined) => void;
    onPeriodEndChange: (data: Date | null | undefined) => void;

    onIsLiveChange: (data: boolean) => void;
}