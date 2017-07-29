using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IMS.Properties;
using Microsoft.Win32;
using IMS.Favorite;
using IMS.Model;

namespace IMS.View
{
    /// <summary>
    /// Interaction logic for frmSQLServer.xaml
    /// </summary>
    public partial class frmSQLServer : Window
    {
        SQLServer sql;


        public frmSQLServer()
        {
            InitializeComponent();
            sql = UString.ReadRegistry();     
            if(sql != null)
            {
                txtSerer.Text = sql.Name;
                txtDatabaseName.Text = sql.Database;
                txtUserName.Text = sql.User;
                txtPassword.Password = sql.Key;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            sql = new SQLServer();
            sql.Name = txtSerer.Text;
            sql.Database = txtDatabaseName.Text;
            sql.User = txtUserName.Text;
            sql.Key = txtPassword.Password;          

            UString.WriteRegistry(sql);
            IMS.Properties.Settings.Default.SmallERPConnectionString = "Data Source=" + sql.Name + ";Initial Catalog=" + sql.Database + ";User ID=" + sql.User + ";Password=" + sql.Key + ";Encrypt=False;TrustServerCertificate=True";
            this.Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
