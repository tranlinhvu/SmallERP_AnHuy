using IMS.Favorite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class frmService : Window
    {
        pgService pgServiceParam = null;
        pgServiceDetail pgServiceDetailParam = null;

        int idServiceGroup = -1;
        int idService = -1;

       
        public frmService( )
        {
            InitializeComponent();
        }

        public frmService(int idServiceGroup_, int idService_, pgService pgService_)
        {
            InitializeComponent();
            idServiceGroup = idServiceGroup_;
            idService = idService_;
            pgServiceParam = pgService_;

            UString.SetSystem();

            IMSDataContext dc = new IMSDataContext();
            Service sQuery = null;

            if (idService != -1)
            {
                sQuery = (from sg2 in dc.Services
                           where (sg2.Id == idService)
                           select sg2).First();

                if (sQuery != null)
                {
                    txtServiceName.Text = sQuery.Name;
                    txtServicePrice.Text = string.Format("{0:N0}", sQuery.Price);           
                    txtServiceNote.Text = sQuery.Note;
                }
            }

            //Service Group
            ServiceGroup sgUpdate = null;
            sgUpdate = (from sg2 in dc.ServiceGroups
                        where (sg2.Id == idServiceGroup)
                        select sg2).First();

            var lst = (from s in dc.ServiceGroups select s);
            
            cmbServiceGroup.ItemsSource = lst;
            cmbServiceGroup.DisplayMemberPath = "Name";
            cmbServiceGroup.SelectedValuePath = "Id";
            cmbServiceGroup.SelectedItem = sgUpdate;
        }

        public frmService(int idServiceGroup_, int idService_, pgServiceDetail pgServiceDetail_)
        {
            InitializeComponent();
            idServiceGroup = idServiceGroup_;
            idService = idService_;
            pgServiceDetailParam = pgServiceDetail_;

            UString.SetSystem();

            IMSDataContext dc = new IMSDataContext();
            Service sQuery = null;

            if (idService != -1)
            {
                sQuery = (from sg2 in dc.Services
                          where (sg2.Id == idService)
                          select sg2).First();

                if (sQuery != null)
                {
                    txtServiceName.Text = sQuery.Name;
                    txtServicePrice.Text = string.Format("{0:N0}", sQuery.Price);
                    txtServiceNote.Text = sQuery.Note;
                }
            }

            //Service Group

            ServiceGroup sgUpdate = null;
            sgUpdate = (from sg2 in dc.ServiceGroups
                        where (sg2.Id == idServiceGroup)
                        select sg2).First();

            var lst = (from s in dc.ServiceGroups select s);

            cmbServiceGroup.ItemsSource = lst;
            cmbServiceGroup.DisplayMemberPath = "Name";
            cmbServiceGroup.SelectedValuePath = "Id";
            cmbServiceGroup.SelectedItem = sgUpdate;


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (idService == -1)
                {
                    IMSDataContext dc = new IMSDataContext();
                    Service service = new Service();
                    service.Name = txtServiceName.Text;
                    service.Price = long.Parse(txtServicePrice.Text);
                    service.Note = txtServiceNote.Text;
                    service.IdServiceGroup = idServiceGroup;

                    dc.Services.InsertOnSubmit(service);
                    dc.SubmitChanges();
                    this.Close();

                    if (pgServiceParam == null)
                    {
                        pgServiceDetailParam.Page_Refresh(service);
                    }
                    else
                    {
                        pgServiceParam.Page_Refresh(service);
                    }

                }
                else
                {
                    //Remove all data matching Id in textbox
                    IMSDataContext dc = new IMSDataContext();
                    Service sUpdate = null;

                    sUpdate = (from sg2 in dc.Services
                               where (sg2.Id == idService)
                               select sg2).First();

                    if (sUpdate != null)
                    {
                        sUpdate.Name = txtServiceName.Text;
                        sUpdate.Price = long.Parse(txtServicePrice.Text.Replace(".", ""));
                        sUpdate.Note = txtServiceNote.Text;
                        sUpdate.IdServiceGroup = int.Parse(cmbServiceGroup.SelectedValue.ToString());
                        dc.SubmitChanges();
                        this.Close();

                        if (pgServiceParam == null)
                        {
                            pgServiceDetailParam.Page_Refresh(sUpdate);
                        }
                        else
                        {
                            pgServiceParam.Page_Refresh(sUpdate);
                        }
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
