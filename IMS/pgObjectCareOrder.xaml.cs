using IMS.Model;
using IMS.View;
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

namespace IMS
{
    /// <summary>
    /// Interaction logic for pgServiceSaleOrder.xaml
    /// </summary>
    public partial class pgObjectCareSaleOrder : Page
    {
        int idCustomer = -1;
        int idObjectCare = -1;
        int idObjectCareDetail = -1;
        pgObjectCareSaleOrder pgCus;
        public pgObjectCareSaleOrder()
        {
            InitializeComponent();


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

            //Hiển thị danh sách sửa chữa          
            List<ObjectCare> lsObjectCare = (from s in dc.ObjectCares
                                             where s.IdCustomer == idCustomer
                                             select s).ToList();

            var list1 = lsObjectCare.AsEnumerable().Select((ObjectCare, index) => new ObjectCare()
            {
                RowNumber = index + 1,
                Id = ObjectCare.Id,
                Code = ObjectCare.Code,
                Name = ObjectCare.Name
            }).ToList();

            lsViewObjectCare.ItemsSource = null;
            lsViewObjectCare.ItemsSource = list1;

            //Hiển thị danh sách đợt sửa chữa         
            List<ObjectCareDetailView> lsObjectCareDetail = (from s in dc.ObjectCareDetailViews
                                                       where s.IdObjectCare == idObjectCare
                                                             select s).ToList();

            var list2 = lsObjectCareDetail.AsEnumerable().Select((ObjectCareDetailView, index) => new ObjectCareDetailView()
            {
                RowNumber = index + 1,
                Id = ObjectCareDetailView.Id,
                Code = ObjectCareDetailView.Code,
                Name = ObjectCareDetailView.Name,
                CarePersonName = ObjectCareDetailView.CarePersonName,
                AssistanceName = ObjectCareDetailView.AssistanceName,
                IsDone = ObjectCareDetailView.IsDone
            }).ToList();

            lsViewObjectCareDetail.ItemsSource = null;
            lsViewObjectCareDetail.ItemsSource = list2;
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

        }

        private void tbrRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lsViewCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var customerView = e.AddedItems[0] as CustomerView;
                idCustomer = customerView.Id;
                idObjectCare = -1;

                IMSDataContext dc = new IMSDataContext();
                List<ObjectCare> lsObjectCare = (from s in dc.ObjectCares
                                                 where s.IdCustomer == idCustomer
                                                 select s).ToList();

                var list = lsObjectCare.AsEnumerable().Select((ObjectCare, index) => new ObjectCare()
                {
                    RowNumber = index + 1,
                    Id = ObjectCare.Id,
                    Code = ObjectCare.Code,
                    Name = ObjectCare.Name
                }).ToList();

