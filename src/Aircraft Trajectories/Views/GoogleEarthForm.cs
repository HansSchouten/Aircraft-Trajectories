using System;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using GEPlugin;
using MathNet.Numerics.Interpolation;
using AircraftTrajectories.Models.WGS.Research.Core;
using System.Drawing;
using System.Xml;
using System.Device.Location;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using AircraftTrajectories.Models.Contours;

namespace AircraftTrajectories.Views
{
    [ComVisibleAttribute(true)]
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public partial class GoogleEarthForm : Form, IGoogleEarthForm
    {
        public double feetToMeters = 1;//0.3048;

        private const string PLUGIN_URL =
            @"http://earth-api-samples.googlecode.com/svn/trunk/demos/desktop-embedded/pluginhost.html";
        private IGEPlugin ge = null;
        public String CURRENT_DIR = Path.GetDirectoryName(Application.ExecutablePath);

        public GoogleEarthForm()
        {
            InitializeComponent();
        }

        public CubicSpline ySpline;
        public CubicSpline xSpline;
        public CubicSpline zSpline;
        double totalDuration = 0;
        private void GoogleEarthForm_Load(object sender, EventArgs e)
        {
            pnlControl.Enabled = false;
            webBrowser.Navigate(PLUGIN_URL);
            webBrowser.ObjectForScripting = this;

            planeData = AircraftTrajectories.Properties.Resources.EHAM.Split('\n').Select(q => q.Trim().Split(',').Select(Convert.ToDouble).ToArray()).ToArray();

            RijksdriehoekComponent r = new RijksdriehoekComponent();
            PointF latLong = r.ConvertToLatLong(planeData[0][0] * feetToMeters, planeData[0][1] * feetToMeters);

            Double[] tData = new Double[planeData.Length];
            Double[] xData = new Double[planeData.Length];
            Double[] yData = new Double[planeData.Length];
            Double[] zData = new Double[planeData.Length];
            /*
            RijksdriehoekComponent r = new RijksdriehoekComponent();
            PointF latLong = r.ConvertToLatLong(planeData[0][0] * feetToMeters, planeData[0][1] * feetToMeters);
            */
            xData[0] = planeData[0][0] * feetToMeters;
            yData[0] = planeData[0][1] * feetToMeters;
            zData[0] = planeData[0][2] * feetToMeters;
            tData[0] = 0;

            double xMetricPrevious = planeData[0][0] * feetToMeters;
            double yMetricPrevious = planeData[0][1] * feetToMeters;
            double zMetricPrevious = planeData[0][2] * feetToMeters;
            for (int t = 1; t < planeData.Length; t++)
            {
                double xMetric = planeData[t][0] * feetToMeters;
                double yMetric = planeData[t][1] * feetToMeters;
                double zMetric = planeData[t][2] * feetToMeters;
                double deltaX = xMetric - xMetricPrevious;
                double deltaY = yMetric - yMetricPrevious;
                double deltaZ = zMetric - zMetricPrevious;
                double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
                double duration = distance / (200*0.514);

                //latLong = r.ConvertToLatLong(planeData[t][0] * feetToMeters, planeData[t][1] * feetToMeters);
                xData[t] = planeData[t][0] * feetToMeters;
                yData[t] = planeData[t][1] * feetToMeters;
                zData[t] = planeData[t][2] * feetToMeters;
                tData[t] = totalDuration + duration;

                // Prepare next iteration
                xMetricPrevious = xMetric;
                yMetricPrevious = yMetric;
                zMetricPrevious = zMetric;
                totalDuration += duration;
            }
            xSpline = CubicSpline.InterpolateNatural(tData, xData);
            ySpline = CubicSpline.InterpolateNatural(tData, yData);
            zSpline = CubicSpline.InterpolateNatural(tData, zData);

            createAnimationKML();
            //CalculateNoiseContours();
            this.Close();
        }

        public IEnumerable<Contour> CalculateNoiseContours(double t1, double t2)
        {
            // Create current_position.dat
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(CURRENT_DIR + @"\current_position.dat", false))
            {
                file.WriteLine("Sys");
                file.WriteLine("====================================================================================");
                file.WriteLine("met");
                file.WriteLine("         x         y         h         V         T         m");
                file.WriteLine("====================================================================================");
                file.WriteLine(xSpline.Interpolate(t1) + "      " + ySpline.Interpolate(t1) + "     " + zSpline.Interpolate(t1) + "     400      50000        2");
                file.WriteLine(xSpline.Interpolate(t2) + "      " + ySpline.Interpolate(t2) + "     " + zSpline.Interpolate(t2) + "     400      50000        2");
                file.WriteLine();
                file.WriteLine("nois_id / engine mount");
                file.WriteLine("====================================================================================");
                file.WriteLine("GP7270");
                file.WriteLine("wing");
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            // Calculate noise levels
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = Application.StartupPath;
            startInfo.FileName = "INMTM_v3.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "current_position.dat schiphol_grid2D.dat";
            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }

            // Read noise levels calculated by external noise model
            String rawNoise = File.ReadAllText(CURRENT_DIR + "/noise.out");
            Double[][] noiseData = rawNoise
                .Split('\n')
                .Skip(2)
                .Select(q =>
                    q.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(Convert.ToDouble)
                     .ToArray()
                )
                .ToArray();
            
            // Store noise levels in a 2D grid
            double[][] noiseDataGrid = {};
            double currentX = noiseData[0][0];
            List<double> column = new List<double>();
            int columnIndex = 0;
            for (int i = 0; i < noiseData.Length-1; i++) {
                // Check whether we encountered a new column
                if (currentX != noiseData[i][0]) {
                    // Check whether this was the first column
                    if (columnIndex == 0) {
                        // Now the total number of columns of the grid is known
                        int numberOfColumns = noiseData.Length / column.Count;
                        noiseDataGrid = new double[numberOfColumns][];
                    }
                    // Add the column to the grid
                    noiseDataGrid[columnIndex] = column.ToArray();

                    column = new List<double>();
                    currentX = noiseData[i][0];
                    columnIndex++;
                }

                column.Add(noiseData[i][4]);
            }
            noiseDataGrid[columnIndex] = column.ToArray();
            
            // Write 2D grid to a csv file
            using (StreamWriter outfile = new StreamWriter(@"C:\Users\hanss\Desktop\Aircraft-Trajectories\src\Aircraft Trajectories\bin\Debug\test.csv"))
            {
                for (int x = 0; x < noiseDataGrid.Length-1; x++)
                {
                    string content = "";
                    for (int y = 0; y < noiseDataGrid[0].Length-1; y++)
                    {
                        content += noiseDataGrid[x][y].ToString("0.00") + ",";
                    }
                    outfile.WriteLine(content);
                }
            }

            // Calculate noise contours
            IEnumerable<ContourPoint>[][] hgrid, vgrid;
            var contours = Contour.CreateContours(noiseDataGrid, out hgrid, out vgrid).ToArray();

            watch.Stop();
            double elapsedMs = watch.ElapsedMilliseconds;

            return contours;
        }


