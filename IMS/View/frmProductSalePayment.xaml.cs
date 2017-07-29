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
    public partial class frmProductSalePayment : Window
    {
        pgProductSale frmProductSalePayment_;
        int idObjectCareDetail = -1;
        string code;
        int careAmount = 0;
        int prePaid = 0;
        int curBalance = 0;
        int curDiscount = 0;
        public frmProductSalePayment(pgProductSale _pgObjectCarePayment, long idProductSale, int _careAmount, int _prePaid)
        {
            InitializeComponent();
            //Thiết lập định dạng VN

            frmProductSalePayment_ = _pgObjectCarePayment;
            UString.SetSystem();
           
            careAmount = _careAmount;
            prePaid = _prePaid;

            IMSDataContext dc = new IMSDataContext();
            ProductSaleView sQuery = null;

            sQuery = (from s in dc.ProductSaleViews
                      where (s.Id == idProductSale)
                       select s).First();

            txtCode.Text = sQuery.InvoiceNo;
            txtCareAmount.Text = UString.GetVNFormatString(Convert.ToInt32(sQuery.Amount));
            txtPrePaid.Text = UString.GetVNFormatString(Convert.ToInt32(sQuery.Payment));
            curBalance = Convert.ToInt32(sQuery.Amount) - Convert.ToInt32(sQuery.Payment);
            //if(curBalance == 0)
            //{
            //    curBalance = Convert.ToInt32(sQuery.Amount);
            //}
            txtVAT.Text = UString.GetVNFormatString(Convert.ToInt32(sQuery.Discount));
            curDiscount = Convert.ToInt32(sQuery.Discount);
            curBalance = curBalance + curDiscount;
            txtPayment.Text = "0";
            txtBalance.Text = curBalance.ToString();

            txtPayment.Focus();
            txtPayment.SelectAll();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int balance = curBalance - int.Parse(txtPayment.Text.ToString().Replace(".", ""));

                if(balance < 0)
                {
                    MessageBox.Show("Số tiền thanh toán nhiều hơn số tiền cần thanh toán. Hãy nhập lại!", "IMS - Thông báo lỗi");
                    txtPayment.Text = "0";
                    txtPayment.Focus();
                    txtPayment.SelectAll();
                    return;
                }
                //Update payment
                IMSDataContext dc = new IMSDataContext();
                ObjectCareDetail objectCareDetailUpdate = null;

                objectCareDetailUpdate = (from s in dc.ObjectCareDetails
                                            where (s.Code == code)
                                            select s).First();

                if (objectCareDetailUpdate != null)
                {
                    objectCareDetailUpdate.Code = txtCode.Text;
                    
                    objectCareDetailUpdate.Discount = curDiscount + int.Parse(txtVAT.Text.ToString().Replace(".", ""));
                   
                    objectCareDetailUpdate.Payment = int.Parse(txtPayment.Text.ToString().Replace(".", "")) + int.Parse(txtPrePaid.Text.ToString().Replace(".", ""));
                    objectCareDetailUpdate.Balance = int.Parse(txtBalance.Text.ToString().Replace(".", ""));
                    dc.SubmitChanges();
                   
                    this.Close();
                    //frmProductSalePayment.Refresh_GUI();

                    //frmPrintReceipt a = new frmPrintReceipt(objectCareDetailUpdate.Id);
                    //a.Show();
                    dc.ProcRemovePrintJob(true, Convert.ToInt64(objectCareDetailUpdate.Id));
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
                txtPayment.Text = UString.GetVNFormatString(Convert.ToInt32(txtPayment.Text));
                //if (txtDiscount.Text == "")
                //{
                //    txtDiscount.Text = "0";
                //    txtPayment.Text = UString.GetVNFormatString(Convert.ToInt32(txtPayment.Text));
                //    txtBalance.Text = UString.GetVNFormatString((int.Parse(txtPrePaid.Text.ToString()) - int.Parse(txtPayment.Text.ToString())));
                //}
                //else
                //{
                //    txtDiscount.Text = UString.GetVNFormatString(Convert.ToInt32(txtDiscount.Text));
                //    txtPayment.Text = UString.GetVNFormatString(Convert.ToInt32(txtPayment.Text));
                //    txtBalance.Text = UString.GetVNFormatString((int.Parse(txtPrePaid.Text.ToString().Replace(".", "")) - int.Parse(txtPayment.Text.ToString().Replace(".", "")) - int.Parse(txtDiscount.Text.ToString().Replace(".", ""))));
                //}
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
                //txtUnitPrice.Text = UString.ConvertToVNCurrency(txtUnitPrice.Text);
                double.Parse(txtPayment.Text.Replace(".", ""));
                txtVAT.Text = UString.GetVNFormatString(Convert.ToInt32(txtVAT.Text.Replace(".", "")));
            }
            catch
            {
                txtPayment.Text = "";
            }       
        }

        private void txtDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //txtUnitPrice.Text = UString.ConvertToVNCurrency(txtUnitPrice.Text);
                double.Parse(txtVAT.Text.Replace(".", ""));
                txtBalance.Text = UString.GetVNFormatString((curBalance - int.Parse(txtPayment.Text.ToString().Replace(".", "")) - int.Parse(txtVAT.Text.ToString().Replace(".", ""))));
            }
            catch
            {
                txtVAT.Text = "";
            }
        }

        private void txtPayment_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //txtUnitPrice.Text = UString.ConvertToVNCurrency(txtUnitPrice.Text);
                double.Parse(txtPayment.Text.Replace(".", ""));


                if (txtVAT.Text == "")
                {
                    txtVAT.Text = "0";
                    txtBalance.Text = UString.GetVNFormatString((curBalance - int.Parse(txtPayment.Text.ToString().Replace(".", "")) - int.Parse(txtVAT.Text.ToString().Replace(".", ""))));

                }
                else
                {
                    txtVAT.Text = UString.GetVNFormatString(Convert.ToInt32(txtVAT.Text.Replace(".", "")));
                    txtBalance.Text = UString.GetVNFormatString((curBalance - int.Parse(txtPayment.Text.ToString().Replace(".", "")) - int.Parse(txtVAT.Text.ToString().Replace(".", ""))));
                }               

               
            }
            catch
            {
                txtPayment.Text = "";
            }
        }
    }
}
