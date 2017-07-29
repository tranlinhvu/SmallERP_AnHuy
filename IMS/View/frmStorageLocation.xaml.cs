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
    /// Interaction logic for frmProductGroup.xaml
    /// </summary>
    public partial class frmStorageLocation : Window
    {
        int idProductGroup;
        frmProduct1 frmProduct_ = null;
        public frmStorageLocation(frmProduct1 _frmProduct)
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

                

                //Lấy dữ liệu từ ProductGroupView                
                List<ProductGroup> ls = (from s in dc.ProductGroups
                                         select s).ToList();

                var list = ls.AsEnumerable().Select((ProductGroup, index) => new ProductGroup()
                {
                    RowNumber = index + 1,
                    Id = ProductGroup.Id,
                    Name = ProductGroup.Name,
                    Note = ProductGroup.Note

                }).ToList();

                lsViewProductGroup.ItemsSource = null;
                lsViewProductGroup.ItemsSource = list;
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
                if (idProductGroup == -1)
                {
                    try
                    {
                        var ProductGroupAdd1 = (from s in dc.ProductGroups
                                        where (s.Name == txtName.Text)
                                           select s).First();
                        if (ProductGroupAdd1 != null)
                        {
                            MessageBox.Show("Nhóm hàng  đã tồn tại!", "IMS - Thông báo lỗi");
                            return;
                        }
                    }
                    catch
                    {
                        ;
                    }

                    ProductGroup ProductGroupAdd = new  ProductGroup();
                    ProductGroupAdd.Name = txtName.Text;
                    ProductGroupAdd.Note = txtNote.Text;
                    dc.ProductGroups.InsertOnSubmit(ProductGroupAdd);
                    dc.SubmitChanges();

                    grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                    this.Title = "IMS - Nhóm hàng ";
                }
                else
                {                    
                    ProductGroup ProductGroupUpdate = null;
                    ProductGroupUpdate = (from s in dc.ProductGroups
                                      where (s.Id == idProductGroup)
                                     select s).First();

                    if (ProductGroupUpdate != null)
                    {
                        ProductGroupUpdate.Name = txtName.Text;
                        ProductGroupUpdate.Note = txtNote.Text;
                        dc.SubmitChanges();

                        grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                        this.Title = "IMS - Nhóm hàng ";
                    }
                }
                

                lsViewProductGroup.IsEnabled = true;
                //Lấy dữ liệu từ ProductGroupView                
                List<ProductGroup> ls = (from s in dc.ProductGroups
                                     select s).ToList();

                var list = ls.AsEnumerable().Select((ProductGroup, index) => new ProductGroup()
                {
                    RowNumber = index + 1,
                    Id = ProductGroup.Id,
                    Name = ProductGroup.Name,
                    Note = ProductGroup.Note
                   

                }).ToList();

                lsViewProductGroup.ItemsSource = null;
                lsViewProductGroup.ItemsSource = list;
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
            lsViewProductGroup.IsEnabled = true;
        }

        private void tbrAddSProductGroup_Click(object sender, RoutedEventArgs e)
        {
            idProductGroup = -1;
            this.Title = "IMS - Thêm nhóm hàng ";

            grdAll.RowDefinitions[0].Height = new GridLength(110, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            lsViewProductGroup.IsEnabled = false;

            txtName.Focus();
            txtName.SelectAll();
        }

        private void tbrEditProductGroup_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "IMS - Sửa chữa nhóm hàng ";

            grdAll.RowDefinitions[0].Height = new GridLength(110, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            lsViewProductGroup.IsEnabled = false;

            //Lấy dữ liệu từ idProductGroup
            IMSDataContext dc = new IMSDataContext();
            if (idProductGroup != -1)
            {
                var queryProductGroup = (from s in dc.ProductGroups
                                 where (s.Id == idProductGroup)
                                 select s).First();

                if (queryProductGroup != null)
                {
                    txtName.Text = queryProductGroup.Name;
                    txtNote.Text = queryProductGroup.Note;                   
                }
            }
            txtName.Focus();
            txtName.SelectAll();
        }

        private void lsViewProductGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var userView = e.AddedItems[0] as ProductGroup;
                idProductGroup = userView.Id;
            }
            catch
            {
                ;
            }
        }

        private void btnRemoveProductGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            frmProduct_.Refresh_GUI("ProductGroup");
        }
    }
}
