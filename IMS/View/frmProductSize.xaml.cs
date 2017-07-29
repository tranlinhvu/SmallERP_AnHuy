using IMS.Favorite;
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
using System.Windows.Shapes;

namespace IMS.View
{
    /// <summary>
    /// Interaction logic for frmProductSize.xaml
    /// </summary>
    public partial class frmProductSize : Window
    {
        long idProductSize;
        frmProduct1 frmProduct_ = null;
        public frmProductSize(frmProduct1 _frmProduct)
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
                IMSDataContext dc = new IMSDataContext();

                

                //Lấy dữ liệu từ ProductSizeView                
                List<ProductSize> ls = (from s in dc.ProductSizes
                                        select s).ToList();

                var list = ls.AsEnumerable().Select((ProductSize, index) => new ProductSize()
                {
                    RowNumber = index + 1,
                    Id = ProductSize.Id,
                    Lenth = ProductSize.Lenth,
                    Code = ProductSize.Code

                }).ToList();

                lsViewProductSize.ItemsSource = null;
                lsViewProductSize.ItemsSource = list;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                if (idProductSize == -1)
                {
                    try
                    {
                        var ProductSizeAdd1 = (from s in dc.ProductSizes
                                        where (s.Lenth == txtName.Text)
                                           select s).First();
                        if (ProductSizeAdd1 != null)
                        {
                            MessageBox.Show("Nhóm hàng  đã tồn tại!", "IMS - Thông báo lỗi");
                            return;
                        }
                    }
                    catch
                    {
                        ;
                    }

                    ProductSize ProductSizeAdd = new  ProductSize();
                    ProductSizeAdd.Lenth = txtName.Text;
                    ProductSizeAdd.Code = txtCode.Text;
                    dc.ProductSizes.InsertOnSubmit(ProductSizeAdd);
                    dc.SubmitChanges();

                    grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                    this.Title = "IMS - Nhóm hàng ";
                }
                else
                {                    
                    ProductSize ProductSizeUpdate = null;
                    ProductSizeUpdate = (from s in dc.ProductSizes
                                      where (s.Id == idProductSize)
                                     select s).First();

                    if (ProductSizeUpdate != null)
                    {
                        ProductSizeUpdate.Lenth = txtName.Text;
                        ProductSizeUpdate.Code = txtCode.Text;
                        dc.SubmitChanges();

                        grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                        this.Title = "IMS - Nhóm hàng ";
                    }
                }
                

                lsViewProductSize.IsEnabled = true;
                //Lấy dữ liệu từ ProductSizeView                
                List<ProductSize> ls = (from s in dc.ProductSizes
                                     select s).ToList();

                var list = ls.AsEnumerable().Select((ProductSize, index) => new ProductSize()
                {
                    RowNumber = index + 1,
                    Id = ProductSize.Id,
                    Lenth = ProductSize.Lenth,
                    Code = ProductSize.Code
                   

                }).ToList();

                lsViewProductSize.ItemsSource = null;
                lsViewProductSize.ItemsSource = list;
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
            lsViewProductSize.IsEnabled = true;
        }

        private void tbrAddSProductSize_Click(object sender, RoutedEventArgs e)
        {
            idProductSize = -1;
            this.Title = "IMS - Thêm nhóm hàng ";

            grdAll.RowDefinitions[0].Height = new GridLength(110, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            lsViewProductSize.IsEnabled = false;

            txtName.Focus();
            txtName.SelectAll();
        }

        private void tbrEditProductSize_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "IMS - Sửa chữa nhóm hàng ";

            grdAll.RowDefinitions[0].Height = new GridLength(110, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            lsViewProductSize.IsEnabled = false;

            //Lấy dữ liệu từ idProductSize
            IMSDataContext dc = new IMSDataContext();
            if (idProductSize != -1)
            {
                var queryProductSize = (from s in dc.ProductSizes
                                 where (s.Id == idProductSize)
                                 select s).First();

                if (queryProductSize != null)
                {
                    txtName.Text = queryProductSize.Lenth;
                    txtCode.Text = queryProductSize.Code;                   
                }
            }
            txtName.Focus();
            txtName.SelectAll();
        }

        private void lsViewProductSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var userView = e.AddedItems[0] as ProductSize;
                idProductSize = userView.Id;
            }
            catch
            {
                ;
            }
        }

        private void btnRemoveProductSize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            frmProduct_.Refresh_GUI("ProductSize");
        }
    }
}
