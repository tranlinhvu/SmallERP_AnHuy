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
    /// Interaction logic for frmManufacture.xaml
    /// </summary>
    public partial class frmManufacture : Window
    {
        int idManufacture;
        pgManufacture pgCus;
        public frmManufacture(int idManufacture_, pgManufacture pgCus_)
        {
            InitializeComponent();
            idManufacture = idManufacture_;
            pgCus = pgCus_;

            //Thiết lập định dạng VN
            UString.SetSystem();

            //Khởi tạo DataContext
            IMSDataContext dc = new IMSDataContext();
          
            //Lấy dữ liệu từ idManufacture
            if (idManufacture != -1)
            {
                var queryManufacture= (from vendor in dc.Manufactures
                                    where (vendor.Id == idManufacture)
                                    select vendor).First();

                if (queryManufacture!= null)
                {
                    txtName.Text = queryManufacture.Name;
                    txtCode.Text = queryManufacture.Code;
                    txtStreetNumber.Text = queryManufacture.Address;         
                    txtEmail.Text = queryManufacture.Email;
                    txtPhone.Text = queryManufacture.Phone;
                }
            }
            txtName.Focus();
            txtName.SelectAll();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idManufacture == -1)
                {
                    IMSDataContext dc = new IMSDataContext();
                    Manufacture manufAdd = new Manufacture();
                    manufAdd.Name = txtName.Text;
                    manufAdd.Code = txtCode.Text;
                    manufAdd.Address = txtStreetNumber.Text;
                    manufAdd.Email = txtEmail.Text;
                    manufAdd.Phone = txtPhone.Text;

                    dc.Manufactures.InsertOnSubmit(manufAdd);
                    dc.SubmitChanges();
                    this.Close();
                    pgCus.Page_Refresh(manufAdd);
                }
                else
                {
                    //Remove all data matching Id in textbox
                    IMSDataContext dc = new IMSDataContext();
                    Manufacture manufUpdate = null;

                    manufUpdate = (from vendor in dc.Manufactures
                                      where (vendor.Id == idManufacture)
                                     select vendor).First();

                    if (manufUpdate != null)
                    {
                        manufUpdate.Name = txtName.Text;
                        manufUpdate.Code = txtCode.Text;
                        manufUpdate.Address = txtStreetNumber.Text;
                        manufUpdate.Email = txtEmail.Text;
                        manufUpdate.Phone = txtPhone.Text;

                        dc.SubmitChanges();
                        this.Close();
                        pgCus.Page_Refresh(manufUpdate);
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
