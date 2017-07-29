using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS.Report
{
    public partial class frmIORReportCall : Form
    {
        public frmIORReportCall()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (cmbReportKind.SelectedIndex == 0)
            {
                pgIOR a = new pgIOR();
                a.Show();
            }
            else if (cmbReportKind.SelectedIndex == 1)
            {
                pgIOR_1 a = new pgIOR_1();
                a.Show();
            }
            if (cmbReportKind.SelectedIndex == 2)
            {
                pgIOR_2 a = new pgIOR_2();
                a.Show();
            }
        }
    }
}
