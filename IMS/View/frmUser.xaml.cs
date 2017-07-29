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
    /// Interaction logic for frmUser.xaml
    /// </summary>
    public partial class frmUser : Window
    {
        int idUser;        
        public frmUser()
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

                //Load dữ liệu vào các combobox                      
                //---lsProvince---             
                var lsUserGroup = (from s in dc.UserGroups select s);
                cmbUserGroup.ItemsSource = lsUserGroup;
                cmbUserGroup.DisplayMemberPath = "Name";
                cmbUserGroup.SelectedValuePath = "Id";

                var lsStaff = (from s in dc.Staffs select s);
                cmbStaff.ItemsSource = lsStaff;
                cmbStaff.DisplayMemberPath = "Name";
                cmbStaff.SelectedValuePath = "Id";

                //Lấy dữ liệu từ UserView                
                List<UserView> ls = (from s in dc.UserViews
                                      select s).ToList();

                var list = ls.AsEnumerable().Select((UserView, index) => new UserView()
                {
                    RowNumber = index + 1,
                    Id = UserView.Id,
                    Name = UserView.Name,
                    Password = UserView.Password,
                    StaffName = UserView.StaffName,                   
                    UserGroupName = UserView.UserGroupName

                }).ToList();

                lsViewUser.ItemsSource = null;
                lsViewUser.ItemsSource = list;
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
                if (idUser == -1)
                {
                    try
                    {
                        var UserAdd1 = (from s in dc.MUsers
                                        where (s.Name == txtName.Text)
                                           select s).First();
                        if (UserAdd1 != null)
                        {
                            MessageBox.Show("Người dùng đã tồn tại!", "IMS - Thông báo lỗi");
                            return;
                        }
                    }
                    catch
                    {
                        ;
                    }

                    MUser UserAdd = new  MUser();
                    UserAdd.Name = txtName.Text;
                    UserAdd.Password = txtPassword.Password;                    
                    UserAdd.IdUserGroup = int.Parse(cmbUserGroup.SelectedValue.ToString());
                    UserAdd.IdStaff = int.Parse(cmbStaff.SelectedValue.ToString());
                    dc.MUsers.InsertOnSubmit(UserAdd);
                    dc.SubmitChanges();

                    grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                    this.Title = "IMS - Người dùng";
                }
                else
                {                    
                    MUser UserUpdate = null;
                    UserUpdate = (from s in dc.MUsers
                                      where (s.Id == idUser)
                                     select s).First();

                    if (UserUpdate != null)
                    {
                        UserUpdate.Name = txtName.Text;
                        UserUpdate.Password = txtPassword.Password;
                        UserUpdate.IdUserGroup = int.Parse(cmbUserGroup.SelectedValue.ToString());
                        UserUpdate.IdStaff = int.Parse(cmbStaff.SelectedValue.ToString());
                        dc.SubmitChanges();

                        grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                        this.Title = "IMS - Người dùng";
                    }
                }
                

                lsViewUser.IsEnabled = true;
                //Lấy dữ liệu từ UserView                
                List<UserView> ls = (from s in dc.UserViews
                                     select s).ToList();

                var list = ls.AsEnumerable().Select((UserView, index) => new UserView()
                {
                    RowNumber = index + 1,
                    Id = UserView.Id,
                    Name = UserView.Name,
                    Password = UserView.Password,
                    StaffName = UserView.StaffName,
                    UserGroupName = UserView.UserGroupName

                }).ToList();

                lsViewUser.ItemsSource = null;
                lsViewUser.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "IMS - Người dùng";
            grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
            grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
            lsViewUser.IsEnabled = true;
        }

        private void tbrAddSUser_Click(object sender, RoutedEventArgs e)
        {
            idUser = -1;
            this.Title = "IMS - Thêm người dùng";

            grdAll.RowDefinitions[0].Height = new GridLength(220, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            lsViewUser.IsEnabled = false;

            txtName.Focus();
            txtName.SelectAll();
        }

        private void tbrEditUser_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "IMS - Sửa chữa người dùng";

            grdAll.RowDefinitions[0].Height = new GridLength(220, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            lsViewUser.IsEnabled = false;

            //Lấy dữ liệu từ idUser
            IMSDataContext dc = new IMSDataContext();
            if (idUser != -1)
            {
                var queryUser = (from s in dc.UserViews
                                 where (s.Id == idUser)
                                 select s).First();

                if (queryUser != null)
                {
                    txtName.Text = queryUser.Name;
                    txtPassword.Password = queryUser.Password;
                    cmbUserGroup.SelectedValue = queryUser.IdUserGroup;
                    cmbStaff.SelectedValue = queryUser.IdStaff;
                }
            }
            txtName.Focus();
            txtName.SelectAll();
        }

        private void lsViewUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var userView = e.AddedItems[0] as UserView;
                idUser = userView.Id;
            }
            catch
            {
                ;
            }
        }

        private void btnRemoveUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
