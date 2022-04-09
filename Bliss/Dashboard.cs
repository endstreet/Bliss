using Bliss.Services;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Bliss
{
    public partial class Dashboard : Form
    {
        // layers
        readonly GMapOverlay _top = new GMapOverlay();
        internal readonly GMapOverlay Objects = new GMapOverlay("objects");
        internal readonly GMapOverlay Routes = new GMapOverlay("routes");
        internal readonly GMapOverlay Tracks = new GMapOverlay("tracks");
        internal readonly GMapOverlay Polygons = new GMapOverlay("polygons");

        // marker
        GMapMarker _currentMarker;

        // polygons
        //GMapPolygon _polygon;

        // etc
        readonly Random _rnd = new Random();
        readonly DescendingComparer _comparerIpStatus = new DescendingComparer();
        //GMapMarkerRect _curentRectMarker;
        string _mobileGpsLog = string.Empty;
        bool _isMouseDown;
        PointLatLng _start;
        PointLatLng _end;

        System.Windows.Forms.Timer GetLocation = new System.Windows.Forms.Timer();

        public Dashboard()
        {
            SQLite.Test();
            InitializeComponent();
            if (!GMapControl.IsDesignerHosted)
            {
                // add your custom map db provider
                //MsSQLPureImageCache ch = new MsSQLPureImageCache();
                //ch.ConnectionString = @"data source = sql5040.site4now.net;User Id=DB_A3B2C9_GMapNET_admin; initial catalog = DB_A3B2C9_GMapNET; password = Usuario@2018;";                
                //MainMap.Manager.SecondaryCache = ch;

                // set your proxy here if need
                //GMapProvider.IsSocksProxy = true;
                //GMapProvider.WebProxy = new WebProxy("127.0.0.1", 1080);
                //GMapProvider.WebProxy.Credentials = new NetworkCredential("ogrenci@bilgeadam.com", "bilgeada");
                // or
                //GMapProvider.WebProxy = WebRequest.DefaultWebProxy;
                //                          

                //// set cache mode only if no internet available
                if (!Stuff.PingNetwork("google.com"))
                {
                    MainMap.Manager.Mode = AccessMode.CacheOnly;
                    MessageBox.Show("No internet connection available, going to CacheOnly mode.", "Blissed Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //----------------------------------------
                // Config Map at Startup
                //----------------------------------------
                MainMap.MapProvider = GMapProviders.OpenStreetMap;
                MainMap.ShowCenter = false;

                //----------------------------------------
                // Initial Position
                //----------------------------------------
                //-28.804256, 32.043904
                MainMap.Position = new PointLatLng(-28.804256, 32.043904); 
                MainMap.MinZoom = 0;
                MainMap.MaxZoom = 24;
                MainMap.Zoom = 9;

                //textBoxLat.Text = MainMap.Position.Lat.ToString(CultureInfo.InvariantCulture);
                //textBoxLng.Text = MainMap.Position.Lng.ToString(CultureInfo.InvariantCulture);
                //textBoxGeo.Text = "Lithuania, Vilnius";

                GoogleMapProvider.Instance.ApiKey = AppSettings.Default.GoogleMapApiKey;

                MainMap.ScaleMode = ScaleModes.Fractional;
                
                //----------------------------------------
                // Map Events
                //----------------------------------------
                

                MainMap.OnPositionChanged += MainMap_OnPositionChanged;
                MainMap.OnTileLoadStart += MainMap_OnTileLoadStart;
                MainMap.OnTileLoadComplete += MainMap_OnTileLoadComplete;
                MainMap.OnMapClick += MainMap_OnMapClick;

                MainMap.OnMapZoomChanged += MainMap_OnMapZoomChanged;
                MainMap.OnMapTypeChanged += MainMap_OnMapTypeChanged;

                MainMap.OnMarkerClick += MainMap_OnMarkerClick;
                MainMap.OnMarkerDoubleClick += MainMap_OnMarkerDoubleClick;

                MainMap.OnMarkerEnter += MainMap_OnMarkerEnter;
                MainMap.OnMarkerLeave += MainMap_OnMarkerLeave;

                MainMap.OnPolygonEnter += MainMap_OnPolygonEnter;
                MainMap.OnPolygonLeave += MainMap_OnPolygonLeave;

                MainMap.OnPolygonClick += MainMap_OnPolygonClick;
                MainMap.OnPolygonDoubleClick += MainMap_OnPolygonDoubleClick;

                MainMap.OnRouteEnter += MainMap_OnRouteEnter;
                MainMap.OnRouteLeave += MainMap_OnRouteLeave;

                MainMap.OnRouteClick += MainMap_OnRouteClick;
                MainMap.OnRouteDoubleClick += MainMap_OnRouteDoubleClick;

                MainMap.Manager.OnTileCacheComplete += OnTileCacheComplete;
                MainMap.Manager.OnTileCacheStart += OnTileCacheStart;
                MainMap.Manager.OnTileCacheProgress += OnTileCacheProgress;

                MainMap.MouseMove += MainMap_MouseMove;
                MainMap.MouseDown += MainMap_MouseDown;
                MainMap.MouseUp += MainMap_MouseUp;
                MainMap.MouseDoubleClick += MainMap_MouseDoubleClick;

                //----------------------------------------
                // Custom Layers
                //----------------------------------------
                MainMap.Overlays.Add(Routes);
                MainMap.Overlays.Add(Polygons);
                MainMap.Overlays.Add(Objects);
                MainMap.Overlays.Add(_top);

                //----------------------------------------
                // Other Events
                //----------------------------------------
                Routes.Routes.CollectionChanged += Routes_CollectionChanged;
                Objects.Markers.CollectionChanged += Markers_CollectionChanged;

                //----------------------------------------
                // Populate Map ComboBox
                //----------------------------------------
                //                comboBoxMapType.ValueMember = "Name";
                //                comboBoxMapType.DataSource = GMapProviders.List;
                //                comboBoxMapType.SelectedItem = MainMap.MapProvider;

                //                // acccess mode
                //                comboBoxMode.DataSource = Enum.GetValues(typeof(AccessMode));
                //                comboBoxMode.SelectedItem = MainMap.Manager.Mode;

                //                // get cache modes
                //                checkBoxUseRouteCache.Checked = MainMap.Manager.UseRouteCache;

                // get zoom  
                //trackBarZoom.Minimum = MainMap.MinZoom * 100;
                //trackBarZoom.Maximum = MainMap.MaxZoom * 100;
                //trackBarZoom.TickFrequency = 100;

#if DEBUG
                //checkBoxDebug.Checked = true;
                //MainMap.ShowTileGridLines = checkBoxDebug.Checked;
#endif

                // set current marker
                _currentMarker = new GMarkerGoogle(MainMap.Position, GMarkerGoogleType.red);
                _currentMarker.IsHitTestVisible = false;
                _top.Markers.Add(_currentMarker);



                if (Objects.Markers.Count > 0)
                {
                    MainMap.ZoomAndCenterMarkers(null);
                }

                RegeneratePolygon();

                try
                {
                    var overlay = DeepClone(Objects);
                    Debug.WriteLine("ISerializable status for markers: OK");

                    var overlay2 = DeepClone(Polygons);
                    Debug.WriteLine("ISerializable status for polygons: OK");

                    var overlay3 = DeepClone(Routes);
                    Debug.WriteLine("ISerializable status for routes: OK");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ISerializable failure: " + ex.Message);
#if DEBUG
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }
#endif
                }
            }
            //Timer
            GetLocation.Tick += MainMap_LocationUpdate;
            GetLocation.Interval = AppSettings.Default.LocationUpdateInterval;
            GetLocation.Start();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            //trackBarZoom.Value = (int)MainMap.Zoom * 100;
            Activate();
            TopMost = true;
            TopMost = false;
        }

        private void DrawWaypoints(PointLatLng from, PointLatLng to)
        {
            GMapRoute line_layer;

            line_layer = new GMapRoute("single_line");
            line_layer.Stroke = new Pen(Brushes.DodgerBlue, 2); //width and color of line

            Routes.Routes.Add(line_layer);
            MainMap.Overlays.Add(Routes);

            //Once the layer is created, simply add the two points you want
            line_layer.Points.Add(from);
            line_layer.Points.Add(to);

            //Note that if you are using the MouseEventArgs you need to use local coordinates and convert them:
            //line_layer.Points.Add(MainMap.FromLocalToLatLng(e.X, e.Y));

            //To force the draw, you need to update the route
            MainMap.UpdateRouteLocalPosition(line_layer);

            //you can even add markers at the end of the lines by adding markers to the same layer:
            GMarkerGoogle _waypoint = new GMarkerGoogle(to,GMarkerGoogleType.lightblue_pushpin);
            Routes.Markers.Add(_waypoint);
        }

        private void DrawTrack(PointLatLng from, PointLatLng to)
        {
            GMapRoute track_layer = new GMapRoute("track_layer");
            track_layer.Stroke = new Pen(Brushes.Green, 2); //width and color of line
            if (!Tracks.Routes.Contains(track_layer))
            {
                Tracks.Routes.Add(track_layer);
            }
            if (!MainMap.Overlays.Contains(Tracks))
            {
                MainMap.Overlays.Add(Tracks);
            }

            //Once the layer is created, simply add the two points you want
            track_layer.Points.Add(from);
            track_layer.Points.Add(to);

            //Note that if you are using the MouseEventArgs you need to use local coordinates and convert them:
            //line_layer.Points.Add(MainMap.FromLocalToLatLng(e.X, e.Y));

            //To force the draw, you need to update the route
            MainMap.UpdateRouteLocalPosition(track_layer);

            //you can even add markers at the end of the lines by adding markers to the same layer:
            //GMarkerGoogle _waypoint = new GMarkerGoogle(to, GMarkerGoogleType.green_pushpin);
            //Tracks.Markers.Add(_waypoint);
        }
        private void DrawWaypoint()
        {

        }
        private void MainMap_OnMapClick(PointLatLng pointClick, MouseEventArgs e)
        {
            MainMap.FromLocalToLatLng(e.X, e.Y);
            //MainMap.Position = GPSSensor.lastLocation;//new PointLatLng(54.6961334816182, 25.2985095977783); // Lithuania, Vilnius
        }

        private void MainMap_OnRouteDoubleClick(GMapRoute item, MouseEventArgs e)
        {

        }

        private void MainMap_OnRouteClick(GMapRoute item, MouseEventArgs e)
        {

        }

        private void MainMap_OnPolygonDoubleClick(GMapPolygon item, MouseEventArgs e)
        {

        }

        private void MainMap_OnPolygonClick(GMapPolygon item, MouseEventArgs e)
        {

        }

        private void MainMap_OnMarkerDoubleClick(GMapMarker item, MouseEventArgs e)
        {

        }

        public T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                formatter.Serialize(ms, obj);

                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        void Markers_CollectionChanged(object sender, GMap.NET.ObjectModel.NotifyCollectionChangedEventArgs e)
        {
            //textBoxMarkerCount.Text = Objects.Markers.Count.ToString();
        }

        void Routes_CollectionChanged(object sender, GMap.NET.ObjectModel.NotifyCollectionChangedEventArgs e)
        {
            //textBoxRouteCount.Text = Routes.Routes.Count.ToString();
        }

        #region -- performance test --

        double NextDouble(Random rng, double min, double max)
        {
            return min + (rng.NextDouble() * (max - min));
        }

        /// <summary>
        /// Update GPS position from Timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainMap_LocationUpdate(object? sender, EventArgs e)
        {
            if (!GPSSensor.IsValid) return;
            if (MainMap.Position == GPSSensor.lastLocation) return;
            _top.Markers.Clear();
            DrawTrack(MainMap.Position, GPSSensor.lastLocation);
            MainMap.Position = GPSSensor.lastLocation;
            _currentMarker = new GMarkerGoogle(MainMap.Position, GMarkerGoogleType.red);
            _top.Markers.Add(_currentMarker);
        }
        //System.Windows.Forms.Timer _timerPerf = new System.Windows.Forms.Timer();
        
        //System.Windows.Forms.Timer GetLocation = new System.Windows.Forms.Timer();
        
        //_timerPerf.Tick += timer_Tick;
        #endregion


        #region -- some functions --

        void RegeneratePolygon()
        {
            //var polygonPoints = new List<PointLatLng>();

            //foreach (var m in Objects.Markers)
            //{
            //    if (m is GMapMarkerRect)
            //    {
            //        m.Tag = polygonPoints.Count;
            //        polygonPoints.Add(m.Position);
            //    }
            //}

            //if (_polygon == null)
            //{
            //    _polygon = new GMapPolygon(polygonPoints, "polygon test");
            //    _polygon.IsHitTestVisible = true;
            //    Polygons.Polygons.Add(_polygon);
            //}
            //else
            //{
            //    _polygon.Points.Clear();
            //    _polygon.Points.AddRange(polygonPoints);

            //    if (Polygons.Polygons.Count == 0)
            //    {
            //        Polygons.Polygons.Add(_polygon);
            //    }
            //    else
            //    {
            //        MainMap.UpdatePolygonLocalPosition(_polygon);
            //    }
            //}
        }


        bool TryExtractLeafletjs()
        {
            try
            {
                string launch = string.Empty;

                var x = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                foreach (string f in x)
                {
                    if (f.Contains("leafletjs"))
                    {
                        string fName = f.Replace("Demo.WindowsForms.", string.Empty);
                        fName = fName.Replace(".", "\\");
                        int ll = fName.LastIndexOf("\\");
                        string name = fName.Substring(0, ll) + "." + fName.Substring(ll + 1, fName.Length - ll - 1);

                        //Demo.WindowsForms.leafletjs.dist.leaflet.js

                        using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(f))
                        {
                            string fileFullPath = MainMap.CacheLocation + name;

                            if (fileFullPath.Contains("gmap.html"))
                            {
                                launch = fileFullPath;
                            }

                            string dir = Path.GetDirectoryName(fileFullPath);
                            if (!Directory.Exists(dir))
                            {
                                Directory.CreateDirectory(dir);
                            }

                            using (var fileStream = File.Create(fileFullPath, (int)stream.Length))
                            {
                                // Fill the bytes[] array with the stream data
                                var bytesInStream = new byte[stream.Length];
                                stream.Read(bytesInStream, 0, bytesInStream.Length);

                                // Use FileStream object to write to the specified file
                                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(launch))
                {
                    Process.Start(launch);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("TryExtractLeafletjs: " + ex);
                return false;
            }
            return true;
        }

        #endregion


        #region -- cache events --

        void OnTileCacheComplete()
        {
            Debug.WriteLine("OnTileCacheComplete");
            long size = 0;
            int db = 0;
            try
            {
                var di = new DirectoryInfo(MainMap.CacheLocation);
                var dbs = di.GetFiles("*.gmdb", SearchOption.AllDirectories);
                foreach (var d in dbs)
                {
                    size += d.Length;
                    db++;
                }
            }
            catch
            {
            }

            if (!IsDisposed)
            {
                MethodInvoker m = delegate
                {
                    ////textBoxCacheSize.Text = string.Format(CultureInfo.InvariantCulture, "{0} db in {1:00} MB", db, size / (1024.0 * 1024.0));
                    //progressBar1.Visible = false;
                    //groupBoxProgress.Text = "all tiles saved!";
                };

                if (!IsDisposed)
                {
                    try
                    {
                        Invoke(m);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        void OnTileCacheStart()
        {
            //Debug.WriteLine("OnTileCacheStart");

            //if (!IsDisposed)
            //{
            //    MethodInvoker m = delegate
            //    {
            //        progressBar1.Visible = true;
            //        progressBar1.Value = progressBar1.Minimum;
            //        groupBoxProgress.Text = "saving tiles...";
            //    };
            //    Invoke(m);
            //}
        }

        void OnTileCacheProgress(int left)
        {
            //if (!IsDisposed)
            //{
            //    MethodInvoker m = delegate
            //    {
            //        progressBar1.Visible = true;
            //        progressBar1.Value = (progressBar1.Value < progressBar1.Maximum) ? (progressBar1.Value + 1) : progressBar1.Minimum;
            //        groupBoxProgress.Text = left + " tiles to save...";
            //    };
            //    Invoke(m);
            //}
        }

        #endregion


        #region -- map events --


        void MainMap_OnMarkerLeave(GMapMarker item)
        {
            //if (item is GMapMarkerRect)
            //{
            //    _curentRectMarker = null;

            //    var rc = item as GMapMarkerRect;
            //    rc.Pen.Color = Color.Blue;

            //    Debug.WriteLine("OnMarkerLeave: " + item.Position);
            //}
        }

        void MainMap_OnMarkerEnter(GMapMarker item)
        {
            //if (item is GMapMarkerRect)
            //{
            //    var rc = item as GMapMarkerRect;
            //    rc.Pen.Color = Color.Red;

            //    _curentRectMarker = rc;
            //}
            //Debug.WriteLine("OnMarkerEnter: " + item.Position);
        }

        GMapPolygon _currentPolygon;
        void MainMap_OnPolygonLeave(GMapPolygon item)
        {
            _currentPolygon = null;
            item.Stroke.Color = Color.MidnightBlue;
            Debug.WriteLine("OnPolygonLeave: " + item.Name);
        }

        void MainMap_OnPolygonEnter(GMapPolygon item)
        {
            _currentPolygon = item;
            item.Stroke.Color = Color.Red;
            Debug.WriteLine("OnPolygonEnter: " + item.Name);
        }

        GMapRoute _currentRoute;
        void MainMap_OnRouteLeave(GMapRoute item)
        {
            _currentRoute = null;
            item.Stroke.Color = Color.MidnightBlue;
            Debug.WriteLine("OnRouteLeave: " + item.Name);
        }

        void MainMap_OnRouteEnter(GMapRoute item)
        {
            _currentRoute = item;
            item.Stroke.Color = Color.Red;
            Debug.WriteLine("OnRouteEnter: " + item.Name);
        }

        void MainMap_OnMapTypeChanged(GMapProvider type)
        {
            //comboBoxMapType.SelectedItem = type;

            ////trackBarZoom.Minimum = MainMap.MinZoom * 100;
            ////trackBarZoom.Maximum = MainMap.MaxZoom * 100;

        }

        void MainMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isMouseDown = false;
            }
        }

        // add demo circle
        void MainMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //var cc = new GMapMarkerCircle(MainMap.FromLocalToLatLng(e.X, e.Y));
            //Objects.Markers.Add(cc);
        }

        void MainMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //_isMouseDown = true;
                //MainMap.Position = GPSSensor.lastLocation;//new PointLatLng(54.6961334816182, 25.2985095977783); // Lithuania, Vilnius
                //_currentMarker = new GMarkerGoogle(MainMap.Position, GMarkerGoogleType.red_small);
                //_currentMarker.IsHitTestVisible = false;
                //_top.Markers.Add(_currentMarker);
                if (_currentMarker.IsVisible)
                {
                    _currentMarker.Position = MainMap.FromLocalToLatLng(e.X, e.Y);

                    var px = MainMap.MapProvider.Projection.FromLatLngToPixel(_currentMarker.Position.Lat, _currentMarker.Position.Lng, (int)MainMap.Zoom);
                    var tile = MainMap.MapProvider.Projection.FromPixelToTileXY(px);

                    Debug.WriteLine("MouseDown: geo: " + _currentMarker.Position + " | px: " + px + " | tile: " + tile);
                }
            }
        }

        // move current marker with left holding
        void MainMap_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left && _isMouseDown)
            //{
            //    if (_curentRectMarker == null)
            //    {
            //        if (_currentMarker.IsVisible)
            //        {
            //            _currentMarker.Position = MainMap.FromLocalToLatLng(e.X, e.Y);
            //        }
            //    }
            //    else // move rect marker
            //    {
            //        var pnew = MainMap.FromLocalToLatLng(e.X, e.Y);

            //        var pIndex = (int?)_curentRectMarker.Tag;
            //        if (pIndex.HasValue)
            //        {
            //            if (pIndex < _polygon.Points.Count)
            //            {
            //                _polygon.Points[pIndex.Value] = pnew;
            //                MainMap.UpdatePolygonLocalPosition(_polygon);
            //            }
            //        }

            //        if (_currentMarker.IsVisible)
            //        {
            //            _currentMarker.Position = pnew;
            //        }
            //        _curentRectMarker.Position = pnew;

            //        if (_curentRectMarker.InnerMarker != null)
            //        {
            //            _curentRectMarker.InnerMarker.Position = pnew;
            //        }
            //    }

            //    MainMap.Refresh(); // force instant invalidation
            //}
        }

        // MapZoomChanged
        void MainMap_OnMapZoomChanged()
        {
            //trackBarZoom.Value = (int)(MainMap.Zoom * 100.0);
            //textBoxZoomCurrent.Text = MainMap.Zoom.ToString();
        }

        // click on some marker
        void MainMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    if (item is GMapMarkerRect)
            //    {
            //        GeoCoderStatusCode status;
            //        var pos = GMapProviders.GoogleMap.GetPlacemark(item.Position, out status);
            //        if (status == GeoCoderStatusCode.OK && pos != null)
            //        {
            //            var v = item as GMapMarkerRect;
            //            {
            //                v.ToolTipText = pos.Value.Address;
            //            }
            //            MainMap.Invalidate(false);
            //        }
            //    }
            //    else
            //    {
            //        if (item.Tag != null)
            //        {
            //            if (_currentTransport != null)
            //            {
            //                _currentTransport.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            //                _currentTransport = null;
            //            }
            //            _currentTransport = item;
            //            _currentTransport.ToolTipMode = MarkerTooltipMode.Always;
            //        }
            //    }
            //}
        }

        // loader start loading tiles
        void MainMap_OnTileLoadStart()
        {
            MethodInvoker m = delegate ()
            {
                // HACK JKU CLEAN
                //panelMenu.Text = "Menu: loading tiles...";
            };
            try
            {
                BeginInvoke(m);
            }
            catch
            {
            }
        }

        // loader end loading tiles
        void MainMap_OnTileLoadComplete(long elapsedMilliseconds)
        {
            // HACK JKU CLEAN
            //MainMap.ElapsedMilliseconds = elapsedMilliseconds;

            MethodInvoker m = delegate ()
            {

                // HACK JKU CLEAN
                //panelMenu.Text = "Menu, last load in " + MainMap.ElapsedMilliseconds + "ms";
                //textBoxMemory.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00} MB of {1:0.00} MB", MainMap.Manager.MemoryCache.Size, MainMap.Manager.MemoryCache.Capacity);
            };
            try
            {
                BeginInvoke(m);
            }
            catch
            {
            }
        }

        // current point changed
        void MainMap_OnPositionChanged(PointLatLng point)
        {
            //textBoxLatCurrent.Text = point.Lat.ToString(CultureInfo.InvariantCulture);
            //textBoxLngCurrent.Text = point.Lng.ToString(CultureInfo.InvariantCulture);

        }

        #endregion


        #region -- ui events --

        // change map type
        private void comboBoxMapType_DropDownClosed(object sender, EventArgs e)
        {
            //if (!_userAcceptedLicenseOnce)
            //{
            //    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "License.txt"))
            //    {
            //        string txt = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "License.txt");

            //        var d = new Demo.WindowsForms.Forms.Message();
            //        d.richTextBox1.Text = txt;

            //        if (DialogResult.Yes == d.ShowDialog())
            //        {
            //            _userAcceptedLicenseOnce = true;
            //            Text += " - license accepted by " + Environment.UserName + " at " + DateTime.Now;
            //        }
            //    }
            //    else
            //    {
            //        // user deleted License.txt ;}
            //        _userAcceptedLicenseOnce = true;
            //    }
            //}

            //if (_userAcceptedLicenseOnce)
            //{
            //    MainMap.MapProvider = comboBoxMapType.SelectedItem as GMapProvider;
            //}
            //else
            //{
            //    MainMap.MapProvider = GMapProviders.OpenStreetMap;
            //    comboBoxMapType.SelectedItem = MainMap.MapProvider;
            //}
        }

        // change mode
        private void comboBoxMode_DropDownClosed(object sender, EventArgs e)
        {
            //MainMap.Manager.Mode = (AccessMode)comboBoxMode.SelectedValue;
            //MainMap.ReloadMap();
        }

        // goto by geocoder
        private void textBoxGeo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((Keys)e.KeyChar == Keys.Enter)
            //{
            //    var status = MainMap.SetPositionByKeywords(textBoxGeo.Text);

            //    if (status != GeoCoderStatusCode.OK)
            //    {
            //        MessageBox.Show("Geocoder can't find: '" + textBoxGeo.Text + "', reason: " + status.ToString(), "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //}
        }

        // go to
        private void btnGoTo_Click(object sender, EventArgs e)
        {
            //var status = MainMap.SetPositionByKeywords(textBoxGeo.Text);

            //if (status != GeoCoderStatusCode.OK)
            //{
            //    MessageBox.Show("Geocoder can't find: '" + textBoxGeo.Text + "', reason: " + status.ToString(), "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        // reload map
        private void btnReload_Click(object sender, EventArgs e)
        {
            MainMap.ReloadMap();
        }

        // cache config
        private void checkBoxUseCache_CheckedChanged(object sender, EventArgs e)
        {
            //MainMap.Manager.UseRouteCache = checkBoxUseRouteCache.Checked;
            //MainMap.Manager.UseGeocoderCache = checkBoxUseRouteCache.Checked;
            //MainMap.Manager.UsePlacemarkCache = checkBoxUseRouteCache.Checked;
            //MainMap.Manager.UseDirectionsCache = checkBoxUseRouteCache.Checked;
        }

        // clear cache
        private void btnCacheClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You sure?", "Clear GMap.NET cache?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    MainMap.Manager.PrimaryCache.DeleteOlderThan(DateTime.Now, null);
                    MessageBox.Show("Done. Cache is clear.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // add test route
        private void btnAddRoute_Click(object sender, EventArgs e)
        {
            var rp = MainMap.MapProvider as RoutingProvider;
            if (rp == null)
            {
                rp = GMapProviders.OpenStreetMap; // use OpenStreetMap if provider does not implement routing
            }

            var route = rp.GetRoute(_start, _end, false, false, (int)MainMap.Zoom);
            if (route != null)
            {
                // add route
                var r = new GMapRoute(route.Points, route.Name);
                r.IsHitTestVisible = true;
                Routes.Routes.Add(r);

                // add route start/end marks
                GMapMarker m1 = new GMarkerGoogle(_start, GMarkerGoogleType.green_big_go);
                m1.ToolTipText = "Start: " + route.Name;
                m1.ToolTipMode = MarkerTooltipMode.Always;

                GMapMarker m2 = new GMarkerGoogle(_end, GMarkerGoogleType.red_big_stop);
                m2.ToolTipText = "End: " + _end.ToString();
                m2.ToolTipMode = MarkerTooltipMode.Always;

                Objects.Markers.Add(m1);
                Objects.Markers.Add(m2);

                MainMap.ZoomAndCenterRoute(r);
            }
        }

        // add marker on current position
        private void btnAddMarker_Click(object sender, EventArgs e)
        {
            var m = new GMarkerGoogle(_currentMarker.Position, GMarkerGoogleType.green_pushpin);
            //var mBorders = new GMapMarkerRect(_currentMarker.Position);
            //{
            //    mBorders.InnerMarker = m;
            //    if (_polygon != null)
            //    {
            //        mBorders.Tag = _polygon.Points.Count;
            //    }
            //    mBorders.ToolTipMode = MarkerTooltipMode.Always;
            //}

            Placemark? p = null;
            //if (checkBoxPlacemarkInfo.Checked)
            //{
            //    GeoCoderStatusCode status;
            //    var ret = GMapProviders.GoogleMap.GetPlacemark(_currentMarker.Position, out status);
            //    if (status == GeoCoderStatusCode.OK && ret != null)
            //    {
            //        p = ret;
            //    }
            //}

            //if (p != null)
            //{
            //    mBorders.ToolTipText = p.Value.Address;
            //}
            //else
            //{
            //    mBorders.ToolTipText = _currentMarker.Position.ToString();
            //}

            //Objects.Markers.Add(m);
            //Objects.Markers.Add(mBorders);

            RegeneratePolygon();
        }

        // clear markers, routes and polygons
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            Objects.Markers.Clear();
            Routes.Routes.Clear();
            Polygons.Polygons.Clear();
        }

        // show current marker
        private void checkBoxCurrentMarker_CheckedChanged(object sender, EventArgs e)
        {
            //_currentMarker.IsVisible = checkBoxCurrentMarker.Checked;
        }

        // can drag
        private void checkBoxCanDrag_CheckedChanged(object sender, EventArgs e)
        {
            //MainMap.CanDragMap = checkBoxCanDrag.Checked;
        }

        // set route start
        private void btnSetStart_Click(object sender, EventArgs e)
        {
            _start = _currentMarker.Position;
        }

        // set route end
        private void btnSetEnd_Click(object sender, EventArgs e)
        {
            _end = _currentMarker.Position;
        }

        // zoom to max for markers
        private void btnZoomCenter_Click(object sender, EventArgs e)
        {
            MainMap.ZoomAndCenterMarkers("objects");
        }

        // export map data
        private void btnExport_Click(object sender, EventArgs e)
        {
            MainMap.ShowExportDialog();
        }

        // import map data
        private void btnImport_Click(object sender, EventArgs e)
        {
            MainMap.ShowImportDialog();
        }

        // prefetch
        private void btnCachePrefetch_Click(object sender, EventArgs e)
        {
            var area = MainMap.SelectedArea;
            if (!area.IsEmpty)
            {
                for (int i = (int)MainMap.Zoom; i <= MainMap.MaxZoom; i++)
                {
                    var res = MessageBox.Show("Ready ripp at Zoom = " + i + " ?", "GMap.NET", MessageBoxButtons.YesNoCancel);

                    if (res == DialogResult.Yes)
                    {
                        using (var obj = new TilePrefetcher())
                        {
                            obj.Overlay = Objects; // set overlay if you want to see cache progress on the map

                            obj.Shuffle = MainMap.Manager.Mode != AccessMode.CacheOnly;

                            obj.Owner = this;
                            obj.ShowCompleteMessage = true;
                            obj.Start(area, i, MainMap.MapProvider, MainMap.Manager.Mode == AccessMode.CacheOnly ? 0 : 100, MainMap.Manager.Mode == AccessMode.CacheOnly ? 0 : 1);
                        }
                    }
                    else if (res == DialogResult.No)
                    {
                        continue;
                    }
                    else if (res == DialogResult.Cancel)
                    {
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Select map area holding ALT", "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // debug tile grid
        private void checkBoxDebug_CheckedChanged(object sender, EventArgs e)
        {
            //MainMap.ShowTileGridLines = checkBoxDebug.Checked;
        }

        // launch static map maker
        private void btnSave_Click(object sender, EventArgs e)
        {
            //var st = new StaticImage(this);
            //st.Owner = this;
            //st.Show();
        }

        // key-up events
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            int offset = -22;

            if (e.KeyCode == Keys.Left)
            {
                MainMap.Offset(-offset, 0);
            }
            else if (e.KeyCode == Keys.Right)
            {
                MainMap.Offset(offset, 0);
            }
            else if (e.KeyCode == Keys.Up)
            {
                MainMap.Offset(0, -offset);
            }
            else if (e.KeyCode == Keys.Down)
            {
                MainMap.Offset(0, offset);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (_currentPolygon != null)
                {
                    Polygons.Polygons.Remove(_currentPolygon);
                    _currentPolygon = null;
                }

                if (_currentRoute != null)
                {
                    Routes.Routes.Remove(_currentRoute);
                    _currentRoute = null;
                }

                //if (_curentRectMarker != null)
                //{
                //    Objects.Markers.Remove(_curentRectMarker);

                //    if (_curentRectMarker.InnerMarker != null)
                //    {
                //        Objects.Markers.Remove(_curentRectMarker.InnerMarker);
                //    }
                //    _curentRectMarker = null;

                //    RegeneratePolygon();
                //}
            }
            else if (e.KeyCode == Keys.Escape)
            {
                MainMap.Bearing = 0;
            }
        }

        // key-press events
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (MainMap.Focused)
            {
                if (e.KeyChar == '+')
                {
                    MainMap.Zoom = ((int)MainMap.Zoom) + 1;
                }
                else if (e.KeyChar == '-')
                {
                    MainMap.Zoom = ((int)(MainMap.Zoom + 0.99)) - 1;
                }
                else if (e.KeyChar == 'a')
                {
                    MainMap.Bearing--;
                }
                else if (e.KeyChar == 'z')
                {
                    MainMap.Bearing++;
                }
            }
        }

        // engage some live demo
        private void RealTimeChanged(object sender, EventArgs e)
        {
            Objects.Markers.Clear();
            Polygons.Polygons.Clear();
            Routes.Routes.Clear();

            // start performance test
            //if (radioButtonPerf.Checked)
            //{
            //    _timerPerf.Interval = 44;
            //    _timerPerf.Start();
            //}
            //else
            //{
            //    // stop performance test
            //    _timerPerf.Stop();
            //}

            // vehicle demo
            //if (radioButtonVehicle.Checked)
            //{
            //    if (!_transportWorker.IsBusy)
            //    {
            //        _firstLoadTrasport = true;
            //        _transportWorker.RunWorkerAsync();
            //    }
            //}
            //else
            //{
            //    if (_transportWorker.IsBusy)
            //    {
            //        _transportWorker.CancelAsync();
            //    }
            //}
        }

        // http://leafletjs.com/
        // Leaflet is a modern open-source JavaScript library for mobile-friendly interactive maps
        // Leaflet is designed with simplicity, performance and usability in mind. It works efficiently across all major desktop and mobile platforms out of the box
        private void checkBoxTileHost_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBoxTileHost.Checked)
            //{
            //    try
            //    {
            //        MainMap.Manager.EnableTileHost(8844);
            //        TryExtractLeafletjs();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("EnableTileHost: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //else
            //{
            //    MainMap.Manager.DisableTileHost();
            //}
        }

        #endregion

        #region -- UNUSED CODE --

        // zoom
        private void UNUSED_trackBarZoom_ValueChanged(object sender, EventArgs e)
        {
            //MainMap.Zoom = trackBarZoom.Value / 100.0;
        }

        private void UNUSED_btnZoomUp_Click(object sender, EventArgs e)
        {
            MainMap.Zoom = ((int)MainMap.Zoom) + 1;
        }

        private void UNUSED_btnZoomDown_Click(object sender, EventArgs e)
        {
            MainMap.Zoom = ((int)(MainMap.Zoom + 0.99)) - 1;
        }

        // saves current map view 
        private void UNUSED_btnSaveCurrentMapView_Click(object sender, EventArgs e)
        {
            try
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PNG (*.png)|*.png";
                    sfd.FileName = "GMap.NET image";

                    var tmpImage = MainMap.ToImage();
                    if (tmpImage != null)
                    {
                        using (tmpImage)
                        {
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                tmpImage.Save(sfd.FileName);

                                MessageBox.Show("Image saved: " + sfd.FileName, "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Image failed to save: " + ex.Message, "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // load gpx file
        private void UNUSED_btnLoadGpxFile(object sender, EventArgs e)
        {
            using (FileDialog dlg = new OpenFileDialog())
            {
                dlg.CheckPathExists = true;
                dlg.CheckFileExists = false;
                dlg.AddExtension = true;
                dlg.DefaultExt = "gpx";
                dlg.ValidateNames = true;
                dlg.Title = "GMap.NET: open gpx log";
                dlg.Filter = "gpx files (*.gpx)|*.gpx";
                dlg.FilterIndex = 1;
                dlg.RestoreDirectory = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string gpx = File.ReadAllText(dlg.FileName);

                        var r = MainMap.Manager.DeserializeGPX(gpx);
                        if (r != null)
                        {
                            if (r.trk.Length > 0)
                            {
                                foreach (var trk in r.trk)
                                {
                                    var points = new List<PointLatLng>();

                                    foreach (var seg in trk.trkseg)
                                    {
                                        foreach (var p in seg.trkpt)
                                        {
                                            points.Add(new PointLatLng((double)p.lat, (double)p.lon));
                                        }
                                    }

                                    var rt = new GMapRoute(points, string.Empty);
                                    {
                                        rt.Stroke = new Pen(Color.FromArgb(144, Color.Red));
                                        rt.Stroke.Width = 5;
                                        rt.Stroke.DashStyle = DashStyle.DashDot;
                                    }
                                    Routes.Routes.Add(rt);
                                }

                                MainMap.ZoomAndCenterRoutes(null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("GPX import: " + ex.ToString());
                        MessageBox.Show("Error importing gpx: " + ex.Message, "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }



        // open disk cache location
        private void UNUSED_btnOpenDiskCacheLocation_Click(object sender, EventArgs e)
        {
            try
            {
                string argument = "/select, \"" + MainMap.CacheLocation + "TileDBv5\"";
                Process.Start("explorer.exe", argument);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open: " + ex.Message, "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // export mobile gps log to gpx file
        private void buttonExportToGpx_Click(object sender, EventArgs e)
        {
            try
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "GPX (*.gpx)|*.gpx";
                    sfd.FileName = "mobile gps log";

                    DateTime? date = null;
                    DateTime? dateEnd = null;

                    //if (MobileLogFrom.Checked)
                    //{
                    //    date = MobileLogFrom.Value.ToUniversalTime();

                    //    sfd.FileName += " from " + MobileLogFrom.Value.ToString("yyyy-MM-dd HH-mm");
                    //}

                    //if (MobileLogTo.Checked)
                    //{
                    //    dateEnd = MobileLogTo.Value.ToUniversalTime();

                    //    sfd.FileName += " to " + MobileLogTo.Value.ToString("yyyy-MM-dd HH-mm");
                    //}

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var log = Stuff.GetRoutesFromMobileLog(_mobileGpsLog, date, dateEnd, 3.3);
                        if (log != null)
                        {
                            if (MainMap.Manager.ExportGPX(log, sfd.FileName))
                            {
                                MessageBox.Show("GPX saved: " + sfd.FileName, "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("GPX failed to save: " + ex.Message, "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainMap.Visible = true;
            InstrumentPanel.Visible = false;

        }

        private void dashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainMap.Visible = false;
            InstrumentPanel.Visible = true;
        }

        private void dashToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }
    }
}
