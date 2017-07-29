using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for frmProductSalePrintInvoice.xaml
    /// </summary>
    public partial class frmProductSalePrintInvoice : Window
    {
        long idProductSale = -1;
        public frmProductSalePrintInvoice(long idProductSale_)
        {
            InitializeComponent();
            idProductSale = idProductSale_;

            IMSDataContext dc = new IMSDataContext();

            ProductSaleView sQuery = null;
            sQuery = (from s in dc.ProductSaleViews
                      where (s.Id == idProductSale)
                      select s).First();
            txtInvoiceNo.Text = sQuery.InvoiceNo;

            double amount = Convert.ToSingle(sQuery.Amount);
            double vatValue = double.Parse(txtVAT.Text);
            double totalAmout = amount + amount * vatValue / 100;

            txtAmount.Text = amount.ToString("N0");
            txtTotalAmount.Text = totalAmout.ToString("N0");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (idProductSale < 0)
            {
                MessageBox.Show("Hãy chọn đơn hàng", "IMS - Thông báo lỗi");
                return;
            }
            IMSDataContext dc = new IMSDataContext();            

            //In biên nhận           
            Process p = new Process();
            p.StartInfo.FileName = "PrintJobs.exe";
            p.StartInfo.Arguments = "Receipt " + idProductSale.ToString();
            p.Start();            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtVAT_TextChanged(object sender, TextChangedEventArgs e)
        {
            //txtTotalAmount.Text = string.Format("{0:N0}", (double.Parse(((decimal)Convert.ToSingle(sQuery.Amount) + (decimal)Convert.ToSingle(sQuery.Amount) / 100).ToString()) * double.Parse(txtVAT.Text)).ToString());
        }
    }
}
