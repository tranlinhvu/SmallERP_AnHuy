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
    public partial class frmProduct : Window
    {
        int idProduct = -1;
        pgProduct pgProductParam;
        public frmProduct(int idProduct_, pgProduct pgProduct_)
        {
            InitializeComponent();

            //Thiết lập định dạng VN
            UString.SetSystem();

            //Khởi tạo DataContext
            IMSDataContext dc = new IMSDataContext();

            //Gán giá trị cho các biến từ tham số truyền vào của hàm gọi
            idProduct = idProduct_;
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
            var manufactureList = (from s in dc.Manufactures select s);
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

                if (queryProduct != null)
                {
                    txtCode.Text = queryProduct.Code;
                    txtName.Text = queryProduct.Name;                    
                    txtPriceIn.Text = string.Format("{0:N0}", queryProduct.PriceIn);
                    txtPriceOut.Text = string.Format("{0:N0}", queryProduct.PriceOut);
                    cmbUnit.SelectedValue = queryProduct.IdUnit;
                    cmbProductKind.SelectedValue = queryProduct.IdProductKind;
                    cmbGroup.SelectedValue = queryProduct.IdGroup;
                    cmbManufactureName.SelectedValue = queryProduct.IdManufacture;
                }
            }
            txtCode.Focus();
            txtCode.SelectAll();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idProduct == -1)
                {
                    IMSDataContext dc = new IMSDataContext();

                    try
                    {
                        var productAdd1 = (from product in dc.Products
                                           where (product.Code == txtCode.Text)
                                           select product).First();
                        if (productAdd1 != null)
                        {
                            MessageBox.Show("Mã hàng đã tồn tại!", "IMS - Thông báo lỗi");
                            return;
                        }
                    }
                    catch
                    {
                        ;
                    }

                    Product productAdd = new Product();
                   
                    productAdd.Code = txtCode.Text;
                    productAdd.Name = txtName.Text;                    
                    productAdd.PriceIn = long.Parse(txtPriceIn.Text);
                    productAdd.PriceOut = long.Parse(txtPriceOut.Text);
                    productAdd.IdUnit = int.Parse(cmbUnit.SelectedValue.ToString());
                    productAdd.IdProductKind = int.Parse(cmbProductKind.SelectedValue.ToString());  
                    productAdd.IdGroup = int.Parse(cmbGroup.SelectedValue.ToString());
                    productAdd.IdManufacture = int.Parse(cmbManufactureName.SelectedValue.ToString());

                    dc.Products.InsertOnSubmit(productAdd);
                    dc.SubmitChanges();
                    this.Close();
                    pgProductParam.Page_Refresh(productAdd);
                }
                else
                {
                    //Remove all data matching Id in textbox
                    IMSDataContext dc = new IMSDataContext();
                    Product productUpdate = null;                    

                  
                    productUpdate = (from product in dc.Products
                                     where (product.Id == idProduct)
                                     select product).First();

                    if (productUpdate != null)
                    {                        
                        productUpdate.Code = txtCode.Text;
                        productUpdate.Name = txtName.Text;                        
                        productUpdate.PriceIn = long.Parse(txtPriceIn.Text.Replace(".", ""));
                        productUpdate.PriceOut = long.Parse(txtPriceOut.Text.Replace(".", ""));
                        productUpdate.IdUnit = int.Parse(cmbUnit.SelectedValue.ToString());
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
            //txtCode.Text = cmbGroup.Text + "." + cmbColor.Text + cmbSize.Text;
        }
       

        private void btnEditProductGroup_Click(object sender, RoutedEventArgs e)
        {
            frmProductGroup frmProductGroup_ = new frmProductGroup(this);
            frmProductGroup_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductGroup_.ShowDialog();
        }

        private void cmbSize_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        public void Refresh_GUI(string obj)
        {
            if(obj == "ProductGroup")
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
            if (obj == "ProductKind")
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
        }

        private void btnEditUnit_Click(object sender, RoutedEventArgs e)
        {
            frmUnit frmUnit_ = new frmUnit(null);
            frmUnit_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmUnit_.ShowDialog();
        }

        private void btnEditProductKind_Click(object sender, RoutedEventArgs e)
        {
            frmProductKind frmProductKind_ = new frmProductKind(this);
            //frmProductKind_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmProductKind_.ShowDialog();
        }

        private void cmbProductKind_LostFocus(object sender, RoutedEventArgs e)
        {
            ;
        }
    }
}
