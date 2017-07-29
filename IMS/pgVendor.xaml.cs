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

namespace IMS
{
    /// <summary>
    /// Interaction logic for pgVendor.xaml
    /// </summary>
    public partial class pgVendor : Page
    {
        int idVendor;
        pgVendor pgCus;
        public pgVendor()
        {
            InitializeComponent();

            try
            {
                idVendor = -1;
                IMSDataContext dc = new IMSDataContext();
                List<Vendor> lsVendor = (from product in dc.Vendors
                                         select product).ToList();

                var list = lsVendor.AsEnumerable().Select((Vendor, index) => new Vendor()
                {
                    RowNumber = index + 1,
                    Id = Vendor.Id,
                    Name = Vendor.Name,
                    Address = Vendor.Address,
                    Email = Vendor.Email,
                    Phone = Vendor.Phone
                }).ToList();

                lsViewVendor.ItemsSource = null;
                lsViewVendor.ItemsSource = list;
                lsViewVendor.SelectedItem = list;

            }
            catch
            {
                ;
            }
        }

        private void tbrAddVendor_Click(object sender, RoutedEventArgs e)
        {
            frmVendor frmVendor_ = new frmVendor(-1, this);
            frmVendor_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmVendor_.ShowDialog();
        }

        private void tbrEditVendor_Click(object sender, RoutedEventArgs e)
        {
            frmVendor frmVendor_ = new frmVendor(idVendor, this);
            frmVendor_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmVendor_.ShowDialog();
        }

        private void tbrSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbrRefresh_Click(object sender, RoutedEventArgs e)
        {

        }
      

        private void btnRemoveVendor_Click(object sender, RoutedEventArgs e)
        {
            var results = MessageBox.Show("Bạn có thực sự muốn xóa dòng dữ liệu?", "IMS - Thông báo", MessageBoxButton.OKCancel);

            if (results != MessageBoxResult.OK)
                return;

            Vendor vendorParam = (sender as Button).DataContext as Vendor;
                        
            IMSDataContext dc = new IMSDataContext();
            int result = dc.ProcDeleteTable("Vendor", vendorParam.Id);

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
                List<Vendor> lsVendor = (from vendor in dc.Vendors
                                         select vendor).ToList();

                var list = lsVendor.AsEnumerable().Select((Vendor, index) => new Vendor()
                {
                    RowNumber = index + 1,
                    Id = Vendor.Id,
                    Name = Vendor.Name,
                    Address = Vendor.Address,
                    Email = Vendor.Email,
                    Phone = Vendor.Phone
                }).ToList();

                lsViewVendor.ItemsSource = null;
                lsViewVendor.ItemsSource = list;                
            }
        }

        public void Page_Refresh(Vendor cus)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                List<Vendor> lsVendor = (from vendor in dc.Vendors
                                                 select vendor).ToList();

                var list = lsVendor.AsEnumerable().Select((Vendor, index) => new Vendor()
                {
                    RowNumber = index + 1,
                    Id = Vendor.Id,
                    Name = Vendor.Name,                   
                    Address = Vendor.Address,                   
                    Email = Vendor.Email,
                    Phone = Vendor.Phone
                }).ToList();

                lsViewVendor.ItemsSource = null;
                lsViewVendor.ItemsSource = list;
                lsViewVendor.SelectedItem = list;

            }
            catch
            {
                ;
            }
        }
 
        private void lsViewVendor_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var Vendor = e.AddedItems[0] as Vendor;
                idVendor = Vendor.Id;
            }
            catch
            {
                ;
            }
        }

        private void lsViewVendor_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            frmVendor frmVendor_ = new frmVendor(idVendor, this);
            frmVendor_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmVendor_.ShowDialog();
        }
    }
}
