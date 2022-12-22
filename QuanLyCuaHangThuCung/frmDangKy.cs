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
    public partial class frmDangKy : Form
    {
        Module.DataAccess database = new Module.DataAccess();
        public frmDangKy()
        {
            InitializeComponent();
        }

        private void Form_DangKi_Load(object sender, EventArgs e)
        {
            txt_MaNhanVien.Focus();
            toolTip1.SetToolTip(btnThoat, "Click vào đây để thoát");
            toolTip1.SetToolTip(txt_TaiKhoanDangKi, "Nhập vào tài khoản đăng kí tại đây");
            toolTip1.SetToolTip(txt_MatKhauDangKi, "Nhập mật khẩu tại đây");
            toolTip1.SetToolTip(txt_XacNhanMatKhau, "Nhập lại mật khẩu ở trên");
            toolTip1.SetToolTip(btn_DangKy, "Click vào đây để đăng kí tài khoản mới");
            toolTip1.SetToolTip(CheckBox_HienThiMatKhau, "Tích vào đây để xem mật khẩu");
            toolTip1.SetToolTip(Quay_Lai_Form_Dang_Nhap, "Click vào đây để trở về giao diện đăng nhập");
            toolTip1.SetToolTip(txt_MaNhanVien, "Nhập mã nhân viên của bạn tại đây");
        }

        private void btn_DangKy_Click(object sender, EventArgs e)
        {
            if(txt_MaNhanVien.Text.Trim() == "" || txt_MatKhauDangKi.Text.Trim() == "" || 
                txt_TaiKhoanDangKi.Text.Trim() == "" || txt_XacNhanMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải điền đủ thông tin!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if(txt_MatKhauDangKi.Text != txt_XacNhanMatKhau.Text)
                {
                    MessageBox.Show("Mật khẩu không đồng nhất!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            frmHoaDB HDB = new frmHoaDB();
            DataTable dtNhanVien = database.DataReader("Select * from NhanVien where MaNV = N'" + txt_MaNhanVien.Text + "'");
            if (dtNhanVien.Rows.Count > 0)
            {
                string sqlUpdateUser = "insert into DangNhap(MaNV, TaiKhoan, MatKhau) values ('" + txt_MaNhanVien.Text + "', N'"+txt_TaiKhoanDangKi.Text+"'," +
                    "N'"+txt_XacNhanMatKhau.Text+"')";
                database.UpdateData(sqlUpdateUser);
                MessageBox.Show("Bạn đã đăng ký tài khoản thành công, trở về form đăng nhập để hoàn tất đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Mã nhân viên không chính xác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_MaNhanVien.Focus();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn thoát không?", "Thoát?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void CheckBox_HienThiMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if(CheckBox_HienThiMatKhau.Checked == true)
            {
                txt_MatKhauDangKi.UseSystemPasswordChar = false;
                txt_XacNhanMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txt_MatKhauDangKi.UseSystemPasswordChar = true;
                txt_XacNhanMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void Quay_Lai_Form_Dang_Nhap_Click(object sender, EventArgs e)
        {
            frmDangNhap frm_DangNhap = new frmDangNhap();
            frm_DangNhap.Show();
            this.Hide();
        }
    }
}
