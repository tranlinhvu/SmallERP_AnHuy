using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Management;
using System.Threading;
using IMS.Model;
using System.Security.AccessControl;
using Microsoft.Win32;

namespace IMS.Favorite
{
    class UString
    {
        static public string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }

        static public string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        static public string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }

        static public int GetCharPosInString(string param, char chrFind)
        {            
            for (int i = param.Length - 1; i >= 0; i--)
            {
                if (param[i] == chrFind)
                {
                    return i;
                }
            }
            return -1;
        }

        static public string AddZeroBefore(string param, int num)
        {
            string newString = "";

            if (param.Length < num)
            {
                for (int i = 0; i < num - param.Length; i++)
                {
                    newString = newString + "0";
                }
                return newString + param;
            }

            return param;
        }

        static public string AddZeroToDate(DateTime date)
        {
            string newString = "";
            try
            {                
                newString = String.Format("{0:dd/MM/yyyy}", date);
            }
            catch { };
            return newString;
        }

        static public string AddZeroToTime(DateTime time)
        {
            string newString = "";
            try
            {               
                newString = String.Format("{0:HH:mm:ss}", time);
            }
            catch { };
            return newString;
        }

        static public string ConvertToVNCurrency(string money)
        {
            //System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-US");
            string newString = "";
            try
            {
                double doub = Convert.ToDouble(money);
                newString = string.Format("{0:c}", doub);
                return newString;
            }
            catch
            {
                return "";
            };
        }

        static public long GetLongFromDate(DateTime dt)
        {
            long number = 0;

            //number = dt.ToFileTime();
            TimeSpan span = dt - new DateTime(1970, 1, 1);
            number = (long)span.TotalMilliseconds;
            return number;
        }

        static public DateTime GetDateFromLong(long number)
        {
            DateTime dateValue;

            //dateValue = DateTime.FromFileTime(number);
            dateValue = new DateTime(1970, 1, 1).Add(TimeSpan.FromMilliseconds(number));
            return dateValue;
        }

        static public string GetDateStringFromLong(long number)
        {
            string dateValue;

            //dateValue = DateTime.FromFileTime(number);
            dateValue = new DateTime(1970, 1, 1).Add(TimeSpan.FromMilliseconds(number)).ToString();
            return dateValue;
        }

        static public DateTime GetLastDateOfMonth(string monthYear)
        {
            DateTime lastDate;
            try
            {
                int month = int.Parse(Left(monthYear, 2));
                int year = int.Parse(Right(monthYear, 4));

                int numberOfDays = DateTime.DaysInMonth(year, month);
                lastDate = new DateTime(year, month, numberOfDays);
                lastDate = DateTime.Parse(lastDate.ToShortDateString() + " 23:59:59");
                return lastDate;
            }
            catch
            {
                lastDate = new DateTime(1970, 1, 1, 0, 0, 0);
            }
            return lastDate;
        }

        static public string GetLastDateOfMonthString(string monthYear)
        {
            int month = int.Parse(Left(monthYear, 2));
            int year = int.Parse(Right(monthYear, 4));

            int numberOfDays = DateTime.DaysInMonth(year, month);
            DateTime lastDay = new DateTime(year, month, numberOfDays);
            return lastDay.ToShortDateString(); // +" 23:59:59";
        }

        static public DateTime GetLastDateOfMonth(int month, int year)
        {    
            int numberOfDays = DateTime.DaysInMonth(year, month);
            DateTime lastDay = new DateTime(year, month, numberOfDays);
            return lastDay;
        }

        static public long MonthDifference(DateTime startDate, DateTime endDate)
        {
            long monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return monthsApart;
        }

        

        static public bool IsNumeric(string num)
        {
            double result;           
            return double.TryParse(num, out result);        
        }

        static public void SetSystem()
        {
            int[] ARR = { 3, 3, 3 };
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("vi-VN");
            System.Globalization.DateTimeFormatInfo dateTimeInfo = new System.Globalization.DateTimeFormatInfo();
            System.Globalization.NumberFormatInfo NumberInfo = new System.Globalization.NumberFormatInfo();

            dateTimeInfo.DateSeparator = "/";
            dateTimeInfo.LongDatePattern = "dd/MMM/yyyy";
            dateTimeInfo.ShortDatePattern = "dd/MM/yyyy";
            dateTimeInfo.LongTimePattern = "HH:mm:ss";
            dateTimeInfo.ShortTimePattern = "hh:mm tt";

            NumberInfo.CurrencySymbol = "";
            NumberInfo.CurrencyDecimalDigits = 0;
            NumberInfo.CurrencyDecimalSeparator = ",";
            NumberInfo.CurrencyGroupSizes = ARR;
            NumberInfo.CurrencyGroupSeparator = ".";
            NumberInfo.PositiveInfinitySymbol = " ";
            NumberInfo.NumberGroupSeparator = ".";

            //dateTimeInfo.SetAllDateTimePatterns = "dd/MM/yyyy,hh:mm:ss tt";
            cultureInfo.DateTimeFormat = dateTimeInfo;
            cultureInfo.NumberFormat = NumberInfo;
            //Application.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        static public SQLServer ReadRegistry()
        {

            SQLServer sql = new SQLServer();                 
            
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("IMSERP_AnHuy", false);          

            try
            {
                if (rk != null)
                {                    
                    sql.Name = rk.GetValue("ServerName").ToString();
                    sql.Database = rk.GetValue("DatabaseName").ToString();
                    sql.User = rk.GetValue("UserName").ToString();
                    sql.Key = rk.GetValue("Key").ToString();
                }
                else
                {
                    sql = null;
                }
            }
            catch (Exception ex)
            {
                sql = null;
            }            
            
            if (rk != null) rk.Close();
           
            return sql;
        }

        static public bool WriteRegistry(SQLServer sql)
        {
            bool result = false;
            string user = Environment.UserDomainName + "\\" + Environment.UserName;

            RegistrySecurity rs = new RegistrySecurity();         
            rs.AddAccessRule(new RegistryAccessRule(user,
                RegistryRights.WriteKey | RegistryRights.ReadKey | RegistryRights.ChangePermissions,
                InheritanceFlags.None,
                PropagationFlags.None,
                AccessControlType.Allow));


            RegistryKey rk = null;
            try
            {
                try
                {
                    Registry.CurrentUser.DeleteSubKey("IMSERP_AnHuy");
                }
                catch(Exception ex)
                {
                    ;
                }
                rk = Registry.CurrentUser.CreateSubKey("IMSERP_AnHuy", RegistryKeyPermissionCheck.Default, rs);
                rk.SetValue("ServerName", sql.Name);
                rk.SetValue("DatabaseName", sql.Database);
                rk.SetValue("UserName", sql.User);
                rk.SetValue("Key", sql.Key);
                result = true;           
            }
            catch (Exception ex)
            {
                result = false;
            }

            if (rk != null) rk.Close();           

            return result;            
        }

        static public string GetVNFormatString(long num)
        {
            return string.Format("{0:C}", num);
        }
    }
}
