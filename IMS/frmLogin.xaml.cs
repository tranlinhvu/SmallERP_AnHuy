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

namespace IMS.View
{
    /// <summary>
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window
    {
        MainWindow mainWindow = null;
        public frmLogin()
        {
            InitializeComponent();
        }

        public frmLogin(MainWindow mainWindow_)
        {
            InitializeComponent();
            mainWindow = mainWindow_;

            txtUser.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                int result = dc.ProcLogin(txtUser.Text, txtKey.Password.ToString());
                if (result > 0)
                {
                    var sQuery = (from s in dc.UserViews
                                  where (s.Id == result)
                                  select s).First();
                    IMS.Properties.Settings.Default.UserName = sQuery.UserName;
                    IMS.Properties.Settings.Default.UserGroupName = sQuery.GroupName;
                    IMS.Properties.Settings.Default.StaffName = sQuery.StaffName;
                    IMS.Properties.Settings.Default.IdStaff = sQuery.Id;

                    mainWindow.RefreshGUI(1);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đăng nhập không thành công. Hãy thử lại", "IMS - Thông báo lỗi");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            frmSQLServer frmSQLServer_ = new frmSQLServer();
            frmSQLServer_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmSQLServer_.ShowDialog();
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                txtKey.Focus();
                txtKey.SelectAll();
            }
        }

        private void txtKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(null, null);
            }            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                btnLogin_Click(null, null);
            }
        }
    }
}
