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
    public partial class frmProductPurchase : Window
    {        
        long idProductPurchase = -1;
        pgProductPurchase pgProductPurchaseParam = null;
        
        public frmProductPurchase(long idProductPurchase_, pgProductPurchase pgProductPurchase_, bool isDoneEdit)
        {
            InitializeComponent();

            if (IMS.General.GeneralParams.purchaseKind == IMS.General.GeneralParams.ProductPurchaseOrder)
            {
                txtStaff.Text = "Nhân viên đặt mua hàng";
                this.Title = "IMS - Chứng từ đặt mua hàng";

                lblVendor.Visibility = System.Windows.Visibility.Visible;
                grdVendor.Visibility = System.Windows.Visibility.Visible;
                lblCus.Visibility = System.Windows.Visibility.Hidden;
                grdCus.Visibility = System.Windows.Visibility.Hidden;

            }
            else if (IMS.General.GeneralParams.purchaseKind == IMS.General.GeneralParams.ProductPurchase)
            {
                txtStaff.Text = "Nhân viên mua hàng";
                this.Title = "IMS - Chứng từ mua hàng";

                lblVendor.Visibility = System.Windows.Visibility.Visible;
                grdVendor.Visibility = System.Windows.Visibility.Visible;
                lblCus.Visibility = System.Windows.Visibility.Hidden;
                grdCus.Visibility = System.Windows.Visibility.Hidden;

            }
            else if (IMS.General.GeneralParams.purchaseKind == IMS.General.GeneralParams.ProductReturn)
            {
                txtStaff.Text = "Nhân viên nhận hàng";
                this.Title = "IMS - Chứng từ nhận hàng trả";

                lblVendor.Visibility = System.Windows.Visibility.Hidden;
                grdVendor.Visibility = System.Windows.Visibility.Hidden;
                lblCus.Visibility = System.Windows.Visibility.Visible;
                grdCus.Visibility = System.Windows.Visibility.Visible;
            }

            //Thiết lập định dạng VN
            UString.SetSystem();

            //Khởi tạo DataContext
            IMSDataContext dc = new IMSDataContext();
            IMS_TableDataContext dcTable = new IMS_TableDataContext();
            //Gán giá trị cho các biến từ tham số truyền vào của hàm gọi          
            idProductPurchase = idProductPurchase_;
            pgProductPurchaseParam = pgProductPurchase_;

            //Load dữ liệu vào các combobox                      
            //Staff Combobox           
            var lst1 = (from s in dc.Staffs select s);
            cmbStaff.ItemsSource = lst1;
            cmbStaff.DisplayMemberPath = "Name";
            cmbStaff.SelectedValuePath = "Id";
            cmbStaff.SelectedValue = IMS.Properties.Settings.Default.IdStaff;

            //CarePerson Combobox 
            var lst2 = (from s in dc.Vendors select s);            
            cmbVendor.ItemsSource = lst2;
            cmbVendor.DisplayMemberPath = "Name";
            cmbVendor.SelectedValuePath = "Id";

            //Customer Combobox 
            var lst3 = (from s in dc.Customers select s);
            cmbCustomer.ItemsSource = lst3;
            cmbCustomer.DisplayMemberPath = "Name";
            cmbCustomer.SelectedValuePath = "Id";  
           
            

            if(isDoneEdit)
            {
                chkIsDone.IsEnabled = true;
            }
            else
            {
                chkIsDone.IsEnabled = false;
            }
            //Lấy dữ liệu từ idProductPurchase
            if (idProductPurchase != -1)
            {
                var queryProductPurchase = (from s in dcTable.ProductPurchase1s
                          where (s.Id == idProductPurchase)
                          select s).First();

                if (queryProductPurchase != null)
                {
                    txtCode.Text = queryProductPurchase.InvoiceNo;
                    dtCreatedDate.Text = queryProductPurchase.CreatedDateEx;
                    cmbStaff.SelectedValue = queryProductPurchase.IdStaff;
                    dtCareDate.Text = queryProductPurchase.InvoiceDateEx;
                    cmbVendor.SelectedValue = queryProductPurchase.IdVendor;
                    cmbCustomer.SelectedValue = queryProductPurchase.IdCustomer;
                    chkIsDone.IsChecked = queryProductPurchase.IsDone;
                }
            }
            else
            {
                dtCreatedDate.Text = DateTime.Now.ToShortDateString();
                dtCareDate.Text = DateTime.Now.ToShortDateString();
            }
            txtCode.Focus();
            txtCode.SelectAll();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idProductPurchase == -1)
                {
                    IMSDataContext dc = new IMSDataContext(); 
                    IMS_TableDataContext dcTable = new IMS_TableDataContext();                   
                    int curDiffNo = dc.ProcInvoiceNo("", "ProductPurchase") + 1;
                    DateTime dCurDate = DateTime.Now;

                    ProductPurchase1 productPurchaseAdd = new ProductPurchase1();

                    if (IMS.General.GeneralParams.purchaseKind == IMS.General.GeneralParams.ProductPurchaseOrder)
                    {
                        if (txtCode.Text == "")
                        {
                            productPurchaseAdd.InvoiceNo = "ĐM" + UString.Right(DateTime.Now.Year.ToString(), 2) + "." + UString.AddZeroBefore(curDiffNo.ToString(), 5);

                        }
                        else
                        {
                            productPurchaseAdd.InvoiceNo = txtCode.Text;

                        }


                        productPurchaseAdd.CreatedDate = long.Parse(UString.GetLongFromDate(dCurDate).ToString());
                        productPurchaseAdd.CreatedDateEx = dCurDate.ToShortDateString() + " " + dCurDate.ToShortTimeString();
                        productPurchaseAdd.InvoiceDate = UString.GetLongFromDate(DateTime.Parse(dtCareDate.Text));
                        productPurchaseAdd.InvoiceDateEx = UString.GetDateStringFromLong(long.Parse(productPurchaseAdd.InvoiceDate.ToString()));
                        productPurchaseAdd.IdStaff = long.Parse(cmbStaff.SelectedValue.ToString());
                        productPurchaseAdd.IdVendor = long.Parse(cmbVendor.SelectedValue.ToString());
                        productPurchaseAdd.IdCustomer = -1;
                        productPurchaseAdd.IsDone = false;
                        productPurchaseAdd.Kind = IMS.General.GeneralParams.purchaseKind;
                        dcTable.ProductPurchase1s.InsertOnSubmit(productPurchaseAdd);
                        dcTable.SubmitChanges();
                    }
                    else if (IMS.General.GeneralParams.purchaseKind == IMS.General.GeneralParams.ProductPurchase)
                    {
                        if (txtCode.Text == "")
                        {
                            productPurchaseAdd.InvoiceNo = "MH" + UString.Right(DateTime.Now.Year.ToString(), 2) + "." + UString.AddZeroBefore(curDiffNo.ToString(), 5);

                        }
                        else
                        {
                            productPurchaseAdd.InvoiceNo = txtCode.Text;

                        }

                        
                        productPurchaseAdd.CreatedDate = long.Parse(UString.GetLongFromDate(dCurDate).ToString());
                        productPurchaseAdd.CreatedDateEx = dCurDate.ToShortDateString() + " " + dCurDate.ToShortTimeString();
                        productPurchaseAdd.InvoiceDate = UString.GetLongFromDate(DateTime.Parse(dtCareDate.Text));
                        productPurchaseAdd.InvoiceDateEx = UString.GetDateStringFromLong(long.Parse(productPurchaseAdd.InvoiceDate.ToString()));
                        productPurchaseAdd.IdStaff = long.Parse(cmbStaff.SelectedValue.ToString());
                        productPurchaseAdd.IdVendor = long.Parse(cmbVendor.SelectedValue.ToString());
                        productPurchaseAdd.IdCustomer = -1;
                        productPurchaseAdd.IsDone = false;
                        productPurchaseAdd.Kind = IMS.General.GeneralParams.purchaseKind;
                        dcTable.ProductPurchase1s.InsertOnSubmit(productPurchaseAdd);
                        dcTable.SubmitChanges();
                    }
                    else if (IMS.General.GeneralParams.purchaseKind == IMS.General.GeneralParams.ProductReturn)
                    {
                        if (txtCode.Text == "")
                        {
                            productPurchaseAdd.InvoiceNo = "NH" + UString.Right(DateTime.Now.Year.ToString(), 2) + "." + UString.AddZeroBefore(curDiffNo.ToString(), 5);

                        }
                        else
                        {
                            productPurchaseAdd.InvoiceNo = txtCode.Text;

                        }

                        productPurchaseAdd.InvoiceNo = "NH" + UString.Right(DateTime.Now.Year.ToString(), 2) + "." + UString.AddZeroBefore(curDiffNo.ToString(), 5);

                        productPurchaseAdd.CreatedDate = long.Parse(UString.GetLongFromDate(dCurDate).ToString());
                        productPurchaseAdd.CreatedDateEx = dCurDate.ToShortDateString() + " " + dCurDate.ToShortTimeString();
                        productPurchaseAdd.InvoiceDate = UString.GetLongFromDate(DateTime.Parse(dtCareDate.Text));
                        productPurchaseAdd.InvoiceDateEx = UString.GetDateStringFromLong(long.Parse(productPurchaseAdd.InvoiceDate.ToString()));
                        productPurchaseAdd.IdStaff = long.Parse(cmbStaff.SelectedValue.ToString());
                        productPurchaseAdd.IdVendor = -1;
                        productPurchaseAdd.IdCustomer = long.Parse(cmbCustomer.SelectedValue.ToString());
                        productPurchaseAdd.IsDone = false;
                        productPurchaseAdd.Kind = IMS.General.GeneralParams.purchaseKind;
                        dcTable.ProductPurchase1s.InsertOnSubmit(productPurchaseAdd);
                        dcTable.SubmitChanges();
                    }
                                     
                    
                    this.Close();
                    pgProductPurchaseParam.Refresh_GUI("Purchase");

                }
                else
                {
                    IMSDataContext dc = new IMSDataContext();
                    IMS_TableDataContext dcTable = new IMS_TableDataContext();
                    ProductPurchase1 productPurchaseUpdate = null;

                    productPurchaseUpdate = (from s in dcTable.ProductPurchase1s
                                     where (s.Id == idProductPurchase)
                                     select s).First();

                    if (productPurchaseUpdate != null)
                    {
                        if (IMS.General.GeneralParams.purchaseKind == IMS.General.GeneralParams.ProductPurchase)
                        {
                            //productPurchaseUpdate.Code = txtCode.Text;
                            productPurchaseUpdate.InvoiceNo = txtCode.Text;
                            productPurchaseUpdate.IdStaff = IMS.Properties.Settings.Default.IdStaff;
                            productPurchaseUpdate.IdVendor = int.Parse(cmbVendor.SelectedValue.ToString());
                            productPurchaseUpdate.InvoiceDate = UString.GetLongFromDate(DateTime.Now);
                            productPurchaseUpdate.InvoiceDateEx = UString.GetDateStringFromLong(long.Parse(productPurchaseUpdate.InvoiceDate.ToString()));
                            productPurchaseUpdate.IdStaff = long.Parse(cmbStaff.SelectedValue.ToString());
                            productPurchaseUpdate.IdVendor = long.Parse(cmbVendor.SelectedValue.ToString());
                            productPurchaseUpdate.IdCustomer = -1;
                            //productPurchaseUpdate.IsDone = bool.Parse(chkIsDone.IsChecked.ToString());
                            //productPurchaseUpdate.IdObjectCare = idObjectCare;
                            dcTable.SubmitChanges();
                        }
                        else if (IMS.General.GeneralParams.purchaseKind == IMS.General.GeneralParams.ProductReturn)
                        {
                            //productPurchaseUpdate.Code = txtCode.Text;
                            productPurchaseUpdate.InvoiceNo = txtCode.Text;
                            productPurchaseUpdate.IdStaff = IMS.Properties.Settings.Default.IdStaff;
                            productPurchaseUpdate.IdVendor = int.Parse(cmbVendor.SelectedValue.ToString());
                            productPurchaseUpdate.InvoiceDate = UString.GetLongFromDate(DateTime.Now);
                            productPurchaseUpdate.InvoiceDateEx = UString.GetDateStringFromLong(long.Parse(productPurchaseUpdate.InvoiceDate.ToString()));
                            productPurchaseUpdate.IdStaff = long.Parse(cmbStaff.SelectedValue.ToString());
                            productPurchaseUpdate.IdVendor = -1;
                            productPurchaseUpdate.IdCustomer = long.Parse(cmbCustomer.SelectedValue.ToString());
                            //productPurchaseUpdate.IsDone = bool.Parse(chkIsDone.IsChecked.ToString());
                            //productPurchaseUpdate.IdObjectCare = idObjectCare;
                            dcTable.SubmitChanges();
                        }
                        this.Close();
                        pgProductPurchaseParam.Refresh_GUI("Purchase");                       

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
