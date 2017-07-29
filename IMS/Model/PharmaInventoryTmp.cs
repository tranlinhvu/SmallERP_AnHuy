using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;

namespace IMS.Model
{
    public class PharmaInventoryTmp
    {
        int invId;
        int pharmaId;
        int inputNum;
        int outputNum;
        int available;
        int minInStock;
        int maxInStock;
        ulong expiry;

        public PharmaInventoryTmp()
        {
            ;
        }

        public int InvId
        {
            get { return invId; }
            set { invId = value; }
        }

        public int PharmaId
        {
            get { return pharmaId; }
            set { pharmaId = value; }
        }

        public ulong Expiry
        {
            get { return expiry; }
            set { expiry = value; }
        }

        public int InputNum
        {
            get { return inputNum; }
            set { inputNum = value; }
        }

        public int OutputNum
        {
            get { return outputNum; }
            set { outputNum = value; }
        }

        public int Available
        {
            get { return available; }
            set { available = value; }
        }

        public int MinInStock
        {
            get { return minInStock; }
            set { minInStock = value; }
        }

        public int MaxInStock
        {
            get { return maxInStock; }
            set { maxInStock = value; }
        }

        public bool IsExisted()
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from PharmaInventoryTmp where InvId = @InvId and PharmaId = @PharmaId and Expiry = @Expiry", conn);
                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
                    this.invId = int.Parse(rdr["InvId"].ToString());
                    this.pharmaId = int.Parse(rdr["PharmaId"].ToString());
                    this.expiry = ulong.Parse(rdr["Expiry"].ToString());
                    this.inputNum = int.Parse(rdr["InputNum"].ToString());
                    this.outputNum = int.Parse(rdr["OutputNum"].ToString());
                    this.available = int.Parse(rdr["Available"].ToString());
                    this.minInStock = int.Parse(rdr["MinInStock"].ToString());
                    this.maxInStock = int.Parse(rdr["MaxInStock"].ToString());

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

        public bool IsNotSafeInventory()
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from PharmaInventoryTmp where Available = 0", conn);

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
                    result = true;
                    break;

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

        public bool GetValueFromFields(string fieldName1, string dataType1, string FieldValue1, string fieldName2, string dataType2, string FieldValue2)
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                string sqlSelectString = "select * from PharmaInventoryTmp where " + fieldName1 + " = @FieldValue1 and ";
                sqlSelectString += fieldName2 + " = @FieldValue2";

