using IMS.DBHelper;
using IMS.Favorite;
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

namespace IMS.View
{
    /// <summary>
    /// Interaction logic for frmProductSize.xaml
    /// </summary>
    public partial class frmPurchaseExcelInput : Window
    {
        long idProductSize;
        frmProduct1 frmProduct_ = null;
        public frmPurchaseExcelInput(frmProduct1 _frmProduct)
        {
            try
            {
                InitializeComponent();
                frmProduct_ = _frmProduct;

                //Thiết lập định dạng VN
                UString.SetSystem();

                grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                //Khởi tạo DataContext
                IMSDataContext dc = new IMSDataContext();

                

                //Lấy dữ liệu từ ProductSizeView                
                List<ProductSize> ls = (from s in dc.ProductSizes
                                        select s).ToList();

                var list = ls.AsEnumerable().Select((ProductSize, index) => new ProductSize()
                {
                    RowNumber = index + 1,
                    Id = ProductSize.Id,
                    Lenth = ProductSize.Lenth,
                    Code = ProductSize.Code

                }).ToList();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }

        }

        public frmPurchaseExcelInput()
        {
            try
            {
                InitializeComponent();               

                //Thiết lập định dạng VN
                UString.SetSystem();

                grdAll.RowDefinitions[0].Height = new GridLength(0.8, GridUnitType.Star);
                grdAll.RowDefinitions[1].Height = new GridLength(0.6, GridUnitType.Star);
                grdAll.RowDefinitions[2].Height = new GridLength(5, GridUnitType.Star);

                //Khởi tạo DataContext
                IMSDataContext dc = new IMSDataContext();



                //Lấy dữ liệu từ ProductSizeView                
                List<ProductSize> ls = (from s in dc.ProductSizes
                                        select s).ToList();

                var list = ls.AsEnumerable().Select((ProductSize, index) => new ProductSize()
                {
                    RowNumber = index + 1,
                    Id = ProductSize.Id,
                    Lenth = ProductSize.Lenth,
                    Code = ProductSize.Code

                }).ToList();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "IMS - Nhóm hàng ";
            grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
            grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);           
        }

        private void tbrAddSProductSize_Click(object sender, RoutedEventArgs e)
        {
            idProductSize = -1;
            this.Title = "IMS - Thêm nhóm hàng ";

            grdAll.RowDefinitions[0].Height = new GridLength(110, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);                       
        }

        private void tbrEditProductSize_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "IMS - Sửa chữa nhóm hàng ";

            grdAll.RowDefinitions[0].Height = new GridLength(110, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);            

            //Lấy dữ liệu từ idProductSize
            IMSDataContext dc = new IMSDataContext();
            if (idProductSize != -1)
            {
                var queryProductSize = (from s in dc.ProductSizes
                                 where (s.Id == idProductSize)
                                 select s).First();

                if (queryProductSize != null)
                {                    
                    txtCode.Text = queryProductSize.Code;                   
                }
            }
            
        }

        private void lsViewProductSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var userView = e.AddedItems[0] as ProductSize;
                idProductSize = userView.Id;
            }
            catch
            {
                ;
            }
        }

        private void btnRemoveProductSize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //frmProduct_.Refresh_GUI("ProductSize");
        }

        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            //string AccessDBAsValue = string.Empty;
            //RegistryKey rkACDBKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes");
            //if (rkACDBKey != null)
            //{
            //    //int lnSubKeyCount = 0;
            //    //lnSubKeyCount =rkACDBKey.SubKeyCount; 
            //    foreach (string subKeyName in rkACDBKey.GetSubKeyNames())
            //    {
            //        if (subKeyName.Contains("Microsoft.ACE.OLEDB"))
            //        {
            //            MessageBox.Show(subKeyName);
            //        }
            //    }
            //}

            string file = @"D:\TLV\Blues\Sells\AnHuy\abc.xlsx";

            //System.Data.OleDb.OleDbConnection MyConnection;
            //System.Data.DataSet DtSet;
            //System.Data.OleDb.OleDbDataAdapter MyCommand;
            //MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.12.0;Data Source='c:\\abc.xlsx';Extended Properties=Excel 12.0;");
            //MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection);
            //MyCommand.TableMappings.Add("Table", "TestTable");
            //DtSet = new System.Data.DataSet();
            //MyCommand.Fill(DtSet);
            //dtgProductList.DataContext = DtSet.Tables[0].DefaultView;
            //MyConnection.Close();

            try
            {

                DataTable employeeData = SqlDataConnection.ReadExcelContents(file);
                DataRow r = employeeData.Rows[0];
                MessageBox.Show(r[0].ToString());
                dtgProductList.DataContext = employeeData;
            }
            catch (Exception ex)
            {
                ;
            }
                     
        }
    }
}
