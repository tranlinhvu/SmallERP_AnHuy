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
using IMS.Favorite;
using IMS.Printing;

namespace IMS.View
{
    /// <summary>
    /// Interaction logic for frmObjectCarePayment.xaml
    /// </summary>
    public partial class frmObjectCarePayment : Window
    {
        pgObjectCarePayment pgObjectCarePayment;
        int idObjectCareDetail = -1;
        string code;
        int careAmount = 0;
        public frmObjectCarePayment(pgObjectCarePayment _pgObjectCarePayment, string _code, int _careAmount)
        {
            InitializeComponent();
            //Thiết lập định dạng VN

            pgObjectCarePayment = _pgObjectCarePayment;
            UString.SetSystem();

            code = _code;
            careAmount = _careAmount;

            IMSDataContext dc = new IMSDataContext();
            ObjectCareDetailView sQuery = null;

            sQuery = (from s in dc.ObjectCareDetailViews
                                    where (s.Code == code)
                                      select s).First();

            txtCode.Text = code;
            txtCareAmount.Text = UString.GetVNFormatString(Convert.ToInt32(sQuery.CareAmount));
            txtDiscount.Text = sQuery.Discount.ToString();
            txtPayment.Text = sQuery.Payment.ToString();
            txtBalance.Text = sQuery.Balance.ToString();

            txtPayment.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                //Update payment
                IMSDataContext dc = new IMSDataContext();
                ObjectCareDetail objectCareDetailUpdate = null;

                objectCareDetailUpdate = (from s in dc.ObjectCareDetails
                                            where (s.Code == code)
                                            select s).First();

                if (objectCareDetailUpdate != null)
                {
                    objectCareDetailUpdate.Code = txtCode.Text;
                    if (cmbDiscountType.Text.Contains("VNĐ"))
                    {
                        if (txtDiscount.Text == "")
                        {
                            txtDiscount.Text = "0";
                        }
                        objectCareDetailUpdate.Discount = int.Parse(txtDiscount.Text.ToString().Replace(".", ""));
                    }
                    else
                    {
                        if (txtDiscount.Text == "")
                        {
                            txtDiscount.Text = "0";
                        }
                        objectCareDetailUpdate.Discount = int.Parse(txtDiscount.Text.ToString().Replace(".", "")) * int.Parse(txtCareAmount.Text.ToString().Replace(".", "")) / 100;
                    }
                    objectCareDetailUpdate.Payment = int.Parse(txtPayment.Text.ToString().Replace(".", ""));
                    objectCareDetailUpdate.Balance = int.Parse(txtBalance.Text.ToString().Replace(".", ""));
                    dc.SubmitChanges();
                   
                    this.Close();
                    pgObjectCarePayment.Refresh_GUI();

                    frmPrintReceipt a = new frmPrintReceipt(objectCareDetailUpdate.Id);
                    a.Show();
                   
                   
                }
                
            }
            catch
            {
                ;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtPayment_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtDiscount.Text == "")
                {
                    txtDiscount.Text = "0";
                    txtPayment.Text = UString.GetVNFormatString(Convert.ToInt32(txtPayment.Text));
                    txtBalance.Text = UString.GetVNFormatString((int.Parse(txtCareAmount.Text.ToString()) - int.Parse(txtPayment.Text.ToString())));
                }
                else
                {
                    txtDiscount.Text = UString.GetVNFormatString(Convert.ToInt32(txtDiscount.Text));
                    txtPayment.Text = UString.GetVNFormatString(Convert.ToInt32(txtPayment.Text));
                    txtBalance.Text = UString.GetVNFormatString((int.Parse(txtCareAmount.Text.ToString().Replace(".", "")) - int.Parse(txtPayment.Text.ToString().Replace(".", "")) - int.Parse(txtDiscount.Text.ToString().Replace(".", ""))));
                }
            }
            catch
            {
                ;
            }
        }

        private void txtDiscount_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtDiscount.Text == "")
                {
                    txtDiscount.Text = "0";
                    txtBalance.Text = (int.Parse(txtCareAmount.Text.ToString()) - int.Parse(txtPayment.Text.ToString())).ToString();
                }
                else
                {
                    txtDiscount.Text = UString.GetVNFormatString(Convert.ToInt32(txtDiscount.Text));
                    txtBalance.Text = UString.GetVNFormatString((int.Parse(txtCareAmount.Text.ToString().Replace(".", "")) - int.Parse(txtPayment.Text.ToString().Replace(".", "")) - int.Parse(txtDiscount.Text.ToString().Replace(".", ""))));
                }
            }
            catch
            {
                ;
            }
        }

        private void txtDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //txtUnitPrice.Text = UString.ConvertToVNCurrency(txtUnitPrice.Text);
                double.Parse(txtDiscount.Text.Replace(".", ""));
            }
            catch
            {
                txtDiscount.Text = "";
            }
        }

        private void txtPayment_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //txtUnitPrice.Text = UString.ConvertToVNCurrency(txtUnitPrice.Text);
                double.Parse(txtPayment.Text.Replace(".", ""));
            }
            catch
            {
                txtPayment.Text = "";
            }
        }
    }
}
