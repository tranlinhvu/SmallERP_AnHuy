using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;

namespace IMS.Model
{
    public class PurchaseOrderDetail
    {
        int purchaseOrderId;
        int pharmaId;
        int manuf;
        int unit;
        double unitPrice;
        double quantity;
        double amount;
        ulong expiry;
        string expiry1;
        int inventory;


        public PurchaseOrderDetail()
        {
            ;
        }

        public int Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public int PurchaseOrderId
        {
            get { return purchaseOrderId; }
            set { purchaseOrderId = value; }
        }

        public int PharmaId
        {
            get { return pharmaId; }
            set { pharmaId = value; }
        }

        public int Manuf
        {
            get { return manuf; }
            set { manuf = value; }
        }

        public int Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        public double UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public ulong Expiry
        {
            get { return expiry; }
            set { expiry = value; }
        }

        public string Expiry1
        {
            get { return expiry1; }
            set { expiry1 = value; }
        }

        public bool IsExisted()
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from PurchaseOrderDetail where PurchaseOrderId = @PurchaseOrderId and PharmaId = @PharmaId", conn);
                sqlCmd.Parameters.Add("@PurchaseOrderId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.purchaseOrderId;
                sqlCmd.Parameters[1].Value = this.pharmaId;

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

        public bool GetValueFromFields(string fieldName1, string dataType1, string FieldValue1, string fieldName2, string dataType2, string FieldValue2)
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                string sqlSelectString = "select * from PurchaseOrderDetail where " + fieldName1 + " = @FieldValue1 and ";
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

                    this.purchaseOrderId = int.Parse(rdr["PurchaseOrderId"].ToString());
                    this.pharmaId = int.Parse(rdr["PharmaId"].ToString());
                    this.manuf = int.Parse(rdr["Manuf"].ToString());
                    this.unit = int.Parse(rdr["Unit"].ToString());
                    this.unitPrice = double.Parse(rdr["UnitPrice"].ToString());
                    this.quantity = double.Parse(rdr["Quantity"].ToString());
                    this.amount = double.Parse(rdr["Amount"].ToString());
                    this.expiry = ulong.Parse(rdr["Expiry"].ToString());
                    this.expiry1 =rdr["Expiry1"].ToString();
                    this.inventory = int.Parse(rdr["Inventory"].ToString());

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
                string sqlInsert = "insert into PurchaseOrderDetail(" +
                    "PurchaseOrderId, PharmaId, Manuf, Unit, UnitPrice, Quantity, Amount, Expiry, Expiry1, Inventory)" +
                    " values(@PurchaseOrderId, @PharmaId, @Manuf, @Unit, @UnitPrice, @Quantity, @Amount, @Expiry, @Expiry1, @Inventory)";

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);
                sqlCmd.Parameters.Add("@PurchaseOrderId", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Manuf", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Unit", SqlDbType.Int);
                sqlCmd.Parameters.Add("@UnitPrice", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Quantity", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Amount", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@Expiry1", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Inventory", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.purchaseOrderId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.manuf;
                sqlCmd.Parameters[3].Value = this.unit;
                sqlCmd.Parameters[4].Value = this.unitPrice;
                sqlCmd.Parameters[5].Value = this.quantity;
                sqlCmd.Parameters[6].Value = this.amount;
                sqlCmd.Parameters[7].Value = this.expiry;
                sqlCmd.Parameters[8].Value = this.expiry1;
                sqlCmd.Parameters[9].Value = this.inventory;

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
                string sqlInsert = "insert into PurchaseOrderDetail(" +
                    "PurchaseOrderId, PharmaId, Manuf, Unit, UnitPrice, Quantity, Amount, Expiry, Expiry1, Inventory)" +
                    " values(@PurchaseOrderId, @PharmaId, @Manuf, @Unit, @UnitPrice, @Quantity, @Amount, @Expiry, @Expiry1, @Inventory)";

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);
                sqlCmd.Transaction = trans;
                
