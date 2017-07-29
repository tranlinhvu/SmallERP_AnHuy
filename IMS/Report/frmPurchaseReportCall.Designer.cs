namespace IMS
{
    partial class frmPurchaseReportCall
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
            this.chkEmp = new System.Windows.Forms.CheckBox();
            this.cmbVendor = new System.Windows.Forms.ComboBox();
            this.chkPharmaGroup = new System.Windows.Forms.CheckBox();
            this.cmbProductKind = new System.Windows.Forms.ComboBox();
            this.cmbProductColor = new System.Windows.Forms.ComboBox();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.dtFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkKind = new System.Windows.Forms.CheckBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpFilterReport.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFilterReport
            // 
            this.grpFilterReport.Controls.Add(this.chkEmp);
            this.grpFilterReport.Controls.Add(this.cmbVendor);
            this.grpFilterReport.Controls.Add(this.chkPharmaGroup);
            this.grpFilterReport.Controls.Add(this.cmbProductKind);
            this.grpFilterReport.Controls.Add(this.cmbProductColor);
            this.grpFilterReport.Controls.Add(this.dtToDate);
            this.grpFilterReport.Controls.Add(this.dtFromDate);
            this.grpFilterReport.Controls.Add(this.label2);
            this.grpFilterReport.Controls.Add(this.label1);
            this.grpFilterReport.Controls.Add(this.chkKind);
            this.grpFilterReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFilterReport.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
            this.grpFilterReport.Location = new System.Drawing.Point(12, 95);
            this.grpFilterReport.Name = "grpFilterReport";
            this.grpFilterReport.Size = new System.Drawing.Size(562, 260);
            this.grpFilterReport.TabIndex = 0;
            this.grpFilterReport.TabStop = false;
            // 
            // chkEmp
            // 
            this.chkEmp.AutoSize = true;
            this.chkEmp.Location = new System.Drawing.Point(24, 178);
            this.chkEmp.Name = "chkEmp";
            this.chkEmp.Size = new System.Drawing.Size(115, 21);
            this.chkEmp.TabIndex = 115;
            this.chkEmp.Text = "Nhà cung cấp";
            this.chkEmp.UseVisualStyleBackColor = true;
            this.chkEmp.CheckedChanged += new System.EventHandler(this.chkEmp_CheckedChanged);
            // 
            // cmbVendor
            // 
            this.cmbVendor.Enabled = false;
            this.cmbVendor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbVendor.FormattingEnabled = true;
            this.cmbVendor.Location = new System.Drawing.Point(24, 203);
            this.cmbVendor.Name = "cmbVendor";
            this.cmbVendor.Size = new System.Drawing.Size(226, 28);
            this.cmbVendor.TabIndex = 112;
            this.cmbVendor.SelectedIndexChanged += new System.EventHandler(this.cmbEmp_SelectedIndexChanged);
            // 
            // chkPharmaGroup
            // 
            this.chkPharmaGroup.AutoSize = true;
            this.chkPharmaGroup.Location = new System.Drawing.Point(303, 98);
            this.chkPharmaGroup.Name = "chkPharmaGroup";
            this.chkPharmaGroup.Size = new System.Drawing.Size(84, 21);
            this.chkPharmaGroup.TabIndex = 913;
            this.chkPharmaGroup.Text = "Màu sắc:";
            this.chkPharmaGroup.UseVisualStyleBackColor = true;
            this.chkPharmaGroup.CheckedChanged += new System.EventHandler(this.chkPharmaGroup_CheckedChanged);
            // 
            // cmbProductKind
            // 
            this.cmbProductKind.Enabled = false;
            this.cmbProductKind.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProductKind.FormattingEnabled = true;
            this.cmbProductKind.Location = new System.Drawing.Point(24, 122);
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
            this.cmbProductColor.Location = new System.Drawing.Point(303, 122);
            this.cmbProductColor.Name = "cmbProductColor";
            this.cmbProductColor.Size = new System.Drawing.Size(226, 28);
            this.cmbProductColor.TabIndex = 912;
            this.cmbProductColor.SelectedIndexChanged += new System.EventHandler(this.cmbPharmaGroup_SelectedIndexChanged);
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
            // chkKind
            // 
            this.chkKind.AutoSize = true;
            this.chkKind.Location = new System.Drawing.Point(24, 98);
            this.chkKind.Name = "chkKind";
            this.chkKind.Size = new System.Drawing.Size(94, 21);
            this.chkKind.TabIndex = 911;
            this.chkKind.Text = "Loại hàng:";
            this.chkKind.UseVisualStyleBackColor = true;
            this.chkKind.CheckedChanged += new System.EventHandler(this.chkKind_CheckedChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.Maroon;
            this.btnPrint.Image = global::IMS.Properties.Resources.Preview_16x16;
            this.btnPrint.Location = new System.Drawing.Point(428, 361);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(146, 39);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "Xem báo cáo";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Location = new System.Drawing.Point(12, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 74);
            this.panel1.TabIndex = 9;
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
            // frmPurchaseReportCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.ClientSize = new System.Drawing.Size(582, 412);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.grpFilterReport);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPurchaseReportCall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IMS - Báo cáo mua hàng";
            this.Load += new System.EventHandler(this.frmPurchaseReport_Load);
            this.Resize += new System.EventHandler(this.frmPurchaseReport_Resize);
            this.grpFilterReport.ResumeLayout(false);
            this.grpFilterReport.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFilterReport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.CheckBox chkEmp;
        private System.Windows.Forms.ComboBox cmbVendor;
        private System.Windows.Forms.CheckBox chkKind;
        private System.Windows.Forms.ComboBox cmbProductKind;
        private System.Windows.Forms.CheckBox chkPharmaGroup;
        private System.Windows.Forms.ComboBox cmbProductColor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        //private TextGridView cmbPharmaCode;
    }
}