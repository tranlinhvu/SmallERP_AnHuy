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
    /// Interaction logic for pgStaff.xaml
    /// </summary>
    public partial class pgStaff : Page
    {
        int idStaff;
        pgStaff pgCus;
        public pgStaff()
        {
            InitializeComponent();
            
            try
            {
                idStaff = - 1;
                IMSDataContext dc = new IMSDataContext();
                List<StaffView> ls = (from s in dc.StaffViews
                                         select s).ToList();

                var list = ls.AsEnumerable().Select((StaffView, index) => new StaffView()
                {
                    RowNumber = index + 1,
                    Id = StaffView.Id,                   
                    Name = StaffView.Name,
                    DateOfBirth = StaffView.DateOfBirth,
                    Province = StaffView.Province,
                    District = StaffView.District,
                    Ward = StaffView.Ward,
                    Address = StaffView.Address,
                    Ocupation = StaffView.Ocupation,
                    Email = StaffView.Email,
                    Phone = StaffView.Phone,
                    StaffGroupName = StaffView.StaffGroupName              

                }).ToList();

                lsViewStaff.ItemsSource = null;
                lsViewStaff.ItemsSource = list;

            }
            catch
            {
                ;
            }
        }

        private void tbrAddStaff_Click(object sender, RoutedEventArgs e)
        {
            frmStaff frmStaff_ = new frmStaff(-1, this);
            frmStaff_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmStaff_.ShowDialog();
        }

        private void tbrEditStaff_Click(object sender, RoutedEventArgs e)
        {
            frmStaff frmStaff_ = new frmStaff(idStaff, this);
            frmStaff_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmStaff_.ShowDialog();
        }

        private void tbrSearch_Click(object sender, RoutedEventArgs e)
        {
            IMSDataContext dc = new IMSDataContext();
            List<StaffView> ls = (from s in dc.StaffViews
                                  where s.Name.Contains(txtStaffName.Text)
                                  select s).ToList();

            var list = ls.AsEnumerable().Select((StaffView, index) => new StaffView()
            {
                RowNumber = index + 1,
                Id = StaffView.Id,
                Name = StaffView.Name,
                DateOfBirth = StaffView.DateOfBirth,
                Province = StaffView.Province,
                District = StaffView.District,
                Ward = StaffView.Ward,
                Address = StaffView.Address,
                Ocupation = StaffView.Ocupation,
                Email = StaffView.Email,
                Phone = StaffView.Phone,
                StaffGroupName = StaffView.StaffGroupName

            }).ToList();

            lsViewStaff.ItemsSource = null;
            lsViewStaff.ItemsSource = list;
        }

        private void tbrRefresh_Click(object sender, RoutedEventArgs e)
        {
            IMSDataContext dc = new IMSDataContext();
            List<StaffView> ls = (from s in dc.StaffViews
                                  where s.Name.Contains(txtStaffName.Text)
                                  select s).ToList();

            var list = ls.AsEnumerable().Select((StaffView, index) => new StaffView()
            {
                RowNumber = index + 1,
                Id = StaffView.Id,
                Name = StaffView.Name,
                DateOfBirth = StaffView.DateOfBirth,
                Province = StaffView.Province,
                District = StaffView.District,
                Ward = StaffView.Ward,
                Address = StaffView.Address,
                Ocupation = StaffView.Ocupation,
                Email = StaffView.Email,
                Phone = StaffView.Phone,
                StaffGroupName = StaffView.StaffGroupName

            }).ToList();

            lsViewStaff.ItemsSource = null;
            lsViewStaff.ItemsSource = list;
        }

        private void lsViewStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var StaffView = e.AddedItems[0] as StaffView;
                idStaff = StaffView.Id;               
            }
            catch
            {
                ;
            }
        }

        private void btnRemoveStaff_Click(object sender, RoutedEventArgs e)
        {
            var results = MessageBox.Show("Bạn có thực sự muốn xóa dòng dữ liệu?", "IMS - Thông báo", MessageBoxButton.OKCancel);

            if (results != MessageBoxResult.OK)
                return;

            StaffView staff = (sender as Button).DataContext as StaffView;
            //Remove all data matching Id in textbox
            IMSDataContext dc = new IMSDataContext();
            int result = dc.ProcDeleteTable("Staff", staff.Id);

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
                List<StaffView> ls = (from s in dc.StaffViews
                                      select s).ToList();

                var list = ls.AsEnumerable().Select((StaffView, index) => new StaffView()
                {
                    RowNumber = index + 1,
                    Id = StaffView.Id,
                    Name = StaffView.Name,
                    DateOfBirth = StaffView.DateOfBirth,
                    Province = StaffView.Province,
                    District = StaffView.District,
                    Ward = StaffView.Ward,
                    Address = StaffView.Address,
                    Ocupation = StaffView.Ocupation,
                    Email = StaffView.Email,
                    Phone = StaffView.Phone,
                    StaffGroupName = StaffView.StaffGroupName

                }).ToList();

                lsViewStaff.ItemsSource = null;
                lsViewStaff.ItemsSource = list;
            }
        }

        public void Page_Refresh(Staff cus)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                List<StaffView> lsStaff = (from s in dc.StaffViews
                                                select s).ToList();

                var list = lsStaff.AsEnumerable().Select((StaffView, index) => new StaffView()
                {

                    RowNumber = index + 1,
                    Id = StaffView.Id,
                    Name = StaffView.Name,
                    DateOfBirth = StaffView.DateOfBirth,
                    Province = StaffView.Province,
                    District = StaffView.District,
                    Ward = StaffView.Ward,
                    Address = StaffView.Address,
                    Ocupation = StaffView.Ocupation,
                    Email = StaffView.Email,
                    Phone = StaffView.Phone,
                    StaffGroupName = StaffView.StaffGroupName
                }).ToList();

                lsViewStaff.ItemsSource = null;
                lsViewStaff.ItemsSource = list;
                lsViewStaff.SelectedItem = cus;

            }
            catch
            {
                ;
            }
        }

        private void lsViewStaff_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            frmStaff frmStaff_ = new frmStaff(idStaff, this);
            frmStaff_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmStaff_.ShowDialog();
        }
    }
}
