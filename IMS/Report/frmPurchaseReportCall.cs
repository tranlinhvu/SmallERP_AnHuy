using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using IMS.DBHelper;
using IMS.Favorite;
using IMS.Report;


namespace IMS
{
    public partial class frmPurchaseReportCall : Form
    {
        private BindingSource bindingSourcePurchaseReport;
        private BindingSource bindingSourcePurchaseDetail;
        private BindingSource bindingSourceEmp;
        private BindingSource bindingSourceVendor;
        private BindingSource bindingSourceProductKind;
        private BindingSource bindingSourceInv;
        private BindingSource bindingSourcePharmaGroup;
        private BindingSource bindingSourcePurchaseType;

        int empId;
        string empName;
        int purchaseType;
        string purchaseTypeName;

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            try
            {
                if (msg.WParam.ToInt32() == (int)Keys.Escape)
                {
                    ;
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

        public frmPurchaseReportCall()
        {
            InitializeComponent();
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

        private double CalTotalAmount(DataGridView dtview, int col)
        {
            try
            {
                double totalAmount = 0;
                for (int i = 0; i < dtview.Rows.Count - 1; i++)
                {
                    totalAmount += double.Parse(dtview[col, i].Value.ToString());
                }
                return totalAmount;
            }
            catch
            {
                return 0;
            }
        }

        private void frmPurchaseReport_Load(object sender, EventArgs e)
        {
            try
            {
              
                BindingSource bindingSourceVendor = new BindingSource();
                bindingSourceVendor.DataSource = SqlDataConnection.GetData("select * from Vendor Order by Name ASC");
                cmbVendor.DataSource = bindingSourceVendor;
                cmbVendor.DisplayMember = "Name";
                cmbVendor.ValueMember = "Id";

                BindingSource bindingSourceProductKind = new BindingSource();
                bindingSourceProductKind.DataSource = SqlDataConnection.GetData("select * from ProductKind Order by Name ASC");
                cmbProductKind.DataSource = bindingSourceProductKind;
                cmbProductKind.DisplayMember = "Name";
                cmbProductKind.ValueMember = "Id";

                BindingSource bindingSourceProductKindColor = new BindingSource();
                bindingSourceProductKindColor.DataSource = SqlDataConnection.GetData("select * from ProductKindColorView Order by ProductColorName ASC");
                cmbProductColor.DataSource = bindingSourceProductKindColor;
                cmbProductColor.DisplayMember = "ProductColorName";
                cmbProductColor.ValueMember = "Id";
                
                SetSystem();

                dtFromDate.Format = DateTimePickerFormat.Custom;
                dtFromDate.CustomFormat = "dd/MM/yyyy";

                dtToDate.Format = DateTimePickerFormat.Custom;
                dtToDate.CustomFormat = "dd/MM/yyyy";

                //
                bindingSourcePurchaseReport = new BindingSource();
                bindingSourcePurchaseDetail = new BindingSource();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                dtFromDate.Text = dtFromDate.Value.ToShortDateString() + " 00:00:00";
                dtToDate.Text = dtToDate.Value.ToShortDateString() + " 23:59:59";

            }
            catch
            {
                ;
            }
        }

        private void optInvoice_CheckedChanged(object sender, EventArgs e)
        {
            


        }

        private void optPharmaDetail_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if ((e.ColumnIndex == 2) && (e.RowIndex >= 0))
            //    {
            //        dataGridView1[3, e.RowIndex].Value = UString.GetDateFromLong(ulong.Parse(dataGridView1[2, e.RowIndex].Value.ToString())).ToShortDateString();
            //    }
            //}
            //catch
            //{
            //    ;
            //}
        }

        private void frmPurchaseReport_Resize(object sender, EventArgs e)
        {
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //string pharmaGroup = "";
            //string kind = "";
            //string inv = "";
            //string vender = "";

            dtFromDate.Text = dtFromDate.Value.ToShortDateString() + " 00:00:00";
            dtToDate.Text = dtToDate.Value.ToShortDateString() + " 23:59:59";

            //string sqlString = "Select * from View_PurchaseEx";
            //sqlString += " WHERE (PurchaseDate >= " + UString.GetLongFromDate(DateTime.Parse(dtFromDate.Text)) + ")";
            //sqlString += " and (PurchaseDate <= " + UString.GetLongFromDate(DateTime.Parse(dtToDate.Text)) + ") and (PurchaseType = 1)";

            //if (chkPharmaCode.Checked)
            //{
            //    sqlString += " and (PharmaCode like N'%" + cmbPharmaCode.Text + "%'";
            //    sqlString += "  or Name like N'%" + cmbPharmaCode.Text + "%')";
            //}

            //if (chkPharmaGroup.Checked)
            //{
            //    pharmaGroup = cmbPharmaGroup.Text;
            //    sqlString += " and PharmaGroupName = N'" + pharmaGroup + "'";
            //}

            //if (chkKind.Checked)
            //{
            //    kind = cmbKind.Text;
            //    sqlString += " and Kind = N'" + kind + "'";
            //}

            //if (chkInventory.Checked)
            //{
            //    //InventoryCheckType = cmbInv.Text;

            //    inv = cmbInv.Text;
            //    sqlString += " and InventoryName = N'" + inv + "'";
            //}
            
            //if(chkEmp.Checked)
            //{
            //    vender = cmbEmp.Text;
            //    sqlString += " and VendorName like N'%" + vender + "%'";
            //}


            //frmReportPurchase frmReportPurchase_ = new frmReportPurchase(sqlString, UString.GetLongFromDate(DateTime.Parse(dtFromDate.Text)), UString.GetLongFromDate(DateTime.Parse(dtToDate.Text)), cmbPharmaCode.Text, kind, empId, empName, purchaseType, purchaseTypeName, SmartPOS.Business.LoginSession.PharmaName, SmartPOS.Business.LoginSession.PharmaAddress, 1);
            //frmReportPurchase_.Show();

            frmPurchaseReportViewWPF frmPurchaseReportView_ = new frmPurchaseReportViewWPF();
            //frmPurchaseReportView_.StartPosition = FormStartPosition.CenterScreen;
            frmPurchaseReportView_.Show();
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ;
        }

        private void chkEmp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cmbVendor.Enabled = chkEmp.Checked;
                if (cmbVendor.Enabled == false)
                {
                    empId = 0;
                    empName = "";
                }
                else
                {
                    empId = int.Parse(cmbVendor.SelectedValue.ToString());
                    empName = "Nhà cung cấp: " + cmbVendor.Text;
                }
            }
            catch
            {
                ;
            }
        }

