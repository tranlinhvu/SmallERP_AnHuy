using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;
using IMS.Favorite;

namespace IMS.Model
{
    public class PurchaseOrder
    {
        int id;
        int vendor;
        string purchaseNo;
        ulong purchaseDate;
        ulong issuedDate;
        int purchasedBy;
        double totalAmount;
        double payment;
        double discount;
        int purchaseType;
        string note;

        public PurchaseOrder()
        {
            ;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Vendor
        {
            get { return vendor; }
            set { vendor = value; }
        }


        public string PurchaseNo
        {
            get { return purchaseNo; }
            set { purchaseNo = value; }
        }

        public ulong PurchaseDate
        {
            get { return purchaseDate; }
            set { purchaseDate = value; }
        }

        public ulong IssuedDate
        {
            get { return issuedDate; }
            set { issuedDate = value; }
        }

        public int PurchasedBy
        {
            get { return purchasedBy; }
            set { purchasedBy = value; }
        }

        public double TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        public double Payment
        {
            get { return payment; }
            set { payment = value; }
        }

        public double Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public int PurchaseType
        {
            get { return purchaseType; }
            set { purchaseType = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        public string GetNewPurchaseNo()
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();

            //Số  đơn
            string period;
            period = UString.Right(DateTime.Now.Year.ToString(), 2) + "." + UString.AddZeroBefore(DateTime.Now.Month.ToString(), 2);
            return "NH" + period + "-" + UString.AddZeroBefore((purchaseOrder.GetPurchaseNoMax(period) + 1).ToString(), 4);
        }

        public int GetPurchaseNoMax(string period)
        {
            int result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                string sqlSelect = "select top 1 PurchaseNo from PurchaseOrder where PurchaseNo like @PurchaseNo order by PurchaseNo desc";
                SqlCommand sqlCmd = new SqlCommand(sqlSelect, conn);
                sqlCmd.Parameters.Add("@PurchaseNo", SqlDbType.NVarChar);

                sqlCmd.Parameters[0].Value = "%" + period + "%";

                rdr = sqlCmd.ExecuteReader();
                result = 0;
                while (rdr.Read())
                {
                    result = int.Parse(UString.Right(rdr["PurchaseNo"].ToString(), 4));
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
                SqlCommand sqlCmd = new SqlCommand("select * from PurchaseOrder where Id = @Id", conn);
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
                SqlCommand sqlCmd = new SqlCommand("select * from PurchaseOrder where " + fieldName + " = @Value", conn);
                sqlCmd.Parameters.Add("@Value", dataType);
                sqlCmd.Parameters[0].Value = value;

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {
                    this.id = int.Parse(rdr["Id"].ToString());
                    this.vendor = int.Parse(rdr["Vendor"].ToString());
                    this.purchaseNo = rdr["PurchaseNo"].ToString();
                    this.purchaseDate = ulong.Parse(rdr["PurchaseDate"].ToString());
                    this.IssuedDate = ulong.Parse(rdr["IssuedDate"].ToString());
                    this.purchasedBy = int.Parse(rdr["PurchasedBy"].ToString());
                    this.TotalAmount = double.Parse(rdr["TotalAmount"].ToString());
                    this.discount = int.Parse(rdr["Discount"].ToString());
                    this.payment = double.Parse(rdr["Payment"].ToString());
                    this.purchaseType = int.Parse(rdr["PurchaseType"].ToString());
                    this.note = rdr["Note"].ToString();

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
            SqlDataReader rdr = null;
            int result;
            try
            {
                string sqlInsert = "insert into PurchaseOrder(" +
                    "Vendor, PurchaseNo, PurchaseDate, IssuedDate, PurchasedBy, TotalAmount, Discount, Payment, PurchaseType, Note) " +
                    "values(@Vendor, @PurchaseNo, @PurchaseDate, @IssuedDate, @PurchasedBy, @TotalAmount, @Discount,@Payment, @PurchaseType, @Note) " +
                    "SELECT Id FROM PurchaseOrder" +
                    "WHERE Id = SCOPE_IDENTITY();";                

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);
                sqlCmd.Parameters.Add("@Vendor", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PurchaseNo", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@PurchaseDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@IssuedDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@PurchasedBy", SqlDbType.Int);
                sqlCmd.Parameters.Add("@TotalAmount", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Discount", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Payment", SqlDbType.Money);
                sqlCmd.Parameters.Add("@PurchaseType", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Note", SqlDbType.NVarChar);

                sqlCmd.Parameters[0].Value = this.vendor;
                sqlCmd.Parameters[1].Value = this.purchaseNo;
                sqlCmd.Parameters[2].Value = this.purchaseDate;
                sqlCmd.Parameters[3].Value = this.issuedDate;
                sqlCmd.Parameters[4].Value = this.purchasedBy;
                sqlCmd.Parameters[5].Value = this.totalAmount;
                sqlCmd.Parameters[6].Value = this.discount;
                sqlCmd.Parameters[7].Value = this.payment;
                sqlCmd.Parameters[8].Value = this.purchaseType;
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
                DBHelper.SqlDataConnection.CloseSqlConnection();
            }
            if (result > 0) return true;
            else return false;
        }

        public int MoveToDB(SqlConnection conn, SqlTransaction trans)
        {            
            SqlDataReader rdr = null;
            int result = 0;
            object returnObj = null;
            try
            {                

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "ProcInsertPurchaseOrder";
                sqlCmd.Connection = conn;
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@Vendor", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PurchaseNo", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@PurchaseDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@IssuedDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@PurchasedBy", SqlDbType.Int);
                sqlCmd.Parameters.Add("@TotalAmount", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Discount", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Payment", SqlDbType.Money);
                sqlCmd.Parameters.Add("@PurchaseType", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Note", SqlDbType.NVarChar);

                sqlCmd.Parameters[0].Value = this.vendor;
                sqlCmd.Parameters[1].Value = this.purchaseNo;
                sqlCmd.Parameters[2].Value = this.purchaseDate;
                sqlCmd.Parameters[3].Value = this.issuedDate;
                sqlCmd.Parameters[4].Value = this.purchasedBy;
                sqlCmd.Parameters[5].Value = this.totalAmount;
                sqlCmd.Parameters[6].Value = this.discount;
                sqlCmd.Parameters[7].Value = this.payment;
                sqlCmd.Parameters[8].Value = this.purchaseType;
                sqlCmd.Parameters[9].Value = this.note;

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

        public bool DeleteDBFromField(string fieldName, string dataType, string value)
        {
            int result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from PurchaseOrder where " + fieldName + " = @FieldValue", conn);
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
                SqlCommand sqlCmd = new SqlCommand("delete from PurchaseOrder where Id = @Id", conn);
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
            //SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();

            try
            {
                SqlCommand sqlCmd = new SqlCommand("delete from PurchaseOrder where Id = @Id", conn);
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

        public bool UpdateToDB()
        {
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            int result;
            try
            {              

                string sqlUpdate = "update PurchaseOrder set " +
                    "Vendor = @Vendor, PurchaseNo = @PurchaseNo, PurchaseDate = @PurchaseDate, " +
                    "PurchasedBy = @PurchasedBy, TotalAmount = @TotalAmount, Discount = @Discount , Payment = @Payment" +
                    "where Id = @Id";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Parameters.Add("@Id", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Vendor", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PurchaseNo", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@PurchaseDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@IssuedDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@PurchasedBy", SqlDbType.Int);
                sqlCmd.Parameters.Add("@TotalAmount", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Discount", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Payment", SqlDbType.Money);
                sqlCmd.Parameters.Add("@PurchaseType", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Note", SqlDbType.NVarChar);

                sqlCmd.Parameters[0].Value = this.id;
                sqlCmd.Parameters[1].Value = this.vendor;
                sqlCmd.Parameters[2].Value = this.purchaseNo;
                sqlCmd.Parameters[3].Value = this.purchaseDate;
                sqlCmd.Parameters[4].Value = this.issuedDate;
                sqlCmd.Parameters[5].Value = this.purchasedBy;
                sqlCmd.Parameters[6].Value = this.TotalAmount;
                sqlCmd.Parameters[7].Value = this.discount;
                sqlCmd.Parameters[8].Value = this.payment;
                sqlCmd.Parameters[9].Value = this.purchaseType;
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

                string sqlUpdate = "update PurchaseOrder set " +
                    "Vendor = @Vendor, PurchaseNo = @PurchaseNo, PurchaseDate = @PurchaseDate, " +
                    "PurchasedBy = @PurchasedBy, TotalAmount = @TotalAmount, Discount = @Discount, Payment = @Payment, " +
                    "PurchaseType = @PurchaseType, Note = @Note " +
                    "where Id = @Id";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Transaction = trans;

                sqlCmd.Parameters.Add("@Id", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Vendor", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PurchaseNo", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@PurchaseDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@IssuedDate", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@PurchasedBy", SqlDbType.Int);
                sqlCmd.Parameters.Add("@TotalAmount", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Discount", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Payment", SqlDbType.Money);
                sqlCmd.Parameters.Add("@PurchaseType", SqlDbType.Int);
                sqlCmd.Parameters.Add("@Note", SqlDbType.NVarChar);

                sqlCmd.Parameters[0].Value = this.id;
                sqlCmd.Parameters[1].Value = this.vendor;
                sqlCmd.Parameters[2].Value = this.purchaseNo;
                sqlCmd.Parameters[3].Value = this.purchaseDate;
                sqlCmd.Parameters[4].Value = this.issuedDate;
                sqlCmd.Parameters[5].Value = this.purchasedBy;
                sqlCmd.Parameters[6].Value = this.totalAmount;
                sqlCmd.Parameters[7].Value = this.discount;
                sqlCmd.Parameters[8].Value = this.payment;
                sqlCmd.Parameters[9].Value = this.purchaseType;
                sqlCmd.Parameters[10].Value = this.note;

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

        public bool UpdateDBFromField(string fieldName, string value, string dataType)
        {
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            int result;
            try
            {
                string sqlUpdate = "update PurchaseOrder set " +
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