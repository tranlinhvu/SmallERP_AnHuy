using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;

namespace IMS.Model
{
    public class Vendor
    {
        int id;
        string name;
        string address;
        string taxNo;
        string country;
        string email;
        string phone;


        public Vendor()
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

        public string TaxNo
        {
            get { return taxNo; }
            set { taxNo = value; }
        }

        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public bool IsExisted()
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from Vendor where Id = @Id", conn);
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
                SqlCommand sqlCmd = new SqlCommand("select * from Vendor where " + fieldName + " = @FieldValue", conn);
                sqlCmd.Parameters.Add("@FieldValue", dataType);
                sqlCmd.Parameters[0].Value = value;

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {    
                    this.id = int.Parse(rdr["Id"].ToString());
                    this.name = rdr["Name"].ToString();
                    this.address = rdr["Address"].ToString();
                    this.taxNo = rdr["TaxNo"].ToString();                    
                    this.country = rdr["Country"].ToString();
                    this.email = rdr["Email"].ToString();
                    this.phone = rdr["Phone"].ToString();

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
                string sqlInsert = "insert into Vendor(" +
                    "Name, Address, TaxNo, Country, Email, Phone)" +
                    " values(@Name, @Address, @TaxNo, @Country, @Email, @Phone)";

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);
                sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Address", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@TaxNo", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Country", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Phone", SqlDbType.NVarChar);

                sqlCmd.Parameters[0].Value = this.name;
                sqlCmd.Parameters[1].Value = this.address;
                sqlCmd.Parameters[2].Value = this.taxNo;
                sqlCmd.Parameters[3].Value = this.country;
                sqlCmd.Parameters[4].Value = this.email;
                sqlCmd.Parameters[5].Value = this.phone;

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
                SqlCommand sqlCmd = new SqlCommand("select * from Vendor where " + fieldName + " = @FieldValue", conn);
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
                SqlCommand sqlCmd = new SqlCommand("delete from Vendor where Id = @Id", conn);
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
                string sqlUpdate = "update Vendor set " +
                    "Name = @Name, Address = @Address, TaxNo = @TaxNo, Country = @Country, " +
                    "Email = @Email, Phone = @Phone where Id = @Id";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Parameters.Add("@Id", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Address", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@TaxNo", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Country", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Phone", SqlDbType.NVarChar);

                sqlCmd.Parameters[0].Value = this.id;
                sqlCmd.Parameters[1].Value = this.name;
                sqlCmd.Parameters[2].Value = this.address;
                sqlCmd.Parameters[3].Value = this.taxNo;
                sqlCmd.Parameters[4].Value = this.country;
                sqlCmd.Parameters[5].Value = this.email;
                sqlCmd.Parameters[6].Value = this.phone;

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
                string sqlUpdate = "update Vendor set " +
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