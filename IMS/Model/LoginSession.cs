using System;
using System.Data;
using System.Data.SqlClient;
using IMS.DBHelper;

namespace IMS.Model
{
    public class LoginSession
    {
        int id;
        DateTime loginTime;
        DateTime logoutTime;
        static int userLogin;
        static int userGroup;
        static int employee;
        static string employeeName;
        static public string PharmaName { set; get; }
        static public string PharmaAddress { set; get; }

        bool isPassRemember;

        public LoginSession()
        {
            ;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime LoginTime
        {
            get { return loginTime; }
            set { loginTime = value; }
        }

        public DateTime LogoutTime
        {
            get { return logoutTime; }
            set { logoutTime = value; }
        }

        static public int UserLogin
        {
            get { return userLogin; }
            set { userLogin = value; }
        }

        static public int UserGroup
        {
            get { return userGroup; }
            set { userGroup = value; }
        }

        static public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }

        static public int Employee
        {
            get { return employee; }
            set { employee = value; }
        }  

        public bool IsPassRemember
        {
            get { return isPassRemember; }
            set { isPassRemember = value; }
        }        
    }
}