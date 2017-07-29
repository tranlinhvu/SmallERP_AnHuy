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
using System.Windows.Navigation;
using System.Windows.Shapes;
using IMS.View;
using IMS.DBHelper;
using System.Data;
using IMS.Favorite;


namespace IMS
{
    /// <summary>
    /// Interaction logic for pgCustomer.xaml
    /// </summary>
    public partial class pgCustomer : Page
    {
        int idCustomer;
        pgCustomer pgCus;
        public pgCustomer()
        {
            InitializeComponent();

            try
            {
                idCustomer = -1;
                IMSDataContext dc = new IMSDataContext();
                List<CustomerView> ls = (from s in dc.CustomerViews
                                         select s).ToList();

                var list = ls.AsEnumerable().Select((CustomerView, index) => new CustomerView()
                {
                    RowNumber = index + 1,
                    Id = CustomerView.Id,
                    Name = CustomerView.Name,
                    DateOfBirth = CustomerView.DateOfBirth,
                    Province = CustomerView.Province,
                    District = CustomerView.District,
                    Ward = CustomerView.Ward,
                    Address = CustomerView.Address,
                    Ocupation = CustomerView.Ocupation,
                    Email = CustomerView.Email,
                    Phone = CustomerView.Phone

                }).ToList();

                lsViewCustomer.ItemsSource = null;
                lsViewCustomer.ItemsSource = list;

            }
            catch
            {
                ;
            }
        }

        private void tbrAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            frmCustomer frmCustomer_ = new frmCustomer(-1, this);
            frmCustomer_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmCustomer_.ShowDialog();
        }

