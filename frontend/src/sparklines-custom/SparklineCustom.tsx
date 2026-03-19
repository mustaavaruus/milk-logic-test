import React from 'react';
import { Sparklines, SparklinesLine } from 'react-sparklines-typescript';
import { SparklinesPropsContainerModel } from '../models/sparklines-props-container-model';

const SparklineSensorChart: React.FC<SparklinesPropsContainerModel> = (data: SparklinesPropsContainerModel) => {
  return (
    <Sparklines data={data?.data}>
      <SparklinesLine color="orange" />
    </Sparklines>
  );
};

export default SparklineSensorChart;