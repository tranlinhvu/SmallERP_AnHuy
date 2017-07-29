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
    /// Interaction logic for frmAddPurchase.xaml
    /// </summary>
    public partial class frmAddPurchase : Window
    {
        string objName = null;
        pgProductPurchase pg = null;
        long idProductPurchase = -1;
        int idService = -1;
        int idProduct = -1;
        decimal price = 0;
        public frmAddPurchase(string objName_, pgProductPurchase pg_, long idProductPurchase_, int idService_, decimal price_)
        {
            InitializeComponent();
            objName = objName_;
            pg = pg_;
            idProductPurchase = idProductPurchase_;
            idService = idService_;
            idProduct = idService_;
            price = price_;

            txtPrice.Text = UString.GetVNFormatString(Convert.ToInt64(price_));
            txtQuantity.Text = "1";
            txtPrice.Focus();
            txtPrice.SelectAll();
        }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (objName == "Service")
                {
                    IMSDataContext dc = new IMSDataContext();
                    ServiceSaleItem serviceSaleItem = new ServiceSaleItem();
                    serviceSaleItem.IdService = idService;
                    serviceSaleItem.Price = decimal.Parse(txtPrice.Text.Replace(".", ""));
                    serviceSaleItem.Quantity = int.Parse(txtQuantity.Text);
                    serviceSaleItem.Amount = decimal.Parse(txtPrice.Text) * int.Parse(txtQuantity.Text);
                    serviceSaleItem.IdObjectCareDetail = int.Parse(idProductPurchase.ToString());

                    dc.ServiceSaleItems.InsertOnSubmit(serviceSaleItem);
                    dc.SubmitChanges();

                    this.Close();
                    pg.Refresh_GUI("Service");
                }
                else if (objName == "Product")
                {
                    IMSDataContext dc = new IMSDataContext();
                    ProductPurchaseItem productPurchaseItem = new ProductPurchaseItem();
                    productPurchaseItem.IdProduct = idProduct;
                    productPurchaseItem.Price = long.Parse(txtPrice.Text.Replace(".", ""));
                    //float a = (float)double.Parse(txtQuantity.Text.Replace(" ", ""));
                    productPurchaseItem.Quantity = (float)Convert.ToSingle(txtQuantity.Text);
                    productPurchaseItem.Amount = (float)productPurchaseItem.Price * productPurchaseItem.Quantity;
                    productPurchaseItem.IdProductPurchase = idProductPurchase;

                    dc.ProductPurchaseItems.InsertOnSubmit(productPurchaseItem);
                    dc.SubmitChanges();
                    this.Close();
                    pg.Refresh_GUI("Product");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}
