using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS.DBHelper;
using IMS.Favorite;
using IMS.Report;
using System.Threading;

namespace IMS.Report
{
    public partial class frmSaleReportCall : Form
    {
        public frmSaleReportCall()
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
        private void frmSaleReportCall_Load(object sender, EventArgs e)
        {
            BindingSource bindingSourceVendor = new BindingSource();
            bindingSourceVendor.DataSource = SqlDataConnection.GetData("select * from Customer Order by Name ASC");
            cmbCustomer.DataSource = bindingSourceVendor;
            cmbCustomer.DisplayMember = "Name";
            cmbCustomer.ValueMember = "Id";

            BindingSource bindingSourceStaff = new BindingSource();
            bindingSourceStaff.DataSource = SqlDataConnection.GetData("select * from Staff Order by Name ASC");
            cmbStaff.DataSource = bindingSourceStaff;
            cmbStaff.DisplayMember = "Name";
            cmbStaff.ValueMember = "Id";

            BindingSource bindingSourceProductKind = new BindingSource();
            bindingSourceProductKind.DataSource = SqlDataConnection.GetData("select * from ProductKind Order by Name ASC");
            cmbProductKind.DataSource = bindingSourceProductKind;
            cmbProductKind.DisplayMember = "Name";
            cmbProductKind.ValueMember = "Id";

            //BindingSource bindingSourceProductKindColor = new BindingSource();
            //bindingSourceProductKindColor.DataSource = SqlDataConnection.GetData("select * from ProductKindColorView Order by ProductColorName ASC");
            //cmbProductColor.DataSource = bindingSourceProductKindColor;
            //cmbProductColor.DisplayMember = "ProductColorName";
            //cmbProductColor.ValueMember = "Id";

            SetSystem();

            dtFromDate.Format = DateTimePickerFormat.Custom;
            dtFromDate.CustomFormat = "dd/MM/yyyy";

            dtToDate.Format = DateTimePickerFormat.Custom;
            dtToDate.CustomFormat = "dd/MM/yyyy";
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

        private void chkStaff_CheckedChanged(object sender, EventArgs e)
        {
            cmbStaff.Enabled = chkStaff.Checked;
        }

        private void chkCustomer_CheckedChanged(object sender, EventArgs e)
        {
            cmbCustomer.Enabled = chkCustomer.Checked;
        }

        private void chkProductKind_CheckedChanged(object sender, EventArgs e)
        {
            cmbProductKind.Enabled = chkProductKind.Checked;
        }

        private void chkProductColor_CheckedChanged(object sender, EventArgs e)
        {
            cmbProductColor.Enabled = chkProductColor.Checked;
        }
    }
}
