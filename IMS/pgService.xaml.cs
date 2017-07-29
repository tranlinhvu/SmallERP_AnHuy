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
using System.Windows.Navigation;
using System.Windows.Shapes;
using IMS.View;
using IMS.Model;
using IMS.Favorite;

namespace IMS
{
    /// <summary>
    /// Interaction logic for pgService.xaml
    /// </summary>
    public partial class pgService : Page
    {
        string tbrAction = string.Empty;
        int lCurServiceGroupId = -1;
        int lCurSeviceId = -1;
        public pgService()
        {
            InitializeComponent();

            UString.SetSystem();

            List<ServiceGroup> list = new List<ServiceGroup>();
            IMSDataContext dc = new IMSDataContext();
            var lst = (from s in dc.ServiceGroups select s);

            list = lst.AsEnumerable().Select((ServiceGroup, index) => new ServiceGroup()
            {
                RowNumber = index + 1,
                Id = ServiceGroup.Id,
                Name = ServiceGroup.Name,
                Note = ServiceGroup.Note
            }).ToList();           

            lsGroupService.ItemsSource = null;
            lsGroupService.ItemsSource = list;
        }
       
        private void tbrAdd_Click(object sender, RoutedEventArgs e)
        {
            ;
        }
        private void tbrEdit_Click(object sender, RoutedEventArgs e)
        {
            ;
        }

        //private void tbrSave_Click(object sender, RoutedEventArgs e)
        //{
        //    grdEdit.Visibility = Visibility.Collapsed;
        //    lblSave.Foreground = new SolidColorBrush(Colors.Gray);
        //    lblCancel.Foreground = new SolidColorBrush(Colors.Gray);
        //    tbrSave.IsEnabled = false;
        //    tbrCancel.IsEnabled = false;

        //    tbrAdd.IsEnabled = true;            
        //    tbrEdit.IsEnabled = true;

        //    tbrAdd.Foreground = new SolidColorBrush(Colors.Blue);            
        //    tbrEdit.Foreground = new SolidColorBrush(Colors.Blue);

        //    if (tbrAction == "Add")
        //    {
        //        //ServiceGroup sg = new ServiceGroup(1, txtName.Text, txtNote.Text);                
        //        //sg.MoveToDB();
        //        IMSDataContext dc = new IMSDataContext();
        //        ServiceGroup sg = new ServiceGroup();
        //        sg.Name = txtName.Text;
        //        sg.Note = txtNote.Text;

        //        dc.ServiceGroups.InsertOnSubmit(sg);
        //        dc.SubmitChanges();
        //        var lst = (from s in dc.ServiceGroups select s);
        //        lsGroupService.ItemsSource = null;
        //        lsGroupService.ItemsSource = lst;
        //    }
        //    else if (tbrAction == "Remove")
        //    {
        //        //Remove all data matching Id in textbox
        //        IMSDataContext dc = new IMSDataContext();
        //        List<ServiceGroup> removeFromGroup = (from sg1 in dc.ServiceGroups
        //                                              where (sg1.Id == long.Parse(txtId.Text))
        //                                              select sg1).ToList();
        //        dc.ServiceGroups.DeleteAllOnSubmit(removeFromGroup);
        //        dc.SubmitChanges();

        //        //Refresh display
        //        var lst = (from s in dc.ServiceGroups select s);
        //        lsGroupService.ItemsSource = null;
        //        lsGroupService.ItemsSource = lst;
        //    }
        //    else if (tbrAction == "Edit")
        //    {
        //        //Remove all data matching Id in textbox
        //        IMSDataContext dc = new IMSDataContext();

        //        long lId;
        //        if (long.TryParse(txtId.Text, out lId))
        //        {
        //            ServiceGroup sgUpdate = (from sg2 in dc.ServiceGroups
        //                                     where (sg2.Id == lId)
        //                                     select sg2).First();

        //            if (sgUpdate != null)
        //            {
        //                sgUpdate.Name = txtName.Text;
        //                sgUpdate.Note = txtNote.Text;
        //                dc.SubmitChanges();
        //            }
        //        }

