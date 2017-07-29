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
using IMS.Model;
using System.Diagnostics;

namespace IMS
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class pgServiceGroup : Page
    {
        long lCurServiceGroupId = -1;
        string tbrAction = string.Empty;
        ServiceGroup sgroup;
        public pgServiceGroup()
        {
            InitializeComponent();
            try
            {                
                IMSDataContext dc = new IMSDataContext();

                List<ServiceGroup> lst = (from s in dc.ServiceGroups
                          select s).ToList();

                var list = lst.AsEnumerable().Select((ServiceGroup, index) => new ServiceGroup()
                {
                    RowNumber = index + 1,
                    Name = ServiceGroup.Name,
                    Note = ServiceGroup.Note
                }).ToList();

                myListView.ItemsSource = null;
                myListView.ItemsSource = list;
            }
            catch
            {
                ;
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ((TextBlock)sender).DataContext;
            sgroup = (ServiceGroup)item;
            txtId.Text = sgroup.Id.ToString();
            txtName.Text = sgroup.Name;
            txtNote.Text = sgroup.Note;
        }

        private void myListView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //var item = ((TextBlock)sender).DataContext;
            //ServiceGroup sgroup = (ServiceGroup)item;
            //txtName.Text = sgroup.Name;
            //txtNote.Text = sgroup.Note;
        }      

        private void tbrAdd_Click(object sender, RoutedEventArgs e)
        {
            tbrAction = "Add";

            txtName.Focus();
            tbrAdd.Foreground = new SolidColorBrush(Colors.Gray);
            tbrAdd.IsEnabled = false;

            grdEdit.Visibility = Visibility.Visible;
            lblSave.Foreground = new SolidColorBrush(Colors.Blue);
            lblCancel.Foreground = new SolidColorBrush(Colors.Red);
            tbrSave.IsEnabled = true;
            tbrCancel.IsEnabled = true;
        }

        private void tbrEdit_Click(object sender, RoutedEventArgs e)
        {
            tbrAction = "Edit";
            tbrEdit.IsEnabled = false;
            tbrEdit.Foreground = new SolidColorBrush(Colors.Gray);

            grdEdit.Visibility = Visibility.Visible;
            lblSave.Foreground = new SolidColorBrush(Colors.Blue);
            lblCancel.Foreground = new SolidColorBrush(Colors.Red);
            tbrSave.IsEnabled = true;
            tbrCancel.IsEnabled = true;
        }
       
        private void tbrSave_Click(object sender, RoutedEventArgs e)
        {
            grdEdit.Visibility = Visibility.Collapsed;
            lblSave.Foreground = new SolidColorBrush(Colors.Gray);
            lblCancel.Foreground = new SolidColorBrush(Colors.Gray);
            tbrSave.IsEnabled = false;
            tbrCancel.IsEnabled = false;

            tbrAdd.IsEnabled = true;            
            tbrEdit.IsEnabled = true;

            tbrAdd.Foreground = new SolidColorBrush(Colors.Blue);            
            tbrEdit.Foreground = new SolidColorBrush(Colors.Blue);

            if(tbrAction == "Add")
            {
                //ServiceGroup sg = new ServiceGroup(1, txtName.Text, txtNote.Text);                
                //sg.MoveToDB();

                IMSDataContext dc = new IMSDataContext();
                ServiceGroup sg = new ServiceGroup();
                sg.Name = txtName.Text;
                sg.Note = txtNote.Text;

                dc.ServiceGroups.InsertOnSubmit(sg);
                dc.SubmitChanges();

                myListView.SelectedItem = sg;

                var lst = (from s in dc.ServiceGroups select s);
                myListView.ItemsSource = null;
                myListView.ItemsSource = lst;
                myListView.SelectedItem = sg;
            }
            else if (tbrAction == "Remove")
            {
                //Remove all data matching Id in textbox
                IMSDataContext dc = new IMSDataContext();
                List<ServiceGroup> removeFromGroup = (from sg1 in dc.ServiceGroups
                                      where (sg1.Id == long.Parse(txtId.Text))
                                      select sg1).ToList();
                dc.ServiceGroups.DeleteAllOnSubmit(removeFromGroup);
                dc.SubmitChanges();

               //Refresh display
                var lst = (from s in dc.ServiceGroups select s);
                myListView.ItemsSource = null;
                myListView.ItemsSource = lst;
            }
            else if (tbrAction == "Edit")
            {
                //Remove all data matching Id in textbox
                IMSDataContext dc = new IMSDataContext();
                ServiceGroup sgUpdate = null;
                long lId;
                if (long.TryParse(txtId.Text, out lId))
                {
                    sgUpdate = (from sg2 in dc.ServiceGroups
                                             where (sg2.Id == lId)
                                             select sg2).First();

                    if (sgUpdate != null)
                    {
                        sgUpdate.Name = txtName.Text;
                        sgUpdate.Note = txtNote.Text;
                        dc.SubmitChanges();
                    }

                    myListView.SelectedItem = sgUpdate;
                    var var2 = myListView.Items.IndexOf(myListView.SelectedItem);
                    myListView.SelectedIndex = var2;
                }

                //Refresh display
                var lst = (from s in dc.ServiceGroups select s);
                myListView.ItemsSource = null;
                myListView.ItemsSource = lst;
                myListView.SelectedItem = sgUpdate;
            }
        }

        private void tbrCancel_Click(object sender, RoutedEventArgs e)
        {
            grdEdit.Visibility = Visibility.Collapsed;
            lblSave.Foreground = new SolidColorBrush(Colors.Gray);
            lblCancel.Foreground = new SolidColorBrush(Colors.Gray);
            tbrSave.IsEnabled = false;
            tbrCancel.IsEnabled = false;

            tbrAdd.IsEnabled = true;            
            tbrEdit.IsEnabled = true;
        }

        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try

            {
                Debug.WriteLine("Selected: {0}", e.AddedItems[0]);
                var sgroup = e.AddedItems[0] as ServiceGroup;
                txtId.Text = sgroup.Id.ToString();
                txtName.Text = sgroup.Name;
                txtNote.Text = sgroup.Note;
                lCurServiceGroupId = sgroup.Id;

                //txtName.IsFocused = false;
                //txtName.IsFocused = true;

                txtName.Focus();
                txtName.SelectAll();
            }
            catch
            {
                ;
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            ServiceGroup sg = (sender as Button).DataContext as ServiceGroup;
            //Remove all data matching Id in textbox
            IMSDataContext dc = new IMSDataContext();
            List<ServiceGroup> removeFromGroup = (from sg1 in dc.ServiceGroups
                                                  where (sg1.Id == sg.Id)
                                                  select sg1).ToList();
            dc.ServiceGroups.DeleteAllOnSubmit(removeFromGroup);
            dc.SubmitChanges();

            //Refresh display
            var lst = (from s in dc.ServiceGroups select s);
            myListView.ItemsSource = null;
            myListView.ItemsSource = lst;
            //myListView.SelectedIndex = 0;
        }

        private void grdEdit_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            txtName.Focus();
            txtName.SelectAll();
        }
    }    
}
