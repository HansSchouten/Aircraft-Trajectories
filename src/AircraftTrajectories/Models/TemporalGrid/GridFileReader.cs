using AircraftTrajectories.Models.Space3D;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace AircraftTrajectories.Models.TemporalGrid
{
	class GridFileReader
	{
		protected Point3D _lowerLeftCorner;
		protected int _cellSize;
		protected int _width;

		public GridFileReader(Point3D lowerLeftCorner, int cellSize, int width)
		{
			_lowerLeftCorner = lowerLeftCorner;
			_cellSize = cellSize;
			_width = width;
		}

		public Grid createGridFromFile(string filePath, int skipLinesCount = 0)
		{
			double[][] data = readData(filePath, skipLinesCount);
			return new Grid(data, _lowerLeftCorner, _cellSize);
		}

		/// <summary>
		/// Parses the noise values along the grid
		/// </summary>
		/// <returns></returns>
		protected double[][] readData(string filePath, int skipLinesCount)
		{
			string rawGeneric = File.ReadAllText(filePath);

			double[][] doubleArray = rawGeneric
				.Split('\n')
				.Skip(skipLinesCount)
				.Select(q =>
					q.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
				     .Select(Convert.ToDouble)
					 .ToArray()
				)
				.ToArray();

			int total = 0;
			for (int c = 0; c < doubleArray.Length; c++)
			{
				for (int r = 0; r < doubleArray[c].Length; r++)
				{
					total++;
				}
			}

			double[] allData = new double[total];
			int i = 0;
			for (int c = 0; c < doubleArray.Length; c++)
			{
				for (int r = 0; r < doubleArray[c].Length; r++)
				{
					allData[i++] = doubleArray[c][r];
				}
			}

			// create empty grid
			int height = allData.Length / _width;
			double[][] data = new double[_width][];
			for (int x = 0; x < _width; x++)
			{
				double[] column = new double[height];
				for (int y = 0; y < height; y++)
				{
					column[y] = 0;
				}
				data[x] = column;
			}

			// fill grid with noise values
			for (i = 0; i < allData.Length; i++)
			{
				int x = i % _width;
				int y = height - 1 - (int) Math.Floor((double) i / _width);
				data[x][y] = allData[i];
			}

			return data;
		}
	}
}
