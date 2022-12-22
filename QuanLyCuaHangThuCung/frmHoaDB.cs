using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelHDB = Microsoft.Office.Interop.Excel;

namespace QuanLyCuaHangThuCung
{
    public partial class frmHoaDB : Form
    {
        Module.DataAccess dtbase = new Module.DataAccess();
        Module.SinhMa sm = new Module.SinhMa();
        public frmHoaDB()
        {
            InitializeComponent();
        }

        private void frmHoaDB_Load(object sender, EventArgs e)
        {
            //Lấy tên đăng nhập đổ vào txtMaNV và txtTenNV
            txtMaNV.Text = "" + Form1.userMaNV;
            DataTable dtNhanVien = dtbase.DataReader("Select * from NhanVien where MaNV = '" + txtMaNV.Text + "'");
            txtTenNV.Text = dtNhanVien.Rows[0]["TenNV"].ToString();
            //đổ tên vào label xin chào
            lbTenNVHD.Text = "Xin chào:" + txtTenNV.Text;

            this.StartPosition = FormStartPosition.CenterScreen;
            //Đổ dữ liệu ra các ComboBox
            DataTable KhachHang = dtbase.DataReader("Select * from KhachHang");
            DataTable SanPham = dtbase.DataReader("Select * from SanPham");
            sm.FillCombo(cboMaKH, KhachHang, "MaKH", "MaKH");
            sm.FillCombo(cboMaSP, SanPham, "MaSP", "MaSP");
            LoadData();
            dgvHoaDonBan.Columns[0].HeaderText = "Mã sản phẩm";
            dgvHoaDonBan.Columns[1].HeaderText = "Tên sản phẩm";
            dgvHoaDonBan.Columns[2].HeaderText = "Số lượng bán";
            dgvHoaDonBan.Columns[3].HeaderText = "Khuyến mại";
            dgvHoaDonBan.Columns[4].HeaderText = "Thành tiền";
            HienChiTietHDB(false);
            ResetValue();
            ResetOption();
        }

        void LoadData()
        {
            dgvHoaDonBan.DataSource = dtbase.DataReader("Select CTHoaDonBan.MaSP, TenSP, CtHoaDonBan.SoLuongBan, KhuyenMai, ThanhTien " +
                " from CTHoaDonBan inner join SanPham on CtHoaDonBan.MaSP = SanPham.MaSP where MaHDB = '" + txtMaHDB.Text + "'");
        }

        void ResetValue()
        {
            txtMaHDB.Text = "";
            dtpNgayBan.Value = DateTime.Now;
            cboMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            cboMaSP.Text = "";
            txtTenSP.Text = "";
            txtSoLuong.Text = "";
            txtKhuyenMai.Text = "";
            txtDonGia.Text = "";
            txtThanhTien.Text = "";
            txtTongTien.Text = "0";
        }

        void ResetOption()
        {
            cboMaKH.Enabled = false;
            dtpNgayBan.Enabled = false;
            cboTimMaHDB.Text = "";
            btnThemHDB.Enabled = true;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            btnHuyHD.Enabled = false;
            btnXoaSP.Enabled = false;
            btnCTHoaDB.Enabled = false;
        }
        void HienChiTietHDB(bool hien)
        {
            cboMaSP.Enabled = hien;
            txtKhuyenMai.Enabled = hien;
            txtSoLuong.Enabled = hien;
            //set cac chuc nang
            btnThemHDB.Enabled = hien;
            btnHuyHD.Enabled = hien;
            btnXoaSP.Enabled = hien;
        }
        private void dgvHoaDonBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cboMaSP.Text = dgvHoaDonBan.CurrentRow.Cells[0].Value.ToString();
                txtSoLuong.Text = dgvHoaDonBan.CurrentRow.Cells[2].Value.ToString();
                txtKhuyenMai.Text = dgvHoaDonBan.CurrentRow.Cells[3].Value.ToString();
                txtThanhTien.Text = dgvHoaDonBan.CurrentRow.Cells[4].Value.ToString();

