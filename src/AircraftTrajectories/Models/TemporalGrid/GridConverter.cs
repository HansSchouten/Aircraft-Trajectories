using System;
using System.Windows.Forms;

namespace AircraftTrajectories.Models.TemporalGrid
{
    public enum GridTransformation { MAX, SEL };

    public class GridConverter
    {
        protected TemporalGrid _input;
        protected TemporalGrid _output;
        protected GridTransformation _transformation;

        /// <summary>
        /// Create a GridConverter object
        /// </summary>
        /// <param name="input">The temporalgrid of which the grids needs to be transformed</param>
        /// <param name="transformation"></param>
        public GridConverter(TemporalGrid input, GridTransformation transformation)
        {
            _input = input;
            _transformation = transformation;
        }

        /// <summary>
        /// Transforms all grids from one metric to the metrics stored in the converter
        /// </summary>
        /// <returns></returns>
        public TemporalGrid transform()
        {
            _output = new TemporalGrid();
            _output.Interval = _input.Interval;
            memoryGrid = _input.GetGrid(0);

            for (int t=0; t<_input.GetNumberOfGrids(); t++)
            {
                Grid inputGrid = _input.GetGrid(t);
                Grid outputGrid = transformGrid(inputGrid);
                _output.AddGrid(outputGrid);
            }

            return _output;
        }

        protected Grid memoryGrid;
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
                    newData[r][c] = calculate(memoryGrid.Data[r][c], input.Data[r][c]);
                }
            }
            Grid newGrid = new Grid(newData);
            memoryGrid = newGrid;
            return newGrid;
        }

        /// <summary>
        /// Calculates the noise for the chosen output level based on the previous and current value
        /// </summary>
        /// <param name="previousVal"></param>
        /// <param name="newVal"></param>
        /// <returns></returns>
        protected double calculate(double previousVal, double newVal)
        {
            switch (_transformation)
            {
                case GridTransformation.MAX:
                    return Math.Max(previousVal, newVal);
                case GridTransformation.SEL:
                    double previousSum = Math.Pow(10.0, previousVal/10.0);
                    double currentSum = previousSum + Math.Pow(10.0, newVal / 10.0);
                    return 10.0*Math.Log10(currentSum);
                default:
                    return 0;
            }
        }
    }
}
