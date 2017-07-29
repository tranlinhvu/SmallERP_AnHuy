namespace IMS.Report
{
    partial class frmSaleReportCall
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpFilterReport = new System.Windows.Forms.GroupBox();
            this.chkStaff = new System.Windows.Forms.CheckBox();
            this.chkCustomer = new System.Windows.Forms.CheckBox();
            this.cmbStaff = new System.Windows.Forms.ComboBox();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.chkProductColor = new System.Windows.Forms.CheckBox();
            this.cmbProductKind = new System.Windows.Forms.ComboBox();
            this.cmbProductColor = new System.Windows.Forms.ComboBox();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.dtFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkProductKind = new System.Windows.Forms.CheckBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.grpFilterReport.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFilterReport
            // 
            this.grpFilterReport.Controls.Add(this.chkStaff);
            this.grpFilterReport.Controls.Add(this.chkCustomer);
            this.grpFilterReport.Controls.Add(this.cmbStaff);
            this.grpFilterReport.Controls.Add(this.cmbCustomer);
            this.grpFilterReport.Controls.Add(this.chkProductColor);
            this.grpFilterReport.Controls.Add(this.cmbProductKind);
            this.grpFilterReport.Controls.Add(this.cmbProductColor);
            this.grpFilterReport.Controls.Add(this.dtToDate);
            this.grpFilterReport.Controls.Add(this.dtFromDate);
            this.grpFilterReport.Controls.Add(this.label2);
            this.grpFilterReport.Controls.Add(this.label1);
            this.grpFilterReport.Controls.Add(this.chkProductKind);
            this.grpFilterReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFilterReport.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.grpFilterReport.Location = new System.Drawing.Point(8, 120);
            this.grpFilterReport.Name = "grpFilterReport";
            this.grpFilterReport.Size = new System.Drawing.Size(562, 265);
            this.grpFilterReport.TabIndex = 9;
            this.grpFilterReport.TabStop = false;
            // 
            // chkStaff
            // 
            this.chkStaff.AutoSize = true;
            this.chkStaff.Location = new System.Drawing.Point(24, 93);
            this.chkStaff.Name = "chkStaff";
            this.chkStaff.Size = new System.Drawing.Size(158, 21);
            this.chkStaff.TabIndex = 915;
            this.chkStaff.Text = "Nhân viên giao hàng";
            this.chkStaff.UseVisualStyleBackColor = true;
            this.chkStaff.CheckedChanged += new System.EventHandler(this.chkStaff_CheckedChanged);
            // 
            // chkCustomer
            // 
            this.chkCustomer.AutoSize = true;
            this.chkCustomer.Location = new System.Drawing.Point(303, 91);
            this.chkCustomer.Name = "chkCustomer";
            this.chkCustomer.Size = new System.Drawing.Size(103, 21);
            this.chkCustomer.TabIndex = 115;
            this.chkCustomer.Text = "Khách hàng";
            this.chkCustomer.UseVisualStyleBackColor = true;
            this.chkCustomer.CheckedChanged += new System.EventHandler(this.chkCustomer_CheckedChanged);
            // 
            // cmbStaff
            // 
            this.cmbStaff.Enabled = false;
            this.cmbStaff.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStaff.FormattingEnabled = true;
            this.cmbStaff.Location = new System.Drawing.Point(24, 116);
            this.cmbStaff.Name = "cmbStaff";
            this.cmbStaff.Size = new System.Drawing.Size(226, 28);
            this.cmbStaff.TabIndex = 914;
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.Enabled = false;
            this.cmbCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(303, 116);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(226, 28);
            this.cmbCustomer.TabIndex = 112;
            // 
            // chkProductColor
            // 
            this.chkProductColor.AutoSize = true;
            this.chkProductColor.Location = new System.Drawing.Point(303, 173);
            this.chkProductColor.Name = "chkProductColor";
            this.chkProductColor.Size = new System.Drawing.Size(84, 21);
            this.chkProductColor.TabIndex = 913;
            this.chkProductColor.Text = "Màu sắc:";
            this.chkProductColor.UseVisualStyleBackColor = true;
            this.chkProductColor.CheckedChanged += new System.EventHandler(this.chkProductColor_CheckedChanged);
            // 
            // cmbProductKind
            // 
            this.cmbProductKind.Enabled = false;
            this.cmbProductKind.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProductKind.FormattingEnabled = true;
            this.cmbProductKind.Location = new System.Drawing.Point(24, 198);
            this.cmbProductKind.Name = "cmbProductKind";
            this.cmbProductKind.Size = new System.Drawing.Size(226, 28);
            this.cmbProductKind.TabIndex = 910;
            this.cmbProductKind.SelectedIndexChanged += new System.EventHandler(this.cmbProductKind_SelectedIndexChanged);
            // 
            // cmbProductColor
            // 
            this.cmbProductColor.Enabled = false;
            this.cmbProductColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProductColor.FormattingEnabled = true;
            this.cmbProductColor.Location = new System.Drawing.Point(303, 198);
            this.cmbProductColor.Name = "cmbProductColor";
            this.cmbProductColor.Size = new System.Drawing.Size(226, 28);
            this.cmbProductColor.TabIndex = 912;
            // 
            // dtToDate
            // 
            this.dtToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtToDate.Location = new System.Drawing.Point(303, 43);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(226, 26);
            this.dtToDate.TabIndex = 15;
            // 
            // dtFromDate
            // 
            this.dtFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFromDate.Location = new System.Drawing.Point(24, 43);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(226, 26);
            this.dtFromDate.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(303, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Đến ngày:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày:";
            // 
            // chkProductKind
            // 
            this.chkProductKind.AutoSize = true;
            this.chkProductKind.Location = new System.Drawing.Point(24, 175);
            this.chkProductKind.Name = "chkProductKind";
            this.chkProductKind.Size = new System.Drawing.Size(94, 21);
            this.chkProductKind.TabIndex = 911;
            this.chkProductKind.Text = "Loại hàng:";
            this.chkProductKind.UseVisualStyleBackColor = true;
            this.chkProductKind.CheckedChanged += new System.EventHandler(this.chkProductKind_CheckedChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.Maroon;
            this.btnPrint.Image = global::IMS.Properties.Resources.Preview_16x16;
            this.btnPrint.Location = new System.Drawing.Point(426, 403);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(146, 39);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "Xem báo cáo";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Location = new System.Drawing.Point(8, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 74);
            this.panel1.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.label3.Location = new System.Drawing.Point(20, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 17);
            this.label3.TabIndex = 913;
            this.label3.Text = "Phân loại báo cáo";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Báo cáo theo loại hàng",
            "Báo cáo theo màu sắc",
            "Báo cáo tổng hợp"});
            this.comboBox1.Location = new System.Drawing.Point(23, 30);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(505, 28);
            this.comboBox1.TabIndex = 912;
            // 
            // frmSaleReportCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.ClientSize = new System.Drawing.Size(582, 454);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpFilterReport);
            this.Controls.Add(this.btnPrint);
            this.Name = "frmSaleReportCall";
            this.Text = "IMS - Báo cáo bán hàng";
            this.Load += new System.EventHandler(this.frmSaleReportCall_Load);
            this.grpFilterReport.ResumeLayout(false);
            this.grpFilterReport.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFilterReport;
        private System.Windows.Forms.CheckBox chkStaff;
        private System.Windows.Forms.CheckBox chkCustomer;
        private System.Windows.Forms.ComboBox cmbStaff;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.CheckBox chkProductColor;
        private System.Windows.Forms.ComboBox cmbProductKind;
        private System.Windows.Forms.ComboBox cmbProductColor;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkProductKind;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}