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
    public partial class frmChuong : Form
    {
        Module.DataAccess dtBase = new Module.DataAccess();
        public frmChuong()
        {
            InitializeComponent();
        }

        private void frmChuong_Load(object sender, EventArgs e)
        {
            //Lấy tên đăng nhập đổ vào label Tên
            lbTenNVC.Text = "Xin chào:" + Form1.userName;
            LoadData();
            dgvChuong.Font = new Font("Times New Roman", 10);
            dgvChuong.Columns[0].Width = 180;
            dgvChuong.Columns[1].Width = 200;
            //đặt màu cho hàng đầu 
            dgvChuong.EnableHeadersVisualStyles = false;
            dgvChuong.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkOliveGreen;
            dgvChuong.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvChuong.DefaultCellStyle.SelectionBackColor = Color.NavajoWhite;
            //tải lên dataGridView
            dgvChuong.DataSource = dtBase.DataReader("Select MaChuong,TenChuong from Chuong");
            dgvChuong.Columns[0].HeaderText = "Mã Chuồng";
            dgvChuong.Columns[1].HeaderText = "Tên Chuồng";
            ResetValue();
        }

        Boolean KtTrangThai()
        {
           String sql = "select MaChuong from SanPham where MaChuong = N'"+txtMaChuong.Text+"'";
           DataTable Test =  dtBase.DataReader(sql);
            if(Test.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        void LoadData()
        {
            DataTable dtChuong = dtBase.DataReader("Select MaChuong,TenChuong from Chuong");
            dgvChuong.DataSource = dtChuong;
        }
        void ResetValue()
        {
            txtMaChuong.Text = "";
            txtTenChuong.Text = "";
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            lbTrangThai.Text = "";
            txtTenChuong.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
            Module.SinhMa SinhMa = new Module.SinhMa();
            txtMaChuong.Text = SinhMa.SinhMaTD("Chuong", "C0", "MaChuong");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kiểm tra nhập dữ liệu
            if (txtTenChuong.Text.Trim() == "")
            {
                MessageBox.Show("Bạn cần nhập đầy đủ hết dữ liệu", "Thông báo", MessageBoxButtons.OK);
                ResetValue();
                return;
            }
            if (MessageBox.Show("Bạn có muốn thêm chuồng " + txtTenChuong.Text + " có mã " + txtMaChuong.Text + " này không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                String sqlInsert = "insert into Chuong values(N'"+ txtMaChuong.Text + "',N'"+txtTenChuong.Text + "')";
                dtBase.UpdateData(sqlInsert);
                LoadData();
                ResetValue();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(KtTrangThai())
            MessageBox.Show("Bạn không được xóa chuồng đang có Thú cưng", "Thông báo");
            else
            {
                if (MessageBox.Show("Bạn có muốn xóa mã chuồng  " + txtMaChuong.Text + " không ?", "lựa chọn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    dtBase.UpdateData("delete Chuong where MaChuong = '" + txtMaChuong.Text + "'");
                LoadData();
                ResetValue();
            }    
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            String sqlUpdate = "update Chuong set TenChuong = N'"+txtTenChuong.Text+"' where MaChuong = N'"+txtMaChuong.Text+"'";
            dtBase.UpdateData(sqlUpdate);
            //MessageBox.Show(sqlUpdate);
            LoadData();
            ResetValue();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
        }

        private void dgvChuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = false;
            btnBoQua.Enabled = true;
            txtMaChuong.Text = dgvChuong.CurrentRow.Cells[0].Value.ToString();
            txtTenChuong.Text = dgvChuong.CurrentRow.Cells[1].Value.ToString();
            if (KtTrangThai())
            {
                lbTrangThai.Text = "Có Thú Cưng";
            }
            else
            {
                lbTrangThai.Text = "Đang Trống";
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