        private void tbrEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            frmCustomer frmCustomer_ = new frmCustomer(idCustomer, this);
            frmCustomer_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmCustomer_.ShowDialog();
        }

        private void tbrSearch_Click(object sender, RoutedEventArgs e)
        {
            //Hiển thị danh sách khách hàng
            IMSDataContext dc = new IMSDataContext();
            List<CustomerView> ls = (from s in dc.CustomerViews
                                     where s.Name.Contains(txtCustomerName.Text)
                                     select s).ToList();

            var list = ls.AsEnumerable().Select((CustomerView, index) => new CustomerView()
            {
                RowNumber = index + 1,
                Id = CustomerView.Id,
                Name = CustomerView.Name,
                DateOfBirth = CustomerView.DateOfBirth,
                Province = CustomerView.Province,
                District = CustomerView.District,
                Ward = CustomerView.Ward,
                Address = CustomerView.Address,
                Ocupation = CustomerView.Ocupation,
                Email = CustomerView.Email,
                Phone = CustomerView.Phone

            }).ToList();

            lsViewCustomer.ItemsSource = null;
            lsViewCustomer.ItemsSource = list;
        }

        private void tbrRefresh_Click(object sender, RoutedEventArgs e)
        {
            //Hiển thị danh sách khách hàng
            IMSDataContext dc = new IMSDataContext();
            List<CustomerView> ls = (from s in dc.CustomerViews
                                     select s).ToList();

            var list = ls.AsEnumerable().Select((CustomerView, index) => new CustomerView()
            {
                RowNumber = index + 1,
                Id = CustomerView.Id,
                Name = CustomerView.Name,
                DateOfBirth = CustomerView.DateOfBirth,
                Province = CustomerView.Province,
                District = CustomerView.District,
                Ward = CustomerView.Ward,
                Address = CustomerView.Address,
                Ocupation = CustomerView.Ocupation,
                Email = CustomerView.Email,
                Phone = CustomerView.Phone

            }).ToList();

            lsViewCustomer.ItemsSource = null;
            lsViewCustomer.ItemsSource = list;

            txtCustomerName.Text = "";
        }

        private void lsViewCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var customerView = e.AddedItems[0] as CustomerView;
                idCustomer = customerView.Id;
            }
            catch
            {
                ;
            }
        }

        private void btnRemoveCustomer_Click(object sender, RoutedEventArgs e)
        {
            var results = MessageBox.Show("Bạn có thực sự muốn xóa dòng dữ liệu?", "IMS - Thông báo", MessageBoxButton.OKCancel);

            if (results != MessageBoxResult.OK)
                return;

            CustomerView cus = (sender as Button).DataContext as CustomerView;
            //Remove all data matching Id in textbox
            IMSDataContext dc = new IMSDataContext();
            int result = dc.ProcDeleteCustomer(cus.Id);

            if (result < 0)
            {
                MessageBox.Show("Không thể xóa do lỗi dữ liệu!", "IMS - Thông báo lỗi");
            }
            else if (result == 0)
            {
                MessageBox.Show("Không thể xóa vì dòng dữ liệu có liên quan đối tượng khác", "IMS - Thông báo lỗi");
            }
            else
            {
                List<CustomerView> ls = (from s in dc.CustomerViews
                                         where s.Name.Contains(txtCustomerName.Text)
                                         select s).ToList();

                var list = ls.AsEnumerable().Select((CustomerView, index) => new CustomerView()
                {
                    RowNumber = index + 1,
                    Id = CustomerView.Id,
                    Name = CustomerView.Name,
                    DateOfBirth = CustomerView.DateOfBirth,
                    Province = CustomerView.Province,
                    District = CustomerView.District,
                    Ward = CustomerView.Ward,
                    Address = CustomerView.Address,
                    Ocupation = CustomerView.Ocupation,
                    Email = CustomerView.Email,
                    Phone = CustomerView.Phone
                }).ToList();

                lsViewCustomer.ItemsSource = null;
                lsViewCustomer.ItemsSource = list;
            }
        }

        public void PageRefresh(Customer cus)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                List<CustomerView> lsCustomer = (from product in dc.CustomerViews
                                                 select product).ToList();

                var list = lsCustomer.AsEnumerable().Select((CustomerView, index) => new CustomerView()
                {
                    RowNumber = index + 1,
                    Id = CustomerView.Id,
                    Name = CustomerView.Name,
                    DateOfBirth = CustomerView.DateOfBirth,
                    Province = CustomerView.Province,
                    District = CustomerView.District,
                    Ward = CustomerView.Ward,
                    Address = CustomerView.Address,
                    Ocupation = CustomerView.Ocupation,
                    Email = CustomerView.Email,
                    Phone = CustomerView.Phone
                }).ToList();

                lsViewCustomer.ItemsSource = null;
                lsViewCustomer.ItemsSource = list;
                lsViewCustomer.SelectedItem = cus;

            }
            catch
            {
                ;
            }
        }

        public void SearchCustomer(string cusName)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                List<CustomerView> lsCustomer = (from cus in dc.CustomerViews
                                                 where cus.Name.Contains(cusName)
                                                 select cus).ToList();

                var list = lsCustomer.AsEnumerable().Select((CustomerView, index) => new CustomerView()
                {
                    RowNumber = index + 1,
                    Id = CustomerView.Id,
                    Name = CustomerView.Name,
                    DateOfBirth = CustomerView.DateOfBirth,
                    Province = CustomerView.Province,
                    District = CustomerView.District,
                    Ward = CustomerView.Ward,
                    Address = CustomerView.Address,
                    Ocupation = CustomerView.Ocupation,
                    Email = CustomerView.Email,
                    Phone = CustomerView.Phone
                }).ToList();

                lsViewCustomer.ItemsSource = null;
                lsViewCustomer.ItemsSource = list;
            }
            catch
            {
                ;
            }
        }

        private void lsViewCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            frmCustomer frmCustomer_ = new frmCustomer(idCustomer, this);
            frmCustomer_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmCustomer_.ShowDialog();
        }

        private void tbrAddCustomerFromFile_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "Excel Files|*.xls;*.xlsx" };
            var result = ofd.ShowDialog();
            if (result == false) return;
            string file = ofd.FileName;
            MyProgressBar pb = new MyProgressBar();
            pb.Show();
            try
            {

                IMS_ProcDataContext dc = new IMS_ProcDataContext();
                DataTable customerData = SqlDataConnection.ReadExcelContents(file);

                for (int i = 0; i < customerData.Rows.Count; i++)
                {
                    DataRow row = customerData.Rows[i];

                    string name = row[1].ToString();
                    string dateOfBirth = row[0].ToString(); ;
                    int idProvince = 58;
                    int idDistrict = 650;
                    int idWard = 10402;
                    string address = row[2].ToString(); ;
                    string ocupation = "";
                    string email = "";
                    string phone = "";

                    dc.ProcInsertCustomer(name, dateOfBirth, idProvince, idDistrict, idWard, address, ocupation, email, phone);
                }
                PageRefresh(null);
            }
            catch (Exception ex)
            {
                ;
            }
        }
        private void txtCustomerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchCustomer(txtCustomerName.Text);
        }
    }
}
