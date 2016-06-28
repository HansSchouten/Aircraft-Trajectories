using System;

namespace AircraftTrajectories.Models.TemporalGrid
{
    public enum GridTransformation { MAX, LDEN, SEL };

    public class GridConverter
    {
        protected TemporalGrid _input;
        protected TemporalGrid _output;
        protected GridTransformation _transformation;

        public GridConverter(TemporalGrid input, GridTransformation transformation)
        {
            _input = input;
            _transformation = transformation;
        }

        Grid calculationGrid;
        /// <summary>
        /// Transforms all grids from one metric to the metrics stored in the converter
        /// </summary>
        /// <returns></returns>
        public TemporalGrid Transform()
        {
            _output = new TemporalGrid();
            _output.Interval = _input.Interval;

            calculationGrid = Grid.CreateEmptyGrid(
                _input.GetGrid(0).Data[0].Length, 
                _input.GetGrid(0).Data.Length, 
                _input.GetGrid(0).LowerLeftCorner,
                _input.GetGrid(0).ReferencePoint,
                _input.GetGrid(0).CellSize
            );

            for (int t=0; t<_input.GetNumberOfGrids(); t++)
            {
                Grid inputGrid = _input.GetGrid(t);
                Grid outputGrid = transformGrid(inputGrid);
                _output.AddGrid(outputGrid);
            }

            return _output;
        }
        
        /// <summary>
        /// Converts a single grid to the required unit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected Grid transformGrid(Grid input)
        {
            double[][] newData = new double[input.Data.Length][];
            for (int r=0; r<input.Data.Length; r++)
            {
                newData[r] = new double[input.Data[0].Length];
                for (int c=0; c<input.Data[0].Length; c++)
                {
                    double calcVal;
                    double l1 = input.Data[0].Length;
                    double l2 = calculationGrid.Data[0].Length;
                    double val = calculationGrid.Data[r][c];
                    double val2 = input.Data[r][c];
                    newData[r][c] = calculate(calculationGrid.Data[r][c], input.Data[r][c], out calcVal);
                    calculationGrid.Data[r][c] = calcVal;
                }
            }
            Grid newGrid = new Grid(newData, input.LowerLeftCorner, input.CellSize);
            newGrid.ReferencePoint = input.ReferencePoint;
            return newGrid;
        }

        /// <summary>
        /// Calculates the noise for the chosen output level based on the previous and current value
        /// </summary>
        /// <param name="previousVal"></param>
        /// <param name="newVal"></param>
        /// <returns></returns>
        protected double calculate(double calculationValue, double newVal, out double newCalculationValue)
        {
            switch (_transformation)
            {
                case GridTransformation.MAX:
                    newCalculationValue = Math.Max(calculationValue, newVal);
                    return newCalculationValue;
                case GridTransformation.SEL:
                    newCalculationValue = calculationValue + Math.Pow(10.0, newVal / 10.0);
                    return 10.0 * Math.Log10(newCalculationValue);
                case GridTransformation.LDEN:
                    newCalculationValue = calculationValue + Math.Pow(10.0, newVal / 10.0);
                    return 10.0*Math.Log10(newCalculationValue);
                default:
                    newCalculationValue = calculationValue;
                    return 0;
            }
        }

    }
}
