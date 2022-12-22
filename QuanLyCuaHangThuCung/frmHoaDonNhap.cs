using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelHDN = Microsoft.Office.Interop.Excel;

namespace QuanLyCuaHangThuCung
{
    public partial class frmHoaDonNhap : Form
    {
        Module.DataAccess dtbase = new Module.DataAccess();
        Module.SinhMa sm = new Module.SinhMa();
       // public static string MaNVDangNhap = "";
        public frmHoaDonNhap()
        {
            InitializeComponent();
        }
        private void frmHoaDonNhap_Load(object sender, EventArgs e)
        {
            //toolTipTrangChu.SetToolTip(btnBackHome, "Quay lại trang chủ");
            // lấy MaNV đăng nhập
            txtMaNV.Text = Form1.userMaNV;
            // đổ dữ liệu ra combobox
           // DataTable dtNhanvien = dtbase.DataReader("select MaNV,TenNV from NhanVien where MaNV = N'"+txtMaNV.Text+"'");
            DataTable dtNhaCungCap = dtbase.DataReader("select * from NhaCungCap");
            DataTable dtSanPham = dtbase.DataReader("select * from SanPham");
            txtTenNV.Text = Form1.userName;
            sm.FillCombo(cboMaNCC,dtNhaCungCap,"MaNCC", "MaNCC");
            sm.FillCombo(cboMaSP, dtSanPham, "MaSP", "MaSP");
            LoadData();
            //tải lên datagridview
            dgvHoaDonNhap.Columns[0].HeaderText = "Mã Sản Phẩm";
            dgvHoaDonNhap.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvHoaDonNhap.Columns[2].HeaderText = "Số lượng nhập";
            dgvHoaDonNhap.Columns[3].HeaderText = "Thành Tiền";
            ResetValue();
            ResetOption();
            HienthichitietHDN(false);
        }
        void LoadData()
        {
            dgvHoaDonNhap.DataSource = dtbase.DataReader("select CTHDN.MaSP,TenSP,soluongNhap,ThanhTien from (CTHoaDonNhap CTHDN  left join HoaDonNhap HDN on CTHDN.MaHDN = HDN.MaHDN )  " +
                " left join SanPham SP on CTHDN.MaSP = SP.MaSP where CTHDN.MaHDN = N'" + txtMaHDN.Text + "'");
        }
        void ResetValue()
        {
            txtMaHDN.Text = "";
            cboMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtLienHe.Text = "";
            cboMaSP.Text = "";
            cboTimMaHDN.Text = "";
            txtTenSP.Text = "";
            txtSoLuong.Text = "";
            txtThanhTien.Text = "0";
            txtTongTien.Text = "0";
            txtDonGia.Text = "";
            dtpNgayNhap.Value = DateTime.Now;
        }
        void ResetOption()
        {
            btnThemHDN.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;

            // btnBoQua.Enabled = false;
            // btnInHDN.Enabled = false;
            btnXoaSanPham.Enabled = false;
            btnHuyHDN.Enabled = false;
        }
        void HienthichitietHDN(bool hien)
        {
            cboMaNCC.Enabled = hien;
            cboMaSP.Enabled = hien;
            txtSoLuong.Enabled = hien;
            dtpNgayNhap.Enabled = false;
            //sét các chức năng
           // btnThemHDN.Enabled = hien;
           // btnHuyHDN.Enabled = hien;
        }

        double TinhTienSP(int slNhap)
        {
            double Thanhtien, DonGiaNhap;
            if (txtSoLuong.Text.Trim() == "")
            {
                slNhap = 0;
            }
            else
            {
                slNhap = int.Parse(txtSoLuong.Text);
            }
            DonGiaNhap = double.Parse(txtDonGia.Text);
            Thanhtien = (double)(slNhap * DonGiaNhap);
            return Thanhtien;
        }

