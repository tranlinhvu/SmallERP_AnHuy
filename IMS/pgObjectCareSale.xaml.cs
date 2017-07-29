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
    /// Interaction logic for pgObjectCaring.xaml
    /// </summary>
    public partial class pgObjectCareSale : Page
    {
        double h1;
        double h2;

        public pgObjectCareSale()
        {
            InitializeComponent();

            grdAllInOne.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
            grdAllInOne.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            grdAllInOne.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

            int idObjectCareDetail = -1;
            //var abc = this.NavigationService.CurrentSource;
            {
                idObjectCareDetail = -1;
            }

            IMSDataContext dc = new IMSDataContext();
            //Hiển thị danh sách đợt sửa chữa         
            List<ObjectCareDetailView> lsObjectCareDetail;
            if (idObjectCareDetail == -1)
            {
                lsObjectCareDetail = (from s in dc.ObjectCareDetailViews                                                                 
                                                                 select s).ToList();
            }
            else
            {
                lsObjectCareDetail = (from s in dc.ObjectCareDetailViews
                                                             where s.Id == idObjectCareDetail
                                                             select s).ToList();
            }
           

            var list2 = lsObjectCareDetail.AsEnumerable().Select((ObjectCareDetailView, index) => new ObjectCareDetailView()
            {
                RowNumber = index + 1,
                Id = ObjectCareDetailView.Id,
                Code = ObjectCareDetailView.Code,
                Name = ObjectCareDetailView.Name,
                CarePersonName = ObjectCareDetailView.CarePersonName,
                AssistanceName = ObjectCareDetailView.AssistanceName,
                IsDone = ObjectCareDetailView.IsDone,
                CustomerName = ObjectCareDetailView.CustomerName,
                CareAmount = ObjectCareDetailView.CareAmount,

            }).ToList();

            lsViewObjectCareDetail.ItemsSource = null;
            lsViewObjectCareDetail.ItemsSource = list2;


        }

        private void tbrAddServiceSaleItem_Click(object sender, RoutedEventArgs e)
        {

            grdAllInOne.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
            grdAllInOne.RowDefinitions[1].Height = new GridLength(7, GridUnitType.Star);
            grdAllInOne.RowDefinitions[2].Height = new GridLength(3, GridUnitType.Star);

            grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);

            IMSDataContext dc = new IMSDataContext();
            List<Service> lsService_ = (from s in dc.Services                                        
                                        select s).ToList();

            var list = lsService_.AsEnumerable().Select((Service, index) => new Service()
            {

                RowNumber = index + 1,
                Id = Service.Id,
                Name = Service.Name,
                Price = Service.Price,
                Note = Service.Note
            }).ToList();

            lsViewService.ItemsSource = null;
            lsViewService.ItemsSource = list;           
        }

        private void tbrComplete_Click(object sender, RoutedEventArgs e)
        {
            grdAllInOne.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
            grdAllInOne.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            grdAllInOne.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
        }

        private void tbrAddProductSale_Click(object sender, RoutedEventArgs e)
        {
            grdAllInOne.RowDefinitions[0].Height = new GridLength(7, GridUnitType.Star);
            grdAllInOne.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            grdAllInOne.RowDefinitions[2].Height = new GridLength(3, GridUnitType.Star);

            grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
            grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

            IMSDataContext dc = new IMSDataContext();
            List<ProductView> lsProduct = (from product in dc.ProductViews
                                           select product).ToList();

            var list = lsProduct.AsEnumerable().Select((ProductView, index) => new ProductView()
            {
                RowNumber = index + 1,
                Id = ProductView.Id,
                Code = ProductView.Code,
                Name = ProductView.Name,                
                PriceIn = ProductView.PriceIn,
                PriceOut = ProductView.PriceOut,
                UnitName = ProductView.UnitName,
                GroupName = ProductView.GroupName,
                ManufactureName = ProductView.ManufactureName

            }).ToList();

            lsviewProduct.ItemsSource = null;
            lsviewProduct.ItemsSource = list;
        }

        int lCurSeviceId = -1;
        int lCurProductId = -1;
        int lCurIdObjectCareDetail = -1;
        int lCurIdObjectCare = -1;
        private void lsViewService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var service = e.AddedItems[0] as Service;
                lCurSeviceId = service.Id;
            }
            catch
            {
                ;
            }
        }

        private void btnAddService2Sale_Click(object sender, RoutedEventArgs e)
        {
            Service service = (sender as Button).DataContext as Service;
            lCurSeviceId = service.Id;

            frmAddSale frmAddSale_ = new frmAddSale("Service", this, lCurIdObjectCareDetail, lCurSeviceId, decimal.Parse(service.Price.ToString()));
            frmAddSale_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmAddSale_.ShowDialog();
            //IMSDataContext dc = new IMSDataContext();
            //ServiceSaleItem serviceSaleItem = new ServiceSaleItem();
            //serviceSaleItem.IdService = lCurSeviceId;
            //serviceSaleItem.Price = 1;
            //serviceSaleItem.Quantity = 1;
            //serviceSaleItem.Amount = 1;
            //serviceSaleItem.IdObjectCareDetail = lCurIdObjectCareDetail;

            //dc.ServiceSaleItems.InsertOnSubmit(serviceSaleItem);
            //dc.SubmitChanges();

            ////Hiển thị ServiceSaleItem                    
            //List<ServiceSaleItemView> lsServiceSaleItemView;

            //lsServiceSaleItemView = (from s in dc.ServiceSaleItemViews
            //                         where s.IdObjectCareDetail == lCurIdObjectCareDetail
            //                         select s).ToList();



            //var list2 = lsServiceSaleItemView.AsEnumerable().Select((ServiceSaleItemView, index) => new ServiceSaleItemView()
            //{
            //    RowNumber = index + 1,
            //    Id = ServiceSaleItemView.Id,
            //    Name = ServiceSaleItemView.Name,
            //    Quantity = ServiceSaleItemView.Quantity,

            //}).ToList();

            //lsViewServiceSaleItem.ItemsSource = null;
            //lsViewServiceSaleItem.ItemsSource = list2;
        }

        private void lsViewObjectCareDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var objCareDetail = e.AddedItems[0] as ObjectCareDetailView;
                lCurIdObjectCareDetail = objCareDetail.Id;
                lCurIdObjectCare = objCareDetail.IdObjectCare;

                IMSDataContext dc = new IMSDataContext();
                //Hiển thị ServiceSaleItem          
                List<ServiceSaleItemView> lsServiceSaleItemView;

                lsServiceSaleItemView = (from s in dc.ServiceSaleItemViews
                                         where s.IdObjectCareDetail == lCurIdObjectCareDetail
                                         select s).ToList();



                var list2 = lsServiceSaleItemView.AsEnumerable().Select((ServiceSaleItemView, index) => new ServiceSaleItemView()
                {
                    RowNumber = index + 1,
                    Id = ServiceSaleItemView.Id,
                    Name = ServiceSaleItemView.Name,
                    Quantity = ServiceSaleItemView.Quantity,

                }).ToList();

                lsViewServiceSaleItem.ItemsSource = null;
                lsViewServiceSaleItem.ItemsSource = list2;

                //Hiển thị ProductSaleItemView                    
                List<ProductSaleItemView> lsProductSaleItemView;

                lsProductSaleItemView = (from s in dc.ProductSaleItemViews
                                         where s.IdObjectCareDetail == lCurIdObjectCareDetail
                                         select s).ToList();



                var list3 = lsProductSaleItemView.AsEnumerable().Select((ProductSaleItemView, index) => new ProductSaleItemView()
                {
                    RowNumber = index + 1,
                    Id = ProductSaleItemView.Id,
                    Code = ProductSaleItemView.Code,
                    Name = ProductSaleItemView.Name,
                    Quantity = ProductSaleItemView.Quantity,
                    UnitName = ProductSaleItemView.UnitName


                }).ToList();

                lsViewProductSaleItem.ItemsSource = null;
                lsViewProductSaleItem.ItemsSource = list3;
            }
            catch
            {
                ;
            }
            
        }

        private void btnAddProductSale_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as ProductView;
            lCurProductId = product.Id;

            frmAddSale frmAddSale_ = new frmAddSale("Product", this, lCurIdObjectCareDetail, lCurProductId, decimal.Parse(product.PriceOut.ToString()));
            frmAddSale_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmAddSale_.ShowDialog();            
        }

        public void Refresh_GUI(string objName)
        {
            if (objName == "Service")
            {

                IMSDataContext dc = new IMSDataContext();

                //Hiển thị ServiceSaleItem                    
                List<ServiceSaleItemView> lsServiceSaleItemView;

                lsServiceSaleItemView = (from s in dc.ServiceSaleItemViews
                                         where s.IdObjectCareDetail == lCurIdObjectCareDetail
                                         select s).ToList();



                var list2 = lsServiceSaleItemView.AsEnumerable().Select((ServiceSaleItemView, index) => new ServiceSaleItemView()
                {
                    RowNumber = index + 1,
                    Id = ServiceSaleItemView.Id,
                    Name = ServiceSaleItemView.Name,
                    Quantity = ServiceSaleItemView.Quantity,

                }).ToList();

                lsViewServiceSaleItem.ItemsSource = null;
                lsViewServiceSaleItem.ItemsSource = list2;
            }
            else if (objName == "Product")
            {
                IMSDataContext dc = new IMSDataContext();

                //Hiển thị ProductSaleItemView                    
                List<ProductSaleItemView> lsProductSaleItemView;

                lsProductSaleItemView = (from s in dc.ProductSaleItemViews
                                         where s.IdObjectCareDetail == lCurIdObjectCareDetail
                                         select s).ToList();



                var list2 = lsProductSaleItemView.AsEnumerable().Select((ProductSaleItemView, index) => new ProductSaleItemView()
                {
                    RowNumber = index + 1,
                    Id = ProductSaleItemView.Id,
                    Code = ProductSaleItemView.Code,
                    Name = ProductSaleItemView.Name,
                    Quantity = ProductSaleItemView.Quantity,
                    UnitName = ProductSaleItemView.UnitName


                }).ToList();

                lsViewProductSaleItem.ItemsSource = null;
                lsViewProductSaleItem.ItemsSource = list2;
            }
        }

        private void lsviewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var product = e.AddedItems[0] as ProductView;
                lCurProductId = product.Id;
            }
            catch
            {
                ;
            }
        }

        private void tbrEditObjectCareDetail_Click(object sender, RoutedEventArgs e)
        {
            if (lCurIdObjectCareDetail == -1)
            {
                return;
            }
            frmObjectCareDetail frmObjectCareDetail_ = new frmObjectCareDetail(lCurIdObjectCare, lCurIdObjectCareDetail, this, true);
            frmObjectCareDetail_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmObjectCareDetail_.ShowDialog();
        }

        private void btnRemoveServiceSaleItem_Click(object sender, RoutedEventArgs e)
        {
            var results = MessageBox.Show("Bạn có thực sự muốn xóa dòng dữ liệu?", "IMS - Thông báo", MessageBoxButton.OKCancel);

            if (results != MessageBoxResult.OK)
                return;

            ServiceSaleItemView serviceSaleItem = (sender as Button).DataContext as ServiceSaleItemView;
            //Remove all data matching Id in textbox
            IMSDataContext dc = new IMSDataContext();
            int result = dc.ProcDeleteServiceSaleItem(Convert.ToInt32(serviceSaleItem.Id));

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
                Refresh_GUI("Service");
            }
        }

        private void btnRemoveProductSaleItem_Click(object sender, RoutedEventArgs e)
        {
            var results = MessageBox.Show("Bạn có thực sự muốn xóa dòng dữ liệu?", "IMS - Thông báo", MessageBoxButton.OKCancel);

            if (results != MessageBoxResult.OK)
                return;

            ProductSaleItemView productSaleItem = (sender as Button).DataContext as ProductSaleItemView;
            //Remove all data matching Id in textbox
            IMSDataContext dc = new IMSDataContext();
            int result = dc.ProcDeleteProductSaleItem(Convert.ToInt32(productSaleItem.Id));

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
                Refresh_GUI("Product");
            }
        }

        private void txtServiceName_TextChanged(object sender, TextChangedEventArgs e)
        {
            IMSDataContext dc = new IMSDataContext();
            List<Service> lsService_ = (from s in dc.Services
                                        where s.Name.Contains(txtServiceName.Text)
                                        select s).ToList();

            var list = lsService_.AsEnumerable().Select((Service, index) => new Service()
            {

                RowNumber = index + 1,
                Id = Service.Id,
                Name = Service.Name,
                Price = Service.Price,
                Note = Service.Note
            }).ToList();

            lsViewService.ItemsSource = null;
            lsViewService.ItemsSource = list;
        }

        private void tbrSearchService_Click(object sender, RoutedEventArgs e)
        {
            IMSDataContext dc = new IMSDataContext();
            List<Service> lsService_ = (from s in dc.Services
                                        where s.Name.Contains(txtServiceName.Text)
                                        select s).ToList();

            var list = lsService_.AsEnumerable().Select((Service, index) => new Service()
            {

                RowNumber = index + 1,
                Id = Service.Id,
                Name = Service.Name,
                Price = Service.Price,
                Note = Service.Note
            }).ToList();

            lsViewService.ItemsSource = null;
            lsViewService.ItemsSource = list;
        }

        private void tbrRefreshService_Click(object sender, RoutedEventArgs e)
        {
            IMSDataContext dc = new IMSDataContext();
            List<Service> lsService_ = (from s in dc.Services
                                        where s.Name.Contains(txtServiceName.Text)
                                        select s).ToList();

            var list = lsService_.AsEnumerable().Select((Service, index) => new Service()
            {

                RowNumber = index + 1,
                Id = Service.Id,
                Name = Service.Name,
                Price = Service.Price,
                Note = Service.Note
            }).ToList();

            lsViewService.ItemsSource = null;
            lsViewService.ItemsSource = list;
        }
    }
}
