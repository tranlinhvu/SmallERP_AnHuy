using IMS.DBHelper;
using IMS.Favorite;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows;

namespace IMS.Printing
{
    /// <summary>
    /// Interaction logic for frmPrintReceipt.xaml
    /// </summary>
    public partial class frmPrintReceipt1 : Window
    {
        int idObjectCareDetail = -1;
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        public frmPrintReceipt1(int idObjectCareDetail_)
        {
            InitializeComponent();
            idObjectCareDetail = idObjectCareDetail_;

            _reportViewer.Load += ReportViewer_Load;

        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            //Set system
            UString.SetSystem();

            //Get config          
            DataSet dsReport = new DataSet();

            try
            {
                string sqlReceipt = "select * from ObjectCareDetailView where Id =" + idObjectCareDetail.ToString();

                dsReport.DataSetName = "SmallERPDataSet";
                dsReport = SqlDataConnection.GetDataSet(sqlReceipt);

                
                //ReportParameter[] RptParameters = new ReportParameter[9];//declare the number of parameters
                //ReportParameter p1 = new ReportParameter("curDate", DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToShortDateString());// first parameter 
                //ReportParameter p2 = new ReportParameter("cusName", "");
                //ReportParameter p3 = new ReportParameter("totalAmount", string.Format("{0:C}", 1));               
                //ReportParameter p4 = new ReportParameter("paymentAmount", string.Format("{0:C}", 1));
                //ReportParameter p5 = new ReportParameter("balanceAmount", string.Format("{0:C}", 1));
                //ReportParameter p6 = new ReportParameter("objectCareCode", "");
                //ReportParameter p7 = new ReportParameter("objectCareDetailCode", "");
                //ReportParameter p8 = new ReportParameter("objectCareDetailName", "");               
                //ReportParameter p9 = new ReportParameter("nextCareDate", "");

                //this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8, p9 });

                //provide local report information to viewer         
                _reportViewer.LocalReport.ReportEmbeddedResource = "IMS.Printing.Report1.rdlc";

                //prepare report data source        
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1";
                rds.Value = dsReport.Tables[0];
                _reportViewer.LocalReport.DataSources.Add(rds);

                //MessageBox.Show("xyz");
                //Export(_reportViewer.LocalReport);
                //Print(2);

                //load report viewer   
                _reportViewer.Show();
                _reportViewer.RefreshReport();
                //this.Close();
            }
            catch (Exception ex)
            {
                //display generic error message back to user         
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }
            finally
            {
                //check if connection is still open then attempt to close it        
                ;
            }
        }

        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>2.83465in</PageWidth>
                <PageHeight>3.73622in</PageHeight>
                <MarginTop>0.0in</MarginTop>
                <MarginLeft>0.0in</MarginLeft>
                <MarginRight>0.0in</MarginRight>
                <MarginBottom>0.0in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
               280, 300);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(System.Drawing.Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Print(short copies)
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = "POS-80";
            if (!printDoc.PrinterSettings.IsValid)
            {
                MessageBox.Show("Không tìm thấy máy in", "IMS - Thông báo lỗi");
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                PaperSize pkCustomSize1 = new PaperSize("First custom size", 280, 300);

                printDoc.DefaultPageSettings.PaperSize = pkCustomSize1;
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.DefaultPageSettings.PrinterSettings.Copies = 1;
                printDoc.Print();
            }
        }
    }
}