        // called from initCallback in JavaScript
        public void JSInitSuccessCallback_(object pluginInstance)
        {
            ge = (IGEPlugin)pluginInstance;
            pnlControl.Enabled = true;
        }

        // called from failureCallback in JavaScript
        public void JSInitFailureCallback_(string error)
        {
            MessageBox.Show("Error: " + error, "Plugin Load Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }

        public void createAnimationKML()
        {
            // line 6782 (t = 03:03)
            RijksdriehoekComponent RDConverter = new RijksdriehoekComponent();
            PointF startLocation = RDConverter.ConvertToLatLong(xSpline.Interpolate(0), ySpline.Interpolate(0));

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };
            XmlWriter kml = XmlWriter.Create(@"C:\Users\hanss\Desktop\anim.kml", xmlWriterSettings);

            kml.WriteRaw("<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\">");

            kml.WriteStartElement("Document");
                kml.WriteStartElement("Placemark");
                    kml.WriteElementString("name","Aircraft Animation");
                    kml.WriteElementString("visibility", "1");
                    kml.WriteStartElement("Model");
                      kml.WriteAttributeString("id","model");
                        kml.WriteElementString("altitudeMode", "absolute");
                        kml.WriteStartElement("Location");
                          kml.WriteAttributeString("id", "model_location");
                            kml.WriteElementString("latitude", startLocation.Y.ToString());
                            kml.WriteElementString("longitude", startLocation.X.ToString());
                            kml.WriteElementString("altitude", zSpline.Interpolate(0).ToString());
                        kml.WriteEndElement();
                        kml.WriteStartElement("Orientation");
                          kml.WriteAttributeString("id", "model_orientation");
                            kml.WriteElementString("heading", getHeading(0,1).ToString());
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

            kml.WriteRaw(@"
                <Style id='contour_style1'>
                  <LineStyle>
                    <color>FF78B43C</color>
                    <width>1</width>
                  </LineStyle>
                  <PolyStyle>
                    <color>7F78D23C</color>
                    <fill>1</fill>
                    <outline>1</outline>
                  </PolyStyle>
                </Style>
                <Style id='contour_style2'>
                  <LineStyle>
                    <color>FF1478F0</color>
                    <width>1</width>
                  </LineStyle>
                  <PolyStyle>
                    <color>7F14B4FF</color>
                    <fill>1</fill>
                    <outline>1</outline>
                  </PolyStyle>
                </Style>
                <Style id='contour_style3'>
                  <LineStyle>
                    <color>FF1400BE</color>
                    <width>1</width>
                  </LineStyle>
                  <PolyStyle>
                    <color>7F143CFF</color>
                    <fill>1</fill>
                    <outline>1</outline>
                  </PolyStyle>
                </Style>
                <Style id='plotair_style'>
                  <LineStyle>
                    <width>0</width>
                  </LineStyle>
                  <PolyStyle>
                    <color>80F0B414</color>
                    <fill>1</fill>
                  </PolyStyle>
                </Style>

                <Placemark id='contour_placemark1'>
                  <name>Contour</name>
                  <styleUrl>#contour_style1</styleUrl>
                  <Polygon>
                    <tessellate>1</tessellate>
                    <outerBoundaryIs>
                      <LinearRing id='contour1'>
                        <coordinates></coordinates>
                      </LinearRing>
                    </outerBoundaryIs>
                  </Polygon>
                </Placemark>
                <Placemark id='contour_placemark2'>
                  <name>Contour</name>
                  <styleUrl>#contour_style2</styleUrl>
                  <Polygon>
                    <tessellate>1</tessellate>
                    <outerBoundaryIs>
                      <LinearRing id='contour2'>
                        <coordinates></coordinates>
                      </LinearRing>
                    </outerBoundaryIs>
                  </Polygon>
                </Placemark>
                <Placemark id='contour_placemark3'>
                  <name>Contour</name>
                  <styleUrl>#contour_style3</styleUrl>
                  <Polygon>
                    <tessellate>1</tessellate>
                    <outerBoundaryIs>
                      <LinearRing id='contour3'>
                        <coordinates></coordinates>
                      </LinearRing>
                    </outerBoundaryIs>
                  </Polygon>
                </Placemark>
                <Placemark id='contour_plotair'>
                  <name>Plotair</name>
                  <styleUrl>#plotair_style</styleUrl>
                  <Polygon>
                    <altitudeMode>absolute</altitudeMode>
                    <tessellate>1</tessellate>
                    <outerBoundaryIs>
                      <LinearRing id='plotair'>
                        <coordinates></coordinates>
                      </LinearRing>
                    </outerBoundaryIs>
                  </Polygon>
                </Placemark>
                    ");

            kml.WriteRaw("<gx:Tour><name>Flight</name><gx:Playlist>");

            MessageBox.Show(totalDuration.ToString());
            string plotAirGroundCoordinates = "";
            string plotAirAirCoordinates = "";
            for (int t = 0; t < totalDuration; t++) {
                    Console.WriteLine(t);
                    PointF latLong = RDConverter.ConvertToLatLong(xSpline.Interpolate(t), ySpline.Interpolate(t));
                    double currentLat = latLong.Y;
                    double currentLong = latLong.X;
                    double currentAlt = zSpline.Interpolate(t);
                    var heading = getHeading(t, t + 1);
                    var tilt = getTilt(t, t + 1);
                    double bankAngle = 0;
                    if(t > 1) {
                        bankAngle = getBankAngle(t - 1, t, t + 1);
                    }

                    var cameraLat = currentLat;
                    var cameraLong = currentLong;
                    var cameraAlt = currentAlt + 300;
                    var cameraHeading = (heading + 180) % 360;
                    var cameraTilt = 73;

                    var R = 6378.1;
                    var brng = cameraHeading * Math.PI / 180;   //Bearing converted to radians.
                    var d = -1.3;                               //Distance in km
                    var lat1 = currentLat * Math.PI / 180;      //Current lat point converted to radians
                    var lon1 = currentLong * Math.PI / 180;     //Current long point converted to radians

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
                            <latitude>" + currentLat + @"</latitude>
                            <longitude>" + currentLong + @"</longitude>
                            <altitude>" + currentAlt + @"</altitude>
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
                /*
                plotAirGroundCoordinates += currentLong + "," + currentLat + ",0\n";
                plotAirAirCoordinates = currentLong + "," + currentLat + "," + currentAlt + "\n"+ plotAirAirCoordinates;
                plotUpdate(kml, plotAirGroundCoordinates+plotAirAirCoordinates, "plotair");
                */
                var contours = CalculateNoiseContours(t, t+0.01);
                RijksdriehoekComponent converter = new RijksdriehoekComponent();
                List<int> visibleContours = new List<int>();
                foreach (Contour contour in contours) {
                    if (!contour.IsClosed) { continue; }
                    
                    int contourId = -1;
                    if (contour.Value == 65) {
                        contourId = 1;
                    }
                    else if (contour.Value == 70) {
                        contourId = 2;
                    }
                    else if (contour.Value == 80) {
                        contourId = 3;
                    } else {
                        continue;
                    }
                    visibleContours.Add(contourId);
                    
                    var coordinateString = "";
                    foreach (ContourPoint p in contour.Points) {
                        double x = (104062 + (p.Location.X * 125));
                        double y = (475470 + (p.Location.Y * 125));
                        PointF contourlatLong = converter.ConvertToLatLong(x, y);
                        coordinateString += contourlatLong.X + ","+ contourlatLong.Y + ",0\n";
                    }
                    plotUpdate(kml, coordinateString, "contour" + contourId);
                }
                for(int i=1; i<=3; i++) {
                    if(!visibleContours.Contains(i)) {
                        plotUpdate(kml, currentLong + "," + currentLat + ",0", "contour" + i);
                    }
                }
            }

                kml.WriteRaw("</gx:Playlist></gx:Tour>");
            kml.WriteEndElement();

            kml.WriteRaw("</kml>");
            kml.Close();

            this.Close();
        }

        void plotUpdate(XmlWriter kml, String coordinateString, string targetId)
        {
            kml.WriteRaw(@"
                <gx:AnimatedUpdate>
                   <gx:duration>1.0</gx:duration>
                   <Update>
                      <Change>
                         <LinearRing targetId='" + targetId + @"'>
                            <coordinates>
                            " + coordinateString + @"
                            </coordinates>
                         </LinearRing>
                      </Change>
                   </Update>
                </gx:AnimatedUpdate>
                    ");

        }

        double getBankAngle(double t1, double t2, double t3)
        {
            //http://www.regentsprep.org/regents/math/geometry/gcg6/RCir.htm
            RijksdriehoekComponent RDConverter = new RijksdriehoekComponent();
            PointF point1 = RDConverter.ConvertToLatLong(xSpline.Interpolate(t1), ySpline.Interpolate(t1));
            PointF point2 = RDConverter.ConvertToLatLong(xSpline.Interpolate(t2), ySpline.Interpolate(t2));
            PointF point3 = RDConverter.ConvertToLatLong(xSpline.Interpolate(t3), ySpline.Interpolate(t3));

            double m_r = (point2.X - point1.X) / (point2.Y - point1.Y);
            double m_t = (point3.X - point2.X) / (point3.Y - point2.Y);
            double x_c = (m_r * m_t * (point3.X - point1.X) + m_r * (point2.Y + point3.Y) - m_t * (point1.Y + point2.Y)) / (2 * (m_r - m_t));
            double y_c = -(1 / m_r) * (x_c - ((point1.Y + point2.Y) / 2)) + ((point1.X + point2.X) / 2);

            if (double.IsInfinity(x_c)) {
                return 0;
            }
            
            GeoCoordinate c1 = new GeoCoordinate(point1.Y, point1.X);
            GeoCoordinate centroid = new GeoCoordinate(x_c, y_c);
            double radius = c1.GetDistanceTo(centroid);

            double TAS = (200 * 0.514);
            double g = 9.81;
            return Math.Atan(((TAS * TAS) / radius) / g) * (180 / Math.PI);
        }

        double getHeading(double t1, double t2)
        {
            RijksdriehoekComponent RDConverter = new RijksdriehoekComponent();
            PointF point1 = RDConverter.ConvertToLatLong(xSpline.Interpolate(t1), ySpline.Interpolate(t1));
            PointF point2 = RDConverter.ConvertToLatLong(xSpline.Interpolate(t2), ySpline.Interpolate(t2));
            return (DegreeBearing(point1.Y, point1.X, point2.Y, point2.X) + 180) % 360;
        }

        double getTilt(double t1, double t2)
        {
            return Math.Atan((zSpline.Interpolate(t2) - zSpline.Interpolate(t1)) / 103) * (180 / Math.PI);
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













        public KmlModelCoClass model;
        public IKmlLineString line3D;
        public IKmlLineString lineGroundTrack;

        private void btnCenterRunway_Click(object sender, EventArgs e)
        {
            //ge.getLayerRoot().enableLayerById(ge.LAYER_BORDERS, 1);
            //ge.getLayerRoot().enableLayerById(ge.LAYER_ROADS, 1);

            ge.getOptions().setFlyToSpeed(ge.SPEED_TELEPORT);

            // LineString
            var placemarkLine = ge.createPlacemark("");
            placemarkLine.setName("line air");
            lineGroundTrack = ge.createLineString("");

            line3D = ge.createLineString("");
            line3D.setAltitudeMode(ge.ALTITUDE_RELATIVE_TO_GROUND);
            line3D.setExtrude(1);
            line3D.setTessellate(1);
            placemarkLine.setGeometry(line3D);
            var lineStyle = new KmlLineStringCoClass();
            //lineStyle.
            ge.getFeatures().appendChild(placemarkLine);

            // Model
            var placemark = ge.createPlacemark("");
            placemark.setName("model");
            model = ge.createModel("");
            ge.getFeatures().appendChild(placemark);

            var link = ge.createLink("");
            link.setHref(@"https://raw.githubusercontent.com/Hansschouten/Aircraft-Trajectories/Animation/src/Animation/kml%20files/B738.dae");
            model.setLink(link);

            var loc = ge.createLocation("");
            loc.setLatitude(52.28913044987623);
            loc.setLongitude(4.737291638740602);
            loc.setAltitude(10.668);
            model.setLocation(loc);

            var or = ge.createOrientation("");
            or.setTilt(10.11);
            or.setRoll(0);
            or.setHeading(57.95);
            model.setOrientation(or);

            // Camera
            /*
            camera = ge.getView().copyAsCamera(ge.ALTITUDE_RELATIVE_TO_GROUND);
            camera.setLatitude(52.289671797);
            camera.setLongitude(4.7398828508);
            camera.setAltitude(122);
            camera.setHeading(-122);
            camera.setTilt(65);
            */

            placemark.setGeometry(model);
            //ge.getView().setAbstractView(camera);

            btnAnimate.PerformClick();
        }

        private void tmrAnimationStep_Tick(object sender, EventArgs e) {
            tmrAnimationStep.Enabled = false;
            animationStep();
            tmrAnimationStep.Interval = 1;
            if(elapsedMs < 33)
            {
                tmrAnimationStep.Interval = (int) (33 - elapsedMs);
            }
            tmrAnimationStep.Enabled = true;
        }
        
        public double currentStep;
        double[][] planeData;

        private void btnAnimate_Click(object sender, EventArgs e) {
            Console.WriteLine("");
            Console.WriteLine(currentStep);
            currentStep = 1;
            tmrAnimationStep.Enabled = !tmrAnimationStep.Enabled;
        }

        double elapsedMs = 0;
        public void animationStep()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            double interpLong = xSpline.Interpolate(currentStep);
            double interpLat = ySpline.Interpolate(currentStep);
            double interpAlt = zSpline.Interpolate(currentStep);
            
            var heading = getHeading(currentStep, currentStep + 1);
            //MessageBox.Show("(" + interpLat.ToString() + "," + interpLong.ToString() + ") bearing: " + bearing.ToString());

            updateModel(interpLong, interpLat, interpAlt, heading);
            updateCamera(interpLong, interpLat, interpAlt, heading);
            //line3D.getCoordinates().pushLatLngAlt(interpLat, interpLong, interpAlt);

            currentStep = currentStep + 0.033;

            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
        }

        public void updateCamera(double longitude, double latitude, double altitude, double heading)
        {
            heading = (heading + 180) % 360;
            /*
            double stepSize = 0.005;
            double latitudeDisplacementFactor = Math.Sin(bearing * 0.01745329);
            longitude = longitude - (latitudeDisplacementFactor * stepSize);
            latitude = latitude - ((1- latitudeDisplacementFactor) * stepSize);
            */
            var lookat = ge.getView().copyAsLookAt(ge.ALTITUDE_ABSOLUTE);
            lookat.setHeading(heading);
            lookat.setLongitude(longitude);
            lookat.setLatitude(latitude);
            lookat.setAltitude(altitude+200);
            lookat.setRange(300);
            lookat.setTilt(0);
            ge.getView().setAbstractView(lookat);
        }

        public void updateModel(double longitude, double latitude, double altitude, double heading)
        {
            var loc = model.getLocation();
            model.setAltitudeMode(ge.ALTITUDE_ABSOLUTE);
            loc.setLongitude(longitude);
            loc.setLatitude(latitude);
            loc.setAltitude(altitude);
            model.setLocation(loc);
            if (heading >= 0)
            {
                var or = ge.createOrientation("");
                //or.setTilt(0);
                //or.setRoll(0);
                or.setHeading(heading);
                model.setOrientation(or);
            }
        }


    }
}