                lsViewObjectCare.ItemsSource = null;
                lsViewObjectCare.ItemsSource = list;
                lsViewObjectCare.SelectedIndex = 0;

            }
            catch
            {
                ;
            }
        }

        public void Page_Refresh(Customer cus)
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

        public void Page_Refresh(ObjectCare cus)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                List<ObjectCare> lsObjectCare = (from s in dc.ObjectCares
                                                 where s.IdCustomer == idCustomer
                                                 select s).ToList();

                var list = lsObjectCare.AsEnumerable().Select((ObjectCare, index) => new ObjectCare()
                {
                    RowNumber = index + 1,
                    Id = ObjectCare.Id,
                    Code = ObjectCare.Code,
                    Name = ObjectCare.Name
                }).ToList();

                lsViewObjectCare.ItemsSource = null;
                lsViewObjectCare.ItemsSource = list;
                lsViewObjectCare.SelectedItem = cus;
            }
            catch
            {
                ;
            }
        }

        public void Page_Refresh(ObjectCareDetail cus)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                List<ObjectCareDetailView> lsObjectCareDetail = (from s in dc.ObjectCareDetailViews
                                                                 where s.IdObjectCare == idObjectCare
                                                                 select s).ToList();

                var list1 = lsObjectCareDetail.AsEnumerable().Select((ObjectCareDetailView, index) => new ObjectCareDetailView()
                {
                    RowNumber = index + 1,
                    Id = ObjectCareDetailView.Id,
                    Code = ObjectCareDetailView.Code,
                    Name = ObjectCareDetailView.Name,
                    CarePersonName = ObjectCareDetailView.CarePersonName,
                    AssistanceName = ObjectCareDetailView.AssistanceName,
                    IsDone = ObjectCareDetailView.IsDone
                }).ToList();

                lsViewObjectCareDetail.ItemsSource = null;
                lsViewObjectCareDetail.ItemsSource = list1;
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
       

        private void tbrAddObjectCare_Click(object sender, RoutedEventArgs e)
        {
            if (idCustomer < 0)
                return;
            frmObjectCare frmObjectCare_ = new frmObjectCare(idCustomer, - 1, this);
            frmObjectCare_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmObjectCare_.ShowDialog();
        }

        private void tbrEditObjectCare_Click(object sender, RoutedEventArgs e)
        {
            if (idObjectCare == -1)
            {
                return;
            }
            frmObjectCare frmObjectCare_ = new frmObjectCare(idCustomer, idObjectCare, this);
            frmObjectCare_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmObjectCare_.ShowDialog();
        }       

        private void lsViewObjectCare_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var objectCare = e.AddedItems[0] as ObjectCare;
                idObjectCare = objectCare.Id;
                idObjectCareDetail = -1;

                IMSDataContext dc = new IMSDataContext();
                List<ObjectCareDetailView> lsObjectCareDetail = (from s in dc.ObjectCareDetailViews
                                                                 where s.IdObjectCare == idObjectCare
                                                                 select s).ToList();

                var list1 = lsObjectCareDetail.AsEnumerable().Select((ObjectCareDetailView, index) => new ObjectCareDetailView()
                {
                    RowNumber = index + 1,
                    Id = ObjectCareDetailView.Id,
                    Code = ObjectCareDetailView.Code,
                    Name = ObjectCareDetailView.Name,
                    CarePersonName = ObjectCareDetailView.CarePersonName,
                    AssistanceName = ObjectCareDetailView.AssistanceName,
                    IsDone = ObjectCareDetailView.IsDone
                }).ToList();

                lsViewObjectCareDetail.ItemsSource = null;
                lsViewObjectCareDetail.ItemsSource = list1;
                

            }
            catch
            {
                ;
            }
        }

        private void lsViewObjectCare_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (idObjectCare == -1)
            {
                return;
            }
            frmObjectCare frmObjectCare_ = new frmObjectCare(idCustomer, idObjectCare, this);
            frmObjectCare_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmObjectCare_.ShowDialog();
        }

        private void tbrAddObjectCareDetail_Click(object sender, RoutedEventArgs e)
        {
            if(idObjectCare == -1)
            {
                MessageBox.Show("Hãy chọ sửa chữa trước khi thêm hoặc sửa chữa đợt sửa chữa", "IMS - Thông báo lỗi");
                return;
            }
            frmObjectCareDetail frmObjectCareDetail_ = new frmObjectCareDetail(idObjectCare, -1, this, false);
            frmObjectCareDetail_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmObjectCareDetail_.ShowDialog();
        }

        private void tbrEditObjectCareDetail_Click(object sender, RoutedEventArgs e)
        {
            if(idObjectCareDetail == -1)
            {
                return;
            }           
            frmObjectCareDetail frmObjectCareDetail_ = new frmObjectCareDetail(idObjectCare, idObjectCareDetail, this, false);
            frmObjectCareDetail_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmObjectCareDetail_.ShowDialog();
        } 

        private void lsViewObjectCareDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var objectCareDetail = e.AddedItems[0] as ObjectCareDetailView;
                idObjectCareDetail  = objectCareDetail.Id;
            }
            catch
            {
                ;
            }
           
        }

        private void lsViewObjectCareDetail_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (idObjectCareDetail == -1)
            {
                return;
            }
            frmObjectCareDetail frmObjectCareDetail_ = new frmObjectCareDetail(idObjectCare, idObjectCareDetail, this, false);
            frmObjectCareDetail_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmObjectCareDetail_.ShowDialog();
        }

        private void tbrSearchCustomer_Click(object sender, RoutedEventArgs e)
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

        private void tbrRefreshCustomer_Click(object sender, RoutedEventArgs e)
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

        
        private void tbrSearchObjectCare_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbrRefreshObjectCare_Click(object sender, RoutedEventArgs e)
        {

        }
        private void tbrSearchObjectCareDetail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbrRefreshObjectCareDetail_Click(object sender, RoutedEventArgs e)
        {

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
                //Refresh display
                //var lst = (from s in dc.CustomerViews select s);
                //lsViewCustomer.ItemsSource = null;
                //lsViewCustomer.ItemsSource = lst;
                //myListView.SelectedIndex = 0;
                Customer cus1 = new Customer();
                Page_Refresh(cus1);
            }
        }
        private void btnRemoveObjectCareDetail_Click(object sender, RoutedEventArgs e)
        {
            var results = MessageBox.Show("Bạn có thực sự muốn xóa dòng dữ liệu?", "IMS - Thông báo", MessageBoxButton.OKCancel);

            if (results != MessageBoxResult.OK)
                return;

            ObjectCareDetailView objCareDetail = (sender as Button).DataContext as ObjectCareDetailView;
            //Remove all data matching Id in textbox
            IMSDataContext dc = new IMSDataContext();
            int result = dc.ProcDeleteObjectCareDetail(objCareDetail.Id);

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
                //Refresh display
                //var lst = (from s in dc.ObjectCareDetailViews select s);
                //lsViewObjectCareDetail.ItemsSource = null;
                //lsViewObjectCareDetail.ItemsSource = lst;
                //myListView.SelectedIndex = 0;
                ObjectCareDetail cus1 = new ObjectCareDetail();
                Page_Refresh(cus1);
            }
        }
        private void btnRemoveObjectCare_Click(object sender, RoutedEventArgs e)
        {
            var results = MessageBox.Show("Bạn có thực sự muốn xóa dòng dữ liệu?", "IMS - Thông báo", MessageBoxButton.OKCancel);

            if (results != MessageBoxResult.OK)
                return;

            ObjectCare objCare = (sender as Button).DataContext as ObjectCare;
            //Remove all data matching Id in textbox
            IMSDataContext dc = new IMSDataContext();
            int result = dc.ProcDeleteObjectCare(objCare.Id);

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
                //Refresh display
                //var lst = (from s in dc.ObjectCares select s);
                //lsViewObjectCare.ItemsSource = null;
                //lsViewObjectCare.ItemsSource = lst;
                //myListView.SelectedIndex = 0;
                ObjectCare cus1 = new ObjectCare();
                Page_Refresh(cus1);
            }
        }

        private void tbrObjectCaring_Click(object sender, RoutedEventArgs e)
        {

            ;
        }

        private void txtCustomerName_TextChanged(object sender, TextChangedEventArgs e)
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
    }
}
