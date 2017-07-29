using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;

namespace IMS.Model
{
    public class Pharma
    {
        int id;
        string pharmaCode;
        string name;
        string description;
        int manuf;
        int pharmaGroup;
        string standardPack;
        double refSelPrice;
        int unit;
        int prescriptionType;

        public Pharma()
        {
            ;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string PharmaCode
        {
            get { return pharmaCode; }
            set { pharmaCode = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int Manuf
        {
            get { return manuf; }
            set { manuf = value; }
        }

        public int PharmaGroup
        {
            get { return pharmaGroup; }
            set { pharmaGroup = value; }
        }

        public string StandardPack
        {
            get { return standardPack; }
            set { standardPack = value; }
        }

        public double RefSelPrice
        {
            get { return refSelPrice; }
            set { refSelPrice = value; }
        }

        public int Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        public int PrescriptionType
        {
            get { return prescriptionType; }
            set { prescriptionType = value; }
        }
        public long GetIdLagest()
        {
            long result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select top 1 * from Pharma order by Id desc", conn);

                rdr = sqlCmd.ExecuteReader();
                result = 0;
                while (rdr.Read())
                {
                    result = int.Parse(rdr["Id"].ToString());
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
        public bool IsExistedInInventory()
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from PharmaInventory where PharmaInventory.PharmaId = @Id", conn);

                sqlCmd.Parameters.Add("@Id", SqlDbType.Int);
                sqlCmd.Parameters[0].Value = this.id;

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
        
        public bool IsExisted()
        {
            bool result;
            SqlConnection conn = DBHelper.SqlDataConnection.GetSqlConnection();
            SqlDataReader rdr = null;
            try
            {
                SqlCommand sqlCmd = new SqlCommand("select * from Pharma where Id = @Id", conn);
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
                SqlCommand sqlCmd = new SqlCommand("select * from Pharma where " + fieldName + " = @Value", conn);
                sqlCmd.Parameters.Add("@Value", dataType);
                sqlCmd.Parameters[0].Value = value;

                rdr = sqlCmd.ExecuteReader();
                result = false;
                while (rdr.Read())
                {     
                    this.id = int.Parse(rdr["Id"].ToString());
                    this.pharmaCode = rdr["PharmaCode"].ToString();
                    this.name = rdr["Name"].ToString();
                    this.description = rdr["Description"].ToString();
                    this.manuf = int.Parse(rdr["Manuf"].ToString());
                    this.pharmaGroup = int.Parse(rdr["PharmaGroup"].ToString());
                    this.standardPack = rdr["StandardPack"].ToString();
                    this.refSelPrice = double.Parse(rdr["RefSelPrice"].ToString());
                    this.unit = int.Parse(rdr["Unit"].ToString());
                    this.prescriptionType = int.Parse(rdr["PrescriptionType"].ToString());

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
                string sqlInsert = "insert into Pharma(" +
                    "PharmaCode, Name, Description, Manuf, PharmaGroup, StandardPack, RefSelPrice, Unit, PrescriptionType)" +
                    " values(@PharmaCode, @Name, @Description, @Manuf, @PharmaGroup, @StandardPack, @RefSelPrice, @Unit, @PrescriptionType)";

                SqlCommand sqlCmd = new SqlCommand(sqlInsert, conn);
                sqlCmd.Parameters.Add("@PharmaCode", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Manuf", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaGroup", SqlDbType.Int);
                sqlCmd.Parameters.Add("@StandardPack", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@RefSelPrice", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Unit", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PrescriptionType", SqlDbType.Int);

                sqlCmd.Parameters[0].Value = this.pharmaCode;
                sqlCmd.Parameters[1].Value = this.name;
                sqlCmd.Parameters[2].Value = this.description;
                sqlCmd.Parameters[3].Value = this.manuf;
                sqlCmd.Parameters[4].Value = this.pharmaGroup;
                sqlCmd.Parameters[5].Value = this.standardPack;
                sqlCmd.Parameters[6].Value = this.refSelPrice;
                sqlCmd.Parameters[7].Value = this.unit;
                sqlCmd.Parameters[8].Value = this.prescriptionType;

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
                SqlCommand sqlCmd = new SqlCommand("select * from Pharma where " + fieldName + " = @FieldValue", conn);
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
                SqlCommand sqlCmd = new SqlCommand("delete from Pharma where Id = @Id", conn);
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
                string sqlUpdate = "update Pharma set " +
                    "PharmaCode = @PharmaCode, Name = @Name, Description = @Description, Manuf = @Manuf, " +
                    "PharmaGroup = @PharmaGroup, StandardPack = @StandardPack, RefSelPrice = @RefSelPrice,  PrescriptionType = @PrescriptionType, " +
                    "Unit =@Unit where Id = @Id";

                SqlCommand sqlCmd = new SqlCommand(sqlUpdate, conn);
                sqlCmd.Parameters.Add("@Id", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaCode", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@Manuf", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PharmaGroup", SqlDbType.Int);
                sqlCmd.Parameters.Add("@StandardPack", SqlDbType.NVarChar);
                sqlCmd.Parameters.Add("@RefSelPrice", SqlDbType.Money);
                sqlCmd.Parameters.Add("@Unit", SqlDbType.Int);
                sqlCmd.Parameters.Add("@PrescriptionType", SqlDbType.Int);


                sqlCmd.Parameters[0].Value = this.id;
                sqlCmd.Parameters[1].Value = this.pharmaCode;
                sqlCmd.Parameters[2].Value = this.name;
                sqlCmd.Parameters[3].Value = this.description;
                sqlCmd.Parameters[4].Value = this.manuf;
                sqlCmd.Parameters[5].Value = this.pharmaGroup;
                sqlCmd.Parameters[6].Value = this.standardPack;
                sqlCmd.Parameters[7].Value = this.refSelPrice;
                sqlCmd.Parameters[8].Value = this.unit;
                sqlCmd.Parameters[9].Value = this.prescriptionType;

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
                string sqlUpdate = "update Pharma set " +
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