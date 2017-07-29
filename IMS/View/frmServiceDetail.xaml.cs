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
    /// Interaction logic for frmServiceDetail.xaml
    /// </summary>
    public partial class frmServiceDetail : Window
    {
        pgServiceDetail pgServiceDetailParam = null;
        int idService = -1;
        int idServiceDetal = -1;
        public frmServiceDetail(int idService_, int idServiceDetail_, pgServiceDetail pgServiceDetail_)
        {
            InitializeComponent();

            pgServiceDetailParam = pgServiceDetail_;

            idService = idService_;
            idServiceDetal = idServiceDetail_;            

            UString.SetSystem();

            IMSDataContext dc = new IMSDataContext();
            ServiceDetail sQuery = null;

            if (idServiceDetail_ != -1)
            {
                sQuery = (from s in dc.ServiceDetails
                          where (s.Id == idServiceDetal)
                          select s).First();

                if (sQuery != null)
                {
                    txtName.Text = sQuery.Name;                    
                    txtNote.Text = sQuery.Note;
                }
            }

            //Service
            Service sgUpdate = null;
            sgUpdate = (from sg2 in dc.Services
                        where (sg2.Id == idService)
                        select sg2).First();

            var lst = (from s in dc.Services select s);

            cmbService.ItemsSource = lst;
            cmbService.DisplayMemberPath = "Name";
            cmbService.SelectedValuePath = "Id";
            cmbService.SelectedItem = sgUpdate;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (idServiceDetal == -1)
            {
                IMSDataContext dc = new IMSDataContext();
                ServiceDetail serviceDetail = new ServiceDetail();
                serviceDetail.Name = txtName.Text;
                serviceDetail.Note = txtNote.Text;
                serviceDetail.IdService = idService;

                dc.ServiceDetails.InsertOnSubmit(serviceDetail);
                dc.SubmitChanges();
                this.Close();             
                pgServiceDetailParam.Page_Refresh2(serviceDetail);
            }
            else
            {
                //Remove all data matching Id in textbox
                IMSDataContext dc = new IMSDataContext();
                ServiceDetail sUpdate = null;

                sUpdate = (from s in dc.ServiceDetails
                           where (s.Id == idServiceDetal)
                           select s).First();

                if (sUpdate != null)
                {
                    sUpdate.Name = txtName.Text;                   
                    sUpdate.Note = txtNote.Text;
                    sUpdate.IdService = int.Parse(cmbService.SelectedValue.ToString());
                    dc.SubmitChanges();
                    this.Close();

                    pgServiceDetailParam.Page_Refresh2(sUpdate);
                    
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
