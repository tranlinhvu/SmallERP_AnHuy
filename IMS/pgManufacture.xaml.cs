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
    /// Interaction logic for pgManufacture.xaml
    /// </summary>
    public partial class pgManufacture : Page
    {
        int idManufacture;
        pgManufacture pgCus;
        public pgManufacture()
        {
            InitializeComponent();

            try
            {
                idManufacture = -1;
                IMSDataContext dc = new IMSDataContext();
                List<Manufacture> lsManufacture = (from product in dc.Manufactures
                                         select product).ToList();

                var list = lsManufacture.AsEnumerable().Select((Manufacture, index) => new Manufacture()
                {
                    RowNumber = index + 1,
                    Id = Manufacture.Id,
                    Name = Manufacture.Name,
                    Code = Manufacture.Code,
                    Address = Manufacture.Address,
                    Email = Manufacture.Email,
                    Phone = Manufacture.Phone
                }).ToList();

                lsViewManufacture.ItemsSource = null;
                lsViewManufacture.ItemsSource = list;
            }
            catch
            {
                ;
            }
        }

        private void tbrAddManufacture_Click(object sender, RoutedEventArgs e)
        {
            frmManufacture frmManufacture_ = new frmManufacture(-1, this);
            frmManufacture_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmManufacture_.ShowDialog();
        }

        private void tbrEditManufacture_Click(object sender, RoutedEventArgs e)
        {
            frmManufacture frmManufacture_ = new frmManufacture(idManufacture, this);
            frmManufacture_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmManufacture_.ShowDialog();
        }

        private void tbrSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbrRefresh_Click(object sender, RoutedEventArgs e)
        {

        }
      

        private void btnRemoveManufacture_Click(object sender, RoutedEventArgs e)
        {
            var results = MessageBox.Show("Bạn có thực sự muốn xóa dòng dữ liệu?", "IMS - Thông báo", MessageBoxButton.OKCancel);

            if (results != MessageBoxResult.OK)
                return;

            Manufacture manufacture = (sender as Button).DataContext as Manufacture;

            IMSDataContext dc = new IMSDataContext();
            int result = dc.ProcDeleteTable("Manufacture", manufacture.Id);

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
                List<Manufacture> lsManufacture = (from product in dc.Manufactures
                                                   select product).ToList();

                var list = lsManufacture.AsEnumerable().Select((Manufacture, index) => new Manufacture()
                {
                    RowNumber = index + 1,
                    Id = Manufacture.Id,
                    Name = Manufacture.Name,
                    Code = Manufacture.Code,
                    Address = Manufacture.Address,
                    Email = Manufacture.Email,
                    Phone = Manufacture.Phone
                }).ToList();

                lsViewManufacture.ItemsSource = null;
                lsViewManufacture.ItemsSource = list;
            }
        }

        public void Page_Refresh(Manufacture cus)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                List<Manufacture> lsManufacture = (from vendor in dc.Manufactures
                                                 select vendor).ToList();

                var list = lsManufacture.AsEnumerable().Select((Manufacture, index) => new Manufacture()
                {
                    RowNumber = index + 1,
                    Id = Manufacture.Id,
                    Name = Manufacture.Name,
                    Code = Manufacture.Code,
                    Address = Manufacture.Address,
                    Email = Manufacture.Email,
                    Phone = Manufacture.Phone
                }).ToList();

                lsViewManufacture.ItemsSource = null;
                lsViewManufacture.ItemsSource = list;

            }
            catch
            {
                ;
            }
        }
 
        private void lsViewManufacture_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var Manufacture = e.AddedItems[0] as Manufacture;
                idManufacture = Manufacture.Id;
            }
            catch
            {
                ;
            }
        }

        private void lsViewManufacture_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            frmManufacture frmManufacture_ = new frmManufacture(idManufacture, this);
            frmManufacture_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmManufacture_.ShowDialog();
        }
    }
}
