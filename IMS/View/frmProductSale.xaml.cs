using IMS.Favorite;
using IMS.Model;
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
    /// Interaction logic for frmService.xaml
    /// </summary>
    public partial class frmProductSale : Window
    {        
        long idProductSale = -1;
        pgProductSale pgProductSaleParam = null;
        
        public frmProductSale(long idProductSale_, pgProductSale pgProductSale_, bool isDoneEdit)
        {
            try
            {
                InitializeComponent();

                //Thiết lập định dạng VN
                UString.SetSystem();

                //Khởi tạo DataContext
                IMSDataContext dc = new IMSDataContext();

                //Gán giá trị cho các biến từ tham số truyền vào của hàm gọi          
                idProductSale = idProductSale_;
                pgProductSaleParam = pgProductSale_;

                //Load dữ liệu vào các combobox                      
                //Staff Combobox           
                var lst1 = (from s in dc.Staffs select s);
                cmbStaff.ItemsSource = lst1;
                cmbStaff.DisplayMemberPath = "Name";
                cmbStaff.SelectedValuePath = "Id";
                cmbStaff.SelectedValue = IMS.Properties.Settings.Default.IdStaff;

                //CarePerson Combobox 
                var lst2 = (from s in dc.Customers select s);
                cmbCustomer.ItemsSource = lst2;
                cmbCustomer.DisplayMemberPath = "Name";
                cmbCustomer.SelectedValuePath = "Id";


                if (isDoneEdit)
                {
                    chkIsDone.IsEnabled = true;
                }
                else
                {
                    chkIsDone.IsEnabled = false;
                }
                //Lấy dữ liệu từ idProductSale
                if (idProductSale != -1)
                {                  

                    var queryProductSale = (from s in dc.ProductSaleViews
                                                where (s.Id == idProductSale)
                                                select s).First();

                    if (queryProductSale != null)
                    {
                        txtCode.Text = queryProductSale.InvoiceNo;
                        dtCreatedDate.Text = queryProductSale.CreatedDateEx;
                        cmbStaff.SelectedValue = queryProductSale.IdStaff;
                        dtCareDate.Text = queryProductSale.InvoiceDateEx;
                        cmbCustomer.Text = queryProductSale.CustomerName;
                    }
                }
                else
                {
                    dtCreatedDate.Text = DateTime.Now.ToShortDateString();
                }
                txtCode.Focus();
                txtCode.SelectAll();
            }
            catch(Exception ex)
            {
                ;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idProductSale == -1)
                {
                    DateTime dCurDate = DateTime.Now;
                    IMSDataContext dc = new IMSDataContext();
                    int curDiffNo = dc.ProcInvoiceNo("", "ProductSale") + 1;
                    ProductSale productSaleAdd = new ProductSale();
                    productSaleAdd.InvoiceNo = txtCode.Text;
                    if (txtCode.Text == "")
                    {
                        productSaleAdd.InvoiceNo = "BH" + UString.Right(DateTime.Now.Year.ToString(), 2) + "." + UString.AddZeroBefore(curDiffNo.ToString(), 5);
                    }
                    productSaleAdd.DiffNo = curDiffNo;
                    productSaleAdd.CreatedDate = UString.GetLongFromDate(dCurDate);
                    productSaleAdd.CreatedDateEx = dCurDate.ToString("dd-MM-yyyy HH:mm:ss");
                    productSaleAdd.InvoiceDate = UString.GetLongFromDate(DateTime.Parse(dtCareDate.Text));
                    productSaleAdd.InvoiceDateEx = UString.GetDateFromLong(productSaleAdd.InvoiceDate.Value).ToString("dd-MM-yyyy HH:mm:ss");
                    productSaleAdd.IdStaff = int.Parse(cmbStaff.SelectedValue.ToString());
                    productSaleAdd.IdCustomer = int.Parse(cmbCustomer.SelectedValue.ToString());
                    productSaleAdd.IsDone = false;
                    dc.ProductSales.InsertOnSubmit(productSaleAdd);
                    dc.SubmitChanges();
                    this.Close();
                    pgProductSaleParam.Refresh_GUI("Purchase");
                }
                else
                {
                    //Remove all data matching Id in textbox
                    IMSDataContext dc = new IMSDataContext();
                    ProductSale productSaleUpdate = null;

                    productSaleUpdate = (from s in dc.ProductSales
                                     where (s.Id == idProductSale)
                                     select s).First();

                    if (productSaleUpdate != null)
                    {
                        //productSaleUpdate.Code = txtCode.Text;
                        productSaleUpdate.InvoiceNo = txtCode.Text;                       
                        productSaleUpdate.IdCustomer = int.Parse(cmbCustomer.SelectedValue.ToString());
                        productSaleUpdate.InvoiceDate = UString.GetLongFromDate(DateTime.Now);
                        productSaleUpdate.InvoiceDateEx = UString.GetDateStringFromLong(long.Parse(productSaleUpdate.InvoiceDate.ToString()));
                        productSaleUpdate.IdStaff = int.Parse(cmbStaff.SelectedValue.ToString());
                        productSaleUpdate.IdCustomer = int.Parse(cmbCustomer.SelectedValue.ToString());
                        //productSaleUpdate.IsDone = bool.Parse(chkIsDone.IsChecked.ToString());
                        //productSaleUpdate.IdObjectCare = idObjectCare;
                        dc.SubmitChanges();
                        this.Close();

                        pgProductSaleParam.Refresh_GUI("Purchase");                       

                    }
                }
            }
            catch(Exception ex)
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
