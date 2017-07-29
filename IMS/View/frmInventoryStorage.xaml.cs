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
    /// Interaction logic for frmInventoryStorage.xaml
    /// </summary>
    public partial class frmInventoryStorage : Window
    {
        int idInventoryStorage;
        
        public frmInventoryStorage()
        {
            try
            {
                InitializeComponent();               

                //Thiết lập định dạng VN
                UString.SetSystem();

                grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                //Khởi tạo DataContext
                IMSDataContext dc = new IMSDataContext();

                

                //Lấy dữ liệu từ InventoryStorageView                
                List<InventoryStorage> ls = (from s in dc.InventoryStorages
                                         select s).ToList();

                var list = ls.AsEnumerable().Select((InventoryStorage, index) => new InventoryStorage()
                {
                    RowNumber = index + 1,
                    Id = InventoryStorage.Id,
                    Name = InventoryStorage.Name,
                    Address = InventoryStorage.Address

                }).ToList();

                lsViewInventoryStorage.ItemsSource = null;
                lsViewInventoryStorage.ItemsSource = list;
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
                if (idInventoryStorage == -1)
                {
                    try
                    {
                        var InventoryStorageAdd1 = (from s in dc.InventoryStorages
                                        where (s.Name == txtName.Text)
                                           select s).First();
                        if (InventoryStorageAdd1 != null)
                        {
                            MessageBox.Show("Nhóm hàng  đã tồn tại!", "IMS - Thông báo lỗi");
                            return;
                        }
                    }
                    catch
                    {
                        ;
                    }

                    InventoryStorage InventoryStorageAdd = new  InventoryStorage();
                    InventoryStorageAdd.Name = txtName.Text;
                    InventoryStorageAdd.Address = txtAddress.Text;
                    dc.InventoryStorages.InsertOnSubmit(InventoryStorageAdd);
                    dc.SubmitChanges();

                    grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                    this.Title = "IMS - Nhóm hàng ";
                }
                else
                {                    
                    InventoryStorage InventoryStorageUpdate = null;
                    InventoryStorageUpdate = (from s in dc.InventoryStorages
                                      where (s.Id == idInventoryStorage)
                                     select s).First();

                    if (InventoryStorageUpdate != null)
                    {
                        InventoryStorageUpdate.Name = txtName.Text;
                        InventoryStorageUpdate.Address = txtAddress.Text;
                        dc.SubmitChanges();

                        grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                        this.Title = "IMS - Nhóm hàng ";
                    }
                }
                

                lsViewInventoryStorage.IsEnabled = true;
                //Lấy dữ liệu từ InventoryStorageView                
                List<InventoryStorage> ls = (from s in dc.InventoryStorages
                                     select s).ToList();

                var list = ls.AsEnumerable().Select((InventoryStorage, index) => new InventoryStorage()
                {
                    RowNumber = index + 1,
                    Id = InventoryStorage.Id,
                    Name = InventoryStorage.Name,
                    Address = InventoryStorage.Address
                   

                }).ToList();

                lsViewInventoryStorage.ItemsSource = null;
                lsViewInventoryStorage.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "IMS - Kho chưa hàng";
            grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
            grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
            lsViewInventoryStorage.IsEnabled = true;
        }

        private void tbrAddInventoryStorage_Click(object sender, RoutedEventArgs e)
        {
            idInventoryStorage = -1;
            this.Title = "IMS - Thêm kho hàng";

            grdAll.RowDefinitions[0].Height = new GridLength(110, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            lsViewInventoryStorage.IsEnabled = false;

            txtName.Focus();
            txtName.SelectAll();
        }

        private void tbrEditInventoryStorage_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "IMS - Sửa kho hàng";

            grdAll.RowDefinitions[0].Height = new GridLength(110, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            lsViewInventoryStorage.IsEnabled = false;

            //Lấy dữ liệu từ idInventoryStorage
            IMSDataContext dc = new IMSDataContext();
            if (idInventoryStorage != -1)
            {
                var queryInventoryStorage = (from s in dc.InventoryStorages
                                 where (s.Id == idInventoryStorage)
                                 select s).First();

                if (queryInventoryStorage != null)
                {
                    txtName.Text = queryInventoryStorage.Name;
                    txtAddress.Text = queryInventoryStorage.Address;                   
                }
            }
            txtName.Focus();
            txtName.SelectAll();
        }

        private void lsViewInventoryStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var userView = e.AddedItems[0] as InventoryStorage;
                idInventoryStorage = userView.Id;
            }
            catch
            {
                ;
            }
        }

        private void btnRemoveInventoryStorage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();            
        }
    }
}
