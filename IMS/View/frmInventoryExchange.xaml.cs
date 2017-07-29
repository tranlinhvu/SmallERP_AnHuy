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
    public partial class frmInventoryExchange : Window
    {        
        long idProductSale = -1;
        pgProductSale pgProductSaleParam = null;
        pgInventoryExchange pgInventoryOutputParam = null;

        public frmInventoryExchange(long idProductSale_, pgInventoryExchange pgProductSale_, bool isDoneEdit, int i)
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
                pgInventoryOutputParam = pgProductSale_;

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
            catch (Exception ex)
            {
                ;
            }
        }
        public frmInventoryExchange(long idProductSale_, pgProductSale pgProductSale_, bool isDoneEdit)
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
                    IMS_TableDataContext dc = new IMS_TableDataContext();
                    IMSDataContext dcProc = new IMSDataContext();
                    int curDiffNo = dcProc.ProcInvoiceNo("", "ProductSale") + 1;
                    ProductSale1 productSaleAdd = new ProductSale1();
                    productSaleAdd.InvoiceNo = txtCode.Text;
                    if (txtCode.Text == "")
                    {
                        if (IMS.General.GeneralParams.saleKind == IMS.General.GeneralParams.ProductSale)
                        {
                            this.Title = "IMS - Bán hàng";
                            productSaleAdd.InvoiceNo = "BH" + UString.Right(DateTime.Now.Year.ToString(), 2) + "." + UString.AddZeroBefore(curDiffNo.ToString(), 5);
                            productSaleAdd.IsGiven = false;
                        }
                        else if (IMS.General.GeneralParams.saleKind == IMS.General.GeneralParams.ProductGiven)
                        {
                            this.Title = "IMS - Ký gửi hàng";
                            productSaleAdd.InvoiceNo = "KG" + UString.Right(DateTime.Now.Year.ToString(), 2) + "." + UString.AddZeroBefore(curDiffNo.ToString(), 5);
                            productSaleAdd.IsGiven = true;                            
                        }
                    }

                    productSaleAdd.DiffNo = curDiffNo;
                    productSaleAdd.CreatedDate = UString.GetLongFromDate(dCurDate);
                    productSaleAdd.CreatedDateEx = dCurDate.ToString("dd-MM-yyyy HH:mm:ss");
                    productSaleAdd.InvoiceDate = UString.GetLongFromDate(DateTime.Parse(dtCareDate.Text));
                    productSaleAdd.InvoiceDateEx = UString.GetDateFromLong(productSaleAdd.InvoiceDate.Value).ToString("dd-MM-yyyy");
                    productSaleAdd.IdStaff = int.Parse(cmbStaff.SelectedValue.ToString());
                    productSaleAdd.IdCustomer = int.Parse(cmbCustomer.SelectedValue.ToString());
                    productSaleAdd.IsDone = false;
                    productSaleAdd.Kind = IMS.General.GeneralParams.saleKind;
                    dc.ProductSale1s.InsertOnSubmit(productSaleAdd);
                    dc.SubmitChanges();

                    this.Close();
                    pgProductSaleParam.Refresh_GUI("Sale");
                }
                else
                {
                    //Remove all data matching Id in textbox
                    IMS_TableDataContext dc = new IMS_TableDataContext();
                    //IMSDataContext dcProc = new IMSDataContext();
                    ProductSale1 productSaleUpdate = null;

                    productSaleUpdate = (from s in dc.ProductSale1s
                                     where (s.Id == idProductSale)
                                     select s).First();

                    if (IMS.General.GeneralParams.saleKind == IMS.General.GeneralParams.ProductSale)
                    {
                        this.Title = "IMS - Bán hàng";                        
                        productSaleUpdate.IsGiven = false;
                    }
                    else if (IMS.General.GeneralParams.saleKind == IMS.General.GeneralParams.ProductGiven)
                    {
                        this.Title = "IMS - Ký gửi hàng";                        
                        productSaleUpdate.IsGiven = true;
                    }

                    if (productSaleUpdate != null)
                    {
                        //productSaleUpdate.Code = txtCode.Text;
                        productSaleUpdate.InvoiceNo = txtCode.Text;                       
                        productSaleUpdate.IdCustomer = int.Parse(cmbCustomer.SelectedValue.ToString());
                        productSaleUpdate.InvoiceDate = UString.GetLongFromDate(DateTime.Now);
                        productSaleUpdate.InvoiceDateEx = UString.GetDateFromLong(productSaleUpdate.InvoiceDate.Value).ToString("dd-MM-yyyy");
                        productSaleUpdate.IdStaff = int.Parse(cmbStaff.SelectedValue.ToString());
                        productSaleUpdate.IdCustomer = int.Parse(cmbCustomer.SelectedValue.ToString());
                        //productSaleUpdate.IsDone = bool.Parse(chkIsDone.IsChecked.ToString());
                        //productSaleUpdate.IdObjectCare = idObjectCare;
                        dc.SubmitChanges();
                        this.Close();

                        pgProductSaleParam.Refresh_GUI("Sale");                  

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
