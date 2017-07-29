using IMS;
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
    public partial class frmObjectCareDetail : Window
    {
        int idObjectCare = -1;
        int idObjectCareDetail = -1;
        pgObjectCareSaleOrder pgObjectCareDetailParam = null;
        pgObjectCareSale pgObjectCare = null;
        public frmObjectCareDetail(int idObjectCare_, int idObjectCareDetail_, pgObjectCareSaleOrder pgObjectCareDetail_, bool isDoneEdit)
        {
            InitializeComponent();

            //Thiết lập định dạng VN
            UString.SetSystem();

            //Khởi tạo DataContext
            IMSDataContext dc = new IMSDataContext();

            //Gán giá trị cho các biến từ tham số truyền vào của hàm gọi
            idObjectCare = idObjectCare_;
            idObjectCareDetail = idObjectCareDetail_;
            pgObjectCareDetailParam = pgObjectCareDetail_;

            //Load dữ liệu vào các combobox                      
            //Staff Combobox           
            var lst1 = (from s in dc.Staffs select s);
            cmbStaff.ItemsSource = lst1;
            cmbStaff.DisplayMemberPath = "Name";
            cmbStaff.SelectedValuePath = "Id";
            cmbStaff.SelectedValue = IMS.Properties.Settings.Default.IdStaff;

            //CarePerson Combobox 
            cmbCarePerson.ItemsSource = lst1;
            cmbCarePerson.DisplayMemberPath = "Name";
            cmbCarePerson.SelectedValuePath = "Id";

            //Assistance Combobox             
            cmbAssistance.ItemsSource = lst1;
            cmbAssistance.DisplayMemberPath = "Name";
            cmbAssistance.SelectedValuePath = "Id";

            if(isDoneEdit)
            {
                chkIsDone.IsEnabled = true;
            }
            else
            {
                chkIsDone.IsEnabled = false;
            }
            //Lấy dữ liệu từ idObjectCareDetail
            if (idObjectCareDetail != -1)
            {
                var queryObjectCareDetail = (from s in dc.ObjectCareDetailViews
                          where (s.Id == idObjectCareDetail)
                          select s).First();

                if (queryObjectCareDetail != null)
                {
                    txtCode.Text = queryObjectCareDetail.Code;
                    txtName.Text = queryObjectCareDetail.Name;
                    cmbStaff.SelectedValue = IMS.Properties.Settings.Default.IdStaff;
                    cmbCarePerson.SelectedValue = queryObjectCareDetail.IdCarePerson;
                    cmbAssistance.SelectedValue = queryObjectCareDetail.IdAssistance;
                    dtCreatedDate.Text = queryObjectCareDetail.CreatedDate;
                    dtCareDate.Text = queryObjectCareDetail.CareDate;
                    dtNextCareDate.Text = queryObjectCareDetail.NextCareDate;
                    chkIsDone.IsChecked = queryObjectCareDetail.IsDone;

                }
            }
            else
            {
                dtCreatedDate.Text = DateTime.Now.ToShortDateString();
            }
            txtCode.Focus();
            txtCode.SelectAll();
        }

        public frmObjectCareDetail(int idObjectCare_, int idObjectCareDetail_, pgObjectCareSale pgObjectCare_, bool isDoneEdit)
        {
            InitializeComponent();

            //Thiết lập định dạng VN
            UString.SetSystem();

            //Khởi tạo DataContext
            IMSDataContext dc = new IMSDataContext();

            //Gán giá trị cho các biến từ tham số truyền vào của hàm gọi
            idObjectCare = idObjectCare_;
            idObjectCareDetail = idObjectCareDetail_;
            pgObjectCare = pgObjectCare_;

            //Load dữ liệu vào các combobox                      
            //Staff Combobox           
            var lst1 = (from s in dc.Staffs select s);
            cmbStaff.ItemsSource = lst1;
            cmbStaff.DisplayMemberPath = "Name";
            cmbStaff.SelectedValuePath = "Id";
            cmbStaff.SelectedValue = IMS.Properties.Settings.Default.IdStaff;

            //CarePerson Combobox 
            cmbCarePerson.ItemsSource = lst1;
            cmbCarePerson.DisplayMemberPath = "Name";
            cmbCarePerson.SelectedValuePath = "Id";

            //Assistance Combobox             
            cmbAssistance.ItemsSource = lst1;
            cmbAssistance.DisplayMemberPath = "Name";
            cmbAssistance.SelectedValuePath = "Id";

            if (isDoneEdit)
            {
                chkIsDone.IsChecked = true;
            }
            else
            {
                chkIsDone.IsChecked = false;
            }
            //Lấy dữ liệu từ idObjectCareDetail
            if (idObjectCareDetail != -1)
            {
                var queryObjectCareDetail = (from s in dc.ObjectCareDetailViews
                                             where (s.Id == idObjectCareDetail)
                                             select s).First();

                if (queryObjectCareDetail != null)
                {
                    txtCode.Text = queryObjectCareDetail.Code;
                    txtName.Text = queryObjectCareDetail.Name;
                    cmbStaff.SelectedValue = IMS.Properties.Settings.Default.IdStaff;
                    cmbCarePerson.SelectedValue = queryObjectCareDetail.IdCarePerson;
                    cmbAssistance.SelectedValue = queryObjectCareDetail.IdAssistance;
                    dtCreatedDate.Text = queryObjectCareDetail.CreatedDate;
                    dtCareDate.Text = queryObjectCareDetail.CareDate;
                    dtNextCareDate.Text = queryObjectCareDetail.NextCareDate;
                    chkIsDone.IsChecked = queryObjectCareDetail.IsDone;

                }
            }
            else
            {
                dtCreatedDate.Text = DateTime.Now.ToShortDateString();
            }
            txtCode.Focus();
            txtCode.SelectAll();
        }

        public frmObjectCareDetail(int idObjectCare_, int idObjectCareDetail_, pgObjectCareManatement pgObjectCare_, bool isDoneEdit)
        {
            InitializeComponent();

            //Thiết lập định dạng VN
            UString.SetSystem();

            //Khởi tạo DataContext
            IMSDataContext dc = new IMSDataContext();

            //Gán giá trị cho các biến từ tham số truyền vào của hàm gọi
            idObjectCare = idObjectCare_;
            idObjectCareDetail = idObjectCareDetail_;
            //pgObjectCare = pgObjectCare_;

            //Load dữ liệu vào các combobox                      
            //Staff Combobox           
            var lst1 = (from s in dc.Staffs select s);
            cmbStaff.ItemsSource = lst1;
            cmbStaff.DisplayMemberPath = "Name";
            cmbStaff.SelectedValuePath = "Id";
            cmbStaff.SelectedValue = IMS.Properties.Settings.Default.IdStaff;

            //CarePerson Combobox 
            cmbCarePerson.ItemsSource = lst1;
            cmbCarePerson.DisplayMemberPath = "Name";
            cmbCarePerson.SelectedValuePath = "Id";

            //Assistance Combobox             
            cmbAssistance.ItemsSource = lst1;
            cmbAssistance.DisplayMemberPath = "Name";
            cmbAssistance.SelectedValuePath = "Id";

            if (isDoneEdit)
            {
                chkIsDone.IsChecked = true;
            }
            else
            {
                chkIsDone.IsChecked = false;
            }
            //Lấy dữ liệu từ idObjectCareDetail
            if (idObjectCareDetail != -1)
            {
                var queryObjectCareDetail = (from s in dc.ObjectCareDetailViews
                                             where (s.Id == idObjectCareDetail)
                                             select s).First();

                if (queryObjectCareDetail != null)
                {
                    txtCode.Text = queryObjectCareDetail.Code;
                    txtName.Text = queryObjectCareDetail.Name;
                    cmbStaff.SelectedValue = IMS.Properties.Settings.Default.IdStaff;
                    cmbCarePerson.SelectedValue = queryObjectCareDetail.IdCarePerson;
                    cmbAssistance.SelectedValue = queryObjectCareDetail.IdAssistance;
                    dtCreatedDate.Text = queryObjectCareDetail.CreatedDate;
                    dtCareDate.Text = queryObjectCareDetail.CareDate;
                    dtNextCareDate.Text = queryObjectCareDetail.NextCareDate;
                    chkIsDone.IsChecked = queryObjectCareDetail.IsDone;

                }
            }
            else
            {
                dtCreatedDate.Text = DateTime.Now.ToShortDateString();
            }
            txtCode.Focus();
            txtCode.SelectAll();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idObjectCareDetail == -1)
                {
                    IMSDataContext dc = new IMSDataContext();
                    ObjectCareDetail objectCareDetailAdd = new ObjectCareDetail();
                    int curDiffNo = dc.ProcInvoiceNo("", "ObjectCareDetail") + 1;
                    if (curDiffNo <= 0)
                    {
                        objectCareDetailAdd.Code = "ĐS"  + UString.Right(DateTime.Now.Year.ToString(), 2) + "." + UString.AddZeroBefore("1", 5);
                        objectCareDetailAdd.DiffNo = 1;
                    }
                    else
                    {                       
                        objectCareDetailAdd.Code = "ĐS" + UString.Right(DateTime.Now.Year.ToString(), 2) + "." + UString.AddZeroBefore(curDiffNo.ToString(), 5);
                        objectCareDetailAdd.DiffNo = curDiffNo;
                    }
                    objectCareDetailAdd.Name = txtName.Text;
                    objectCareDetailAdd.IdStaff = IMS.Properties.Settings.Default.IdStaff;
                    objectCareDetailAdd.IdCarePerson = int.Parse(cmbCarePerson.SelectedValue.ToString());
                    objectCareDetailAdd.IdAssistance = int.Parse(cmbAssistance.SelectedValue.ToString());
                    objectCareDetailAdd.CreatedDate = DateTime.Now.ToShortDateString();
                    objectCareDetailAdd.CareDate = dtCareDate.Text;
                    objectCareDetailAdd.NextCareDate = dtNextCareDate.Text;
                    objectCareDetailAdd.IsDone = bool.Parse(chkIsDone.IsChecked.ToString());
                    objectCareDetailAdd.IdObjectCare = idObjectCare;
                    dc.ObjectCareDetails.InsertOnSubmit(objectCareDetailAdd);
                    dc.SubmitChanges();
                    this.Close();
                    pgObjectCareDetailParam.Page_Refresh(objectCareDetailAdd);               
                    
                }
                else
                {
                    //Remove all data matching Id in textbox
                    IMSDataContext dc = new IMSDataContext();
                    ObjectCareDetail objectCareDetailUpdate = null;

                    objectCareDetailUpdate = (from s in dc.ObjectCareDetails
                                     where (s.Id == idObjectCareDetail)
                                     select s).First();

                    if (objectCareDetailUpdate != null)
                    {
                        //objectCareDetailUpdate.Code = txtCode.Text;
                        objectCareDetailUpdate.Name = txtName.Text;
                        objectCareDetailUpdate.IdStaff = IMS.Properties.Settings.Default.IdStaff;
                        objectCareDetailUpdate.IdCarePerson = int.Parse(cmbCarePerson.SelectedValue.ToString());
                        objectCareDetailUpdate.IdAssistance = int.Parse(cmbAssistance.SelectedValue.ToString());
                        objectCareDetailUpdate.CreatedDate = dtCreatedDate.Text;
                        objectCareDetailUpdate.CareDate = dtCareDate.Text;
                        objectCareDetailUpdate.NextCareDate = dtNextCareDate.Text;
                        objectCareDetailUpdate.IsDone = bool.Parse(chkIsDone.IsChecked.ToString());
                        //objectCareDetailUpdate.IdObjectCare = idObjectCare;
                        dc.SubmitChanges();
                        this.Close();
                        if(pgObjectCare != null)
                        {
                            ;
                        }
                        else
                        {
                            pgObjectCareDetailParam.Page_Refresh(objectCareDetailUpdate);
                        }
                        
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
