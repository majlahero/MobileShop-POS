namespace HTTT.QUAN_LY
{
    partial class F_Email
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
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.txtTieuDe = new System.Windows.Forms.TextBox();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.labelTenSP = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.labelDonGia = new System.Windows.Forms.Label();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.btnGui = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBoxInput.Controls.Add(this.txtTieuDe);
            this.groupBoxInput.Controls.Add(this.checkBox);
            this.groupBoxInput.Controls.Add(this.labelTenSP);
            this.groupBoxInput.Controls.Add(this.txtEmail);
            this.groupBoxInput.Controls.Add(this.labelDonGia);
            this.groupBoxInput.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.groupBoxInput.Location = new System.Drawing.Point(12, 12);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(810, 157);
            this.groupBoxInput.TabIndex = 2;
            this.groupBoxInput.TabStop = false;
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Location = new System.Drawing.Point(161, 64);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(394, 30);
            this.txtTieuDe.TabIndex = 5;
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(10, 115);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(293, 26);
            this.checkBox.TabIndex = 4;
            this.checkBox.Text = "Gửi email tới toàn bộ khách hàng";
            this.checkBox.UseVisualStyleBackColor = true;
            // 
            // labelTenSP
            // 
            this.labelTenSP.AutoSize = true;
            this.labelTenSP.Location = new System.Drawing.Point(6, 17);
            this.labelTenSP.Name = "labelTenSP";
            this.labelTenSP.Size = new System.Drawing.Size(149, 22);
            this.labelTenSP.TabIndex = 0;
            this.labelTenSP.Text = "Email người nhận";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(161, 14);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(394, 30);
            this.txtEmail.TabIndex = 1;
            // 
            // labelDonGia
            // 
            this.labelDonGia.AutoSize = true;
            this.labelDonGia.Location = new System.Drawing.Point(6, 72);
            this.labelDonGia.Name = "labelDonGia";
            this.labelDonGia.Size = new System.Drawing.Size(69, 22);
            this.labelDonGia.TabIndex = 3;
            this.labelDonGia.Text = "Tiêu đề";
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoiDung.Location = new System.Drawing.Point(12, 208);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(810, 293);
            this.txtNoiDung.TabIndex = 2;
            this.txtNoiDung.Text = "Chào bạn, chúng tôi gửi email từ \"Đồ án môn phân tích thiết kế hệ thống thông tin" +
    "\"";
            // 
            // btnGui
            // 
            this.btnGui.BackColor = System.Drawing.Color.SteelBlue;
            this.btnGui.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.btnGui.ForeColor = System.Drawing.Color.White;
            this.btnGui.Location = new System.Drawing.Point(678, 507);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(144, 59);
            this.btnGui.TabIndex = 3;
            this.btnGui.Text = "Gửi";
            this.btnGui.UseVisualStyleBackColor = false;
            this.btnGui.Click += new System.EventHandler(this.btnGui_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nội dung";
            // 
            // F_Email
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(854, 577);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.btnGui);
            this.Controls.Add(this.txtNoiDung);
            this.Name = "F_Email";
            this.Text = "F_Email";
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.Label labelTenSP;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label labelDonGia;
        private System.Windows.Forms.TextBox txtNoiDung;
        private System.Windows.Forms.Button btnGui;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.TextBox txtTieuDe;
        private System.Windows.Forms.Label label1;
    }
}