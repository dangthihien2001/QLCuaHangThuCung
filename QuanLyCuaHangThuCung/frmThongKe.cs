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
    public partial class frmThongKe : Form
    {
        Module.DataAccess data = new Module.DataAccess();
        public frmThongKe()
        {
            InitializeComponent();
        }

        private void DanhMucSP_CheckedChanged(object sender, EventArgs e)
        {
            // Thống kê tất cả các sản phẩm của cửa hàng
            dtgv_ThongTinChiTiet.DataSource = data.DataReader("select * from SanPham");
            dtgv_ThongTinChiTiet.Columns[0].HeaderText = "Mã sản phẩm";
            dtgv_ThongTinChiTiet.Columns[1].HeaderText = "Mã loại";
            dtgv_ThongTinChiTiet.Columns[2].HeaderText = "Mã chuồng";
            dtgv_ThongTinChiTiet.Columns[3].HeaderText = "Tên sản phẩm";
            dtgv_ThongTinChiTiet.Columns[4].HeaderText = "Cân nặng";
            dtgv_ThongTinChiTiet.Columns[5].HeaderText = "Màu lông";
            dtgv_ThongTinChiTiet.Columns[6].HeaderText = "Màu mắt";
            dtgv_ThongTinChiTiet.Columns[7].HeaderText = "Ngày sinh";
            dtgv_ThongTinChiTiet.Columns[8].HeaderText = "Ảnh";
            dtgv_ThongTinChiTiet.Columns[9].HeaderText = "Giá nhập";
            dtgv_ThongTinChiTiet.Columns[10].HeaderText = "Giá bán";
            dtgv_ThongTinChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        private void NhanVienCuaHang_CheckedChanged(object sender, EventArgs e)
        {
            // Thống kê tất cả các nhân viên của cửa hàng
            dtgv_ThongTinChiTiet.DataSource = data.DataReader("select * from NhanVien");
            dtgv_ThongTinChiTiet.Columns[0].HeaderText = "Mã nhân viên";
            dtgv_ThongTinChiTiet.Columns[1].HeaderText = "Tên nhân viên";
            dtgv_ThongTinChiTiet.Columns[2].HeaderText = "Giới tính";
            dtgv_ThongTinChiTiet.Columns[3].HeaderText = "Địa chỉ";
            dtgv_ThongTinChiTiet.Columns[4].HeaderText = "Số điện thoại";
            dtgv_ThongTinChiTiet.Columns[5].HeaderText = "Ngày sinh";
            dtgv_ThongTinChiTiet.Columns[6].HeaderText = "Ngày vào làm";
            dtgv_ThongTinChiTiet.Columns[7].HeaderText = "Chức vụ";
            dtgv_ThongTinChiTiet.Columns[8].HeaderText = "Lương";
            dtgv_ThongTinChiTiet.Columns[9].HeaderText = "Căn cước công dân";
            dtgv_ThongTinChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        private void KhachHang_CheckedChanged(object sender, EventArgs e)
        {
            // Thống kê khách mua hàng
            dtgv_ThongTinChiTiet.DataSource = data.DataReader("select * from KhachHang");
            dtgv_ThongTinChiTiet.Columns[0].HeaderText = "Mã khách hàng";
            dtgv_ThongTinChiTiet.Columns[1].HeaderText = "Tên khách hàng";
            dtgv_ThongTinChiTiet.Columns[2].HeaderText = "Địa chỉ";
            dtgv_ThongTinChiTiet.Columns[3].HeaderText = "Số điện thoại";
            dtgv_ThongTinChiTiet.Columns[4].HeaderText = "Giới tính";
            dtgv_ThongTinChiTiet.Columns[5].HeaderText = "Ngày sinh";
            dtgv_ThongTinChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void HoaDonBanHang_CheckedChanged(object sender, EventArgs e)
        {
            // Thống  kê hóa đơn bán hàng
            dtgv_ThongTinChiTiet.DataSource = data.DataReader("select * from HoaDonBan");
            dtgv_ThongTinChiTiet.Columns[0].HeaderText = "Mã hóa đơn bán";
            dtgv_ThongTinChiTiet.Columns[1].HeaderText = "Mã nhân viên";
            dtgv_ThongTinChiTiet.Columns[2].HeaderText = "Ngày bán";
            dtgv_ThongTinChiTiet.Columns[3].HeaderText = "Mã khách hàng";
            dtgv_ThongTinChiTiet.Columns[4].HeaderText = "Tổng tiền bán";
            dtgv_ThongTinChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void HoaDonNhapHang_CheckedChanged(object sender, EventArgs e)
        {
            // Thống kê hóa đơn nhập hàng
            dtgv_ThongTinChiTiet.DataSource = data.DataReader("select * from HoaDonNhap");
            dtgv_ThongTinChiTiet.Columns[0].HeaderText = "Mã hóa đơn nhập";
            dtgv_ThongTinChiTiet.Columns[1].HeaderText = "Mã nhân viên";
            dtgv_ThongTinChiTiet.Columns[2].HeaderText = "Mã nhà cung cấp";
            dtgv_ThongTinChiTiet.Columns[3].HeaderText = "Ngày nhập";
            dtgv_ThongTinChiTiet.Columns[4].HeaderText = "Tổng tiền nhập";
            dtgv_ThongTinChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void MatHangMuaNhieuNhat_CheckedChanged(object sender, EventArgs e)
        {
            // Mặt hàng được mua nhiều nhất
            string sql = "select TOP 3 MaSP , SUM(CTHoaDonBan.SoLuongBan) as N'Số lượng bán ra' from CTHoaDonBan  group by MaSP  order by SUM(CTHoaDonBan.SoLuongBan) desc";
            dtgv_ThongTinChiTiet.DataSource = data.DataReader(sql);
            dtgv_ThongTinChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void HoaDonBanGiaCaoNhat_CheckedChanged(object sender, EventArgs e)
        {
            string sql_Max = "select TOP 3 * from HoaDonBan order by TongTienBan DESC";
            dtgv_ThongTinChiTiet.DataSource = data.DataReader(sql_Max);
            dtgv_ThongTinChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void HoaDonNhapGiaCaoNhat_CheckedChanged(object sender, EventArgs e)
        {
            string sql_Max = "select TOP 3 * from HoaDonNhap order by TongTienNhap DESC";
            dtgv_ThongTinChiTiet.DataSource = data.DataReader(sql_Max);
            dtgv_ThongTinChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Top3SanPhamGiaCao_CheckedChanged(object sender, EventArgs e)
        {
            // Top 3 sản phẩm giá cao nhất
            dtgv_ThongTinChiTiet.DataSource = data.DataReader("select TOP 3 * from SanPham Order by GiaBan DESC");
            dtgv_ThongTinChiTiet.Columns[0].HeaderText = "Mã sản phẩm";
            dtgv_ThongTinChiTiet.Columns[1].HeaderText = "Mã loại";
            dtgv_ThongTinChiTiet.Columns[2].HeaderText = "Mã chuồng";
            dtgv_ThongTinChiTiet.Columns[3].HeaderText = "Tên sản phẩm";
            dtgv_ThongTinChiTiet.Columns[4].HeaderText = "Cân nặng";
            dtgv_ThongTinChiTiet.Columns[5].HeaderText = "Màu lông";
            dtgv_ThongTinChiTiet.Columns[6].HeaderText = "Màu mắt";
            dtgv_ThongTinChiTiet.Columns[7].HeaderText = "Ngày sinh";
            dtgv_ThongTinChiTiet.Columns[8].HeaderText = "Ảnh";
            dtgv_ThongTinChiTiet.Columns[9].HeaderText = "Giá nhập";
            dtgv_ThongTinChiTiet.Columns[10].HeaderText = "Giá bán";
            dtgv_ThongTinChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        private void dtgv_ThongTinChiTiet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DanhMucSP.Checked == true)
            {
                try
                {
                    FrmSanPham.imageName = dtgv_ThongTinChiTiet.CurrentRow.Cells[8].Value.ToString(); // tên form sản phẩm
                    picturebox.Image = Image.FromFile(Application.StartupPath.ToString() + "\\Images\\Product\\" + FrmSanPham.imageName); // Tên form sản phẩm
                }
                catch
                {

                }
            }
        }
        //Thanh menustrip
        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSanPham SanPham = new FrmSanPham();
            this.Hide();
            SanPham.ShowDialog();
        }


        private void hóaĐơnNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoaDonNhap HDN = new frmHoaDonNhap();
            this.Hide();
            HDN.ShowDialog();
        }

        private void hóaĐơnBánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoaDonNhap HDB = new frmHoaDonNhap();
            this.Hide();
            HDB.ShowDialog();
        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 TrangChu = new Form1();
            this.Hide();
            TrangChu.ShowDialog();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKhachHang KH = new frmKhachHang();
            this.Hide();
            KH.ShowDialog();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form1.userCV == "Quản Lý")
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
    }
}
