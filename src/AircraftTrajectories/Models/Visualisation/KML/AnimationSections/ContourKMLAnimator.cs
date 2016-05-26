
namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{
    using TemporalGrid;
    using Contours;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Trajectory;
    using Space3D;
    using System.Windows.Forms;
    public class ContourKMLAnimator : KMLAnimatorSectionInterface
    {
        protected TemporalGrid _temporalGrid;
        protected Trajectory _trajectory;
        protected List<int> _labeledContours;
        public int NumberOfContours { get; set; }
        public int FirstContourValue { get; set; }
        public int ContourValueStep { get; set; }

        public ContourKMLAnimator(TemporalGrid temporalGrid, Trajectory trajectory, List<int> labeledContours)
        {
            _temporalGrid = temporalGrid;
            _trajectory = trajectory;
            _labeledContours = labeledContours;

            NumberOfContours = 30;
            FirstContourValue = 55;
            ContourValueStep = 1;
        }

        public string KMLSetup()
        {
            Color c1 = Color.FromArgb(0, 100, 237, 75);
            Color c2 = Color.FromArgb(150, 20, 53, 255);
            Color[] colors = interpolateColors(c1, c2, NumberOfContours);

            string contourSetup = "";
            for (int i = 1; i <= NumberOfContours; i++)
            {
                var c = colors[i - 1];
                var color = "00000000";
                if (_labeledContours.Contains(FirstContourValue + (i * ContourValueStep)))
                {
                    color = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", Math.Min(255, c.A + 150), c.R, c.G, c.B);
                }
                contourSetup += @"
<Style id='contour_style" + i + @"'>
    <LineStyle>
        <color>" + color + @"</color>
        <width>2</width>
    </LineStyle>
    <PolyStyle>
        <color>" + string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B) + @"</color>
    </PolyStyle>
    <IconStyle>
        <scale>0</scale>
    </IconStyle>
    <LabelStyle>
        <color>FFFFFFFF</color>
        <scale>0.35</scale>
    </LabelStyle>
</Style>
<Placemark id='contour_placemark" + i + @"'>
    <name>" + (FirstContourValue + (i * ContourValueStep)) + @"dB</name>
    <styleUrl>#contour_style" + i + @"</styleUrl>
    <MultiGeometry>
        <Polygon>
            <tessellate>1</tessellate>
            <outerBoundaryIs>
                <LinearRing id='contour" + i + @"'>
                <coordinates></coordinates>
                </LinearRing>
            </outerBoundaryIs>
        </Polygon>
        <Point id='contourPoint" + i + @"'>
            <Coordinates></Coordinates>
        </Point>
    </MultiGeometry>
</Placemark>
                ";
            }
            return contourSetup;
        }

        public string KMLAnimationStep(int t)
        {
            string updateStep = "";
            Grid grid = _temporalGrid.GetGrid(t);
            List<int> visibleContours = new List<int>();
            foreach (Contour contour in grid.Contours)
            {
                if (!contour.IsClosed) { continue; }

                int contourId = -1;
                for (int i = 0; i < NumberOfContours; i++)
                {
                    if (contour.Value == FirstContourValue + (i * ContourValueStep))
                    {
                        contourId = i + 1;
                    }
                }
                if (contourId == -1) { continue; }
                visibleContours.Add(contourId);

                var coordinateString = "";
                double pointLongitude = 0;
                double pointLatitude = 0;
                double smallestHeadingDeviation = 180;
                foreach (ContourPoint p in contour.Points)
                {
                    GeoPoint3D contourPoint = _temporalGrid.GridCoordinate(p.Location.X, p.Location.Y);
                    coordinateString += contourPoint.Longitude + "," + contourPoint.Latitude + ",0\n";

                    double pointHeading = contourPoint.HeadingTo(_trajectory.GeoPoint(t));
                    double desiredHeading = (_trajectory.Heading(t) + 160) % 360;
                    if (pointHeading < desiredHeading && Math.Abs(pointHeading - desiredHeading) < smallestHeadingDeviation)
                    {
                        smallestHeadingDeviation = Math.Abs(pointHeading - desiredHeading);
                        pointLongitude = contourPoint.Longitude;
                        pointLatitude = contourPoint.Latitude;
                    }
                }
                updateStep += plotUpdate("LinearRing", coordinateString, "contour" + contourId);
                // Plot point
                if (_labeledContours.Contains(FirstContourValue + (contourId * ContourValueStep)))
                {
                    updateStep += plotUpdate("Point", pointLongitude + "," + pointLatitude + ",0", "contourPoint" + contourId);
                }
            }
            for (int i = 1; i <= NumberOfContours; i++)
            {
                if (!visibleContours.Contains(i))
                {
                    updateStep += plotUpdate("LinearRing", _trajectory.Longitude(t) + "," + _trajectory.Latitude(t) + ",0", "contour" + i);
                }
            }

            return updateStep;
        }

        public string KMLFinish()
        {
            return "";
        }

        protected string plotUpdate(String type, String coordinateString, string targetId)
        {
            return @"
            <" + type + @" targetId='" + targetId + @"'>
            <coordinates>
            " + coordinateString + @"
            </coordinates>
            </" + type + @">
            ";
        }

        protected Color[] interpolateColors(Color lowerBound, Color upperBound, int numberOfIntervals)
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
    }
}
