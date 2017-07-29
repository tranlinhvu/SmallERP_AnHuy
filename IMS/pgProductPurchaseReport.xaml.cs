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
    public partial class pgProductPurchaseReport : Page
    {
        List<string> curProductInventory = new List<string>();
        int idProductKind = -1;
        public pgProductPurchaseReport()
        {
            InitializeComponent();            
            try
            {
                IMSDataContext dc = new IMSDataContext();
                IMS_TableDataContext dcTable = new IMS_TableDataContext();
                //---Load Customer---
                List<Vendor> vendorList = (from s in dc.Vendors select s).ToList();
                Vendor vendor = new Vendor();
                vendor.Id = 0;
                vendor.Name = " Tất cả";
                vendorList.Add(vendor);
                var varVendorList = vendorList.OrderBy(x => x.Name);
                cmbVendor.ItemsSource = varVendorList;
                cmbVendor.DisplayMemberPath = "Name";
                cmbVendor.SelectedValuePath = "Id";
                cmbVendor.SelectedValue = 0;

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
            List<ProductPurchaseItemView> lsProductPurchaseItemView = (from s in dc.ProductPurchaseItemViews
                                                                             where true
                                                                             select s).ToList();
            var list3 = lsProductPurchaseItemView.AsEnumerable().Select((ProductPurchaseItemView, index) => new ProductPurchaseItemView()
            {
                RowNumber = index + 1,
                Id = ProductPurchaseItemView.Id,
                Code = ProductPurchaseItemView.Code,
                Name = ProductPurchaseItemView.Name,
                Price = ProductPurchaseItemView.Price,
                Quantity = ProductPurchaseItemView.Quantity,
                UnitName = ProductPurchaseItemView.UnitName,
                Amount = ProductPurchaseItemView.Amount
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
