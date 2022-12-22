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
    public partial class frmKhachHang : Form
    {
        Module.DataAccess dtbase = new Module.DataAccess();
        Module.SinhMa Sm = new Module.SinhMa();
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            toolTipTimKiem.SetToolTip(btnTimKiem, "Tìm kiếm khách hàng");

            //Lấy tên đăng nhập đổ vào label Tên
            lbTenNV.Text = "Xin chào:" + Form1.userName;
            LoadData();
            ResetOption();
            //đặt màu cho hàng đầu
            dgvKhachHang.EnableHeadersVisualStyles = false;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkOliveGreen;
            //đặt lại tên cột
            dgvKhachHang.Columns[0].HeaderText = "Mã khách hàng";
            dgvKhachHang.Columns[1].HeaderText = "Tên khách hàng";
            dgvKhachHang.Columns[2].HeaderText = "Địa chỉ";
            dgvKhachHang.Columns[3].HeaderText = "Số điện thoại";
            dgvKhachHang.Columns[4].HeaderText = "Giới tính";
            dgvKhachHang.Columns[5].HeaderText = "Ngày sinh";
            HienChiTiet(false);
            ResetValue();
        }

        void LoadData()
        {
            //Load dữ liệu từ sql lên dgcKhachHang
            DataTable dtKhachHang = dtbase.DataReader("Select * from KhachHang");
            dgvKhachHang.DataSource = dtKhachHang;
            
        }

        void ResetValue()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            rdoNam.Checked = false;
            rdoNu.Checked = false;
            dtpNgaySinh.Value = DateTime.Now;
            txtTimKiem.Text = "";
        }

        void ResetOption()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = true;
        }

        void HienChiTiet(bool hien)
        {
            txtTenKH.Enabled = hien;
            txtDiaChi.Enabled = hien;
            txtSDT.Enabled = hien;
            rdoNam.Enabled = hien;
            rdoNu.Enabled = hien;
            dtpNgaySinh.Enabled = hien;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            HienChiTiet(true);
            ResetValue();
            LoadData();
            txtMaKH.Text = Sm.SinhMaTD("KhachHang", "KH0", "MaKH");
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
        }

        string GioiTinh()
        {
            string gioitinh;
            if (rdoNam.Checked == true)
            {
                gioitinh = "nam";
            }
            else gioitinh = "nữ";
            return gioitinh;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kiểm tra thông tin nhập
            if(txtTenKH.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tên khách hàng");
                return;
            }
            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ khách hàng");
                return;
            }
            if (txtSDT.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ khách hàng");
                return;
            }
            if (dtpNgaySinh.Text.Trim() == "")
            {
                dtpNgaySinh.Text = "";
            }
            if (rdoNam.Checked == false && rdoNu.Checked == false)
            {
                MessageBox.Show("Bạn chưa chọn giới tính của khách hàng");
                return;
            }

            //lệnh SQL insert
            string gioitinh = GioiTinh().ToString();
            string sqlInsert = "Insert into KhachHang values('" + txtMaKH.Text + "', N'" + txtTenKH.Text + "', N'" + txtDiaChi.Text + "'," +
                "N'" + txtSDT.Text + "', N'" + gioitinh + "', N'" + dtpNgaySinh.Value.ToString("MM/dd/yyyy") + "')";

            //insert dữ liệu vào bảng
            dtbase.UpdateData(sqlInsert);
            MessageBox.Show("Bạn đã thêm khách hàng " + txtMaKH.Text + " có mã " + txtMaKH.Text + " thành công");
            LoadData();
            ResetValue();
            ResetOption();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string gioitinh = GioiTinh().ToString();
            string sqlUpdate = "update KhachHang set TenKH = N'" + txtTenKH.Text + "', DiaChi = N'" + txtDiaChi.Text + "', Sdt = N'" + txtSDT.Text + "'," +
                "GioiTinh = N'" + gioitinh + "', NgaySinh = '" + dtpNgaySinh.Value.ToString("MM/dd/yyyy") + "' where MaKH = N'" + txtMaKH.Text + "'";
            dtbase.UpdateData(sqlUpdate);
            MessageBox.Show("Bạn đã sửa khách hàng " + txtTenKH.Text + " có mã " + txtMaKH.Text + " thành công", "Thông báo");
            //hiển thị dữ liệu lại lên dgv
            LoadData();
            //Xóa trắng các điều khiển
            ResetValue();
            ResetOption();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn không thể xóa khách hàng ?", "Xóa khách hàng", MessageBoxButtons.OK);
            
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //bắt lỗi khi người dùng kích linh tinh trên dgvMatHang
            try
            {
                txtMaKH.Text = dgvKhachHang.CurrentRow.Cells[0].Value.ToString();
                txtTenKH.Text = dgvKhachHang.CurrentRow.Cells[1].Value.ToString();
                txtDiaChi.Text = dgvKhachHang.CurrentRow.Cells[2].Value.ToString();
                txtSDT.Text = dgvKhachHang.CurrentRow.Cells[3].Value.ToString();
                string gioitinh = dgvKhachHang.CurrentRow.Cells[4].Value.ToString();
                if (gioitinh == "nam")
                {
                    rdoNam.Checked = true;
                }
                if (gioitinh == "nữ") rdoNu.Checked = true;
                dtpNgaySinh.Value = (DateTime)dgvKhachHang.CurrentRow.Cells[5].Value;

                //Ẩn nút Thêm, hiện nút Sửa và Xóa
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoQua.Enabled = true;
                btnThem.Enabled = false;
                //hiện lên các thông tin để chỉnh sửa
                HienChiTiet(true);
            }
            catch
            {
            }
        }


        private void btnBoQua_Click(object sender, EventArgs e)
        {
            HienChiTiet(false);
            ResetValue();
            ResetOption();
            LoadData();
        }

        private void txtTenKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiaChi.Focus();
            }
        }

        private void txtDiaChi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSDT.Focus();
            }
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            Form1 TrangChu = new Form1();
            this.Hide();
            TrangChu.ShowDialog();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sqlSelect = "Select * from KhachHang " +
                          " where MaKH is not null";
            if (txtTimKiem.Text.Trim() != "")
            {
                //Tìm kiếm gần đúng với từ khóa like
                sqlSelect = sqlSelect + " and (MaKH like N'%" + txtTimKiem.Text + "%' or TenKH like N'%" + txtTimKiem.Text + "%' " +
                    "or DiaChi like N'%" + txtTimKiem.Text + "%')";

            }
            dgvKhachHang.DataSource = dtbase.DataReader(sqlSelect);
        }
        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!txtTimKiem.AcceptsReturn)
                {
                    btnTimKiem.PerformClick();
                }
            }
        }

        private void btnThem_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = Properties.Resources.butbut;
        }

        private void btnThem_MouseDown(object sender, MouseEventArgs e)
        {
            ((Button)sender).BackgroundImage = Properties.Resources.xanh;
        }

        private void btnThem_MouseMove(object sender, MouseEventArgs e)
        {
            ((Button)sender).BackgroundImage = Properties.Resources._133;
        }


        //thanh menustrip
        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSanPham SanPham = new FrmSanPham();
            this.Hide();
            SanPham.ShowDialog();
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

        private void ThongKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form1.userCV == "Quản Lý")
            {
                frmThongKe TK = new frmThongKe();
                this.Hide();
                TK.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không đủ quyền để đăng nhập vào form này!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }
        }
    }
}
