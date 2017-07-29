using IMS.Favorite;
using IMS.View;
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

namespace IMS
{
    /// <summary>
    /// Interaction logic for pgServiceDetail.xaml
    /// </summary>
    public partial class pgServiceDetail : Page
    {
        int idService = -1;
        int idServiceDetail = -1;
        int idServiceGroup = -1;
        public pgServiceDetail()
        {
            InitializeComponent();

            UString.SetSystem();

            List<ServiceView> list = new List<ServiceView>();
            IMSDataContext dc = new IMSDataContext();
            var lst = (from s in dc.ServiceViews select s);

            list = lst.AsEnumerable().Select((ServiceView, index) => new ServiceView()
            {
                RowNumber = index + 1,
                Id = ServiceView.Id,
                Name = ServiceView.Name,
                IdServiceGroup = ServiceView.IdServiceGroup  
                          
            }).ToList();

            lsViewService.ItemsSource = null;
            lsViewService.ItemsSource = list;

        }

        private void lsServiceDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var serviceDetail = e.AddedItems[0] as ServiceDetail;
                idServiceDetail = serviceDetail.Id;
            }
            catch
            {
                ;
            }
        }
    

        private void lsService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                var service = e.AddedItems[0] as Service;
                idService = service.Id;
            }
            catch
            {
                ;
            }
        }
     
       

        private void btnRemoveServiceDetail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lsViewService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var serviceView = e.AddedItems[0] as ServiceView;
                idService = serviceView.Id;
                idServiceGroup = serviceView.IdServiceGroup;

                IMSDataContext dc = new IMSDataContext();
                List<ServiceDetail> lsService = (from serviceview in dc.ServiceDetails
                                                  where (serviceview.IdService == idService)
                                            select serviceview).ToList();

                var list = lsService.AsEnumerable().Select((ServiceDetail, index) => new ServiceDetail()
                {

                    RowNumber = index + 1,
                    Id = ServiceDetail.Id,
                    Name = ServiceDetail.Name,
                    Note = ServiceDetail.Note,
                    IsCheck = ServiceDetail.IsCheck
                }).ToList();

                lsViewServiceDetail.ItemsSource = null;
                lsViewServiceDetail.ItemsSource = list;
            }
            catch
            {
                ;
            }
        }

        private void lsViewService_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            frmService frmService_ = new frmService(idServiceGroup, idService, this);
            frmService_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmService_.ShowDialog();
        }       

        public void Page_Refresh(Service sv)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                List<ServiceView> lsService = (from s in dc.ServiceViews
                                                select s).ToList();

                var list = lsService.AsEnumerable().Select((ServiceView, index) => new ServiceView()
                {
                    RowNumber = index + 1,
                    Id = ServiceView.Id,
                    Name = ServiceView.Name,
                    IdServiceGroup = ServiceView.IdServiceGroup
                }).ToList();

                lsViewService.ItemsSource = null;
                lsViewService.ItemsSource = list;
                lsViewService.SelectedItem = sv;
            }
            catch
            {
                ;
            }
        }

        public void Page_Refresh2(ServiceDetail sv)
        {
            try
            {
                IMSDataContext dc = new IMSDataContext();
                List<ServiceDetail> lsService = (from s in dc.ServiceDetails
                                                 where (s.IdService == idService)
                                                 select s).ToList();

                var list = lsService.AsEnumerable().Select((ServiceDetail, index) => new ServiceDetail()
                {
                    RowNumber = index + 1,
                    Id = ServiceDetail.Id,
                    Name = ServiceDetail.Name,
                    Note = ServiceDetail.Note,
                    IdService = ServiceDetail.IdService
                }).ToList();

                lsViewServiceDetail.ItemsSource = null;
                lsViewServiceDetail.ItemsSource = list;
                lsViewServiceDetail.SelectedItem = sv;
            }
            catch
            {
                ;
            }
        }

        private void tbrAddService_Click(object sender, RoutedEventArgs e)
        {
            frmService frmService_ = new frmService(idServiceGroup, -1, this);
            frmService_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmService_.ShowDialog();
        }

        private void tbrEditService_Click(object sender, RoutedEventArgs e)
        {
            frmService frmService_ = new frmService(idServiceGroup, idService, this);
            frmService_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmService_.ShowDialog();
        }       

        private void tbrAddServiceDetail_Click(object sender, RoutedEventArgs e)
        {
            frmServiceDetail frmServiceDetail_ = new frmServiceDetail(idService, -1, this);
            frmServiceDetail_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmServiceDetail_.ShowDialog();
        }

        private void tbrEditServiceDetail_Click(object sender, RoutedEventArgs e)
        {
            frmServiceDetail frmServiceDetail_ = new frmServiceDetail(idService, idServiceDetail, this);
            frmServiceDetail_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmServiceDetail_.ShowDialog();
        }

        private void lsViewServiceDetail_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            frmServiceDetail frmServiceDetail_ = new frmServiceDetail(idService, idServiceDetail, this);
            frmServiceDetail_.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmServiceDetail_.ShowDialog();
        }
    }
}
