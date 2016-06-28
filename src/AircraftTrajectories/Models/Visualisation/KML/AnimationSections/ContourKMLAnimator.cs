
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
    using System.Linq;
    using System.Windows.Forms;
    public class ContourKMLAnimator : KMLAnimatorSectionInterface
    {
        protected TemporalGrid _temporalGrid;
        protected Trajectory _trajectory;
        protected List<int> _gradientContours;
        protected List<int> _highlightedContours;
        public bool AltitudeOffset = false;
        public int LowestContourValue { get; set; }
        public int HighestContourValue { get; set; }
        public int ContourValueStep { get; set; }
        protected int _numberOfContours { get; set; }


        public ContourKMLAnimator(TemporalGrid temporalGrid, Trajectory trajectory = null, List<int> highlightedContours = null)
        {
            LowestContourValue = int.MaxValue;
            HighestContourValue = int.MinValue;
            _temporalGrid = temporalGrid;
            _trajectory = trajectory;

            _gradientContours = new List<int>();
            _highlightedContours = (highlightedContours == null) ? new List<int>() : highlightedContours;
            if (_highlightedContours.Count > 0)
            {
                LowestContourValue = _highlightedContours.Min();
                HighestContourValue = _highlightedContours.Max();
            }
        }

        public void SetGradientSettings(int lowestGradientValue, int highestGradientValue, int step)
        {
            ContourValueStep = step;

            for (int value = lowestGradientValue; value <= highestGradientValue; value += step)
            {
                _gradientContours.Add(value);
            }

            HighestContourValue = Math.Max(HighestContourValue, highestGradientValue);
            LowestContourValue = Math.Min(LowestContourValue, lowestGradientValue);
        }

        Dictionary<int, int> maxNumberOfOccurrences;
        protected void DetermineNumberOfContoursPerValue()
        {
            maxNumberOfOccurrences = new Dictionary<int, int>();

            for (int t = 0; t < _temporalGrid.GetNumberOfGrids(); t++)
            {
                // Count current number of occurrences per contour
                Dictionary<int, int> numberOfContours = new Dictionary<int, int>();
                Grid grid = _temporalGrid.GetGrid(t);
                foreach (Contour contour in grid.Contours)
                {
                    int occurences;
                    numberOfContours.TryGetValue(contour.Value, out occurences);
                    numberOfContours[contour.Value] = occurences + 1;
                }
                // Updates max number of occurrences per contour
                foreach (int value in numberOfContours.Keys)
                {
                    int previousMax;
                    maxNumberOfOccurrences.TryGetValue(value, out previousMax);
                    maxNumberOfOccurrences[value] = Math.Max(numberOfContours[value], previousMax);
                }
            }
        }


        /// <summary>
        ///  Return a string in KML format containing all pre animation definitions 
        ///  that are required for the contour animation
        /// </summary>
        /// <returns></returns>
        public string KMLSetup()
        {
            if (_gradientContours.Count == 0 && _highlightedContours.Count == 0) { return ""; }
            _numberOfContours = HighestContourValue - LowestContourValue + 1;
            
            DetermineNumberOfContoursPerValue();

            // Define contour colors
            Color c1 = Color.FromArgb(0, 20, 240, 0);
            Color c2 = Color.FromArgb(150, 20, 0, 255);
            Color[] colors = interpolateColors(c1, c2, _numberOfContours);
            
            string contourSetup = @"
<Folder> 
 <open>0</open>
 <name>Contours</name>
            ";
            for (int i = 0; i < _numberOfContours; i++)
            {
                int value = LowestContourValue + i;
                if (!_highlightedContours.Contains(value) && !_gradientContours.Contains(value)) { continue; }

                int occurrences;
                maxNumberOfOccurrences.TryGetValue(value, out occurrences);
                if (occurrences == 0) { continue;  }

                var c = colors[i];
                    
                string altitudeMode = (AltitudeOffset) ? "<altitudeMode>absolute</altitudeMode>" : "";
                contourSetup += @"
<Style id='contour_style" + i + @"'>
    <LineStyle>";
                if (!_highlightedContours.Contains(value))
                {
                    contourSetup += "<color>00000000</color>";
                }
                else
                {
                    contourSetup += "<color>" + string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", Math.Min(255, c.A + 150), c.R, c.G, c.B) + "</color>";
                }
                contourSetup += @"
        <width>2</width>
    </LineStyle>
    <PolyStyle>";
                if (_gradientContours.Contains(value))
                {
                    contourSetup += "<color>" + string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", c.A, c.R, c.G, c.B) + "</color>";
                }
                else 
                {
                    contourSetup += "<color>00000000</color>";
                }
                contourSetup += @"
    </PolyStyle>
    <IconStyle>
        <scale>0</scale>
    </IconStyle>
    <LabelStyle>
        <color>FFFFFFFF</color>
        <scale>0.40</scale>
    </LabelStyle>
</Style>
                ";
                for (int o = 0; o < occurrences; o++)
                {
                    contourSetup += @"
<Placemark id='contour_placemark" + i+"_"+o + @"'>";
                    contourSetup += "<name>" + value + "dB</name>" + @"
    <styleUrl>#contour_style" + i + @"</styleUrl>
    <MultiGeometry>
        <Polygon>
            " + altitudeMode + @"
            <tessellate>1</tessellate>
            <outerBoundaryIs>
                <LinearRing id='contour" + i+"_"+o + @"'>
                    <coordinates></coordinates>
                </LinearRing>
            </outerBoundaryIs>
        </Polygon>
        <Point id='contourPoint" + i+"_"+o + @"'>
            <Coordinates></Coordinates>
        </Point>
    </MultiGeometry>
</Placemark>
                    ";
                }
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
            if (_gradientContours.Count == 0 && _highlightedContours.Count == 0) { return ""; }

            string updateStep = "";
            Grid grid = _temporalGrid.GetGrid(t);
            List<string> visibleContours = new List<string>();
            Dictionary<int, int> occurrenceIndexes = new Dictionary<int, int>();

            foreach (Contour contour in grid.Contours)
            {
                int value = contour.Value;
                //if (!contour.IsClosed) { continue; }
                if (!_highlightedContours.Contains(value) && !_gradientContours.Contains(value)) { continue; }

                string contourId = "";
                for (int i = 0; i < _numberOfContours; i++)
                {
                    if (value == LowestContourValue + i)
                    {
                        contourId = i+"_";
                    }
                }
                int o;
                if(!occurrenceIndexes.TryGetValue(value, out o))
                {
                    o = 0;
                } else
                {
                    o++;
                }
                occurrenceIndexes[value] = o;
                contourId = contourId + o;
                

                visibleContours.Add(contourId);

                // Plot Contour
                var coordinateString = "";
                GeoPoint3D firstContourPoint = grid.GridGeoCoordinate(contour.Points[0].Location.X, contour.Points[0].Location.Y);
                GeoPoint3D contourPoint = firstContourPoint;

                foreach (ContourPoint p in contour.Points)
                {
                    contourPoint = grid.GridGeoCoordinate(p.Location.X, p.Location.Y);
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
                if (_trajectory == null || !_highlightedContours.Contains(value)) { continue; }
                double pointLongitude = contourPoint.Longitude;
                double pointLatitude = contourPoint.Latitude;
                double smallestHeadingDeviation = 180;
                double desiredHeading = (_trajectory.Heading(t) + 160) % 360;
                foreach (ContourPoint p in contour.Points)
                {
                    contourPoint = grid.GridGeoCoordinate(p.Location.X, p.Location.Y);
                    double pointHeading = contourPoint.HeadingTo(_trajectory.GeoPoint(t));
                    if (pointHeading < desiredHeading && Math.Abs(pointHeading - desiredHeading) < smallestHeadingDeviation)
                    {
                        smallestHeadingDeviation = Math.Abs(pointHeading - desiredHeading);
                        pointLongitude = contourPoint.Longitude;
                        pointLatitude = contourPoint.Latitude;
                    }
                }
                if (_highlightedContours.Contains(contour.Value))
                {
                    var planeCoord = new GeoCoordinate(_trajectory.Latitude(t), _trajectory.Longitude(t));
                    var contourCoord = new GeoCoordinate(pointLatitude, pointLongitude);
                    var distance = planeCoord.GetDistanceTo(contourCoord);
                    var labelPoint = _trajectory.GeoPoint(t).MoveInDirection(distance, desiredHeading);
                    updateStep += plotUpdate("Point", labelPoint.Longitude + "," + labelPoint.Latitude + ",0", "contourPoint" + contourId);
                }
            }
            /*
            for (int i = 0; i < _numberOfContours; i++)
            {
                int value = LowestContourValue + i;
                if (!_highlightedContours.Contains(value) && !_gradientContours.Contains(value)) { continue; }
                //foreach ()
                if (!visibleContours.Contains(i))
                {
                    string longLat = (_trajectory == null) ? "0,0," : _trajectory.Longitude(t) + "," + _trajectory.Latitude(t);
                    updateStep += plotUpdate("LinearRing", longLat + ",0", "contour" + i);
                }
            }
            */
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
