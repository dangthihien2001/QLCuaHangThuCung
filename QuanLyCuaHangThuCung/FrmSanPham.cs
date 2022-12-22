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
    public partial class FrmSanPham : Form
    {
        Module.DataAccess dtBase = new Module.DataAccess();
        Module.SinhMa ft = new Module.SinhMa();
       public static string imageName ;
        float dongianhap;
        float dongiaban;
        String sql = "select Masp,TenSp,TenLoai,TenChuong,CanNang,MauLong,MauMat,NgaySinh,Anh,GiaNhap,GiaBan,SoLuong " +
            "from SanPham left join Loai on SanPham.MaLoai = Loai.MaLoai left join Chuong on SanPham.MaChuong = Chuong.MaChuong";
        public FrmSanPham()
        {
            InitializeComponent();
        }

        private void FrmSanPham_Load(object sender, EventArgs e)
        {
            //Lấy tên đăng nhập đổ vào label Tên
            lbTenNVSP.Text = "Xin chào:" + Form1.userName;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnAnh.Enabled = false;
            dgvSanPham.Font = new Font("Times New Roman", 10);
            dgvSanPham.BackgroundColor = Color.Pink;

            //đưa dữ liệu ra combobox 
            DataTable dtLoai = dtBase.DataReader("select * from Loai");
            DataTable dtChuong = dtBase.DataReader("select * from Chuong");
            ft.FillCombo(cbTenLoai,dtLoai, "TenLoai", "MaLoai");
            ft.FillCombo(cbTenChuong, dtChuong, "TenChuong", "MaChuong");
            cbTenChuong.Text = "";
            cbTenLoai.Text = "";
            //datagridview
            dgvSanPham.DataSource = dtBase.DataReader(sql);
            dgvSanPham.Columns[0].HeaderText = "Mã Sản Phẩm";
            dgvSanPham.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvSanPham.Columns[2].HeaderText = "Tên Loại";
            dgvSanPham.Columns[3].HeaderText = "Tên chuồng";
            dgvSanPham.Columns[4].HeaderText = "Cân Nặng";
            dgvSanPham.Columns[5].HeaderText = "Màu Lông";
            dgvSanPham.Columns[6].HeaderText = "Màu Mắt";
            dgvSanPham.Columns[7].HeaderText = "Ngày Sinh";
            dgvSanPham.Columns[8].HeaderText = "Ảnh";
            dgvSanPham.Columns[9].HeaderText = "Giá Nhập";
            dgvSanPham.Columns[10].HeaderText = "Giá Bán";
            dgvSanPham.Columns[11].HeaderText = "Số Lượng";
            LoadData();
            //ResetValue();
            An();
        }
        void LoadData()
        { 
            dgvSanPham.DataSource = dtBase.DataReader(sql);
        }
        void ResetValue()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtMauMat.Text = "";
            txtMauLong.Text = "";
            txtGiaNhap.Text = "";
            txtGiaBan.Text = "";
            txtCanNang.Text = "";
            txtSoLuong.Text = "";
            ptbAnh.Image = null;
            dtpNgaySinh.Value = DateTime.Now;
            ptbAnh.Image = null;
            cbTenChuong.Text="";
            cbTenLoai.Text="";
            txtTim.Text = "";
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnAnh.Enabled = false;
        }

        void Hien()
        {
            txtTenSP.Enabled = true;
            txtMauLong.Enabled = true;
            txtMauMat.Enabled = true;
            txtGiaBan.Enabled = true;
            txtGiaNhap.Enabled = true;
            txtCanNang.Enabled = true;
            cbTenChuong.Enabled = true;
            cbTenLoai.Enabled = true;
            btnAnh.Enabled = true;
            dtpNgaySinh.Enabled = true;
            txtSoLuong.Enabled = true;
        }
        void An()
        {
            txtTenSP.Enabled = false;
            txtMauLong.Enabled = false;
            txtMauMat.Enabled = false;
            txtGiaBan.Enabled = false;
            txtGiaNhap.Enabled = false;
            txtCanNang.Enabled = false;
            cbTenChuong.Enabled = false;
            cbTenLoai.Enabled = false;
            btnAnh.Enabled = false;
            dtpNgaySinh.Enabled = false;
            txtSoLuong.Enabled = false;
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            string[] pathAnh;
            OpenFileDialog dlgOpen = new OpenFileDialog(); //tạo một hộp thoại chứa file ảnh
            dlgOpen.Filter = "JPEG Images|*.jpg|All Fies|*.*"; // chỉ định những file mà hộp thoại sẽ hiện thị 
            //xác định thư mục mặc định cho hộp thoại khi được gọi 
            dlgOpen.InitialDirectory = Application.StartupPath.ToString() + "\\Images"; 
            if (dlgOpen.ShowDialog() == DialogResult.OK) 
            {
                ptbAnh.Image = Image.FromFile(dlgOpen.FileName);
                pathAnh = dlgOpen.FileName.Split('\\');  //cắt lấy đường dẫn ảnh
                imageName = pathAnh[pathAnh.Length - 1];
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Hien();
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
            btnAnh.Enabled = true;
            Module.SinhMa SinhMa = new Module.SinhMa();
            txtMaSP.Text = SinhMa.SinhMaTD("SanPham", "SP0", "MaSP");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            btnAnh.Image = null;
            //kiểm tra nhập dữ liệu
            if (txtTenSP.Text.Trim() == "" || txtGiaBan.Text.Trim()==""||txtGiaNhap.Text.Trim()==""||txtSoLuong.Text.Trim()==""||txtCanNang.Text.Trim()==""||cbTenChuong.Text.Trim()==""||cbTenLoai.Text.Trim()=="")
            {
                MessageBox.Show("Bạn cần nhập đầy đủ hết dữ liệu cần thiết", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            dongianhap = float.Parse(txtGiaNhap.Text);
            dongiaban = float.Parse(txtGiaBan.Text);
            if (dongianhap > dongiaban)
            {
                MessageBox.Show("Giá bán đang nhỏ hơn giá nhập rồi kìa. ", "Thông báo", MessageBoxButtons.OK);
                    txtGiaNhap.Text = "";
                    txtGiaBan.Text = "";
                    return;
            }
            if (MessageBox.Show("Bạn có muốn thêm Sản Phẩm " + txtTenSP.Text + " có mã " + txtMaSP.Text + " này không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                //(cbTenLoai.SelectedIndex!=-1?",MaLoai = '"+cbTenLoai.SelectedValue.ToString().Trim()+"'": "")
                string MLoai;
                if (cbTenLoai.SelectedIndex != -1 || cbTenLoai.Text.Trim() != "")
                {
                    MLoai = cbTenLoai.SelectedValue.ToString().Trim();
                }
                else
                {
                    MLoai = "";
                }
                string MChuong;
                if (cbTenChuong.SelectedIndex != -1 || cbTenChuong.Text.Trim() != "")
                {
                    MChuong = cbTenChuong.SelectedValue.ToString().Trim();
                }
                else
                {
                    MChuong = "";
                }
                try
                {
                    String sqlInsert = "insert into SanPham values(N'" + txtMaSP.Text + "',N'" + txtTenSP.Text + "', N'" + MLoai + "','" + MChuong + "'" +
                    ", " + float.Parse(txtCanNang.Text) + ", N'" + txtMauMat.Text + "',N'" + txtMauLong.Text + "','" + dtpNgaySinh.Value.ToString("MM/dd/yyyy") + "', N'" + imageName + "',N'" + txtGiaNhap.Text + "',N'" + txtGiaBan.Text + "'," + int.Parse(txtSoLuong.Text) + ")";
                    dtBase.UpdateData(sqlInsert);
                    ResetValue();
                    An();
                    LoadData();
                }
                catch { }
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            An();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn không được xóa sản phẩm", "Thông báo");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            dongianhap = float.Parse(txtGiaNhap.Text);
            dongiaban = float.Parse(txtGiaBan.Text);
            //kiểm tra nhập dữ liệu
            if (txtTenSP.Text.Trim() == "" || txtGiaBan.Text.Trim() == "" || txtGiaNhap.Text.Trim() == ""||txtSoLuong.Text.Trim()=="")
            {
                MessageBox.Show("Bạn không được để trống những thông tin quan trọng", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (dongianhap > dongiaban)
            {
                MessageBox.Show("Giá bán đang nhỏ hơn giá nhập rồi kìa. ", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            String sqlUpdate = "update SanPham set TenSP = N'" + txtTenSP.Text + "'  " + (cbTenLoai.SelectedIndex != -1 ? ",MaLoai = '" + cbTenLoai.SelectedValue.ToString().Trim() + "'" : "") + "  " + (cbTenChuong.SelectedIndex != -1 ? ",MaChuong = '" + cbTenChuong.SelectedValue.ToString().Trim() + "'" : "") + " " +
                ",CanNang = " + float.Parse(txtCanNang.Text) + ", MauLong = N'" + txtMauLong.Text + "', MauMat = N'" + txtMauMat.Text + "', NgaySinh = '" + dtpNgaySinh.Value.ToString("MM/dd/yyyy") + "',Anh = N'" + imageName + "'" +
                ", GiaNhap = N'" + txtGiaNhap.Text + "',GiaBan = N'" + txtGiaBan.Text + "', SoLuong = " + int.Parse(txtSoLuong.Text) + " where MaSP = N'" + txtMaSP.Text + "'";   
            dtBase.UpdateData(sqlUpdate);
            LoadData();
            MessageBox.Show("Update thành công!");
            ResetValue();
            An();
        }

        private void btnLoai_Click(object sender, EventArgs e)
        {
            frmLoai formLoai = new frmLoai();
            formLoai.ShowDialog();
            cbTenLoai.DataSource = dtBase.DataReader("Select * from Loai");
            cbTenLoai.SelectedIndex = -1;
        }

        private void btnChuong_Click(object sender, EventArgs e)
        {
            frmChuong formChuong = new frmChuong();
            formChuong.ShowDialog();
            cbTenChuong.DataSource = dtBase.DataReader("select * from Chuong");
            cbTenChuong.SelectedIndex = -1;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //trường hợp khi chưa có tiêu chí thì sẽ lấy ra tất cả
            string TimKiem = txtTim.Text.Trim();
            string sqlSelect = sql + " where MaSP is not null";
            if (TimKiem != "")
            {
                //Tìm kiếm gần đúng với từ khóa like
                sqlSelect = sqlSelect + " and (MaSP like N'%" + TimKiem + "%' or TenSP like N'%" + TimKiem + "%' or TenChuong like N'%"+TimKiem+"%' or TenLoai like N'%"+TimKiem+"%')";
            }
            dgvSanPham.DataSource = dtBase.DataReader(sqlSelect);
        }
        //keypress
        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            //kiểm tra nếu phím người dùng nhấn ngoài các phím số thì ta không cho phép 
            if ((Convert.ToInt16(e.KeyChar) < Convert.ToInt16('0') ||
                Convert.ToInt16(e.KeyChar) > Convert.ToInt16('9')) && Convert.ToInt16(e.KeyChar) != 8)
            {
                e.Handled = true; //bắt phím được nhấn , không hiển thị ký tự của phím đó ra 
            }
        }

        private void txtGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < Convert.ToInt16('0') ||
                Convert.ToInt16(e.KeyChar) > Convert.ToInt16('9')) && Convert.ToInt16(e.KeyChar) != 8)
            {
                e.Handled = true;
            }
        }

        private void txtGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < Convert.ToInt16('0') ||
                Convert.ToInt16(e.KeyChar) > Convert.ToInt16('9')) && Convert.ToInt16(e.KeyChar) != 8)
            {
                e.Handled = true; 
            }
        }

        private void dgvSanPham_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Hien();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = false;
            ptbAnh.Image = null;
            txtMaSP.Text = dgvSanPham.CurrentRow.Cells[0].Value.ToString();
            txtTenSP.Text = dgvSanPham.CurrentRow.Cells[1].Value.ToString();
            txtCanNang.Text = dgvSanPham.CurrentRow.Cells[4].Value.ToString();
            txtMauLong.Text = dgvSanPham.CurrentRow.Cells[5].Value.ToString();
            txtMauMat.Text = dgvSanPham.CurrentRow.Cells[6].Value.ToString();
            dtpNgaySinh.Text = dgvSanPham.CurrentRow.Cells[7].Value.ToString();
            txtGiaNhap.Text = dgvSanPham.CurrentRow.Cells[9].Value.ToString();
            txtGiaBan.Text = dgvSanPham.CurrentRow.Cells[10].Value.ToString();
            txtSoLuong.Text = dgvSanPham.CurrentRow.Cells[11].Value.ToString();
            //Loai
            cbTenLoai.Text = "";
            cbTenLoai.SelectedText = dgvSanPham.CurrentRow.Cells[2].Value.ToString();
            //Chuong
            cbTenChuong.Text = "";
            cbTenChuong.SelectedText = dgvSanPham.CurrentRow.Cells[3].Value.ToString();
            try
            {
                ptbAnh.Image = Image.FromFile(Application.StartupPath.ToString() + "\\Images\\Product\\" + dgvSanPham.CurrentRow.Cells[8].Value.ToString());
            }
            catch
            {

            }
        }

        //thanh menustrip
        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKhachHang KhachHang = new frmKhachHang();
            this.Hide();
            KhachHang.ShowDialog();
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

        private void ThongKeuToolStripMenuItem_Click(object sender, EventArgs e)
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