                DataTable dtHDban = dtbase.DataReader("Select * from HoaDonBan where MaHDB = '" + txtMaHDB.Text + "'");
                cboMaKH.Text = dtHDban.Rows[0]["MaKh"].ToString();
                txtTongTien.Text = dtHDban.Rows[0]["TongTienBan"].ToString();
                dtpNgayBan.Text = dtHDban.Rows[0]["NgayBan"].ToString();
            }
            catch
            {

            }

            //khi click vào thì ko cho sửa MaKH
            cboMaKH.Enabled = false;
            dtpNgayBan.Enabled = false;
            btnSua.Enabled = true;
            btnHuyHD.Enabled = true;

            //Kiểm tra MaNV set các chức năng
            if (txtMaNV.Text != Form1.userMaNV)
            {
                HienChiTietHDB(false);
                btnThemHDB.Enabled = false;
                btnLuu.Enabled = false;
                btnSua.Enabled = false;
                btnHuyHD.Enabled = false;
                btnXoaSP.Enabled = false;
            }
        }

        //Đổ dữ liệu ra các textbox tương ứng với khách hàng
        private void cboMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtKhachHang = dtbase.DataReader("Select * from KhachHang where MaKh = '" + cboMaKH.SelectedValue.ToString() + "'");
                txtTenKH.Text = dtKhachHang.Rows[0]["TenKh"].ToString();
                txtDiaChi.Text = dtKhachHang.Rows[0]["DiaChi"].ToString();
                txtSDT.Text = dtKhachHang.Rows[0]["Sdt"].ToString();
            }
            catch
            {
                txtTenKH.Text = "";
                txtDiaChi.Text = "";
                txtSDT.Text = "";
            }
        }

        //Đổ dữ liệu ra textbox tương ứng với sản phẩm
        private void cboMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtSanPham = dtbase.DataReader("Select * from SanPham where MaSP = '" + cboMaSP.SelectedValue.ToString() + "'");
                txtTenSP.Text = dtSanPham.Rows[0]["TenSP"].ToString();
                txtDonGia.Text = dtSanPham.Rows[0]["GiaBan"].ToString();
            }
            catch
            {

            }
        }

        //Sự kiện click vào btnthem: Sinh mới MaHDB, set lại button
        private void btnThemHDB_Click(object sender, EventArgs e)
        {
            ResetValue();
            //load lại dữ liệu lên datagridview
            LoadData();
            //Hiện thông tin sản phẩm
            HienChiTietHDB(true);
            txtMaHDB.Text = sm.SinhMaTD("HoaDonBan", "HDB" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString(), "MaHDB");
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            btnHuyHD.Enabled = false;
            cboMaKH.Enabled = true;
            btnCTHoaDB.Enabled = true;
          
           
        }

        //Tình tiền từng mặt hàng
        double TinhTienSP()
        {
            double thanhtien, dongia, sl, giamgia;
            if (txtKhuyenMai.Text.Trim() == "")
                giamgia = 0;
            else
                giamgia = double.Parse(txtKhuyenMai.Text);
            if (txtSoLuong.Text.Trim() == "")
            {
                sl = 0;
            }
            else
            {
                sl = double.Parse(txtSoLuong.Text);
            }
            dongia = double.Parse(txtDonGia.Text);
            thanhtien = dongia * sl * (1 - giamgia / 100);
            return thanhtien;
        }

        //Khi số lượng và giảm giá thay đổi sẽ tinh tiền của mỗi sản phẩm
        private void txtKhuyenMai_TextChanged(object sender, EventArgs e)
        {

            txtThanhTien.Text = TinhTienSP().ToString();
        }

        //Sự kiện nút Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kiểm tra tính đầy đủ của dữ liệu
            if (cboMaKH.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa có thông tin về khách hàng");
                return;
            }
            if (cboMaSP.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa có thông tin về sản phẩm");
                return;
            }
            if (txtSoLuong.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập số lượng sản phẩm");
                return;
            }
            if (txtKhuyenMai.Text.Trim() == "")
            {
                txtKhuyenMai.Text = "0";
            }

            //Kiểm tra HD đã tồn tại chưa, chưa có thì thêm mới, có rồi thì update lại tổng tiền vào bảng HDB
            DataTable dtHDBan = dtbase.DataReader("Select * from HoaDonBan where MaHDB = '" + txtMaHDB.Text + "'");
            string sqlHDBan, sqlCTHoaDonBan, sqlSanPham;
            int slBan, slCon;
            //khi chua ton tai hoa don thi them moi
            if (dtHDBan.Rows.Count == 0)
            {
                txtTongTien.Text = txtThanhTien.Text;
                sqlHDBan = "insert into HoaDonBan values ('" + txtMaHDB.Text + "', '" + txtMaNV.Text + "'," +
                    " '" + dtpNgayBan.Value.ToString("MM/dd/yyyy") + "', '" + cboMaKH.SelectedValue.ToString() + "', '" + txtTongTien.Text + "')";
            }
            else
            {
                //Nếu đã hóa đơn đã có thì update lại tổng tiền vào bảng HDB
                txtTongTien.Text = (double.Parse(txtTongTien.Text) + double.Parse(txtThanhTien.Text)).ToString();
                sqlHDBan = "update HoaDonBan set TongTienBan = '" + txtTongTien.Text + "' where MaHDB = '" + txtMaHDB.Text + "'";
            }

            //dtbase.UpdateData(sqlHDBan);
            //Xử lý việc mua sản phẩm vượt quá số lượng cho phép
            DataTable dtSP = dtbase.DataReader("Select * from SanPham where MaSP = '" + cboMaSP.SelectedValue.ToString() + "'");
            //lấy ra số lượng sp còn lại trong bảng sản phẩm
            slCon = int.Parse(dtSP.Rows[0]["SoLuong"].ToString());
            slBan = int.Parse(txtSoLuong.Text);
            if (slCon < slBan)
            {
                MessageBox.Show("Kho chỉ còn " + slCon + " , số lượng bạn nhập quá lớn!!!");
                return;
            }
            else
            {
                slCon = slCon - slBan;
                //update lại bảng hàng với sl đã tính
                sqlSanPham = "update SanPham set SoLuong = " + slCon + " where MaSP = '" + cboMaSP.Text + "'";

            }

            //Xem Hóa đơn với loại hàng này đã có chưa để thực hiện thêm , sửa sp  trong hóa đơn
            DataTable dtCTHoaDonBan = dtbase.DataReader("Select * from CTHoaDonBan where MaHDB = N'" + txtMaHDB.Text + "' and " +
                "MaSP = N'" + cboMaSP.Text + "'");
            if (dtCTHoaDonBan.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn chắc chắn muốn chỉnh sửa sản phẩm này?? Nếu chỉnh sửa ấn Yes để tiếp tục, nếu không ấn No",
                    "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string sqlSLUpdate;
                    int slcu;
                    double TongTiencu, TienSPcu;
                    //lấy ra số lượng sản phẩm cũ
                    slcu = int.Parse(dtCTHoaDonBan.Rows[0]["SoLuongBan"].ToString());
                    TongTiencu = double.Parse(dtHDBan.Rows[0]["TongTienBan"].ToString());
                    TienSPcu = double.Parse(dtCTHoaDonBan.Rows[0]["ThanhTien"].ToString());
                    slCon = slCon + slcu;
                    if (slCon < slBan)
                    {
                        MessageBox.Show("Kho chỉ còn " + slCon + " , số lượng bạn nhập quá lớn!!!");
                        return;
                    }
                    else
                    {
                        slCon = slCon - slBan;
                        //update lại bảng hàng với sl đã tính
                        dtbase.UpdateData("update SanPham set SoLuong = " + slCon + " where MaSP = '" + cboMaSP.Text + "'");
                    }
                    //update lại số lượng đã sửa cho bảng chi tiết HDB
                    sqlSLUpdate = "update CTHoaDonBan set SoLuongBan = " + slBan + " , KhuyenMai = N'"+txtKhuyenMai.Text+"' where MaSP = '" + cboMaSP.Text + "'";
                    dtbase.UpdateData(sqlSLUpdate);

                    //update lại thanh tien vào bảng ctHoaDonBan
                    txtThanhTien.Text = TinhTienSP().ToString();
                    dtbase.UpdateData("update CTHoaDonBan set ThanhTien = '" + txtThanhTien.Text + "' where MaSP = '" + cboMaSP.Text + "' ");


                    //update lại tổng tiền của hóa đơn vừa sửa vào bảng HDB
                    txtTongTien.Text = (TongTiencu - TienSPcu + double.Parse(txtThanhTien.Text)).ToString();
                    sqlHDBan = "update HoaDonBan set TongTienBan = '" + txtTongTien.Text + "' where MaHDB = '" + txtMaHDB.Text + "'";
                }
                else
                {
                    return;
                }
            }
            else
            {
                //chưa có thì insert vào tblChiTietHD
                sqlCTHoaDonBan = "insert into CTHoaDonBan(MaHDB, MaSP, SoLuongBan, KhuyenMai, ThanhTien) values(N'" + txtMaHDB.Text + "', N'" + cboMaSP.Text + "'," +
                    " " + int.Parse(txtSoLuong.Text) + ", " + int.Parse(txtKhuyenMai.Text.ToString()) + ", '" + txtThanhTien.Text + "')";
                dtbase.UpdateData(sqlCTHoaDonBan);
            }
            dtbase.UpdateData(sqlHDBan);
            dtbase.UpdateData(sqlSanPham);
            //Load dữ liệu lên datagridview
            LoadData();
            //ẩn các chức năng
            HienChiTietHDB(false);
            ResetOption();
            btnSua.Enabled = false;
            btnHuyHD.Enabled = false;
            btnCTHoaDB.Enabled = true;

        }

        private void btnCTHoaDB_Click(object sender, EventArgs e)
        {
            cboMaSP.Enabled = true;
            txtSoLuong.Enabled = true;
            txtKhuyenMai.Enabled = true;
            btnLuu.Enabled = true;

        }

        private void btnThemHDB_MouseDown(object sender, MouseEventArgs e)
        {
            ((Button)sender).BackgroundImage = Properties.Resources.butbut;
        }

        private void btnThemHDB_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = Properties.Resources.xanh;
        }

        private void btnThemHDB_MouseMove(object sender, MouseEventArgs e)
        {
            ((Button)sender).BackgroundImage = Properties.Resources.btuufon112;
        }

        //khi click vào nút sửa thì hiện các chi tiết cho phép người dùng chỉnh sửa thông tin sản phẩm
        private void btnSua_Click(object sender, EventArgs e)
        {
            //Hiện các thông tin chi tiết cho phép chỉnh sửa
            HienChiTietHDB(true);
            //chi cho sua so luong va khuyen mai
            cboMaSP.Enabled = false;
            cboMaKH.Enabled = false;
            dtpNgayBan.Enabled = false;
            //Ẩn các nút thêm và hủy hóa đơn
            btnThemHDB.Enabled = false;
            btnHuyHD.Enabled = false;
            btnLuu.Enabled = true;
        }

        private void btnHuyHD_Click(object sender, EventArgs e)
        {
            //Bảng ChiTietHD lấy ra hàng hóa trong cùng 1 hóa đơn
            DataTable dtChitietHD = dtbase.DataReader("Select * from CTHoaDonBan where MaHDB = '" + txtMaHDB.Text + "'");
            DialogResult rs = MessageBox.Show("Bạn muốn xóa hóa đơn có mã" + txtMaHDB.Text + " không???", "Thông báo",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (rs == System.Windows.Forms.DialogResult.Yes)
            {
                for (int i = 0; i < dtChitietHD.Rows.Count; i++)
                {
                    dtbase.UpdateData("Update SanPham set SoLuong = SoLuong + " + dtChitietHD.Rows[i]["SoLuongBan"].ToString() + " " +
                        "where MaSP = '" + dtChitietHD.Rows[i]["MaSP"].ToString() + "'");
                    
                }
                dtbase.UpdateData("Delete from CTHoaDonBan where MaHDB = '" + txtMaHDB.Text + "'");
                dtbase.UpdateData("Delete from HoaDonBan where MaHDB = '" + txtMaHDB.Text + "'");
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValue();
                HienChiTietHDB(false);
                LoadData();
                ResetOption();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            //Lấy tên đăng nhập đổ vào txtMaNV và TenNV
            txtMaNV.Text = "" + Form1.userMaNV;
            DataTable NhanVien = dtbase.DataReader("Select * from Nhanvien where MaNV = '" + txtMaNV.Text + "'");
            txtTenNV.Text = NhanVien.Rows[0]["TenNV"].ToString();
            //Cam nhap vào groupBox chi tiết
            HienChiTietHDB(false);
            //xoa trang chi tiết
            ResetValue();
            //Thiết lập lại các nút như ban đầu
            ResetOption();
            LoadData();
        }

        private void btnInHDB_Click(object sender, EventArgs e)
        {
            if (dgvHoaDonBan.Rows.Count > 0)
            {
                //Khởi tạo và khai báo các đối tượng
                //Khai báo và khởi tạo một ứng dụng excel
                ExcelHDB.Application exApp = new ExcelHDB.Application();
                //add vào 1 workbook : file excel
                ExcelHDB.Workbook exbook = exApp.Workbooks.Add(ExcelHDB.XlWBATemplate.xlWBATWorksheet);
                //trỏ đến sheet đầu tiên
                ExcelHDB.Worksheet exsheet = (ExcelHDB.Worksheet)exbook.Worksheets[1];

                //Định dạng chung
                ExcelHDB.Range tenCuaHang = (ExcelHDB.Range)exsheet.Cells[1, 1];
                exsheet.get_Range("A1:D1").Merge(true);
                tenCuaHang.Font.Name = "Times New Roman";
                tenCuaHang.Font.Size = 16;
                tenCuaHang.Font.Bold = true;
                tenCuaHang.Font.Color = Color.Blue;
                tenCuaHang.Value = "CỬA HÀNG THÚ CƯNG";

                ExcelHDB.Range tenKH = (ExcelHDB.Range)exsheet.Cells[2, 1];
                tenKH.Font.Name = "Times New Roman";
                tenKH.Font.Size = 12;
                tenKH.Font.Bold = true;
                tenKH.Value = "Tên khách hàng :  " + txtTenKH.Text;

                ExcelHDB.Range DcSDT = (ExcelHDB.Range)exsheet.Cells[3, 1];
                exsheet.get_Range("A3:D3").Merge(true);
                DcSDT.Font.Name = "Times New Roman";
                DcSDT.Font.Size = 12;
                DcSDT.Font.Bold = true;
                DcSDT.Value = "Địa chỉ:  " + txtDiaChi.Text + ",  Số điện thoại:  " + txtSDT.Text;


                ExcelHDB.Range header = (ExcelHDB.Range)exsheet.Cells[5, 3];
                exsheet.get_Range("C5:E5").Merge(true);
                header.Font.Name = "Times New Roman";
                header.Font.Size = 16;
                header.Font.Bold = true;
                header.Font.Color = Color.Red;
                header.Value = "Hóa Đơn Bán";

                //Định dạng tiêu đề bảng
                exsheet.get_Range("A6:F6").Font.Name = "Times New Roman";
                exsheet.get_Range("A6:F6").Font.Size = 12;
                exsheet.get_Range("A6:F6").HorizontalAlignment =
                Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                exsheet.get_Range("A6").Value = "STT";
                exsheet.get_Range("A6").Font.Bold = true;
                exsheet.get_Range("B6").Value = "Mã sản phẩm";
                exsheet.get_Range("B6").Font.Bold = true;
                exsheet.get_Range("B6").ColumnWidth = 15;
                exsheet.get_Range("C6").Value = "Tên sản phẩm";
                exsheet.get_Range("C6").Font.Bold = true;
                exsheet.get_Range("C6").ColumnWidth = 25;
                exsheet.get_Range("D6").Value = "Số lượng";
                exsheet.get_Range("D6").Font.Bold = true;
                exsheet.get_Range("D6").ColumnWidth = 12;
                exsheet.get_Range("E6").Value = "Khuyến mại";
                exsheet.get_Range("E6").Font.Bold = true;
                exsheet.get_Range("E6").ColumnWidth = 12;
                exsheet.get_Range("F6").Value = "Thành tiền";
                exsheet.get_Range("F6").Font.Bold = true;
                exsheet.get_Range("F6").ColumnWidth = 15;

                //In dữ liệu
                for (int i = 0; i < dgvHoaDonBan.Rows.Count -1 ; i++)
                {
                    exsheet.get_Range("A" + (i + 7).ToString() + ":F" + (i + 7).ToString()).Font.Name = "Times New Roman";
                    exsheet.get_Range("A" + (i + 7).ToString() + ":F" + (i + 7).ToString()).HorizontalAlignment =
                    Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    exsheet.get_Range("A" + (i + 7).ToString()).Value = (i + 1).ToString();
                    exsheet.get_Range("B" + (i + 7).ToString()).Value = dgvHoaDonBan.Rows[i].Cells["MaSP"].Value;
                    exsheet.get_Range("C" + (i + 7).ToString()).Value = dgvHoaDonBan.Rows[i].Cells["TenSP"].Value;
                    exsheet.get_Range("D" + (i + 7).ToString()).Value = dgvHoaDonBan.Rows[i].Cells["SoLuongBan"].Value;
                    exsheet.get_Range("E" + (i + 7).ToString()).Value = dgvHoaDonBan.Rows[i].Cells["KhuyenMai"].Value;
                    exsheet.get_Range("F" + (i + 7).ToString()).Value = dgvHoaDonBan.Rows[i].Cells["ThanhTien"].Value;
                }
                //in ra tổng tiền ở cuối bảng
                int tong = dgvHoaDonBan.Rows.Count + 7;
                ExcelHDB.Range TongTien = (ExcelHDB.Range)exsheet.Cells[tong, 6];
                TongTien.Font.Name = "Times New Roman";
                exsheet.get_Range("F" + tong).HorizontalAlignment =
               Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                TongTien.Font.Size = 12;
                TongTien.Font.Bold = true;
                TongTien.Font.Color = Color.Blue;
                TongTien.Value = "Tổng Tiền: " + txtTongTien.Text + " VNĐ";

                //đặt tên cho sheet
                exsheet.Name = "ExHDBan";
                //kích hoạt file excel
                exbook.Activate();
                SaveFileDialog dlgSave = new SaveFileDialog();
                //liệt kê những tên đuôi được phép lưu
                dlgSave.Filter = "Excel Workbook(*.xlsx)|*.xlsx|All files(*.*)|*.*";
                //chỉ định hiển thị sẵn bộ lọc đầu tiên
                dlgSave.FilterIndex = 1;
                dlgSave.AddExtension = true;
                dlgSave.DefaultExt = ".xlsx";
                //nếu mở thành công
                try
                {
                    //lưu file dialog đường dẫn được lấy bằng thuộc tính FileName của hộp thoại lưu file 
                    if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        exbook.SaveAs(dlgSave.FileName.ToString());
                    }
                }
                catch
                {
                    // MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                exApp.Quit();
            }
            else
                MessageBox.Show("Không có dữ liệu để in!!!");
        }

        //nút trở về trang chủ
        private void btnBackHome_Click(object sender, EventArgs e)
        {
            Form1 TrangChu = new Form1();
            this.Hide();
            TrangChu.ShowDialog();
            
        }

        private void cboTimMaHDB_Click(object sender, EventArgs e)
        {
            sm.FillCombo(cboTimMaHDB, dtbase.DataReader("Select * from HoaDonBan"), "MaHDB", "MaHDB");
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            ResetValue();
            DataTable dtHDban = dtbase.DataReader("Select * from HoaDonBan where MaHDB = '" + cboTimMaHDB.Text + "'");
            txtMaHDB.Text = cboTimMaHDB.Text;
            txtTongTien.Text = dtHDban.Rows[0]["TongTienBan"].ToString();
            txtMaNV.Text = dtHDban.Rows[0]["MaNV"].ToString();
            cboMaKH.Text = dtHDban.Rows[0]["MaKh"].ToString();
            dtpNgayBan.Text = dtHDban.Rows[0]["NgayBan"].ToString();
            LoadData();
            HienChiTietHDB(false);
        }

        //button add thêm khách hàng sẽ dẫn người dùng đến form Khách hàng
        private void btnAddKH_Click(object sender, EventArgs e)
        {
            frmKhachHang KhachHang = new frmKhachHang();
            this.Hide();
            KhachHang.ShowDialog();
            this.Show();
            //sau khi người dùng ấn thêm thì đưa chất liệu ra combobox
            cboMaKH.DataSource = dtbase.DataReader("Select * from KhachHang");
        }

        //button add thêm San Pham sẽ dẫn người dùng đến form Khách hàng
        private void btnAddSP_Click(object sender, EventArgs e)
        {
            FrmSanPham SanPham = new FrmSanPham();
            this.Hide();
            SanPham.ShowDialog();
            this.Show();
            //sau khi người dùng ấn thêm thì đưa sản phẩm ra combobox
            cboMaSP.DataSource = dtbase.DataReader("Select * from SanPham");
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn muốn chắc chắn xóa sản phẩm " + txtTenSP.Text + " thuộc hóa đơn có mã" +
                " " + txtMaHDB.Text + "không???", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (rs == System.Windows.Forms.DialogResult.Yes)
            {
                double thanhTien = Convert.ToDouble(txtThanhTien.Text.Trim());
                int SoLuong = int.Parse(txtSoLuong.Text.Trim());
                string tongtiengocHDB =txtTongTien.Text.Trim();
                tongtiengocHDB = (Convert.ToDouble(tongtiengocHDB) - thanhTien).ToString();

                //update lại số lượng cho bảng sản phẩm
                dtbase.UpdateData("update SanPham set SoLuong = SoLuong + " + SoLuong + " where MaSP = N'" + cboMaSP.Text + "'");

                //update lại tổng tiền bán cho Hóa Đơn Bán
                txtTongTien.Text = tongtiengocHDB;
                dtbase.UpdateData("update HoaDonBan set TongTienBan = '" + tongtiengocHDB + "' where MaHDB = '" + txtMaHDB.Text.Trim() + "'");

                //Xóa sản phẩm đó tại CHiTietHDB
                dtbase.UpdateData("Delete CTHoaDonBan where MaHDB = '"+txtMaHDB.Text.Trim()+"' and MaSP ='" + cboMaSP.Text.Trim() + "'");
                //Thông báo
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                HienChiTietHDB(false);
                ResetOption();
            }
           
        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtNhanVien = dtbase.DataReader("Select * from NhanVien where MaNV = '" + txtMaNV.Text + "'");
                txtTenNV.Text = dtNhanVien.Rows[0]["TenNV"].ToString();
            }
            catch
            {

            }
        }

        //bắt sự kiện nhấn phím 
        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            //kiểm tra nếu phím người dùng nhấn ngoài các phím số thì ta không cho phép 
            if ((Convert.ToInt16(e.KeyChar) < Convert.ToInt16('0') ||
                Convert.ToInt16(e.KeyChar) > Convert.ToInt16('9')) && Convert.ToInt16(e.KeyChar) != 8)
            {
                e.Handled = true; //bắt phím được nhấn , không hiển thị ký tự của phím đó ra 
            }
        }

        //thanh menustrip
        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 TrangChu = new Form1();
            this.Hide();
            TrangChu.ShowDialog();
        }

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
