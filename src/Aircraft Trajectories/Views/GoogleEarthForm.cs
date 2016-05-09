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

            longData[0] = planeData[0][0] * 0.3048;
            latData[0] = planeData[0][1] * 0.3048;
            altData[0] = planeData[0][2] * 0.3048;
            double totalDuration = 0;
            for (int t = 1; t < planeData.Length; t++)
            {
                longData[t] = planeData[t][0] * 0.3048;
                latData[t] = planeData[t][1] * 0.3048;
                altData[t] = planeData[t][2] * 0.3048;

                double deltaX = longData[t] - longData[t - 1];
                double deltaY = latData[t] - latData[t - 1];
                double deltaZ = altData[t] - altData[t - 1];
                double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
                double duration = distance / (200*0.514);
                //MessageBox.Show(duration.ToString());
                tData[t] = totalDuration + duration;
                totalDuration += duration;
            }
            longSpline = CubicSpline.InterpolateNatural(tData, longData);
            latSpline = CubicSpline.InterpolateNatural(tData, latData);
            altSpline = CubicSpline.InterpolateNatural(tData, altData);

            createAnimationKML();
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

        public KmlModelCoClass model;
        //public KmlCameraCoClass camera;
        //public KmlOrientationCoClass or;
        public IKmlLineString line3D;
        public IKmlLineString lineGroundTrack;

        private void btnCenterRunway_Click(object sender, EventArgs e)
        {
            //ge.getLayerRoot().enableLayerById(ge.LAYER_BORDERS, 1);
            //ge.getLayerRoot().enableLayerById(ge.LAYER_ROADS, 1);

            ge.getOptions().setFlyToSpeed(ge.SPEED_TELEPORT);

            var t = ge.createTour("");
            

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

        public double currentStep;
        double[][] planeData;

        double[][] previousCoordinates;
        private void btnAnimate_Click(object sender, EventArgs e)
        {
            Console.WriteLine("");
            Console.WriteLine(currentStep);
            currentStep = 1;
            tmrAnimationStep.Enabled = !tmrAnimationStep.Enabled;

            previousCoordinates = new double[80][];
            for (int i = 0; i < previousCoordinates.Length; i++)
            {
                previousCoordinates[i] = new double[3];
                for (int a = 0; a < 3; a++)
                {
                    previousCoordinates[i][a] = -1;
                }
            }
        }

        public void createAnimationKML()
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };
            XmlWriter kml = XmlWriter.Create(@"C:\Users\Hans Schouten\Desktop\animation.kml", xmlWriterSettings);

            kml.WriteRaw("<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\">");

            kml.WriteStartElement("Document");
                kml.WriteStartElement("Placemark");
                    kml.WriteElementString("name","Aircraft Animation");
                    kml.WriteElementString("visibility", "1");
                    kml.WriteStartElement("Model");
                      kml.WriteAttributeString("id","model");
                        kml.WriteElementString("altitudeMode", "relativeToGround");
                        kml.WriteStartElement("Location");
                          kml.WriteAttributeString("id", "model_location");
                            kml.WriteElementString("latitude", "52.28913044987623");
                            kml.WriteElementString("longitude", "4.737291638740602");
                            kml.WriteElementString("altitude", "10.668");
                        kml.WriteEndElement();
                        kml.WriteStartElement("Orientation");
                          kml.WriteAttributeString("id", "model_orientation");
                            kml.WriteElementString("heading", "57.95");
                            kml.WriteElementString("tilt", "10.1797");
                            kml.WriteElementString("roll", "4.0296");
                        kml.WriteEndElement();
                        kml.WriteStartElement("Link");
                            kml.WriteElementString("href", "B733.dae");
                        kml.WriteEndElement();
                    kml.WriteEndElement();
                kml.WriteEndElement();

                kml.WriteRaw("<gx:Tour><gx:Playlist>");


                kml.WriteRaw(@"
<gx:AnimatedUpdate>
               <gx:duration>0.821510026074543</gx:duration>
               <Update>
                  <Change>
                     <Location targetId='model_location'>
                        <latitude>52.28882112816898</latitude>
                        <longitude>4.736491488313217</longitude>
                        <altitude>22.26130963842451</altitude>
                     </Location>
                  </Change>
               </Update>
            </gx:AnimatedUpdate>			
            <gx:AnimatedUpdate>
               <gx:duration>0.821510026074543</gx:duration>
               <Update>
                  <Change>
                     <Orientation targetId='model_orientation'>
                        <heading>58.26624356697369</heading>
                        <tilt>10.18347728868717</tilt>
                        <roll>3.183238427102504</roll>
                     </Orientation>
                  </Change>
               </Update>
            </gx:AnimatedUpdate>			
            <gx:FlyTo>
               <gx:duration>0.821510026074543</gx:duration>
               <gx:flyToMode>smooth</gx:flyToMode>
               <LookAt>
                  <longitude>4.73988285083995</longitude>
                  <latitude>52.28967179734476</latitude>
                  <altitude>122.2613096384245</altitude>
                  <altitudeMode>absolute</altitudeMode>
                  <heading>-121.7337564330263</heading>
                  <tilt>65</tilt>
               </LookAt>
            </gx:FlyTo>
			
            <gx:AnimatedUpdate>
               <gx:duration>0.821510026074543</gx:duration>
               <Update>
                  <Change>
                     <Location targetId='model_location'>
                        <latitude>52.28851180646174</latitude>
                        <longitude>4.735691337885832</longitude>
                        <altitude>33.85461927684903</altitude>
                     </Location>
                  </Change>
               </Update>
            </gx:AnimatedUpdate>			
            <gx:AnimatedUpdate>
               <gx:duration>0.821510026074543</gx:duration>
               <Update>
                  <Change>
                     <Orientation targetId='model_orientation'>
                        <heading>58.58248713394735</heading>
                        <tilt>10.18724244702327</tilt>
                        <roll>2.336860771967286</roll>
                     </Orientation>
                  </Change>
               </Update>
            </gx:AnimatedUpdate>			
            <gx:FlyTo>
               <gx:duration>0.821510026074543</gx:duration>
               <gx:flyToMode>smooth</gx:flyToMode>
               <LookAt>
                  <longitude>4.739090271105224</longitude>
                  <latitude>52.28935100467422</latitude>
                  <altitude>133.854619276849</altitude>
                  <altitudeMode>absolute</altitudeMode>
                  <heading>-121.4175128660527</heading>
                  <tilt>65</tilt>
               </LookAt>
            </gx:FlyTo>
            ");


                kml.WriteRaw("</gx:Playlist></gx:Tour>");
            kml.WriteEndElement();

            kml.WriteRaw("</kml>");
            kml.Close();

            this.Close();
        }


        private void tmrAnimationStep_Tick(object sender, EventArgs e)
        {
            tmrAnimationStep.Enabled = false;
            animationStep();
            tmrAnimationStep.Interval = 1;
            if(elapsedMs < 100)
            {
                tmrAnimationStep.Interval = (int) (100 - elapsedMs);
            }
            tmrAnimationStep.Enabled = true;
        }

        public double previousLat;
        public double previousLong;
        double elapsedMs = 0;
        public void animationStep()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            double interpLong = longSpline.Interpolate(currentStep);
            double interpLat = latSpline.Interpolate(currentStep);
            double interpAlt = altSpline.Interpolate(currentStep);

            RijksdriehoekComponent r = new RijksdriehoekComponent();
            PointF newCoordinates = r.ConvertToLatLong(interpLong, interpLat);
            interpLat = newCoordinates.X;
            interpLong = newCoordinates.Y;

            double bearing = -1;
            bearing = DegreeBearing(interpLat, interpLong, previousLat, previousLong);
            //MessageBox.Show("(" + interpLat.ToString() + "," + interpLong.ToString() + ") bearing: " + bearing.ToString());

            updateModel(interpLong, interpLat, interpAlt, bearing);
            updateCamera(interpLong, interpLat, interpAlt, bearing);
            //line3D.getCoordinates().pushLatLngAlt(interpLat, interpLong, interpAlt);

            // Prepare next iteration
            currentStep = currentStep + 0.1;
            previousLat = interpLat;
            previousLong = interpLong;
            
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
        }

        public void updateCamera(double longitude, double latitude, double altitude, double bearing)
        {
            double heading = (bearing + 180) % 360;
            /*
            double stepSize = 0.005;
            double latitudeDisplacementFactor = Math.Sin(bearing * 0.01745329);
            longitude = longitude - (latitudeDisplacementFactor * stepSize);
            latitude = latitude - ((1- latitudeDisplacementFactor) * stepSize);
            */
            var lookat = ge.getView().copyAsLookAt(ge.ALTITUDE_ABSOLUTE);
            lookat.setHeading(180);
            lookat.setLongitude(longitude);
            lookat.setLatitude(latitude);
            lookat.setAltitude(altitude);
            lookat.setRange(150);
            lookat.setTilt(65);
            ge.getView().setAbstractView(lookat);
        }

        public void updateModel(double longitude, double latitude, double altitude, double bearing)
        {
            var loc = model.getLocation();
            model.setAltitudeMode(ge.ALTITUDE_ABSOLUTE);
            loc.setLongitude(longitude);
            loc.setLatitude(latitude);
            loc.setAltitude(altitude);
            model.setLocation(loc);
            if (bearing >= 0)
            {
                var or = ge.createOrientation("");
                or.setTilt(10.11);
                or.setRoll(0);
                or.setHeading(0);
                model.setOrientation(or);
            }
        }







        public void trajectoryUpdated()
        {

        }


        private void tmrFileCheck_Tick(object sender, EventArgs e)
        {
            
        }








        static double DegreeBearing(double lat1, double lon1, double lat2, double lon2)
        {
            var dLon = ToRad(lon2 - lon1);
            var dPhi = Math.Log(
                Math.Tan(ToRad(lat2) / 2 + Math.PI / 4) / Math.Tan(ToRad(lat1) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            return ToBearing(Math.Atan2(dLon, dPhi));
        }

        public static double ToRad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        public static double ToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        public static double ToBearing(double radians)
        {
            return (ToDegrees(radians) + 360) % 360;
        }
    }
}
