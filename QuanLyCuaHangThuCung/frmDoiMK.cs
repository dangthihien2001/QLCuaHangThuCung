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
    public partial class frmDoiMK : Form
    {
        Module.DataAccess database = new Module.DataAccess();
        public static string TenTaiKhoan = frmTaiKhoan.TenDangNhap;
        public frmDoiMK()
        {
            InitializeComponent();
        }
       
        private void btnBack_Click(object sender, EventArgs e)
        {
            frmTaiKhoan TaiKhoan = new frmTaiKhoan();
            this.Hide();
            TaiKhoan.ShowDialog();
            this.Close();
        }

        private void btn_DoiMatKhau_Click(object sender, EventArgs e)
        {
            if (txt_MatKhauCu.Text == "" || txt_MatKhauMoi.Text == "" || txt_XacNhanMK.Text == "")
            {
                MessageBox.Show("Chưa điền đầy đủ thông tin cần thiết");
                return;
            }
            else
            {
                // Kiêm tra xem mật khẩu cũ có đúng hay không:
                DataTable dt = database.DataReader("select TaiKhoan , MatKhau from DangNhap where TaiKhoan = '" + TenTaiKhoan + "' and MatKhau = '" + txt_MatKhauCu.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    // Mật khẩu cũ đã đúng:
                    if (txt_MatKhauMoi.Text == txt_XacNhanMK.Text)
                    {
                        string sql = "update DangNhap set MatKhau = '" + txt_MatKhauMoi.Text + "' where TaiKhoan = '" + TenTaiKhoan + "'";
                        database.UpdateData(sql);
                        MessageBox.Show("Thay đổi mật khẩu thành công");
                        txt_MatKhauCu.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Hai mật khẩu không đồng nhất");
                        return;
                    }
                }

                else
                {
                    MessageBox.Show("Mật khẩu cũ không chính xác , xin kiểm tra lại");
                    return;
                }
            }
        }

        private void checkBox_HienThiMK_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox_HienThiMK.Checked == true)
            {
                txt_MatKhauCu.UseSystemPasswordChar = false;
                txt_MatKhauMoi.UseSystemPasswordChar = false;
                txt_XacNhanMK.UseSystemPasswordChar = false;
            }

            else
            {
                txt_MatKhauCu.UseSystemPasswordChar = true;
                txt_MatKhauMoi.UseSystemPasswordChar = true;
                txt_XacNhanMK.UseSystemPasswordChar = true;
            }
        }

    }
}
