using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using IMS.Business;
using IMS.DBHelper;
using IMS.Favorite;
using System.Threading;

namespace IMS
{
    public partial class frmCustomerCredit : Form
    {
        BindingSource bindingSourceCustomer;
        BindingSource bindingSourceCustomerPayment;

        string customerName = string.Empty;
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            try
            {
                if (msg.WParam.ToInt32() == (int)Keys.Escape)
                {
                    this.Close();
                }
                else
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Key Overrided Events Error:" + Ex.Message);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public frmCustomerCredit()
        {
            InitializeComponent();
        }

        public frmCustomerCredit(string customerName_)
        {           
            
            InitializeComponent();

            txtCustomerName.Text = customerName_;
        }

        public class DeleteCell : DataGridViewButtonCell
        {
            //Image del = Image.FromFile(SmartPOS.Properties.Resources.Delete_16x16);
            protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
                graphics.DrawImage(IMS.Properties.Resources.Delete_16x16, cellBounds);
            }
        }

        public class DeleteColumn : DataGridViewButtonColumn
        {
            public DeleteColumn()
            {
                this.CellTemplate = new DeleteCell();
                this.Width = 20;
                //set other options here 
            }
        }

        private static void SetSystem()
        {
            int[] ARR = { 3, 3, 3 };
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-US");
            System.Globalization.DateTimeFormatInfo dateTimeInfo = new System.Globalization.DateTimeFormatInfo();
            System.Globalization.NumberFormatInfo NumberInfo = new System.Globalization.NumberFormatInfo();

            dateTimeInfo.DateSeparator = "/";
            dateTimeInfo.LongDatePattern = "dd/MMM/yyyy";
            dateTimeInfo.ShortDatePattern = "dd/MM/yyyy";
            dateTimeInfo.LongTimePattern = "HH:mm:ss";
            dateTimeInfo.ShortTimePattern = "hh:mm tt";

            NumberInfo.CurrencySymbol = "";
            NumberInfo.CurrencyDecimalDigits = 0;
            NumberInfo.CurrencyDecimalSeparator = ",";
            NumberInfo.CurrencyGroupSizes = ARR;
            NumberInfo.CurrencyGroupSeparator = ".";
            NumberInfo.PositiveInfinitySymbol = " ";
            NumberInfo.NumberGroupSeparator = ".";

            //dateTimeInfo.SetAllDateTimePatterns = "dd/MM/yyyy,hh:mm:ss tt";
            cultureInfo.DateTimeFormat = dateTimeInfo;
            cultureInfo.NumberFormat = NumberInfo;
            Application.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
        private void frmPharmaCustomerList_Load(object sender, EventArgs e)
        {
            SetSystem();

            dtPaymentDate.Format = DateTimePickerFormat.Custom;
            dtPaymentDate.CustomFormat = "dd/MM/yyyy";

            // TODO: This line of code loads data into the 'sQLCustomer.Customer' table. You can move, or remove it, as needed.
            //dataGridView1 = new DataGridView();

            //dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn col = null;
            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "Id";
            col.Name = "Id";
            col.HeaderText = "TId";
            col.DefaultCellStyle.Format = "C0";
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            col.Visible = false;
            this.dataGridView2.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "PaymentNo";
            col.Name = "PaymentNo";
            col.HeaderText = "Số phiếu";
            //col.DefaultCellStyle.Format = "C0";
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridView2.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "PaymentDateEx";
            col.Name = "PaymentDateEx";
            col.HeaderText = "Ngày thanh toán";
            col.DefaultCellStyle.Format = "C0";
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView2.Columns.Add(col);

            col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = "PaymentAmout";
            col.Name = "PaymentAmout";
            col.HeaderText = "Số tiền thanh toán";
            col.DefaultCellStyle.Format = "C0";
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView2.Columns.Add(col);

            this.dataGridView2.Columns.Add(new DeleteColumn());

            //bindingSourceCustomer = new BindingSource();
            //bindingSourceCustomerPayment = new BindingSource();
            //bindingSourceCustomer.DataSource = SqlDataConnection.GetData("Select * from View_Customer_Credit_Rule Where Name not like '_@_%'");
            //dataGridView1.DataSource = bindingSourceCustomer;

            

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }       

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            ;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ;
        }

        private void FillPaymentArea(int _crediDays)
        {
            //try
            //{
            //    lblWarning.Visible = false;
            //    txtTotalAmount.Text = "";
            //    txtPaidAmount.Text = "";
            //    txtCreditAmount.Text = "";
            //    txtDueDate.Text = "";
            //    txtLastPaymentDate.Text = "";

            //    IMSDataContext dc = new IMSDataContext(IMS.Properties.Settings.Default.SmallERPConnectionString);
            //    string totalSaleAmount = "0";
            //    dc.ProcGetTotal(idCus, "Sale", ref totalSaleAmount);
            //    string totalPaymentAmount = "0";
            //    dc.ProcGetTotal(idCus, "Payment", ref totalPaymentAmount);

            //    long creditAmount = long.Parse(totalSaleAmount) - long.Parse(totalPaymentAmount);
            //    txtTotalAmount.Text = UString.ConvertToVNCurrency(totalSaleAmount);
            //    txtPaidAmount.Text = UString.ConvertToVNCurrency(totalPaymentAmount);
            //    txtCreditAmount.Text = UString.ConvertToVNCurrency(creditAmount.ToString());

            //    string lastPaymentDate = "";
            //    dc.ProcGetLastPaymentDate(idCus, ref lastPaymentDate);
            //    txtLastPaymentDate.Text = lastPaymentDate;
            //    DateTime t = DateTime.Parse(txtLastPaymentDate.Text).AddDays(_crediDays);
            //    txtDueDate.Text = t.ToString("dd-MM-yyyy");

            //    DateTime t1 = DateTime.Parse(txtDueDate.Text);
            //    if (t1 < DateTime.Now)
            //    {
            //        lblWarning.Visible = true;
            //    }
            //    else
            //    {
            //        lblWarning.Visible = false;
            //    }
            //}
            //catch
            //{
            //    ;
            //}
        }
        long idCus = -1;
        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            //try
            //{
            //    int creditDays = int.Parse(dataGridView1["CreditDays", e.RowIndex].Value.ToString());
            //    idCus = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

