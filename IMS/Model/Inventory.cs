using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using IMS.DBHelper;
using System.Data;

namespace IMS.Model
{
    class Inventory
    {
        int id;
        string name;
        string address;

        public Inventory()
        {
            ;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public bool IsExisted()
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from Inventory where Id = @Id", conn);
                sqlCmd.Parameters.Add("@Id", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.id;

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
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

        public bool GetValueFromField(string fieldName, string dataType, string value)
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from Inventory where " + fieldName + " = @FieldValue", conn);
                sqlCmd.Parameters.Add("@FieldValue", dataType);
                sqlCmd.Parameters[0].Value = value;

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
                    this.id = int.Parse(rdr["Id"].ToString());                    
                    this.name = rdr["Name"].ToString();
                    this.address = rdr["Address"].ToString();                   

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

        public bool MoveToDB()
        {
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            int result;
            try
            {
                string sqlInsert = "insert into Inventory(" +
                    "Name, address)" +
                    " values(@Name, @address)";

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);                
                sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@address", SqlDbType.NVarChar);
                
                sqlCmd.Parameters[0].Value = this.name;
                sqlCmd.Parameters[1].Value = this.address;                

                result = sqlCmd.ExecuteNonQuery();
            }
            catch
            {
                //Close the connection
                DBHelper.SqlDataConnection.CloseSqlConnection();
                return false;
            }
            finally
            {
                //Close the connection
                DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            if (result > 0) return true;
            else return false;
        }

        public bool DeleteDBFromField(string fieldName, string dataType, string value)
        {
            int result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from Inventory where " + fieldName + " = @FieldValue", conn);
                sqlCmd.Parameters.Add("@FieldValue", dataType);
                sqlCmd.Parameters[0].Value = value;

                result = sqlCmd.ExecuteNonQuery();
            }
            catch
            {
                //Close the connection
                DBHelper.SqlDataConnection.CloseSqlConnection();
                return false;
            }
            finally
            {
                //Close the connection
                DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            if (result > 0) return true;
            else return false;
        }

        public bool DeleteFromDB()
        {
            int result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();

            try
            {
                SqlCommand sqlCmd = new SqlCommand("delete from Inventory where Id = @Id", conn);
                sqlCmd.Parameters.Add("@Id", SqlDbType.Int);
                sqlCmd.Parameters[0].Value = this.id;

                result = sqlCmd.ExecuteNonQuery();
            }
            catch
            {
                //Close the connection
                DBHelper.SqlDataConnection.CloseSqlConnection();
                return false;
            }
            finally
            {
                //Close the connection
                DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            if (result > 0) return true;
            else return false;
        }

        public bool UpdateToDB()
        {
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            int result;
            try
            {
                string sqlUpdate = "update Inventory set " +
                    "Name = @Name, Address = @Address where Id = @Id";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Parameters.Add("@Id", SqlDbType.Int);                
                sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Address", SqlDbType.NVarChar);                

                sqlCmd.Parameters[0].Value = this.id;                
                sqlCmd.Parameters[1].Value = this.name;
                sqlCmd.Parameters[2].Value = this.address;               

                result = sqlCmd.ExecuteNonQuery();
            }
            catch
            {
                //Close the connection
                DBHelper.SqlDataConnection.CloseSqlConnection();
                return false;
            }
            finally
            {
                //Close the connection
                DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            if (result > 0) return true;
            else return false;
        }

        public bool UpdateDBFromField(string fieldName, string value, string dataType)
        {
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            int result;
            try
            {
                string sqlUpdate = "update Inventory set " +
                    fieldName + " = @FieldValue where Id = @Id";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Parameters.Add("@Id", SqlDbType.Int);
                sqlCmd.Parameters.Add("@FieldValue", dataType);

                sqlCmd.Parameters[0].Value = this.id;
                sqlCmd.Parameters[1].Value = value;

                result = sqlCmd.ExecuteNonQuery();
            }
            catch
            {
                //Close the connection
                DBHelper.SqlDataConnection.CloseSqlConnection();
                return false;
            }
            finally
            {
                //Close the connection
                DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            if (result > 0) return true;
            else return false;
        }
    }
}
