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
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmAddProductKindColor : Window
    {
        frmProductColor mainWindow = null;
        int idProductKindDestination = 0;
        public frmAddProductKindColor()
        {
            InitializeComponent();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as ComboBox).SelectedIndex = 0;
        }
        public frmAddProductKindColor(frmProductColor mainWindow_, int _idProductKindDestination)
        {
            InitializeComponent();
            mainWindow = mainWindow_;
            idProductKindDestination = _idProductKindDestination;

            //Khởi tạo DataContext
            IMS_TableDataContext dcTable = new IMS_TableDataContext();
            IMSDataContext dc = new IMSDataContext();
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

            int idProductKind = int.Parse(cmbProductKind.SelectedValue.ToString());
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
            
            //---ProductKind1---           
            List<ProductKind> kindList1 = (from s in dcTable.ProductKinds select s).ToList();
            ProductKind a1 = new ProductKind();
            a1.Id = 0;
            a1.Name = "Tất cả";
            kindList1.Add(a1);

            var varKindList1 = kindList1.OrderBy(x => x.Id);
            cmbProductKind1.ItemsSource = varKindList1;
            cmbProductKind1.DisplayMemberPath = "Name";
            cmbProductKind1.SelectedValuePath = "Id";
            cmbProductKind1.SelectedValue = idProductKindDestination;

            idProductKind = int.Parse(cmbProductKind1.SelectedValue.ToString());
            //Lấy dữ liệu từ ProductKindColorView 
            List<ProductKindColorView> ls1 = null;
            if (idProductKind == 0)
            {
                ls1 = (from s in dc.ProductKindColorViews
                      select s).GroupBy(n => new { n.ProductColorName, n.ProductColorCode })
                                           .Select(g => g.FirstOrDefault())
                                           .OrderBy(x => x.ProductColorName)
                                           .ToList();
            }
            else if (idProductKind > 0)
            {
                ls1 = (from s in dc.ProductKindColorViews
                      where s.IdProductKind == idProductKind
                      select s).OrderBy(x => x.ProductColorName).ToList();
            }

            var list1 = ls1.AsEnumerable().Select((ProductKindColorView, index) => new ProductKindColorView()
            {
                RowNumber = index + 1,
                IdProductColor = ProductKindColorView.IdProductColor,
                ProductColorName = ProductKindColorView.ProductColorName,
                ProductColorCode = ProductKindColorView.ProductColorCode

            }).ToList().OrderBy(x => x.ProductColorName);

            lsViewProductColor1.ItemsSource = null;
            lsViewProductColor1.ItemsSource = list1;
           
        }

        private void cmbProductKind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();

                int idProductKind = int.Parse(cmbProductKind.SelectedValue.ToString());
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

        private void cmbProductKind1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();

                int idProductKind = int.Parse(cmbProductKind1.SelectedValue.ToString());
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

                lsViewProductColor1.ItemsSource = null;
                lsViewProductColor1.ItemsSource = list;
            }
            catch
            {
                ;
            }
        }       

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj,
                                         string name) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T &&
                          (child as FrameworkElement).Name.Equals(name))
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child, name))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        private void chkAllColor_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox checkBox in FindVisualChildren<CheckBox>(lsViewProductColor, "chkItemColor"))
            {
                checkBox.IsChecked = chkAllColor.IsChecked;
            }
            
        }

        private void chkItemColor_Unchecked(object sender, RoutedEventArgs e)
        {
            chkAllColor.IsChecked = false;
        }

        private void btnAddItemProductColor_Click(object sender, RoutedEventArgs e)
        {
            ProductKindColorView productKindColor = (sender as Button).DataContext as ProductKindColorView;
            //Khởi tạo DataContext
            IMSDataContext dc = new IMSDataContext();
            int idProductKind = int.Parse(cmbProductKind1.SelectedValue.ToString());
            dc.ProcInsertProductKindColor(idProductKind, productKindColor.ProductColorName, productKindColor.ProductColorCode);        
                
            //Lấy dữ liệu từ ProductKindColorView 
            List<ProductKindColorView> ls1 = null;
            if (idProductKind == 0)
            {
                ls1 = (from s in dc.ProductKindColorViews
                      select s).GroupBy(n => new { n.ProductColorName, n.ProductColorCode })
                                           .Select(g => g.FirstOrDefault())
                                           .OrderBy(x => x.ProductColorName)
                                           .ToList();
            }
            else if (idProductKind > 0)
            {
                ls1 = (from s in dc.ProductKindColorViews
                      where s.IdProductKind == idProductKind
                      select s).OrderBy(x => x.ProductColorName).ToList();
            }

            var list1 = ls1.AsEnumerable().Select((ProductKindColorView, index) => new ProductKindColorView()
            {
                RowNumber = index + 1,
                IdProductColor = ProductKindColorView.IdProductColor,
                ProductColorName = ProductKindColorView.ProductColorName,
                ProductColorCode = ProductKindColorView.ProductColorCode

            }).ToList().OrderBy(x => x.ProductColorName);

            lsViewProductColor1.ItemsSource = null;
            lsViewProductColor1.ItemsSource = list1;
        }

        private void btnAddAllProductColor_Click(object sender, RoutedEventArgs e)
        {
            if (chkAllColor.IsChecked == false)
                return;

            //Khởi tạo DataContext
            IMSDataContext dc = new IMSDataContext();
            if (cmbProductKind.Text != "Tất cả")
            {
                dc.ProcCopyProductColor(int.Parse(cmbProductKind.SelectedValue.ToString()), idProductKindDestination);
            }
            else
            {
                dc.ProcCopyProductColor(0, idProductKindDestination);
            }

            int idProductKind = int.Parse(cmbProductKind1.SelectedValue.ToString());          
            //Lấy dữ liệu từ ProductKindColorView 
            List<ProductKindColorView> ls1 = null;
            if (idProductKind == 0)
            {
                ls1 = (from s in dc.ProductKindColorViews
                      select s).GroupBy(n => new { n.ProductColorName, n.ProductColorCode })
                                           .Select(g => g.FirstOrDefault())
                                           .OrderBy(x => x.ProductColorName)
                                           .ToList();
            }
            else if (idProductKind > 0)
            {
                ls1 = (from s in dc.ProductKindColorViews
                      where s.IdProductKind == idProductKind
                      select s).OrderBy(x => x.ProductColorName).ToList();
            }

            var list1 = ls1.AsEnumerable().Select((ProductKindColorView, index) => new ProductKindColorView()
            {
                RowNumber = index + 1,
                IdProductColor = ProductKindColorView.IdProductColor,
                ProductColorName = ProductKindColorView.ProductColorName,
                ProductColorCode = ProductKindColorView.ProductColorCode

            }).ToList().OrderBy(x => x.ProductColorName);

            lsViewProductColor1.ItemsSource = null;
            lsViewProductColor1.ItemsSource = list1;
                        
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.RefreshGUI();
        }

        private void btnRemoveItemProductColor_Click(object sender, RoutedEventArgs e)
        {

        }       
    }
}