            //    bindingSourceCustomerPayment.DataSource = SqlDataConnection.GetData("Select * from CustomerPayment Where idCustomer = " + idCus);
            //    dataGridView2.DataSource = bindingSourceCustomerPayment;

            //    FillPaymentArea(creditDays);               

            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show("Error: " + ex.Message);
            //}
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            //bindingSourceCustomer.DataSource = SqlDataConnection.GetData("Select * from View_Customer_Credit_Rule Where Name not like '_@_%' and Name like N'%" + txtCustomerName.Text + "%'");
            //dataGridView1.DataSource = bindingSourceCustomer;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //SDBDataContext dc = new SDBDataContext(SmartPOS.Properties.Settings.Default.SmartStore_DanhHaConnectionString);

            //Customer1 cus = (from s in dc.Customer1s
            //                                      where s.Id == int.Parse(dataGridView1[0,e.RowIndex].Value.ToString())
            //                                        select s).First();
            //cus.CreditLimit = long.Parse(dataGridView1["CreditLimit", e.RowIndex].Value.ToString());
            //cus.CreditDays = long.Parse(dataGridView1["CreditDays", e.RowIndex].Value.ToString());
            //dc.SubmitChanges();

            

        }

        private void btnMakeNewPayment_Click(object sender, EventArgs e)
        {
            //if(idCus > 0)
            //{
            //    DateTime curDate = dtPaymentDate.Value;
            //    SDBDataContext dc = new SDBDataContext(SmartPOS.Properties.Settings.Default.SmartStore_DanhHaConnectionString);

            //    CustomerPayment cusPayment = new CustomerPayment();

            //    cusPayment.IdCustomer = idCus;
            //    cusPayment.PaymentNo = (long)UString.GetLongFromDate(curDate);
            //    cusPayment.PaymentDate = cusPayment.PaymentNo;
            //    cusPayment.PaymentDateEx = curDate.ToString("dd-MM-yyyy");
            //    cusPayment.PaymentAmout = long.Parse(txtPaymentAmount.Text);
            //    dc.CustomerPayments.InsertOnSubmit(cusPayment);

            //    dc.SubmitChanges();

            //    txtPaymentAmount.Text = "";
            //    txtPaymentAmount.Focus();

            //    bindingSourceCustomerPayment.DataSource = SqlDataConnection.GetData("Select * from CustomerPayment Where idCustomer = " + idCus + " order by PaymentDateEx ASC" );
            //    dataGridView2.DataSource = bindingSourceCustomerPayment;

            //    FillPaymentArea(int.Parse(dataGridView1["CreditDays", dataGridView1.CurrentRow.Index].Value.ToString()));
            //}
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
            try
            {
                int creditDays = 0; // int.Parse(dataGridView1["CreditDays", dataGridView1.CurrentRow.Index].Value.ToString());
                //idCus = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());

                bindingSourceCustomerPayment.DataSource = SqlDataConnection.GetData("Select * from CustomerPayment Where idCustomer = " + idCus + " order by PaymentDateEx ASC");
                dataGridView2.DataSource = bindingSourceCustomerPayment;

                FillPaymentArea(creditDays);

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (IMS.General.GeneralFuctions.DeleteTable("CustomerPayment", int.Parse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString())) > 0)
                {
                    bindingSourceCustomerPayment.DataSource = SqlDataConnection.GetData("Select * from CustomerPayment Where idCustomer = " + idCus + " order by PaymentDateEx ASC");
                    dataGridView2.DataSource = bindingSourceCustomerPayment;

                    //FillPaymentArea(int.Parse(dataGridView1["CreditDays", dataGridView1.CurrentRow.Index].Value.ToString()));
                }
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        //    SDBDataContext dc = new SDBDataContext(SmartPOS.Properties.Settings.Default.SmartStore_DanhHaConnectionString);

        //    CustomerPayment cus = (from s in dc.CustomerPayments
        //                     where s.Id == int.Parse(dataGridView2[0, e.RowIndex].Value.ToString())
        //                     select s).First();
        //    DateTime paymentDate = new DateTime();
        //    try
        //    {
        //        paymentDate = DateTime.Parse(dataGridView2["PaymentDateEx", e.RowIndex].Value.ToString());
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Sai định dạng ngày tháng. Hãy thử lại với định dạng nn-tt-yyyy");
        //        return;
        //    }

        //    cus.PaymentNo = long.Parse(dataGridView2["PaymentNo", e.RowIndex].Value.ToString());
        //    cus.PaymentDate = (long)UString.GetLongFromDate(paymentDate);
        //    cus.PaymentDateEx = paymentDate.ToString("dd-MM-yyyy");
        //    cus.PaymentAmout = long.Parse(dataGridView2["PaymentAmout", e.RowIndex].Value.ToString());
        //    dc.SubmitChanges();

        //    FillPaymentArea(int.Parse(dataGridView1["CreditDays", dataGridView1.CurrentRow.Index].Value.ToString()));
        }         
    }
}
