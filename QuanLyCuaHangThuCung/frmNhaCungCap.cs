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
    public partial class frmNhaCungCap : Form
    {
        Module.DataAccess dtbase = new Module.DataAccess();
        Module.SinhMa Sm = new Module.SinhMa();
        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            toolTipBackHome.SetToolTip(btnBackHome, "Quay lại nhà cung cấp");
            //Lấy tên đăng nhập đổ vào label Tên
            lbTenNV.Text = "Xin chào:" + Form1.userName;
            LoadData();
            ResetOption();
            //đặt màu cho hàng đầu
            dgvNCC.EnableHeadersVisualStyles = false;
            dgvNCC.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkOliveGreen;
            //đặt lại tên cột
            dgvNCC.Columns[0].HeaderText = "Mã NCC";
            dgvNCC.Columns[1].HeaderText = "Tên NCC";
            dgvNCC.Columns[2].HeaderText = "Địa chỉ";
            dgvNCC.Columns[3].HeaderText = "Liên hệ";

            ResetValue();

        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //bắt lỗi khi người dùng kích linh tinh trên dgvMatHang
            try
            {
                txtMaNCC.Text = dgvNCC.CurrentRow.Cells[0].Value.ToString();
                txtTenNCC.Text = dgvNCC.CurrentRow.Cells[1].Value.ToString();
                txtDiaChi.Text = dgvNCC.CurrentRow.Cells[2].Value.ToString();
                txtLienHe.Text = dgvNCC.CurrentRow.Cells[3].Value.ToString();
                //Ẩn nút Thêm, hiện nút Sửa và Xóa
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoQua.Enabled = true;
                btnThem.Enabled = false;
            }
            catch
            {
            }
        }

        void LoadData()
        {
            //Load dữ liệu lên dgcKhachHang
            DataTable dtNCC = dtbase.DataReader("Select * from NhaCungCap where MaNCC = N'" + txtMaNCC.Text + "'");
            dgvNCC.DataSource = dtNCC;

        }

        void ResetValue()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtLienHe.Text = "";
           
        }

        void ResetOption()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnHienTatCa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValue();
            LoadData();
            txtMaNCC.Text = Sm.SinhMaTD("NhaCungCap", "C0", "MaNCC");
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kiểm tra thông tin nhập
            if (txtTenNCC.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tên nhà cung cấp");
                return;
            }
            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ nhà cung cấp");
                return;
            }
            if (txtLienHe.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập cách thức liên hệ");
                return;
            }
            
            //lệnh SQL insert
            string sqlInsert = "Insert into NhaCungCap values('" + txtMaNCC.Text + "', N'" + txtTenNCC.Text + "', N'" + txtDiaChi.Text + "'," +
                "N'" + txtLienHe.Text + "')";

            //insert dữ liệu vào bảng
            dtbase.UpdateData(sqlInsert);
            LoadData();
            MessageBox.Show("Bạn đã thêm nhà cung cấp " + txtMaNCC.Text + " có mã " + txtTenNCC.Text + " thành công");
            ResetValue();
            ResetOption();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sqlUpdate = "update NhaCungCap set TenNCC = N'" + txtTenNCC.Text + "', diachi = N'" + txtDiaChi.Text + "'" +
                ", Lienhe = N'" + txtLienHe.Text + "' where MaNCC = N'" + txtMaNCC.Text + "'";
            dtbase.UpdateData(sqlUpdate);
            LoadData();
            MessageBox.Show("Bạn đã sửa nhà cung cấp " + txtMaNCC.Text + " có mã " + txtTenNCC.Text + " thành công");
            ResetOption();
            ResetValue();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn không nên xóa khách hàng vì có liên qua đến nhiều dữ liệu khác?", "Xóa nhà cung cấp", MessageBoxButtons.OK);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            ResetOption();
            LoadData();
        }

        private void txtMaNCC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTenNCC.Focus();
            }
        }

        private void txtTenNCC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLienHe.Focus();
            }
        }

        private void txtLienHe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiaChi.Focus();
            }
        }

        private void btnHienTatCa_Click(object sender, EventArgs e)
        {
            DataTable dtNCC = dtbase.DataReader("Select * from NhaCungCap");
            dgvNCC.DataSource = dtNCC;
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sqlSelect = "Select * from NhaCungCap " +
                          " where MaNCC is not null";
            if (txtTimKiem.Text.Trim() != "")
            {
                //Tìm kiếm gần đúng với từ khóa like
                sqlSelect = sqlSelect + " and (MaNCC like N'%" + txtTimKiem.Text + "%' or TenNCC like N'%" + txtTimKiem.Text + "%' " +
                    "or DiaChi like N'%" + txtTimKiem.Text + "%' or Lienhe like N'%" + txtTimKiem.Text + "%')";

            }
            dgvNCC.DataSource = dtbase.DataReader(sqlSelect);
        }

        private void btnBackHome_Click(object sender, EventArgs e)
        {
            Form1 TrangChu = new Form1();
            this.Hide();
            TrangChu.ShowDialog();
        }

        private void btnBackSP_Click(object sender, EventArgs e)
        {
            //    frmSanPham SanPham = new ForfrmSanPham();
            //    this.Hide();
            //    SanPham.Show();
        }

        //đổi ảnh khi trỏ chuột di đến button
        private void btnThem_MouseMove(object sender, MouseEventArgs e)
        {
            ((Button)sender).BackgroundImage = Properties.Resources._133;
        }

        //đổi background khi ấn chuột xuống
        private void btnThem_MouseDown(object sender, MouseEventArgs e)
        {
            ((Button)sender).BackgroundImage = Properties.Resources.xanh;
        }

        //khi trỏ chuột ra khỏi vùng button thì trả lại ảnh cũ
        private void btnThem_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = Properties.Resources.btuufon112;
        }

    }
}
