using IMS;
using IMS.Favorite;
using IMS.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IMS
{
    /// <summary>
    /// Interaction logic for pgObjectCaring.xaml
    /// </summary>
    public partial class pgProductSaleReport : Page
    {
        List<string> curProductInventory = new List<string>();
        int idProductKind = -1;
        public pgProductSaleReport()
        {
            InitializeComponent();
            try
            {
                IMSDataContext dc = new IMSDataContext();
                IMS_TableDataContext dcTable = new IMS_TableDataContext();
                //---Load Customer---
                List <CustomerView> cusList = (from s in dc.CustomerViews select s).ToList();
                CustomerView cus = new CustomerView();
                cus.Id = 0;                
                cus.Name = " Tất cả";
                cusList.Add(cus);
                var varCusList = cusList.OrderBy(x => x.Name);
                cmbCustomer.ItemsSource = varCusList;
                cmbCustomer.DisplayMemberPath = "Name";
                cmbCustomer.SelectedValuePath = "Id";
                cmbCustomer.SelectedValue = 0;

                //---Load Staff---
                List<StaffView> staffList = (from s in dc.StaffViews select s).ToList();
                StaffView staff = new StaffView();
                staff.Id = 0;
                staff.Name = " Tất cả";
                staffList.Add(staff);
                var varStaffList = staffList.OrderBy(x => x.Name);
                cmbStaff.ItemsSource = varStaffList;
                cmbStaff.DisplayMemberPath = "Name";
                cmbStaff.SelectedValuePath = "Id";
                cmbStaff.SelectedValue = 0;

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
                cmbProductKind.SelectedValue = 0;          

                //---Color---             
                idProductKind = int.Parse(cmbProductKind.SelectedValue.ToString());
                if (idProductKind == 0)
                {
                    List<ProductKindColorView> colorList = (from s in dc.ProductKindColorViews select s).OrderBy(x => x.ProductColorCode).ToList();
                    ProductKindColorView a1 = new ProductKindColorView();
                    a1.IdProductColor = 0;
                    a1.ProductColorName = "Tất cả";
                    a1.ProductColorCode = "@";
                    colorList.Add(a1);
                    var varColorList = colorList.OrderBy(x => x.ProductColorCode);
                    cmbProductColor.ItemsSource = varColorList;
                    cmbProductColor.DisplayMemberPath = "ProductColorName";
                    cmbProductColor.SelectedValuePath = "IdProductColor";
                    cmbProductColor.SelectedValue = 0;
                }
                else
                {
                    List<ProductKindColorView> colorList = (from s in dc.ProductKindColorViews where s.IdProductKind == idProductKind select s).OrderBy(x => x.ProductColorCode).ToList();
                    ProductKindColorView a1 = new ProductKindColorView();
                    a1.IdProductColor = 0;
                    a1.ProductColorName = "Tất cả";
                    a1.ProductColorCode = "@";
                    colorList.Add(a1);
                    var varColorList = colorList.OrderBy(x => x.ProductColorCode);
                    cmbProductColor.ItemsSource = varColorList;
                    cmbProductColor.DisplayMemberPath = "ProductColorName";
                    cmbProductColor.SelectedValuePath = "IdProductColor";
                    cmbProductColor.SelectedValue = 0;
                }             
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void tbrSearch_Click(object sender, RoutedEventArgs e)
        {

            IMSDataContext dc = new IMSDataContext();  

            //Hiển thị chi tiết đơn hàng    
            lsViewProductSaleItem.ItemsSource = null;
            List<ProductSaleItemView> lsProductSaleItemView = (from s in dc.ProductSaleItemViews
                                                                             where true
                                                                             select s).ToList();
            var list3 = lsProductSaleItemView.AsEnumerable().Select((ProductSaleItemView, index) => new ProductSaleItemView()
            {
                RowNumber = index + 1,
                Id = ProductSaleItemView.Id,
                Code = ProductSaleItemView.Code,
                Name = ProductSaleItemView.Name,
                Price = ProductSaleItemView.Price,
                Quantity = ProductSaleItemView.Quantity,
                UnitName = ProductSaleItemView.UnitName,
                Amount = ProductSaleItemView.Amount
            }).ToList();
            lsViewProductSaleItem.ItemsSource = null;
            lsViewProductSaleItem.ItemsSource = list3;
        }

        private void cmbProductKind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                //---Color---             
                idProductKind = int.Parse(cmbProductKind.SelectedValue.ToString());
                if (idProductKind == 0)
                {
                    List<ProductKindColorView> colorList = (from s in dc.ProductKindColorViews select s).OrderBy(x => x.ProductColorCode).ToList();
                    ProductKindColorView a1 = new ProductKindColorView();
                    a1.IdProductColor = 0;
                    a1.ProductColorName = "Tất cả";
                    a1.ProductColorCode = "@";
                    colorList.Add(a1);
                    var varColorList = colorList.OrderBy(x => x.ProductColorCode);
                    cmbProductColor.ItemsSource = varColorList;
                    cmbProductColor.DisplayMemberPath = "ProductColorName";
                    cmbProductColor.SelectedValuePath = "IdProductColor";
                    cmbProductColor.SelectedValue = 0;
                }
                else
                {
                    List<ProductKindColorView> colorList = (from s in dc.ProductKindColorViews where s.IdProductKind == idProductKind select s).OrderBy(x => x.ProductColorCode).ToList();
                    ProductKindColorView a1 = new ProductKindColorView();
                    a1.IdProductColor = 0;
                    a1.ProductColorName = "Tất cả";
                    a1.ProductColorCode = "@";
                    colorList.Add(a1);
                    var varColorList = colorList.OrderBy(x => x.ProductColorCode);
                    cmbProductColor.ItemsSource = varColorList;
                    cmbProductColor.DisplayMemberPath = "ProductColorName";
                    cmbProductColor.SelectedValuePath = "IdProductColor";
                    cmbProductColor.SelectedValue = 0;
                }
            }
            catch
            {
                ;
            }
        }
    }
}
