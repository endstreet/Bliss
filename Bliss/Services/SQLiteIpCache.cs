
namespace Bliss
{
    using System;
    using System.Data.Common;
    using System.Data.SQLite;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// ultra fast cache system for tiles
    /// </summary>
    internal class SQLiteIpCache
    {
        string _cache;
        string _ipCache;
        string _db;

        public string IpCache
        {
            get
            {
                return _ipCache;
            }
        }

        /// <summary>
        /// local cache location
        /// </summary>
        public string CacheLocation
        {
            get
            {
                return _cache;
            }
            set
            {
                _cache = value;
                _ipCache = Path.Combine(_cache, "IpGeoCacheDB") + Path.DirectorySeparatorChar;

                // make empty db
                {
                    _db = _ipCache + "Data.ipdb";

                    if (!File.Exists(_db))
                    {
                        CreateEmptyDB(_db);
                    }
                }
            }
        }

        public static bool CreateEmptyDB(string file)
        {
            bool ret = true;

            try
            {
                string dir = Path.GetDirectoryName(file);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (SQLiteConnection cn = new SQLiteConnection())
                {
                    cn.ConnectionString = string.Format("Data Source=\"{0}\";FailIfMissing=False;", file);
                    cn.Open();
                    {
                        using (DbTransaction tr = cn.BeginTransaction())
                        {
                            try
                            {
                                using (DbCommand cmd = cn.CreateCommand())
                                {
                                    cmd.Transaction = tr;
                                    cmd.CommandText = Properties.Resources.IpCacheCreateDb;
                                    cmd.ExecuteNonQuery();
                                }
                                tr.Commit();
                            }
                            catch (Exception exx)
                            {
                                Console.WriteLine("CreateEmptyDB: " + exx.ToString());
                                Debug.WriteLine("CreateEmptyDB: " + exx.ToString());

                                tr.Rollback();
                                ret = false;
                            }
                        }
                        cn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CreateEmptyDB: " + ex.ToString());
                ret = false;
            }
            return ret;
        }

        public bool PutDataToCache(string ip, IpInfo data)
        {
            bool ret = true;
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection())
                {
                    cn.ConnectionString = string.Format("Data Source=\"{0}\";", _db);
                    cn.Open();
                    {
                        {
                            using (DbTransaction tr = cn.BeginTransaction())
                            {
                                try
                                {
                                    using (DbCommand cmd = cn.CreateCommand())
                                    {
                                        cmd.Transaction = tr;

                                        cmd.CommandText = "INSERT INTO Cache(Ip, CountryName, RegionName, City, Latitude, Longitude, Time) VALUES(@p1, @p2, @p3, @p4, @p5, @p6, @p7)";

                                        cmd.Parameters.Add(new SQLiteParameter("@p1", ip));
                                        cmd.Parameters.Add(new SQLiteParameter("@p2", data.CountryName));
                                        cmd.Parameters.Add(new SQLiteParameter("@p3", data.RegionName));
                                        cmd.Parameters.Add(new SQLiteParameter("@p4", data.City));
                                        cmd.Parameters.Add(new SQLiteParameter("@p5", data.Latitude));
                                        cmd.Parameters.Add(new SQLiteParameter("@p6", data.Longitude));
                                        cmd.Parameters.Add(new SQLiteParameter("@p7", data.CacheTime));

                                        cmd.ExecuteNonQuery();
                                    }
                                    tr.Commit();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("PutDataToCache: " + ex.ToString());

                                    Debug.WriteLine("PutDataToCache: " + ex.ToString());

                                    tr.Rollback();
                                    ret = false;
                                }
                            }
                        }
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("PutDataToCache: " + ex.ToString());
                ret = false;
            }
            return ret;
        }

        public IpInfo GetDataFromCache(string ip)
        {
            IpInfo ret = null;
            try
            {
                using (SQLiteConnection cn = new SQLiteConnection())
                {
                    cn.ConnectionString = string.Format("Data Source=\"{0}\";", _db);
                    cn.Open();
                    {
                        using (DbCommand com = cn.CreateCommand())
                        {
                            com.CommandText = "SELECT * FROM Cache WHERE Ip = '" + ip + "'";

                            using (DbDataReader rd = com.ExecuteReader())
                            {
                                if (rd.Read())
                                {
                                    IpInfo val = new IpInfo();
                                    {
                                        val.Ip = ip;
                                        val.CountryName = rd["CountryName"] as string;
                                        val.RegionName = rd["RegionName"] as string;
                                        val.City = rd["City"] as string;
                                        val.Latitude = (double)rd["Latitude"];
                                        val.Longitude = (double)rd["Longitude"];
                                        val.CacheTime = (DateTime)rd["Time"];
                                    }
                                    ret = val;
                                }
                                rd.Close();
                            }
                        }
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetDataFromCache: " + ex.ToString());
                ret = null;
            }

            return ret;
        }
    }
}
