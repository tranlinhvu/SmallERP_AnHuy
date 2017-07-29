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
    /// Interaction logic for frmStaff.xaml
    /// </summary>
    public partial class frmStaff : Window
    {
        int idStaff;
        pgStaff pgCus;
        public frmStaff(int idStaff_, pgStaff pgCus_)
        {
            InitializeComponent();
            idStaff = idStaff_;
            pgCus = pgCus_;

            //Thiết lập định dạng VN
            UString.SetSystem();

            //Khởi tạo DataContext
            IMSDataContext dc = new IMSDataContext();

            //Load dữ liệu vào các combobox                      
            //---lsProvince---             
            var lsProvince = (from s in dc.Provinces select s);
            cmbProvince.ItemsSource = lsProvince;
            cmbProvince.DisplayMemberPath = "Name";
            cmbProvince.SelectedValuePath = "Id";

            var lsStaffGroup = (from s in dc.StaffGroups select s);
            cmbStaffGroup.ItemsSource = lsStaffGroup;
            cmbStaffGroup.DisplayMemberPath = "Name";
            cmbStaffGroup.SelectedValuePath = "Id";

            //Lấy dữ liệu từ idStaff
            if (idStaff != -1)
            {
                var queryStaff= (from Staff in dc.Staffs
                                    where (Staff.Id == idStaff)
                                    select Staff).First();

                if (queryStaff!= null)
                {
                    txtName.Text = queryStaff.Name;
                    txtDateOfBirth.Text = queryStaff.DateOfBirth;
                    cmbProvince.SelectedValue = queryStaff.IdProvince;
                    cmbDistrict.SelectedValue = queryStaff.IdDistrict;
                    cmbWard.SelectedValue = queryStaff.IdWard;
                    txtStreetNumber.Text = queryStaff.Address;
                    txtOcupation.Text = queryStaff.Ocupation;
                    txtEmail.Text = queryStaff.Email;
                    txtPhone.Text = queryStaff.Phone;
                    cmbStaffGroup.SelectedValue = queryStaff.IdStaffGroup;
                }
            }
            txtName.Focus();
            txtName.SelectAll();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idStaff == -1)
                {
                    IMSDataContext dc = new IMSDataContext();
                    Staff StaffAdd = new Staff();
                    StaffAdd.Name = txtName.Text;
                    StaffAdd.DateOfBirth = txtDateOfBirth.Text;
                    StaffAdd.IdProvince = int.Parse(cmbProvince.SelectedValue.ToString());
                    StaffAdd.IdDistrict = int.Parse(cmbDistrict.SelectedValue.ToString());
                    StaffAdd.IdWard = int.Parse(cmbWard.SelectedValue.ToString());
                    StaffAdd.Address = txtStreetNumber.Text;
                    StaffAdd.Ocupation = txtOcupation.Text;
                    StaffAdd.Email = txtEmail.Text;
                    StaffAdd.Phone = txtPhone.Text;
                    StaffAdd.IdStaffGroup = int.Parse(cmbStaffGroup.SelectedValue.ToString());

                    dc.Staffs.InsertOnSubmit(StaffAdd);
                    dc.SubmitChanges();
                    this.Close();
                    pgCus.Page_Refresh(StaffAdd);
                }
                else
                {
                    //Remove all data matching Id in textbox
                    IMSDataContext dc = new IMSDataContext();
                    Staff StaffUpdate = null;

                    StaffUpdate = (from Staff in dc.Staffs
                                      where (Staff.Id == idStaff)
                                     select Staff).First();

                    if (StaffUpdate != null)
                    {
                        StaffUpdate.Name = txtName.Text;
                        StaffUpdate.DateOfBirth = txtDateOfBirth.Text;
                        StaffUpdate.IdProvince = int.Parse(cmbProvince.SelectedValue.ToString());
                        StaffUpdate.IdDistrict = int.Parse(cmbDistrict.SelectedValue.ToString());
                        StaffUpdate.IdWard = int.Parse(cmbWard.SelectedValue.ToString());
                        StaffUpdate.Address = txtStreetNumber.Text;
                        StaffUpdate.Ocupation = txtOcupation.Text;
                        StaffUpdate.Email = txtEmail.Text;
                        StaffUpdate.Phone = txtPhone.Text;
                        StaffUpdate.IdStaffGroup = int.Parse(cmbStaffGroup.SelectedValue.ToString());
                        dc.SubmitChanges();
                        this.Close();
                        pgCus.Page_Refresh(StaffUpdate);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbProvince_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var province = e.AddedItems[0] as Province;

                IMSDataContext dc = new IMSDataContext();
                var lsDistrict = (from s in dc.Districts where (s.idProvince == province.Id) select s);
                cmbDistrict.ItemsSource = lsDistrict;
                cmbDistrict.DisplayMemberPath = "Name";
                cmbDistrict.SelectedValuePath = "Id";
            }
            catch
            {
                ;
            }
        }

        private void cmbDistrict_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var district = e.AddedItems[0] as District;

                IMSDataContext dc = new IMSDataContext();
                var lsWard = (from s in dc.Wards where (s.IdDistrict == district.Id) select s);
                cmbWard.ItemsSource = lsWard;
                cmbWard.DisplayMemberPath = "Name";
                cmbWard.SelectedValuePath = "Id";
            }
            catch
            {
                ;
            }
            
        }
    }
}
