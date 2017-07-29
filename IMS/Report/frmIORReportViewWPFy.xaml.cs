using IMS.DBHelper;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace IMS.Report
{
    /// <summary>
    /// Interaction logic for frmPurchaseReportViewWPF.xaml
    /// </summary>
    public partial class frmIORReportViewWPFy : Window
    {
        public frmIORReportViewWPFy()
        {
            InitializeComponent();           
            _reportViewer.Load += ReportViewer_Load;
        }

        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                string sqlString = string.Empty;

                DataSet dsReport = new DataSet();
                try
                {
                    InitializeComponent();

                    //fill dataset in some of your method:


                    dsReport.DataSetName = "DataSource_ProductPurchaseItemView";
                    sqlString = "select * from ProductPurchaseItemView";
                    dsReport = SqlDataConnection.GetDataSet(sqlString);

                    _reportViewer.LocalReport.ReportEmbeddedResource = "IMS.Report.rptPurchase.rdlc";

                    ////pass parameter
                    //ReportParameter[] RptParameters = new ReportParameter[2];//declare the number of parameters
                    //ReportParameter p1 = new ReportParameter("fromDate", UString.GetDateFromLong(fromDate).ToShortDateString());// first parameter 
                    //ReportParameter p2 = new ReportParameter("toDate", UString.GetDateFromLong(toDate).ToShortDateString());//second parameter 
                    //ReportParameter p3 = new ReportParameter("issuedDate", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("HH:mm:ss"));
                    //ReportParameter p4 = new ReportParameter("emp", empName);
                    //ReportParameter p5 = new ReportParameter("purchaseType", purchaseTypeName);
                    //ReportParameter p6 = new ReportParameter("pharmaName", pharmaName);
                    //ReportParameter p7 = new ReportParameter("pharmaAddress", pharmaAddress);
                    //this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7 });

                    //prepare report data source        
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = "DataSet_ProductPurchaseItemView";
                    rds.Value = dsReport.Tables[0];
                    _reportViewer.LocalReport.DataSources.Add(rds);

                    _reportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

                    //load report viewer      
                    _reportViewer.RefreshReport();
                    _isReportViewerLoaded = true;

                }
                catch (Exception ex)
                {
                    //display generic error message back to user         
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //check if connection is still open then attempt to close it        
                    ;
                }     

                
            }
        }
    }
}
