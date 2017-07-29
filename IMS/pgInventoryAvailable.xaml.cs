using IMS.Favorite;
using IMS.Model;
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
    /// Interaction logic for pgProduct.xaml
    /// </summary>
    public partial class pgInventoryAvailable : Page
    {
        int idProduct = -1;
        int idProductKind = -1;
        public pgInventoryAvailable()
        {
            InitializeComponent();
            idProduct = -1;
            try
            {
                IMSDataContext dc = new IMSDataContext();
                IMS_TableDataContext dcTable = new IMS_TableDataContext();
                List<InventoryView> lsProduct = (from product in dc.InventoryViews
                                                 select product).ToList();

                var list = lsProduct.AsEnumerable().Select((InventoryView, index) => new InventoryView()
                {
                    RowNumber = index + 1,
                    Id = InventoryView.Id,
                    Code = InventoryView.Code,
                    Name = InventoryView.Name,
                    ProductKindName = InventoryView.ProductKindName,
                    ProductColorName = InventoryView.ProductColorName,
                    DiffNo = InventoryView.DiffNo,
                    CodeEx = InventoryView.CodeEx,
                    CreatedDateEx = InventoryView.CreatedDateEx,
                    PriceIn = InventoryView.PriceIn,
                    UnitName = InventoryView.UnitName,
                    StorageName = InventoryView.StorageName,
                    InvoiceNo = InventoryView.InvoiceNo

                }).ToList();

                myListView.ItemsSource = null;
                myListView.ItemsSource = list;

                //---ProductKind---
                List<ProductKind> kindList = (from s in dcTable.ProductKinds select s).ToList();
                ProductKind a = new ProductKind();
                a.Id = 0;
                a.Name = "Tất cả";
                kindList.Add(a);

                var varKindList = kindList.OrderBy(x => x.Id);
                cmbProductKind.ItemsSource = varKindList;
                cmbProductKind.DisplayMemberPath = "Name";
                cmbProductKind.SelectedValuePath = "Id";
                cmbProductKind.SelectedValue = 0;               

               
                //---Color---  
                idProductKind = 0;
                if (idProductKind == 0)
                {
                    List<ProductKindColorView> colorList = (from s in dc.ProductKindColorViews select s).OrderBy(x => x.ProductColorCode).ToList();
                    ProductKindColorView a1 = new ProductKindColorView();
                    a1.IdProductColor = 0;
                    a1.ProductColorName = "Tất cả";
                    a1.ProductColorCode = "0";
                    colorList.Add(a1);
                    var varColorList = colorList.OrderBy(x => x.ProductColorCode);
                    cmbProductColor.ItemsSource = varColorList;
                    cmbProductColor.DisplayMemberPath = "ProductColorName";
                    cmbProductColor.SelectedValuePath = "IdProductColor";
                    cmbProductColor.SelectedValue = 0;
                }
                else
                {
                    List<ProductKindColorView> colorList = (from s in dc.ProductKindColorViews where s.IdProductKind == idProductKind select s).OrderBy(x => x.ProductColorCode).ToList();
                    ProductKindColorView a1 = new ProductKindColorView();
                    a1.IdProductColor = 0;
                    a1.ProductColorName = "Tất cả";
                    a1.ProductColorCode = "0";
                    colorList.Add(a1);
                    var varColorList = colorList.OrderBy(x => x.ProductColorCode);
                    cmbProductColor.ItemsSource = varColorList;                    
                    cmbProductColor.DisplayMemberPath = "ProductColorName";
                    cmbProductColor.SelectedValuePath = "IdProductColor";
                    cmbProductColor.SelectedValue = 0;
                }             

            }
            catch
            {
                ;
            }
        }

        private void tbrAdd_Click(object sender, RoutedEventArgs e)
        {
            frmProduct frmProduct_ = new frmProduct(-1, null);
            frmProduct_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProduct_.ShowDialog();
        }

        private void tbrEdit_Click(object sender, RoutedEventArgs e)
        {
            frmProduct frmProduct_ = new frmProduct(idProduct, null);
            frmProduct_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProduct_.ShowDialog();
        }

        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                var product = e.AddedItems[0] as ProductView;
                idProduct = product.Id;
                idProductKind = product.IdProductKind.Value;                
            }
            catch
            {
                ;
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Page_Refresh(Product prt)
        {
            try
            {
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

                myListView.ItemsSource = null;
                myListView.ItemsSource = list;
                myListView.SelectedItem = prt;

            }
            catch
            {
                ;
            }
        }

        private void myListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {

                frmProduct frmProduct_ = new frmProduct(idProduct, null);
                frmProduct_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                frmProduct_.ShowDialog();
            }
            catch
            {
                ;
            }
        }

        private void btnPrintLable_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Hãy thiết lập máy in", "IMS - Thông báo lỗi");
            InventoryView inventoryInputItem = (sender as Button).DataContext as InventoryView;
            //IMSDataContext dc = new IMSDataContext();
            //var pr = (from s in dc.ProductViews
            //          where s.Code == inventoryInputItem.Code
            //          select s).First();

            //MessageBox.Show(pr.Code);
            //frmPrintLabel fprint = new frmPrintLabel(inventoryInputItem.CodeEx, pr.ProductKindName, pr.ProductColorName, pr.ProductSizeName, pr.ManufactureName);
            //fprint.Show();

            string code = inventoryInputItem.Code;

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

        private void tbrRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                List<InventoryView> lsProduct = (from product in dc.InventoryViews
                                                 select product).ToList();

                var list = lsProduct.AsEnumerable().Select((InventoryView, index) => new InventoryView()
                {
                    RowNumber = index + 1,
                    Id = InventoryView.Id,
                    Code = InventoryView.Code,
                    Name = InventoryView.Name,
                    ProductKindName = InventoryView.ProductKindName,
                    ProductColorName = InventoryView.ProductColorName,
                    DiffNo = InventoryView.DiffNo,
                    CodeEx = InventoryView.CodeEx,
                    CreatedDateEx = InventoryView.CreatedDateEx,
                    PriceIn = InventoryView.PriceIn,
                    UnitName = InventoryView.UnitName,
                    StorageName = InventoryView.StorageName,
                    InvoiceNo = InventoryView.InvoiceNo

                }).ToList();

                myListView.ItemsSource = null;
                myListView.ItemsSource = list;

            }
            catch
            {
                ;
            }
        }

        private void tbrSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string productKindName = (cmbProductKind.SelectedValue.ToString() == "0") ? "" : cmbProductKind.Text;
                string productColorName = (cmbProductColor.SelectedValue.ToString() == "0") ? "" : UString.Left(cmbProductColor.Text, cmbProductColor.Text.Length - 4);

                IMSDataContext dc = new IMSDataContext();
                List<InventoryView> lsProduct = (from product in dc.InventoryViews
                                                 where product.ProductKindName.Contains(productKindName) && product.ProductColorName.Contains(productColorName)
                                                 select product).ToList();

                var list = lsProduct.AsEnumerable().Select((InventoryView, index) => new InventoryView()
                {
                    RowNumber = index + 1,
                    Id = InventoryView.Id,
                    Code = InventoryView.Code,
                    Name = InventoryView.Name,
                    ProductKindName = InventoryView.ProductKindName,
                    ProductColorName = InventoryView.ProductColorName,
                    DiffNo = InventoryView.DiffNo,
                    CodeEx = InventoryView.CodeEx,
                    CreatedDateEx = InventoryView.CreatedDateEx,
                    PriceIn = InventoryView.PriceIn,
                    UnitName = InventoryView.UnitName,
                    StorageName = InventoryView.StorageName,
                    InvoiceNo = InventoryView.InvoiceNo

                }).ToList();

                myListView.ItemsSource = null;
                myListView.ItemsSource = list;

            }
            catch
            {
                ;
            }
        }

        private void cmbProductKind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                //---Color---             
                idProductKind = int.Parse(cmbProductKind.SelectedValue.ToString());
                if (idProductKind == 0)
                {
                    List<ProductKindColorView> colorList = (from s in dc.ProductKindColorViews select s).OrderBy(x => x.ProductColorCode).ToList();
                    ProductKindColorView a1 = new ProductKindColorView();
                    a1.IdProductColor = 0;
                    a1.ProductColorName = "Tất cả";
                    a1.ProductColorCode = "@";
                    colorList.Add(a1);
                    var varColorList = colorList.OrderBy(x => x.ProductColorCode);
                    cmbProductColor.ItemsSource = varColorList;
                    cmbProductColor.DisplayMemberPath = "ProductColorName";
                    cmbProductColor.SelectedValuePath = "IdProductColor";
                    cmbProductColor.SelectedValue = 0;
                }
                else
                {
                    List<ProductKindColorView> colorList = (from s in dc.ProductKindColorViews where s.IdProductKind == idProductKind select s).OrderBy(x => x.ProductColorCode).ToList();
                    ProductKindColorView a1 = new ProductKindColorView();
                    a1.IdProductColor = 0;
                    a1.ProductColorName = "Tất cả";
                    a1.ProductColorCode = "@";
                    colorList.Add(a1);
                    var varColorList = colorList.OrderBy(x => x.ProductColorCode);
                    cmbProductColor.ItemsSource = varColorList;
                    cmbProductColor.DisplayMemberPath = "ProductColorName";
                    cmbProductColor.SelectedValuePath = "IdProductColor";
                    cmbProductColor.SelectedValue = 0;
                }             
            }
            catch
            {
                ;
            }
        }

        private void btnSaveEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