        //        //Refresh display
        //        var lst = (from s in dc.ServiceGroups select s);
        //        lsGroupService.ItemsSource = null;
        //        lsGroupService.ItemsSource = lst;
        //    }
        //}

        

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lsGroupService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {  
                var sgroup = e.AddedItems[0] as ServiceGroup;
               
                lCurServiceGroupId = sgroup.Id;

                IMSDataContext dc = new IMSDataContext();
                List<Service> lsService_ = (from sg1 in dc.Services
                                                      where (sg1.IdServiceGroup == lCurServiceGroupId)
                                                      select sg1).ToList();

                var list = lsService_.AsEnumerable().Select((Service, index) => new Service()
                {

                    RowNumber = index + 1,
                    Id = Service.Id,
                    Name = Service.Name,
                    Price = Service.Price,
                    Note = Service.Note
                }).ToList();

                lsService.ItemsSource = null;
                lsService.ItemsSource = list;
                lCurSeviceId = -2;

            }
            catch
            {
                ;
            }
        }

        private void tbrAddService_Click(object sender, RoutedEventArgs e)
        {
            frmService frmService_ = new frmService(lCurServiceGroupId, -1, this);
            frmService_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmService_.ShowDialog();
        }

        private void tbrEditService_Click(object sender, RoutedEventArgs e)
        {
            if (lCurSeviceId >= 0)
            {
                frmService frmService_ = new frmService(lCurServiceGroupId, lCurSeviceId, this);
                frmService_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                frmService_.ShowDialog();
            }
            else
            {
                MessageBox.Show("Hãy chọn một dịch vụ", "IMS - Thông báo lỗi");
            }
        }

        private void lsService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var service = e.AddedItems[0] as Service;
                lCurSeviceId = service.Id;
            }
            catch
            {
                ;
            }
        }

        private void Page_GotFocus(object sender, RoutedEventArgs e)
        {
            string a = "1";
        }

        public void Page_Refresh(Service sv)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                List<Service> lsService_ = (from sg1 in dc.Services
                                            where (sg1.IdServiceGroup == lCurServiceGroupId)
                                            select sg1).ToList();

                var list = lsService_.AsEnumerable().Select((Service, index) => new Service()
                {
                    RowNumber = index + 1,
                    Id = Service.Id,
                    Name = Service.Name,
                    Price = Service.Price,
                    Note = Service.Note
                }).ToList();

                lsService.ItemsSource = null;
                lsService.ItemsSource = list;
                lsService.SelectedItem = sv;

            }
            catch
            {
                ;
            }
        }

        private void lsService_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lCurSeviceId >= 0)
            {
                frmService frmService_ = new frmService(lCurServiceGroupId, lCurSeviceId, this);
                frmService_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                frmService_.ShowDialog();
            }
            else
            {
                MessageBox.Show("Hãy chọn một dịch vụ", "IMS - Thông báo lỗi");
            }
        }

        private void btnRemoveService_Click(object sender, RoutedEventArgs e)
        {
            var results = MessageBox.Show("Bạn có thực sự muốn xóa dòng dữ liệu?", "IMS - Thông báo", MessageBoxButton.OKCancel);

            if (results != MessageBoxResult.OK)
                return;

            Service service = (sender as Button).DataContext as Service;
            //Remove all data matching Id in textbox
            IMSDataContext dc = new IMSDataContext();
            int result = dc.ProcDeleteService(service.Id);            

            if(result < 0)
            {
                MessageBox.Show("Không thể xóa do lỗi dữ liệu!", "IMS - Thông báo lỗi");
            }
            else if(result == 0)
            {
                MessageBox.Show("Không thể xóa vì dòng dữ liệu có liên quan đối tượng khác", "IMS - Thông báo lỗi");
            }
            else
            {
                //Refresh display
                var lst = (from s in dc.Services select s);
                lsService.ItemsSource = null;
                lsService.ItemsSource = lst;
                //myListView.SelectedIndex = 0;
            }
        }
    }
}
