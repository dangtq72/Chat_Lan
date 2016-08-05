using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace NaviCommon
{
    public class PostgresHelper
    {
        string connstring = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    "192.168.2.31", "5432", "postgres",
                    "nvsnvs", "trading");

        public static DataSet ExecuteDataset(string connectionString, string commandText)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(commandText, conn);
                var da = new Npgsql.NpgsqlDataAdapter(command);
                DataSet _ds = new DataSet();
                da.Fill(_ds);

                conn.Close();

                return _ds;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                if ((conn != null))
                    conn.Close();
                return new DataSet();
            }
            finally
            {
                if ((conn != null)) conn.Dispose();
            }
        }

        public static DataSet ExecuteDataset_Test(string connectionString, string commandText)
        {
            string szConnect = "DSN=192.168.2.31;" + "UID=postgres;" +  "PWD=nvsnvs";

            OdbcConnection cnDB = new OdbcConnection(szConnect);

            try
            {
                cnDB.Open();
            }
            catch (OdbcException ex)
            {
                Common.log.Error(ex.ToString());
                return new DataSet();
            }

            DataSet dsDB = new DataSet();
            OdbcDataAdapter adDB = new OdbcDataAdapter();
            OdbcCommandBuilder cbDB = new OdbcCommandBuilder(adDB);
            adDB.SelectCommand = new OdbcCommand( commandText, cnDB);
            adDB.Fill(dsDB);
            return dsDB;
        }

    }
}