                SqlCommand sqlCmd = new SqlCommand(sqlSelectString, conn);
                sqlCmd.Parameters.Add("@FieldValue1", dataType1);
                sqlCmd.Parameters.Add("@FieldValue2", dataType2);
                sqlCmd.Parameters[0].Value = FieldValue1;
                sqlCmd.Parameters[0].Value = FieldValue2;

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
                    this.invId = int.Parse(rdr["InvId"].ToString());
                    this.pharmaId = int.Parse(rdr["PharmaId"].ToString());
                    this.expiry = ulong.Parse(rdr["Expiry"].ToString());
                    this.inputNum = int.Parse(rdr["InputNum"].ToString());
                    this.outputNum = int.Parse(rdr["OutputNum"].ToString());
                    this.available = int.Parse(rdr["Available"].ToString());
                    this.minInStock = int.Parse(rdr["MinInStock"].ToString());
                    this.maxInStock = int.Parse(rdr["MaxInStock"].ToString());

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
                string sqlInsert = "insert into PharmaInventoryTmp(" +
                    "InvId, PharmaId, Expiry, InputNum, OutputNum, Available, MinInStock, MaxInStock)" +
                    " values(@InvId, @PharmaId, @Expiry, @InputNum, @OutputNum, @Available, @MinInStock, @MaxInStock)";

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);
                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@InputNum", SqlDbType.Int);
                sqlCmd.Parameters.Add("@OutputNum", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Available", SqlDbType.Int);
                sqlCmd.Parameters.Add("@MinInStock", SqlDbType.Int);
                sqlCmd.Parameters.Add("@MaxInStock", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.inputNum;
                sqlCmd.Parameters[4].Value = this.outputNum;
                sqlCmd.Parameters[5].Value = this.available;
                sqlCmd.Parameters[6].Value = this.minInStock;
                sqlCmd.Parameters[7].Value = this.maxInStock;

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

        public bool MoveToDB(SqlConnection conn, SqlTransaction trans)
        {
            int result;
            try
            {
                string sqlInsert = "insert into PharmaInventoryTmp(" +
                    "InvId, PharmaId, Expiry, InputNum, OutputNum, Available, MinInStock, MaxInStock)" +
                    " values(@InvId, @PharmaId, @Expiry, @InputNum, @OutputNum, @Available, @MinInStock, @MaxInStock)";

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@InputNum", SqlDbType.Int);
                sqlCmd.Parameters.Add("@OutputNum", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Available", SqlDbType.Int);
                sqlCmd.Parameters.Add("@MinInStock", SqlDbType.Int);
                sqlCmd.Parameters.Add("@MaxInStock", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.inputNum;
                sqlCmd.Parameters[4].Value = this.outputNum;
                sqlCmd.Parameters[5].Value = this.available;
                sqlCmd.Parameters[6].Value = this.minInStock;
                sqlCmd.Parameters[7].Value = this.maxInStock;

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
                //DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            if (result > 0) return true;
            else return false;
        }

        public bool DeleteDBFromField(string fieldName, string dataType, string FieldValue)
        {
            int result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            try
            {
                string sqlSelectString = "delete from PharmaInventoryTmp where " + fieldName + " = @FieldValue";

                SqlCommand sqlCmd = new SqlCommand(sqlSelectString, conn);
                sqlCmd.Parameters.Add("@FieldValue", dataType);
                sqlCmd.Parameters[0].Value = FieldValue;

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
                SqlCommand sqlCmd = new SqlCommand("delete from PharmaInventoryTmp where InvId = @InvId and PharmaId = @PharmaId and Expiry = @Expiry", conn);
                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;

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
                string sqlUpdate = "update PharmaInventoryTmp set " +
                    "InvId = @InvId, PharmaId = @PharmaId, Expiry = @Expiry, InputNum = @InputNum, OutputNum = @OutputNum, " +
                    "Available = @Available, MinInStock = @MinInStock, MaxInStock = @MaxInStock " +
                    "where InvId = @InvId and PharmaId = @PharmaId and Expiry = @Expiry";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@InputNum", SqlDbType.Int);
                sqlCmd.Parameters.Add("@OutputNum", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Available", SqlDbType.Int);
                sqlCmd.Parameters.Add("@MinInStock", SqlDbType.Int);
                sqlCmd.Parameters.Add("@MaxInStock", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.inputNum;
                sqlCmd.Parameters[4].Value = this.outputNum;
                sqlCmd.Parameters[5].Value = this.available;
                sqlCmd.Parameters[6].Value = this.minInStock;
                sqlCmd.Parameters[7].Value = this.maxInStock;

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

        public bool UpdateToDB(SqlConnection conn, SqlTransaction trans)
        {
            int result;
            try
            {
                string sqlUpdate = "update PharmaInventoryTmp set " +
                    "InvId = @InvId, PharmaId = @PharmaId, Expiry = @Expiry, InputNum = @InputNum, OutputNum = @OutputNum, " +
                    "Available = @Available, MinInStock = @MinInStock, MaxInStock = @MaxInStock " +
                    "where InvId = @InvId and PharmaId = @PharmaId and Expiry = @Expiry";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@InputNum", SqlDbType.Int);
                sqlCmd.Parameters.Add("@OutputNum", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Available", SqlDbType.Int);
                sqlCmd.Parameters.Add("@MinInStock", SqlDbType.Int);
                sqlCmd.Parameters.Add("@MaxInStock", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.inputNum;
                sqlCmd.Parameters[4].Value = this.outputNum;
                sqlCmd.Parameters[5].Value = this.available;
                sqlCmd.Parameters[6].Value = this.minInStock;
                sqlCmd.Parameters[7].Value = this.maxInStock;

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
                //DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            if (result > 0) return true;
            else return false;
        }

        public bool UpdateFieldToDB(string fieldName, string dataType, string fieldValue)
        {
            int result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();

            try
            {

                string sqlUpdate = "update PharmaInventoryTmp set " + fieldName + " = @FieldValue ";
                sqlUpdate += "where InvId = @InvId and PharmaId = @PharmaId and Expiry = @Expiry";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@FieldValue", dataType);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = fieldValue;

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