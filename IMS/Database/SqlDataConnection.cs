using System;
using System.Data;
using System.Data.SqlClient;
using SmartPOS.Configuration;
using System.Windows.Forms;
using System.Data.OleDb;
using SmartPOS.Utility;

namespace SmartPOS.Database
{
    public class SqlDataConnection
    {
        static SqlConnection connLocal;
        static SqlConnection connLocal1;
        static SqlConnection connLocal2;
        static SqlConnection connGlobal;

        string connString;        

        public SqlDataConnection()
        { }

        public SqlDataConnection(string connString)
        {
            this.connString = connString;
        }

        public static SqlConnection GetSqlConnection()
        {
            try
            {                
                ConfigManager configManager = ConfigManager.GetInstance();
                configManager.LoadConfigSetting();
                string connString;

                connString = "Data Source=" + configManager.MainServer + ";Initial Catalog=" + configManager.MainDatabase;
                connString += ";Persist Security Info=True;User ID=" + configManager.MainUserName + ";Password=" + configManager.MainPassword;

                connLocal = new SqlConnection(connString);

                connLocal.Open();
                return connLocal;
            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message, "Lỗi kết nối dữ liệu!");
                FileIO.WriteLogFile(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);//this.GetType()+"Login", ex.Message);
                return null;
            }
        }

        public static SqlConnection GetSqlConnection1()
        {
            try
            {
                ConfigManager configManager = ConfigManager.GetInstance();
                configManager.LoadConfigSetting();
                string connString;

                connString = "Data Source=" + configManager.MainServer + ";Initial Catalog=" + configManager.MainDatabase;
                connString += ";Persist Security Info=True;User ID=" + configManager.MainUserName + ";Password=" + configManager.MainPassword;

                connLocal1 = new SqlConnection(connString);

                connLocal1.Open();
                return connLocal1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi kết nối dữ liệu!");
                return null;
            }
        }

        public static SqlConnection GetSqlConnection2()
        {
            try
            {
                ConfigManager configManager = ConfigManager.GetInstance();
                configManager.LoadConfigSetting();
                string connString;

                connString = "Data Source=" + configManager.MainServer + ";Initial Catalog=" + configManager.MainDatabase;
                connString += ";Persist Security Info=True;User ID=" + configManager.MainUserName + ";Password=" + configManager.MainPassword;

                connLocal2 = new SqlConnection(connString);

                connLocal2.Open();
                return connLocal2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi kết nối dữ liệu!");
                return null;
            }
        }

        public static SqlConnection GetSqlConnectionGloble()
        {
            try
            {
                ConfigManager configManager = ConfigManager.GetInstance();
                configManager.LoadConfigSetting();
                string connString;

                connString = "Data Source=" + configManager.MainServer + ";Initial Catalog=" + configManager.MainDatabase;
                connString += ";Persist Security Info=True;User ID=" + configManager.MainUserName + ";Password=" + configManager.MainPassword;

                connGlobal = new SqlConnection(connString);

                connGlobal.Open();
                return connGlobal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi kết nối dữ liệu!");
                return null;
            }
        }

        public static void CloseSqlConnection()
        {
            if (connLocal != null)
            {
                connLocal.Close();
            }
        }

        public static void CloseSqlConnection1()
        {
            if (connLocal1 != null)
            {
                connLocal1.Close();
            }
        }

        public static void CloseSqlConnection2()
        {
            if (connLocal2 != null)
            {
                connLocal2.Close();
            }
        }

        public static void CloseSqlConnectionGlobal()
        {
            if (connGlobal != null)
            {
                connGlobal.Close();
            }
        }

        public static DataTable GetData(string sqlCommand)
        {
            DataTable table = new DataTable();
            try
            {
                SqlConnection conn = Database.SqlDataConnection.GetSqlConnection();

                SqlCommand sqlCmd = new SqlCommand(sqlCommand, conn);
                sqlCmd.CommandTimeout = 3600;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCmd;
                
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);                
            }
            catch(Exception ex)
            {
                Database.SqlDataConnection.CloseSqlConnection(); 
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Database.SqlDataConnection.CloseSqlConnection();                                
            }
            return table;
        }

        public static long GetTotalRecords(string sqlCommand)
        {
            long result = 0;
            SqlConnection conn = Database.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sqlCommand, conn);                               

                rdr = sqlCmd.ExecuteReader();                
                while (rdr.Read())
                {
                    result = long.Parse(rdr["TotalCount"].ToString());
                }
            }
            catch
            {
                return result;
            }
            finally
            {
                //close the sqlreader
                if (rdr != null)
                {
                    rdr.Close();
                }

                //Close the connection
                Database.SqlDataConnection.CloseSqlConnection();
            }
            return result;
        }

        public static DataTable GetData(SqlConnection conn, SqlTransaction trans, string sqlCommand)
        {
            try
            {                
                SqlCommand sqlCmd = new SqlCommand(sqlCommand, conn);
                sqlCmd.Transaction = trans;
                sqlCmd.CommandTimeout = 3600;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCmd;

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch
            {
                return null;
            }
        }

        public static DataSet GetDataSet(string sqlCommand)
        {
            try
            {

                SqlConnection conn = Database.SqlDataConnection.GetSqlConnection();

                SqlCommand command = new SqlCommand(sqlCommand, conn);
                command.CommandTimeout = 3600;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataSet table = new DataSet();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable ReadExcelContents(string fileName)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection();

                connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=Excel 12.0;"); //Excel 97-2003, .xls
                //string excelQuery = @"Select [Day],[Outlook],[temp],[Humidity],[Wind], [PlaySport]
                //   FROM [Sheet1$]";

                string excelQuery = @"Select * FROM [Sheet1$]";
                connection.Open();
                OleDbCommand cmd = new OleDbCommand(excelQuery, connection);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];


                //dataGridView1.DataSource = dt.DefaultView;
                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Program can't read file. " + ex.Message, "Please Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        internal void CreateDbBackup(string fileName)
        {
            using (SqlConnection con = Database.SqlDataConnection.GetSqlConnection())
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"BACKUP DATABASE [SmartMart] TO  DISK = N'{0}' WITH  INIT ,  NOUNLOAD ,  NOSKIP ,  STATS = 10,  NOFORMAT", fileName);

                cmd.ExecuteNonQuery();
            }
        }

        internal void RestoreDbFromBackup(string fileName)
        {
            using (SqlConnection con = Database.SqlDataConnection.GetSqlConnection())
            {
                SqlCommand cmd = con.CreateCommand();



                // Make sure to get exclusive access to DB to avoid any errors  
                cmd.CommandText = "USE MASTER ALTER DATABASE [SmartMart] SET SINGLE_USER With ROLLBACK IMMEDIATE";
                cmd.ExecuteNonQuery();

                cmd.CommandText = string.Format(@"RESTORE DATABASE [SmartMart] FROM  DISK = N'{0}' WITH  FILE = 1,  NOUNLOAD ,  STATS = 10,  RECOVERY ,  REPLACE", fileName);
                cmd.ExecuteNonQuery();
            }
        }
    }
}