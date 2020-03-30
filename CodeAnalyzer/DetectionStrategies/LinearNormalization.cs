﻿namespace CodeAnalyzer.DetectionStrategies
{
    using System;

    internal class LinearNormalization
    {
        private double _measurementRangeMin;
        private double _measurementRangeMax;
        private double _desiredRangeMin;
        private double _desiredRangeMax;

        public static LinearNormalization WithMeasurementRange(int measurementMin, int measurementMax)
        {
            var linearNormalization = new LinearNormalization
                                          {
                                              _measurementRangeMin = measurementMin,
                                              _measurementRangeMax = measurementMax,
                                              _desiredRangeMin = 1,
                                              _desiredRangeMax = 10
                                          };
            return linearNormalization;
        }

        public double ValueFor(double measurement)
        {
            var adjustedMeasurement = EnsureMeasurementIsInRange(measurement);

            return (adjustedMeasurement - _measurementRangeMin) / (_measurementRangeMax - _measurementRangeMin)
                   * (_desiredRangeMax - _desiredRangeMin) + _desiredRangeMin;
        }

        private double EnsureMeasurementIsInRange(double measurement)
        {
            var adjustedMeasurement = Math.Min(measurement, _measurementRangeMax);
            adjustedMeasurement = Math.Max(adjustedMeasurement, _measurementRangeMin);

            return adjustedMeasurement;
        }
    }
}