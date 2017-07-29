using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.OleDb;
using IMS.Favorite;

namespace IMS.DBHelper
{
    public class SqlDataConnection
    {
        static SqlConnection connLocal;        

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
                
                string connString;

                connString = IMS.Properties.Settings.Default.SmallERPConnectionString;

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
        
        public static void CloseSqlConnection()
        {
            if (connLocal != null)
            {
                connLocal.Close();
            }
        }        

        public static DataTable GetData(string sqlCommand)
        {
            DataTable table = new DataTable();
            try
            {
                SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();

                SqlCommand sqlCmd = new SqlCommand(sqlCommand, conn);
                sqlCmd.CommandTimeout = 3600;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = sqlCmd;
                
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);                
            }
            catch(Exception ex)
            {
                DBHelper.SqlDataConnection.CloseSqlConnection(); 
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DBHelper.SqlDataConnection.CloseSqlConnection();                                
            }
            return table;
        }

        public static long GetTotalRecords(string sqlCommand)
        {
            long result = 0;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
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
                DBHelper.SqlDataConnection.CloseSqlConnection();
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

                SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();

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

        private static string ExcelConnection(string fileName)
        {
            return @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                   @"Data Source='" + fileName + "';" +
                   @"Extended Properties= 'Excel 15.0';";
        }
        public static DataTable ReadExcelContents(string fileName)
        {
            try
            {
                string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + fileName + "';Extended Properties=\"Excel 12.0\";";
                //OleDbConnection connection = new OleDbConnection(excelConnectionString);
                OleDbConnection connection = new OleDbConnection(excelConnectionString);
                //string excelQuery = @"Select [Day],[Outlook],[temp],[Humidity],[Wind], [PlaySport]
                //   FROM [Sheet1$]";
                
                connection.Open();
                string excelQuery = @"Select * FROM [Sheet1$]";
                OleDbCommand cmd = new OleDbCommand(excelQuery, connection);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];


                //dataGridView1.DataSource = dt.DefaultView;
                connection.Close();
                return dt;

                //string filename = @"C:\abc.xlsx";

                //string connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES\";", filename);
                //string query = String.Format(@"Select * FROM [Sheet1$]");
                //OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
                //DataSet dataSet = new DataSet();
                //dataAdapter.Fill(dataSet);
                //DataTable YourTable = dataSet.Tables[0];
                //return YourTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Program can't read file. " + ex.Message, "Please Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        internal void CreateDbBackup(string fileName)
        {
            using (SqlConnection con = DBHelper.SqlDataConnection.GetSqlConnection())
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = string.Format(@"BACKUP DATABASE [SmallERP] TO  DISK = N'{0}' WITH  INIT ,  NOUNLOAD ,  NOSKIP ,  STATS = 10,  NOFORMAT", fileName);

                cmd.ExecuteNonQuery();
            }
        }

        internal void RestoreDbFromBackup(string fileName)
        {
            using (SqlConnection con = DBHelper.SqlDataConnection.GetSqlConnection())
            {
                SqlCommand cmd = con.CreateCommand();



                // Make sure to get exclusive access to DB to avoid any errors  
                cmd.CommandText = "USE MASTER ALTER DATABASE [SmallERP] SET SINGLE_USER With ROLLBACK IMMEDIATE";
                cmd.ExecuteNonQuery();

                cmd.CommandText = string.Format(@"RESTORE DATABASE [SmallERP] FROM  DISK = N'{0}' WITH  FILE = 1,  NOUNLOAD ,  STATS = 10,  RECOVERY ,  REPLACE", fileName);
                cmd.ExecuteNonQuery();
            }
        }
    }
}