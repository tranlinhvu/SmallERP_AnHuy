using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;

namespace IMS.Model
{
    public class PrivilegeObject
    {
        int userGroup;
        string windowsObject;
        int privilege;

        public PrivilegeObject()
        {
            ;
        }

        public int UserGroup
        {
            get { return userGroup; }
            set { userGroup = value; }
        }


        public string WindowsObject
        {
            get { return windowsObject; }
            set { windowsObject = value; }
        }

        public int Privilege
        {
            get { return privilege; }
            set { privilege = value; }
        }

        public bool MoveToDB()
        {
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            int result;
            try
            {
                string sqlInsert = "insert into PrivilegeObject(" +
                    "UserGroup, WindowsObject, Privilege) values(@UserGroup, @WindowsObject, @Privilege)";

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);
                sqlCmd.Parameters.Add("@UserGroup", SqlDbType.Int);
                sqlCmd.Parameters.Add("@WindowsObject", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Privilege", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.userGroup;
                sqlCmd.Parameters[1].Value = this.windowsObject;
                sqlCmd.Parameters[2].Value = this.privilege;

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
                SqlCommand sqlCmd = new SqlCommand("delete from PrivilegeObject where UserGroup = @UserGroup and WindowsObject = @WindowsObject", conn);
                sqlCmd.Parameters.Add("@UserGroup", SqlDbType.Int);
                sqlCmd.Parameters.Add("@WindowsObject", SqlDbType.NVarChar);                

                sqlCmd.Parameters[0].Value = this.userGroup;
                sqlCmd.Parameters[1].Value = this.windowsObject;

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

        public bool DeleteAllFromDB()
        {
            int result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();

            try
            {
                SqlCommand sqlCmd = new SqlCommand("delete from PrivilegeObject where UserGroup = @UserGroup", conn);
                sqlCmd.Parameters.Add("@UserGroup", SqlDbType.Int);                

                sqlCmd.Parameters[0].Value = this.userGroup;               

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