                sqlCmd.Parameters.Add("@PurchaseOrderId", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Manuf", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Unit", SqlDbType.Int);
                sqlCmd.Parameters.Add("@UnitPrice", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Quantity", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Amount", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@Expiry1", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Inventory", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.purchaseOrderId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.manuf;
                sqlCmd.Parameters[3].Value = this.unit;
                sqlCmd.Parameters[4].Value = this.unitPrice;
                sqlCmd.Parameters[5].Value = this.quantity;
                sqlCmd.Parameters[6].Value = this.amount;
                sqlCmd.Parameters[7].Value = this.expiry;
                sqlCmd.Parameters[8].Value = this.expiry1;
                sqlCmd.Parameters[9].Value = this.inventory;
           
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
                string sqlSelectString = "delete from PurchaseOrderDetail where " + fieldName + " = @FieldValue";

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
                SqlCommand sqlCmd = new SqlCommand("delete from PurchaseOrderDetail where PurchaseOrderId = @PurchaseOrderId and PharmaId = @PharmaId", conn);
                sqlCmd.Parameters.Add("@PurchaseOrderId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);                

                sqlCmd.Parameters[0].Value = this.purchaseOrderId;
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

        public bool DeletePurchaseOrderFromDB(SqlConnection conn, SqlTransaction trans)
        {
            int result;            

            try
            {
                SqlCommand sqlCmd = new SqlCommand("delete from PurchaseOrderDetail where PurchaseOrderId = @PurchaseOrderId", conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@PurchaseOrderId", SqlDbType.Int);                

                sqlCmd.Parameters[0].Value = this.purchaseOrderId;                

                result = sqlCmd.ExecuteNonQuery();
            }
            catch
            {
                //Close the connection
                //DBHelper.SqlDataConnection.CloseSqlConnection();
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

        public bool UpdateToDB()
        {
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            int result;
            try
            {
                string sqlUpdate = "update PurchaseOrderDetail set " +
                    "PurchaseOrderId = @PurchaseOrderId, PharmaId = @PharmaId, Manuf = @Manuf, Unit = @Unit, UnitPrice = @UnitPrice, " +
                    "Quantity = @Quantity, Amount = @Amount, Expiry = @Expiry, Expiry1 = @Expiry1 " +
                    "where PurchaseOrderId = @PurchaseOrderId and PharmaId = @PharmaId";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Parameters.Add("@PurchaseOrderId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Manuf", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Unit", SqlDbType.Int);
                sqlCmd.Parameters.Add("@UnitPrice", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Quantity", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Amount", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@Expiry1", SqlDbType.NVarChar);

                sqlCmd.Parameters[0].Value = this.purchaseOrderId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.manuf;
                sqlCmd.Parameters[3].Value = this.unit;
                sqlCmd.Parameters[4].Value = this.unitPrice;
                sqlCmd.Parameters[5].Value = this.quantity;
                sqlCmd.Parameters[6].Value = this.amount;
                sqlCmd.Parameters[7].Value = this.expiry;
                sqlCmd.Parameters[8].Value = this.expiry1;

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
            conn = DBHelper.SqlDataConnection.GetSqlConnection();
            int result;
            try
            {
                string sqlUpdate = "update PurchaseOrderDetail set " +
                    "PurchaseOrderId = @PurchaseOrderId, PharmaId = @PharmaId, Manuf = @Manuf, Unit = @Unit, UnitPrice = @UnitPrice, " +
                    "Quantity = @Quantity, Amount = @Amount,  Expiry = @Expiry, Expiry = @Expiry, Inventory = @Inventory" +
                    "where PurchaseOrderId = @PurchaseOrderId and PharmaId = @PharmaId";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@PurchaseOrderId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Manuf", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Unit", SqlDbType.Int);
                sqlCmd.Parameters.Add("@UnitPrice", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Quantity", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Amount", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@Expiry1", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Inventory", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.purchaseOrderId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.manuf;
                sqlCmd.Parameters[3].Value = this.unit;
                sqlCmd.Parameters[4].Value = this.unitPrice;
                sqlCmd.Parameters[5].Value = this.quantity;
                sqlCmd.Parameters[6].Value = this.amount;
                sqlCmd.Parameters[7].Value = this.expiry;
                sqlCmd.Parameters[8].Value = this.expiry1;
                sqlCmd.Parameters[9].Value = this.inventory;

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
    }
}