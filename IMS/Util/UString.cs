using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Management;

namespace SmartPOS.Util
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

        static public ulong GetLongFromDate(DateTime dt)
        {
            ulong number = 0;

            //number = dt.ToFileTime();
            TimeSpan span = dt - new DateTime(1970, 1, 1);
            number = (ulong)span.TotalMilliseconds;
            return number;
        }

        static public DateTime GetDateFromLong(ulong number)
        {
            DateTime dateValue;

            //dateValue = DateTime.FromFileTime(number);
            dateValue = new DateTime(1970, 1, 1).Add(TimeSpan.FromMilliseconds(number));
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

        static public string GetCPUID()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                break;
            }
            return cpuInfo;
        }

        static public bool IsNumeric(string num)
        {
            double result;           
            return double.TryParse(num, out result);        
        }
    }
}
