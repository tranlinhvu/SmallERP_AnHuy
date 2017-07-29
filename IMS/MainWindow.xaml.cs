using IMS.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security;
using System.Security.AccessControl;
using IMS.Model;
using IMS.Favorite;
using IMS.DBHelper;
using System.Threading;
using IMS.Report;

namespace IMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                //Thiet lap timer
                System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();

                //Thiết lập định dang VN
                SetSystem();

                //Hiển thị nội dung công ty
                //splCompany.Visibility = Visibility.Visible;
                tabDynamic.Visibility = Visibility.Collapsed;

                //Thông tin status               
                txtLeftStatus.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                txtRightStatus.Text = "Nhân viên: " + IMS.Properties.Settings.Default.StaffName;
                txtRightStatus.Text = txtRightStatus.Text + "  -  Nhóm người dùng: " + IMS.Properties.Settings.Default.UserGroupName;

                //Menu hiển thị
                mnuMainMenu.SelectedIndex = 0;

                RefreshGUI(0);

                SQLServer sql = UString.ReadRegistry();
                if (sql != null)
                {
                    IMS.Properties.Settings.Default.SmallERPConnectionString = "Data Source=" + sql.Name + ";Initial Catalog=" + sql.Database + ";User ID=" + sql.User + ";Password=" + sql.Key + ";Encrypt=False;TrustServerCertificate=True";
                }
                else
                {
                    MessageBox.Show("Chưa cấu hình cơ sở dữ liệu", "IMS - Thông báo lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // code goes here
            txtLeftStatus.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
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
            //Application.C = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
        public void RefreshGUI(int i)
        {
            if (i == 0)
            {
                tab0.IsEnabled = true;
                tab1.IsEnabled = false;
                tab2.IsEnabled = false;
                tab3.IsEnabled = false;
                tab4.IsEnabled = false;
                tab5.IsEnabled = false;
                tab6.IsEnabled = false;
                tab7.IsEnabled = false;
                tab8.IsEnabled = true;

                btnBackup.IsEnabled = false;
                btnRestore.IsEnabled = false;
                btnLogin.IsEnabled = true;
                btnLogout.IsEnabled = false;
                btnClose.IsEnabled = true;

                //Hiển thị nội dung công ty
                //splCompany.Visibility = Visibility.Visible;
                tabDynamic.Visibility = Visibility.Collapsed;
            }
            else if (i == 1)
            {
                tab0.IsEnabled = true;
                tab1.IsEnabled = true;
                tab2.IsEnabled = true;
                tab3.IsEnabled = true;
                tab4.IsEnabled = true;
                tab5.IsEnabled = true;
                tab6.IsEnabled = true;
                tab7.IsEnabled = true;
                tab8.IsEnabled = true;

                btnBackup.IsEnabled = true;
                btnRestore.IsEnabled = true;
                btnLogin.IsEnabled = false;
                btnLogout.IsEnabled = true;
                btnClose.IsEnabled = true;

                mnuMainMenu.SelectedIndex = 3;

                //Hiển thị nội dung công ty
                //splCompany.Visibility = Visibility.Collapsed;
                tabDynamic.Visibility = Visibility.Visible;

                //Status
                txtLeftStatus.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                txtRightStatus.Text = "Nhân viên: " + IMS.Properties.Settings.Default.StaffName;
                txtRightStatus.Text = txtRightStatus.Text + "  -  Nhóm người dùng: " + IMS.Properties.Settings.Default.UserGroupName;
            }
        } 

        private int GetTabItem(string tabHeader, string tabname)
        {
            //mnuMainMenu.IsMinimized = true;

            // create new tab item
            ClosableTab tab = new ClosableTab();
            tab.HorizontalAlignment = HorizontalAlignment.Left;
            tab.Title = tabHeader;            
            tab.Height = 30;
            tab.FontSize = 18;
            tab.Name = tabname;
            //tab.HeaderTemplate = tabDynamic.FindResource("TabHeader") as DataTemplate;


            for (int i = 0; i < tabDynamic.Items.Count; i++)
            {
                ClosableTab ti = tabDynamic.Items[i] as ClosableTab;

                if (ti.Name == tabname)
                {
                    return i;
                }
            }

            Frame f = new Frame(); 
            switch (tabname)
            {
                //Danh mục
                //Nhân sự
                case "tabStaff":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgStaff.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabCustomer":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgCustomer.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabVendor":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgVendor.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabManufacture":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgManufacture.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                //Dịch vụ
                case "tabServiceGroup":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgServiceGroup.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabService":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgService.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabProduct":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgProduct.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabEquipment":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgEquipment.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabServiceDetail":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgServiceDetail.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;                    
               
                //Dịch vụ
                case "tabObjectCareOrder":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgObjectCareOrder.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabObjectCareSale":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgObjectCareSale.xaml?ObjectCareDetailParam = -1", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabObjectCarePayment":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgObjectCarePayment.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabObjectCareManagement":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgObjectCareManagement.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabObjectCareDating":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgObjectCareDating.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabCustomerDocument":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgCustomerDocument.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
               
                //Công nợ
                case "tabCustomerCreditLimit":
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgCustomerCreditLimit.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;


                //Hàng hóa 
                case "tabProductPurchaseOrder": //Mua hàng
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgProductPurchase.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabProductPurchase": //Mua hàng
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgProductPurchase.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabProductReturn": //Nhận hàng trả
                    // add controls to tab item, this case I added just a textbox                    
                    f.Source = new Uri("pgProductPurchase.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabProductFromProducing": //Y/C nhập từ sản xuất
                    // add controls to tab item, this case I added just a textbox                   
                    f.Source = new Uri("pgProductPurchase.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;    

                case "tabProductSale": //Bán hàng
                    // add controls to tab item, this case I added just a textbox                   
                    f.Source = new Uri("pgProductSale.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabProductGiven": //Ký gửi hàng
                    // add controls to tab item, this case I added just a textbox                   
                    f.Source = new Uri("pgProductSale.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabProduct2Producing": //Y/C xuất SX
                    // add controls to tab item, this case I added just a textbox                   
                    f.Source = new Uri("pgProductSale.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                          
                //Quản lý kho
                case "tabInventoryInput": //Nhập hàng
                    // add controls to tab item, this case I added just a textbox                   
                    f.Source = new Uri("pgInventoryInput.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabInventoryOutput": //Nhập hàng
                    // add controls to tab item, this case I added just a textbox                   
                    f.Source = new Uri("pgInventoryOutput.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabInventoryExchange": //Nhập hàng
                    // add controls to tab item, this case I added just a textbox                   
                    f.Source = new Uri("pgInventoryExchange.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabInventoryAvailable": //Tồn kho
                    // add controls to tab item, this case I added just a textbox                   
                    f.Source = new Uri("pgInventoryAvailable.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabInventoryCheck": //Kiểm kê
                    // add controls to tab item, this case I added just a textbox                   
                    f.Source = new Uri("pgInventoryCheck.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;

                //Quản lý chứng từ hóa đơn
                case "tabProductPurchaseManagement": //Quản lý đơn mua hàng
                    // add controls to tab item, this case I added just a textbox                   
                    f.Source = new Uri("pgInventoryInput.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                case "tabProductSaleManagement": //Quản lý đơn bán hàng
                    // add controls to tab item, this case I added just a textbox                   
                    f.Source = new Uri("pgProductSaleManagement.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;

                //Báo cáo 
                case "tabIORReport": //NXT
                    // add controls to tab item, this case I added just a textbox                   
                    f.Source = new Uri("Report\\pgIORReport.xaml", UriKind.Relative);
                    tab.Content = f;
                    break;
                //case "tabProductSaleReport": //Báo cáo 1
                //    // add controls to tab item, this case I added just a textbox                   
                //    f.Source = new Uri("pgProductSaleReport.xaml", UriKind.Relative);
                //    tab.Content = f;
                //    break;               
                //case "tabProductPurchaseReport": //Báo cáo 2
                //    // add controls to tab item, this case I added just a textbox                   
                //    f.Source = new Uri("pgProductPurchaseReport.xaml", UriKind.Relative);
                //    tab.Content = f;
                //    break;
                default:
                    break;           
            } 
            tabDynamic.Items.Add(tab);

            for (int i = 0; i < tabDynamic.Items.Count; i++)
            {
                ClosableTab ti = tabDynamic.Items[i] as ClosableTab;

                if (ti.Name == tabname)
                {
                    return i;
                }
            }

            return -1;           
        }       

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tabName = (sender as Button).CommandParameter.ToString();

                var item = tabDynamic.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();

                TabItem tab = item as TabItem;

                if (tab != null)
                {
                    tabDynamic.Items.Remove(tab);  
                    if (tabDynamic.Items.Count < 1)
                    {
                        mnuMainMenu.IsMinimized = false;
                        tabDynamic.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch
            {
                ;
            }
        }

        private void btnDelete_MouseLeave(object sender, MouseEventArgs e)
        {
            //var btn = sender as Button;
            //btn.Content = "";
        }

        private void btnDelete_MouseEnter(object sender, MouseEventArgs e)
        {
            //var btn = sender as Button;
            //btn.Content = ""
        }

        private void btnServiceGroup_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.Visibility = Visibility.Visible;
            tabDynamic.SelectedIndex = this.GetTabItem("Nhóm dịch vụ", "tabServiceGroup");            
        }

        private void btnService_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;
          
            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Dịch vụ", "tabService");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;          

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Hàng hóa ", "tabProduct");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnServiceDetail_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Công đoạn dịch vụ", "tabServiceDetail");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnSQLServer_Click(object sender, RoutedEventArgs e)
        {
            frmSQLServer frmSQLServer_ = new frmSQLServer();
            frmSQLServer_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmSQLServer_.ShowDialog();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            frmLogin frmLogin_ = new frmLogin(this);
            frmLogin_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmLogin_.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            RefreshGUI(0);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fileName = "";

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "SQL files (*.bak)|*.bak|All files|*.*";
                var result = saveFileDialog1.ShowDialog();
                if (result == true) // Test result.
                {
                    fileName = saveFileDialog1.FileName;
                    SqlDataConnection cn = new SqlDataConnection();
                    cn.CreateDbBackup(fileName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string fileName = "";
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "SQL files (*.bak)|*.bak|All files|*.*";
                var result = openFileDialog1.ShowDialog(); // Show the dialog.
                if (result ==  true) // Test result.
                {
                    fileName = openFileDialog1.FileName;
                    SqlDataConnection cn = new SqlDataConnection();
                    cn.RestoreDbFromBackup(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
           
            // select newly added tab item
            tabDynamic.DataContext = null;
            tabDynamic.Visibility = Visibility.Visible;
            tabDynamic.SelectedIndex = this.GetTabItem("Tiếp nhận", "tabObjectCareOrder"); ;
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            // select newly added tab item
            tabDynamic.DataContext = null;
            tabDynamic.Visibility = Visibility.Visible;
            tabDynamic.SelectedIndex = this.GetTabItem("Khách hàng", "tabCustomer");
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Nhân viên", "tabStaff");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnVendor_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;     

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Nhà cung cấp", "tabVendor");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnObjectCareSale_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Sửa chữa", "tabObjectCareSale");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnGoods_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Vật tư", "tabEquipment");
            tabDynamic.Visibility = Visibility.Visible;
        }        

        private void btnPayment_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Thanh toán", "tabObjectCarePayment");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnSetDate_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Lịch hẹn", "tabObjectCareDating");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnProductPurchase_Click(object sender, RoutedEventArgs e)
        {
            General.GeneralParams.inputEdit = false;

            IMS.General.GeneralParams.purchaseKind = IMS.General.GeneralParams.ProductPurchase;

            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Mua hàng", "tabProductPurchase");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnProductSale_Click(object sender, RoutedEventArgs e)
        {
            IMS.General.GeneralParams.saleKind = IMS.General.GeneralParams.ProductSale;
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Bán hàng", "tabProductSale");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnGoodsIn_Click(object sender, RoutedEventArgs e)
        {
            General.GeneralParams.inputEdit = false;

            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Nhập kho", "tabInventoryInput");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnInventoryAvailable_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Tồn kho", "tabInventoryAvailable");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnManufacture_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Nhà sản xuất", "tabManufacture");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            frmUser frmUser_ = new frmUser();
            frmUser_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmUser_.ShowDialog();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void btnObjectCareManagement_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Bảng công việc", "tabObjectCareManagement");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btncustomerDocument_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Hồ sơ khách hàng", "tabCustomerDocument");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnStorage_Click(object sender, RoutedEventArgs e)
        {
            frmInventoryStorage frmInventoryStorage_ = new frmInventoryStorage();
            frmInventoryStorage_.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            frmInventoryStorage_.Show();
        }

        private void btnManageSaleInvoice_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Quản lý đơn bán hàng", "tabProductSaleManagement");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnManagePurchaseInvoice_Click(object sender, RoutedEventArgs e)
        {
            General.GeneralParams.inputEdit = true;

            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Quản lý đơn mua hàng", "tabProductPurchaseManagement");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnProductSaleReport_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Báo cáo doanh thu", "tabProductSaleReport");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnProductPurchaseReport_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Báo cáo mua hàng", "tabProductPurchaseReport");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnRefreshDB_Click(object sender, RoutedEventArgs e)
        {
            IMSDataContext dc = new IMSDataContext();
            dc.ProcRefreshDB();
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            frmAbout a = new frmAbout();
            a.ShowDialog();
        }

        private void btnInventoryCheck_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Kiểm kê", "tabInventoryCheck");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnDelete_Unloaded(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void tabDynamic_Unloaded(object sender, RoutedEventArgs e)
        {
            string a = "a";
        }

        private void btnProductReturn_Click(object sender, RoutedEventArgs e)
        {
            IMS.General.GeneralParams.purchaseKind = IMS.General.GeneralParams.ProductReturn;
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Nhận hàng trả lại", "tabProductPurchase");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnPrintLabel_Click(object sender, RoutedEventArgs e)
        {
            //frmAddQuantityOfLabel abc = new frmAddQuantityOfLabel();
            //abc.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //abc.Show();
        }

        private void btnProductReserve_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnProductGiven_Click(object sender, RoutedEventArgs e)
        {
            IMS.General.GeneralParams.saleKind = IMS.General.GeneralParams.ProductGiven;
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Ký gửi hàng", "tabProductGiven");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnInventoryOutput_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Xuất kho", "tabInventoryOutput");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnCreateBOM_Click(object sender, RoutedEventArgs e)
        {
            //IMS.General.GeneralParams.saleKind = IMS.General.GeneralParams.ProductOutToProducing;
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Y/C xuất SX", "tabProduct2Producing");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnExtractBOM_Click(object sender, RoutedEventArgs e)
        {
            //IMS.General.GeneralParams.purchaseKind = IMS.General.GeneralParams.ProductInFromProducing;

            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Y/C nhập từ SX", "tabProductFromProducing");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnProductCustomerCreditLimit_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Tổng hợp công nợ khách hàng", "tabCustomerCreditLimit");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnCustomerCredit_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Error when loading table CustomerCredit", "IMS - Thông báo lỗi!");

            frmCustomerCredit abc = new frmCustomerCredit();
            abc.Show();
        }

        private void btnInventoryExchange_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Chuyển kho", "tabInventoryExchange");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnViewReport_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Error when loading reportviewer.dll", "IMS - Thông báo lỗi!");
        }

        private void btnInventoryInputReport_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnSaleReport_Click(object sender, RoutedEventArgs e)
        {
            frmSaleReportCall frmSaleReportCall_ = new frmSaleReportCall();
            frmSaleReportCall_.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            frmSaleReportCall_.Show();
        }    

        private void btnPurchaseReport_Click(object sender, RoutedEventArgs e)
        {
            frmPurchaseReportCall frmPurchaseReportCall_ = new frmPurchaseReportCall();
            frmPurchaseReportCall_.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            frmPurchaseReportCall_.Show();
        }

        private void btnInventoryReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInvIOR_Click(object sender, RoutedEventArgs e)
        {
            frmIORReportCall frmIORReportCall_ = new frmIORReportCall();
            frmIORReportCall_.Show();
        }

        private void btnInvIORReport_Click(object sender, RoutedEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Báo cáo nhập xuất tồn", "tabIORReport");
            tabDynamic.Visibility = Visibility.Visible;
        }

        private void btnInvAvailebleReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBusinessReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnProductPurchaseOrder_Click(object sender, RoutedEventArgs e)
        {
            General.GeneralParams.inputEdit = false;

            IMS.General.GeneralParams.purchaseKind = IMS.General.GeneralParams.ProductPurchaseOrder;

            // clear tab control binding
            tabDynamic.DataContext = null;

            // select newly added tab item
            tabDynamic.SelectedIndex = this.GetTabItem("Đơn đặt mua hàng", "tabProductPurchaseOrder");
            tabDynamic.Visibility = Visibility.Visible;
        }
    }
}