        private void cmbEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                empId = int.Parse(cmbVendor.SelectedValue.ToString());
                empName = "Nhà cung cấp: " + cmbVendor.Text;
            }
            catch
            {
                ;
            }
        }

        private void chkPurchaseType_CheckedChanged(object sender, EventArgs e)
        {
            ;
        }

        private void cmbPurchaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ;

        }

        private void dataGridView5_Leave(object sender, EventArgs e)
        {
            ;
        }

        private void chkKind_CheckedChanged(object sender, EventArgs e)
        {
            cmbProductKind.Enabled = chkKind.Checked;
        }

        private void chkInventory_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void chkPharmaGroup_CheckedChanged(object sender, EventArgs e)
        {
            cmbProductColor.Enabled = chkPharmaGroup.Checked;
        }

        private void chkPharmaCode_CheckedChanged(object sender, EventArgs e)
        {
            //cmbPharmaCode.Enabled = chkPharmaCode.Checked;
        }

        private void cmbPharmaGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ;
            }
            catch
            {
                ;
            }
        }

        private void cmbProductKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindingSource bindingSourceProductKindColor = new BindingSource();
                string sql = "select * from ProductKindColorView where ProductKindName = N'" + cmbProductKind.Text + "' Order by ProductColorName ASC";
                bindingSourceProductKindColor.DataSource = SqlDataConnection.GetData(sql);
                cmbProductColor.DataSource = bindingSourceProductKindColor;
                cmbProductColor.DisplayMember = "ProductColorName";
                cmbProductColor.ValueMember = "Id";
            }
            catch
            {
                ;
            }
        }            
    }
}
