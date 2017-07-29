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
    /// Interaction logic for frmUnit.xaml
    /// </summary>
    public partial class frmUnit : Window
    {
        int idUnit;
        frmProduct1 frmProduct_ = null;
        public frmUnit(frmProduct1 _frmProduct)
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

                

                //Lấy dữ liệu từ UnitView                
                List<Unit> ls = (from s in dc.Units
                                         select s).ToList();

                var list = ls.AsEnumerable().Select((Unit, index) => new Unit()
                {
                    RowNumber = index + 1,
                    Id = Unit.Id,
                    Name = Unit.Name,
                    Note = Unit.Note

                }).ToList();

                lsViewUnit.ItemsSource = null;
                lsViewUnit.ItemsSource = list;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                if (idUnit == -1)
                {
                    try
                    {
                        var UnitAdd1 = (from s in dc.Units
                                        where (s.Name == txtName.Text)
                                           select s).First();
                        if (UnitAdd1 != null)
                        {
                            MessageBox.Show("Đơn vị đã tồn tại!", "IMS - Thông báo lỗi");
                            return;
                        }
                    }
                    catch
                    {
                        ;
                    }

                    Unit UnitAdd = new  Unit();
                    UnitAdd.Name = txtName.Text;
                    UnitAdd.Note = txtNote.Text;
                    dc.Units.InsertOnSubmit(UnitAdd);
                    dc.SubmitChanges();

                    grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                    grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                    this.Title = "IMS - Đơn vị";
                }
                else
                {                    
                    Unit UnitUpdate = null;
                    UnitUpdate = (from s in dc.Units
                                      where (s.Id == idUnit)
                                     select s).First();

                    if (UnitUpdate != null)
                    {
                        UnitUpdate.Name = txtName.Text;
                        UnitUpdate.Note = txtNote.Text;
                        dc.SubmitChanges();

                        grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
                        grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
                        this.Title = "IMS - Đơn vị";
                    }
                }
                

                lsViewUnit.IsEnabled = true;
                //Lấy dữ liệu từ UnitView                
                List<Unit> ls = (from s in dc.Units
                                     select s).ToList();

                var list = ls.AsEnumerable().Select((Unit, index) => new Unit()
                {
                    RowNumber = index + 1,
                    Id = Unit.Id,
                    Name = Unit.Name,
                    Note = Unit.Note
                   

                }).ToList();

                lsViewUnit.ItemsSource = null;
                lsViewUnit.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMS - Thông báo lỗi");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "IMS - Đơn vị";
            grdAll.RowDefinitions[0].Height = new GridLength(0, GridUnitType.Star);
            grdAll.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
            lsViewUnit.IsEnabled = true;
        }

        private void tbrAddSUnit_Click(object sender, RoutedEventArgs e)
        {
            idUnit = -1;
            this.Title = "IMS - Thêm đơn vị";

            grdAll.RowDefinitions[0].Height = new GridLength(110, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            lsViewUnit.IsEnabled = false;

            txtName.Focus();
            txtName.SelectAll();
        }

        private void tbrEditUnit_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "IMS - Sửa chữa đơn vị";

            grdAll.RowDefinitions[0].Height = new GridLength(110, GridUnitType.Pixel);
            grdAll.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Pixel);
            grdAll.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

            lsViewUnit.IsEnabled = false;

            //Lấy dữ liệu từ idUnit
            IMSDataContext dc = new IMSDataContext();
            if (idUnit != -1)
            {
                var queryUnit = (from s in dc.Units
                                 where (s.Id == idUnit)
                                 select s).First();

                if (queryUnit != null)
                {
                    txtName.Text = queryUnit.Name;
                    txtNote.Text = queryUnit.Note;                   
                }
            }
            txtName.Focus();
            txtName.SelectAll();
        }

        private void lsViewUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var userView = e.AddedItems[0] as Unit;
                idUnit = userView.Id;
            }
            catch
            {
                ;
            }
        }

        private void btnRemoveUnit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            frmProduct_.Refresh_GUI("Unit");
        }
    }
}
