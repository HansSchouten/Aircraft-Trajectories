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

namespace AircraftTrajectories.Views
{
    [ComVisibleAttribute(true)]
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public partial class GoogleEarthForm : Form, IGoogleEarthForm
    {
        private const string PLUGIN_URL =
            @"http://earth-api-samples.googlecode.com/svn/trunk/demos/desktop-embedded/pluginhost.html";
        private IGEPlugin ge = null;

        public GoogleEarthForm()
        {
            InitializeComponent();
        }

        public CubicSpline latSpline;
        public CubicSpline longSpline;
        public CubicSpline altSpline;
        double totalDuration = 0;
        private void GoogleEarthForm_Load(object sender, EventArgs e)
        {
            pnlControl.Enabled = false;
            webBrowser.Navigate(PLUGIN_URL);
            webBrowser.ObjectForScripting = this;

            planeData = AircraftTrajectories.Properties.Resources.A380.Split('\n').Select(q => q.Trim().Split(',').Select(Convert.ToDouble).ToArray()).ToArray();

            Double[] tData = new Double[planeData.Length];
            Double[] longData = new Double[planeData.Length];
            Double[] latData = new Double[planeData.Length];
            Double[] altData = new Double[planeData.Length];

            RijksdriehoekComponent r = new RijksdriehoekComponent();
            PointF latLong = r.ConvertToLatLong(planeData[0][0] * 0.3048, planeData[0][1] * 0.3048);
            longData[0] = latLong.Y;
            latData[0] = latLong.X;
            altData[0] = planeData[0][2] * 0.3048;
            tData[0] = 0;

            double xMetricPrevious = planeData[0][0] * 0.3048;
            double yMetricPrevious = planeData[0][1] * 0.3048;
            double zMetricPrevious = planeData[0][2] * 0.3048;
            for (int t = 1; t < planeData.Length; t++)
            {
                double xMetric = planeData[t][0] * 0.3048;
                double yMetric = planeData[t][1] * 0.3048;
                double zMetric = planeData[t][2] * 0.3048;
                double deltaX = xMetric - xMetricPrevious;
                double deltaY = yMetric - yMetricPrevious;
                double deltaZ = zMetric - zMetricPrevious;
                double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
                double duration = distance / (200*0.514);

                latLong = r.ConvertToLatLong(planeData[t][0] * 0.3048, planeData[t][1] * 0.3048);
                longData[t] = latLong.Y;
                latData[t] = latLong.X;
                altData[t] = planeData[t][2] * 0.3048;
                tData[t] = totalDuration + duration;

                // Prepare next iteration
                xMetricPrevious = xMetric;
                yMetricPrevious = yMetric;
                zMetricPrevious = zMetric;
                totalDuration += duration;
            }
            longSpline = CubicSpline.InterpolateNatural(tData, longData);
            latSpline = CubicSpline.InterpolateNatural(tData, latData);
            altSpline = CubicSpline.InterpolateNatural(tData, altData);

            //createAnimationKML();
            LaunchCommandLineApp();
            this.Close();
        }

        static void LaunchCommandLineApp()
        {
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = Application.StartupPath;
            startInfo.FileName = "INMTM_v3.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "track_A380.dat grid2D.dat";

            var watch = System.Diagnostics.Stopwatch.StartNew();
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }

            watch.Stop();
            double elapsedMs = watch.ElapsedMilliseconds;
            MessageBox.Show(elapsedMs.ToString());
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
                            kml.WriteElementString("latitude", latSpline.Interpolate(0).ToString());
                            kml.WriteElementString("longitude", longSpline.Interpolate(0).ToString());
                            kml.WriteElementString("altitude", altSpline.Interpolate(0).ToString());
                        kml.WriteEndElement();
                        kml.WriteStartElement("Orientation");
                          kml.WriteAttributeString("id", "model_orientation");
                            kml.WriteElementString("heading", getHeading(0,1).ToString());
                            kml.WriteElementString("tilt", "0.0");
                            kml.WriteElementString("roll", "0.0");
                        kml.WriteEndElement();
                        kml.WriteStartElement("Scale");
                          kml.WriteAttributeString("id", "model_scale");
                            kml.WriteElementString("x", "1");
                            kml.WriteElementString("y", "1");
                            kml.WriteElementString("z", "1");
                        kml.WriteEndElement();
                        kml.WriteStartElement("Link");
                            kml.WriteElementString("href", "B738.dae");
                        kml.WriteEndElement();
                    kml.WriteEndElement();
                kml.WriteEndElement();
                
                kml.WriteRaw("<gx:Tour><name>Flight</name><gx:Playlist>");
                
                for (int t = 0; t < totalDuration; t++) {
                    double currentLat = latSpline.Interpolate(t);
                    double currentLong = longSpline.Interpolate(t);
                    double currentAlt = altSpline.Interpolate(t);
                    var heading = getHeading(t, t + 1);
                    var tilt = getTilt(t, t + 1);
                    double bankAngle = 0;
                    if(t > 1) {
                        bankAngle = getBankAngle(t - 1, t, t + 1);
                    }

                    var cameraLat = currentLat;
                    var cameraLong = currentLong;
                    var cameraAlt = currentAlt + 80;
                    var cameraHeading = (heading + 180) % 360;
                
                    var R = 6378.1;
                    var brng = cameraHeading * Math.PI / 180;   //Bearing converted to radians.
                    var d = -0.2;                               //Distance in km
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
                      <tilt>65</tilt>
                   </LookAt>
                </gx:FlyTo>
                    ");
                }

                kml.WriteRaw("</gx:Playlist></gx:Tour>");
            kml.WriteEndElement();

            kml.WriteRaw("</kml>");
            kml.Close();

            this.Close();
        }

        double getBankAngle(double t1, double t2, double t3)
        {
            //http://www.regentsprep.org/regents/math/geometry/gcg6/RCir.htm

            double lat1 = latSpline.Interpolate(t1);
            double long1 = longSpline.Interpolate(t1);
            double lat2 = latSpline.Interpolate(t2);
            double long2 = longSpline.Interpolate(t2);
            double lat3 = latSpline.Interpolate(t3);
            double long3 = longSpline.Interpolate(t3);

            double m_r = (long2 - long1) / (lat2 - lat1);
            double m_t = (long3 - long2) / (lat3 - lat2);
            double x_c = (m_r * m_t * (long3 - long1) + m_r * (lat2 + lat3) - m_t * (lat1 + lat2)) / (2 * (m_r - m_t));
            double y_c = -(1 / m_r) * (x_c - ((lat1 + lat2) / 2)) + ((long1 + long2) / 2);

            GeoCoordinate c1 = new GeoCoordinate(lat1, long1);
            GeoCoordinate centroid = new GeoCoordinate(x_c, y_c);
            double radius = c1.GetDistanceTo(centroid);

            double TAS = (200 * 0.514);
            double g = 9.81;
            return Math.Atan(((TAS * TAS) / radius) / g) * (180 / Math.PI);
        }

        double getHeading(double t1, double t2)
        {
            return (DegreeBearing(latSpline.Interpolate(t1), longSpline.Interpolate(t1), latSpline.Interpolate(t2), longSpline.Interpolate(t2)) + 180) % 360;
        }

        double getTilt(double t1, double t2)
        {
            return Math.Atan((altSpline.Interpolate(t2) - altSpline.Interpolate(t1)) / 103) * (180 / Math.PI);
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
            double interpLong = longSpline.Interpolate(currentStep);
            double interpLat = latSpline.Interpolate(currentStep);
            double interpAlt = altSpline.Interpolate(currentStep);
            
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
