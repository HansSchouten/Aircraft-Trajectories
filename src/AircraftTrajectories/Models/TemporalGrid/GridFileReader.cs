using AircraftTrajectories.Models.Space3D;
using System;
using System.Collections.Generic;
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

		public Grid createGridFromFile(string filePath)
		{
			double[][] data = readData(filePath);
			return new Grid(data, _lowerLeftCorner, _width);
		}

		/// <summary>
		/// Parses the noise values along the grid
		/// </summary>
		/// <returns></returns>
		protected double[][] readData(string filePath)
		{
			string rawGeneric = File.ReadAllText(filePath);

			double[] allData = rawGeneric
				.Split(' ')
				.Select(Convert.ToDouble)
				.ToArray();

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
			for (int i = 0; i < allData.Length; i++)
			{
				int x = i % _width;
				int y = height - 1 - (int) Math.Floor((double) i / _width);
				data[x][y] = allData[i];
			}

			return data;
		}
	}
}
