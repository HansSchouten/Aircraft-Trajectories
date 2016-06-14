using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AircraftTrajectories.Models.TemporalGrid
{
    public class TemporalGridFileReader
    {
        /// <summary>
        /// Create Temporal Grid from the position files for each time step
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public TemporalGrid createTemporalGridFromFile(string filePath)
        {
            double[][] genericData = readGenericData(filePath);
            TemporalGrid temporalGrid = new TemporalGrid();

            for (int i = 0; i < genericData[0].Length - 3; i++) {
                temporalGrid.AddGrid(GenericDataToGrid(genericData, i));
            }

            return temporalGrid;
        }

        /// <summary>
        /// Parses the generic input values into a 2D array
        /// </summary>
        /// <returns></returns>
        protected double[][] readGenericData(string filePath)
        {
            string rawGeneric = File.ReadAllText(filePath);
            double[][] genericData = rawGeneric
                .Split('\n')
                .Skip(2)
                .Select(q =>
                    q.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(Convert.ToDouble)
                     .ToArray()
                )
                .ToArray();
            return genericData;
        }

        /// <summary>
        /// Converts the generic input file into a grid of values
        /// </summary>
        /// <param name="genericData"></param>
        /// <returns></returns>
        protected Grid GenericDataToGrid(double[][] genericData, int dataColumnIndex)
        {
            double[][] genericDataGrid = { };
            double currentX = genericData[0][0];
            List<double> column = new List<double>();
            int columnIndex = 0;
            for (int i = 0; i < genericData.Length - 1; i++) {
                if (currentX != genericData[i][0]) {
                    if (columnIndex == 0) {
                        int numberOfColumns = genericData.Length / column.Count;
                        genericDataGrid = new double[numberOfColumns][];
                    }
                    genericDataGrid[columnIndex] = column.ToArray();

                    column = new List<double>();
                    currentX = genericData[i][0];
                    columnIndex++;
                }

                column.Add(genericData[i][dataColumnIndex]);
            }
            genericDataGrid[columnIndex] = column.ToArray();

            return new Grid(genericDataGrid);
        }

    }
}