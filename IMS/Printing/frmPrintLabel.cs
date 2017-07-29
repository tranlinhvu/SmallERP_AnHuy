using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using QRCoder;
namespace IMS.Printing
{
    public partial class frmPrintLabel : Form
    {

        public frmPrintLabel()
        {
            InitializeComponent();            
        }

        private int m_currentPageIndex;
        private IList<Stream> m_streams;       

        public byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        private byte[] RenderQrCode(string s)
        {
            try
            {
                string level = "Q";
                QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(s, eccLevel))
                    {
                        using (QRCode qrCode = new QRCode(qrCodeData))
                        {
                            Image g = qrCode.GetGraphic(50, Color.Black, Color.White, null, 0);
                            return ImageToByte(g);
                        }
                    }
                }
            }
            catch
            {
                return null;
            }

        }
        public frmPrintLabel(string productBarcode, string productKind, string productColor, string productSize, string manufName)
        {
            try
            {
                InitializeComponent();
               
                //fill dataset in some of your method:
                using (SqlConnection conn = new SqlConnection(IMS.Properties.Settings.Default.SmallERPConnectionString))
                {
                    DataSet ds = new DataSet();
                    ds.DataSetName = "abc";

                    DataTable barcode = new DataTable("Barcode");
                    ds.Tables.Add(barcode);
                    string query1 = "SELECT Id,  qr FROM Barcode";
                    using (SqlDataAdapter da = new SqlDataAdapter(query1, conn))
                        da.Fill(ds.Tables[0]);

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                        b.IncludeLabel = true;

                        b.Alignment = BarcodeLib.AlignmentPositions.CENTER;
                        //b.RotateFlipType = RotateFlipType.Rotate180FlipNone;
                        b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;
                        row[0] = ImageToByte(b.Encode(BarcodeLib.TYPE.CODE128, productBarcode, System.Drawing.Color.Black, System.Drawing.Color.White, 250, 130));
                        row[1] = RenderQrCode("HUONG TRINH FABRIC COMPANY - ADDRESS: 766/37 LAC LONG QUAN, P9 TAN BINH, TP. CHCM - PHONE: 0915035788");
                    }
                   
                    //provide local report information to viewer         
                    reportViewer1.LocalReport.ReportEmbeddedResource = "IMS.Printing.rptLabel.rdlc";
                    ReportParameter[] RptParameters = new ReportParameter[5];//declare the number of parameters
                    ReportParameter p1 = new ReportParameter("barcode", productBarcode);
                    ReportParameter p2 = new ReportParameter("kind", productKind);
                    ReportParameter p3 = new ReportParameter("color", productColor);
                    ReportParameter p4 = new ReportParameter("size", productSize);
                    ReportParameter p5 = new ReportParameter("manuf", manufName);

                    this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5 });

                    //prepare report data source        
                    ReportDataSource rds = new ReportDataSource();
                    rds.Name = "DataSet1";
                    rds.Value = ds.Tables[0];
                    reportViewer1.LocalReport.DataSources.Add(rds);

                    reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);

                    Export(reportViewer1.LocalReport);
                    Print(1);

                    reportViewer1.Show();
                    //this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmPrintReceipt_Load(object sender, EventArgs e)
        {


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
                <PageWidth>2.95276in</PageWidth>
                <PageHeight>1.5748in</PageHeight>
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
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
               296, 158);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

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
            //printDoc.PrinterSettings.PrinterName = "LabelPrinter";
            if (!printDoc.PrinterSettings.IsValid)
            {
                MessageBox.Show("Không tìm thấy máy in", "IMS - Thông báo lỗi");
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                PaperSize pkCustomSize1 = new PaperSize("First custom size", 296, 158);

                printDoc.DefaultPageSettings.PaperSize = pkCustomSize1;
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.DefaultPageSettings.PrinterSettings.Copies = copies;
                printDoc.Print();
            }
        }

        private void frmPrintLabel_Load(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
        // Create a local report for Report.rdlc, load the data,
        //    export the report to an .emf file, and print it.   
    }
}

