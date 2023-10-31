using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace LiveDLL
{
    class ConnManager
    {
        static ConnManager instance = null;
        static readonly object padlock = new object();
        static readonly object OpenConnLock = new object();

        public int User_Id = 0;

        private bool _OldDB = false;

        public static ConnManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ConnManager();
                    }
                    return instance;
                }
            }
        }

        public String StringConexao = LiveDLL.Properties.Settings.Default.connString;
        public bool isConnected = false;
        public DateTime LastOpenTime = new DateTime(1900, 01, 01);

        private SqlConnection _ConexaoDB;

        public SqlConnection ConexaoDB
        {
            get
            {
                TimeSpan span = DateTime.Now - LastOpenTime;
                if (span.TotalMinutes > 30 && LastOpenTime != new DateTime(1900, 01, 01))
                {
                    if (_ConexaoDB != null)
                    {
                        try
                        {
                            _ConexaoDB.Close();
                            _ConexaoDB.Dispose();
                            _ConexaoDB = null;
                        }
                        catch { }
                    }
                    OpenConnection(_OldDB);
                }
                return _ConexaoDB;
            }
        }

        public bool OpenConnection(bool UseOldConn)
        {
            _OldDB = UseOldConn;

            if (_OldDB)
            {
                StringConexao = LiveDLL.Properties.Settings.Default.connString;
                StringConexao = StringConexao.Replace("NestSrv06", "NestSrv02");
                StringConexao = StringConexao.Replace("NESTDB", "NESTSIM");
            }
            else
            {
                StringConexao = LiveDLL.Properties.Settings.Default.connString;
            }

            lock (OpenConnLock)
            {
                if (_ConexaoDB == null)
                {
                    _ConexaoDB = new SqlConnection(StringConexao);
                }
                else
                {
                    int waitConnect = 0;

                    while (_ConexaoDB.State == ConnectionState.Connecting)
                    {
                        waitConnect++;
                    }
                }

                try
                {
                    if (_ConexaoDB.State != ConnectionState.Open)
                    {
                        _ConexaoDB.Open();
                        LastOpenTime = DateTime.Now;
                    }
                    isConnected = true;
                    return true;
                }
                catch
                {
                    isConnected = false;
                    return false;
                }
            }
        }

        public void ReOpenConnection()
        {
            lock (OpenConnLock)
            {
                if (_ConexaoDB == null)
                {
                    _ConexaoDB = new SqlConnection(StringConexao);
                }
                else
                {
                    int waitConnect = 0;

                    while (_ConexaoDB.State == ConnectionState.Connecting)
                    {
                        waitConnect++;
                    }

                    if (_ConexaoDB.State != ConnectionState.Closed)
                    {
                        _ConexaoDB.Close();
                    }
                }

                OpenConnection(_OldDB);
            }
        }

    }

    class NestSrv02Conn
    {
        static NestSrv02Conn instance = null;
        static readonly object padlock = new object();
        static readonly object OpenConnLock = new object();

        public int User_Id = 0;

        public static NestSrv02Conn Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new NestSrv02Conn();
                    }
                    return instance;
                }
            }
        }

        public String StringConexao = LiveDLL.Properties.Settings.Default.NestSrv02;
        public bool isConnected = false;
        public DateTime LastOpenTime = new DateTime(1900, 01, 01);

        private SqlConnection _ConexaoDB;

        public SqlConnection ConexaoDB
        {
            get
            {
                TimeSpan span = DateTime.Now - LastOpenTime;
                if (span.TotalMinutes > 30 && LastOpenTime != new DateTime(1900, 01, 01))
                {
                    _ConexaoDB.Close();
                    _ConexaoDB.Dispose();
                    _ConexaoDB = null;

                    OpenConnection();
                }
                return _ConexaoDB;
            }
        }

        public bool OpenConnection()
        {
            lock (OpenConnLock)
            {
                if (_ConexaoDB == null)
                {
                    _ConexaoDB = new SqlConnection(StringConexao);
                }
                else
                {
                    int waitConnect = 0;

                    while (_ConexaoDB.State == ConnectionState.Connecting)
                    {
                        waitConnect++;
                    }
                }

                try
                {
                    if (_ConexaoDB.State != ConnectionState.Open)
                    {
                        _ConexaoDB.Open();
                        LastOpenTime = DateTime.Now;
                    }
                    isConnected = true;
                    return true;
                }
                catch
                {
                    isConnected = false;
                    return false;
                }
            }
        }

        public void ReOpenConnection()
        {
            lock (OpenConnLock)
            {
                if (_ConexaoDB == null)
                {
                    _ConexaoDB = new SqlConnection(StringConexao);
                }
                else
                {
                    int waitConnect = 0;

                    while (_ConexaoDB.State == ConnectionState.Connecting)
                    {
                        waitConnect++;
                    }

                    if (_ConexaoDB.State != ConnectionState.Closed)
                    {
                        _ConexaoDB.Close();
                    }
                }

                OpenConnection();
            }
        }
    }
}
