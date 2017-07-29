using IMS.Favorite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for frmObjectCare.xaml
    /// </summary>
    public partial class frmObjectCare : Window
    {
        pgObjectCareSaleOrder pgObjectCareSaleOrderParam = null;
        int idCustomer = -1;

        int idObjectCare = -1; 
       
        public frmObjectCare( )
        {
            InitializeComponent();
        }

        public frmObjectCare(int idCustomer_, int idObjectCare_, pgObjectCareSaleOrder pgObjectCareSaleOrderParam_)
        {
            try
            {
                

                InitializeComponent();
                idCustomer = idCustomer_;
                idObjectCare = idObjectCare_;
                pgObjectCareSaleOrderParam = pgObjectCareSaleOrderParam_;

                if (idCustomer < 0)
                {
                    MessageBox.Show("Hãy chọn khách hàng trước", "IMS - Thông báo lỗi");
                    this.Close();
                    return;
                }
                UString.SetSystem();
                IMSDataContext dc = new IMSDataContext();
                ObjectCare sQuery = null;

                //Staff Combobox           
                var lst1 = (from s in dc.Staffs select s);
                cmbStaff.ItemsSource = lst1;
                cmbStaff.DisplayMemberPath = "Name";
                cmbStaff.SelectedValuePath = "Id";

                //Customer Combobox            
                var lst = (from s in dc.Customers select s);
                cmbCustomer.ItemsSource = lst;
                cmbCustomer.DisplayMemberPath = "Name";
                cmbCustomer.SelectedValuePath = "Id";

                Customer customer = null;
                customer = (from s in dc.Customers
                            where (s.Id == idCustomer)
                            select s).First();
                cmbCustomer.SelectedItem = customer;

                Staff staff = null;
                staff = (from s in dc.Staffs
                         where (s.Id == IMS.Properties.Settings.Default.IdStaff)
                         select s).First();
                cmbStaff.SelectedItem = staff;

                if (idObjectCare != -1)
                {
                    sQuery = (from s in dc.ObjectCares
                              where (s.Id == idObjectCare)
                              select s).First();

                    if (sQuery != null)
                    {
                        txtCode.Text = sQuery.Code;
                        txtName.Text = sQuery.Name;
                        txtNote.Text = sQuery.Note;
                    }
                }
            }
            catch
            {
                ;
            }
        }       

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (idObjectCare == -1)
            {
                IMSDataContext dc = new IMSDataContext();
                ObjectCare objectCare = new ObjectCare();
                int curDiffNo = dc.ProcInvoiceNo("", "ObjectCare") + 1;
                if (curDiffNo <= 0)
                {
                    objectCare.Code = "SC" + UString.Right(DateTime.Now.Year.ToString(), 2) + "." + UString.AddZeroBefore("1", 5);
                    objectCare.DiffNo = 1;
                }
                else
                {                    
                    objectCare.Code = "SC" + UString.Right(DateTime.Now.Year.ToString(), 2) + "." + UString.AddZeroBefore(curDiffNo.ToString(), 5);
                    objectCare.DiffNo = curDiffNo;
                }
               
                objectCare.Name = txtName.Text;
                objectCare.Note = txtNote.Text;
                objectCare.CreatedDate = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                objectCare.IdCustomer = int.Parse(cmbCustomer.SelectedValue.ToString());
                objectCare.IdStaff = int.Parse(cmbStaff.SelectedValue.ToString());

                dc.ObjectCares.InsertOnSubmit(objectCare);
                dc.SubmitChanges();
                this.Close();

                pgObjectCareSaleOrderParam.Page_Refresh(objectCare);

            }
            else
            {
                //Remove all data matching Id in textbox
                IMSDataContext dc = new IMSDataContext();
                ObjectCare objectCare = new ObjectCare();

                objectCare = (from s in dc.ObjectCares
                            where (s.Id == idObjectCare)
                            select s).First();

                if (objectCare != null)
                {
                    objectCare.Name = txtName.Text;
                    objectCare.Note = txtNote.Text;
                    objectCare.IdCustomer = int.Parse(cmbCustomer.SelectedValue.ToString());
                    objectCare.IdStaff = int.Parse(cmbStaff.SelectedValue.ToString());
                    dc.SubmitChanges();
                    this.Close();

                    pgObjectCareSaleOrderParam.Page_Refresh(objectCare);
                }               
            }            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
