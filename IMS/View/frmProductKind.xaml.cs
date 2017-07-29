using IMS.DBHelper;
using IMS.Favorite;
using IMS.General;
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
using System.Windows.Shapes;

namespace IMS.View
{
    /// <summary>
    /// Interaction logic for frmProductKind.xaml
    /// </summary>
    public partial class frmProductKind : Window
    {
        int idProductKind;
        frmProduct frmProduct_ = null;
        frmProduct1 frmProduct1_ = null;
        pgProduct pgProduct_ = null;
        public frmProductKind(pgProduct _frmProduct)
        {
            try
            {
                InitializeComponent();
                pgProduct_ = _frmProduct;

                //Thiết lập định dạng VN
                UString.SetSystem();

                grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                //Khởi tạo DataContext
                IMS_TableDataContext dc = new IMS_TableDataContext();

                

                //Lấy dữ liệu từ ProductKindView                
                List<ProductKind> ls = (from s in dc.ProductKinds
                                        select s).ToList();

                var list = ls.AsEnumerable().Select((ProductKind, index) => new ProductKind()
                {
                    RowNumber = index + 1,
                    Id = ProductKind.Id,
                    Name = ProductKind.Name,
                    Code = ProductKind.Code,
                    IsColorDiff = ProductKind.IsColorDiff

                }).ToList();

                lsViewProductKind.ItemsSource = null;
                lsViewProductKind.ItemsSource = list;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }

        }

        public frmProductKind(frmProduct1 _frmProduct)
        {
            try
            {
                InitializeComponent();
                frmProduct1_ = _frmProduct;

                //Thiết lập định dạng VN
                UString.SetSystem();

                grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                //Khởi tạo DataContext
                IMS_TableDataContext dc = new IMS_TableDataContext();



                //Lấy dữ liệu từ ProductKindView                
                List<ProductKind> ls = (from s in dc.ProductKinds
                                        select s).ToList();

                var list = ls.AsEnumerable().Select((ProductKind, index) => new ProductKind()
                {
                    RowNumber = index + 1,
                    Id = ProductKind.Id,
                    Name = ProductKind.Name,
                    Code = ProductKind.Code,
                    IsColorDiff = ProductKind.IsColorDiff
                }).ToList();

                lsViewProductKind.ItemsSource = null;
                lsViewProductKind.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }

        }


        public frmProductKind(frmProduct _frmProduct)
        {
            try
            {
                InitializeComponent();
                frmProduct_ = _frmProduct;

                //Thiết lập định dạng VN
                UString.SetSystem();

                grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                //Khởi tạo DataContext
                IMS_TableDataContext dc = new IMS_TableDataContext();



                //Lấy dữ liệu từ ProductKindView                
                List<ProductKind> ls = (from s in dc.ProductKinds
                                        select s).ToList();

                var list = ls.AsEnumerable().Select((ProductKind, index) => new ProductKind()
                {
                    RowNumber = index + 1,
                    Id = ProductKind.Id,
                    Name = ProductKind.Name,
                    Code = ProductKind.Code,
                    IsColorDiff = ProductKind.IsColorDiff

                }).ToList();

                lsViewProductKind.ItemsSource = null;
                lsViewProductKind.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IMS_TableDataContext dc = new IMS_TableDataContext();
                if (idProductKind == -1)
                {
                    try
                    {
                        var ProductKindAdd1 = (from s in dc.ProductKinds
                                        where (s.Name == txtName.Text)
                                           select s).First();
                        if (ProductKindAdd1 != null)
                        {
                            MessageBox.Show("Nhóm hàng  đã tồn tại!", "IMS - Thông báo lỗi");
                            return;
                        }
                    }
                    catch
                    {
                        ;
                    }

                    ProductKind ProductKindAdd = new  ProductKind();
                    ProductKindAdd.Name = txtName.Text;
                    ProductKindAdd.Code = txtCode.Text;
                    ProductKindAdd.IsColorDiff = chkColorDiff.IsChecked;
                    dc.ProductKinds.InsertOnSubmit(ProductKindAdd);
                    dc.SubmitChanges();

                    grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                    this.Title = "IMS - Nhóm hàng ";
                }
                else
                {                    
                    ProductKind ProductKindUpdate = null;
                    ProductKindUpdate = (from s in dc.ProductKinds
                                      where (s.Id == idProductKind)
                                     select s).First();

                    if (ProductKindUpdate != null)
                    {
                        ProductKindUpdate.Name = txtName.Text;
                        ProductKindUpdate.Code = txtCode.Text;
                        ProductKindUpdate.IsColorDiff = chkColorDiff.IsChecked;
                        dc.SubmitChanges();

                        grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                        this.Title = "IMS - Nhóm hàng ";
                    }
                }               

                lsViewProductKind.IsEnabled = true;
                //Lấy dữ liệu từ ProductKindView                
                List<ProductKind> ls = (from s in dc.ProductKinds
                                     select s).ToList();

                var list = ls.AsEnumerable().Select((ProductKind, index) => new ProductKind()
                {
                    RowNumber = index + 1,
                    Id = ProductKind.Id,
                    Name = ProductKind.Name,
                    Code = ProductKind.Code,
                    IsColorDiff = ProductKind.IsColorDiff
                }).ToList();

                lsViewProductKind.ItemsSource = null;
                lsViewProductKind.ItemsSource = list;

                //frmProduct_.Refresh_GUI("ProductKind");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "IMS - Nhóm hàng ";
            grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
            grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
            lsViewProductKind.IsEnabled = true;
        }

        private void tbrAddSProductKind_Click(object sender, RoutedEventArgs e)
        {
            idProductKind = -1;
            this.Title = "IMS - Thêm nhóm hàng ";

            grdAll.RowDefinitions[0].Height = new GridLength(150, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            lsViewProductKind.IsEnabled = false;

            txtName.Focus();
            txtName.SelectAll();
        }

        private void tbrEditProductKind_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "IMS - Sửa chữa nhóm hàng ";

            grdAll.RowDefinitions[0].Height = new GridLength(150, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            lsViewProductKind.IsEnabled = false;

            //Lấy dữ liệu từ idProductKind
            IMS_TableDataContext dc = new IMS_TableDataContext();
            if (idProductKind != -1)
            {
                var queryProductKind = (from s in dc.ProductKinds
                                 where (s.Id == idProductKind)
                                 select s).First();

                if (queryProductKind != null)
                {
                    txtName.Text = queryProductKind.Name;
                    txtCode.Text = queryProductKind.Code;
                    chkColorDiff.IsChecked = queryProductKind.IsColorDiff;
                }
            }
            txtName.Focus();
            txtName.SelectAll();
        }

        private void lsViewProductKind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var userView = e.AddedItems[0] as ProductKind;
                idProductKind = userView.Id;
            }
            catch
            {
                ;
            }
        }

