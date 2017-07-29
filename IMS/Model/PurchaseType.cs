using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;

namespace IMS.Model
{
    public class PurchaseType
    {
        static int id;
        static string name;
        static string sign;

        public PurchaseType()
        {
            ;
        }

        static public int Id
        {
            get { return id; }
            set { id = value; }
        }

        static public string Name
        {
            get { return name; }
            set { name = value; }
        }

        static public string Sign
        {
            get { return sign; }
            set { sign = value; }
        }

        static public bool IsExisted()
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from PurchaseType where Id = @Id", conn);
                sqlCmd.Parameters.Add("@Id", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = id;

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
                    id = int.Parse(rdr["Id"].ToString());
                    name = rdr["Name"].ToString();
                    sign = rdr["Sign"].ToString();
                    result = true;
                }
            }
            catch
            {
                return false;
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
    }
}