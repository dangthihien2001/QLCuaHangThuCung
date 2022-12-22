using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangThuCung
{
    public partial class frmTaiKhoan : Form
    {
        Module.DataAccess dtbase = new Module.DataAccess();
        public static string TenDangNhap;
        public frmTaiKhoan()
        {
            InitializeComponent();
        }

        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            txtMaNV.Text = Form1.userMaNV;
            txtTenNV.Text = Form1.userName;
            DataTable dtdangNhap = dtbase.DataReader("Select * from DangNhap where MaNV = '" + txtMaNV.Text + "'");
            txtTenTK.Text = dtdangNhap.Rows[0]["TaiKhoan"].ToString();
            txtMatKhau.Text = dtdangNhap.Rows[0]["MatKhau"].ToString();
            TenDangNhap = txtTenTK.Text;//
            HienThi();
        }

        void HienThi()
        {
            txtMaNV.Enabled = false;
            txtTenNV.Enabled = false;
            txtTenTK.Enabled = false;
            txtMatKhau.Enabled = false;
        }

        private void cbHienThi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHienThi.Checked == true)
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 TrangChu = new Form1();
            this.Hide();
            TrangChu.ShowDialog();
            this.Close();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDoiMK DoiMK = new frmDoiMK();
            this.Hide();
            DoiMK.ShowDialog();
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            frmDoiMK DoiMK = new frmDoiMK();
            this.Hide();
            DoiMK.ShowDialog();
        }
    }
}
