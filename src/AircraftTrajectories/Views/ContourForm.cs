using AircraftTrajectories.Models.Contours;
using AircraftTrajectories.Models.Space3D;
using AircraftTrajectories.Models.TemporalGrid;
using System;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace AircraftTrajectories.Views
{
	public partial class ContourForm : Form
	{
		public ContourForm()
		{
			InitializeComponent();
		}

		private int _dbMin = 30;
		private int _dbMax = 60;
		private Grid _grid;

		private void ContourForm_Load(object sender, EventArgs e)
		{
			for (int i = _dbMin; i <= _dbMax; i++)
			{
				lbVisibleContours.Items.Add(i + " dB");
			}
			lbVisibleContours.SetSelected(15, true);
			lbVisibleContours.SetSelected(20, true);
			lbVisibleContours.SetSelected(25, true);
			lbVisibleContours.SetSelected(30, true);

			Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
		}

		private void btnReadNoiseFile_Click(object sender, EventArgs e)
		{
			btnReadNoiseFile.Enabled = false;

			if (tbInputFile.Text.Length == 0)
			{
				showFileDialog();
			}

			Point3D lowerLeftCorner = new Point3D((int)nudLowerLeftX.Value, (int)nudLowerLeftY.Value);
			int cellSize = (int)nudCellSize.Value;
			int gridHorizontalValueCount = (int)nudGridWidth.Value;
			GridFileReader reader = new GridFileReader(lowerLeftCorner, cellSize, gridHorizontalValueCount);

			_grid = reader.createGridFromFile(tbInputFile.Text, (int) nudSkipLinesCount.Value);

			MessageBox.Show("Noise data read from file", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

			btnReadNoiseFile.Enabled = true;
		}

		private void btnGenerateKMLFile_Click(object sender, EventArgs e)
		{
			btnGenerateKMLFile.Enabled = false;

			CreateContoursKML(_grid);
			MessageBox.Show("Contours KML has been generated", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

			btnGenerateKMLFile.Enabled = true;
		}

		private void CreateContoursKML(Grid grid)
		{
			StringBuilder kml = new StringBuilder();
			kml.AppendLine("<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\">");
			kml.AppendLine("<Document>");

			// define noise value color gradient
			Color c1 = Color.FromArgb(0, 20, 240, 0);
			Color c2 = Color.FromArgb(150, 20, 0, 255);
			Color[] colors = InterpolateColors(c1, c2, _dbMax - _dbMin + 1);

			// define which noise value contours to visualise
			List<int> visualisedContours = new List<int>();
			for (int x = 0; x < lbVisibleContours.Items.Count; x++)
			{
				if (lbVisibleContours.GetSelected(x) == true)
				{
					visualisedContours.Add(_dbMin + x);
				}
			}

			int i = 0;
			foreach (Contour contour in grid.Contours)
			{
				if (!visualisedContours.Contains(contour.Value)) { continue; }
				i++;
				var c = colors[contour.Value - _dbMin];

				String coordinates = GetContourCoordinates(grid, contour);

				String contourKml = @"
<Style id='contour_style" + i + @"'>
    <LineStyle>";
				contourKml += "<color>" + string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", Math.Min(255, c.A + 150), c.R, c.G, c.B) + "</color>";
				contourKml += @"
        <width>2</width>
    </LineStyle>
    <PolyStyle>";
				contourKml += "<color>" + string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B) + "</color>";
				contourKml += @"
    </PolyStyle>
    <IconStyle>
        <scale>0</scale>
    </IconStyle>
    <LabelStyle>
        <color>FFFFFFFF</color>
        <scale>0.40</scale>
    </LabelStyle>
</Style>
<Placemark id='contour_placemark" + i + @"'>
	<name>" + contour.Value + "dB</name>" + @"
    <styleUrl>#contour_style" + i + @"</styleUrl>
    <MultiGeometry>
        <Polygon>
            <tessellate>1</tessellate>
            <outerBoundaryIs>
                <LinearRing id='contour" + i + @"'>
                    <coordinates>
					" + coordinates + @"
					</coordinates>
                </LinearRing>
            </outerBoundaryIs>
        </Polygon>
    </MultiGeometry>
</Placemark>";
				kml.Append(contourKml);
			}

			kml.AppendLine("</Document>");
			kml.AppendLine("</kml>");

			// write contours to kml file
			String filepath = "contours.kml";
			System.IO.StreamWriter file = new System.IO.StreamWriter(filepath);
			file.Write(kml.ToString());
			file.Close();
		}

		private String GetContourCoordinates(Grid grid, Contour contour)
		{
			var coordinateString = "";
			GeoPoint3D firstContourPoint = grid.GridGeoCoordinate(contour.Points[0].Location.X, contour.Points[0].Location.Y);
			GeoPoint3D contourPoint = firstContourPoint;

			foreach (ContourPoint p in contour.Points)
			{
				contourPoint = grid.GridGeoCoordinate(p.Location.X, p.Location.Y);
				coordinateString += contourPoint.Longitude + "," + contourPoint.Latitude + ",";
				coordinateString += "0\n";
			}
			if (!contour.IsClosed)
			{
				coordinateString += firstContourPoint.Longitude + "," + firstContourPoint.Latitude + ",";
				coordinateString += "0\n";
			}

			return coordinateString;
		}

		/// <summary>
		/// Interpolates the colors between chosen noise values
		/// </summary>
		/// <param name="lowerBound"></param>
		/// <param name="upperBound"></param>
		/// <param name="numberOfIntervals"></param>
		/// <returns></returns>
		private Color[] InterpolateColors(Color lowerBound, Color upperBound, int numberOfIntervals)
		{
			Color[] colorPalette = new Color[numberOfIntervals];

			int interval_A = (upperBound.A - lowerBound.A) / numberOfIntervals;
			int interval_R = (upperBound.R - lowerBound.R) / numberOfIntervals;
			int interval_G = (upperBound.G - lowerBound.G) / numberOfIntervals;
			int interval_B = (upperBound.B - lowerBound.B) / numberOfIntervals;

			int current_A = lowerBound.A;
			int current_R = lowerBound.R;
			int current_G = lowerBound.G;
			int current_B = lowerBound.B;

			for (var i = 0; i < numberOfIntervals; i++)
			{
				colorPalette[i] = Color.FromArgb(current_A, current_R, current_G, current_B);

				current_A += interval_A;
				current_R += interval_R;
				current_G += interval_G;
				current_B += interval_B;
			}

			return colorPalette;
		}

		private void tbInputFile_Click(object sender, EventArgs e)
		{
			showFileDialog();
		}

		private void showFileDialog()
		{
			if (ofdNoiseFile.ShowDialog() == DialogResult.OK)
			{
				tbInputFile.Text = ofdNoiseFile.FileName;
			}
		}

		private void ContourForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Application.Exit();
		}
	}
}
