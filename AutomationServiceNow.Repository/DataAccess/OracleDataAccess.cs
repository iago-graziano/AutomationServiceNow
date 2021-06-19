using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;

namespace AutomationServiceNow.Repository.DataAccess
{
    public class OracleDataAccess : IDisposable
    {
        public OracleConnection Connection = new OracleConnection();
        public OracleDataAccess()
        {
            OracleConnectionStringBuilder ocsb = new OracleConnectionStringBuilder();
            ocsb.Password = ConfigurationManager.AppSettings["oraclePassword"];
            ocsb.UserID = ConfigurationManager.AppSettings["oracleUser"];
            ocsb.DataSource = ConfigurationManager.AppSettings["oracleDataSource"];

            Connection.ConnectionString = ocsb.ConnectionString;
        }

        public void OpenConnection()
        {
            try
            {
                Connection.Open();
                Console.WriteLine("Connection established(" + Connection.ServerVersion + ")");
            }
            catch (Exception ex)
            {

            }
        }

        public void CloseConnection()
        {
            Connection.Close();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
