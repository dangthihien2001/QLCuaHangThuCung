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
    public partial class frmLoai : Form
    {
        Module.DataAccess dtBase = new Module.DataAccess();
        public frmLoai()
        {
            InitializeComponent();
        }

        private void frmLoai_Load(object sender, EventArgs e)
        {
            //Lấy tên đăng nhập đổ vào label Tên
            lbTenNV.Text = "Xin chào:" + Form1.userName;
            LoadData();
            dgvLoai.Font = new Font("Times New Roman", 10);
            dgvLoai.Columns[0].Width = 150;
            dgvLoai.Columns[1].Width = 190;
            //đặt màu cho hàng đầu 
            dgvLoai.EnableHeadersVisualStyles = false;
            dgvLoai.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkOliveGreen;
            dgvLoai.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLoai.DefaultCellStyle.SelectionBackColor = Color.NavajoWhite;
            //tải lên dataGridView
            dgvLoai.DataSource = dtBase.DataReader("Select * from Loai");
            dgvLoai.Columns[0].HeaderText = "Mã Loại";
            dgvLoai.Columns[1].HeaderText = "Tên Loại";
            ResetValue();
        }
        void LoadData()
        {
            DataTable dtLoai = dtBase.DataReader("Select * from Loai");
            dgvLoai.DataSource = dtLoai;
        }
        void ResetValue()
        {
            txtMaLoai.Text = "";
            txtTenLoai.Text = "";
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
            Module.SinhMa SinhMa = new Module.SinhMa();
            txtMaLoai.Text = SinhMa.SinhMaTD("Loai", "L0", "MaLoai");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kiểm tra nhập dữ liệu
            if (txtTenLoai.Text.Trim() == "")
            {
                MessageBox.Show("Bạn cần nhập đầy đủ hết dữ liệu", "Thông báo", MessageBoxButtons.OK);
                ResetValue();
                return;
            }
            if (MessageBox.Show("Bạn có muốn thêm loại "+txtTenLoai.Text + " có mã " + txtMaLoai.Text + " này không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                String sqlInsert = "insert into Loai values(N'" + txtMaLoai.Text + "',N'" + txtTenLoai.Text + "')";
                dtBase.UpdateData(sqlInsert);
                LoadData();
                ResetValue();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            String sqlUpdate = "update Loai set TenLoai = N'" + txtTenLoai.Text + "' where MaLoai = N'" + txtMaLoai.Text + "'";
            dtBase.UpdateData(sqlUpdate);
            //MessageBox.Show(sqlUpdate);
            LoadData();
            ResetValue();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
        }
        Boolean KtTrangThai()
        {
            String sql = "select MaLoai from SanPham where MaLoai = N'" + txtMaLoai.Text + "'";
            DataTable Test = dtBase.DataReader(sql);
            if (Test.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (KtTrangThai())
                MessageBox.Show("Bạn không được xóa Loại đang có Thú cưng", "Thông báo");
            else
            {
                if (MessageBox.Show("Bạn có muốn xóa mã loại  " + txtMaLoai.Text + " không ?", "lựa chọn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    dtBase.UpdateData("delete Loai where MaLoai = '" + txtMaLoai.Text + "'");
                LoadData();
                ResetValue();
            }

        }

        private void dgvLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = false;
            btnBoQua.Enabled = true;
            txtMaLoai.Text = dgvLoai.CurrentRow.Cells[0].Value.ToString();
            txtTenLoai.Text = dgvLoai.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
