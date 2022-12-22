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
    public partial class Form1 : Form
    {
        Module.DataAccess dtbase = new Module.DataAccess();
        public static string userMaNV = "";
        public static string userName = "";
        public static string userCV = "";
        public static string userMatKhau = "";
        public static string userTenTK = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //lấy ra tên của nhân viên vừa đăng nhập
            frmDangNhap frmDN = new frmDangNhap();
            DataTable NhanVien = dtbase.DataReader("Select * from Nhanvien where MaNV = '" + userMaNV + "'");

            userName = NhanVien.Rows[0]["TenNV"].ToString();
            userCV = NhanVien.Rows[0]["Chucvu"].ToString();
            lbTenNV.Text = "Xin chào:" + userName;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            if(userCV == "Quản Lý")
            {
                frmNhanVien NhanVien = new frmNhanVien();
                this.Hide();
                NhanVien.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Bạn không đủ quyền để đăng nhập vào form này!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }
           
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHang KhachHang = new frmKhachHang();
            this.Hide();
            KhachHang.ShowDialog();
            this.Show();
        }

        private void btnHoaDonBan_Click(object sender, EventArgs e)
        {
            frmHoaDB HDB = new frmHoaDB();
            this.Hide();
            HDB.ShowDialog();
            this.Show();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn chắc chắn muốn đăng xuất?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                frmDangNhap frmDN = new frmDangNhap();
                MessageBox.Show("Tạm biệt " + userName + "");
                this.Hide();
                frmDN.ShowDialog();
            }
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            FrmSanPham SanPham = new FrmSanPham();
            this.Hide();
            SanPham.ShowDialog();
            this.Show();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (userCV == "Quản Lý")
            {
                frmThongKe ThongKe = new frmThongKe();
                this.Hide();
                ThongKe.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Bạn không đủ quyền để đăng nhập vào form này!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            frmTaiKhoan TaiKhoan = new frmTaiKhoan();
            this.Hide();
            TaiKhoan.ShowDialog();
            this.Show();
        }

        private void btnHoaDonNhap_Click(object sender, EventArgs e)
        {
            frmHoaDonNhap HDN = new frmHoaDonNhap();
            this.Hide();
            HDN.ShowDialog();
        }
    }
}
