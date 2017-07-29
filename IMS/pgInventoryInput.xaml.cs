using IMS;
using IMS.Favorite;
using IMS.Model;
using IMS.Printing;
using IMS.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
    public partial class pgInventoryInput : Page
    {
        bool isFullScreen = false;
        long lCurIdProductPurchase = -1;
        public pgInventoryInput()
        {
            InitializeComponent();            

            grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

            int idProductPurchase = -1;
            //var abc = this.NavigationService.CurrentSource;
            {
                idProductPurchase = -1;
            }

            IMSDataContext dc = new IMSDataContext();
            //Hiển thị danh sách đợt sửa chữa         
            List<ProductPurchaseView> lsProdcutPurchase;
            if (idProductPurchase == -1)
            {
                lsProdcutPurchase = (from s in dc.ProductPurchaseViews
                                     where s.IsDone != true
                                     select s).ToList();
            }
            else
            {
                lsProdcutPurchase = (from s in dc.ProductPurchaseViews
                                     where s.Id == idProductPurchase
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


            var storageList = (from s in dc.InventoryStorages select s);
            cmbStorage.ItemsSource = storageList;
            cmbStorage.DisplayMemberPath = "Name";
            cmbStorage.SelectedValuePath = "Id";
            cmbStorage.SelectedIndex = 0;
        }

        private void tbrAddServiceSaleItem_Click(object sender, RoutedEventArgs e)
        {

            ; 
        }

        private void tbrComplete_Click(object sender, RoutedEventArgs e)
        {
            grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
        }

        private void tbrAddProductSale_Click(object sender, RoutedEventArgs e)
        {
            ;
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

                IMSDataContext dc = new IMSDataContext();
                //Hiển thị danh sách đợt sửa chữa         
                List<ProductPurchaseView> lsProdcutPurchase;
                lsProdcutPurchase = (from s in dc.ProductPurchaseViews
                                     where s.IsDone != true
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
            else if (objName == "Product")
            {
                IMSDataContext dc = new IMSDataContext();

                //Hiển thị ProductSaleItemView                    
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
                    Quantity = ProductPurchaseItemView.Quantity,
                    UnitName = ProductPurchaseItemView.UnitName,
                    Price = ProductPurchaseItemView.Price

                }).ToList();

                lsViewProductPurchaseItem.ItemsSource = null;
                lsViewProductPurchaseItem.ItemsSource = list2;
            }
            else if (objName == "Inventory")
            {                 
                
                IMSDataContext dc = new IMSDataContext();

                //Hiển thị ProductSaleItemView                    
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
                    Quantity = ProductPurchaseItemView.Quantity,
                    UnitName = ProductPurchaseItemView.UnitName,
                    Price = ProductPurchaseItemView.Price

                }).ToList();

                lsViewProductPurchaseItem.ItemsSource = null;
                lsViewProductPurchaseItem.ItemsSource = list2;
                
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

            frmAddPurchase frmAddPurchase_ = new frmAddPurchase("Product", null, lCurIdProductPurchase, lCurProductId, decimal.Parse(product.PriceIn.ToString()));
            frmAddPurchase_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmAddPurchase_.ShowDialog();
        }

        private void tbrAddProductPurchase_Click(object sender, RoutedEventArgs e)
        {
            frmProductPurchase frmProductPurchase_ = new frmProductPurchase(-1, null, false);
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
                    Price = ProductPurchaseItemView.Price,
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
            frmProductPurchase frmProductPurchase_ = new frmProductPurchase(lCurIdProductPurchase, null, false);
            frmProductPurchase_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductPurchase_.ShowDialog();
        }


        public byte[] ImageToByte(System.Drawing.Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        //Hàm nạp các item hàng hóa từ chứng từ mua vào danh sách nhập kho
        private void tbrAddItem2Inventory_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Dictionary<String, int> rollNo = new Dictionary<string, int>();
                //Add item to temp inventory (mem)
                IMSDataContext dc = new IMSDataContext();

                List<InventoryInputItem> lstInv = new List<InventoryInputItem>();
                InventoryInputItem invItem = new InventoryInputItem();
                int curDiffNo = 0;
                if (chkDiffNoAuto.IsChecked.Value)
                {
                    int outRollNo, outRollNo1;
                    if ((!int.TryParse(txtRollNoStart.Text.ToString(), out outRollNo)) || (!int.TryParse(txtRollNoEnd.Text.ToString(), out outRollNo1)))
                    {
                        MessageBox.Show("Hãy nhập Roll No. là số", "IMS - Thông báo lỗi");
                        return;
                    }
                    if (outRollNo < 1)
                    {
                        MessageBox.Show("Roll No. phải lớn hơn 0!", "IMS - Thông báo lỗi");
                        return;
                    }

                    curDiffNo = int.Parse(txtRollNoStart.Text);
                }
                else
                {
                    curDiffNo = dc.ProcGetDiffNo("");
                    curDiffNo++;
                }

                foreach (ProductPurchaseItemView item in lsViewProductPurchaseItem.Items.Cast<ProductPurchaseItemView>())
                {
                    if (chkDiffNoAuto.IsChecked.Value)
                    {
                        if (curDiffNo > int.Parse(txtRollNoEnd.Text))
                        {
                            break;
                        }
                        if (curDiffNo < 1)
                        {
                            invItem.DiffNo = int.Parse(txtRollNoStart.Text) + 1;
                            curDiffNo = int.Parse(txtRollNoStart.Text) + 1;
                        }
                        else
                        {
                            invItem.DiffNo = curDiffNo++;
                        }
                    }
                    else
                    {
                        if (curDiffNo < 1)
                        {
                            invItem.DiffNo = 1;
                            curDiffNo = 1;
                        }
                        else
                        {
                            invItem.DiffNo = curDiffNo++;
                        }
                    }


                    //MessageBox.Show(item.Quantity.ToString());
                    for (int i = 0; i < 1; i++)
                    {
                        invItem.Code = item.Code;
                        invItem.Quantity = item.Quantity;

                        if (chkDiffNoAuto.IsChecked == true)
                        {
                            invItem.CodeEx = invItem.Code + UString.AddZeroBefore(item.Quantity.ToString(), 5) + "." + UString.AddZeroBefore(invItem.DiffNo.ToString(), 7);
                        }
                        else
                        {
                            invItem.CodeEx = invItem.Code + UString.AddZeroBefore(item.Quantity.ToString(), 5) + "." + UString.AddZeroBefore(invItem.DiffNo.ToString(), 7);
                        }
                        //invItem.DiffNo = 
                        //invItem.CodeEx = invItem.Code + "." + UString.AddZeroBefore(invItem.DiffNo.ToString(), 8);
                        invItem.PriceIn = item.Price;
                        invItem.IdProductPurchase = lCurIdProductPurchase;
                        lstInv.Add(new InventoryInputItem() { Code = invItem.Code, DiffNo = invItem.DiffNo, CodeEx = invItem.CodeEx, Quantity = invItem.Quantity, IdProductPurchase = invItem.IdProductPurchase, PriceIn = invItem.PriceIn });

                    }

                }

                var list3 = lstInv.AsEnumerable().Select((InventoryInputItem, index) => new InventoryInputItem()
                {
                    RowNumber = index + 1,
                    Code = InventoryInputItem.Code,
                    DiffNo = InventoryInputItem.DiffNo,
                    CodeEx = InventoryInputItem.CodeEx,
                    PriceIn = InventoryInputItem.PriceIn,
                    Quantity = InventoryInputItem.Quantity,
                    IdProductPurchase = Convert.ToInt64(InventoryInputItem.IdProductPurchase)

                }).ToList();

                lsViewProductInventory.ItemsSource = null;
                lsViewProductInventory.ItemsSource = list3;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }            
        } //End funtion

        private void tbrSaveInventoryInput_Click(object sender, RoutedEventArgs e)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.IncludeLabel = false;
            b.Alignment = BarcodeLib.AlignmentPositions.CENTER;
            //b.RotateFlipType = RotateFlipType.Rotate180FlipNone;
            b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;

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
                    inventory.IdInventoryStorage = int.Parse(cmbStorage.SelectedValue.ToString());
                    inventory.Code = item.Code;
                    inventory.DiffNo = item.DiffNo;
                    inventory.CodeEx = item.CodeEx;
                    inventory.Barcode = null; // ImageToByte(b.Encode(BarcodeLib.TYPE.CODE128, "123", System.Drawing.Color.Black, System.Drawing.Color.White));
                    inventory.CreatedDate = long.Parse(UString.GetLongFromDate(dCurDate).ToString());
                    inventory.CreatedDateEx = dCurDate.ToShortDateString() + " " + dCurDate.ToShortTimeString();
                    inventory.IdInvoice = item.IdProductPurchase;
                    inventory.PriceIn = item.PriceIn;
                    //inventory.PriceOut = item.PriceOut;
                    dc.Inventories.InsertOnSubmit(inventory);
                    dc.SubmitChanges();
                }
                dc.ProcUpdateIsDone("ProductPurchase", idInvoice, true);
                Refresh_GUI("Purchase");
                Refresh_GUI("Product");

                lsViewProductPurchaseItem.ItemsSource = null;
                lsViewProductInventory.ItemsSource = null;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                        
        }

        private static T GetFrameworkElementByName<T>(FrameworkElement referenceElement) where T : FrameworkElement

        {

            FrameworkElement child = null;

            for (Int32 i = 0; i < VisualTreeHelper.GetChildrenCount(referenceElement); i++)

            {

                child = VisualTreeHelper.GetChild(referenceElement, i) as FrameworkElement;

                System.Diagnostics.Debug.WriteLine(child);

                if (child != null && child.GetType() == typeof(T))

                { break; }

                else if (child != null)

                {

                    child = GetFrameworkElementByName<T>(child);

                    if (child != null && child.GetType() == typeof(T))

                    {

                        break;

                    }

                }

            }

            return child as T;

        }
        private void chkDiffNoAuto_Checked(object sender, RoutedEventArgs e)
        {     

        }

        private void txtDiffNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            InventoryInputItem curItem = (sender as TextBox).DataContext as InventoryInputItem;
            var a = sender as TextBox;
            curItem.CodeEx = curItem.Code + "." + a.Text;
        }

        private void tbrFullScreen_Click(object sender, RoutedEventArgs e)
        {
            isFullScreen = !isFullScreen;

            if (isFullScreen == true)
            {
                grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

                txtFullScreen.Text = "";
                tbrFullScreen.ToolTip = "Thu nhỏ màn hình";
            }
            else
            {
                grdServiceAndProductSaleItem.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                grdServiceAndProductSaleItem.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
                txtFullScreen.Text = "";
                tbrFullScreen.ToolTip = "Mở rộng màn hình";
            }
        }

        private void tbrPrintAllLabel_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Hãy thiết lập máy in", "IMS - Thông báo lỗi");

            try
            {
                foreach (InventoryInputItem item in lsViewProductInventory.Items.Cast<InventoryInputItem>())
                {
                    string code = UString.Left(item.CodeEx, 7);

                    IMSDataContext dc = new IMSDataContext();
                    List<ProductView> lsProduct = (from product in dc.ProductViews
                                                   where product.Code == code
                                                   select product).ToList();

                    string unitName = lsProduct[0].UnitName;

                    Process p = new Process();
                    p.StartInfo.FileName = "PrintJobs.exe";
                    p.StartInfo.Arguments = "Label " + item.CodeEx + " " + UString.Mid(item.CodeEx, 7, 5) + unitName;
                    p.Start();  
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnPrintLable_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Hãy thiết lập máy in", "IMS - Thông báo lỗi");
            InventoryInputItem inventoryInputItem = (sender as Button).DataContext as InventoryInputItem;
            //IMSDataContext dc = new IMSDataContext();
            //var pr = (from s in dc.ProductViews
            //          where s.Code == inventoryInputItem.Code
            //          select s).First();

            //MessageBox.Show(pr.Code);
            //frmPrintLabel fprint = new frmPrintLabel(inventoryInputItem.CodeEx, pr.ProductKindName, pr.ProductColorName, pr.ProductSizeName, pr.ManufactureName);
            //fprint.Show();

            string code = UString.Left(inventoryInputItem.CodeEx, 7);

            IMSDataContext dc = new IMSDataContext();
            List<ProductView> lsProduct = (from product in dc.ProductViews
                                           where product.Code == code
                                           select product).ToList();

            string unitName = lsProduct[0].UnitName;

            Process p = new Process();
            p.StartInfo.FileName = "PrintJobs.exe";
            p.StartInfo.Arguments = "Label " + inventoryInputItem.CodeEx + " " + UString.Mid(inventoryInputItem.CodeEx, 7, 5) + unitName;
            p.Start();            
        }

        private void cmbInvoiceKind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
