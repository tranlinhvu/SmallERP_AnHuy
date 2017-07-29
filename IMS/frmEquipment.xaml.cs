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
    public partial class frmEquipment : Window
    {
        int idEquipment = -1;
        pgEquipment pgEquipmentParam;
        public frmEquipment(int idEquipment_, pgEquipment pgEquipment_)
        {
            InitializeComponent();

            //Thiết lập định dạng VN
            UString.SetSystem();

            //Khởi tạo DataContext
            IMSDataContext dc = new IMSDataContext();

            //Gán giá trị cho các biến từ tham số truyền vào của hàm gọi
            idEquipment = idEquipment_;
            pgEquipmentParam = pgEquipment_;

            //Load dữ liệu vào các combobox                      
            //---Unit---             
            var unitList = (from s in dc.Units select s);
            cmbUnit.ItemsSource = unitList;
            cmbUnit.DisplayMemberPath = "Name";
            cmbUnit.SelectedValuePath = "Id";

            //---EquipmentGroup---
            var groupList = (from s in dc.EquipmentGroups select s);
            cmbGroup.ItemsSource = groupList;
            cmbGroup.DisplayMemberPath = "Name";
            cmbGroup.SelectedValuePath = "Id";

            //---Manufacture---            
            var manufactureList = (from s in dc.Manufactures select s);
            cmbManufactureName.ItemsSource = manufactureList;
            cmbManufactureName.DisplayMemberPath = "Name";
            cmbManufactureName.SelectedValuePath = "Id";

            //Lấy dữ liệu từ idEquipment
            if (idEquipment != -1)
            {
                var queryEquipment = (from product in dc.Equipments
                          where (product.Id == idEquipment)
                          select product).First();

                if (queryEquipment != null)
                {
                    txtCode.Text = queryEquipment.Code;
                    txtName.Text = queryEquipment.Name;
                    txtStandard.Text = queryEquipment.Standard;
                    txtPriceIn.Text = string.Format("{0:N0}", queryEquipment.PriceIn);
                    txtPriceOut.Text = string.Format("{0:N0}", queryEquipment.PriceOut);
                    cmbUnit.SelectedValue = queryEquipment.IdUnit;
                    cmbGroup.SelectedValue = queryEquipment.IdGroup;
                    cmbManufactureName.SelectedValue = queryEquipment.IdManufacture;
                }
            }
            txtCode.Focus();
            txtCode.SelectAll();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idEquipment == -1)
                {
                    IMSDataContext dc = new IMSDataContext();
                    Equipment productAdd = new Equipment();
                    productAdd.Code = txtCode.Text;
                    productAdd.Name = txtName.Text;
                    productAdd.Standard = txtStandard.Text;
                    productAdd.PriceIn = long.Parse(txtPriceIn.Text);
                    productAdd.PriceOut = long.Parse(txtPriceOut.Text);
                    productAdd.IdUnit = int.Parse(cmbUnit.SelectedValue.ToString());
                    productAdd.IdGroup = int.Parse(cmbGroup.SelectedValue.ToString());
                    productAdd.IdManufacture = int.Parse(cmbManufactureName.SelectedValue.ToString());

                    dc.Equipments.InsertOnSubmit(productAdd);
                    dc.SubmitChanges();
                    this.Close();
                    pgEquipmentParam.Page_Refresh(productAdd);
                }
                else
                {
                    //Remove all data matching Id in textbox
                    IMSDataContext dc = new IMSDataContext();
                    Equipment productUpdate = null;

                    productUpdate = (from product in dc.Equipments
                                     where (product.Id == idEquipment)
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
                        pgEquipmentParam.Page_Refresh(productUpdate);
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
