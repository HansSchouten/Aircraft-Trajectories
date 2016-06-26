
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
        protected List<double> _highlightedContours;
        public bool AltitudeOffset = false;
        public int NumberOfContours { get; set; }
        public int FirstContourValue { get; set; }
        public int ContourValueStep { get; set; }
        public bool ShowOnlyHighlightedContours = false;


        public ContourKMLAnimator(TemporalGrid temporalGrid, Trajectory trajectory = null, List<double> highlightedContours = null)
        {
            _temporalGrid = temporalGrid;
            _trajectory = trajectory;
            _highlightedContours = highlightedContours;
            
            NumberOfContours = 40;
            FirstContourValue = 35;
            ContourValueStep = 1;
        }

        /// <summary>
        ///  Return a string in KML format containing all pre animation definitions 
        ///  that are required for the contour animation
        /// </summary>
        /// <returns></returns>
        public string KMLSetup()
        {
            Color c1 = Color.FromArgb(0, 20, 240, 0);
            Color c2 = Color.FromArgb(150, 20, 0, 255);
            Color[] colors = interpolateColors(c1, c2, NumberOfContours);

            string contourSetup = @"
<Folder> 
 <open>0</open>
 <name>Contours</name>
            ";
            for (int i = 1; i <= NumberOfContours; i++)
            {
                var c = colors[i - 1];
                var color = "00000000";
                if (_highlightedContours != null && _highlightedContours.Contains(FirstContourValue + (i * ContourValueStep)))
                {
                    color = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", Math.Min(255, c.A + 150), c.R, c.G, c.B);
                }
                string altitudeMode = (AltitudeOffset) ? "<altitudeMode>absolute</altitudeMode>" : "";
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
        <scale>0.40</scale>
    </LabelStyle>
</Style>
<Placemark id='contour_placemark" + i + @"'>
    <name>" + (FirstContourValue + (i * ContourValueStep)) + @"dB</name>
    <styleUrl>#contour_style" + i + @"</styleUrl>
    <MultiGeometry>
        <Polygon>
            " + altitudeMode + @"
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
            contourSetup += "</Folder>";
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
                //if (!contour.IsClosed) { continue; }
                if (ShowOnlyHighlightedContours && !_highlightedContours.Contains(contour.Value)) { continue; }

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


                // Plot Contour
                var coordinateString = "";
                GeoPoint3D firstContourPoint = grid.GridCoordinate(contour.Points[0].Location.X, contour.Points[0].Location.Y);
                GeoPoint3D contourPoint = firstContourPoint;
                foreach (ContourPoint p in contour.Points)
                {
                    contourPoint = grid.GridCoordinate(p.Location.X, p.Location.Y);
                    coordinateString += contourPoint.Longitude + "," + contourPoint.Latitude + ",";
                    coordinateString += (AltitudeOffset) ? "50\n" : "0\n";
                }
                if (!contour.IsClosed)
                {
                    coordinateString += firstContourPoint.Longitude + "," + firstContourPoint.Latitude + ",";
                    coordinateString += (AltitudeOffset) ? "50\n" : "0\n";
                }
                updateStep += plotUpdate("LinearRing", coordinateString, "contour" + contourId);


                // Plot Labels
                if (_trajectory == null) { continue; }
                double pointLongitude = contourPoint.Longitude;
                double pointLatitude = contourPoint.Latitude;
                double smallestHeadingDeviation = 180;
                double desiredHeading = (_trajectory.Heading(t) + 160) % 360;
                foreach (ContourPoint p in contour.Points)
                {
                    contourPoint = grid.GridCoordinate(p.Location.X, p.Location.Y);
                    double pointHeading = contourPoint.HeadingTo(_trajectory.GeoPoint(t));
                    if (pointHeading < desiredHeading && Math.Abs(pointHeading - desiredHeading) < smallestHeadingDeviation)
                    {
                        smallestHeadingDeviation = Math.Abs(pointHeading - desiredHeading);
                        pointLongitude = contourPoint.Longitude;
                        pointLatitude = contourPoint.Latitude;
                    }
                }
                if (_highlightedContours.Contains(FirstContourValue + (contourId * ContourValueStep)))
                {
                    var planeCoord = new GeoCoordinate(_trajectory.Latitude(t), _trajectory.Longitude(t));
                    var contourCoord = new GeoCoordinate(pointLatitude, pointLongitude);
                    var distance = planeCoord.GetDistanceTo(contourCoord);
                    var labelPoint = _trajectory.GeoPoint(t).MoveInDirection(distance, desiredHeading);
                    updateStep += plotUpdate("Point", labelPoint.Longitude + "," + labelPoint.Latitude + ",0", "contourPoint" + contourId);
                }
            }

            for (int i = 1; i <= NumberOfContours; i++)
            {
                if (!visibleContours.Contains(i))
                {
                    string longLat = (_trajectory == null) ? "0,0," : _trajectory.Longitude(t) + "," + _trajectory.Latitude(t);
                    updateStep += plotUpdate("LinearRing", longLat + ",0", "contour" + i);
                }
            }
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
