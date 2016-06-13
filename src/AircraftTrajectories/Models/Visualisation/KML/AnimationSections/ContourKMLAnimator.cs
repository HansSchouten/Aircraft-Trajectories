
namespace AircraftTrajectories.Models.Visualisation.KML.AnimationSections
{
    using TemporalGrid;
    using Contours;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Trajectory;
    using Space3D;
    using System.Device.Location;
    public class ContourKMLAnimator : KMLAnimatorSectionInterface
    {
        protected TemporalGrid _temporalGrid;
        protected Trajectory _trajectory;
        protected List<int> _labeledContours;

        public int NumberOfContours { get; set; }
        public int FirstContourValue { get; set; }
        public int ContourValueStep { get; set; }
        public double smallestHeadingDeviation = 180;
        public double pointLongitude;
        public double pointLatitude;

        private string coordinateString = "";

        public ContourKMLAnimator(TemporalGrid temporalGrid, Trajectory trajectory, List<int> labeledContours)
        {
            _temporalGrid = temporalGrid;
            _trajectory = trajectory;
            _labeledContours = labeledContours;

            NumberOfContours = 30;
            FirstContourValue = 55;
            ContourValueStep = 1;
        }

        /// <summary>
        ///  Return a string in KML format containing all pre animation definitions 
        ///  that are required for the contour animation
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Return a string in KML format containing all updates
        /// for the contour animation at the given moment in time
        /// </summary>
        /// <returns></returns>
        public string KMLAnimationStep(int t)
        {
            string updateStep = "";
            Grid grid = _temporalGrid.GetGrid(t);
            List<int> visibleContours = new List<int>();
            foreach (Contour contour in grid.Contours)
            {
                if (!contour.IsClosed) { continue; }
                int contourId = -1;

                indexingContours(NumberOfContours, contour, contourId);

                if (contourId == -1) { continue; }
                visibleContours.Add(contourId);

                GeoPoint3D contourPoint = _temporalGrid.GridCoordinate(contour.Points[0].Location.X, contour.Points[0].Location.Y);
                double desiredHeading = (_trajectory.Heading(t) + 160) % 360;

                addUpdateContours(updateStep, t, contour, contourPoint, contourId, desiredHeading);
            }

            updateStep = plotLinearRing(updateStep, t, visibleContours);

            return updateStep;
        }

        /// <summary>
        /// Return a string in KML format containing all updates
        /// for the contour animation at the given moment in time
        /// </summary>
        /// <returns></returns>
        public string KMLFinish()
        {
            return "";
        }

        /// <summary>
        /// Plot point by adding a contour point to the KML string
        /// </summary>
        /// <param name="update"></param>
        /// <param name="t"></param>
        /// <param name="pointLatitude"></param>
        /// <param name="pointLongitude"></param>
        /// <param name="desiredHeading"></param>
        /// <param name="contourId"></param>
        /// <returns></returns>
        protected String plotPoint(String update, int t, double pointLatitude, double pointLongitude, double desiredHeading, int contourId)
        {
            var planeCoord = new GeoCoordinate(_trajectory.Latitude(t), _trajectory.Longitude(t));
            var contourCoord = new GeoCoordinate(pointLatitude, pointLongitude);
            var distance = planeCoord.GetDistanceTo(contourCoord);
            var labelPoint = _trajectory.GeoPoint(t).MoveInDirection(distance, desiredHeading);

            return update += plotUpdate("Point", labelPoint.Longitude + "," + labelPoint.Latitude + ",0", "contourPoint" + contourId);
        }

        /// <summary>
        /// Plot linear ring by extending KML string 
        /// </summary>
        /// <param name="updateStep"></param>
        /// <param name="t"></param>
        /// <param name="visibleContours"></param>
        /// <returns></returns>
        protected String plotLinearRing(String updateStep, int t, List<int> visibleContours)
        {
            for (int i = 1; i <= NumberOfContours; i++) {
                if (!visibleContours.Contains(i)) {
                    updateStep += plotUpdate("LinearRing", _trajectory.Longitude(t) + "," + _trajectory.Latitude(t) + ",0", "contour" + i);
                }
            }
            return updateStep;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="contourPoint"></param>
        /// <param name="coordinateString"></param>
        /// <param name="desiredHeading"></param>
        protected void setHeadingPoint(ContourPoint point, GeoPoint3D contourPoint, double desiredHeading, int t)
        {
            contourPoint = _temporalGrid.GridCoordinate(point.Location.X, point.Location.Y);
            coordinateString += contourPoint.Longitude + "," + contourPoint.Latitude + ",0\n";

            double pointHeading = contourPoint.HeadingTo(_trajectory.GeoPoint(t));
            if (pointHeading < desiredHeading && Math.Abs(pointHeading - desiredHeading) < smallestHeadingDeviation) {
                smallestHeadingDeviation = Math.Abs(pointHeading - desiredHeading);
                double pointLongitude = contourPoint.Longitude;
                double pointLatitude = contourPoint.Latitude;
            }
        }

        /// <summary>
        /// Adds a labeled point and linear ring for each plotted contour (with the right heading)
        /// </summary>
        /// <param name="updateStep"></param>
        /// <param name="t"></param>
        /// <param name="contour"></param>
        /// <param name="contourPoint"></param>
        /// <param name="contourId"></param>
        /// <param name="desiredHeading"></param>
        protected void addUpdateContours(String updateStep, int t, Contour contour, GeoPoint3D contourPoint, int contourId, double desiredHeading)
        {
            foreach (ContourPoint p in contour.Points) {
                setHeadingPoint(p, contourPoint, desiredHeading, t);
            }

            updateStep += plotUpdate("LinearRing", coordinateString, "contour" + contourId);

            if (_labeledContours.Contains(FirstContourValue + (contourId * ContourValueStep))) {
                updateStep = plotPoint(updateStep, t, pointLatitude, pointLongitude, desiredHeading, contourId);
            }
        }

        /// <summary>
        /// Indexes all contours in increasing order
        /// </summary>
        /// <param name="numberOfContours"></param>
        /// <param name="contour"></param>
        protected void indexingContours(int numberOfContours, Contour contour, int contourId)
        {
            for (int i = 0; i < NumberOfContours; i++) {
                if (contour.Value == FirstContourValue + (i * ContourValueStep)) {
                    contourId = i + 1;
                }
            }
        }

        /// <summary>
        /// Updates the coordinates of the contours
        /// </summary>
        /// <param name="type"></param>
        /// <param name="coordinateString"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Interpolates the colors between chosen noise values
        /// </summary>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <param name="numberOfIntervals"></param>
        /// <returns></returns>
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

            for (var i = 0; i < numberOfIntervals; i++) {
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
