
namespace QuanLyCuaHangThuCung
{
    partial class frmDangNhap
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangNhap));
            this.Website = new System.Windows.Forms.PictureBox();
            this.Facebook = new System.Windows.Forms.PictureBox();
            this.Youtube = new System.Windows.Forms.PictureBox();
            this.btn_DangNhap = new System.Windows.Forms.Button();
            this.DangKi = new System.Windows.Forms.LinkLabel();
            this.Xem_Mat_Khau = new System.Windows.Forms.PictureBox();
            this.txt_MatKhau = new System.Windows.Forms.TextBox();
            this.txt_TaiKhoan = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnThoat = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Website)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Facebook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Youtube)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Xem_Mat_Khau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Website
            // 
            this.Website.BackColor = System.Drawing.Color.Transparent;
            this.Website.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Website.BackgroundImage")));
            this.Website.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Website.Location = new System.Drawing.Point(310, 564);
            this.Website.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Website.Name = "Website";
            this.Website.Size = new System.Drawing.Size(50, 51);
            this.Website.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Website.TabIndex = 27;
            this.Website.TabStop = false;
            this.Website.Click += new System.EventHandler(this.Website_Click);
            // 
            // Facebook
            // 
            this.Facebook.BackColor = System.Drawing.Color.Transparent;
            this.Facebook.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Facebook.BackgroundImage")));
            this.Facebook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Facebook.Location = new System.Drawing.Point(225, 564);
            this.Facebook.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Facebook.Name = "Facebook";
            this.Facebook.Size = new System.Drawing.Size(50, 51);
            this.Facebook.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Facebook.TabIndex = 26;
            this.Facebook.TabStop = false;
            this.Facebook.Click += new System.EventHandler(this.Facebook_Click);
            // 
            // Youtube
            // 
            this.Youtube.BackColor = System.Drawing.Color.Transparent;
            this.Youtube.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Youtube.BackgroundImage")));
            this.Youtube.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Youtube.Location = new System.Drawing.Point(147, 564);
            this.Youtube.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Youtube.Name = "Youtube";
            this.Youtube.Size = new System.Drawing.Size(50, 51);
            this.Youtube.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Youtube.TabIndex = 25;
            this.Youtube.TabStop = false;
            this.Youtube.Click += new System.EventHandler(this.Youtube_Click);
            // 
            // btn_DangNhap
            // 
            this.btn_DangNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_DangNhap.ForeColor = System.Drawing.Color.Red;
            this.btn_DangNhap.Location = new System.Drawing.Point(91, 396);
            this.btn_DangNhap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_DangNhap.Name = "btn_DangNhap";
            this.btn_DangNhap.Size = new System.Drawing.Size(245, 66);
            this.btn_DangNhap.TabIndex = 24;
            this.btn_DangNhap.Text = "Đăng nhập";
            this.btn_DangNhap.UseVisualStyleBackColor = false;
            this.btn_DangNhap.Click += new System.EventHandler(this.btn_DangNhap_Click);
            // 
            // DangKi
            // 
            this.DangKi.AutoSize = true;
            this.DangKi.BackColor = System.Drawing.Color.Transparent;
            this.DangKi.Location = new System.Drawing.Point(144, 492);
            this.DangKi.Name = "DangKi";
            this.DangKi.Size = new System.Drawing.Size(146, 20);
            this.DangKi.TabIndex = 23;
            this.DangKi.TabStop = true;
            this.DangKi.Text = "Chưa có tài khoản?";
            this.DangKi.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DangKi_LinkClicked);
            this.DangKi.Click += new System.EventHandler(this.DangKi_Click);
            // 
            // Xem_Mat_Khau
            // 
            this.Xem_Mat_Khau.BackColor = System.Drawing.Color.Transparent;
            this.Xem_Mat_Khau.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Xem_Mat_Khau.BackgroundImage")));
            this.Xem_Mat_Khau.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Xem_Mat_Khau.Location = new System.Drawing.Point(344, 350);
            this.Xem_Mat_Khau.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Xem_Mat_Khau.Name = "Xem_Mat_Khau";
            this.Xem_Mat_Khau.Size = new System.Drawing.Size(29, 28);
            this.Xem_Mat_Khau.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Xem_Mat_Khau.TabIndex = 22;
            this.Xem_Mat_Khau.TabStop = false;
            this.Xem_Mat_Khau.MouseLeave += new System.EventHandler(this.Xem_Mat_Khau_MouseLeave);
            this.Xem_Mat_Khau.MouseHover += new System.EventHandler(this.Xem_Mat_Khau_MouseHover);
            // 
            // txt_MatKhau
            // 
            this.txt_MatKhau.Location = new System.Drawing.Point(91, 350);
            this.txt_MatKhau.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_MatKhau.Name = "txt_MatKhau";
            this.txt_MatKhau.Size = new System.Drawing.Size(246, 26);
            this.txt_MatKhau.TabIndex = 20;
            this.txt_MatKhau.UseSystemPasswordChar = true;
            // 
            // txt_TaiKhoan
            // 
            this.txt_TaiKhoan.Location = new System.Drawing.Point(91, 291);
            this.txt_TaiKhoan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_TaiKhoan.Name = "txt_TaiKhoan";
            this.txt_TaiKhoan.Size = new System.Drawing.Size(246, 26);
            this.txt_TaiKhoan.TabIndex = 19;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(55, 350);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(29, 28);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(55, 291);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.Transparent;
            this.btnThoat.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnThoat.BackgroundImage")));
            this.btnThoat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnThoat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnThoat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Location = new System.Drawing.Point(335, 212);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(60, 51);
            this.btnThoat.TabIndex = 28;
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // frmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(434, 720);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.Website);
            this.Controls.Add(this.Facebook);
            this.Controls.Add(this.Youtube);
            this.Controls.Add(this.btn_DangNhap);
            this.Controls.Add(this.DangKi);
            this.Controls.Add(this.Xem_Mat_Khau);
            this.Controls.Add(this.txt_MatKhau);
            this.Controls.Add(this.txt_TaiKhoan);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DangNhap";
            this.Load += new System.EventHandler(this.Form_DangNhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Website)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Facebook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Youtube)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Xem_Mat_Khau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Website;
        private System.Windows.Forms.PictureBox Facebook;
        private System.Windows.Forms.PictureBox Youtube;
        private System.Windows.Forms.Button btn_DangNhap;
        private System.Windows.Forms.LinkLabel DangKi;
        private System.Windows.Forms.PictureBox Xem_Mat_Khau;
        private System.Windows.Forms.TextBox txt_MatKhau;
        private System.Windows.Forms.TextBox txt_TaiKhoan;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}