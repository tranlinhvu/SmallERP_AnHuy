using IMS;
using IMS.Favorite;
using IMS.General;
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
    public partial class frmBOM : Window
    {
      
        double h1;
        double h2;
        long lCurIdProductSale = -1;

        List<string> curProductInventory = new List<string>();
        
        int idProduct = 0;
        //int idMainProduct;
        public frmBOM(int _idProduct)
        {
            InitializeComponent();
            idProduct = _idProduct;
            IMS_ViewDataContext dc = new IMS_ViewDataContext();           

            //Fill thông tin sản phẩm chính
            Product1 product = null;
            product = (from pro in dc.Product1s
                       where (pro.Id == idProduct)
                       select pro).First();

            txtProductCode.Text = product.Code;
            txtProductName.Text = product.Name;

            //Load danh sách sản phẩm
            SearchProduct();

            //Load BOM
            LoadBOM(idProduct); 
        }

        void SearchProduct()
        {
            //Load danh sách sản phẩm
            IMS_ViewDataContext dc1 = new IMS_ViewDataContext();
            List<ProductView1> lsProduct = (from product1 in dc1.ProductView1s
                                            where product1.Id != idProduct && (product1.Code.Contains(txtSearchProduct.Text) || product1.Name.Contains(txtSearchProduct.Text))
                                            select product1).ToList();

            var list = lsProduct.AsEnumerable().Select((ProductView1, index) => new ProductView1()
            {
                RowNumber = index + 1,
                Id = ProductView1.Id,
                Code = ProductView1.Code,
                Name = ProductView1.Name,
                RefNo = ProductView1.RefNo,
                RefNoLenth = ProductView1.RefNoLenth,
                ProductKindName = ProductView1.ProductKindName,
                ProductColorName = ProductView1.ProductColorName,
                PriceIn = ProductView1.PriceIn,
                PriceOut = ProductView1.PriceOut,
                UnitName = ProductView1.UnitName,
                GroupName = ProductView1.GroupName,
                ManufactureName = ProductView1.ManufactureName,
                ExpiryRule = ProductView1.ExpiryRule,
                LottNoRule = ProductView1.LottNoRule,
                StockLimit = ProductView1.StockLimit,
                IsBOM = ProductView1.IsBOM
            }).ToList();

            lsViewProduct.ItemsSource = null;
            lsViewProduct.ItemsSource = list;
        }
        void LoadBOM(int idProductMain_)
        {
            //Load danh sách các thành phần trong BOM gồm cả tp/btp
            IMSTableDataContext dcTable = new IMSTableDataContext();
            List<View_BOM_No_Extract> lsProductBOM = (from productBOM in dcTable.View_BOM_No_Extracts
                                                      where productBOM.IdProductMain == idProductMain_
                                                      select productBOM).ToList();

            var list = lsProductBOM.AsEnumerable().Select((View_BOM_No_Extract, index) => new View_BOM_No_Extract()
            {
                RowNumber = index + 1,
                Id = (int)View_BOM_No_Extract.Id,
                IdProductMain = (int)View_BOM_No_Extract.IdProductMain,
                IdProductSub = (int)View_BOM_No_Extract.IdProductSub,
                Code = View_BOM_No_Extract.Code,
                Name = View_BOM_No_Extract.Name,
                UnitName = View_BOM_No_Extract.UnitName,
                Quantity = View_BOM_No_Extract.Quantity,
                IsBOM = View_BOM_No_Extract.IsBOM
            }).ToList();

            lsViewProductBOMNoExtract.ItemsSource = null;
            lsViewProductBOMNoExtract.ItemsSource = list;

            //Load danh sách các thành phần trong BOM không gồm tp/btp          
            List<View_BOM_Extract> lsProductBOMExtract = (from productBOMExtract in dcTable.View_BOM_Extracts
                                                          where productBOMExtract.IdEx == idProductMain_
                                                          select productBOMExtract).ToList();

            var list1 = lsProductBOMExtract.AsEnumerable().Select((View_BOM_Extract, index) => new View_BOM_Extract()
            {
                RowNumber = index + 1,
                Id = (int)View_BOM_Extract.Id,
                Code = View_BOM_Extract.Code,
                Name = View_BOM_Extract.Name,
                UnitName = View_BOM_Extract.UnitName,
                Quantity = View_BOM_Extract.Quantity,
                IsBOM = View_BOM_Extract.IsBOM
            }).ToList();

            lsViewProductBOMExtract.ItemsSource = null;
            lsViewProductBOMExtract.ItemsSource = list1;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            IMSTableDataContext dc = new IMSTableDataContext();

            View_BOM_No_Extract productBOMNoExtract = (sender as TextBox).DataContext as View_BOM_No_Extract;
            long idProductBOM = productBOMNoExtract.Id;

            BOM bom = null;
            bom = (from s in dc.BOMs
                    where (s.Id == idProductBOM)
                    select s).First();

            bom.Quantity = productBOMNoExtract.Quantity;
            dc.SubmitChanges();
        }

        private void btnAddProduct2BOM_Click(object sender, RoutedEventArgs e)
        {
            ProductView1 product = (sender as Button).DataContext as ProductView1;

            IMSTableDataContext dc = new IMSTableDataContext();
            BOM bom = new BOM();
            bom.IdEx = GeneralParams.idProductMainBOM;
            bom.IdProductMain = idProduct;
            bom.IdProductSub = product.Id;
            bom.Quantity = 1;
            dc.BOMs.InsertOnSubmit(bom);
            dc.SubmitChanges();

            LoadBOM(idProduct);
        }

        private void lsViewProductBOMNoExtract_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //ProductView1 product = (sender as ListViewItem).DataContext as ProductView1;

                View_BOM_No_Extract bomNoExtract = lsViewProductBOMNoExtract.SelectedItem as View_BOM_No_Extract;
                if (bomNoExtract.IsBOM == true)
                {
                    frmBOM frmBOM_ = new frmBOM((int)bomNoExtract.IdProductSub);
                    frmBOM_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    //frmBOM_.WindowState = WindowState.Maximized;
                    frmBOM_.ShowDialog();
                }


            }
            catch
            {
                ;
            }
        }        
    }
}