        private void btnThemHDN_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnInHDN.Enabled = true;
            //load lại dữ liệu lên datagridview
            LoadData();
            txtMaHDN.Text = sm.SinhMaTD("HoaDonNhap", "HDN" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString(), "MaHDN");
            btnLuu.Enabled = true;
            btnThemHDN.Enabled = false;
            cboMaNCC.Enabled = true;
            HienthichitietHDN(true);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kiểm tra tính đầy đủ của dữ liệu
            if (cboMaNCC.Text.Trim()=="")
            {
                MessageBox.Show("Chưa có thông tin về nhà cung cấp");
                return;
            }
            if (cboMaSP.Text.Trim()=="")
            {
                MessageBox.Show("Chưa có thông tin về sản phẩm");
                return;
            }
            if (txtSoLuong.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập số lượng sản phẩm");
                return;
            }
            //Kiểm tra hd đã tồn tại chưa, chưa có thì thêm mới, có rồi thì update lại tổng tiền vào bảng hdn
            DataTable dtHoadon = dtbase.DataReader("select * from HoaDonNhap where MaHDN = N'"+txtMaHDN.Text+"'");
            string sqlHDN, sqlCTHDN, sqlSanPham;
            double Dongianhap;
            int slHDN,slSanPham;
            if (dtHoadon.Rows.Count == 0)
            {
                txtTongTien.Text = txtThanhTien.Text;
                 sqlHDN = "Insert into HoaDonNhap values (N'"+txtMaHDN.Text+"', N'"+txtMaNV.Text+"', N'"+cboMaNCC.SelectedValue.ToString()+"'," +
                    " '"+dtpNgayNhap.Value.ToString("MM/dd/yyyy")+"', N'"+txtTongTien.Text+"')";
            }
            else
            {
                //nếu đã có hóa đơn rồi thì update tổng tiền vào bang hóa đơn bán 
                txtTongTien.Text = (double.Parse(txtTongTien.Text)+ double.Parse(txtThanhTien.Text)).ToString();
                sqlHDN = "update HoaDonNhap set TongTienNhap = N'" + txtTongTien.Text + "' where MaHDN = N'"+txtMaHDN.Text+"'";
            }
            //lấy ra số lượng slSanPham có trong bảng sản phẩm
            DataTable dtSanPham = dtbase.DataReader("select * from SanPham where MaSP = N'" + cboMaSP.SelectedValue.ToString() + "'");
            Dongianhap = double.Parse(dtSanPham.Rows[0]["GiaNhap"].ToString());
            slSanPham = int.Parse(dtSanPham.Rows[0]["SoLuong"].ToString());
            slHDN = int.Parse(txtSoLuong.Text);
            //update lại bảng sản phẩm với số lượng vừa thêm vào 
            slSanPham = slSanPham +slHDN;
            sqlSanPham = "update SanPham set SoLuong = "+slSanPham+" where MaSP = N'"+cboMaSP.SelectedValue.ToString()+"'";

