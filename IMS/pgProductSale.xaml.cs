using IMS;
using IMS.Favorite;
using IMS.View;
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

namespace IMS
{
    /// <summary>
    /// Interaction logic for pgObjectCaring.xaml
    /// </summary>
    public partial class pgProductSale : Page
    {
      
        double h1;
        double h2;
        long lCurIdProductSale = -1;

        List<string> curProductInventory = new List<string>();
        public pgProductSale()
        {
            try

                {
                InitializeComponent();

                if (chkInventoryOutput.IsChecked == true)
                {
                    grdAllInOne.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                    grdAllInOne.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                    grdAllInOne.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                    grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                    grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

                    //tbrAddProductSaleItem.IsEnabled = false;
                    //tbrEditProductSaleItem.IsEnabled = false;

                    txtBarcode.Focus();
                    txtBarcode.SelectAll();
                }
                else
                {
                    grdAllInOne.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                    grdAllInOne.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                    grdAllInOne.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                    grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                    grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);

                    //tbrAddProductSaleItem.IsEnabled = true;
                    //tbrEditProductSaleItem.IsEnabled = true;
                }


                //Hiển thị danh sách bán hàng
                IMSDataContext dc = new IMSDataContext();

                List<ProductSaleView> lsProductSale;
                lsProductSale = (from s in dc.ProductSaleViews
                                 where s.IsDone != true
                                 select s).ToList();



                var list2 = lsProductSale.AsEnumerable().Select((ProductSaleView, index) => new ProductSaleView()
                {
                    Id = ProductSaleView.Id,
                    RowNumber = index + 1,
                    InvoiceNo = ProductSaleView.InvoiceNo,
                    CustomerName = ProductSaleView.CustomerName,
                    CreatedDateEx = ProductSaleView.CreatedDateEx

                }).ToList();

                lsViewProductSale.ItemsSource = null;
                lsViewProductSale.ItemsSource = list2;

                txtBarcode.Focus();
            }
            catch(Exception ex)
            {
                ;
            }
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
            //grdAllInOne.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
            //grdAllInOne.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            //grdAllInOne.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            //grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            //grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            try
            {

                IMSDataContext dc = new IMSDataContext();

                long idInvoice = -1;
                foreach (InventoryInputItem item in lsViewProductInventory.Items.Cast<InventoryInputItem>())
                {
                    Inventory inventory = new Inventory();
                    idInvoice = Convert.ToInt64(item.IdProductPurchase);
                    DateTime dCurDate = DateTime.Now;
                    //MessageBox.Show(item.Quantity.ToString());
                    inventory.Code = item.Code;
                    inventory.DiffNo = item.DiffNo;
                    inventory.CodeEx = item.CodeEx;
                    inventory.CreatedDate = long.Parse(UString.GetLongFromDate(dCurDate).ToString());
                    inventory.CreatedDateEx = dCurDate.ToShortDateString() + " " + dCurDate.ToShortTimeString();
                    inventory.IdInvoice = item.IdProductPurchase;
                    dc.Inventories.InsertOnSubmit(inventory);
                    dc.SubmitChanges();
                }
                dc.ProcUpdateIsDone("ProductPurchase", idInvoice, true);
                Refresh_GUI("Purchase");
                Refresh_GUI("Product");

                lsViewProductInventory.ItemsSource = null;
                lsViewProductInventory.ItemsSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
        public void Refresh_GUI(string objName)
        {
            if (objName == "Purchase")
            {
                //Hiển thị danh sách bán hàng
                IMSDataContext dc = new IMSDataContext();

                List<ProductSaleView> lsProductSale;
                lsProductSale = (from s in dc.ProductSaleViews
                                 where s.IsDone != true
                                 select s).ToList();
                var list2 = lsProductSale.AsEnumerable().Select((ProductSaleView, index) => new ProductSaleView()
                {
                    Id = ProductSaleView.Id,
                    RowNumber = index + 1,
                    InvoiceNo = ProductSaleView.InvoiceNo,
                    CustomerName = ProductSaleView.CustomerName,
                    CreatedDateEx = ProductSaleView.CreatedDateEx

                }).ToList();

                lsViewProductSale.ItemsSource = null;
                lsViewProductSale.ItemsSource = list2;
            }
            else if (objName == "Product")
            {               
                //Hiển thị danh sách bán hàng
                IMSDataContext dc = new IMSDataContext();

                List<ProductSaleView> lsProductSale;
                lsProductSale = (from s in dc.ProductSaleViews
                                 where s.IsDone != true
                                 select s).ToList();
                var list2 = lsProductSale.AsEnumerable().Select((ProductSaleView, index) => new ProductSaleView()
                {
                    Id = ProductSaleView.Id,
                    RowNumber = index + 1,
                    InvoiceNo = ProductSaleView.InvoiceNo,
                    CustomerName = ProductSaleView.CustomerName,
                    CreatedDateEx = ProductSaleView.CreatedDateEx

                }).ToList();

                lsViewProductSale.ItemsSource = null;
                lsViewProductSale.ItemsSource = list2;
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

        //private void btnAddProductSale_Click(object sender, RoutedEventArgs e)
        //{
        //    var product = (sender as Button).DataContext as ProductView;
        //    lCurProductId = product.Id;

        //    frmAddPurchase frmAddPurchase_ = new frmAddPurchase("Product", this, lCurIdProductSale, lCurProductId, decimal.Parse(product.PriceIn.ToString()));
        //    frmAddPurchase_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        //    frmAddPurchase_.ShowDialog();
        //}
        
        private void lsViewProductSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lsViewProductInventory.ItemsSource = null;
                lsViewProductSaleItem.ItemsSource = null;

                var productPurchase = e.AddedItems[0] as ProductSaleView;
                lCurIdProductSale = long.Parse(productPurchase.Id.ToString());

                IMSDataContext dc = new IMSDataContext();

                //Hiển thị chi tiết kho                   
                List<InventoryOutputItemView> lsInventoryOutputItemView = (from s in dc.InventoryOutputItemViews
                                                                           where s.IdProductSale == lCurIdProductSale
                                                                           select s).ToList();
                var list2 = lsInventoryOutputItemView.AsEnumerable().Select((InventoryOutputItemView, index) => new InventoryOutputItemView()
                {
                    RowNumber = index + 1,
                    Id = InventoryOutputItemView.Id,
                    Code = InventoryOutputItemView.Code,
                    DiffNo = InventoryOutputItemView.DiffNo,
                    CodeEx = InventoryOutputItemView.CodeEx,
                    Name = InventoryOutputItemView.Name,
                    PriceOut = InventoryOutputItemView.PriceOut,
                    Quantity = InventoryOutputItemView.Quantity,
                    QuantityDiscount = InventoryOutputItemView.QuantityDiscount,
                    UnitName = InventoryOutputItemView.UnitName,
                }).ToList();
                lsViewProductInventory.ItemsSource = null;
                lsViewProductInventory.ItemsSource = list2;

                //Hiển thị chi tiết đơn hàng                    
                List<InventoryOutputItemGroupView> lsInventoryOutputItemView1 = (from s in dc.InventoryOutputItemGroupViews
                                                                                 where s.IdProductSale == lCurIdProductSale
                                                                                 select s).ToList();
                var list3 = lsInventoryOutputItemView1.AsEnumerable().Select((InventoryOutputItemGroupView, index) => new InventoryOutputItemGroupView()
                {
                    RowNumber = index + 1,                    
                    Code = InventoryOutputItemGroupView.Code,
                    Name = InventoryOutputItemGroupView.Name,
                    PriceOut = InventoryOutputItemGroupView.PriceOut,
                    Quantity = InventoryOutputItemGroupView.Quantity,
                    QuantityDiscount = InventoryOutputItemGroupView.QuantityDiscount,
                    UnitName = InventoryOutputItemGroupView.UnitName,
                }).ToList();
                lsViewProductSaleItem.ItemsSource = null;
                lsViewProductSaleItem.ItemsSource = list3;
            }
            catch(Exception ex)
            {
                ;
            }
        }

        private void btnRemoveProductSaleItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var results = MessageBox.Show("Bạn có thực sự muốn xóa dòng dữ liệu?", "IMS - Thông báo", MessageBoxButton.OKCancel);

                if (results != MessageBoxResult.OK)
                    return;

                InventoryOutputItemGroupView productSaleItem = (sender as Button).DataContext as InventoryOutputItemGroupView;
                //Remove all data matching Id in textbox
                IMSDataContext dc = new IMSDataContext();
                int result = dc.ProcDeleteProductSaleItem(Convert.ToInt32(productSaleItem.IdProductSale));

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
                    //Hiển thị chi tiết hàng bán lên listview                 
                    List<InventoryOutputItemView> lsInventoryOutputItemView = (from s in dc.InventoryOutputItemViews
                                                                               where s.IdProductSale == lCurIdProductSale
                                                                               select s).ToList();
                    var list2 = lsInventoryOutputItemView.AsEnumerable().Select((InventoryOutputItemView, index) => new InventoryOutputItemView()
                    {
                        RowNumber = index + 1,
                        Id = InventoryOutputItemView.Id,
                        Code = InventoryOutputItemView.Code,
                        DiffNo = InventoryOutputItemView.DiffNo,
                        CodeEx = InventoryOutputItemView.CodeEx,
                        Name = InventoryOutputItemView.Name,
                        PriceOut = InventoryOutputItemView.PriceOut,
                        Quantity = InventoryOutputItemView.Quantity,
                        QuantityDiscount = InventoryOutputItemView.QuantityDiscount,
                        UnitName = InventoryOutputItemView.UnitName,
                    }).ToList();

                    lsViewProductInventory.ItemsSource = null;
                    lsViewProductInventory.ItemsSource = list2;

                    //Hiển thị chi tiết đơn hàng                    
                    List<InventoryOutputItemGroupView> lsInventoryOutputItemView1 = (from s in dc.InventoryOutputItemGroupViews
                                                                                     where s.IdProductSale == lCurIdProductSale
                                                                                     select s).ToList();
                    var list3 = lsInventoryOutputItemView1.AsEnumerable().Select((InventoryOutputItemGroupView, index) => new InventoryOutputItemGroupView()
                    {
                        RowNumber = index + 1,
                        Id = InventoryOutputItemGroupView.Id,
                        Code = InventoryOutputItemGroupView.Code,
                        Name = InventoryOutputItemGroupView.Name,
                        PriceOut = InventoryOutputItemGroupView.PriceOut,
                        Quantity = InventoryOutputItemGroupView.Quantity,
                        QuantityDiscount = InventoryOutputItemGroupView.QuantityDiscount,
                        UnitName = InventoryOutputItemGroupView.UnitName,
                    }).ToList();

                    lsViewProductSaleItem.ItemsSource = null;
                    lsViewProductSaleItem.ItemsSource = list3;

                    //Refresh_GUI("Product");
                }
            }
            catch
            {
                ;
            }
        }

        private void tbrEditProductSale_Click(object sender, RoutedEventArgs e)
        {
            frmProductSale frmProductSale_ = new frmProductSale(lCurIdProductSale, this, false);
            frmProductSale_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductSale_.ShowDialog();
        }

        private void tbrAddProductSaleItem_Click(object sender, RoutedEventArgs e)
        {
            grdAllInOne.RowDefinitions[0].Height = new GridLength(7, GridUnitType.Star);
            grdAllInOne.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            grdAllInOne.RowDefinitions[2].Height = new GridLength(3, GridUnitType.Star);

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

        private void tbrAddProductSale_Click(object sender, RoutedEventArgs e)
        {
            frmProductSale frmProductSale_ = new frmProductSale(-1, this, false);
            frmProductSale_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductSale_.ShowDialog();
        }

        private void btnAddProductSale_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button).DataContext as ProductView;
            lCurProductId = product.Id;

            frmAddSale frmAddSale_ = new frmAddSale("Product", this, lCurIdProductSale, lCurProductId, decimal.Parse(product.PriceOut.ToString()));
            frmAddSale_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmAddSale_.ShowDialog();
        }
        

        private void tbrEditProductSale_Click_1(object sender, RoutedEventArgs e)
        {
            frmProductSale frmProductSale_ = new frmProductSale(lCurIdProductSale, this, false);
            frmProductSale_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductSale_.ShowDialog();
        }

        private void chkInventoryOutput_Click(object sender, RoutedEventArgs e)
        {
            if(chkInventoryOutput.IsChecked == true)
            {
                grdAllInOne.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                grdAllInOne.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                grdAllInOne.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

                //tbrAddProductSaleItem.IsEnabled = false;
                //tbrEditProductSaleItem.IsEnabled = false;

                txtBarcode.Focus();
                txtBarcode.SelectAll();
            }
            else
            {
                grdAllInOne.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                grdAllInOne.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                grdAllInOne.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);

                //tbrAddProductSaleItem.IsEnabled = true;
                //tbrEditProductSaleItem.IsEnabled = true;
            }
        }

        private void txtBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(lCurIdProductSale < 0)
            {
                MessageBox.Show("Hãy chọn đơn hàng", "IMS - Thông báo lỗi");
                return;
            }
            IMSDataContext dc = new IMSDataContext();

            InventoryView inventory = null;

            try
            {
                inventory = (from s in dc.InventoryViews
                             where (s.CodeEx == txtBarcode.Text)
                             select s).First();
            }
            catch
            {
                ;
            }

            if (inventory != null)
            {
                //Cập nhật lại trạng thái item trong kho: chưa xuất kho               
                var invItem1 = (from c in dc.Inventories
                                where c.CodeEx == inventory.CodeEx
                                select c).First();
                invItem1.IsOutput = true;
                dc.SubmitChanges();

                //
                InventoryOutputItem inventoryOutputItem = null;
                try
                {
                    inventoryOutputItem = (from s in dc.InventoryOutputItems
                                           where (s.Code == inventory.Code && s.IdProductSale == lCurIdProductSale)
                                 select s).First();
                }
                catch
                {
                    ;
                }

                if (curProductInventory.Contains(inventory.CodeEx))
                {
                    txtBarcode.Text = "";
                    return;
                }
                curProductInventory.Add(inventory.CodeEx);

                txtBarcode.Text = "";

                //Lưu danh sách hàng bán vào table tạm InventoryOutputItem
                InventoryOutputItem productInventoryOutput = new InventoryOutputItem();
                productInventoryOutput.Code = inventory.Code;
                productInventoryOutput.Name = inventory.Name;
                productInventoryOutput.DiffNo = inventory.DiffNo;
                productInventoryOutput.CodeEx = inventory.CodeEx;
                productInventoryOutput.PriceOut = inventory.PriceOut.Value;
                if (inventoryOutputItem != null)
                {
                    productInventoryOutput.PriceOut = inventoryOutputItem.PriceOut;
                }               
                
                productInventoryOutput.Quantity = float.Parse(UString.Mid(inventory.CodeEx, 7, 5));
                productInventoryOutput.QuantityDiscount = 0;
                productInventoryOutput.IdProductSale = lCurIdProductSale;

                dc.InventoryOutputItems.InsertOnSubmit(productInventoryOutput);
                dc.SubmitChanges();


                //Hiển thị chi tiết hàng bán lên listview                 
                List<InventoryOutputItemView> lsInventoryOutputItemView = (from s in dc.InventoryOutputItemViews
                                                                           where s.IdProductSale == lCurIdProductSale
                                                                           select s).ToList();
                var list2 = lsInventoryOutputItemView.AsEnumerable().Select((InventoryOutputItemView, index) => new InventoryOutputItemView()
                {
                    RowNumber = index + 1,
                    Id = InventoryOutputItemView.Id,
                    Code = InventoryOutputItemView.Code,
                    DiffNo = InventoryOutputItemView.DiffNo,
                    CodeEx = InventoryOutputItemView.CodeEx,
                    Name = InventoryOutputItemView.Name,
                    PriceOut = InventoryOutputItemView.PriceOut,
                    Quantity = InventoryOutputItemView.Quantity,
                    QuantityDiscount = InventoryOutputItemView.QuantityDiscount,
                    UnitName = InventoryOutputItemView.UnitName,
                }).ToList();

                lsViewProductInventory.ItemsSource = null;
                lsViewProductInventory.ItemsSource = list2;

                txtBarcode.Focus();

                //Hiển thị chi tiết đơn hàng                    
                List<InventoryOutputItemGroupView> lsInventoryOutputItemView1 = (from s in dc.InventoryOutputItemGroupViews
                                                                                 where s.IdProductSale == lCurIdProductSale
                                                                            select s).ToList();

                var list3 = lsInventoryOutputItemView1.AsEnumerable().Select((InventoryOutputItemGroupView, index) => new InventoryOutputItemGroupView()
                {
                    RowNumber = index + 1,                    
                    Code = InventoryOutputItemGroupView.Code,
                    Name = InventoryOutputItemGroupView.Name,
                    PriceOut = InventoryOutputItemGroupView.PriceOut,
                    Quantity = InventoryOutputItemGroupView.Quantity,
                    QuantityDiscount = InventoryOutputItemGroupView.QuantityDiscount,
                    UnitName = InventoryOutputItemGroupView.UnitName,


                }).ToList();

                lsViewProductSaleItem.ItemsSource = null;
                lsViewProductSaleItem.ItemsSource = list3;                
            } 
        }

        private void cmbSaleInvoiceStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ////Hiển thị danh sách bán hàng
            //IMSDataContext dc = new IMSDataContext();
            //List<ProductSaleView> lsProductSale = null;
            //if (cmbSaleInvoiceStatus.Text == "Tất cả")
            //{                
            //    lsProductSale = (from s in dc.ProductSaleViews                                 
            //                     select s).ToList();
            //}
            //else if (cmbSaleInvoiceStatus.Text == "Đã xử lý")
            //{
            //    lsProductSale = (from s in dc.ProductSaleViews
            //                     where s.IsDone == true
            //                     select s).ToList();
            //}
            //else if (cmbSaleInvoiceStatus.Text == "Đang xử lý")
            //{                
            //    lsProductSale = (from s in dc.ProductSaleViews
            //                     where s.IsDone == false
            //                     select s).ToList();
            //}


            //var list2 = lsProductSale.AsEnumerable().Select((ProductSaleView, index) => new ProductSaleView()
            //{
            //    Id = ProductSaleView.Id,
            //    RowNumber = index + 1,
            //    InvoiceNo = ProductSaleView.InvoiceNo,
            //    CustomerName = ProductSaleView.CustomerName,
            //    CreatedDateEx = ProductSaleView.CreatedDateEx

            //}).ToList();

            //lsViewProductSale.ItemsSource = null;
            //lsViewProductSale.ItemsSource = list2;

            //txtBarcode.Focus();
        }

        private void tbrSaveProductSale_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lCurIdProductSale < 0)
                {
                    MessageBox.Show("Hãy chọn đơn hàng", "IMS - Thông báo lỗi");
                    return;
                }
                IMSDataContext dc = new IMSDataContext();
                
                foreach (InventoryOutputItemView item in lsViewProductInventory.Items.Cast<InventoryOutputItemView>())
                {                    
                    ProductSaleItem productSaleItem = new ProductSaleItem();
                    productSaleItem.IdProduct = (int)item.Id;
                    productSaleItem.Price = item.PriceOut;
                    productSaleItem.Quantity = item.Quantity;
                    productSaleItem.QuantityDiscount = item.QuantityDiscount;
                    productSaleItem.Amount = (decimal)Convert.ToSingle(item.PriceOut) * ((decimal)item.Quantity - (decimal)item.QuantityDiscount);
                    productSaleItem.IdProductSale = (int)lCurIdProductSale;
                    dc.ProductSaleItems.InsertOnSubmit(productSaleItem);
                    dc.SubmitChanges();
                }
                dc.ProcUpdateIsDone("ProductSale", lCurIdProductSale, true);                

                ////lsViewProductInventory.ItemsSource = null;            
    
                //Print receipt
                Process p = new Process();
                p.StartInfo.FileName = "PrintJobs.exe";
                p.StartInfo.Arguments = "Receipt " + 1;

                p.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbrProductSalePayment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lCurIdProductSale < 0)
                {
                    MessageBox.Show("Hãy chọn đơn hàng", "IMS - Thông báo lỗi");
                    return;
                }
                IMSDataContext dc = new IMSDataContext();

                foreach (InventoryOutputItemGroupView item in lsViewProductSaleItem.Items.Cast<InventoryOutputItemGroupView>())
                {
                    IMS_ProcDataContext dcProc = new IMS_ProcDataContext();                    

                    //Lưu dòng dữ liệu bán hàng từ listview trên GUI
                    ProductSaleItem productSaleItem = new ProductSaleItem();
                    productSaleItem.IdProduct = (int)item.Id;
                    productSaleItem.Price = item.PriceOut;
                    productSaleItem.Quantity = (float)item.Quantity;
                    productSaleItem.QuantityDiscount = (float)item.QuantityDiscount;
                    productSaleItem.Amount = (decimal)Convert.ToSingle(item.PriceOut) * ((decimal)item.Quantity - (decimal)item.QuantityDiscount);
                    productSaleItem.IdProductSale = (int)lCurIdProductSale;
                    dc.ProductSaleItems.InsertOnSubmit(productSaleItem);
                    dc.SubmitChanges();

                    //Cập nhật giá mới và KM mới từ listview trên GUI
                    //dcProc.ProcUpdateSalePrice(item.Code, item.PriceOut, (float)item.QuantityDiscount, lCurIdProductSale);
                }

                dc.ProcUpdateIsDone("ProductSale", lCurIdProductSale, true);

                //In biên nhận
                //Print receipt
                Process p = new Process();
                p.StartInfo.FileName = "PrintJobs.exe";
                p.StartInfo.Arguments = "Receipt " + lCurIdProductSale.ToString();

                p.Start();

                //Hiển thị danh sách bán hàng 
                List<ProductSaleView> lsProductSale;
                lsProductSale = (from s in dc.ProductSaleViews
                                 where s.IsDone != true
                                 select s).ToList();

                var list2 = lsProductSale.AsEnumerable().Select((ProductSaleView, index) => new ProductSaleView()
                {
                    Id = ProductSaleView.Id,
                    RowNumber = index + 1,
                    InvoiceNo = ProductSaleView.InvoiceNo,
                    CustomerName = ProductSaleView.CustomerName,
                    CreatedDateEx = ProductSaleView.CreatedDateEx

                }).ToList();

                lsViewProductSale.ItemsSource = null;
                lsViewProductSale.ItemsSource = list2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemoveBarcodeItem_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                IMSDataContext dc = new IMSDataContext();

                //Xóa item trong danh sách barcode
                var invOutItem = (sender as Button).DataContext as InventoryOutputItemView;
                var invOutItem1 =
                            (from c in dc.InventoryOutputItems
                             where c.CodeEx == invOutItem.CodeEx
                             select c).First();

                dc.InventoryOutputItems.DeleteOnSubmit(invOutItem1);
                dc.SubmitChanges();

                //Cập nhật lại trạng thái item trong kho: chưa xuất kho               
                var invItem1 = (from c in dc.Inventories
                                where c.CodeEx == invOutItem.CodeEx
                                select c).First();
                invItem1.IsOutput = false;
                
                dc.SubmitChanges();


                //Hiển thị chi tiết hàng bán lên listview                 
                List<InventoryOutputItemView> lsInventoryOutputItemView = (from s in dc.InventoryOutputItemViews
                                                                           where s.IdProductSale == lCurIdProductSale
                                                                           select s).ToList();
                var list2 = lsInventoryOutputItemView.AsEnumerable().Select((InventoryOutputItemView, index) => new InventoryOutputItemView()
                {
                    RowNumber = index + 1,
                    Id = InventoryOutputItemView.Id,
                    Code = InventoryOutputItemView.Code,
                    DiffNo = InventoryOutputItemView.DiffNo,
                    CodeEx = InventoryOutputItemView.CodeEx,
                    Name = InventoryOutputItemView.Name,
                    PriceOut = InventoryOutputItemView.PriceOut,
                    Quantity = InventoryOutputItemView.Quantity,
                    UnitName = InventoryOutputItemView.UnitName,
                }).ToList();

                lsViewProductInventory.ItemsSource = null;
                lsViewProductInventory.ItemsSource = list2;                

                //Hiển thị chi tiết đơn hàng                    
                List<InventoryOutputItemGroupView> lsInventoryOutputItemView1 = (from s in dc.InventoryOutputItemGroupViews
                                                                                 where s.IdProductSale == lCurIdProductSale
                                                                                 select s).ToList();
                var list3 = lsInventoryOutputItemView1.AsEnumerable().Select((InventoryOutputItemGroupView, index) => new InventoryOutputItemGroupView()
                {
                    RowNumber = index + 1,
                    Id = InventoryOutputItemGroupView.Id,
                    Code = InventoryOutputItemGroupView.Code,
                    Name = InventoryOutputItemGroupView.Name,
                    PriceOut = InventoryOutputItemGroupView.PriceOut,
                    Quantity = InventoryOutputItemGroupView.Quantity,
                    QuantityDiscount = InventoryOutputItemGroupView.QuantityDiscount,
                    UnitName = InventoryOutputItemGroupView.UnitName,
                }).ToList();

                lsViewProductSaleItem.ItemsSource = null;
                lsViewProductSaleItem.ItemsSource = list3;
            }
        }

        private void btnSaveProductSale_Click(object sender, RoutedEventArgs e)
        {
            IMSDataContext dc = new IMSDataContext();

            foreach (InventoryOutputItemGroupView item in lsViewProductSaleItem.Items.Cast<InventoryOutputItemGroupView>())
            {
                ProductSaleItem productSaleItem = new ProductSaleItem();
                productSaleItem.IdProduct = (int)item.Id;
                productSaleItem.Price = item.PriceOut;
                productSaleItem.Quantity = (float)item.Quantity;
                productSaleItem.Amount = (decimal)Convert.ToSingle(item.PriceOut) * (decimal)item.Quantity;
                productSaleItem.IdProductSale = (int)lCurIdProductSale;
                dc.ProductSaleItems.InsertOnSubmit(productSaleItem);
                dc.SubmitChanges();
            }
            dc.ProcUpdateIsDone("ProductSale", lCurIdProductSale, true);

        }

        private void btnSaveProductSaleItem_Click(object sender, RoutedEventArgs e)
        {
            IMSDataContext dc = new IMSDataContext();
            var invOutItem = (sender as Button).DataContext as InventoryOutputItemGroupView;
            dc.ProcUpdateSalePrice(invOutItem.Code, invOutItem.PriceOut, lCurIdProductSale);
        }

        private void RefreshOnPurchaseInvoice()
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();

                //Hiển thị chi tiết kho                   
                List<InventoryOutputItemView> lsInventoryOutputItemView = (from s in dc.InventoryOutputItemViews
                                                                           where s.IdProductSale == lCurIdProductSale
                                                                           select s).ToList();
                var list2 = lsInventoryOutputItemView.AsEnumerable().Select((InventoryOutputItemView, index) => new InventoryOutputItemView()
                {
                    RowNumber = index + 1,
                    Id = InventoryOutputItemView.Id,
                    Code = InventoryOutputItemView.Code,
                    DiffNo = InventoryOutputItemView.DiffNo,
                    CodeEx = InventoryOutputItemView.CodeEx,
                    Name = InventoryOutputItemView.Name,
                    PriceOut = InventoryOutputItemView.PriceOut,
                    Quantity = InventoryOutputItemView.Quantity,
                    QuantityDiscount = InventoryOutputItemView.QuantityDiscount,
                    UnitName = InventoryOutputItemView.UnitName,
                }).ToList();
                lsViewProductInventory.ItemsSource = null;
                lsViewProductInventory.ItemsSource = list2;

                //Hiển thị chi tiết đơn hàng                    
                List<InventoryOutputItemGroupView> lsInventoryOutputItemView1 = (from s in dc.InventoryOutputItemGroupViews
                                                                                 where s.IdProductSale == lCurIdProductSale
                                                                                 select s).ToList();
                var list3 = lsInventoryOutputItemView1.AsEnumerable().Select((InventoryOutputItemGroupView, index) => new InventoryOutputItemGroupView()
                {
                    RowNumber = index + 1,
                    Code = InventoryOutputItemGroupView.Code,
                    Name = InventoryOutputItemGroupView.Name,
                    PriceOut = InventoryOutputItemGroupView.PriceOut,
                    Quantity = InventoryOutputItemGroupView.Quantity,
                    QuantityDiscount = InventoryOutputItemGroupView.QuantityDiscount,
                    UnitName = InventoryOutputItemGroupView.UnitName,
                }).ToList();
                lsViewProductSaleItem.ItemsSource = null;
                lsViewProductSaleItem.ItemsSource = list3;
            }
            catch
            {
                ;
            }
        }

        private void tbrRefreshProductSale_Click(object sender, RoutedEventArgs e)
        {
            //Hiển thị danh sách bán hàng
            IMSDataContext dc = new IMSDataContext();

            List<ProductSaleView> lsProductSale;
            lsProductSale = (from s in dc.ProductSaleViews
                             where s.IsDone != true
                             select s).ToList();

            var list2 = lsProductSale.AsEnumerable().Select((ProductSaleView, index) => new ProductSaleView()
            {
                Id = ProductSaleView.Id,
                RowNumber = index + 1,
                InvoiceNo = ProductSaleView.InvoiceNo,
                CustomerName = ProductSaleView.CustomerName,
                CreatedDateEx = ProductSaleView.CreatedDateEx

            }).ToList();

            lsViewProductSale.ItemsSource = null;
            lsViewProductSale.ItemsSource = list2;

            txtBarcode.Focus();
        }

        private void tbrPreview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lCurIdProductSale < 0)
                {
                    MessageBox.Show("Hãy chọn đơn hàng", "IMS - Thông báo lỗi");
                    return;
                }
                IMSDataContext dc = new IMSDataContext();

                foreach (InventoryOutputItemGroupView item in lsViewProductSaleItem.Items.Cast<InventoryOutputItemGroupView>())
                {
                    IMS_ProcDataContext dcProc = new IMS_ProcDataContext();

                    //Lưu dòng dữ liệu bán hàng từ listview trên GUI
                    ProductSaleItem productSaleItem = new ProductSaleItem();
                    productSaleItem.IdProduct = (int)item.Id;
                    productSaleItem.Price = item.PriceOut;
                    productSaleItem.Quantity = (float)item.Quantity;
                    productSaleItem.QuantityDiscount = (float)item.QuantityDiscount;
                    productSaleItem.Amount = (decimal)Convert.ToSingle(item.PriceOut) * ((decimal)item.Quantity - (decimal)item.QuantityDiscount);
                    productSaleItem.IdProductSale = (int)lCurIdProductSale;
                    dc.ProductSaleItems.InsertOnSubmit(productSaleItem);
                    dc.SubmitChanges();

                    //Cập nhật giá mới và KM mới từ listview trên GUI
                    //dcProc.ProcUpdateSalePrice(item.Code, item.PriceOut, (float)item.QuantityDiscount, lCurIdProductSale);
                }

                dc.ProcUpdateIsDone("ProductSale", lCurIdProductSale, false);

                //In biên nhận
                //Print receipt
                Process p = new Process();
                p.StartInfo.FileName = "PrintJobs.exe";
                p.StartInfo.Arguments = "Receipt " + lCurIdProductSale.ToString();

                p.Start();

                //Hiển thị danh sách bán hàng 
                List<ProductSaleView> lsProductSale;
                lsProductSale = (from s in dc.ProductSaleViews
                                 where s.IsDone != true
                                 select s).ToList();

                //var list2 = lsProductSale.AsEnumerable().Select((ProductSaleView, index) => new ProductSaleView()
                //{
                //    Id = ProductSaleView.Id,
                //    RowNumber = index + 1,
                //    InvoiceNo = ProductSaleView.InvoiceNo,
                //    CustomerName = ProductSaleView.CustomerName,
                //    CreatedDateEx = ProductSaleView.CreatedDateEx

                //}).ToList();

                //lsViewProductSale.ItemsSource = null;
                //lsViewProductSale.ItemsSource = list2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        decimal curPrice = 0;
        string curKind = string.Empty;
        private void lsViewProductSaleItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var invGroupView = e.AddedItems[0] as InventoryOutputItemGroupView;
                curPrice = decimal.Parse(invGroupView.PriceOut.ToString());
                curKind = invGroupView.Code;
            }
            catch
            {
                ;
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        private void tbrEqualizePrice_Click(object sender, RoutedEventArgs e)
        {
            //Nhận giá trị trả về từ event là đối tượng kiểu InventoryOutputItemGroupView
            var invOutItem = (sender as Button).DataContext as InventoryOutputItemGroupView;
            string sCurPrice = invOutItem.PriceOut.ToString();            

            foreach (InventoryOutputItemGroupView item in lsViewProductSaleItem.Items.Cast<InventoryOutputItemGroupView>())
            {
                //InventoryOutputItemGroupView mlbox = (sender as TextBox).DataContext as InventoryOutputItemGroupView;
                ListBoxItem myListBoxItem = (ListBoxItem)(lsViewProductSaleItem.ItemContainerGenerator.ContainerFromItem(item));

                // Getting the ContentPresenter of myListBoxItem
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);

                // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                TextBox myTextBox = (TextBox)myDataTemplate.FindName("txtPrice", myContentPresenter);

                //myTextBox.Text = sCurPrice;

                //Cập nhật giá mới và KM mới từ listview trên GUI
                IMS_ProcDataContext dcProc = new IMS_ProcDataContext();                
                dcProc.ProcUpdateSalePrice(item.Code, invOutItem.PriceOut, -1, lCurIdProductSale);
            }
            RefreshOnPurchaseInvoice();
        }

        private void tbrUpdateDiscount_Click(object sender, RoutedEventArgs e)
        {
            //Nhận giá trị trả về từ event là đối tượng kiểu InventoryOutputItemGroupView
            var invOutItem = (sender as Button).DataContext as InventoryOutputItemGroupView;
            string sCurDiscount = invOutItem.QuantityDiscount.ToString();

            foreach (InventoryOutputItemGroupView item in lsViewProductSaleItem.Items.Cast<InventoryOutputItemGroupView>())
            {
                //InventoryOutputItemGroupView mlbox = (sender as TextBox).DataContext as InventoryOutputItemGroupView;
                ListBoxItem myListBoxItem = (ListBoxItem)(lsViewProductSaleItem.ItemContainerGenerator.ContainerFromItem(item));

                // Getting the ContentPresenter of myListBoxItem
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);

                // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                TextBox myTextBox = (TextBox)myDataTemplate.FindName("txtDiscount", myContentPresenter);

                //myTextBox.Text = sCurDiscount;

                //Cập nhật giá mới và KM mới từ listview trên GUI
                IMS_ProcDataContext dcProc = new IMS_ProcDataContext();
                dcProc.ProcUpdateSalePrice(item.Code, item.PriceOut, (float)invOutItem.QuantityDiscount, lCurIdProductSale);
            }
            RefreshOnPurchaseInvoice();
        }

        private void TextBlock_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Nhận giá trị trả về từ event là đối tượng kiểu InventoryOutputItemGroupView
            var invOutItem = (sender as TextBox).DataContext as InventoryOutputItemGroupView;
            string sCurDiscount = invOutItem.PriceOut.ToString();
            
            //Cập nhật giá mới và KM mới từ listview trên GUI
            IMS_ProcDataContext dcProc = new IMS_ProcDataContext();
            dcProc.ProcUpdateSalePrice(invOutItem.Code, invOutItem.PriceOut, -1, lCurIdProductSale);
        }

        private void txtDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Nhận giá trị trả về từ event là đối tượng kiểu InventoryOutputItemGroupView
            var invOutItem = (sender as TextBox).DataContext as InventoryOutputItemGroupView;
            string sCurDiscount = invOutItem.PriceOut.ToString();
           
            //Cập nhật giá mới và KM mới từ listview trên GUI
            IMS_ProcDataContext dcProc = new IMS_ProcDataContext();
            dcProc.ProcUpdateSalePrice(invOutItem.Code, invOutItem.PriceOut, (float)invOutItem.QuantityDiscount, lCurIdProductSale);            
        }
    }
}
