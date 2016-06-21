using AircraftTrajectories.Models.Space3D;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AircraftTrajectories.Models.TemporalGrid
{
    public class TemporalGridFileReader
    {

        public TemporalGrid createTemporalGridFromFile(string filePath)
        {
            double[][] genericData = readGenericData(filePath);
            TemporalGrid temporalGrid = new TemporalGrid();

            for (int i = 0; i < genericData[0].Length - 3; i++)
            {
                temporalGrid.AddGrid(GenericDataToGrid(genericData, i));
            }

            return temporalGrid;
        }

        /// <summary>
        /// Parses the generic input values 
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
            // Store generic input in a 2D grid
            double[][] genericDataGrid = { };
            double currentX = genericData[0][0];
            List<double> column = new List<double>();
            int columnIndex = 0;
            for (int i = 0; i < genericData.Length - 1; i++)
            {
                // Check whether we encountered a new column
                if (currentX != genericData[i][0])
                {
                    // Check whether this was the first column
                    if (columnIndex == 0)
                    {
                        // Now the total number of columns of the grid is known
                        int numberOfColumns = genericData.Length / column.Count;
                        genericDataGrid = new double[numberOfColumns][];
                    }
                    // Add the column to the grid
                    genericDataGrid[columnIndex] = column.ToArray();

                    column = new List<double>();
                    currentX = genericData[i][0];
                    columnIndex++;
                }

                column.Add(genericData[i][dataColumnIndex]);
            }
            genericDataGrid[columnIndex] = column.ToArray();

            Point3D lowerLeftCorner = new Point3D(genericData[0][0], genericData[0][1]);
            return new Grid(genericDataGrid, lowerLeftCorner);
        }

    }
}