        private void btnRemoveProductKind_Click(object sender, RoutedEventArgs e)
        {
            GeneralFuctions.DeleteTable("ProductKind", idProductKind);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (frmProduct1_ != null)
            {
                frmProduct1_.Refresh_GUI("ProductKind");
            }
            else if(pgProduct_ != null)
            {
                pgProduct_.RefreshGUI();
            }
        }

        private void tbrAddProductKindFromFile_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "Excel Files *.xls|*.xlsx" };
            var result = ofd.ShowDialog();
            if (result == false) return;
            string file = ofd.FileName;
            MyProgressBar pb = new MyProgressBar();
            pb.Show();
            try
            {
                IMS_TableDataContext dcTable = new IMS_TableDataContext();
                IMS_ProcDataContext dcProc = new IMS_ProcDataContext();
                DataTable productKindData = SqlDataConnection.ReadExcelContents(file);
                for (int i = 0; i < productKindData.Rows.Count; i++)                
                {
                    DataRow row = productKindData.Rows[i];
                    ProductKind producKind= new ProductKind();
                    producKind.Name = row[0].ToString();
                    producKind.Code = row[1].ToString();
                    if (row[2].ToString() == "C")
                    {
                        producKind.IsColorDiff = true;
                    }
                    else
                    {
                        producKind.IsColorDiff = false;
                    }
                    
                    dcProc.ProcInsertProductKind(producKind.Name, producKind.Code, producKind.IsColorDiff);
                }

                //Lấy dữ liệu từ ProductKindView                
                List<ProductKind> ls = (from s in dcTable.ProductKinds
                                        select s).ToList();

                var list = ls.AsEnumerable().Select((ProductKind, index) => new ProductKind()
                {
                    RowNumber = index + 1,
                    Id = ProductKind.Id,
                    Name = ProductKind.Name,
                    Code = ProductKind.Code,
                    IsColorDiff = ProductKind.IsColorDiff

                }).ToList();

                lsViewProductKind.ItemsSource = null;
                lsViewProductKind.ItemsSource = list;
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (frmProduct1_ != null)
            {
                frmProduct1_.Refresh_GUI("ProductKind");
            }
            else if (pgProduct_ != null)
            {
                pgProduct_.RefreshGUI();
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ProductKind productKind = (sender as CheckBox).DataContext as ProductKind;
                idProductKind = productKind.Id;
                IMS_TableDataContext dc = new IMS_TableDataContext();                
                
                ProductKind ProductKindUpdate = null;
                ProductKindUpdate = (from s in dc.ProductKinds
                                        where (s.Id == idProductKind)
                                        select s).First();

                if (ProductKindUpdate != null)
                {
                    //ProductKindUpdate.Name = txtName.Text;
                    //ProductKindUpdate.Code = txtCode.Text;
                    ProductKindUpdate.IsColorDiff = productKind.IsColorDiff;
                    dc.SubmitChanges();

                    grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                    this.Title = "IMS - Nhóm hàng ";
                }                

                lsViewProductKind.IsEnabled = true;
                //Lấy dữ liệu từ ProductKindView                
                List<ProductKind> ls = (from s in dc.ProductKinds
                                        select s).ToList();

                var list = ls.AsEnumerable().Select((ProductKind, index) => new ProductKind()
                {
                    RowNumber = index + 1,
                    Id = ProductKind.Id,
                    Name = ProductKind.Name,
                    Code = ProductKind.Code,
                    IsColorDiff = ProductKind.IsColorDiff
                }).ToList();

                lsViewProductKind.ItemsSource = null;
                lsViewProductKind.ItemsSource = list;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }
        }
    }
}
