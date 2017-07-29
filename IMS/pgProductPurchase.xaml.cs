using IMS;
using IMS.DBHelper;
using IMS.Favorite;
using IMS.General;
using IMS.View;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class pgProductPurchase : Page
    {
       
        long lCurIdProductPurchase = -1;
        int lCurSeviceId = -1;
        int lCurProductId = -1;
        string sCurInvoiceKind = "MH";
        public pgProductPurchase()
        {
            try
            {
                InitializeComponent();

                UString.SetSystem();

                grdAllInOne.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                grdAllInOne.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
                grdAllInOne.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

                int idProductPurchase = -1;
                //var abc = this.NavigationService.CurrentSource;
                {
                    idProductPurchase = -1;
                }

                IMSDataContext dc = new IMSDataContext();

                //Hiển thị danh sách chứng từ mua hàng    
                List<ProductPurchaseView> lsProdcutPurchase;
                if (idProductPurchase == -1)
                {
                    lsProdcutPurchase = (from s in dc.ProductPurchaseViews
                                         where s.IsDone != true && s.Kind == IMS.General.GeneralParams.ProductPurchase
                                         select s).ToList();
                }
                else
                {
                    lsProdcutPurchase = (from s in dc.ProductPurchaseViews
                                         where s.Id == idProductPurchase && s.IsDone != true && s.Kind == IMS.General.GeneralParams.ProductPurchase
                                         select s).ToList();
                }
                var list2 = lsProdcutPurchase.AsEnumerable().Select((ProductPurchaseView, index) => new ProductPurchaseView()
                {
                    Id = ProductPurchaseView.Id,
                    RowNumber = index + 1,
                    InvoiceNo = ProductPurchaseView.InvoiceNo,
                    VendorName = ProductPurchaseView.VendorName,
                    Kind = ProductPurchaseView.Kind,
                    CreatedDateEx = ProductPurchaseView.CreatedDateEx
                }).ToList();
                lsViewProductPurchase.ItemsSource = null;
                lsViewProductPurchase.ItemsSource = list2;

                //Hiển thị danh sách đơn đặt mua hàng    
                List<ProductPurchaseView> lsProdcutPurchaseOrder;

                lsProdcutPurchaseOrder = (from s in dc.ProductPurchaseViews
                                         where s.IsDone != true && s.Kind == IMS.General.GeneralParams.ProductPurchaseOrder
                                          select s).ToList();
                
                var list3 = lsProdcutPurchaseOrder.AsEnumerable().Select((ProductPurchaseView, index) => new ProductPurchaseView()
                {
                    Id = ProductPurchaseView.Id,
                    RowNumber = index + 1,
                    InvoiceNo = ProductPurchaseView.InvoiceNo,
                    VendorName = ProductPurchaseView.VendorName,
                    Kind = ProductPurchaseView.Kind,
                    CreatedDateEx = ProductPurchaseView.CreatedDateEx
                }).ToList();
                lsViewProductPurchaseOrder.ItemsSource = null;
                lsViewProductPurchaseOrder.ItemsSource = list3;
            }
            catch
            {
                ;
            }
        }        

        private void tbrComplete_Click(object sender, RoutedEventArgs e)
        {
            grdAllInOne.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
            grdAllInOne.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
            grdAllInOne.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
        }

        private void tbrAddProductSale_Click(object sender, RoutedEventArgs e)
        {
            sCurInvoiceKind = "MH";

            grdAllInOne.RowDefinitions[0].Height = new GridLength(7, GridUnitType.Star);
            grdAllInOne.RowDefinitions[1].Height = new GridLength(3, GridUnitType.Star);
            grdAllInOne.RowDefinitions[2].Height = new GridLength(0, GridUnitType.Star);

            grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);

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

        public void Refresh_GUI(string objName)
        {
            if (objName == "Purchase")
            {

                IMSDataContext dc = new IMSDataContext();

                //Hiển thị danh sách chứng từ mua hàng    
                List<ProductPurchaseView> lsProdcutPurchase;
                
                lsProdcutPurchase = (from s in dc.ProductPurchaseViews
                                    where s.IsDone != true && s.Kind == IMS.General.GeneralParams.ProductPurchase
                                    select s).ToList();

                var list2 = lsProdcutPurchase.AsEnumerable().Select((ProductPurchaseView, index) => new ProductPurchaseView()
                {
                    Id = ProductPurchaseView.Id,
                    RowNumber = index + 1,
                    InvoiceNo = ProductPurchaseView.InvoiceNo,
                    VendorName = ProductPurchaseView.VendorName,
                    Kind = ProductPurchaseView.Kind,
                    CreatedDateEx = ProductPurchaseView.CreatedDateEx
                }).ToList();
                lsViewProductPurchase.ItemsSource = null;
                lsViewProductPurchase.ItemsSource = list2;

                //Hiển thị danh sách đơn đặt mua hàng    
                List<ProductPurchaseView> lsProdcutPurchaseOrder;

                lsProdcutPurchaseOrder = (from s in dc.ProductPurchaseViews
                                          where s.IsDone != true && s.Kind == IMS.General.GeneralParams.ProductPurchaseOrder
                                          select s).ToList();

                var list3 = lsProdcutPurchaseOrder.AsEnumerable().Select((ProductPurchaseView, index) => new ProductPurchaseView()
                {
                    Id = ProductPurchaseView.Id,
                    RowNumber = index + 1,
                    InvoiceNo = ProductPurchaseView.InvoiceNo,
                    VendorName = ProductPurchaseView.VendorName,
                    Kind = ProductPurchaseView.Kind,
                    CreatedDateEx = ProductPurchaseView.CreatedDateEx
                }).ToList();
                lsViewProductPurchaseOrder.ItemsSource = null;
                lsViewProductPurchaseOrder.ItemsSource = list3;
            }
            else if (objName == "Product")
            {
                IMSDataContext dc = new IMSDataContext();

                if (sCurInvoiceKind == "MH")
                {
                    //Hiển thị danh sách hàng trong ct mua hàng                 
                    List<ProductPurchaseItemView> lsProductPurchaseItemView;
                    lsProductPurchaseItemView = (from s in dc.ProductPurchaseItemViews
                                                 where s.IdProductPurchase == lCurIdProductPurchase
                                                 select s).ToList();



                    var list2 = lsProductPurchaseItemView.AsEnumerable().Select((ProductPurchaseItemView, index) => new ProductPurchaseItemView()
                    {
                        RowNumber = index + 1,
                        Id = ProductPurchaseItemView.Id,
                        Code = ProductPurchaseItemView.Code,
                        Name = ProductPurchaseItemView.Name,
                        Price = ProductPurchaseItemView.Price,
                        Quantity = ProductPurchaseItemView.Quantity,
                        UnitName = ProductPurchaseItemView.UnitName
                    }).ToList();

                    lsViewProductPurchaseItem.ItemsSource = null;
                    lsViewProductPurchaseItem.ItemsSource = list2;
                }
                else if (sCurInvoiceKind == "ĐH")
                {

                    //Hiển thị danh sách hàng trong ct đơn đặt mua hàng                 
                    List<ProductPurchaseItemView> lsProductPurchaseOrderItemView;
                    lsProductPurchaseOrderItemView = (from s in dc.ProductPurchaseItemViews
                                                      where s.IdProductPurchase == lCurIdProductPurchase
                                                      select s).ToList();



                    var list3 = lsProductPurchaseOrderItemView.AsEnumerable().Select((ProductPurchaseItemView, index) => new ProductPurchaseItemView()
                    {
                        RowNumber = index + 1,
                        Id = ProductPurchaseItemView.Id,
                        Code = ProductPurchaseItemView.Code,
                        Name = ProductPurchaseItemView.Name,
                        Price = ProductPurchaseItemView.Price,
                        Quantity = ProductPurchaseItemView.Quantity,
                        UnitName = ProductPurchaseItemView.UnitName
                    }).ToList();

                    lsViewProductPurchaseOrderItem.ItemsSource = null;
                    lsViewProductPurchaseOrderItem.ItemsSource = list3;
                }
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

        private void btnAddProductPurchase_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as ProductView;
            lCurProductId = product.Id;

            frmAddPurchase frmAddPurchase_ = new frmAddPurchase("Product", this, lCurIdProductPurchase, lCurProductId, product.PriceIn.Value);
            frmAddPurchase_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmAddPurchase_.ShowDialog();
        }

        private void tbrAddProductPurchase_Click(object sender, RoutedEventArgs e)
        {
            frmProductPurchase frmProductPurchase_ = new frmProductPurchase(-1, this, false);
            frmProductPurchase_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductPurchase_.ShowDialog();
        }

        private void lsViewProductPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var productPurchase = e.AddedItems[0] as ProductPurchaseView;
                lCurIdProductPurchase = long.Parse(productPurchase.Id.ToString());

                //Hiển thị ProductSaleItemView                 
                IMSDataContext dc = new IMSDataContext();
                List<ProductPurchaseItemView> lsProductPurchaseItemView;

                lsProductPurchaseItemView = (from s in dc.ProductPurchaseItemViews
                                         where s.IdProductPurchase == lCurIdProductPurchase
                                             select s).ToList();

                var list3 = lsProductPurchaseItemView.AsEnumerable().Select((ProductPurchaseItemView, index) => new ProductPurchaseItemView()
                {
                    RowNumber = index + 1,
                    Id = ProductPurchaseItemView.Id,
                    Code = ProductPurchaseItemView.Code,
                    Name = ProductPurchaseItemView.Name,
                    Quantity = ProductPurchaseItemView.Quantity,
                    UnitName = ProductPurchaseItemView.UnitName


                }).ToList();

                lsViewProductPurchaseItem.ItemsSource = null;
                lsViewProductPurchaseItem.ItemsSource = list3;
            }
            catch(Exception ex)
            {
                ;
            }
        }

        private void btnRemoveProductPurchaseItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var results = MessageBox.Show("Bạn có thực sự muốn xóa dòng dữ liệu?", "IMS - Thông báo", MessageBoxButton.OKCancel);

                if (results != MessageBoxResult.OK)
                    return;

                ProductPurchaseItemView productPurchaseItem = (sender as Button).DataContext as ProductPurchaseItemView;
                //Remove all data matching Id in textbox
                IMSDataContext dc = new IMSDataContext();
                int result = dc.ProcDeleteProductPurchaseItem(Convert.ToInt32(productPurchaseItem.Id), productPurchaseItem.Quantity);

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
            catch
            {
                ;
            }
        }

        private void tbrEditProductPurchase_Click(object sender, RoutedEventArgs e)
        {
            frmProductPurchase frmProductPurchase_ = new frmProductPurchase(lCurIdProductPurchase, this, false);
            frmProductPurchase_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductPurchase_.ShowDialog();
        }

        private void tbrAddProductSaleFromFile_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "Excel Files (*.xls|*.xlsx"};
            var result = ofd.ShowDialog();
            if (result == false) return;
            string file = ofd.FileName;
            MyProgressBar pb = new MyProgressBar();
            pb.Show();
            try
            {
                DataTable employeeData = SqlDataConnection.ReadExcelContents(file);
                for (int i = 0; i < employeeData.Rows.Count; i++)
                {
                    DataRow row = employeeData.Rows[i];

                    IMSDataContext dc = new IMSDataContext();
                    ProductPurchaseItem productPurchaseItem = new ProductPurchaseItem();
                    string code = row[0].ToString().Replace(" ", "").Length < 7 ? UString.AddZeroBefore(row[0].ToString().Replace(" ", ""), 7 - row[0].ToString().Replace(" ", "").Length) : row[0].ToString().Replace(" ", "");
                    productPurchaseItem.IdProduct = dc.ProcInvoiceNo(code, "Product");
                    productPurchaseItem.Price = 0;
                    //float a = (float)double.Parse(txtQuantity.Text.Replace(" ", ""));
                    productPurchaseItem.Quantity = (float)Convert.ToSingle(row[1]);
                    productPurchaseItem.Amount = (float)productPurchaseItem.Price * productPurchaseItem.Quantity;
                    productPurchaseItem.IdProductPurchase = lCurIdProductPurchase;
                    dc.ProductPurchaseItems.InsertOnSubmit(productPurchaseItem);
                    dc.SubmitChanges();
                }
                Refresh_GUI("Product");
            }
            catch (Exception ex)
            {
                ;
            }
        }
        private void btnRemoveProductPurchase_Click(object sender, RoutedEventArgs e)
        {
            GeneralFuctions.DeleteTable("ProductPurchase",(int)lCurIdProductPurchase);

            //Hiển thị danh sách chứng từ 
            IMSDataContext dc = new IMSDataContext();                
            List<ProductPurchaseView> lsProdcutPurchase;            
            lsProdcutPurchase = (from s in dc.ProductPurchaseViews
                                     where s.IsDone != true && s.Kind == IMS.General.GeneralParams.purchaseKind
                                     select s).ToList();

            var list2 = lsProdcutPurchase.AsEnumerable().Select((ProductPurchaseView, index) => new ProductPurchaseView()
            {
                Id = ProductPurchaseView.Id,
                RowNumber = index + 1,
                InvoiceNo = ProductPurchaseView.InvoiceNo,
                VendorName = ProductPurchaseView.VendorName,
                Kind = ProductPurchaseView.Kind,
                CreatedDateEx = ProductPurchaseView.CreatedDateEx

            }).ToList();

            lsViewProductPurchase.ItemsSource = null;
            lsViewProductPurchase.ItemsSource = list2;
        }

        private void tbrMove2ProductPurchaseInvoice_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbrAddProductPurchaseOrder_Click(object sender, RoutedEventArgs e)
        {
            sCurInvoiceKind = "ĐH";

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

        private void lsViewProductPurchaseOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var productPurchase = e.AddedItems[0] as ProductPurchaseView;
                lCurIdProductPurchase = long.Parse(productPurchase.Id.ToString());

                //Hiển thị ProductSaleItemView                 
                IMSDataContext dc = new IMSDataContext();
                List<ProductPurchaseItemView> lsProductPurchaseItemView;

                lsProductPurchaseItemView = (from s in dc.ProductPurchaseItemViews
                                             where s.IdProductPurchase == lCurIdProductPurchase
                                             select s).ToList();

                var list3 = lsProductPurchaseItemView.AsEnumerable().Select((ProductPurchaseItemView, index) => new ProductPurchaseItemView()
                {
                    RowNumber = index + 1,
                    Id = ProductPurchaseItemView.Id,
                    Code = ProductPurchaseItemView.Code,
                    Name = ProductPurchaseItemView.Name,
                    Quantity = ProductPurchaseItemView.Quantity,
                    UnitName = ProductPurchaseItemView.UnitName


                }).ToList();

                lsViewProductPurchaseOrderItem.ItemsSource = null;
                lsViewProductPurchaseOrderItem.ItemsSource = list3;
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void tbrAddProductPurchaseOrderInvoice_Click(object sender, RoutedEventArgs e)
        {
            GeneralParams.purchaseKind = GeneralParams.ProductPurchaseOrder;

            frmProductPurchase frmProductPurchase_ = new frmProductPurchase(-1, this, false);
            frmProductPurchase_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductPurchase_.ShowDialog();
        }

        private void mnuLink2ProductPurchase_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void mnuCompareProductOnPurchaseAndOrder_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
