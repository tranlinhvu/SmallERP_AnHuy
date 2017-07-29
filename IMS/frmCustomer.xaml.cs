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
    /// Interaction logic for frmCustomer.xaml
    /// </summary>
    public partial class frmCustomer : Window
    {
        int idCustomer;
        pgCustomer pgCus = null;
        pgObjectCareSaleOrder pgObjCareOrder = null;
        public frmCustomer(int idCustomer_, pgCustomer pgCus_)
        {
            InitializeComponent();
            idCustomer = idCustomer_;
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

            //---lsDistrict---
            //var lsDistrict = (from s in dc.Districts select s);
            //cmbDistrict.ItemsSource = lsDistrict;            
            //cmbDistrict.DisplayMemberPath = "Name";
            //cmbDistrict.SelectedValuePath = "Id";

            //---lsWard---            
            //var lsWard = (from s in dc.Wards select s);
            //cmbWard.ItemsSource = lsWard;            
            //cmbWard.DisplayMemberPath = "Name";
            //cmbWard.SelectedValuePath = "Id";

            //Lấy dữ liệu từ idCustomer
            if (idCustomer != -1)
            {
                var queryCustomer= (from customer in dc.Customers
                                    where (customer.Id == idCustomer)
                                    select customer).First();

                if (queryCustomer!= null)
                {
                    txtName.Text = queryCustomer.Name;
                    txtDateOfBirth.Text = queryCustomer.DateOfBirth;
                    cmbProvince.SelectedValue = queryCustomer.IdProvince;
                    cmbDistrict.SelectedValue = queryCustomer.IdDistrict;
                    cmbWard.SelectedValue = queryCustomer.IdWard;
                    txtStreetNumber.Text = queryCustomer.Address;
                    txtOcupation.Text = queryCustomer.Ocupation;
                    txtEmail.Text = queryCustomer.Email;
                    txtPhone.Text = queryCustomer.Phone;
                }
            }
            txtName.Focus();
            txtName.SelectAll();

        }

        public frmCustomer(int idCustomer_, pgObjectCareSaleOrder pgObjCareOrder_)
        {
            InitializeComponent();
            idCustomer = idCustomer_;
            pgObjCareOrder = pgObjCareOrder_;

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

            //---lsDistrict---
            //var lsDistrict = (from s in dc.Districts select s);
            //cmbDistrict.ItemsSource = lsDistrict;            
            //cmbDistrict.DisplayMemberPath = "Name";
            //cmbDistrict.SelectedValuePath = "Id";

            //---lsWard---            
            //var lsWard = (from s in dc.Wards select s);
            //cmbWard.ItemsSource = lsWard;            
            //cmbWard.DisplayMemberPath = "Name";
            //cmbWard.SelectedValuePath = "Id";

            //Lấy dữ liệu từ idCustomer
            if (idCustomer != -1)
            {
                var queryCustomer = (from customer in dc.Customers
                                     where (customer.Id == idCustomer)
                                     select customer).First();

                if (queryCustomer != null)
                {
                    txtName.Text = queryCustomer.Name;
                    txtDateOfBirth.Text = queryCustomer.DateOfBirth;
                    cmbProvince.SelectedValue = queryCustomer.IdProvince;
                    cmbDistrict.SelectedValue = queryCustomer.IdDistrict;
                    cmbWard.SelectedValue = queryCustomer.IdWard;
                    txtStreetNumber.Text = queryCustomer.Address;
                    txtOcupation.Text = queryCustomer.Ocupation;
                    txtEmail.Text = queryCustomer.Email;
                    txtPhone.Text = queryCustomer.Phone;
                }
            }
            txtName.Focus();
            txtName.SelectAll();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idCustomer == -1)
                {
                    IMSDataContext dc = new IMSDataContext();
                    Customer customerAdd = new Customer();
                    customerAdd.Name = txtName.Text;
                    customerAdd.DateOfBirth = txtDateOfBirth.Text;
                    customerAdd.IdProvince = int.Parse(cmbProvince.SelectedValue.ToString());
                    customerAdd.IdDistrict = int.Parse(cmbDistrict.SelectedValue.ToString());
                    customerAdd.IdWard = int.Parse(cmbWard.SelectedValue.ToString());
                    customerAdd.Address = txtStreetNumber.Text;
                    customerAdd.Ocupation = txtOcupation.Text;
                    customerAdd.Email = txtEmail.Text;
                    customerAdd.Phone = txtPhone.Text;

                    dc.Customers.InsertOnSubmit(customerAdd);
                    dc.SubmitChanges();
                    this.Close();
                    if (pgCus != null)
                    {
                        pgCus.Page_Refresh(customerAdd);
                    }
                    else
                    {
                        if (pgObjCareOrder != null)
                        {
                            pgObjCareOrder.Page_Refresh(customerAdd);
                        }
                    }
                }
                else
                {
                    //Remove all data matching Id in textbox
                    IMSDataContext dc = new IMSDataContext();
                    Customer customerUpdate = null;

                    customerUpdate = (from customer in dc.Customers
                                      where (customer.Id == idCustomer)
                                     select customer).First();

                    if (customerUpdate != null)
                    {
                        customerUpdate.Name = txtName.Text;
                        customerUpdate.DateOfBirth = txtDateOfBirth.Text;
                        customerUpdate.IdProvince = int.Parse(cmbProvince.SelectedValue.ToString());
                        customerUpdate.IdDistrict = int.Parse(cmbDistrict.SelectedValue.ToString());
                        customerUpdate.IdWard = int.Parse(cmbWard.SelectedValue.ToString());
                        customerUpdate.Address = txtStreetNumber.Text;
                        customerUpdate.Ocupation = txtOcupation.Text;
                        customerUpdate.Email = txtEmail.Text;
                        customerUpdate.Phone = txtPhone.Text;

                        dc.SubmitChanges();
                        this.Close();
                        if (pgCus != null)
                        {
                            pgCus.Page_Refresh(customerUpdate);
                        }
                        else
                        {
                            if (pgObjCareOrder != null)
                            {
                                pgObjCareOrder.Page_Refresh(customerUpdate);
                            }
                        }
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
