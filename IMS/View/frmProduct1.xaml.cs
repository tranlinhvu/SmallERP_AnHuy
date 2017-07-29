using IMS.Favorite;
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
    public partial class frmProduct1 : Window
    {
        int idProduct = -1;
        int idProductForProductKindChange = -1;
        pgProduct pgProductParam;
        public frmProduct1(int idProduct_, pgProduct pgProduct_)
        {
            InitializeComponent();

            //Thiết lập định dạng VN
            UString.SetSystem();

            //Khởi tạo DataContext
            IMSDataContext dc = new IMSDataContext();

            //Gán giá trị cho các biến từ tham số truyền vào của hàm gọi
            idProduct = idProduct_;
            idProductForProductKindChange = idProduct_;
            pgProductParam = pgProduct_;

            //Load dữ liệu vào các combobox                      
            //---Unit---             
            var unitList = (from s in dc.Units select s);
            cmbUnit.ItemsSource = unitList;
            cmbUnit.DisplayMemberPath = "Name";
            cmbUnit.SelectedValuePath = "Id";
            cmbUnit.SelectedIndex = 0;

            //---ProductKind---
            var kindList = (from s in dc.ProductKindViews select s);
            cmbProductKind.ItemsSource = kindList;
            cmbProductKind.DisplayMemberPath = "Name";
            cmbProductKind.SelectedValuePath = "Id";
            cmbProductKind.SelectedIndex = 0;

                      

            //---ProductGroup---
            var groupList = (from s in dc.ProductGroups select s);
            cmbGroup.ItemsSource = groupList;
            cmbGroup.DisplayMemberPath = "Name";
            cmbGroup.SelectedValuePath = "Id";
            cmbGroup.SelectedIndex = 0;

            //---Manufacture---            
            var manufactureList = (from s in dc.ManufactureViews select s);
            cmbManufactureName.ItemsSource = manufactureList;
            cmbManufactureName.DisplayMemberPath = "Name";
            cmbManufactureName.SelectedValuePath = "Id";
            cmbManufactureName.SelectedIndex = 0;

            //Lấy dữ liệu từ idProduct
            if (idProduct != -1)
            {
                var queryProduct = (from product in dc.Products
                          where (product.Id == idProduct)
                          select product).First();

                //---Color---             
                var colorList = (from s in dc.ProductKindColorViews where s.IdProductKind == queryProduct.IdProductKind select s).OrderBy(x => x.ProductColorCode); ;
                cmbColor.ItemsSource = colorList;
                cmbColor.DisplayMemberPath = "ProductColorName";
                cmbColor.SelectedValuePath = "IdProductColor";
                cmbColor.SelectedValue = queryProduct.IdProductColor;

                //Điền thông tin vào form
                if (queryProduct != null)
                {
                    txtCode.Text = queryProduct.Code;
                    txtName.Text = queryProduct.Name;                    
                    txtPriceIn.Text = string.Format("{0:N0}", queryProduct.PriceIn);
                    txtPriceOut.Text = string.Format("{0:N0}", queryProduct.PriceOut);
                    cmbUnit.SelectedValue = queryProduct.IdUnit;
                    cmbColor.SelectedValue = queryProduct.IdProductColor;
                    cmbProductKind.SelectedValue = queryProduct.IdProductKind;
                    cmbGroup.SelectedValue = queryProduct.IdGroup;
                    cmbManufactureName.SelectedValue = queryProduct.IdManufacture;
                }
                idProductForProductKindChange = -1;
            }
            else
            {
                //---Color---             
                var colorList = (from s in dc.ProductKindColorViews
                      select s).GroupBy(n => new { n.ProductColorName, n.ProductColorCode })
                                              .Select(g => g.FirstOrDefault())
                                              .OrderBy(x => x.ProductColorCode)
                                              .ToList();
               
                cmbColor.ItemsSource = colorList;
                cmbColor.DisplayMemberPath = "ProductColorName";
                cmbColor.SelectedValuePath = "IdProductColor";
                cmbColor.SelectedIndex = 0;  
            }
            txtCode.Focus();
            txtCode.SelectAll();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                if (UString.Right(cmbColor.Text, 3) == "JJJ")
                {
                    txtCode.Text = UString.Right(cmbManufactureName.Text, 2) + UString.Right(cmbProductKind.Text, 5);
                }
                else
                {
                    txtCode.Text = UString.Right(cmbManufactureName.Text, 2) + UString.Right(cmbProductKind.Text, 2) + UString.Right(cmbColor.Text, 3);
                }

                if (idProduct == -1)
                {
                    IMSDataContext dc = new IMSDataContext();

                    //Check ProductCode exists or not
                    int isAdd = int.Parse(dc.ProcIsAddProduct(txtCode.Text).ReturnValue.ToString());
                    if (isAdd == 0)
                    {
                        MessageBox.Show("Mã hàng đã tồn tại!", "IMS - Thông báo lỗi");
                        return;
                    }                    

                    //Add new a product code
                    Product productAdd = new Product();
                   
                    productAdd.Code = txtCode.Text;
                    productAdd.Name = UString.Left(cmbProductKind.Text, cmbProductKind.Text.Length - 5) + " " + UString.Right(cmbColor.Text, 3);                    
                    productAdd.PriceIn = long.Parse(txtPriceIn.Text);
                    productAdd.PriceOut = long.Parse(txtPriceOut.Text);
                    productAdd.IdUnit = int.Parse(cmbUnit.SelectedValue.ToString());
                    productAdd.IdProductKind = int.Parse(cmbProductKind.SelectedValue.ToString());
                    productAdd.IdProductColor = int.Parse(cmbColor.SelectedValue.ToString());                    
                    productAdd.IdGroup = int.Parse(cmbGroup.SelectedValue.ToString());
                    productAdd.IdManufacture = int.Parse(cmbManufactureName.SelectedValue.ToString());

                    dc.Products.InsertOnSubmit(productAdd);
                    dc.SubmitChanges();
                    this.Close();
                    pgProductParam.Page_Refresh(productAdd);
                }
                else
                {
                    
                    IMSDataContext dc = new IMSDataContext();

                    //Check ProductCode exists or not
                    int isEdit = int.Parse(dc.ProcIsEditProduct(idProduct).ReturnValue.ToString());
                    if(isEdit == 0)
                    {
                        MessageBox.Show("Không thể chỉnh sửa mã hàng này", "IMS - Thông báo lỗi");
                        return;
                    }
                    
                    //Get Product Infor for updating
                    Product productUpdate = null;                  
                    productUpdate = (from product in dc.Products
                                     where (product.Id == idProduct)
                                     select product).First();

                    if (productUpdate != null)
                    {
                        productUpdate.Code = txtCode.Text;                      
                        productUpdate.Name = UString.Left(cmbProductKind.Text, cmbProductKind.Text.Length - 5) + " " + UString.Right(cmbColor.Text, 3);                     
                        productUpdate.PriceIn = long.Parse(txtPriceIn.Text.Replace(".", ""));
                        productUpdate.PriceOut = long.Parse(txtPriceOut.Text.Replace(".", ""));
                        productUpdate.IdUnit = int.Parse(cmbUnit.SelectedValue.ToString());
                        productUpdate.IdProductColor = int.Parse(cmbColor.SelectedValue.ToString());     
                        productUpdate.IdProductKind = int.Parse(cmbProductKind.SelectedValue.ToString());
                        productUpdate.IdGroup = int.Parse(cmbGroup.SelectedValue.ToString());
                        productUpdate.IdManufacture = int.Parse(cmbManufactureName.SelectedValue.ToString());

                        dc.SubmitChanges();
                        this.Close();
                        pgProductParam.Page_Refresh(productUpdate);
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

        private void cmbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //txtCode.Text = UString.Right(cmbManufactureName.Text, 2) + UString.Right(cmbProductKind.Text, 2) + UString.Right(cmbColor.Text, 3);
                //txtName.Text = UString.Left(cmbProductKind.Text, cmbProductKind.Text.Length - 5) + " " + UString.Right(cmbColor.Text, 3);
            }
            catch { };
        }

        private void cmbSize_LostFocus(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (UString.Right(cmbColor.Text, 3) == "JJJ")
                {
                    txtCode.Text = UString.Right(cmbManufactureName.Text, 2) + UString.Right(cmbProductKind.Text, 5);
                }
                else
                {
                    txtCode.Text = UString.Right(cmbManufactureName.Text, 2) + UString.Right(cmbProductKind.Text, 2) + UString.Right(cmbColor.Text, 3);
                }
                txtName.Text = UString.Left(cmbProductKind.Text, cmbProductKind.Text.Length - 5) + " " + UString.Right(cmbColor.Text, 3);
            }
            catch { };
        }

        private void btnEditProductGroup_Click(object sender, RoutedEventArgs e)
        {
            frmProductGroup frmProductGroup_ = new frmProductGroup(this);
            frmProductGroup_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductGroup_.ShowDialog();
        }

        public void Refresh_GUI(string obj)
        {
            if (obj == "ProductGroup")
            {
                //Khởi tạo DataContext
                IMSDataContext dc = new IMSDataContext();
                //---ProductGroup---
                var groupList = (from s in dc.ProductGroups select s);
                cmbGroup.ItemsSource = groupList;
                cmbGroup.DisplayMemberPath = "Name";
                cmbGroup.SelectedValuePath = "Id";
                cmbGroup.SelectedIndex = 0;

            }
            else if (obj == "Unit")
            {
                //Khởi tạo DataContext
                IMSDataContext dc = new IMSDataContext();

                //Load dữ liệu vào các combobox                      
                //---Unit---             
                var unitList = (from s in dc.Units select s);
                cmbUnit.ItemsSource = unitList;
                cmbUnit.DisplayMemberPath = "Name";
                cmbUnit.SelectedValuePath = "Id";
                cmbUnit.SelectedIndex = 0;
            }
            else if (obj == "ProductKind")
            {
                //Khởi tạo DataContext
                IMSDataContext dc = new IMSDataContext();
                //---ProductGroup---
                var kindList = (from s in dc.ProductKindViews select s);
                cmbProductKind.ItemsSource = kindList;
                cmbProductKind.DisplayMemberPath = "Name";
                cmbProductKind.SelectedValuePath = "Id";
                cmbProductKind.SelectedIndex = 0;
            }
            else if (obj == "ProductColor")
            {
                //Khởi tạo DataContext
                IMSDataContext dc = new IMSDataContext();

                //var queryProduct = (from product in dc.Products
                //                    where (product.Id == idProduct)
                //                    select product).First();

                //---ProductGroup---
                var colorList = (from s in dc.ProductKindColorViews where s.IdProductKind == int.Parse(cmbProductKind.SelectedValue.ToString()) select s).OrderBy(x => x.ProductColorCode); ;
                cmbColor.ItemsSource = colorList;
                cmbColor.DisplayMemberPath = "ProductColorName";
                cmbColor.SelectedValuePath = "IdProductColor";
                //cmbColor.SelectedValue = queryProduct.IdProductColor;
                cmbColor.SelectedIndex = 0;
            }
        }

        private void btnEditUnit_Click(object sender, RoutedEventArgs e)
        {
            frmUnit frmUnit_ = new frmUnit(this);
            frmUnit_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmUnit_.ShowDialog();
        }

        private void btnEditProductKind_Click(object sender, RoutedEventArgs e)
        {
            frmProductKind frmProductKind_ = new frmProductKind(this);
            //frmProductKind_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductKind_.ShowDialog();
        }

        private void btnEditProductSize_Click(object sender, RoutedEventArgs e)
        {
            frmProductSize frmProductSize_ = new frmProductSize(this);
            //frmProductKind_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductSize_.ShowDialog();
        }

        private void btnEditProductColor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frmProductColor frmProductColor_ = new frmProductColor(this, int.Parse(cmbProductKind.SelectedValue.ToString()));
                //frmProductKind_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                frmProductColor_.ShowDialog();
            }
            catch
            {
                ;
            }
        }

        private void cmbProductKind_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UString.Right(cmbColor.Text, 3) == "JJJ")
                {
                    txtCode.Text = UString.Right(cmbManufactureName.Text, 2) + UString.Right(cmbProductKind.Text, 5);
                }
                else
                {
                    txtCode.Text = UString.Right(cmbManufactureName.Text, 2) + UString.Right(cmbProductKind.Text, 2) + UString.Right(cmbColor.Text, 3);
                }
                txtName.Text = UString.Left(cmbProductKind.Text, cmbProductKind.Text.Length - 5) + " " + UString.Right(cmbColor.Text, 3);
            }
            catch { };
        }

        private void cmbProductKind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (idProductForProductKindChange > 0)
                {

                    //Khởi tạo DataContext
                    IMSDataContext dc = new IMSDataContext();

                    var queryProduct = (from product in dc.Products
                                        where (product.Id == idProduct)
                                        select product).First();

                    //---Color---             
                    var colorList = (from s in dc.ProductKindColorViews where s.IdProductKind == queryProduct.IdProductKind select s).OrderBy(x => x.ProductColorCode); ;
                    cmbColor.ItemsSource = colorList;
                    cmbColor.DisplayMemberPath = "ProductColorName";
                    cmbColor.SelectedValuePath = "IdProductColor";
                    cmbColor.SelectedValue = queryProduct.IdProductColor;
                }
                else
                {
                    //Khởi tạo DataContext
                    IMSDataContext dc = new IMSDataContext();                    

                    //---Color---             
                    var colorList = (from s in dc.ProductKindColorViews where s.IdProductKind == int.Parse(cmbProductKind.SelectedValue.ToString()) select s).OrderBy(x => x.ProductColorCode); ;
                    cmbColor.ItemsSource = colorList;
                    cmbColor.DisplayMemberPath = "ProductColorName";
                    cmbColor.SelectedValuePath = "IdProductColor";
                    cmbColor.SelectedIndex = 0;
                }
                
            }
            catch
            {
                ;
            }
        }
    }
}
