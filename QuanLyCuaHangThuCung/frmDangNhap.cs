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
    public partial class frmDangNhap : Form
    {
        Module.DataAccess database = new Module.DataAccess();
       

        public static Boolean QuyenTruyCap; // Để khi đăng nhập xác nhận là quyền truy cập nào
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn thoát không?", "Thoát?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit(); 
            }
        }
        private void Form_DangNhap_Load(object sender, EventArgs e)
        {
            txt_TaiKhoan.Focus();
            toolTip1.SetToolTip(btnThoat, "Thoát");
            toolTip1.SetToolTip(txt_TaiKhoan, "Nhập vào tên tài khoản của bạn");
            toolTip1.SetToolTip(txt_MatKhau, "Nhập vào mật khẩu");
            toolTip1.SetToolTip(Xem_Mat_Khau, "Hiển thị mật khẩu");
            toolTip1.SetToolTip(DangKi, "Đăng kí tài khoản tại đây");
            toolTip1.SetToolTip(Youtube, "Tới kênh Youtube của cửa hàng");
            toolTip1.SetToolTip(Facebook, "Tới trang Fanpage của cửa hàng");
            toolTip1.SetToolTip(Website, "Tới website của cửa hàng");
        }

        private void Xem_Mat_Khau_MouseHover(object sender, EventArgs e)
        {
            // Di chuột tới là hiện mật khẩu
            txt_MatKhau.UseSystemPasswordChar = false;
        }

        private void Xem_Mat_Khau_MouseLeave(object sender, EventArgs e)
        {
            txt_MatKhau.UseSystemPasswordChar = true;
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            Form1 TrangChu = new Form1();
            DataTable dtDangNhap = database.DataReader("Select * from DangNhap where TaiKhoan = N'" + txt_TaiKhoan.Text + "' " +
               "and MatKhau = N'" + txt_MatKhau.Text + "'");
            if (dtDangNhap.Rows.Count > 0)
            {
                MessageBox.Show("Chúc mừng " + txt_TaiKhoan.Text + " đã đăng nhập thành công");
                Form1.userName = txt_MatKhau.Text;
                Form1.userMaNV = (dtDangNhap.Rows[0]["MaNV"]).ToString(); //Truyền dữ liệu
                this.Hide();
                TrangChu.ShowDialog();
                this.Close();

            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng !!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_TaiKhoan.Focus();
            }
            
        }

        private void DangKi_Click(object sender, EventArgs e)
        {
            frmDangKy DangKy = new frmDangKy();
            this.Hide();
            DangKy.ShowDialog();
        }

        private void Youtube_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/user/meotinxinhtuoi");
        }

        private void Facebook_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/matpetfamily");
        }

        private void Website_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://matpetfamily.com/");
        }

        private void DangKi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