            //xem hóa đơn nhập với loại sản phẩm này đã có chưa để thực hiện thêm nhiều sản phẩm vào trong hóa đơn 
            DataTable dtCTHoadonnhap = dtbase.DataReader("select * from CTHoaDonNhap where Masp = N'"+cboMaSP.Text.ToString()+"' and MaHDN = N'"+txtMaHDN.Text+"'");
            int slSPcu,slSPthem,slSPmoi;
            string Thanhtienmoi,TongTienthem,Tongtiencu;
            if (dtCTHoadonnhap.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn chắc chắn muốn thêm  sản phẩm đã có này?? Nếu chỉnh sửa ấn Yes để tiếp tục, nếu không ấn No",
    "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    slSPcu = int.Parse(dtCTHoadonnhap.Rows[0]["SoluongNhap"].ToString());
                    slSPthem = int.Parse(txtSoLuong.Text);
                    slSPmoi = (slSPcu + slSPthem);
                    //MessageBox.Show(slSPmoi.ToString());
                    txtThanhTien.Text = TinhTienSP(slSPthem).ToString();
                    Tongtiencu = dtHoadon.Rows[0]["TongtienNhap"].ToString();
                    TongTienthem = TinhTienSP(slSPthem).ToString();
                    //update lại số lượng đã sửa cho bảng chi tiết HDN 
                    dtbase.UpdateData("update CTHoaDonNhap set SoLuongNhap = "+slSPmoi+"  where MaSP = '" + cboMaSP.SelectedValue.ToString() + "' and MaHDN = N'" + txtMaHDN.Text + "' ");
                    //  DataTable dtSanPham = dtbase.DataReader("select * from SanPham where MaSP = N'" + cboMaSP.SelectedValue.ToString() + "'");
                    //update thành tiền mới 
                    // MessageBox.Show(slSPcu.ToString());
                    // MessageBox.Show(slSPthem.ToString());
                    //MessageBox.Show(slSPmoi.ToString());
                    Thanhtienmoi = (Dongianhap * slSPmoi).ToString();
                    //Thanhtienmoi = TinhTienSP(slSPmoi).ToString();
                    dtbase.UpdateData("update CTHoaDonNhap set ThanhTien = N'"+Thanhtienmoi+"'  where MaSP = '" + cboMaSP.SelectedValue.ToString() + "' and MaHDN = N'" + txtMaHDN.Text + "' ");
                    //update lại tổng tiền của hóa đơn vừa sửa vào bảng HDN
                    txtTongTien.Text = (double.Parse(Tongtiencu) + double.Parse(TongTienthem)).ToString();
                    sqlHDN = "update HoaDonNhap set TongTienNhap = N'"+txtTongTien.Text +"' where MaHDN = '" + txtMaHDN.Text + "'";

                    //update lại bảng sản phẩm với sl đã tính
                    slSanPham = slSanPham + slSPthem;
                    dtbase.UpdateData("update SanPham set SoLuong = " + slSanPham + " where MaSP = '" + cboMaSP.SelectedValue.ToString() + "'");
                }
                else
                {
                    return;
                }
            }
            else
            {
                //chưa có thì thêm vào chi tiết hóa đơn
                 sqlCTHDN = "insert into CTHoaDonNhap values(N'" + txtMaHDN.Text + "',N'" + cboMaSP.SelectedValue.ToString() + "',"+int.Parse(txtSoLuong.Text.ToString())+", N'"+txtThanhTien.Text+"')";
                dtbase.UpdateData(sqlCTHDN);
            }

