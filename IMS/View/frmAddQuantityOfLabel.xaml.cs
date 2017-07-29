using IMS;
using IMS.Favorite;
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
    /// Interaction logic for frmAddSale.xaml
    /// </summary>
    public partial class frmAddQuantityOfLabel : Window
    {
        string objName = null;
        pgObjectCareSale pg = null;
        pgObjectCareManatement pgg = null;
        pgProductSale pg1 = null;
        int idObjectCareDetail = -1;
        long idProductSale = -1;
        int idService = -1;
        int idProduct = -1;
        decimal price = 0;

        public frmAddQuantityOfLabel()
        {
            InitializeComponent();
            
        }
        public frmAddQuantityOfLabel(string objName_, pgObjectCareSale pg_, int idObjectCareDetail_, int idService_, decimal price_)
        {
            InitializeComponent();
            objName = objName_;
            pg = pg_;
            idObjectCareDetail = idObjectCareDetail_;
            idService = idService_;
            idProduct = idService_;
            price = price_;

            txtPrice.Text = UString.GetVNFormatString(Convert.ToInt32(price_));
            txtQuantity.Text = "1";
            txtPrice.Focus();
            txtPrice.SelectAll();
        }

        public frmAddQuantityOfLabel(string objName_, pgObjectCareManatement pg_, int idObjectCareDetail_, int idService_, decimal price_)
        {
            InitializeComponent();
            objName = objName_;
            pgg = pg_;
            idObjectCareDetail = idObjectCareDetail_;
            idService = idService_;
            idProduct = idService_;
            price = price_;

            txtPrice.Text = UString.GetVNFormatString(Convert.ToInt32(price_));
            txtQuantity.Text = "1";
            txtPrice.Focus();
            txtPrice.SelectAll();
        }

        public frmAddQuantityOfLabel(string objName_, pgProductSale pg_, int idObjectCareDetail_, int idService_, decimal price_)
        {
            InitializeComponent();
            objName = objName_;
            pg1 = pg_;
            idObjectCareDetail = idObjectCareDetail_;
            idService = idService_;
            idProduct = idService_;
            price = price_;

            txtPrice.Text = UString.GetVNFormatString(Convert.ToInt32(price_));
            txtQuantity.Text = "1";
            txtPrice.Focus();
            txtPrice.SelectAll();
        }

        public frmAddQuantityOfLabel(string objName_, pgProductSale pg_, long idProductSale_, int idService_, decimal price_)
        {
            InitializeComponent();
            objName = objName_;
            pg1 = pg_;
            idProductSale = idProductSale_;
            idService = idService_;
            idProduct = idService_;
            price = price_;

            txtPrice.Text = UString.GetVNFormatString(Convert.ToInt32(price_));
            txtQuantity.Text = "1";
            txtPrice.Focus();
            txtPrice.SelectAll();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (pg != null)
            {
                if (objName == "Service")
                {
                    IMSDataContext dc = new IMSDataContext();
                    ServiceSaleItem serviceSaleItem = new ServiceSaleItem();
                    serviceSaleItem.IdService = idService;
                    serviceSaleItem.Price = decimal.Parse(txtPrice.Text.Replace(".", ""));
                    serviceSaleItem.Quantity = int.Parse(txtQuantity.Text.Replace(".", ""));
                    serviceSaleItem.Amount = decimal.Parse(txtPrice.Text.Replace(".","")) * int.Parse(txtQuantity.Text.Replace(".", ""));
                    serviceSaleItem.IdObjectCareDetail = idObjectCareDetail;

                    dc.ServiceSaleItems.InsertOnSubmit(serviceSaleItem);
                    dc.SubmitChanges();

                    this.Close();
                    pg.Refresh_GUI("Service");
                }
                else if (objName == "Product")
                {
                    IMSDataContext dc = new IMSDataContext();
                    ProductSaleItem productSaleItem = new ProductSaleItem();
                    productSaleItem.IdProduct = idProduct;
                    productSaleItem.Price = decimal.Parse(txtPrice.Text.Replace(".", ""));
                    productSaleItem.Quantity = int.Parse(txtQuantity.Text.Replace(".", ""));
                    productSaleItem.Amount = decimal.Parse(txtPrice.Text.Replace(".", "")) * int.Parse(txtQuantity.Text.Replace(".", ""));
                    productSaleItem.IdObjectCareDetail = idObjectCareDetail;
                    productSaleItem.IdProductSale = Convert.ToInt32(idProductSale);
                    dc.ProductSaleItems.InsertOnSubmit(productSaleItem);
                    dc.SubmitChanges();
                    this.Close();
                    pg.Refresh_GUI("Product");
                }
            }
            else if(pg1 != null)
            {
                if (objName == "Service")
                {
                    IMSDataContext dc = new IMSDataContext();
                    ServiceSaleItem serviceSaleItem = new ServiceSaleItem();
                    serviceSaleItem.IdService = idService;
                    serviceSaleItem.Price = decimal.Parse(txtPrice.Text.Replace(".", ""));
                    serviceSaleItem.Quantity = int.Parse(txtQuantity.Text.Replace(".", ""));
                    serviceSaleItem.Amount = decimal.Parse(txtPrice.Text.Replace(".", "")) * int.Parse(txtQuantity.Text.Replace(".", ""));
                    serviceSaleItem.IdObjectCareDetail = idObjectCareDetail;

                    dc.ServiceSaleItems.InsertOnSubmit(serviceSaleItem);
                    dc.SubmitChanges();

                    this.Close();
                    pg1.Refresh_GUI("Service");
                }
                else if (objName == "Product")
                {
                    IMSDataContext dc = new IMSDataContext();
                    ProductSaleItem productSaleItem = new ProductSaleItem();
                    productSaleItem.IdProduct = idProduct;
                    productSaleItem.Price = decimal.Parse(txtPrice.Text.Replace(".", ""));
                    productSaleItem.Quantity = int.Parse(txtQuantity.Text.Replace(".", ""));
                    productSaleItem.Amount = decimal.Parse(txtPrice.Text.Replace(".", "")) * int.Parse(txtQuantity.Text.Replace(".", ""));
                    productSaleItem.IdObjectCareDetail = idObjectCareDetail;
                    productSaleItem.IdProductSale = Convert.ToInt32(idProductSale);
                    dc.ProductSaleItems.InsertOnSubmit(productSaleItem);
                    dc.SubmitChanges();
                    this.Close();
                    pg1.Refresh_GUI("Product");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnPrintLabel_Click(object sender, RoutedEventArgs e)
        {
            int quantity = 0;
            int.TryParse(txtQuantity.Text, out quantity);

            quantity = quantity / 6;
            quantity = quantity * 6;

            Process p = new Process();
            p.StartInfo.FileName = "PrintJobs.exe";
            p.StartInfo.Arguments = "Label " + quantity;
            p.Start();

            this.Close();
        }
    }
}
