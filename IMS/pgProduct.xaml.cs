using IMS.Favorite;
using IMS.General;
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
    public partial class pgProduct : Page
    {
        int idProduct = -1;
        int idProductKind = -1;
        public pgProduct()
        {
            InitializeComponent();
            idProduct = -1;
            int idProductKind = -1;
            try
            {
                IMSDataContext dc = new IMSDataContext();
                IMS_TableDataContext dcTable = new IMS_TableDataContext();
                List<ProductView> lsProduct = (from product in dc.ProductViews
                                               select product).ToList();

                var list = lsProduct.AsEnumerable().Select((ProductView, index) => new ProductView()
                {
                    RowNumber = index + 1,
                    Id = ProductView.Id,
                    Code = ProductView.Code,
                    Name = ProductView.Name,     
                    ProductKindName = ProductView.ProductKindName,
                    ProductColorName = ProductView.ProductColorName,
                    PriceIn = ProductView.PriceIn,
                    PriceOut = ProductView.PriceOut,
                    UnitName = ProductView.UnitName,
                    GroupName = ProductView.GroupName,
                    ManufactureName = ProductView.ManufactureName

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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbrAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frmProduct1 frmProduct_ = new frmProduct1(-1, this);
                frmProduct_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                frmProduct_.ShowDialog();
            }
            catch(Exception ex)
            {
                Debug.Print(ex.Message);
            }

        }

        private void tbrEdit_Click(object sender, RoutedEventArgs e)
        {
            frmProduct1 frmProduct_ = new frmProduct1(idProduct, this);
            frmProduct_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProduct_.ShowDialog();
        }

        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                var product = e.AddedItems[0] as ProductView;
                idProduct = product.Id;
            }
            catch
            {
                ;
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            GeneralFuctions.DeleteTable("Product", idProduct);
            Page_Refresh(null);
        }

        public void RefreshGUI()
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                IMS_TableDataContext dcTable = new IMS_TableDataContext();
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

        public void RefreshGUI1()
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
                    ProductKindName = ProductView.ProductKindName,
                    ProductColorName = ProductView.ProductColorName,
                    PriceIn = ProductView.PriceIn,
                    PriceOut = ProductView.PriceOut,
                    UnitName = ProductView.UnitName,
                    GroupName = ProductView.GroupName,
                    ManufactureName = ProductView.ManufactureName

                }).ToList();

                myListView.ItemsSource = null;
                myListView.ItemsSource = list;

                IMS_TableDataContext dcTable = new IMS_TableDataContext();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void myListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {

                frmProduct1 frmProduct1_ = new frmProduct1(idProduct, this);
                frmProduct1_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                frmProduct1_.ShowDialog();
            }
            catch
            {
                ;
            }
        }

        private void tbrAutoGenerate_Click(object sender, RoutedEventArgs e)
        {
            IMSDataContext dc = new IMSDataContext();
            dc.ProcGenerateProducts();
            Page_Refresh(null);
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

        private void tbrSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string productKindName = (cmbProductKind.SelectedValue.ToString() == "0") ? "" : cmbProductKind.Text;
                string productColorName = (cmbProductColor.SelectedValue.ToString() == "0") ? "" : UString.Left(cmbProductColor.Text, cmbProductColor.Text.Length - 4);
                IMSDataContext dc = new IMSDataContext();
                List<ProductView> lsProduct = (from product in dc.ProductViews
                                               where product.ProductKindName.Contains(productKindName) && product.ProductColorName.Contains(productColorName)
                                               select product).ToList();

                var list = lsProduct.AsEnumerable().Select((ProductView, index) => new ProductView()
                {
                    RowNumber = index + 1,
                    Id = ProductView.Id,
                    Code = ProductView.Code,
                    Name = ProductView.Name,
                    ProductKindName = ProductView.ProductKindName,
                    ProductColorName = ProductView.ProductColorName,
                    PriceIn = ProductView.PriceIn,
                    PriceOut = ProductView.PriceOut,
                    UnitName = ProductView.UnitName,
                    GroupName = ProductView.GroupName,
                    ManufactureName = ProductView.ManufactureName

                }).ToList();

                myListView.ItemsSource = null;
                myListView.ItemsSource = list;
            }
            catch
            {
                ;
            }
        }

        private void tbrRefresh_Click(object sender, RoutedEventArgs e)
        {
            Page_Refresh(null);
        }

        private void tbrEditProductKind_Click(object sender, RoutedEventArgs e)
        {
            frmProductKind frmProductKind_ = new frmProductKind(this);
            //frmProductKind_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductKind_.ShowDialog();
        }

        private void tbrEditProductColor_Click(object sender, RoutedEventArgs e)
        {
            frmProductColor frmProductColor_ = new frmProductColor(this, idProductKind);
            //frmProductKind_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductColor_.ShowDialog();
        }
    }
}
