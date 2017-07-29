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
    public partial class pgIOR : Window
    {
        int idProduct = -1;
        int idProductKind = -1;
        public pgIOR()
        {
            InitializeComponent();
            idProduct = -1;
            try
            {
                IMS_ReportDataContext dc = new IMS_ReportDataContext();
                List<View_IOR> lsProduct = (from product in dc.View_IORs
                                            select product).ToList();

                var list = lsProduct.AsEnumerable().Select((View_IOR, index) => new View_IOR()
                {
                    RowNumber = index + 1,                    
                    Code = View_IOR.Code,
                    ProductName = View_IOR.ProductName,
                    QuantityIn = View_IOR.QuantityIn,
                    QuantityOut = View_IOR.QuantityOut,
                    RollNoIn = View_IOR.RollNoIn,
                    RollNoOut = View_IOR.RollNoOut,
                    Available = View_IOR.Available,
                    AvailableRollNo = View_IOR.RollNoIn - View_IOR.RollNoOut

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
                IMS_ReportDataContext dc = new IMS_ReportDataContext();
                List<View_IOR> lsProduct = (from product in dc.View_IORs
                                            select product).ToList();

                var list = lsProduct.AsEnumerable().Select((View_IOR, index) => new View_IOR()
                {
                    RowNumber = index + 1,
                    Code = View_IOR.Code,
                    ProductName = View_IOR.ProductName,
                    QuantityIn = View_IOR.QuantityIn,
                    QuantityOut = View_IOR.QuantityOut,
                    RollNoIn = View_IOR.RollNoIn,
                    RollNoOut = View_IOR.RollNoOut,
                    Available = View_IOR.Available

                }).ToList();

                myListView.ItemsSource = null;
                myListView.ItemsSource = list;



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
                IMS_ReportDataContext dc = new IMS_ReportDataContext();
                List<View_IOR> lsProduct = (from product in dc.View_IORs
                                            select product).ToList();

                var list = lsProduct.AsEnumerable().Select((View_IOR, index) => new View_IOR()
                {
                    RowNumber = index + 1,
                    Code = View_IOR.Code,
                    ProductName = View_IOR.ProductName,
                    QuantityIn = View_IOR.QuantityIn,
                    QuantityOut = View_IOR.QuantityOut,
                    RollNoIn = View_IOR.RollNoIn,
                    RollNoOut = View_IOR.RollNoOut,
                    Available = View_IOR.Available

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
                IMS_ReportDataContext dc = new IMS_ReportDataContext();
                List<View_IOR> lsProduct = (from product in dc.View_IORs
                                            select product).ToList();

                var list = lsProduct.AsEnumerable().Select((View_IOR, index) => new View_IOR()
                {
                    RowNumber = index + 1,
                    Code = View_IOR.Code,
                    ProductName = View_IOR.ProductName,
                    QuantityIn = View_IOR.QuantityIn,
                    QuantityOut = View_IOR.QuantityOut,
                    RollNoIn = View_IOR.RollNoIn,
                    RollNoOut = View_IOR.RollNoOut,
                    Available = View_IOR.Available

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
