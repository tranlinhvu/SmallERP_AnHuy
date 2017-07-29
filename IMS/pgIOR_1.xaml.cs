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
    public partial class pgIOR_1 : Window
    {
        int idProduct = -1;
        int idProductKind = -1;
        public pgIOR_1()
        {
            InitializeComponent();
            idProduct = -1;
            try
            {
                IMS_ReportDataContext dc = new IMS_ReportDataContext();
                List<View_IOR_Kind> lsProduct = (from product in dc.View_IOR_Kinds
                                                 select product).ToList();

                var list = lsProduct.AsEnumerable().Select((View_IOR_Kind, index) => new View_IOR_Kind()
                {
                    RowNumber = index + 1,
                    Code = View_IOR_Kind.Code,
                    KindName = View_IOR_Kind.KindName,
                    QuantityIn = View_IOR_Kind.QuantityIn,
                    QuantityOut = View_IOR_Kind.QuantityOut,
                    RollNoIn = View_IOR_Kind.RollNoIn,
                    RollNoOut = View_IOR_Kind.RollNoOut,
                    Available = View_IOR_Kind.Available,
                    AvailableRollNo = View_IOR_Kind.RollNoIn - View_IOR_Kind.RollNoOut

                }).ToList();

                myListView.ItemsSource = null;
                myListView.ItemsSource = list;



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
                IMS_ViewDataContext dc = new IMS_ViewDataContext();
                List<InventoryView1> lsProduct = (from product in dc.InventoryView1s
                                                 select product).ToList();

                var list = lsProduct.AsEnumerable().Select((InventoryView1, index) => new InventoryView1()
                {
                    RowNumber = index + 1,
                    Id = InventoryView1.Id,
                    Code = InventoryView1.Code,
                    Name = InventoryView1.Name,
                    KindName = InventoryView1.KindName,
                    DiffNo = InventoryView1.DiffNo,
                    CodeEx = InventoryView1.CodeEx,
                    ExpiryEx = InventoryView1.ExpiryEx,
                    LottNo = InventoryView1.LottNo,
                    CreatedDateEx = InventoryView1.CreatedDateEx,
                    PriceIn = InventoryView1.PriceIn,
                    StorageName = InventoryView1.StorageName

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
                //string productKindName = (cmbProductKind.SelectedValue.ToString() == "0") ? "" : cmbProductKind.Text;
                ////string productColorName = (cmbProductColor.SelectedValue.ToString() == "0") ? "" : UString.Left(cmbProductColor.Text, cmbProductColor.Text.Length - 4);

                //IMS_ViewDataContext dc = new IMS_ViewDataContext();
                //List<InventoryView1> lsProduct = (from product in dc.InventoryView1s
                //                                 where product.KindName.Contains(productKindName)
                //                                 select product).ToList();

                //var list = lsProduct.AsEnumerable().Select((InventoryView1, index) => new InventoryView1()
                //{
                //    RowNumber = index + 1,
                //    Id = InventoryView1.Id,
                //    Code = InventoryView1.Code,
                //    Name = InventoryView1.Name,
                //    KindName = InventoryView1.KindName,
                //    DiffNo = InventoryView1.DiffNo,
                //    CodeEx = InventoryView1.CodeEx,
                //    ExpiryEx = InventoryView1.ExpiryEx,
                //    LottNo = InventoryView1.LottNo,
                //    CreatedDateEx = InventoryView1.CreatedDateEx,
                //    PriceIn = InventoryView1.PriceIn,
                //    StorageName = InventoryView1.StorageName

                //}).ToList();

                //myListView.ItemsSource = null;
                //myListView.ItemsSource = list;

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
                ;
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
