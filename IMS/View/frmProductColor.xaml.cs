using IMS.DBHelper;
using IMS.Favorite;
using IMS.General;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for frmProductColor.xaml
    /// </summary>
    ///    
    public partial class frmProductColor : Window
    {
        int idProductColor;
        int idProductKind;
        frmProduct1 frmProduct_ = null;
        pgProduct pgProduct_ = null;

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public frmProductColor(pgProduct _frmProduct, int _idProductKind)
        {
            try
            {
                InitializeComponent();
                pgProduct_ = _frmProduct;
                idProductKind = _idProductKind;

                //Thiet lap timer           
                //dispatcherTimer.Tick += dispatcherTimer_Tick;
                //dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                //dispatcherTimer.Start();

                //Thiết lập định dạng VN
                UString.SetSystem();

                grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                //grdAll.RowDefinitions[3].Height = new GridLength(0, GridUnitType.Star);
                //Khởi tạo DataContext
                IMSDataContext dc = new IMSDataContext();
                IMS_TableDataContext dcTable = new IMS_TableDataContext();
                //---ProductKind---
                List<ProductKind> kindList = (from s in dcTable.ProductKinds select s).ToList();
                ProductKind a = new ProductKind();
                a.Id = 0;
                a.Name = "Tất cả";
                kindList.Add(a);

                var varKindList = kindList.OrderBy(x => x.Id);
                cmbProductKind.ItemsSource = varKindList;
                cmbProductKind.DisplayMemberPath = "Name";
                cmbProductKind.SelectedValuePath = "Id";
                cmbProductKind.SelectedValue = idProductKind;                

                //Lấy dữ liệu từ ProductKindColorView                
                List<ProductKindColorView> ls = (from s in dc.ProductKindColorViews
                                                 where s.IdProductKind == idProductKind
                                                select s).OrderBy(x => x.ProductColorName).ToList();
                var list = ls.AsEnumerable().Select((ProductKindColorView, index) => new ProductKindColorView()
                {
                    RowNumber = index + 1,
                    IdProductColor = ProductKindColorView.IdProductColor,
                    ProductColorName = ProductKindColorView.ProductColorName,
                    ProductColorCode = ProductKindColorView.ProductColorCode

                }).ToList().OrderBy(x => x.ProductColorName);

                lsViewProductColor.ItemsSource = null;
                lsViewProductColor.ItemsSource = list;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }
        }

        public frmProductColor(frmProduct1 _frmProduct, int _idProductKind)
        {
            try
            {
                InitializeComponent();
                frmProduct_ = _frmProduct;
                idProductKind = _idProductKind;

                //Thiet lap timer           
                //dispatcherTimer.Tick += dispatcherTimer_Tick;
                //dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                //dispatcherTimer.Start();

                //Thiết lập định dạng VN
                UString.SetSystem();

                grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                //grdAll.RowDefinitions[3].Height = new GridLength(0, GridUnitType.Star);
                //Khởi tạo DataContext
                IMSDataContext dc = new IMSDataContext();
                IMS_TableDataContext dcTable = new IMS_TableDataContext();
                //---ProductKind---
                List<ProductKind> kindList = (from s in dcTable.ProductKinds select s).ToList();
                ProductKind a = new ProductKind();
                a.Id = 0;
                a.Name = "Tất cả";
                kindList.Add(a);

                var varKindList = kindList.OrderBy(x => x.Id);
                cmbProductKind.ItemsSource = varKindList;
                cmbProductKind.DisplayMemberPath = "Name";
                cmbProductKind.SelectedValuePath = "Id";
                cmbProductKind.SelectedValue = idProductKind;

                //Lấy dữ liệu từ ProductKindColorView                
                List<ProductKindColorView> ls = (from s in dc.ProductKindColorViews
                                                 where s.IdProductKind == idProductKind
                                                 select s).OrderBy(x => x.ProductColorName).ToList();
                var list = ls.AsEnumerable().Select((ProductKindColorView, index) => new ProductKindColorView()
                {
                    RowNumber = index + 1,
                    IdProductColor = ProductKindColorView.IdProductColor,
                    ProductColorName = ProductKindColorView.ProductColorName,
                    ProductColorCode = ProductKindColorView.ProductColorCode

                }).ToList().OrderBy(x => x.ProductColorName);

                lsViewProductColor.ItemsSource = null;
                lsViewProductColor.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // code goes here
            //this.pb.Value  = System.DateTime.Now.Second % 100;
        }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                IMSDataContext dc = new IMSDataContext();
                if (idProductColor == -1)
                {
                    if (idProductKind == 0)
                    {
                        MessageBox.Show("Hãy chọn loại hàng!", "IMS - Thông báo lỗi");
                        return;
                    }

                    //try
                    //{
                    //    var ProductColorAdd1 = (from s in dc.ProductColors
                    //                    where (s.Code == txtCode.Text)
                    //                       select s).First();
                    //    if (ProductColorAdd1 != null)
                    //    {
                    //        MessageBox.Show("Mã màu đã tồn tại!", "IMS - Thông báo lỗi");
                    //        return;
                    //    }
                    //}
                    //catch
                    //{
                    //    ;
                    //}

                    int result = dc.ProcInsertProductKindColor(idProductKind, txtName.Text, txtCode.Text);
                    if (result < 1)
                    {
                        MessageBox.Show("Thêm màu không thành công!", "IMS - Thông báo lỗi");
                        return;
                    }

                    grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                    this.Title = "IMS - Màu hàng hóa";
                }
                else
                {                    
                    ProductColor ProductColorUpdate = null;
                    ProductColorUpdate = (from s in dc.ProductColors
                                      where (s.Id == idProductColor)
                                     select s).First();

                    if (ProductColorUpdate != null)
                    {
                        ProductColorUpdate.Name = txtName.Text;
                        ProductColorUpdate.Code = txtCode.Text;
                        dc.SubmitChanges();

                        grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                        this.Title = "IMS - Nhóm màu ";
                    }
                }                

                lsViewProductColor.IsEnabled = true;
                //Lấy dữ liệu từ ProductKindColorView                
                List<ProductKindColorView> ls = (from s in dc.ProductKindColorViews
                                                 where s.IdProductKind == idProductKind
                                                 select s).OrderBy(x => x.ProductColorName).ToList();
                var list = ls.AsEnumerable().Select((ProductKindColorView, index) => new ProductKindColorView()
                {
                    RowNumber = index + 1,
                    IdProductColor = ProductKindColorView.IdProductColor,
                    ProductColorName = ProductKindColorView.ProductColorName,
                    ProductColorCode = ProductKindColorView.ProductColorCode

                }).ToList().OrderBy(x => x.ProductColorName);

                lsViewProductColor.ItemsSource = null;
                lsViewProductColor.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "IMS - Màu hàng hóa";
            grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
            grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
            lsViewProductColor.IsEnabled = true;
        }

        private void tbrAddSProductColor_Click(object sender, RoutedEventArgs e)
        {
            idProductColor = -1;
            this.Title = "IMS - Thêm màu ";

            grdAll.RowDefinitions[0].Height = new GridLength(110, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            lsViewProductColor.IsEnabled = false;

            txtName.Focus();
            txtName.SelectAll();
        }

        private void tbrEditProductColor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Title = "IMS - Chỉnh sửa màu";

                grdAll.RowDefinitions[0].Height = new GridLength(110, GridUnitType.Pixel);
                grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
                grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                lsViewProductColor.IsEnabled = false;

                //Lấy dữ liệu từ idProductColor
                IMSDataContext dc = new IMSDataContext();
                if (idProductColor != -1)
                {
                    var queryProductColor = (from s in dc.ProductKindColorViews
                                             where (s.IdProductColor == idProductColor)
                                             select s).First();

                    if (queryProductColor != null)
                    {
                        txtName.Text = queryProductColor.ProductColorName;
                        txtCode.Text = queryProductColor.ProductColorCode;
                    }
                }
                txtName.Focus();
                txtName.SelectAll();
            }
            catch
            {
                ;
            }
        }

        private void lsViewProductColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var userView = e.AddedItems[0] as ProductKindColorView;
                idProductColor = userView.IdProductColor.Value;
            }
            catch
            {
                ;
            }
        }

        private void btnRemoveProductColor_Click(object sender, RoutedEventArgs e)
        {
            if(GeneralFuctions.DeleteTable("ProductColor", idProductColor) > 0)
            {
                RefreshGUI();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (frmProduct_ != null)
            {
                frmProduct_.Refresh_GUI("ProductColor");
            }
        }
       
        private void tbrCopyColor_Click(object sender, RoutedEventArgs e)
        {
            idProductKind = int.Parse(cmbProductKind.SelectedValue.ToString());
            if (idProductKind > 0)
            {
                frmAddProductKindColor a = new frmAddProductKindColor(this, idProductKind);
                a.Show();
            }
            else
            {
                MessageBox.Show("Hãy chọn loại hàng", "IMS - Thông báo lỗi");
            }
        }

        private void tbrSearch_Click(object sender, RoutedEventArgs e)
        {
            idProductKind = int.Parse(cmbProductKind.SelectedValue.ToString());

            IMSDataContext dc = new IMSDataContext();
            //Lấy dữ liệu từ ProductKindColorView           

            List<ProductKindColorView> ls = null;
            if (idProductKind == 0)
            {
                ls = (from s in dc.ProductKindColorViews
                      select s).GroupBy(n => new { n.ProductColorName, n.ProductColorCode})
                                           .Select(g => g.FirstOrDefault())
                                           .OrderBy(x => x.ProductColorName)
                                           .ToList();
            }
            else if (idProductKind > 0)
            {
                ls = (from s in dc.ProductKindColorViews
                      where s.IdProductKind == idProductKind
                      select s).OrderBy(x => x.ProductColorName).ToList();
            }

            var list = ls.AsEnumerable().Select((ProductKindColorView, index) => new ProductKindColorView()
            {
                RowNumber = index + 1,
                IdProductColor = ProductKindColorView.IdProductColor,
                ProductColorName = ProductKindColorView.ProductColorName,
                ProductColorCode = ProductKindColorView.ProductColorCode

            }).ToList().OrderBy(x => x.ProductColorName);

            lsViewProductColor.ItemsSource = null;
            lsViewProductColor.ItemsSource = list;
        }
        private void tbrRefresh_Click(object sender, RoutedEventArgs e)
        {
            idProductKind = int.Parse(cmbProductKind.SelectedValue.ToString());

            IMSDataContext dc = new IMSDataContext();
            //Lấy dữ liệu từ ProductKindColorView           

            List<ProductKindColorView> ls = null;
            if (idProductKind == 0)
            {
                ls = (from s in dc.ProductKindColorViews
                      select s).GroupBy(n => new { n.ProductColorName, n.ProductColorCode })
                                           .Select(g => g.FirstOrDefault())
                                           .OrderBy(x => x.ProductColorName)
                                           .ToList();
            }
            else if (idProductKind > 0)
            {
                ls = (from s in dc.ProductKindColorViews
                      where s.IdProductKind == idProductKind
                      select s).OrderBy(x => x.ProductColorName).ToList();
            }

            var list = ls.AsEnumerable().Select((ProductKindColorView, index) => new ProductKindColorView()
            {
                RowNumber = index + 1,
                IdProductColor = ProductKindColorView.IdProductColor,
                ProductColorName = ProductKindColorView.ProductColorName,
                ProductColorCode = ProductKindColorView.ProductColorCode

            }).ToList().OrderBy(x => x.ProductColorName);

            lsViewProductColor.ItemsSource = null;
            lsViewProductColor.ItemsSource = list;
        }

        private void cmbProductKind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                idProductKind = int.Parse(cmbProductKind.SelectedValue.ToString());

                IMSDataContext dc = new IMSDataContext();

                //Lấy dữ liệu từ ProductKindColorView 
                List<ProductKindColorView> ls = null;
                if (idProductKind == 0)
                {
                    ls = (from s in dc.ProductKindColorViews
                          select s).GroupBy(n => new { n.ProductColorName, n.ProductColorCode })
                                               .Select(g => g.FirstOrDefault())
                                               .OrderBy(x => x.ProductColorName)
                                               .ToList();
                }
                else if (idProductKind > 0)
                {
                    ls = (from s in dc.ProductKindColorViews
                          where s.IdProductKind == idProductKind
                          select s).OrderBy(x => x.ProductColorName).ToList();
                }

                var list = ls.AsEnumerable().Select((ProductKindColorView, index) => new ProductKindColorView()
                {
                    RowNumber = index + 1,
                    IdProductColor = ProductKindColorView.IdProductColor,
                    ProductColorName = ProductKindColorView.ProductColorName,
                    ProductColorCode = ProductKindColorView.ProductColorCode

                }).ToList().OrderBy(x => x.ProductColorName);

                lsViewProductColor.ItemsSource = null;
                lsViewProductColor.ItemsSource = list;
            }
            catch
            {
                ;
            }
        }

        //public void Update1(int i)
        //{
        //    if (InvokeRequired)
        //    {
        //        this.BeginInvoke(new Action<int>(Update1), new object[] { i });
        //        return;
        //    }

        //    progressBar1.Value = i;
        //}
        private void tbrAddProductColorFromFile_Click(object sender, RoutedEventArgs e)
        {            
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "Excel Files (*.xls|*.xlsx" };
            var result = ofd.ShowDialog();
            if (result == false) return;
            string file = ofd.FileName;
            MyProgressBar pb = new MyProgressBar();
            pb.Show();
            try
            {
                
                IMSDataContext dc = new IMSDataContext();
                DataTable productColorData = SqlDataConnection.ReadExcelContents(file);
                
                idProductKind = int.Parse(cmbProductKind.SelectedValue.ToString());
                for (int i = 0; i < productColorData.Rows.Count; i++)
                {
                    DataRow row = productColorData.Rows[i];

                    string producColorName = row[0].ToString();
                    string producColorCode = row[1].ToString();
                    int resultInsert = dc.ProcInsertProductKindColor(idProductKind, producColorName, producColorCode);
                    //if (resultInsert < 1)
                    //{
                    //    MessageBox.Show("Thêm màu không thành công!", "IMS - Thông báo lỗi");
                    //    return;
                    //}

                }

                //Lấy dữ liệu từ ProductKindColorView 
                List<ProductKindColorView> ls = null;
                if (idProductKind == 0)
                {
                    ls = (from s in dc.ProductKindColorViews
                          select s).GroupBy(n => new { n.ProductColorName, n.ProductColorCode })
                                               .Select(g => g.FirstOrDefault())
                                               .OrderBy(x => x.ProductColorName)
                                               .ToList();
                }
                else if (idProductKind > 0)
                {
                    ls = (from s in dc.ProductKindColorViews
                          where s.IdProductKind == idProductKind
                          select s).OrderBy(x => x.ProductColorName).ToList();
                }

                var list = ls.AsEnumerable().Select((ProductKindColorView, index) => new ProductKindColorView()
                {
                    RowNumber = index + 1,
                    IdProductColor = ProductKindColorView.IdProductColor,
                    ProductColorName = ProductKindColorView.ProductColorName,
                    ProductColorCode = ProductKindColorView.ProductColorCode

                }).ToList().OrderBy(x => x.ProductColorName);

                lsViewProductColor.ItemsSource = null;
                lsViewProductColor.ItemsSource = list;
                //pb.Close();
            }
            catch (Exception ex)
            {
                ;
            } 
        }

        public void RefreshGUI()
        {
            IMSDataContext dc = new IMSDataContext();
            //Lấy dữ liệu từ ProductKindColorView           

            List<ProductKindColorView> ls = null;
            if (idProductKind == 0)
            {
                ls = (from s in dc.ProductKindColorViews
                      select s).GroupBy(n => new { n.ProductColorName, n.ProductColorCode })
                                           .Select(g => g.FirstOrDefault())
                                           .OrderBy(x => x.ProductColorName)
                                           .ToList();
            }
            else if (idProductKind > 0)
            {
                ls = (from s in dc.ProductKindColorViews
                      where s.IdProductKind == idProductKind
                      select s).OrderBy(x => x.ProductColorName).ToList();
            }

            var list = ls.AsEnumerable().Select((ProductKindColorView, index) => new ProductKindColorView()
            {
                RowNumber = index + 1,
                IdProductColor = ProductKindColorView.IdProductColor,
                ProductColorName = ProductKindColorView.ProductColorName,
                ProductColorCode = ProductKindColorView.ProductColorCode

            }).ToList().OrderBy(x => x.ProductColorName);

            lsViewProductColor.ItemsSource = null;
            lsViewProductColor.ItemsSource = list;
        }
        

        private void Window_Closed(object sender, EventArgs e)
        {
            if (frmProduct_ != null)
            {
                frmProduct_.Refresh_GUI("ProductColor");
            }
            else if (pgProduct_ != null)
            {
                pgProduct_.RefreshGUI1();
            }
        }
    }
}
