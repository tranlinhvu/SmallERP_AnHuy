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

            //---Color---             
            var colorList = (from s in dc.ProductColors select s);
            cmbColor.ItemsSource = colorList;
            cmbColor.DisplayMemberPath = "Name";
            cmbColor.SelectedValuePath = "Code";

            //---Size---             
            var sizeList = (from s in dc.ProductSizes select s);
            cmbSize.ItemsSource = unitList;
            cmbSize.DisplayMemberPath = "Lenth";
            cmbSize.SelectedValuePath = "Id";

            //---ProductGroup---
            var groupList = (from s in dc.ProductGroups select s);
            cmbGroup.ItemsSource = groupList;
            cmbGroup.DisplayMemberPath = "Name";
            cmbGroup.SelectedValuePath = "Id";

            //---Manufacture---            
            var manufactureList = (from s in dc.Manufactures select s);
            cmbManufactureName.ItemsSource = manufactureList;
            cmbManufactureName.DisplayMemberPath = "Name";
            cmbManufactureName.SelectedValuePath = "Id";

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
                    txtStandard.Text = queryProduct.Standard;
                    txtPriceIn.Text = string.Format("{0:N0}", queryProduct.PriceIn);
                    txtPriceOut.Text = string.Format("{0:N0}", queryProduct.PriceOut);
                    cmbUnit.SelectedValue = queryProduct.IdUnit;
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
                    Product productAdd = new Product();
                    productAdd.Code = txtCode.Text;
                    productAdd.Name = txtName.Text;
                    productAdd.Standard = txtStandard.Text;
                    productAdd.PriceIn = long.Parse(txtPriceIn.Text);
                    productAdd.PriceOut = long.Parse(txtPriceOut.Text);
                    productAdd.IdUnit = int.Parse(cmbUnit.SelectedValue.ToString());
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
                        productUpdate.Standard = txtStandard.Text;
                        productUpdate.PriceIn = long.Parse(txtPriceIn.Text.Replace(".", ""));
                        productUpdate.PriceOut = long.Parse(txtPriceOut.Text.Replace(".", ""));
                        productUpdate.IdUnit = int.Parse(cmbUnit.SelectedValue.ToString());
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
    }
}