            dtbase.UpdateData(sqlHDN);
            dtbase.UpdateData(sqlSanPham);
            //load dữ liệu lên datagridview
            LoadData();
            cboMaNCC.Enabled = false;
            //ẩn các chức năng
            //HienthichitietHDN(true);

        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboMaSP.Text.Trim() == "")
                {
                   // MessageBox.Show("Bạn phải chọn mã sản phẩm", "Thông Báo", MessageBoxButtons.OK);
                    cboMaSP.Focus();
                    txtSoLuong.Text = "";
                    return;
                }
                else
                {
                    txtThanhTien.Text = TinhTienSP(int.Parse(txtSoLuong.Text)).ToString();
                }
            }
            catch { }
        }

        //đổ dữ liệu ra textbox tương ứng với nhà cung cấp
        private void cboMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtNhaCC = dtbase.DataReader("Select * from NhaCungCap where MaNCC = N'" + cboMaNCC.SelectedValue.ToString() + "'");
                txtTenNCC.Text = dtNhaCC.Rows[0]["TenNCC"].ToString();
                txtDiaChi.Text = dtNhaCC.Rows[0]["DiaChi"].ToString();
                txtLienHe.Text = dtNhaCC.Rows[0]["Lienhe"].ToString();
            }
            catch
            {

            }

        }

        private void cboMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtSanPham = dtbase.DataReader("Select * from SanPham where MaSP = '" + cboMaSP.SelectedValue.ToString() + "'");
                txtTenSP.Text = dtSanPham.Rows[0]["TenSP"].ToString();
                txtDonGia.Text = dtSanPham.Rows[0]["GiaNhap"].ToString();
            }
            catch
            {

            }
        }

        private void dgvHoaDonNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cboMaSP.Text = dgvHoaDonNhap.CurrentRow.Cells[0].Value.ToString();
               // txtTenSP.Text = dgvHoaDonNhap.CurrentRow.Cells[1].Value.ToString();
                txtSoLuong.Text = dgvHoaDonNhap.CurrentRow.Cells[2].Value.ToString();
                txtThanhTien.Text = dgvHoaDonNhap.CurrentRow.Cells[3].Value.ToString();

                DataTable dtHDnhap = dtbase.DataReader("Select * from HoaDonNhap where MaHDN = '" + txtMaHDN.Text + "'");
        
                cboMaNCC.Text = dtHDnhap.Rows[0]["MaNCC"].ToString();
                txtTongTien.Text = dtHDnhap.Rows[0]["TongTienNhap"].ToString();
                dtpNgayNhap.Text = dtHDnhap.Rows[0]["NgayNhap"].ToString();
            }
            catch
            {

            }
            //khi click vào thì ko cho sửa MaNCC, MaNV và kiểm tra MaNV set các chức năng
            if (txtMaNV.Text != Form1.userMaNV)
            {  
                btnThemHDN.Enabled = false;
                btnLuu.Enabled = false;
                btnSua.Enabled = false;
                btnHuyHDN.Enabled = false;
                btnXoaSanPham.Enabled = false;
            }
            else 
            {
                cboMaNCC.Enabled = false;

                btnHuyHDN.Enabled = true;
                btnLuu.Enabled = false;
                btnThemHDN.Enabled = false;
                btnBoQua.Enabled = true;
                cboMaSP.Enabled = false;
                btnInHDN.Enabled = true;
                if (cboMaSP.Text.Trim() != "")
                {
                    txtSoLuong.Enabled = true; 
                    btnSua.Enabled = true;
                }
                btnXoaSanPham.Enabled = true;
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            //xoa trang chi tiết
            ResetValue(); 
            LoadData();
            //Cam nhap vào groupBox chi tiết
            HienthichitietHDN(false);
            //Thiết lập lại các nút như ban đầu
            ResetOption();
           // btnInHDN.Enabled = false;
        }

        private void cboTimMaHDN_Click(object sender, EventArgs e)
        {
            sm.FillCombo(cboTimMaHDN, dtbase.DataReader("Select * from HoaDonNhap"), "MaHDN", "MaHDN");
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //ResetValue();
            DataTable dtHDnhap = dtbase.DataReader("Select * from HoaDonNhap where MaHDN = '" + cboTimMaHDN.Text + "'");
            txtMaHDN.Text = cboTimMaHDN.Text;
            LoadData();
            HienthichitietHDN(false);
            btnInHDN.Enabled = true;
            btnLuu.Enabled = true;
            btnHuyHDN.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //Hiện các thông tin chi tiết cho phép chỉnh sửa
            //HienthichitietHDN(true);
            btnLuu.Enabled = false;
            if (cboMaSP.Text.Trim() != "")
            {

                DialogResult result = MessageBox.Show("Bạn chắc chắn muốn thêm  sản phẩm đã có này?? Nếu chỉnh sửa ấn Yes để tiếp tục, nếu không ấn No",
    "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                { 
                    int slSPcu, slSPsua,slSanPham,slSPthem,Dongianhap;
                    string Thanhtienmoi, Tongtiencu, Tongtienmoi,Tongtienthem;
                    DataTable dtCTHoadonnhap = dtbase.DataReader("select * from CTHoaDonNhap where MaSP = N'"+cboMaSP.Text.ToString()+"' and MaHDN = N'"+txtMaHDN.Text+"'");
                    DataTable dtSanPham = dtbase.DataReader("select * from SanPham where MaSP = N'" + cboMaSP.Text.ToString() + "'");
                    DataTable dtHoadon = dtbase.DataReader("select * from HoaDonNhap where MaHDN = N'" + txtMaHDN.Text + "'");
                    Tongtiencu = dtHoadon.Rows[0]["TongtienNhap"].ToString();
                    slSPcu = int.Parse(dtCTHoadonnhap.Rows[0]["SoLuongNhap"].ToString());
                    slSanPham = int.Parse(dtSanPham.Rows[0]["SoLuong"].ToString());
                    slSPsua = int.Parse(txtSoLuong.Text);
                    Dongianhap = int.Parse(dtSanPham.Rows[0]["GiaNhap"].ToString());
                    //update lại số lượng mới cho bảng cthoadonnhap
                    //update lại số lượng ở bảng sản phẩm
                    dtbase.UpdateData("update CTHoaDonNhap set SoLuongNhap = " + slSPsua + "  where MaSP = '" + cboMaSP.SelectedValue.ToString() + "' and MaHDN = N'" + txtMaHDN.Text + "' ");
                    if (slSPcu > slSPsua)
                    {
                        //Tongtienmoi = double.Parse(Tongtiencu) - 
                        slSPthem = slSPcu - slSPsua;
                        slSanPham = slSanPham - slSPthem;
                        Tongtienthem = (slSPthem * Dongianhap).ToString();
                        //MessageBox.Show(Tongtiencu);
                       // MessageBox.Show(Tongtienthem);
                        Tongtienmoi = (Double.Parse(Tongtiencu) - double.Parse(Tongtienthem)).ToString();
                        txtTongTien.Text = Tongtienmoi;
                    }
                    else
                    {
                        slSPthem = slSPsua - slSPcu;
                        slSanPham = slSanPham + slSPthem;
                        Tongtienthem = (slSPthem * Dongianhap).ToString();
                        //MessageBox.Show(Tongtiencu);
                        //MessageBox.Show(Tongtienthem);
                        Tongtienmoi = (Double.Parse(Tongtiencu) + double.Parse(Tongtienthem)).ToString(); 
                        txtTongTien.Text = Tongtienmoi;

                    }
                    //update lại tổng tiền của hóa đơn vừa sửa vào bảng HDN
                    dtbase.UpdateData("Update CTHoaDonNhap set SoLuongNhap = " + slSPsua + " where MaSP = '" + cboMaSP.SelectedValue.ToString() + "' and MaHDN = N'" + txtMaHDN.Text + "' ");
                    dtbase.UpdateData("update HoaDonNhap set TongTienNhap = N'" + txtTongTien.Text + "' where MaHDN = '" + txtMaHDN.Text + "'");
 
                    dtbase.UpdateData("update SanPham set SoLuong = " + slSanPham + " where MaSP = '" + cboMaSP.SelectedValue.ToString() + "'");
                    //update lại thành tiền ở bảng cthoadonnhap
                    Thanhtienmoi = TinhTienSP(slSPsua).ToString();
                    dtbase.UpdateData("update CTHoaDonNhap set ThanhTien = N'" + Thanhtienmoi + "'  where MaSP = '" + cboMaSP.SelectedValue.ToString() + "' and MaHDN = N'" + txtMaHDN.Text + "' ");

                    //load dữ liệu lên datagridview
                    LoadData();
                }
                else
                {
                    return;
                }
            }

        }

        private void btnXoaSanPham_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn muốn chắc chắn xóa sản phẩm " + txtTenSP.Text + " thuộc hóa đơn có mã" +
                            " " + txtMaHDN.Text + "không???", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (rs == System.Windows.Forms.DialogResult.Yes)
            {
                double thanhTien = Convert.ToDouble(txtThanhTien.Text.Trim());
                int SoLuong = int.Parse(txtSoLuong.Text.Trim());
                string tongtiengocHDN = txtTongTien.Text.Trim();
                tongtiengocHDN = (Convert.ToDouble(tongtiengocHDN) - thanhTien).ToString();

                //update lại số lượng cho bảng sản phẩm
                dtbase.UpdateData("update SanPham set SoLuong = SoLuong + " + SoLuong + " where MaSP = N'" + cboMaSP.Text + "'");


                //update lại tổng tiền bán cho Hóa Đơn Nhập
                txtTongTien.Text = tongtiengocHDN;
                dtbase.UpdateData("update HoaDonNhap set TongTienNhap = '" + tongtiengocHDN + "' where MaHDN = '" + txtMaHDN.Text.Trim() + "'");


                //Xóa sản phẩm đó tại CHiTietHDB
                dtbase.UpdateData("Delete CTHoaDonNhap where MaHDN = '" + txtMaHDN.Text.Trim() + "' and MaSP ='" + cboMaSP.Text.Trim() + "'");
                //Thông báo
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                HienthichitietHDN(false);
                //ResetValue();
                ResetOption();
            }
        }

        private void btnHuyHDN_Click(object sender, EventArgs e)
        {
            //Bảng ChiTietHD lấy ra hàng hóa trong cùng 1 hóa đơn
            DataTable dtChitietHD = dtbase.DataReader("Select * from CTHoaDonNhap where MaHDN = '" + txtMaHDN.Text + "'");
            DialogResult rs = MessageBox.Show("Bạn muốn xóa hóa đơn có mã" + txtMaHDN.Text + " không???", "Thông báo",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (rs == System.Windows.Forms.DialogResult.Yes)
            {
                for (int i = 0; i < dtChitietHD.Rows.Count; i++)
                {
                    dtbase.UpdateData("Update SanPham set SoLuong = SoLuong + " + dtChitietHD.Rows[i]["SoLuongNhap"].ToString() + " " +
                        "where MaSP = '" + dtChitietHD.Rows[i]["MaSP"].ToString() + "'");

                }
                dtbase.UpdateData("Delete from CTHoaDonNhap where MaHDN = '" + txtMaHDN.Text + "'");
                dtbase.UpdateData("Delete from HoaDonNhap where MaHDN = '" + txtMaHDN.Text + "'");
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValue();
                HienthichitietHDN(false);
                LoadData();
                ResetOption();
            }
        }

        private void btnInHDN_Click(object sender, EventArgs e)
        {
            if (dgvHoaDonNhap.Rows.Count > 0)
            {
                //Khởi tạo và khai báo các đối tượng
                //Khai báo và khởi tạo một ứng dụng excel
                ExcelHDN.Application exApp = new ExcelHDN.Application();
                //add vào 1 workbook : file excel
                ExcelHDN.Workbook exbook = exApp.Workbooks.Add(ExcelHDN.XlWBATemplate.xlWBATWorksheet);
                //trỏ đến sheet đầu tiên
                ExcelHDN.Worksheet exsheet = (ExcelHDN.Worksheet)exbook.Worksheets[1];

                //Định dạng chung
                ExcelHDN.Range tenCuaHang = (ExcelHDN.Range)exsheet.Cells[1, 1];
                exsheet.get_Range("A1:E1").Merge(true);
                tenCuaHang.Font.Name = "Times New Roman";
                tenCuaHang.Font.Size = 16;
                tenCuaHang.Font.Bold = true;
                tenCuaHang.Font.Color = Color.Blue;
                tenCuaHang.Value = "CỬA HÀNG THÚ CƯNG";

                ExcelHDN.Range MaNCC = (ExcelHDN.Range)exsheet.Cells[2, 1];
                exsheet.get_Range("A2:E2").Merge(true);
                MaNCC.Font.Name = "Times New Roman";
                MaNCC.Font.Size = 12;
                MaNCC.Font.Bold = true;
                MaNCC.Value = "Mã NCC: " + cboMaNCC.Text;

                ExcelHDN.Range tenNCC = (ExcelHDN.Range)exsheet.Cells[3, 1];
                exsheet.get_Range("A3:E3").Merge(true);
                tenNCC.Font.Name = "Times New Roman";
                tenNCC.Font.Size = 12;
                tenNCC.Font.Bold = true;
                tenNCC.Value = "Tên nhà cung cấp: " + txtTenNCC.Text ;


                ExcelHDN.Range LienHe = (ExcelHDN.Range)exsheet.Cells[4, 1];
                exsheet.get_Range("A4:E4").Merge(true);
                LienHe.Font.Name = "Times New Roman";
                LienHe.Font.Size = 12;
                LienHe.Font.Bold = true;
                LienHe.Value = " Liên Hệ: " + txtLienHe.Text;

                ExcelHDN.Range DiaChi = (ExcelHDN.Range)exsheet.Cells[5, 1];
                exsheet.get_Range("A5:E5").Merge(true);
                DiaChi.Font.Name = "Times New Roman";
                DiaChi.Font.Size = 12;
                DiaChi.Font.Bold = true;
                DiaChi.Value = "Địa chỉ: " + txtDiaChi.Text;


                ExcelHDN.Range header = (ExcelHDN.Range)exsheet.Cells[6, 3];
                exsheet.get_Range("C6:E6").Merge(true);
                header.Font.Name = "Times New Roman";
                header.Font.Size = 16;
                header.Font.Bold = true;
                header.Font.Color = Color.Red;
                header.Value = "Hóa Đơn Nhập";

                //Định dạng tiêu đề bảng
                exsheet.get_Range("A7:F7").Font.Name = "Times New Roman";
                exsheet.get_Range("A7:F7").Font.Size = 12;
                exsheet.get_Range("A7:F7").HorizontalAlignment =
                Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                exsheet.get_Range("A7").Value = "STT";
                exsheet.get_Range("A7").Font.Bold = true;

                exsheet.get_Range("B7").Value = "Mã sản phẩm";
                exsheet.get_Range("B7").Font.Bold = true;
                exsheet.get_Range("B7").ColumnWidth = 12;

                exsheet.get_Range("C7").Value = "Tên sản phẩm";
                exsheet.get_Range("C7").Font.Bold = true;
                exsheet.get_Range("C7").ColumnWidth = 25;

                exsheet.get_Range("D7").Value = "Số lượng nhập";
                exsheet.get_Range("D7").Font.Bold = true;
                exsheet.get_Range("D7").ColumnWidth = 15;

                exsheet.get_Range("E7").Value = "Thành tiền";
                exsheet.get_Range("E7").Font.Bold = true;
                exsheet.get_Range("E7").ColumnWidth = 17;

                //In dữ liệu
                for (int i = 0; i < dgvHoaDonNhap.Rows.Count-1; i++)
                {
                    exsheet.get_Range("A" + (i + 8).ToString() + ":E" + (i + 8).ToString()).Font.Name = "Times New Roman";
                    exsheet.get_Range("A" + (i + 8).ToString() + ":E" + (i + 8).ToString()).HorizontalAlignment =
                    Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    exsheet.get_Range("A" + (i + 8).ToString()).Value = (i + 1).ToString();
                    exsheet.get_Range("B" + (i + 8).ToString()).Value = dgvHoaDonNhap.Rows[i].Cells["MaSP"].Value;
                    exsheet.get_Range("C" + (i + 8).ToString()).Value = dgvHoaDonNhap.Rows[i].Cells["TenSP"].Value;
                    exsheet.get_Range("D" + (i + 8).ToString()).Value = dgvHoaDonNhap.Rows[i].Cells["SoLuongNhap"].Value;
                    exsheet.get_Range("E" + (i + 8).ToString()).Value = dgvHoaDonNhap.Rows[i].Cells["ThanhTien"].Value;
                }

                //in ra tổng tiền ở cuối bảng
                int tong = dgvHoaDonNhap.Rows.Count + 7;
                ExcelHDN.Range TongTien = (ExcelHDN.Range)exsheet.Cells[tong, 5];
                TongTien.Font.Name = "Times New Roman";
                TongTien.Font.Size = 12;
                TongTien.Font.Bold = true;
                TongTien.Font.Color = Color.Blue;
                TongTien.Value = "Tổng Tiền: " + txtTongTien.Text;
                exsheet.get_Range("F" + tong).HorizontalAlignment =
               Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

                //đặt tên cho sheet
                exsheet.Name = "ExHDNhap";
                //kích hoạt file excel
                exbook.Activate();
                SaveFileDialog dlgSave = new SaveFileDialog();
                //liệt kê những tên đuôi được phép lưu
                dlgSave.Filter = "Excel Workbook(*.xlsx)|*.xlsx|All files(*.*)|*.*";
                dlgSave.InitialDirectory = Application.StartupPath.ToString() + "\\exportExcel";
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

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            frmNhaCungCap formNCC = new frmNhaCungCap();
            formNCC.ShowDialog();
            cboMaNCC.DataSource = dtbase.DataReader("select * from NhaCungCap");
            cboMaNCC.Text = "";
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            //FrmSanPham formSP = new FrmSanPham();
            //formSP.ShowDialog();
            //cboMaSP.DataSource = dtbase.DataReader("select * from SanPham");
            //cboMaSP.Text = "";
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

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmHoaDB FormHoadonban = new frmHoaDB();
            this.Hide();
            FormHoadonban.ShowDialog();
        }
    }
}
