using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;
using IMS.Favorite;
namespace IMS.Model
{
    public class SaleOrder
    {
        int id;
        int customer;
        string saleOrderNo;
        ulong saleOrderDate;
        ulong issuedDate;
        int soldBy;
        double totalAmount;
        double discount;
        double payment;
        bool prePurchase;
        string note;
        int saleType;
        bool byCash;

        public SaleOrder()
        {
            ;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        public string SaleOrderNo
        {
            get { return saleOrderNo; }
            set { saleOrderNo = value; }
        }

        public ulong SaleOrderDate
        {
            get { return saleOrderDate; }
            set { saleOrderDate = value; }
        }

        public ulong IssuedDate
        {
            get { return issuedDate; }
            set { issuedDate = value; }
        }

        public int SoldBy
        {
            get { return soldBy; }
            set { soldBy = value; }
        }

        public double TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        public double Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public double Payment
        {
            get { return payment; }
            set { payment = value; }
        }

        public bool PrePurchase
        {
            get { return prePurchase; }
            set { prePurchase = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        public int SaleType
        {
            get { return saleType; }
            set { saleType = value; }
        }

        public bool ByCash
        {
            get { return byCash; }
            set { byCash = value; }
        }

        public string GetNewSaleOrderNo()
        {
            SaleOrder saleOrder = new SaleOrder();

            //Số  đơn
            string period;
            period = UString.Right(DateTime.Now.Year.ToString(), 2) + "." + UString.AddZeroBefore(DateTime.Now.Month.ToString(), 2);
            return "BH" + period + "-" + UString.AddZeroBefore((saleOrder.GetSaleOrderNoMax(period) + 1).ToString(), 4);
        }

        public int GetSaleOrderNoMax(string period)
        {
            int result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                string sqlSelect = "select top 1 SaleOrderNo from SaleOrder where SaleOrderNo like @SaleOrderNo order by SaleOrderNo desc";
                SqlCommand sqlCmd = new SqlCommand(sqlSelect, conn);
                sqlCmd.Parameters.Add("@SaleOrderNo", SqlDbType.NVarChar);

                sqlCmd.Parameters[0].Value = "%" + period + "%";

                rdr = sqlCmd.ExecuteReader();
                result = 0;
                while (rdr.Read())
                {
                    result = int.Parse(UString.Right(rdr["SaleOrderNo"].ToString(),4));                  
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
                DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            return result;
        }

        public bool IsExisted()
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from SaleOrder where Id = @Id", conn);
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

        public static double GetTotalDiscount(ulong fromDate, ulong toDate)
        {
            double result = 0;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                string sql = "select sum(Discount) as TotalDiscount from SaleOrder where SaleOrderDate >= " + fromDate.ToString() + " and  SaleOrderDate <= " + toDate.ToString();
                SqlCommand sqlCmd = new SqlCommand(sql, conn);                 

                rdr = sqlCmd.ExecuteReader();
                result = 0;
                while (rdr.Read())
                {
                    result = double.Parse(rdr["TotalDiscount"].ToString());
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

        public bool GetValueFromField(string fieldName, string dataType, string value)
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from SaleOrder where " + fieldName + " = @Value", conn);
                sqlCmd.Parameters.Add("@Value", dataType);
                sqlCmd.Parameters[0].Value = value;

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {                   
                    this.id = int.Parse(rdr["Id"].ToString());
                    this.customer = int.Parse(rdr["Customer"].ToString());
                    this.saleOrderNo = rdr["SaleOrderNo"].ToString();
                    this.saleOrderDate = ulong.Parse(rdr["SaleOrderDate"].ToString());
                    this.issuedDate = ulong.Parse(rdr["IssuedDate"].ToString());
                    this.soldBy = int.Parse(rdr["SoldBy"].ToString());
                    this.TotalAmount = double.Parse(rdr["TotalAmount"].ToString());
                    this.discount = int.Parse(rdr["Discount"].ToString());
                    this.totalAmount = double.Parse(rdr["Payment"].ToString());
                    this.prePurchase = bool.Parse(rdr["PrePurchase"].ToString());
                    this.note = rdr["PrePurchase"].ToString();

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

        public int MoveToDB(SqlConnection conn, SqlTransaction trans)
        {
            int result = 0;
            try
            { 
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "ProcInsertSaleOrder";
                sqlCmd.Connection = conn;
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@Customer", SqlDbType.Int);
                sqlCmd.Parameters.Add("@SaleOrderNo", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@SaleOrderDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@IssuedDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@SoldBy", SqlDbType.Int);
                sqlCmd.Parameters.Add("@TotalAmount", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Discount", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Payment", SqlDbType.Money);
                sqlCmd.Parameters.Add("@PrePurchase", SqlDbType.Bit);
                sqlCmd.Parameters.Add("@Note", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@SaleType", SqlDbType.Int);
                sqlCmd.Parameters.Add("@ByCash", SqlDbType.Bit);

                sqlCmd.Parameters[0].Value = this.customer;
                sqlCmd.Parameters[1].Value = this.saleOrderNo;
                sqlCmd.Parameters[2].Value = this.saleOrderDate;
                sqlCmd.Parameters[3].Value = this.issuedDate;
                sqlCmd.Parameters[4].Value = this.soldBy;
                sqlCmd.Parameters[5].Value = this.totalAmount;
                sqlCmd.Parameters[6].Value = this.discount;
                sqlCmd.Parameters[7].Value = this.payment;
                sqlCmd.Parameters[8].Value = this.prePurchase;
                sqlCmd.Parameters[9].Value = this.note;
                sqlCmd.Parameters[10].Value = this.saleType;
                sqlCmd.Parameters[11].Value = this.byCash;

                SqlParameter returnParam = new SqlParameter();
                returnParam.Direction = ParameterDirection.ReturnValue;

                sqlCmd.Parameters.Add(returnParam);

                sqlCmd.ExecuteNonQuery();

                result = int.Parse(returnParam.Value.ToString());
            }
            catch
            {
                //Close the connection
                DBHelper.SqlDataConnection.CloseSqlConnection();
                return result;
            }
            finally
            {
                //Close the connection
                //DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            return result;
        }

        public bool MoveToDB()
        {
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            int result;
            try
            {
                string sqlInsert = "insert into SaleOrder(" +
                    "Customer, SaleOrderNo, SaleOrderDate, IssuedDate, SoldBy, TotalAmount, Discount)" +
                    " values(@Customer, @SaleOrderNo, @SaleOrderDate, @SoldBy, @TotalAmount, @Discount)";

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);
                sqlCmd.Parameters.Add("@Customer", SqlDbType.Int);
                sqlCmd.Parameters.Add("@SaleOrderNo", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@SaleOrderDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@IssuedDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@SoldBy", SqlDbType.Int);
                sqlCmd.Parameters.Add("@TotalAmount", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Discount", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Payment", SqlDbType.Money);
                sqlCmd.Parameters.Add("@PrePurchase", SqlDbType.Bit);
                sqlCmd.Parameters.Add("@Note", SqlDbType.NVarChar);

                sqlCmd.Parameters[0].Value = this.customer;
                sqlCmd.Parameters[1].Value = this.saleOrderNo;
                sqlCmd.Parameters[2].Value = this.saleOrderDate;
                sqlCmd.Parameters[3].Value = this.issuedDate;
                sqlCmd.Parameters[4].Value = this.soldBy;
                sqlCmd.Parameters[5].Value = this.totalAmount;
                sqlCmd.Parameters[6].Value = this.discount;
                sqlCmd.Parameters[7].Value = this.payment;
                sqlCmd.Parameters[8].Value = this.prePurchase;
                sqlCmd.Parameters[9].Value = this.note;

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

        public bool DeleteDBFromField(string fieldName, string dataType, string value)
        {
            int result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from SaleOrder where " + fieldName + " = @FieldValue", conn);
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
                SqlCommand sqlCmd = new SqlCommand("delete from SaleOrder where Id = @Id", conn);
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

        public bool DeleteFromDB(SqlConnection conn, SqlTransaction trans)
        {
            int result;           

            try
            {
                SqlCommand sqlCmd = new SqlCommand("delete from SaleOrder where Id = @Id", conn);
                sqlCmd.Transaction = trans;
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
                string sqlUpdate = "update SaleOrder set " +
                    "Customer = @Customer, SaleOrderNo = @SaleOrderNo, SaleOrderDate = @SaleOrderDate, " +
                    "SoldBy = @SoldBy, TotalAmount = @TotalAmount, Discount = @Discount " +
                    "where Id = @Id";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@Id", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Customer", SqlDbType.Int);
                sqlCmd.Parameters.Add("@SaleOrderNo", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@SaleOrderDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@IssuedDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@SoldBy", SqlDbType.Int);
                sqlCmd.Parameters.Add("@TotalAmount", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Discount", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Payment", SqlDbType.Money);
                sqlCmd.Parameters.Add("@PrePurchase", SqlDbType.Bit);
                sqlCmd.Parameters.Add("@Note", SqlDbType.NVarChar);

                sqlCmd.Parameters[0].Value = this.id;
                sqlCmd.Parameters[1].Value = this.customer;
                sqlCmd.Parameters[2].Value = this.saleOrderNo;
                sqlCmd.Parameters[3].Value = this.saleOrderDate;
                sqlCmd.Parameters[4].Value = this.issuedDate;
                sqlCmd.Parameters[5].Value = this.soldBy;
                sqlCmd.Parameters[6].Value = this.totalAmount;
                sqlCmd.Parameters[7].Value = this.discount;
                sqlCmd.Parameters[8].Value = this.payment;
                sqlCmd.Parameters[9].Value = this.prePurchase;
                sqlCmd.Parameters[10].Value = this.note;

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

        public bool UpdateDBFromField(string fieldName, string value, string dataType)
        {
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            int result;
            try
            {
                string sqlUpdate = "update SaleOrder set " +
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