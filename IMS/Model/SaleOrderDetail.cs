using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;

namespace IMS.Model
{
    public class SaleOrderDetail
    {
        int saleOrderId;
        int pharmaId;
        int unit;
        double unitPrice;
        double quantity;
        double amount;
        int inventory;
        double purchaseUnitPrice;
        ulong expiry;
        string expiry1;

        public SaleOrderDetail()
        {
            ;
        }

        public int SaleOrderId
        {
            get { return saleOrderId; }
            set { saleOrderId = value; }
        }

        public int Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public int PharmaId
        {
            get { return pharmaId; }
            set { pharmaId = value; }
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

        public double PurchaseUnitPrice
        {
            get { return purchaseUnitPrice; }
            set { purchaseUnitPrice = value; }
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
                SqlCommand sqlCmd = new SqlCommand("select * from SaleOrderDetail where SaleOrderId = @SaleOrderId and PharmaId = @PharmaId", conn);
                sqlCmd.Parameters.Add("@SaleOrderId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.saleOrderId;
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
                string sqlSelectString = "select * from SaleOrderDetail where " + fieldName1 + " = @FieldValue1 and ";
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

                    this.saleOrderId = int.Parse(rdr["SaleOrderId"].ToString());
                    this.pharmaId = int.Parse(rdr["PharmaId"].ToString());
                    this.unit = int.Parse(rdr["Unit"].ToString());
                    this.unitPrice = double.Parse(rdr["UnitPrice"].ToString());
                    this.quantity =double.Parse(rdr["Quantity"].ToString());
                    this.amount = double.Parse(rdr["Amount"].ToString());
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
                string sqlInsert = "insert into SaleOrderDetail(" +
                    "SaleOrderId, PharmaId, Unit, UnitPrice, Quantity, Amount)" +
                    " values(@SaleOrderId, @PharmaId, @Unit, @UnitPrice, @Quantity, @Amount)";

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);
                sqlCmd.Parameters.Add("@SaleOrderId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Unit", SqlDbType.Int);
                sqlCmd.Parameters.Add("@UnitPrice", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Quantity", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Amount", SqlDbType.Money);

                sqlCmd.Parameters[0].Value = this.saleOrderId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.unit;
                sqlCmd.Parameters[3].Value = this.unitPrice;
                sqlCmd.Parameters[4].Value = this.quantity;
                sqlCmd.Parameters[5].Value = this.amount;                

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
                string sqlInsert = "insert into SaleOrderDetail(" +
                    "SaleOrderId, PharmaId, Unit, UnitPrice, Quantity, Amount, PurchaseUnitPrice, Inventory, Expiry, Expiry1)" +
                    " values(@SaleOrderId, @PharmaId, @Unit, @UnitPrice, @Quantity, @Amount, @PurchaseUnitPrice, @Inventory, @Expiry, @Expiry1)";

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@SaleOrderId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Unit", SqlDbType.Int);
                sqlCmd.Parameters.Add("@UnitPrice", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Quantity", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Amount", SqlDbType.Money);
                sqlCmd.Parameters.Add("@PurchaseUnitPrice", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Inventory", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@Expiry1", SqlDbType.NVarChar);

                sqlCmd.Parameters[0].Value = this.saleOrderId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.unit;
                sqlCmd.Parameters[3].Value = this.unitPrice;
                sqlCmd.Parameters[4].Value = this.quantity;
                sqlCmd.Parameters[5].Value = this.amount;
                sqlCmd.Parameters[6].Value = this.purchaseUnitPrice;
                sqlCmd.Parameters[7].Value = this.inventory;
                sqlCmd.Parameters[8].Value = this.expiry;
                sqlCmd.Parameters[9].Value = this.expiry1;

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
                string sqlSelectString = "delete from SaleOrderDetail where " + fieldName + " = @FieldValue";

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
                SqlCommand sqlCmd = new SqlCommand("delete from SaleOrderDetail where SaleOrderId = @SaleOrderId and PharmaId = @PharmaId", conn);
                sqlCmd.Parameters.Add("@SaleOrderId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.saleOrderId;
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

        public bool DeleteSaleOrderFromDB(SqlConnection conn, SqlTransaction trans)
        {
            int result;            

            try
            {
                SqlCommand sqlCmd = new SqlCommand("delete from SaleOrderDetail where SaleOrderId = @SaleOrderId", conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@SaleOrderId", SqlDbType.Int);
                
                sqlCmd.Parameters[0].Value = this.saleOrderId;
                
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

        public bool UpdateToDB()
        {
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            int result;
            try
            {
                string sqlUpdate = "update SaleOrderDetail set " +
                    "SaleOrderId = @SaleOrderId, PharmaId = @PharmaId, Unit = @Unit, UnitPrice = @UnitPrice, " +
                    "Quantity = @Quantity, Amount = @Amount, " +
                    "where SaleOrderId = @SaleOrderId and PharmaId = @PharmaId";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Parameters.Add("@SaleOrderId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Unit", SqlDbType.Int);
                sqlCmd.Parameters.Add("@UnitPrice", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Quantity", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Amount", SqlDbType.Money);

                sqlCmd.Parameters[0].Value = this.saleOrderId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.unit;
                sqlCmd.Parameters[3].Value = this.unitPrice;
                sqlCmd.Parameters[4].Value = this.quantity;
                sqlCmd.Parameters[5].Value = this.amount;

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
                string sqlUpdate = "update SaleOrderDetail set " +
                    "SaleOrderId = @SaleOrderId, PharmaId = @PharmaId, Unit = @Unit, UnitPrice = @UnitPrice, " +
                    "Quantity = @Quantity, Amount = @Amount, Inventory = @Inventory" +
                    "where SaleOrderId = @SaleOrderId and PharmaId = @PharmaId";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@SaleOrderId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Unit", SqlDbType.Int);
                sqlCmd.Parameters.Add("@UnitPrice", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Quantity", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Amount", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Inventory", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.saleOrderId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.unit;
                sqlCmd.Parameters[3].Value = this.unitPrice;
                sqlCmd.Parameters[4].Value = this.quantity;
                sqlCmd.Parameters[5].Value = this.amount;
                sqlCmd.Parameters[6].Value = this.inventory;

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