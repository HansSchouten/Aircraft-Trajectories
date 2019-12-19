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
using AircraftTrajectories.Models.Visualisation;

namespace AircraftTrajectories.Views
{
	public partial class NoiseContourForm : Form
	{
		public NoiseContourForm()
		{
			InitializeComponent();
		}

		private int _dbMin = 40;
		private int _dbMax = 60;
		private Grid _grid;
		private bool _useGradient = false;
		private Color[] _gradientColors;

		private void ContourForm_Load(object sender, EventArgs e)
		{
			// default selected contours
			for (int i = _dbMin; i <= _dbMax; i++)
			{
				lbVisibleContours.Items.Add(i + " dB");
			}
			lbVisibleContours.SetSelected(5, true);
			lbVisibleContours.SetSelected(10, true);
			lbVisibleContours.SetSelected(15, true);
			lbVisibleContours.SetSelected(20, true);

			// define noise value color gradient
			Color c1 = Color.FromArgb(20, 252, 236, 3);
			Color c2 = Color.FromArgb(150, 255, 0, 20);
			_gradientColors = InterpolateColors(c1, c2, _dbMax - _dbMin + 1);

			// ensure correct Double decimal character parsing
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
			kml.AppendLine("<?xml version='1.0' encoding='UTF-8'?>");
			kml.AppendLine("<kml xmlns='http://www.opengis.net/kml/2.2' xmlns:gx='http://www.google.com/kml/ext/2.2' xmlns:kml='http://www.opengis.net/kml/2.2' xmlns:atom='http://www.w3.org/2005/Atom'>");
			kml.AppendLine("<Document>");

			if (_useGradient) {
				kml.Append(@"
<ScreenOverlay>
<name>Legend</name>
<Icon><href>webroot/gradientImage.png</href></Icon>
<overlayXY x= ""0.03"" y= ""0.85"" xunits= ""fraction"" yunits= ""fraction"" />
<screenXY x = ""0.03"" y =""0.85"" xunits =""fraction"" yunits =""fraction""/>
<rotationXY x = ""0.5"" y = ""0.5"" xunits =""fraction"" yunits =""fraction""/>
<size x = ""0"" y = ""0"" xunits = ""pixels"" yunits = ""pixels"" />
</ScreenOverlay>

<ScreenOverlay>
<name>Legend Title</name>
<Icon><href>webroot/titleImage.png</href>
</Icon>
<overlayXY x = ""0.02"" y = ""0.94"" xunits = ""fraction"" yunits = ""fraction"" />
<screenXY x = ""0.02"" y = ""0.94"" xunits = ""fraction"" yunits = ""fraction"" />
<rotationXY x = ""0.5"" y = ""0.5"" xunits = ""fraction"" yunits = ""fraction"" />
<size x = ""0"" y = ""0"" xunits = ""pixels"" yunits = ""pixels"" />
</ScreenOverlay>
			");
			}

			// create legend
			if (_useGradient)
			{
				LegendCreator legendCreator = new LegendCreator();
				legendCreator.min = _dbMin;
				legendCreator.max = _dbMax;
				legendCreator.c1 = Color.FromArgb(200, 255, 0, 20);
				legendCreator.c2 = Color.FromArgb(200, 252, 236, 3);
				legendCreator.OutputLegendImage();
				legendCreator.OutputLegendTitle();
			}

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

				String contourKml = getContourStyle(contour, i) + @"
<Placemark id='contour_placemark" + i + @"'>
	<name>" + contour.Value + @" dB(A)</name>
	<description><![CDATA[description: <br>label: " + contour.Value + @"]]></description>
    <styleUrl>#contour_style" + i + @"</styleUrl>
	<ExtendedData>
		<Data name=""description"">
		</Data>
		<Data name=""label"">
			<value>" + contour.Value + @"</value>
		</Data>
	</ExtendedData>
    <Polygon>
        <outerBoundaryIs>
            <LinearRing id='contour" + i + @"'>
				<tessellate>1</tessellate>
                <coordinates>
				" + GetContourCoordinates(grid, contour) + @"
				</coordinates>
            </LinearRing>
        </outerBoundaryIs>
    </Polygon>
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

		private String getContourStyle(Contour contour, int id)
		{
			if (_useGradient)
			{
				Color c = _gradientColors[contour.Value - _dbMin];
				return @"
<Style id='contour_style" + id + @"'>
    <LineStyle>
		<color>" + string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", 210, c.B, c.G, c.R) + @"</color>
        <width>1</width>
    </LineStyle>
    <PolyStyle>
		<color>" + string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.B, c.G, c.R) + @"</color>
    </PolyStyle>
    <IconStyle>
        <scale>0</scale>
    </IconStyle>
    <LabelStyle>
        <color>FFFFFFFF</color>
        <scale>0.40</scale>
    </LabelStyle>
</Style>";
			}

			String lineColor = "ff2dc0fb";
			String polyColor = "4d2dc0fb";

			if (contour.Value >= 58)
			{
				lineColor = "ff5252ff";
				polyColor = "4d5252ff";
			}

			return @"
<Style id='contour_style" + id + @"_normal'>
	<LineStyle>
		<color>" + lineColor + @"</color>
		<width>1.2</width>
	</LineStyle>
	<PolyStyle>
		<color>" + polyColor + @"</color>
	</PolyStyle>
</Style>
<Style id='contour_style" + id + @"_highlight'>
	<LineStyle>
		<color>" + lineColor + @"</color>
		<width>1.8</width>
	</LineStyle>
	<PolyStyle>
		<color>" + polyColor + @"</color>
	</PolyStyle>
</Style>
<StyleMap id='contour_style" + id + @"'>
	<Pair>
		<key>normal</key>
		<styleUrl>#contour_style" + id + @"_normal</styleUrl>
	</Pair>
	<Pair>
		<key>highlight</key>
		<styleUrl>#contour_style" + id + @"_highlight</styleUrl>
	</Pair>
</StyleMap>";
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
