using System;
using System.Drawing;
using System.Windows.Forms;

namespace HTTT.QUAN_LY
{
    partial class F_BaoHanh
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        public class CustomGroupBox : GroupBox
        {
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                e.Graphics.DrawRectangle(new Pen(Color.DarkOrange, 2), 0, 7, this.Width - 1, this.Height - 9);
            }
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCreateWarranty = new System.Windows.Forms.TabPage();
            this.CustomGroupBox5 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboProductInfo = new System.Windows.Forms.ComboBox();
            this.cboInvoiceId = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSaveWarranty = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cboCustomerInfo = new System.Windows.Forms.ComboBox();
            this.nudWarrantyPeriod = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.tabCheckWarranty = new System.Windows.Forms.TabPage();
            this.CustomGroupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvWarrantyInfo = new System.Windows.Forms.DataGridView();
            this.btnCheckWarranty = new System.Windows.Forms.Button();
            this.txtWarrantyId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabCreateAppointment = new System.Windows.Forms.TabPage();
            this.CustomGroupBox2 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAppCustomerInfo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAppProductId = new System.Windows.Forms.TextBox();
            this.btnSaveAppointment = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAppWarrantyId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpAppointmentDate = new System.Windows.Forms.DateTimePicker();
            this.tabCheckAppointment = new System.Windows.Forms.TabPage();
            this.CustomGroupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvAppointmentInfo = new System.Windows.Forms.DataGridView();
            this.btnCheckAppointment = new System.Windows.Forms.Button();
            this.txtAppointmentId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabReturnProduct = new System.Windows.Forms.TabPage();
            this.CustomGroupBox4 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtReturnStatus = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnConfirmReturn = new System.Windows.Forms.Button();
            this.btnCheckReturn = new System.Windows.Forms.Button();
            this.dgvReturnInfo = new System.Windows.Forms.DataGridView();
            this.txtReturnAppointmentId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabCreateWarranty.SuspendLayout();
            this.CustomGroupBox5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWarrantyPeriod)).BeginInit();
            this.tabCheckWarranty.SuspendLayout();
            this.CustomGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarrantyInfo)).BeginInit();
            this.tabCreateAppointment.SuspendLayout();
            this.CustomGroupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabCheckAppointment.SuspendLayout();
            this.CustomGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentInfo)).BeginInit();
            this.tabReturnProduct.SuspendLayout();
            this.CustomGroupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCreateWarranty);
            this.tabControl1.Controls.Add(this.tabCheckWarranty);
            this.tabControl1.Controls.Add(this.tabCreateAppointment);
            this.tabControl1.Controls.Add(this.tabCheckAppointment);
            this.tabControl1.Controls.Add(this.tabReturnProduct);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(200, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1049, 540);
            this.tabControl1.TabIndex = 0;
            // 
            // tabCreateWarranty
            // 
            this.tabCreateWarranty.Controls.Add(this.CustomGroupBox5);
            this.tabCreateWarranty.Location = new System.Drawing.Point(4, 34);
            this.tabCreateWarranty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCreateWarranty.Name = "tabCreateWarranty";
            this.tabCreateWarranty.Size = new System.Drawing.Size(1041, 502);
            this.tabCreateWarranty.TabIndex = 4;
            this.tabCreateWarranty.Text = "Tạo phiếu bảo hành";
            this.tabCreateWarranty.UseVisualStyleBackColor = true;
            // 
            // CustomGroupBox5
            // 
            this.CustomGroupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.CustomGroupBox5.Controls.Add(this.panel1);
            this.CustomGroupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomGroupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomGroupBox5.Location = new System.Drawing.Point(0, 0);
            this.CustomGroupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CustomGroupBox5.Name = "CustomGroupBox5";
            this.CustomGroupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CustomGroupBox5.Size = new System.Drawing.Size(1041, 502);
            this.CustomGroupBox5.TabIndex = 0;
            this.CustomGroupBox5.TabStop = false;
            this.CustomGroupBox5.Text = "Thông tin bảo hành";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.cboProductInfo);
            this.panel1.Controls.Add(this.cboInvoiceId);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnSaveWarranty);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.cboCustomerInfo);
            this.panel1.Controls.Add(this.nudWarrantyPeriod);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Location = new System.Drawing.Point(24, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(728, 248);
            this.panel1.TabIndex = 10;
            // 
            // cboProductInfo
            // 
            this.cboProductInfo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProductInfo.FormattingEnabled = true;
            this.cboProductInfo.Location = new System.Drawing.Point(256, 96);
            this.cboProductInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboProductInfo.Name = "cboProductInfo";
            this.cboProductInfo.Size = new System.Drawing.Size(400, 28);
            this.cboProductInfo.TabIndex = 1;
            // 
            // cboInvoiceId
            // 
            this.cboInvoiceId.FormattingEnabled = true;
            this.cboInvoiceId.Location = new System.Drawing.Point(256, 16);
            this.cboInvoiceId.Name = "cboInvoiceId";
            this.cboInvoiceId.Size = new System.Drawing.Size(152, 28);
            this.cboInvoiceId.TabIndex = 9;
            this.cboInvoiceId.SelectedIndexChanged += new System.EventHandler(this.cboInvoiceId_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(161, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Thông tin sản phẩm:";
            // 
            // btnSaveWarranty
            // 
            this.btnSaveWarranty.BackColor = System.Drawing.Color.Red;
            this.btnSaveWarranty.Location = new System.Drawing.Point(256, 192);
            this.btnSaveWarranty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveWarranty.Name = "btnSaveWarranty";
            this.btnSaveWarranty.Size = new System.Drawing.Size(120, 32);
            this.btnSaveWarranty.TabIndex = 8;
            this.btnSaveWarranty.Text = "Lưu";
            this.btnSaveWarranty.UseVisualStyleBackColor = false;
            this.btnSaveWarranty.Click += new System.EventHandler(this.btnSaveWarranty_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(173, 20);
            this.label11.TabIndex = 2;
            this.label11.Text = "Thông tin khách hàng:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(32, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 20);
            this.label13.TabIndex = 6;
            this.label13.Text = "Mã hóa đơn:";
            // 
            // cboCustomerInfo
            // 
            this.cboCustomerInfo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCustomerInfo.FormattingEnabled = true;
            this.cboCustomerInfo.Location = new System.Drawing.Point(256, 56);
            this.cboCustomerInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboCustomerInfo.Name = "cboCustomerInfo";
            this.cboCustomerInfo.Size = new System.Drawing.Size(400, 28);
            this.cboCustomerInfo.TabIndex = 3;
            // 
            // nudWarrantyPeriod
            // 
            this.nudWarrantyPeriod.Location = new System.Drawing.Point(256, 136);
            this.nudWarrantyPeriod.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudWarrantyPeriod.Maximum = new decimal(new int[] {
            36,
            0,
            0,
            0});
            this.nudWarrantyPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWarrantyPeriod.Name = "nudWarrantyPeriod";
            this.nudWarrantyPeriod.Size = new System.Drawing.Size(100, 27);
            this.nudWarrantyPeriod.TabIndex = 5;
            this.nudWarrantyPeriod.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(32, 144);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(213, 20);
            this.label12.TabIndex = 4;
            this.label12.Text = "Thời gian bảo hành (tháng):";
            // 
            // tabCheckWarranty
            // 
            this.tabCheckWarranty.Controls.Add(this.CustomGroupBox1);
            this.tabCheckWarranty.Location = new System.Drawing.Point(4, 34);
            this.tabCheckWarranty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCheckWarranty.Name = "tabCheckWarranty";
            this.tabCheckWarranty.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCheckWarranty.Size = new System.Drawing.Size(1041, 502);
            this.tabCheckWarranty.TabIndex = 0;
            this.tabCheckWarranty.Text = "Kiểm tra phiếu bảo hành";
            this.tabCheckWarranty.UseVisualStyleBackColor = true;
            // 
            // CustomGroupBox1
            // 
            this.CustomGroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.CustomGroupBox1.Controls.Add(this.dgvWarrantyInfo);
            this.CustomGroupBox1.Controls.Add(this.btnCheckWarranty);
            this.CustomGroupBox1.Controls.Add(this.txtWarrantyId);
            this.CustomGroupBox1.Controls.Add(this.label1);
            this.CustomGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomGroupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CustomGroupBox1.Location = new System.Drawing.Point(3, 2);
            this.CustomGroupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CustomGroupBox1.Name = "CustomGroupBox1";
            this.CustomGroupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CustomGroupBox1.Size = new System.Drawing.Size(1035, 498);
            this.CustomGroupBox1.TabIndex = 0;
            this.CustomGroupBox1.TabStop = false;
            this.CustomGroupBox1.Text = "Thông tin phiếu bảo hành";
            // 
            // dgvWarrantyInfo
            // 
            this.dgvWarrantyInfo.AllowUserToAddRows = false;
            this.dgvWarrantyInfo.AllowUserToDeleteRows = false;
            this.dgvWarrantyInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWarrantyInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvWarrantyInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvWarrantyInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWarrantyInfo.Location = new System.Drawing.Point(20, 64);
            this.dgvWarrantyInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvWarrantyInfo.Name = "dgvWarrantyInfo";
            this.dgvWarrantyInfo.ReadOnly = true;
            this.dgvWarrantyInfo.RowHeadersWidth = 51;
            this.dgvWarrantyInfo.RowTemplate.Height = 24;
            this.dgvWarrantyInfo.Size = new System.Drawing.Size(1009, 429);
            this.dgvWarrantyInfo.TabIndex = 3;
            this.dgvWarrantyInfo.DoubleClick += new System.EventHandler(this.dgvWarrantyInfo_DoubleClick);
            // 
            // btnCheckWarranty
            // 
            this.btnCheckWarranty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCheckWarranty.Location = new System.Drawing.Point(448, 24);
            this.btnCheckWarranty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCheckWarranty.Name = "btnCheckWarranty";
            this.btnCheckWarranty.Size = new System.Drawing.Size(112, 32);
            this.btnCheckWarranty.TabIndex = 2;
            this.btnCheckWarranty.Text = "Kiểm tra";
            this.btnCheckWarranty.UseVisualStyleBackColor = false;
            this.btnCheckWarranty.Click += new System.EventHandler(this.btnCheckWarranty_Click);
            // 
            // txtWarrantyId
            // 
            this.txtWarrantyId.Location = new System.Drawing.Point(216, 24);
            this.txtWarrantyId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtWarrantyId.Name = "txtWarrantyId";
            this.txtWarrantyId.Size = new System.Drawing.Size(200, 28);
            this.txtWarrantyId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã phiếu bảo hành:";
            // 
            // tabCreateAppointment
            // 
            this.tabCreateAppointment.Controls.Add(this.CustomGroupBox2);
            this.tabCreateAppointment.Location = new System.Drawing.Point(4, 34);
            this.tabCreateAppointment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCreateAppointment.Name = "tabCreateAppointment";
            this.tabCreateAppointment.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCreateAppointment.Size = new System.Drawing.Size(1041, 502);
            this.tabCreateAppointment.TabIndex = 1;
            this.tabCreateAppointment.Text = "Lập phiếu hẹn trả hàng";
            this.tabCreateAppointment.UseVisualStyleBackColor = true;
            // 
            // CustomGroupBox2
            // 
            this.CustomGroupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.CustomGroupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CustomGroupBox2.Controls.Add(this.panel3);
            this.CustomGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomGroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomGroupBox2.Location = new System.Drawing.Point(3, 2);
            this.CustomGroupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CustomGroupBox2.Name = "CustomGroupBox2";
            this.CustomGroupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CustomGroupBox2.Size = new System.Drawing.Size(1035, 498);
            this.CustomGroupBox2.TabIndex = 0;
            this.CustomGroupBox2.TabStop = false;
            this.CustomGroupBox2.Text = "Thông tin phiếu hẹn";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel3.Controls.Add(this.txtProductName);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtAppCustomerInfo);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtAppProductId);
            this.panel3.Controls.Add(this.btnSaveAppointment);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtAppWarrantyId);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.dtpAppointmentDate);
            this.panel3.Location = new System.Drawing.Point(32, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(672, 304);
            this.panel3.TabIndex = 11;
            // 
            // txtProductName
            // 
            this.txtProductName.Enabled = false;
            this.txtProductName.Location = new System.Drawing.Point(216, 152);
            this.txtProductName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(400, 27);
            this.txtProductName.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Tên sản phẩm: ";
            // 
            // txtAppCustomerInfo
            // 
            this.txtAppCustomerInfo.Enabled = false;
            this.txtAppCustomerInfo.Location = new System.Drawing.Point(216, 112);
            this.txtAppCustomerInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAppCustomerInfo.Name = "txtAppCustomerInfo";
            this.txtAppCustomerInfo.Size = new System.Drawing.Size(400, 27);
            this.txtAppCustomerInfo.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã sản phẩm:";
            // 
            // txtAppProductId
            // 
            this.txtAppProductId.Location = new System.Drawing.Point(216, 40);
            this.txtAppProductId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAppProductId.Name = "txtAppProductId";
            this.txtAppProductId.Size = new System.Drawing.Size(200, 27);
            this.txtAppProductId.TabIndex = 1;
            // 
            // btnSaveAppointment
            // 
            this.btnSaveAppointment.BackColor = System.Drawing.Color.Red;
            this.btnSaveAppointment.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveAppointment.Location = new System.Drawing.Point(216, 208);
            this.btnSaveAppointment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveAppointment.Name = "btnSaveAppointment";
            this.btnSaveAppointment.Size = new System.Drawing.Size(120, 32);
            this.btnSaveAppointment.TabIndex = 8;
            this.btnSaveAppointment.Text = "Lưu";
            this.btnSaveAppointment.UseVisualStyleBackColor = false;
            this.btnSaveAppointment.Click += new System.EventHandler(this.btnSaveAppointment_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mã phiếu bảo hành:";
            // 
            // txtAppWarrantyId
            // 
            this.txtAppWarrantyId.Location = new System.Drawing.Point(216, 8);
            this.txtAppWarrantyId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAppWarrantyId.Name = "txtAppWarrantyId";
            this.txtAppWarrantyId.Size = new System.Drawing.Size(200, 27);
            this.txtAppWarrantyId.TabIndex = 3;
            this.txtAppWarrantyId.TextChanged += new System.EventHandler(this.txtAppWarrantyId_TabIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Thông tin khách hàng:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Ngày hẹn trả hàng:";
            // 
            // dtpAppointmentDate
            // 
            this.dtpAppointmentDate.CustomFormat = "dd/MM/yyyy";
            this.dtpAppointmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAppointmentDate.Location = new System.Drawing.Point(216, 80);
            this.dtpAppointmentDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpAppointmentDate.Name = "dtpAppointmentDate";
            this.dtpAppointmentDate.Size = new System.Drawing.Size(200, 27);
            this.dtpAppointmentDate.TabIndex = 5;
            this.dtpAppointmentDate.Value = new System.DateTime(2023, 5, 20, 0, 0, 0, 0);
            // 
            // tabCheckAppointment
            // 
            this.tabCheckAppointment.Controls.Add(this.CustomGroupBox3);
            this.tabCheckAppointment.Location = new System.Drawing.Point(4, 34);
            this.tabCheckAppointment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCheckAppointment.Name = "tabCheckAppointment";
            this.tabCheckAppointment.Size = new System.Drawing.Size(1041, 502);
            this.tabCheckAppointment.TabIndex = 2;
            this.tabCheckAppointment.Text = "Kiểm tra phiếu hẹn trả hàng";
            this.tabCheckAppointment.UseVisualStyleBackColor = true;
            // 
            // CustomGroupBox3
            // 
            this.CustomGroupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.CustomGroupBox3.Controls.Add(this.dgvAppointmentInfo);
            this.CustomGroupBox3.Controls.Add(this.btnCheckAppointment);
            this.CustomGroupBox3.Controls.Add(this.txtAppointmentId);
            this.CustomGroupBox3.Controls.Add(this.label7);
            this.CustomGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomGroupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.CustomGroupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CustomGroupBox3.Name = "CustomGroupBox3";
            this.CustomGroupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CustomGroupBox3.Size = new System.Drawing.Size(1041, 502);
            this.CustomGroupBox3.TabIndex = 0;
            this.CustomGroupBox3.TabStop = false;
            this.CustomGroupBox3.Text = "Thông tin phiếu hẹn";
            // 
            // dgvAppointmentInfo
            // 
            this.dgvAppointmentInfo.AllowUserToAddRows = false;
            this.dgvAppointmentInfo.AllowUserToDeleteRows = false;
            this.dgvAppointmentInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAppointmentInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAppointmentInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgvAppointmentInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointmentInfo.Location = new System.Drawing.Point(20, 64);
            this.dgvAppointmentInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvAppointmentInfo.Name = "dgvAppointmentInfo";
            this.dgvAppointmentInfo.ReadOnly = true;
            this.dgvAppointmentInfo.RowHeadersWidth = 51;
            this.dgvAppointmentInfo.RowTemplate.Height = 24;
            this.dgvAppointmentInfo.Size = new System.Drawing.Size(1005, 431);
            this.dgvAppointmentInfo.TabIndex = 3;
            // 
            // btnCheckAppointment
            // 
            this.btnCheckAppointment.BackColor = System.Drawing.Color.Aqua;
            this.btnCheckAppointment.Location = new System.Drawing.Point(400, 24);
            this.btnCheckAppointment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCheckAppointment.Name = "btnCheckAppointment";
            this.btnCheckAppointment.Size = new System.Drawing.Size(112, 32);
            this.btnCheckAppointment.TabIndex = 2;
            this.btnCheckAppointment.Text = "Kiểm tra";
            this.btnCheckAppointment.UseVisualStyleBackColor = false;
            this.btnCheckAppointment.Click += new System.EventHandler(this.btnCheckAppointment_Click);
            // 
            // txtAppointmentId
            // 
            this.txtAppointmentId.Location = new System.Drawing.Point(180, 24);
            this.txtAppointmentId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAppointmentId.Name = "txtAppointmentId";
            this.txtAppointmentId.Size = new System.Drawing.Size(200, 27);
            this.txtAppointmentId.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Mã phiếu hẹn:";
            // 
            // tabReturnProduct
            // 
            this.tabReturnProduct.Controls.Add(this.CustomGroupBox4);
            this.tabReturnProduct.Location = new System.Drawing.Point(4, 34);
            this.tabReturnProduct.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabReturnProduct.Name = "tabReturnProduct";
            this.tabReturnProduct.Size = new System.Drawing.Size(1041, 502);
            this.tabReturnProduct.TabIndex = 3;
            this.tabReturnProduct.Text = "Trả sản phẩm";
            this.tabReturnProduct.UseVisualStyleBackColor = true;
            // 
            // CustomGroupBox4
            // 
            this.CustomGroupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.CustomGroupBox4.Controls.Add(this.panel2);
            this.CustomGroupBox4.Controls.Add(this.btnCheckReturn);
            this.CustomGroupBox4.Controls.Add(this.dgvReturnInfo);
            this.CustomGroupBox4.Controls.Add(this.txtReturnAppointmentId);
            this.CustomGroupBox4.Controls.Add(this.label9);
            this.CustomGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomGroupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomGroupBox4.Location = new System.Drawing.Point(0, 0);
            this.CustomGroupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CustomGroupBox4.Name = "CustomGroupBox4";
            this.CustomGroupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CustomGroupBox4.Size = new System.Drawing.Size(1041, 502);
            this.CustomGroupBox4.TabIndex = 0;
            this.CustomGroupBox4.TabStop = false;
            this.CustomGroupBox4.Text = "Thông tin trả hàng";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel2.Controls.Add(this.txtReturnStatus);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.btnConfirmReturn);
            this.panel2.Location = new System.Drawing.Point(24, 272);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(680, 112);
            this.panel2.TabIndex = 8;
            // 
            // txtReturnStatus
            // 
            this.txtReturnStatus.Location = new System.Drawing.Point(168, 16);
            this.txtReturnStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtReturnStatus.Multiline = true;
            this.txtReturnStatus.Name = "txtReturnStatus";
            this.txtReturnStatus.Size = new System.Drawing.Size(460, 32);
            this.txtReturnStatus.TabIndex = 5;
            this.txtReturnStatus.Text = "Sản phẩm đã được sửa chữa xong, hoạt động tốt";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(167, 20);
            this.label10.TabIndex = 4;
            this.label10.Text = "Tình trạng sản phẩm:";
            // 
            // btnConfirmReturn
            // 
            this.btnConfirmReturn.BackColor = System.Drawing.Color.LightCoral;
            this.btnConfirmReturn.Location = new System.Drawing.Point(168, 64);
            this.btnConfirmReturn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfirmReturn.Name = "btnConfirmReturn";
            this.btnConfirmReturn.Size = new System.Drawing.Size(164, 40);
            this.btnConfirmReturn.TabIndex = 6;
            this.btnConfirmReturn.Text = "Xác nhận trả hàng";
            this.btnConfirmReturn.UseVisualStyleBackColor = false;
            this.btnConfirmReturn.Click += new System.EventHandler(this.btnConfirmReturn_Click);
            // 
            // btnCheckReturn
            // 
            this.btnCheckReturn.BackColor = System.Drawing.Color.Aqua;
            this.btnCheckReturn.Location = new System.Drawing.Point(416, 24);
            this.btnCheckReturn.Name = "btnCheckReturn";
            this.btnCheckReturn.Size = new System.Drawing.Size(112, 32);
            this.btnCheckReturn.TabIndex = 7;
            this.btnCheckReturn.Text = "Kiểm tra";
            this.btnCheckReturn.UseVisualStyleBackColor = false;
            this.btnCheckReturn.Click += new System.EventHandler(this.btnCheckReturn_Click);
            // 
            // dgvReturnInfo
            // 
            this.dgvReturnInfo.AllowUserToAddRows = false;
            this.dgvReturnInfo.AllowUserToDeleteRows = false;
            this.dgvReturnInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvReturnInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvReturnInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgvReturnInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReturnInfo.Location = new System.Drawing.Point(20, 64);
            this.dgvReturnInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvReturnInfo.Name = "dgvReturnInfo";
            this.dgvReturnInfo.ReadOnly = true;
            this.dgvReturnInfo.RowHeadersWidth = 51;
            this.dgvReturnInfo.RowTemplate.Height = 24;
            this.dgvReturnInfo.Size = new System.Drawing.Size(913, 200);
            this.dgvReturnInfo.TabIndex = 3;
            // 
            // txtReturnAppointmentId
            // 
            this.txtReturnAppointmentId.Location = new System.Drawing.Point(180, 24);
            this.txtReturnAppointmentId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtReturnAppointmentId.Name = "txtReturnAppointmentId";
            this.txtReturnAppointmentId.Size = new System.Drawing.Size(200, 27);
            this.txtReturnAppointmentId.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "Mã phiếu hẹn:";
            // 
            // F_BaoHanh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 540);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "F_BaoHanh";
            this.Text = "Hệ thống Quản lý Bảo hành";
            this.Load += new System.EventHandler(this.F_BaoHanh_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabCreateWarranty.ResumeLayout(false);
            this.CustomGroupBox5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWarrantyPeriod)).EndInit();
            this.tabCheckWarranty.ResumeLayout(false);
            this.CustomGroupBox1.ResumeLayout(false);
            this.CustomGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarrantyInfo)).EndInit();
            this.tabCreateAppointment.ResumeLayout(false);
            this.CustomGroupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabCheckAppointment.ResumeLayout(false);
            this.CustomGroupBox3.ResumeLayout(false);
            this.CustomGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentInfo)).EndInit();
            this.tabReturnProduct.ResumeLayout(false);
            this.CustomGroupBox4.ResumeLayout(false);
            this.CustomGroupBox4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabCheckWarranty;
        private System.Windows.Forms.TabPage tabCreateAppointment;
        private System.Windows.Forms.TabPage tabCheckAppointment;
        private System.Windows.Forms.TabPage tabReturnProduct;
        private System.Windows.Forms.TabPage tabCreateWarranty;
        private System.Windows.Forms.GroupBox CustomGroupBox1;
        private System.Windows.Forms.DataGridView dgvWarrantyInfo;
        private System.Windows.Forms.Button btnCheckWarranty;
        private System.Windows.Forms.TextBox txtWarrantyId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox CustomGroupBox2;
        private System.Windows.Forms.Button btnSaveAppointment;
        private System.Windows.Forms.TextBox txtAppCustomerInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpAppointmentDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAppWarrantyId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAppProductId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox CustomGroupBox3;
        private System.Windows.Forms.DataGridView dgvAppointmentInfo;
        private System.Windows.Forms.Button btnCheckAppointment;
        private System.Windows.Forms.TextBox txtAppointmentId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox CustomGroupBox4;
        private System.Windows.Forms.Button btnConfirmReturn;
        private System.Windows.Forms.TextBox txtReturnStatus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dgvReturnInfo;
        private System.Windows.Forms.TextBox txtReturnAppointmentId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox CustomGroupBox5;
        private System.Windows.Forms.Button btnSaveWarranty;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudWarrantyPeriod;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboCustomerInfo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboProductInfo;
        private System.Windows.Forms.Label label8;
        private Button btnCheckReturn;
        private TabControl tabControl1;
        private ComboBox cboInvoiceId;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private TextBox txtProductName;
        private Label label6;
    }
}