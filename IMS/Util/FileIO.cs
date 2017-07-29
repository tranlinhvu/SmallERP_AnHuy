using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Net;

namespace SmartPOS.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class FileIO
    {
        #region Fields

        /// <summary>
        /// Log file chung
        /// </summary>
        private static string LOG_FILE = "./LogFile_{0}.txt";

        /// <summary>
        /// Log file chup hinh, nhan dang
        /// </summary>
        private static string CAPTURE_LOG_FILE = "./CaptureLog_{0}.txt";

        /// <summary>
        /// Log file SQL
        /// </summary>
        private static string SQL_LOG_FILE = "./SQLLog_{0}.txt";

        /// <summary>
        /// Log file kiem tra ket noi
        /// </summary>
        private static string CONNECTION_LOG_FILE = "./ConnectionLog_{0}.txt";

        /// <summary>
        /// Log file ghi giao dich voi OBU
        /// </summary>
        private static string OBU_LOG_FILE = "./ObuLog_{0}_{1}.txt";

        #endregion

        #region Get Value



        /// <summary>
        /// Lấy giá trị số nguyên từ CSDL
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public static int GetIntegerValue(string pID)
        {
            return (string.IsNullOrEmpty(pID)) ? -1 : int.Parse(pID);
        }

        /// <summary>
        /// Lấy giá trị số nguyên từ CSDL
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public static long GetLongIntegerValue(string pID)
        {
            return (string.IsNullOrEmpty(pID)) ? -1 : long.Parse(pID);
        }

        /// <summary>
        /// Lấy giá trị số thực từ CSDL
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public static double GetDoubleValue(string pID)
        {
            return (string.IsNullOrEmpty(pID)) ? -1 : double.Parse(pID);
        }

        /// <summary>
        /// Lấy giá trị ngày tháng từ CSDL
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeValue(object pDateTime)
        {
            return (string.IsNullOrEmpty(pDateTime.ToString())) ? DateTime.MinValue : (DateTime)pDateTime;
        }

        /// <summary>
        /// Lấy giá trị boolean từ CSDL
        /// </summary>
        /// <param name="pBool"></param>
        /// <returns></returns>
        public static bool GetBooleanValue(object pBool)
        {
            return (string.IsNullOrEmpty(pBool.ToString())) ? false : (bool)pBool;
        }

        #endregion

        #region Database

        /// <summary>
        /// Hàm khởi tạo SqlCommand
        /// </summary>
        /// <param name="pStore"></param>
        /// <param name="pActivity"></param>
        /// <returns></returns>
        public static SqlCommand InitCommand(string pStoredName, string pActivity)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = pStoredName;
            cmd.Parameters.Add("@Activity", SqlDbType.NVarChar, 50).Value = pActivity;
            return cmd;
        }

        /// <summary>
        /// Khoi tao command khong co activity
        /// </summary>
        /// <param name="pStoredName"></param>
        /// <returns></returns>
        public static SqlCommand InitCommand(string pStoredName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = pStoredName;
            return cmd;
        }
        /// <summary>
        /// Ham them tham so kieu integer vao command (kiem tra gia tri null)
        /// </summary>
        /// <param name="pCmd"></param>
        /// <param name="pParamName"></param>
        /// <param name="pValue"></param>
        public static void AddIntegerParam(SqlCommand pCmd, string pParamName, int pValue)
        {
            if (pValue != -1)
                pCmd.Parameters.Add(pParamName, SqlDbType.Int).Value = pValue;
            else
                pCmd.Parameters.Add(pParamName, SqlDbType.Int).Value = DBNull.Value;

        }

        public static string AntiSQLInject(string str)
        {
            if (str == null) return "";
            return str.Replace("'", "\\'");
        }

        /// <summary>
        /// Lay dong chua du lieu can tim
        /// </summary>
        /// <param name="pDataTable"></param>
        /// <param name="pColumnName"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static DataRow GetDataRow(DataTable pDataTable, string pColumnName, string pValue)
        {
            foreach (DataRow dr in pDataTable.Rows)
            {
                if (dr[pColumnName].ToString() == pValue)
                    return dr;
            }

            return null;
        }

        #endregion

        #region Zip file

        //private Ultils();
        //sourceFile = @"D:\Ritesh\standards.pdf"
        //destinationFile = @"C:\backup\standards.zip"
        public static bool ZipFile(string sourceFile, string destinationFile)
        {
            try
            {
                FileStream oldFile = File.OpenRead(sourceFile);
                FileStream newFile = File.Create(destinationFile);
                GZipStream compression = new GZipStream(newFile, CompressionMode.Compress);

                byte[] buffer = new byte[1024];
                int numberOfBytesRead = oldFile.Read(buffer, 0, buffer.Length);
                while (numberOfBytesRead > 0)
                {
                    compression.Write(buffer, 0, numberOfBytesRead);
                    numberOfBytesRead = oldFile.Read(buffer, 0, buffer.Length);
                }
                compression.Close();
                return true;
            }
            catch (Exception ex)
            {
                FileIO.WriteLogFile("ZipFile:" + sourceFile, ex.Message);
                return false;
            }
        }

        //Example data to run UnZipFile function :
        //sourceFile = @"C:\backup\standards.zip
        //destinationFile = @"C:\backup\standards.pdf"
        public static bool UnZipFile(string sourceFile, string destinationFile)
        {
            try
            {
                FileStream compressFile = File.Open(sourceFile, FileMode.Open);
                FileStream uncompressedFile = File.Create(destinationFile);
                GZipStream compression = new GZipStream(compressFile, CompressionMode.Decompress);
                int data = compression.ReadByte();
                while (data != -1)
                {
                    uncompressedFile.WriteByte((byte)data);
                    data = compression.ReadByte();
                }
                compression.Close();
                return true;
            }
            catch (Exception ex)
            {
                FileIO.WriteLogFile("UnZipFile:" + sourceFile, ex.Message);
                return false;
            }
        }

        #endregion

        #region Misc

        /// <summary>
        /// Ham ghi log file chung
        /// </summary>
        /// <param name="strFuncName"></param>
        /// <param name="strMsg"></param>
        public static void WriteLogFile(string strFuncName, string strMsg)
        {
            string strDate = String.Format("{0:yyyy/MM/dd}", DateTime.Now).Replace("/", "");
            string filename = string.Format(LOG_FILE, strDate);
            System.IO.StreamWriter sw = System.IO.File.AppendText(filename);
            try
            {
                string logLine = System.String.Format("{0:G}: {1}.", System.DateTime.Now, "[" + strFuncName + " - " + strMsg + "] ");
                sw.WriteLine(logLine);
                sw.WriteLine("-------------------------------------------");
            }
            finally
            {
                sw.Close();
            }
#if DEBUG
            //FileIO.ShowError(strFuncName,strMsg);              
#endif
        }

        /// <summary>
        /// Ham ghi log file chung
        /// </summary>
        /// <param name="strFuncName"></param>
        /// <param name="strMsg"></param>
        public static void WriteLogFile(Exception ex)
        {
#if DEBUG
            FileIO.ShowError(ex);
#endif
            string strDate = String.Format("{0:yyyy/MM/dd}", DateTime.Now).Replace("/", "");
            string filename = string.Format(LOG_FILE, strDate);
            System.IO.StreamWriter sw = System.IO.File.AppendText(filename);
            try
            {
                string strFuncName = string.Format("{0}.{1}()", ex.TargetSite.DeclaringType.FullName, ex.TargetSite.Name);
                string logLine = System.String.Format("{0:G}: [{1}].", System.DateTime.Now, strFuncName);
                sw.WriteLine(logLine);
                sw.WriteLine(ex.Message);
                sw.WriteLine("-------------------------------------------");
            }
            catch (Exception exx)
            {
                FileIO.ShowError(exx.Message);
            }
            finally
            {
                sw.Close();
            }
        }

        /// <summary>
        /// Ham ghi log file capture
        /// </summary>
        /// <param name="strFuncName"></param>
        /// <param name="strMsg"></param>
        public static void WriteLogFile_Capture(Exception ex)
        {
#if DEBUG
            FileIO.ShowError(ex);
#endif
            string strDate = String.Format("{0:yyyy/MM/dd}", DateTime.Now).Replace("/", "");
            string filename = string.Format(CAPTURE_LOG_FILE, strDate);
            System.IO.StreamWriter sw = System.IO.File.AppendText(filename);
            try
            {
                string strFuncName = string.Format("{0}.{1}()", ex.TargetSite.DeclaringType.FullName, ex.TargetSite.Name);
                string logLine = System.String.Format("{0:G}: [{1}].", System.DateTime.Now, strFuncName);
                sw.WriteLine(logLine);
                sw.WriteLine(ex.Message);
                sw.WriteLine("-------------------------------------------");

            }
            catch (Exception exx)
            {
                FileIO.ShowError(exx.Message);
            }
            finally
            {
                sw.Close();
            }
        }

        /// <summary>
        /// Ham ghi log file OBU transaction
        /// </summary>
        /// <param name="strFuncName"></param>
        /// <param name="strMsg"></param>
        public static void WriteLogFile_OBU(string pTransLog)
        {
            string strDate = String.Format("{0:yyyy/MM/dd}", DateTime.Now).Replace("/", "");
            string filename = string.Format(OBU_LOG_FILE, strDate, DateTime.Now.Hour.ToString());
            System.IO.StreamWriter sw = System.IO.File.AppendText(filename);

            try
            {
                sw.WriteLine(pTransLog);
            }
            catch (Exception ex)
            {
                FileIO.ShowError(ex.Message);
            }
            finally
            {
                sw.Close();
            }
        }

        /// <summary>
        /// Ham open log file OBU transaction
        /// </summary>
        /// <param name="strFuncName"></param>
        /// <param name="strMsg"></param>
        public static StreamReader OpenLogFile_OBU()
        {
            try
            {
                string strDate = String.Format("{0:yyyy/MM/dd}", DateTime.Now).Replace("/", "");
                string fileName = string.Format(OBU_LOG_FILE, strDate, DateTime.Now.Hour.ToString());

                return OpenLogFile_OBU(fileName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static StreamReader OpenLogFile_OBU(DateTime pDateTime, int hour)
        {
            try
            {
                string strDate = String.Format("{0:yyyy/MM/dd}", pDateTime).Replace("/", "");
                string fileName = string.Format(OBU_LOG_FILE, strDate, hour.ToString());

                return OpenLogFile_OBU(fileName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Ham open log file OBU transaction
        /// </summary>
        /// <param name="strFuncName"></param>
        /// <param name="strMsg"></param>
        public static StreamReader OpenLogFile_OBU(string pFileName)
        {
            StreamReader sr = null;
            try
            {
                if (File.Exists(pFileName))
                {
                    FileStream fs = new FileStream(pFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    sr = new StreamReader(fs);
                }
            }
            catch (Exception)
            {
            }

            return sr;
        }

        /// <summary>
        /// Ham ghi log file  kiem tra ket noi
        /// </summary>
        /// <param name="strFuncName"></param>
        /// <param name="strMsg"></param>
        public static void WriteLogFile_Connection(Exception ex)
        {
#if DEBUG
            FileIO.ShowError(ex);
#endif
            string strDate = String.Format("{0:yyyy/MM/dd}", DateTime.Now).Replace("/", "");
            string filename = string.Format(CONNECTION_LOG_FILE, strDate);
            System.IO.StreamWriter sw = System.IO.File.AppendText(filename);
            try
            {
                string strFuncName = string.Format("{0}.{1}()", ex.TargetSite.DeclaringType.FullName, ex.TargetSite.Name);
                string logLine = System.String.Format("{0:G}: [{1}].", System.DateTime.Now, strFuncName);
                sw.WriteLine(logLine);
                sw.WriteLine(ex.Message);
                sw.WriteLine("-------------------------------------------");

            }
            catch (Exception exx)
            {
                FileIO.ShowError(exx.Message);
            }
            finally
            {
                sw.Close();
            }
        }

        /// <summary>
        /// Ham ghi log file  kiem tra ket noi
        /// </summary>
        /// <param name="strFuncName"></param>
        /// <param name="strMsg"></param>
        public static void WriteLogFile_SQL(Exception ex)
        {
#if DEBUG
            FileIO.ShowError(ex);
#endif
            string strDate = String.Format("{0:yyyy/MM/dd}", DateTime.Now).Replace("/", "");
            string filename = string.Format(SQL_LOG_FILE, strDate);
            System.IO.StreamWriter sw = System.IO.File.AppendText(filename);
            try
            {
                string strFuncName = string.Format("{0}.{1}()", ex.TargetSite.DeclaringType.FullName, ex.TargetSite.Name);
                string logLine = System.String.Format("{0:G}: [{1}].", System.DateTime.Now, strFuncName);
                sw.WriteLine(logLine);
                sw.WriteLine(ex.Message);
                sw.WriteLine("-------------------------------------------");

            }
            catch (Exception exx)
            {
                FileIO.ShowError(exx.Message);
            }
            finally
            {
                sw.Close();
            }
        }

        /// <summary>
        /// Them so 0 vao truoc mot so
        /// </summary>
        /// <param name="pNumber">So can them</param>
        /// <param name="pNumNumbers">So chu so</param>
        /// <returns></returns>
        public static string AddZeroBeforeNumber(int pNumber, int pNumNumbers)
        {
            string retValue = pNumber.ToString();

            for (int i = pNumber.ToString().Length; i < pNumNumbers; i++)
            {
                retValue = "0" + retValue;
            }
            return retValue;
        }

        //Create unique ID for each transaction
        public static string CreateTransactionID(bool pForObu)
        {
            DateTime now = DateTime.Now;
            string TransID = string.Empty;

            TransID = String.Format("{0:yyyy:MM:dd:HH:mm:ss}", now);
            TransID = TransID.Replace(":", "");
            return TransID;
        }

        /// <summary>
        /// Dùng để move thư mục, du`ng cho cac thu muc cu`ng o dia
        /// </summary>
        /// <param name="pSourceFolder"></param>
        /// <param name="pDestinationFolder"></param>
        public static void MoveFolder(string pSourceFolder, string pDestinationFolder)
        {
            try
            {
                System.IO.Directory.Move(pSourceFolder, pDestinationFolder);
            }
            catch (Exception ex)
            {
                string function = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString()
                                  + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                FileIO.WriteLogFile(function, ex.Message);
            }
        }

        public static void ShowError(string pMessage)
        {
            //MessageBox.Show(pMessage, CarParking.Properties.Resources.TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pSource">Noi phat sinh loi</param>
        /// <param name="pMessage">Noi dung loi</param>
        public static void ShowError(string pSource, string pMessage)
        {
            if (pSource != null && pMessage != null)
            {
                MessageBox.Show(pMessage, pSource, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ShowError(Exception ex)
        {
            //string msg = string.Format("{0}.{1}(): {2}.", ex.TargetSite.DeclaringType.FullName, ex.TargetSite.Name, ex.Message);
            //MessageBox.Show(msg, CarParking.Properties.Resources.TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Chuan hoa servername
        /// Ex: NormalizeServerName(VODINHHUY\SQL2008)=>VODINHHUY
        /// </summary>
        /// <param name="pServerName"></param>
        /// <returns></returns>
        public static string NormalizeServerName(string pServerName)
        {
            try
            {
                string server = string.Empty;
                if (string.IsNullOrEmpty(pServerName))
                {

                    return string.Empty;
                }
                else
                {
                    string[] tempArr = pServerName.Split('\\');
                    if (tempArr.Length > 0) return tempArr[0];
                    else
                        return pServerName;
                }
            }
            catch (Exception ex)
            {
                string function = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString()
                                  + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                FileIO.WriteLogFile(function, ex.Message);
            }
            return string.Empty;
        }

        /// <summary>
        /// Chuyển thời gian sang giây
        /// </summary>
        /// <param name="pTime"></param>
        /// <returns></returns>
        public static int ConvertToSecond(DateTime pTime)
        {
            if (pTime == null)
            {
                return -1;
            }
            else
            {
                int total = (pTime.Hour * 3600 + pTime.Minute * 60 + pTime.Second);
                return total;
            }
        }

        #endregion

        #region Ftp
        public static void UploadFile(string pFTPAddress, string pFolder, string pFilePath, string pUsername, string pPassword)
        {
            ///////////////////////////////
            FileInfo fileInf = new FileInfo(pFilePath);
            string uri = "ftp://" + pFTPAddress + pFolder + fileInf.Name;
            FtpWebRequest reqFTP;
            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(pUsername, pPassword);
            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            reqFTP.KeepAlive = false;
            /*set proxy to null to avoid the System.InvalidOperationException: 
            The requested FTP command is not supported when using HTTP proxy*/
            reqFTP.Proxy = null;//Huy
            //request.UsePassive = true;
            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            // Specify the data transfer type.
            reqFTP.UseBinary = true;
            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = fileInf.Length;

            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            FileStream fs = fileInf.OpenRead();
            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Upload Error");
            }
        }
        #endregion

        #region File doi soat

        /// <summary>
        /// Tạo file tóm tắt các giao dịch
        /// </summary>
        public static void CreateTransactionsSummaryFile(string pStationCode, DateTime pFromDate, DateTime pToDate)
        {
            //tên file: yymmdd_TRANS_95xxxx.txt  
            //dòng tiêu đề
            //dòng giao dịch
            //dòng kết thúc
            /* Mô tả dòng tiêu đề:Loại bản ghi|Mã trạm thu phí|Ngày giao dịch(ddmmyy)
             * Ví dụ: 0001|950001|240310
             * Dòng giao dịch:  Loại bản ghi|Mã định dạng của giao dịch|Số ObuId|Số PAN19|Số tiền giao dịch(12số)
             * |giờ giao dịch(hhmmss:24)|ngày giao dịch(DDMMYYYY)|Transaction Trace Sequence(12số)|Số tham chiếu giao dịch gốc(19char)|Status|
             *Dòng kết thúc:
             *Loại bản ghi|Số dòng giao dịch trong file(9số)|giờ tạo file(hhmmss24)|ngày tạo file(ddMMyy)
             */

            string strDate = String.Format("{0:yyyy/MM/dd}", DateTime.Now).Replace("/", "");
            string RecordType = "0001";
            string StationCode = pStationCode;//"950001";
            int TotalRecord = 9;
            string HeaderRecord = string.Format("{0:0000}|{1:000000}|{2:ddMMyy}", RecordType, StationCode, DateTime.Now);
            string TrailerRecord = string.Format("{0:0000}|{1:000000000}|{2:HHmmss}|{3:ddMMyy}",
                                                    RecordType, TotalRecord, DateTime.Now, DateTime.Now);
            string DetailRecord = string.Format("{0:0000}|{1:6}|{2:10}|{3:19}|{4:000000000000}|{5:HHmmss}|{6:ddMMyyyy}" +
                "|{7:000000000000}|{2:ddMMyy}|{2:ddMMyy}",
                                RecordType, 99, DateTime.Now, DateTime.Now);
            string filename = string.Format(LOG_FILE, strDate);
            System.IO.StreamWriter sw = System.IO.File.AppendText(filename);
            try
            {
                string logLine = "";//System.String.Format("{0:G}: {1}.", System.DateTime.Now, "[" + strFuncName + " - " + strMsg + "] ");
                sw.WriteLine(logLine);
                sw.WriteLine("-------------------------------------------");
            }
            finally
            {
                sw.Close();
            }
        }

        #endregion
    }
    /// <summary>
    /// Lop dung de chua ket qua ping
    /// </summary>
    public class PingSysResult
    {
        public int MainServer = -1;
        public int LocalServer = -1;
        public int Trx = -1;
    }
    /// <summary>
    /// Dung de chua thong tin can hien thi len man hinh soat ve
    /// </summary>
    public class LineDisplay
    {
        /// <summary>
        /// 
        /// </summary>
        public string Line1 = "";
        public string Line2 = "";
        public string Line3 = "";
        public string Line4 = "";
        /// <summary>
        /// Xoa thong tin 
        /// </summary>
        internal void Clear()
        {
            Line1 = Line2 = Line3 = Line4 = "";
        }

    }

    /// <summary>
    /// Trang thai ket noi cua cac máy lien quan
    /// </summary>
    public class ConnectionsStatus
    {
        /// <summary>
        /// Server1
        /// </summary>
        public bool ServerStatus = true;
        public bool LocalStatus = false;
        public bool TrxStatus = false;
    }

}
