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
    public partial class frmNhanVien : Form
    {
        Module.DataAccess dtBase = new Module.DataAccess();
        String GioiTinh = "";
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            toolTipBackHome.SetToolTip(btnTrangChu, "Quay về trang chủ");
            //Lấy tên đăng nhập đổ vào label Tên
            lbTenNV.Text = "Xin chào:" + Form1.userName;
            dgvNhanvien.Font = new Font("Times New Roman", 10);
            //tải chức vụ lên combobox
            cbChucvu.Items.Add("Quản Lý");
            cbChucvu.Items.Add("Nhân Viên");
            //tải lên dataGrid
            dgvNhanvien.DataSource = dtBase.DataReader(" select * from Nhanvien");
            dgvNhanvien.Columns[0].HeaderText = "Mã NV";
            dgvNhanvien.Columns[1].HeaderText = "Tên NV";
            dgvNhanvien.Columns[2].HeaderText = "Giới Tính";
            dgvNhanvien.Columns[3].HeaderText = "Địa Chỉ";
            dgvNhanvien.Columns[4].HeaderText = "SĐT";
            dgvNhanvien.Columns[5].HeaderText = "Ngày Sinh";
            dgvNhanvien.Columns[6].HeaderText = "Ngày Vào Làm";
            dgvNhanvien.Columns[7].HeaderText = "Chức Vụ";
            dgvNhanvien.Columns[8].HeaderText = "Lương";
            dgvNhanvien.Columns[9].HeaderText = "CCCD";
            dgvNhanvien.ForeColor = Color.Black;
            ResetValue();
            LoadData();
            //Kiểm tra chức vụ để set chức năng
            if (Form1.userCV != "Quản Lý")
            {
                HienChiTiet(false);
            }
        }
        void LoadData()
        {
            DataTable dtNhanvien = dtBase.DataReader("Select * from Nhanvien");
            dgvNhanvien.DataSource = dtNhanvien;
        }

        void HienChiTiet(bool hien)
        {
            txtMa.Enabled = hien;
            txtHoTen.Enabled = hien;
            txtLuong.Enabled = hien;
            txtSDT.Enabled  = hien;
            txtCCCD.Enabled  = hien;
            txtDiaChi.Enabled  = hien;
            dtpNgayVaoLam.Enabled  = hien;
            dtpNgaySinh.Enabled  = hien;
            cbChucvu.Enabled  = hien;
            txtTim.Enabled  = hien;
            rdoNu.Enabled  = hien;
            btnLuu.Enabled = hien;
            btnThem.Enabled = hien;
            btnSua.Enabled = hien;
            btnXoa.Enabled = hien;
            btnBoQua.Enabled = hien;
            btnThem.Enabled = hien;
        }
        void ResetValue()
        {
            txtMa.Text = "";
            txtHoTen.Text = "";
            txtLuong.Text = "";
            txtSDT.Text = "";
            txtCCCD.Text = "";
            txtDiaChi.Text = "";
            dtpNgayVaoLam.Value = DateTime.Now;
            dtpNgaySinh.Value = DateTime.Now;
            cbChucvu.Text = "";
            txtTim.Text = "";
            rdoNu.Checked = true;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtHoTen.Focus();
        }

        //Nút thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
            Module.SinhMa SinhMa = new Module.SinhMa();
            txtMa.Text = SinhMa.SinhMaTD("NhanVien", "NV0", "MaNV");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //kiểm tra nhập dữ liệu
            if (txtHoTen.Text.Trim() == "" || txtLuong.Text.Trim() == "" || txtDiaChi.Text.Trim() == ""
                || txtCCCD.Text.Trim() == ""||txtSDT.Text.Trim()==""||cbChucvu.Text.Trim()=="")
            {
                MessageBox.Show("Bạn cần nhập đầy đủ hết dữ liệu","Thông báo",MessageBoxButtons.OK);
                ResetValue();
                return;
            }
            int NamSinh = dtpNgaySinh.Value.Year;
            int NamVaoLam = dtpNgayVaoLam.Value.Year;
            if(NamVaoLam - NamSinh < 18)
            {
                MessageBox.Show("Ngày Tháng Năm không hợp lệ!","Thông báo",MessageBoxButtons.OK);
                return;
            }
            if (rdoNam.Checked == true)
                GioiTinh = "Nam";
            else
                GioiTinh = "Nữ";
            if (MessageBox.Show("Bạn có muốn thêm nhân viên "+txtHoTen.Text+" có mã "+txtMa.Text+" này không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                String sqlInsert = "insert into NhanVien values(N'" + txtMa.Text + "',N'" + txtHoTen.Text + "',N'" + GioiTinh + "',N'" + txtDiaChi.Text + "'" +
                ", N'" + txtSDT.Text + "', '" + dtpNgaySinh.Value.ToString("MM/dd/yyyy") + "', '" + dtpNgayVaoLam.Value.ToString("MM/dd/yyyy") + "', N'" + cbChucvu.Text + "','" + txtLuong.Text + "', N'" + txtCCCD.Text + "')";
                dtBase.UpdateData(sqlInsert);
                LoadData();
                ResetValue();
            }
        }

        //key_Press

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        } 
        private void txtCCCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        //

        private void btnXoa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn không được xóa nhân viên", "Thông báo");
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int NamSinh = dtpNgaySinh.Value.Year;
            int NamVaoLam = dtpNgayVaoLam.Value.Year;
            if (NamVaoLam - NamSinh < 18)
            {
                MessageBox.Show("Ngày Tháng Năm không hợp lệ!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (rdoNam.Checked == true)
                GioiTinh = "Nam";
            else
                GioiTinh = "Nữ";
            
            String sqlUpdate = "update NhanVien set TenNV = N'"+txtHoTen.Text+"', GioiTinh = N'"+GioiTinh+ "',DiaChi = N'" + txtDiaChi.Text + "'" +
               ",SDT = N'"+txtSDT.Text + "', NgaySinh = '" + dtpNgaySinh.Value.ToString("MM/dd/yyyy")+"', NgayVaoLam = '" + dtpNgayVaoLam.Value.ToString("MM/dd/yyyy")+"'" +
                ", ChucVu = N'" + cbChucvu.Text + "', Luong = "+ txtLuong.Text +", CCCD = N'"+txtCCCD.Text+"'  where MaNV = N'"+txtMa.Text+"'";
           dtBase.UpdateData(sqlUpdate);
            //MessageBox.Show(sqlUpdate);
            LoadData();
            ResetValue();   

        }

        private void dgvNhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThem.Enabled = false;
                btnBoQua.Enabled = true;
                txtMa.Text = dgvNhanvien.CurrentRow.Cells[0].Value.ToString();
                txtHoTen.Text = dgvNhanvien.CurrentRow.Cells[1].Value.ToString();
                GioiTinh = dgvNhanvien.CurrentRow.Cells[2].Value.ToString();
                if (GioiTinh == "Nam")
                {
                    rdoNam.Checked = true;
                }
                if (GioiTinh == "Nữ") rdoNu.Checked = true;
                txtDiaChi.Text = dgvNhanvien.CurrentRow.Cells[3].Value.ToString();
                txtSDT.Text = dgvNhanvien.CurrentRow.Cells[4].Value.ToString();
            try
            {
                dtpNgaySinh.Value = (DateTime)dgvNhanvien.CurrentRow.Cells[5].Value;
                dtpNgayVaoLam.Value = (DateTime)dgvNhanvien.CurrentRow.Cells[6].Value;
            }
            catch { }
                cbChucvu.Text = "";
                cbChucvu.SelectedText = dgvNhanvien.CurrentRow.Cells[7].Value.ToString();
                txtLuong.Text = dgvNhanvien.CurrentRow.Cells[8].Value.ToString();
                txtCCCD.Text = dgvNhanvien.CurrentRow.Cells[9].Value.ToString();

        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 TrangChu = new Form1();
            TrangChu.Show();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            String TimKiem = txtTim.Text.Trim();
            string sqlSelect = "Select * from NhanVien " +
                                      " where MaNV is not null";
            if (TimKiem != "")
            {
                //Tìm kiếm gần đúng với từ khóa like
                sqlSelect = sqlSelect + " and (MaNV like N'%" + TimKiem + "%' or TenNV like N'%" + TimKiem + "%' " +
                    "or DiaChi like N'%" + TimKiem + "%' or ChucVu like N'%" + TimKiem + "%')";
            }
            dgvNhanvien.DataSource = dtBase.DataReader(sqlSelect);
        }

        //Thanh menustrip
        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSanPham SanPham = new FrmSanPham();
            this.Hide();
            SanPham.ShowDialog();
            this.Close();
        }


        private void hóaĐơnNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoaDonNhap HDN = new frmHoaDonNhap();
            this.Hide();
            HDN.ShowDialog();
            this.Close();
        }

        private void hóaĐơnBánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoaDonNhap HDB = new frmHoaDonNhap();
            this.Hide();
            HDB.ShowDialog();
            this.Close();
        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 TrangChu = new Form1();
            this.Hide();
            TrangChu.ShowDialog();
            this.Close();
        }

    
        private void ThongkeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form1.userCV == "Quản Lý")
            {
                frmThongKe TK = new frmThongKe();
                this.Hide();
                TK.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Bạn không đủ quyền để đăng nhập vào form này!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKhachHang KH = new frmKhachHang();
            this.Hide();
            KH.ShowDialog();
            this.Close();
        }
    }
}
