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
    /// Interaction logic for frmVendor.xaml
    /// </summary>
    public partial class frmVendor : Window
    {
        int idVendor;
        pgVendor pgCus;
        public frmVendor(int idVendor_, pgVendor pgCus_)
        {
            InitializeComponent();
            idVendor = idVendor_;
            pgCus = pgCus_;

            //Thiết lập định dạng VN
            UString.SetSystem();

            //Khởi tạo DataContext
            IMSDataContext dc = new IMSDataContext();
          
            //Lấy dữ liệu từ idVendor
            if (idVendor != -1)
            {
                var queryVendor= (from vendor in dc.Vendors
                                    where (vendor.Id == idVendor)
                                    select vendor).First();

                if (queryVendor!= null)
                {
                    txtName.Text = queryVendor.Name;
                    txtStreetNumber.Text = queryVendor.Address;         
                    txtEmail.Text = queryVendor.Email;
                    txtPhone.Text = queryVendor.Phone;
                }
            }
            txtName.Focus();
            txtName.SelectAll();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idVendor == -1)
                {
                    IMSDataContext dc = new IMSDataContext();
                    Vendor vendorAdd = new Vendor();
                    vendorAdd.Name = txtName.Text;
                    vendorAdd.Address = txtStreetNumber.Text;
                    vendorAdd.Email = txtEmail.Text;
                    vendorAdd.Phone = txtPhone.Text;

                    dc.Vendors.InsertOnSubmit(vendorAdd);
                    dc.SubmitChanges();
                    this.Close();
                    pgCus.Page_Refresh(vendorAdd);
                }
                else
                {
                    //Remove all data matching Id in textbox
                    IMSDataContext dc = new IMSDataContext();
                    Vendor vendorUpdate = null;

                    vendorUpdate = (from vendor in dc.Vendors
                                      where (vendor.Id == idVendor)
                                     select vendor).First();

                    if (vendorUpdate != null)
                    {
                        vendorUpdate.Name = txtName.Text;                       
                        vendorUpdate.Address = txtStreetNumber.Text;                        
                        vendorUpdate.Email = txtEmail.Text;
                        vendorUpdate.Phone = txtPhone.Text;

                        dc.SubmitChanges();
                        this.Close();
                        pgCus.Page_Refresh(vendorUpdate);
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
      
    }
}
