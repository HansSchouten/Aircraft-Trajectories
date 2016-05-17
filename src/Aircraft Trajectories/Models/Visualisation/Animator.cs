using System;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace AircraftTrajectories.Models.Visualisation
{
    using Trajectory;
    using TemporalGrid;
    using AircraftTrajectories.Models.Space3D;
    using AircraftTrajectories.Models.Contours;
    using System.Device.Location;
    using System.Text;
    using Views;
    class Animator
    {
        protected string _currentFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        protected Trajectory _trajectory;
        protected Aircraft _aircraft;
        protected TemporalGrid _temporalGrid;
        public string kmlString { get; set; }

        public Animator(Trajectory trajectory, Aircraft aircraft, TemporalGrid temporalGrid)
        {
            _trajectory = trajectory;
            _aircraft = aircraft;
            _temporalGrid = temporalGrid;
        }


        public int startDBValue = 55;
        public int stepDBValue = 1;
        public void createAnimationKML()
        {
            GeoPoint3D startLocation = _trajectory.GeoPoint(0);
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };
            StringBuilder builder = new StringBuilder();
            XmlWriter kml = XmlWriter.Create(builder, xmlWriterSettings);

            kml.WriteRaw("<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\">");

            kml.WriteStartElement("Document");
            kml.WriteStartElement("Placemark");
            kml.WriteElementString("name", "Aircraft Animation");
            kml.WriteElementString("visibility", "1");
            kml.WriteStartElement("Model");
            kml.WriteAttributeString("id", "model");
            kml.WriteElementString("altitudeMode", "absolute");
            kml.WriteStartElement("Location");
            kml.WriteAttributeString("id", "model_location");
            kml.WriteElementString("latitude", startLocation.Latitude.ToString());
            kml.WriteElementString("longitude", startLocation.Longitude.ToString());
            kml.WriteElementString("altitude", startLocation.Z.ToString());
            kml.WriteEndElement();
            kml.WriteStartElement("Orientation");
            kml.WriteAttributeString("id", "model_orientation");
            kml.WriteElementString("heading", getHeading(0, 1).ToString());
            kml.WriteElementString("tilt", "0.0");
            kml.WriteElementString("roll", "0.0");
            kml.WriteEndElement();
            kml.WriteStartElement("Scale");
            kml.WriteAttributeString("id", "model_scale");
            kml.WriteElementString("x", "3.5");
            kml.WriteElementString("y", "3.5");
            kml.WriteElementString("z", "3.5");
            kml.WriteEndElement();
            kml.WriteStartElement("Link");
            kml.WriteElementString("href", "B738.dae");
            kml.WriteEndElement();
            kml.WriteEndElement();
            kml.WriteEndElement();


            int numberOfContours = 30;
            Color c1 = Color.FromArgb(0, 100, 237, 75);
            Color c2 = Color.FromArgb(150, 20, 53, 255);
            Color[] colors = interpolateColors(c1, c2, numberOfContours);
            int[] contoursOfInterest = { 65, 70, 75 };
            for (int i = 1; i <= numberOfContours; i++)
            {
                var c = colors[i - 1];
                var color = "00000000";
                if (contoursOfInterest.Contains(startDBValue + (i * stepDBValue)))
                {
                    color = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", Math.Min(255, c.A + 150), c.R, c.G, c.B);
                }
                kml.WriteRaw(@"
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
                  <name>" + (startDBValue + (i * stepDBValue)) + @"dB</name>
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

                ");
            }

            kml.WriteRaw(@"
                <Style id='plotair_style'>
                  <LineStyle>
                    <color>60F0B414</color>
                    <width>0</width>
                  </LineStyle>
                  <PolyStyle>
                    <color>60F0B414</color>
                    <colorMode>normal</colorMode>
                    <fill>1</fill>
                  </PolyStyle>
                </Style>
                <Placemark id='plotair_placemark'>
                    <name>Plotair</name>
                    <styleUrl>#plotair_style</styleUrl>
                    <LineString id='plotair'>
                        <extrude>1</extrude>
                        <tesselate>1</tesselate>
                        <altitudeMode>absolute</altitudeMode>
                        <coordinates>
                        </coordinates>
                    </LineString>
                </Placemark>
                    ");

            kml.WriteRaw("<gx:Tour><name>Flight</name><gx:Playlist>");

            //MessageBox.Show(totalDuration.ToString());
            Console.WriteLine(_trajectory.Duration);
            string plotAirCoordinates = "";
            string plotGroundCoordinates = "";
            for (int t = 0; t < _trajectory.Duration; t++)
            {
                Grid grid = _temporalGrid.GetGrid(t);
                GeoPoint3D aircraft = _trajectory.GeoPoint(t);

                Console.WriteLine(t);
                var heading = getHeading(t, t + 1);
                var tilt = getTilt(t, t + 1);
                double bankAngle = 0;
                if (t > 1)
                {
                    bankAngle = getBankAngle(t - 1, t, t + 1);
                }

                var cameraLat = aircraft.Latitude;
                var cameraLong = aircraft.Longitude;
                var cameraAlt = aircraft.Z + 350;
                var cameraHeading = (heading - 40) % 360;
                var cameraTilt = 75;

                var R = 6378.1;
                var brng = cameraHeading * Math.PI / 180;   //Bearing converted to radians.
                var d = -1.6;                               //Distance in km
                var lat1 = aircraft.Latitude * Math.PI / 180;      //Current lat point converted to radians
                var lon1 = aircraft.Longitude * Math.PI / 180;     //Current long point converted to radians

                var lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(d / R) +
                     Math.Cos(lat1) * Math.Sin(d / R) * Math.Cos(brng));
                var lon2 = lon1 + Math.Atan2(Math.Sin(brng) * Math.Sin(d / R) * Math.Cos(lat1),
                             Math.Cos(d / R) - Math.Sin(lat1) * Math.Sin(lat2));

                cameraLat = lat2 / (Math.PI / 180);
                cameraLong = lon2 / (Math.PI / 180);

                kml.WriteRaw(@"			
                <gx:AnimatedUpdate>
                   <gx:duration>1.0</gx:duration>
                   <Update>
                      <Change>
                         <Location targetId='model_location'>
                            <latitude>" + aircraft.Latitude + @"</latitude>
                            <longitude>" + aircraft.Longitude + @"</longitude>
                            <altitude>" + aircraft.Z + @"</altitude>
                         </Location>
                      </Change>
                   </Update>
                </gx:AnimatedUpdate>			
                <gx:AnimatedUpdate>
                   <gx:duration>1.0</gx:duration>
                   <Update>
                      <Change>
                         <Orientation targetId='model_orientation'>
                            <heading>" + heading + @"</heading>
                            <tilt>" + tilt + @"</tilt>
                            <roll>" + bankAngle + @"</roll>
                         </Orientation>
                      </Change>
                   </Update>
                </gx:AnimatedUpdate>
                <gx:FlyTo>
                   <gx:duration>1.0</gx:duration>
                   <gx:flyToMode>smooth</gx:flyToMode>
                   <LookAt>
                      <latitude>" + cameraLat + @"</latitude>
                      <longitude>" + cameraLong + @"</longitude>
                      <altitude>" + cameraAlt + @"</altitude>
                      <altitudeMode>absolute</altitudeMode>
                      <heading>" + cameraHeading + @"</heading>
                      <tilt>" + cameraTilt + @"</tilt>
                   </LookAt>
                </gx:FlyTo>
                    ");

                plotAirCoordinates += aircraft.Longitude + "," + aircraft.Latitude + "," + aircraft.Z + "\n";
                plotUpdate(kml, "LineString", plotAirCoordinates, "plotair");

                plotGroundCoordinates += aircraft.Longitude + "," + aircraft.Latitude + "," + 0 + "\n";
                //plotUpdate(kml, "LineString", plotGroundCoordinates, "plotground");

                RDToGeographic converter = new RDToGeographic();
                List<int> visibleContours = new List<int>();
                foreach (Contour contour in grid.Contours)
                {
                    if (!contour.IsClosed) { continue; }

                    int contourId = -1;
                    for (int i = 0; i < numberOfContours; i++)
                    {
                        if (contour.Value == startDBValue + i * stepDBValue)
                        {
                            contourId = i + 1;
                        }
                    }
                    if (contourId == -1) { continue; }
                    visibleContours.Add(contourId);

                    var coordinateString = "";
                    double pointX = 0;
                    double pointY = 0;
                    double smallestHeadingDeviation = 180;
                    foreach (ContourPoint p in contour.Points)
                    {
                        double x = (104062 + (p.Location.X * 125));
                        double y = (475470 + (p.Location.Y * 125));

                        PointF contourlatLong = converter.convertToLatLong(x, y);
                        coordinateString += contourlatLong.X + "," + contourlatLong.Y + ",0\n";

                        double pointHeading = getHeadingBetweenPoints(contourlatLong, new PointF((float)_trajectory.Longitude(t), (float)_trajectory.Latitude(t)));
                        double desiredHeading = (heading + 160) % 360;
                        if (pointHeading < desiredHeading && Math.Abs(pointHeading - desiredHeading) < smallestHeadingDeviation)
                        {
                            smallestHeadingDeviation = Math.Abs(pointHeading - desiredHeading);
                            pointX = contourlatLong.X;
                            pointY = contourlatLong.Y;
                        }
                    }
                    plotUpdate(kml, "LinearRing", coordinateString, "contour" + contourId);
                    // Plot point
                    if (contoursOfInterest.Contains(startDBValue + (contourId * stepDBValue)))
                    {
                        plotUpdate(kml, "Point", pointX + "," + pointY + ",0", "contourPoint" + contourId);
                    }
                }
                for (int i = 1; i <= numberOfContours; i++)
                {
                    if (!visibleContours.Contains(i))
                    {
                        plotUpdate(kml, "LinearRing", aircraft.Longitude + "," + aircraft.Latitude + ",0", "contour" + i);
                    }
                }
            }

            kml.WriteRaw("</gx:Playlist></gx:Tour>");


            kml.WriteRaw(@"
                <Style id='plotground_style'>
			        <LineStyle>
				        <color>30F0BE14</color>
				        <gx:physicalWidth>300</gx:physicalWidth>
				        <gx:outerColor>25FFFFFF</gx:outerColor>
				        <gx:outerWidth>0.95</gx:outerWidth>
			        </LineStyle>
                </Style> 
                <Placemark id='plotground_placemark'>
                    <name>Plotground</name>
                    <styleUrl>#plotground_style</styleUrl>
                    <LineString id='plotground'>
                        <extrude>0</extrude>
                        <tesselate>0</tesselate>
                        <altitudeMode>absolute</altitudeMode>
                        <coordinates>
                        " + plotGroundCoordinates + @"
                        </coordinates>
                    </LineString>
                </Placemark>
                ");

            kml.WriteEndElement();

            kml.WriteRaw("</kml>");
            kml.Close();

            kmlString = builder.ToString();
        }

        void plotUpdate(XmlWriter kml, String type, String coordinateString, string targetId)
        {
            kml.WriteRaw(@"
                <gx:AnimatedUpdate>
                   <gx:duration>1.0</gx:duration>
                   <Update>
                      <Change>
                         <" + type + @" targetId='" + targetId + @"'>
                            <coordinates>
                            " + coordinateString + @"
                            </coordinates>
                         </" + type + @">
                      </Change>
                   </Update>
                </gx:AnimatedUpdate>
                    ");

        }

        double getBankAngle(double t1, double t2, double t3)
        {
            //http://www.regentsprep.org/regents/math/geometry/gcg6/RCir.htm
            GeoPoint3D point1 = _trajectory.GeoPoint(t1);
            GeoPoint3D point2 = _trajectory.GeoPoint(t2);
            GeoPoint3D point3 = _trajectory.GeoPoint(t3);

            double m_r = (point2.Longitude - point1.Longitude) / (point2.Latitude - point1.Latitude);
            double m_t = (point3.Longitude - point2.Longitude) / (point3.Latitude - point2.Latitude);
            double x_c = (m_r * m_t * (point3.Longitude - point1.Longitude) + m_r * (point2.Latitude + point3.Latitude) - m_t * (point1.Latitude + point2.Latitude)) / (2 * (m_r - m_t));
            double y_c = -(1 / m_r) * (x_c - ((point1.Latitude + point2.Latitude) / 2)) + ((point1.Longitude + point2.Longitude) / 2);

            if (double.IsInfinity(x_c))
            {
                return 0;
            }

            GeoCoordinate c1 = new GeoCoordinate(point1.Latitude, point1.Longitude);
            GeoCoordinate centroid = new GeoCoordinate(x_c, y_c);
            double radius = c1.GetDistanceTo(centroid);

            double TAS = (200 * 0.514);
            double g = 9.81;
            return Math.Atan(((TAS * TAS) / radius) / g) * (180 / Math.PI);
        }

        double getHeadingBetweenPoints(PointF point1, PointF point2)
        {
            return (DegreeBearing(point1.Y, point1.X, point2.Y, point2.X) + 180) % 360;
        }

        double getHeading(double t1, double t2)
        {
            GeoPoint3D point1 = _trajectory.GeoPoint(t1);
            GeoPoint3D point2 = _trajectory.GeoPoint(t2);
            return (DegreeBearing(point1.Latitude, point1.Longitude, point2.Latitude, point2.Longitude) + 180) % 360;
        }

        double getTilt(double t1, double t2)
        {
            return Math.Atan((_trajectory.Z(t2) - _trajectory.Z(t1)) / 103) * (180 / Math.PI);
        }

        static double DegreeBearing(double lat1, double lon1, double lat2, double lon2)
        {
            var dLon = (lon2 - lon1) * (Math.PI / 180);
            var dPhi = Math.Log(
                Math.Tan((lat2 * (Math.PI / 180)) / 2 + Math.PI / 4) / Math.Tan((lat1 * (Math.PI / 180)) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ((Math.Atan2(dLon, dPhi) * 180 / Math.PI) + 360) % 360;
        }

        public Color[] interpolateColors(Color lowerBound, Color upperBound, int numberOfIntervals)
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
