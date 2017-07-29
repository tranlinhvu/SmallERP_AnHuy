using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;
using IMS.Favorite;

namespace IMS.Model
{
    public class PharmaInventory
    {
        ulong id;
        int invId;
        int pharmaId;
        double inputNum;
        double outputNum;
        double available;
        double minInStock;
        double maxInStock;
        long expiry;
        int expiryLimit;
        double purchaseUnitPrice;
        long issuedDate;
        string expiry1;

        public PharmaInventory()
        {
            ;
        }

        public ulong Id
        {
            get { return id; }
            set { id = value; }
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

        public long Expiry
        {
            get { return expiry; }
            set { expiry = value; }
        }

        public string Expiry1
        {
            get { return expiry1; }
            set { expiry1 = value; }
        }

        public long IssuedDate
        {
            get { return issuedDate; }
            set { issuedDate = value; }
        }

        public double InputNum
        {
            get { return inputNum; }
            set { inputNum = value; }
        }

        public double OutputNum
        {
            get { return outputNum; }
            set { outputNum = value; }
        }

        public double Available
        {
            get { return available; }
            set { available = value; }
        }

        public double MinInStock
        {
            get { return minInStock; }
            set { minInStock = value; }
        }

        public double MaxInStock
        {
            get { return maxInStock; }
            set { maxInStock = value; }
        }

        public int ExpiryLimit
        {
            get { return expiryLimit; }
            set { expiryLimit = value; }
        }

        public double PurchaseUnitPrice
        {
            get { return purchaseUnitPrice; }
            set { purchaseUnitPrice = value; }
        }

        public bool IsExisted()
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId and Expiry = @Expiry and PurchaseUnitPrice = @PurchaseUnitPrice", conn);
                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@PurchaseUnitPrice", SqlDbType.Money);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.purchaseUnitPrice;

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
                    this.invId = int.Parse(rdr["InvId"].ToString());
                    this.pharmaId = int.Parse(rdr["PharmaId"].ToString());
                    this.expiry = long.Parse(rdr["Expiry"].ToString());
                    this.expiry1 = rdr["Expiry1"].ToString();
                    this.purchaseUnitPrice = double.Parse(rdr["PurchaseUnitPrice"].ToString());
                    this.inputNum = double.Parse(rdr["InputNum"].ToString());
                    this.outputNum = double.Parse(rdr["OutputNum"].ToString());
                    this.available = double.Parse(rdr["Available"].ToString());
                    this.minInStock = double.Parse(rdr["MinInStock"].ToString());
                    this.maxInStock = double.Parse(rdr["MaxInStock"].ToString());
                    this.expiryLimit = rdr.IsDBNull(8)==true? 0: int.Parse(rdr["ExpiryLimit"].ToString());
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

        public bool IsExisted(SqlConnection conn, SqlTransaction trans)
        {
            bool result;
            //SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId and Expiry = @Expiry and PurchaseUnitPrice = @PurchaseUnitPrice", conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@PurchaseUnitPrice", SqlDbType.Money);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.purchaseUnitPrice;

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
                    this.invId = int.Parse(rdr["InvId"].ToString());
                    this.pharmaId = int.Parse(rdr["PharmaId"].ToString());
                    this.expiry = long.Parse(rdr["Expiry"].ToString());
                    this.expiry1 = rdr["Expiry1"].ToString();
                    this.purchaseUnitPrice = double.Parse(rdr["PurchaseUnitPrice"].ToString());
                    this.inputNum = double.Parse(rdr["InputNum"].ToString());
                    this.outputNum = double.Parse(rdr["OutputNum"].ToString());
                    this.available = double.Parse(rdr["Available"].ToString());
                    this.minInStock = double.Parse(rdr["MinInStock"].ToString());
                    this.maxInStock = double.Parse(rdr["MaxInStock"].ToString());
                    this.expiryLimit = rdr.IsDBNull(8) == true ? 0 : int.Parse(rdr["ExpiryLimit"].ToString());
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
                //DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            return result;
        }

        public bool IsExisted1(SqlConnection conn, SqlTransaction trans)
        {
            bool result;
            //SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId and Expiry = @Expiry", conn);
                sqlCmd.Transaction = trans;

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
                    this.expiry = long.Parse(rdr["Expiry"].ToString());
                    this.expiry1 = rdr["Expiry1"].ToString();
                    this.purchaseUnitPrice = double.Parse(rdr["PurchaseUnitPrice"].ToString());
                    this.inputNum = double.Parse(rdr["InputNum"].ToString());
                    this.outputNum = double.Parse(rdr["OutputNum"].ToString());
                    this.available = double.Parse(rdr["Available"].ToString());
                    this.minInStock = double.Parse(rdr["MinInStock"].ToString());
                    this.maxInStock = double.Parse(rdr["MaxInStock"].ToString());
                    this.expiryLimit = rdr.IsDBNull(8) == true ? 0 : int.Parse(rdr["ExpiryLimit"].ToString());
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
                //DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            return result;
        }

        public bool IsExisted2(SqlConnection conn, SqlTransaction trans)
        {
            bool result;
            //SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId and PurchaseUnitPrice = @PurchaseUnitPrice", conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);                
                sqlCmd.Parameters.Add("@PurchaseUnitPrice", SqlDbType.Money);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;                
                sqlCmd.Parameters[3].Value = this.purchaseUnitPrice;

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
                    this.invId = int.Parse(rdr["InvId"].ToString());
                    this.pharmaId = int.Parse(rdr["PharmaId"].ToString());
                    this.expiry = long.Parse(rdr["Expiry"].ToString());
                    this.expiry1 = rdr["Expiry1"].ToString();
                    this.purchaseUnitPrice = double.Parse(rdr["PurchaseUnitPrice"].ToString());
                    this.inputNum = double.Parse(rdr["InputNum"].ToString());
                    this.outputNum = double.Parse(rdr["OutputNum"].ToString());
                    this.available = double.Parse(rdr["Available"].ToString());
                    this.minInStock = double.Parse(rdr["MinInStock"].ToString());
                    this.maxInStock = double.Parse(rdr["MaxInStock"].ToString());
                    this.expiryLimit = rdr.IsDBNull(8) == true ? 0 : int.Parse(rdr["ExpiryLimit"].ToString());
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
                //DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            return result;
        }

        public double GetMinInStock(int invId, int pharmaId, SqlConnection conn, SqlTransaction trans)
        {
            double result;
            //SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select top 1 * from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId", conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = invId;
                sqlCmd.Parameters[1].Value = pharmaId;              

                rdr = sqlCmd.ExecuteReader();
                result = 0;
                while (rdr.Read())
                {                    
                    result = double.Parse(rdr["MinInStock"].ToString());                    
                }
            }
            catch
            {
                return 0;
            }
            finally
            {
                //close the sqlreader
                if (rdr != null)
                {
                    rdr.Close();
                }

                //Close the connection
                //DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            return result;
        }

        public double GetCurrentPrice(int invId, int pharmaId, SqlConnection conn, SqlTransaction trans)
        {
            double result;
            //SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId", conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = invId;
                sqlCmd.Parameters[1].Value = pharmaId;

                rdr = sqlCmd.ExecuteReader();
                result = 0;
                double purchaseUnitPrice_;
                double available_;
                double totalWeight = 0;
                double currentPrice = 0;
                while (rdr.Read())
                {
                    purchaseUnitPrice_ = double.Parse(rdr["PurchaseUnitPrice"].ToString());
                    available_ = double.Parse(rdr["Available"].ToString());
                    totalWeight += available_;
                    currentPrice += purchaseUnitPrice_ * available_;
                }
                result = currentPrice / totalWeight;
            }
            catch
            {
                return 0;
            }
            finally
            {
                //close the sqlreader
                if (rdr != null)
                {
                    rdr.Close();
                }

                //Close the connection
                //DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            return result;
        }

        public int GetExpiryLimit(int invId, int pharmaId, SqlConnection conn, SqlTransaction trans)
        {
            int result;
            //SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select top 1 * from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId", conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = invId;
                sqlCmd.Parameters[1].Value = pharmaId;

                rdr = sqlCmd.ExecuteReader();
                result = 0;
                while (rdr.Read())
                {
                    result = this.expiryLimit = rdr.IsDBNull(8) == true ? 0 : int.Parse(rdr["ExpiryLimit"].ToString());
                }
            }
            catch
            {
                return 0;
            }
            finally
            {
                //close the sqlreader
                if (rdr != null)
                {
                    rdr.Close();
                }

                //Close the connection
                //DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            return result;
        }

        public bool IsExisted(int invId)
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select top 1 * from PharmaInventory where InvId = @InvId", conn);
                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = invId;                

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
                    this.invId = int.Parse(rdr["InvId"].ToString());
                    this.pharmaId = int.Parse(rdr["PharmaId"].ToString());
                    this.expiry = long.Parse(rdr["Expiry"].ToString());
                    this.expiry1 = rdr["Expiry1"].ToString();
                    this.inputNum = double.Parse(rdr["InputNum"].ToString());
                    this.outputNum = double.Parse(rdr["OutputNum"].ToString());
                    this.available = double.Parse(rdr["Available"].ToString());
                    this.minInStock = double.Parse(rdr["MinInStock"].ToString());
                    this.maxInStock = double.Parse(rdr["MaxInStock"].ToString());
                    this.expiryLimit = rdr.IsDBNull(8) == true ? 0 : int.Parse(rdr["ExpiryLimit"].ToString());
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

        public double GetAvgPurchaseUPrice(SqlConnection conn, SqlTransaction trans)
        {
            double result = 0;
            //SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select PurchaseUnitPrice, AVG(PurchaseUnitPrice) as AvgPurchaseUPrice from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId group by PurchaseUnitPrice", conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);                             

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                
                rdr = sqlCmd.ExecuteReader();                
                while (rdr.Read())
                {
                    result = double.Parse(rdr["AvgPurchaseUPrice"].ToString()); ;
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

        public double GetAvgPurchaseUPrice()
        {
            double result = 0;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select PurchaseUnitPrice, AVG(PurchaseUnitPrice) as AvgPurchaseUPrice from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId group by PurchaseUnitPrice", conn);
                //sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;

                rdr = sqlCmd.ExecuteReader();
                while (rdr.Read())
                {
                    result = double.Parse(rdr["AvgPurchaseUPrice"].ToString()); ;
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

        public long GetOldestPharmaExpiry()
        {
            long result = UString.GetLongFromDate(DateTime.Now);
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select top 1 * from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId and Available > 0 order by Expiry ASC", conn);
                //sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);                

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;               

                rdr = sqlCmd.ExecuteReader();                
                while (rdr.Read())
                {
                    this.invId = int.Parse(rdr["InvId"].ToString());
                    this.pharmaId = int.Parse(rdr["PharmaId"].ToString());
                    this.expiry = long.Parse(rdr["Expiry"].ToString());
                    this.expiry1 = rdr["Expiry1"].ToString();
                    this.purchaseUnitPrice = double.Parse(rdr["PurchaseUnitPrice"].ToString());
                    this.inputNum = double.Parse(rdr["InputNum"].ToString());
                    this.outputNum = double.Parse(rdr["OutputNum"].ToString());
                    this.available = double.Parse(rdr["Available"].ToString());
                    this.minInStock = double.Parse(rdr["MinInStock"].ToString());
                    this.maxInStock = double.Parse(rdr["MaxInStock"].ToString());
                    this.expiryLimit = rdr.IsDBNull(8) == true ? 0 : int.Parse(rdr["ExpiryLimit"].ToString());
                    result = this.expiry;
                }
            }
            catch
            {
                return this.expiry;
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

        public bool GetPharmaStockNoExpiry()
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select InvId, PharmaId, Sum(Available) As TAvailable from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId Group by InvId, PharmaId", conn);
                
                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
               
                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
                    this.invId = int.Parse(rdr["InvId"].ToString());
                    this.pharmaId = int.Parse(rdr["PharmaId"].ToString());
                    this.available = double.Parse(rdr["TAvailable"].ToString());                  

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

        public bool GetPharmaStockNoExpiry(SqlConnection conn, SqlTransaction trans)
        {
            bool result;
            //SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select InvId, PharmaId, Sum(Available) As TAvailable from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId Group by InvId, PharmaId", conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);


                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
                    this.invId = int.Parse(rdr["InvId"].ToString());
                    this.pharmaId = int.Parse(rdr["PharmaId"].ToString());
                    this.available = double.Parse(rdr["TAvailable"].ToString());

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
                //DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            return result;
        }

        public bool GetTop1()
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select top 1 * from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId and Available > 0 order by Expiry ASC", conn);
                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);                

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
                    this.invId = int.Parse(rdr["InvId"].ToString());
                    this.pharmaId = int.Parse(rdr["PharmaId"].ToString());
                    this.expiry = long.Parse(rdr["Expiry"].ToString());
                    this.expiry1 = rdr["Expiry1"].ToString();
                    this.inputNum = double.Parse(rdr["InputNum"].ToString());
                    this.outputNum = double.Parse(rdr["OutputNum"].ToString());
                    this.available = double.Parse(rdr["Available"].ToString());
                    this.minInStock = double.Parse(rdr["MinInStock"].ToString());
                    this.maxInStock = double.Parse(rdr["MaxInStock"].ToString());
                    this.expiryLimit = int.Parse(rdr["ExpiryLimit"].ToString());
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
                SqlCommand sqlCmd = new SqlCommand("select * from PharmaInventory where Available = 0", conn);              

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
                string sqlSelectString = "select * from PharmaInventory where " + fieldName1 + " = @FieldValue1 and ";
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
                    this.expiry = long.Parse(rdr["Expiry"].ToString());
                    this.expiry1 = rdr["Expiry1"].ToString();
                    this.inputNum = double.Parse(rdr["InputNum"].ToString());
                    this.outputNum = double.Parse(rdr["OutputNum"].ToString());
                    this.available = double.Parse(rdr["Available"].ToString());
                    this.minInStock = double.Parse(rdr["MinInStock"].ToString());
                    this.maxInStock = double.Parse(rdr["MaxInStock"].ToString());
                    this.maxInStock = int.Parse(rdr["ExpiryLimit"].ToString());
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
                string sqlInsert = "insert into PharmaInventory(" +
                    "InvId, PharmaId, Expiry, Expiry1, InputNum, OutputNum, Available, MinInStock, MaxInStock)" +
                    " values(@InvId, @PharmaId, @Expiry, @Expiry1, @InputNum, @OutputNum, @Available, @MinInStock, @MaxInStock)";

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);
                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@Expiry1", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@InputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@OutputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Available", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MinInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MaxInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@ExpiryLimit", SqlDbType.Int);                

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.expiry1;
                sqlCmd.Parameters[4].Value = this.inputNum;
                sqlCmd.Parameters[5].Value = this.outputNum;
                sqlCmd.Parameters[6].Value = this.available;
                sqlCmd.Parameters[7].Value = this.minInStock;
                sqlCmd.Parameters[8].Value = this.maxInStock;
                sqlCmd.Parameters[9].Value = this.expiryLimit;

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
                string sqlInsert = "insert into PharmaInventory(" +
                    "InvId, PharmaId, Expiry, Expiry1, PurchaseUnitPrice, InputNum, OutputNum, Available, MinInStock, MaxInStock, ExpiryLimit)" +
                    " values(@InvId, @PharmaId, @Expiry, @Expiry1, @PurchaseUnitPrice, @InputNum, @OutputNum, @Available, @MinInStock, @MaxInStock, @ExpiryLimit)";

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@Expiry1", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@PurchaseUnitPrice", SqlDbType.Money);
                sqlCmd.Parameters.Add("@InputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@OutputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Available", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MinInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MaxInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@ExpiryLimit", SqlDbType.Int);                 

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.expiry1;
                sqlCmd.Parameters[4].Value = this.purchaseUnitPrice;
                sqlCmd.Parameters[5].Value = this.inputNum;
                sqlCmd.Parameters[6].Value = this.outputNum;
                sqlCmd.Parameters[7].Value = this.available;
                sqlCmd.Parameters[8].Value = this.minInStock;
                sqlCmd.Parameters[9].Value = this.maxInStock;
                sqlCmd.Parameters[10].Value = this.expiryLimit;
                
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

        public bool DeleteDBFromField(string fieldName, string dataType, string FieldValue)
        {
            int result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            try
            {
                string sqlSelectString = "delete from PharmaInventory where " + fieldName + " = @FieldValue";

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

        public bool DeleteFromDB(SqlConnection conn, SqlTransaction trans)
        {
            int result;           

            try
            {
                SqlCommand sqlCmd = new SqlCommand("delete from PharmaInventory where InvId = @InvId and PharmaId = @PharmaId and Expiry = @Expiry and PurchaseUnitPrice = @PurchaseUnitPrice", conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@PurchaseUnitPrice", SqlDbType.Money);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.purchaseUnitPrice;

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

        public bool UpdateToDB(SqlConnection conn, SqlTransaction trans)
        {            
            int result;
            try
            {
                string sqlUpdate = "update PharmaInventory set " +
                    "InputNum = @InputNum, OutputNum = @OutputNum, Available = @Available, MinInStock = @MinInStock, MaxInStock = @MaxInStock, Expiry1 = @Expiry1, ExpiryLimit = @ExpiryLimit " +
                    "where InvId = @InvId and PharmaId = @PharmaId and Expiry = @Expiry and PurchaseUnitPrice = @PurchaseUnitPrice";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@Expiry1", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@PurchaseUnitPrice", SqlDbType.Money);
                sqlCmd.Parameters.Add("@InputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@OutputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Available", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MinInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MaxInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@ExpiryLimit", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.expiry1;
                sqlCmd.Parameters[4].Value = this.purchaseUnitPrice;
                sqlCmd.Parameters[5].Value = this.inputNum;
                sqlCmd.Parameters[6].Value = this.outputNum;
                sqlCmd.Parameters[7].Value = this.available;
                sqlCmd.Parameters[8].Value = this.minInStock;
                sqlCmd.Parameters[9].Value = this.maxInStock;
                sqlCmd.Parameters[10].Value = this.expiryLimit;
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

        public bool UpdateToDBOnQuantity(SqlConnection conn, SqlTransaction trans)
        {
            int result;
            try
            {
                string sqlUpdate = "update PharmaInventory set " +
                    "Available = @Available " +
                    "where Id = @Id";
                //"where InvId = @InvId and PharmaId = @PharmaId and PurchaseUnitPrice = @PurchaseUnitPrice";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@Expiry1", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@PurchaseUnitPrice", SqlDbType.Money);
                sqlCmd.Parameters.Add("@InputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@OutputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Available", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MinInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MaxInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@ExpiryLimit", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Id", SqlDbType.BigInt);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.expiry1;
                sqlCmd.Parameters[4].Value = this.purchaseUnitPrice;
                sqlCmd.Parameters[5].Value = this.inputNum;
                sqlCmd.Parameters[6].Value = this.outputNum;
                sqlCmd.Parameters[7].Value = this.available;
                sqlCmd.Parameters[8].Value = this.minInStock;
                sqlCmd.Parameters[9].Value = this.maxInStock;
                sqlCmd.Parameters[10].Value = this.expiryLimit;
                sqlCmd.Parameters[11].Value = this.id;

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

        public bool UpdateToDBOnExpiry(SqlConnection conn, SqlTransaction trans)
        {
            int result;
            try
            {
                string sqlUpdate = "update PharmaInventory set " +
                    "Expiry = @Expiry, Expiry1 = @Expiry1 " +
                    "where Id = @Id";
                    //"where InvId = @InvId and PharmaId = @PharmaId and PurchaseUnitPrice = @PurchaseUnitPrice";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@Expiry1", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@PurchaseUnitPrice", SqlDbType.Money);
                sqlCmd.Parameters.Add("@InputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@OutputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Available", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MinInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MaxInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@ExpiryLimit", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Id", SqlDbType.BigInt);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.expiry1;
                sqlCmd.Parameters[4].Value = this.purchaseUnitPrice;
                sqlCmd.Parameters[5].Value = this.inputNum;
                sqlCmd.Parameters[6].Value = this.outputNum;
                sqlCmd.Parameters[7].Value = this.available;
                sqlCmd.Parameters[8].Value = this.minInStock;
                sqlCmd.Parameters[9].Value = this.maxInStock;
                sqlCmd.Parameters[10].Value = this.expiryLimit;
                sqlCmd.Parameters[11].Value = this.id;

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

        public bool UpdateToDBOnUnitPrice(SqlConnection conn, SqlTransaction trans)
        {
            int result;
            try
            {
                string sqlUpdate = "update PharmaInventory set " +
                    "PurchaseUnitPrice = @PurchaseUnitPrice " +
                    "where Id = @Id";
                    //"where InvId = @InvId and PharmaId = @PharmaId and Expiry = @Expiry";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@Expiry1", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@PurchaseUnitPrice", SqlDbType.Money);
                sqlCmd.Parameters.Add("@InputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@OutputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Available", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MinInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MaxInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@ExpiryLimit", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Id", SqlDbType.BigInt);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.expiry1;
                sqlCmd.Parameters[4].Value = this.purchaseUnitPrice;
                sqlCmd.Parameters[5].Value = this.inputNum;
                sqlCmd.Parameters[6].Value = this.outputNum;
                sqlCmd.Parameters[7].Value = this.available;
                sqlCmd.Parameters[8].Value = this.minInStock;
                sqlCmd.Parameters[9].Value = this.maxInStock;
                sqlCmd.Parameters[10].Value = this.expiryLimit;
                sqlCmd.Parameters[11].Value = this.id;

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

        public bool UpdateToDB(SqlConnection conn, SqlTransaction trans, double unitPrice)
        {
            int result;
            try
            {
                string sqlUpdate = "update PharmaInventory set " +
                    "InputNum = @InputNum, OutputNum = @OutputNum, Available = @Available, MinInStock = @MinInStock, MaxInStock = @MaxInStock, Expiry1 = @Expiry1, ExpiryLimit = @ExpiryLimit " +
                    "where InvId = @InvId and PharmaId = @PharmaId and Expiry = @Expiry and PurchaseUnitPrice = @PurchaseUnitPrice";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@Expiry1", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@PurchaseUnitPrice", SqlDbType.Money);
                sqlCmd.Parameters.Add("@InputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@OutputNum", SqlDbType.Real);
                sqlCmd.Parameters.Add("@Available", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MinInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@MaxInStock", SqlDbType.Real);
                sqlCmd.Parameters.Add("@ExpiryLimit", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.expiry1;
                sqlCmd.Parameters[4].Value = this.purchaseUnitPrice;
                sqlCmd.Parameters[5].Value = this.inputNum;
                sqlCmd.Parameters[6].Value = this.outputNum;
                sqlCmd.Parameters[7].Value = this.available;
                sqlCmd.Parameters[8].Value = this.minInStock;
                sqlCmd.Parameters[9].Value = this.maxInStock;
                sqlCmd.Parameters[10].Value = this.expiryLimit;
                
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

        public bool UpdateFieldToDB(string fieldName, string dataType, string fieldValue)
        {
            int result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();

            try
            {

                string sqlUpdate = "update PharmaInventory set " + fieldName + " = @FieldValue ";
                sqlUpdate += "where InvId = @InvId and PharmaId = @PharmaId and Expiry = @Expiry and PurchaseUnitPrice = @PurchaseUnitPrice";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Expiry", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@PurchaseUnitPrice", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@FieldValue", dataType);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;
                sqlCmd.Parameters[2].Value = this.expiry;
                sqlCmd.Parameters[3].Value = this.purchaseUnitPrice;
                sqlCmd.Parameters[4].Value = fieldValue;

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

        public bool UpdateFieldToDBOnPharma(string fieldName, string dataType, string fieldValue)
        {
            int result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            
            try
            {

                string sqlUpdate = "update PharmaInventory set " + fieldName + " = @FieldValue ";
                sqlUpdate += "where InvId = @InvId and PharmaId = @PharmaId";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Parameters.Add("@InvId", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaId", SqlDbType.Int);                
                sqlCmd.Parameters.Add("@FieldValue", dataType);

                sqlCmd.Parameters[0].Value = this.invId;
                sqlCmd.Parameters[1].Value = this.pharmaId;                
                sqlCmd.Parameters[2].Value = fieldValue;

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