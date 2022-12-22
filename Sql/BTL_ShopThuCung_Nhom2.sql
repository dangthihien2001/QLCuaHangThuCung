create database ShopThuCung
go

use ShopThuCung
go

create table Nhanvien
(
	MaNV nvarchar(10) primary key not null,
	TenNV nvarchar(50) null,
	GioiTinh nvarchar(10) null,
	DiaChi nvarchar(100) null,
	SDT nvarchar(20) null,
	NgaySinh date null,
	NgayVaoLam date null,
	Chucvu nvarchar(20),
	Luong float null,
	CCCD nvarchar(20)  null
)

--insert into Nhanvien values(N'NV02',N'Nguyễn Thị Quỳnh Như',N'Nữ',N'Thái Bình',N'028499493',null,null,N'Nhân Viên',3500000,37492904803);
--insert into Nhanvien values(N'NV03',N'Đặng Thị Hiền',N'Nữ',N'Vĩnh Phúc',N'09484793',null,null,N'Quản Lý',4500000,377949474932);

create table DangNhap
(	
	MaNV nvarchar(10) not null,
	TaiKhoan nvarchar(50) not null,
	MatKhau nvarchar(50) not null,
	/*constraint fk_DangNhap_MaNV foreign key(MaNV)
		references NhanVien(MaNV),*/
)


create table Chuong
(
	MaChuong nvarchar(10) primary key not null,
	TenChuong nvarchar(20) null,  
)

create table Loai
(
	MaLoai nvarchar(10) primary key not null,
	TenLoai nvarchar(50) null
)


create table SanPham
(
	MaSP nvarchar(10) primary key not null,	
	TenSP nvarchar(50) null,
    Maloai nvarchar(10) null,
	MaChuong nvarchar(10) null,
	CanNang float  null,
	MauLong nvarchar(50) null,
	MauMat nvarchar(50) null,
	NgaySinh date null,
	Anh nvarchar(50) null,
	GiaNhap nvarchar(max) null,
	GiaBan nvarchar(max) null,
	SoLuong bigint null,
	/*constraint fk_SanPham_MaLoai foreign key(MaLoai)
		references Loai(MaLoai),*/
	/*constraint fk_SanPham_MaChuong foreign key(MaChuong)
		references Chuong(MaChuong),*/
)


create table KhachHang
(
	MaKh nvarchar(10) primary key not null,
	TenKh nvarchar(50) null,
	DiaChi nvarchar(100) null,
	Sdt nvarchar(20) null,
	GioiTinh nvarchar(10) null,
	NgaySinh date null
)
--insert into KhachHang values (N'KH00',N'Nguyễn Linh Chi','Hà Nội',N'09479744',N'Nữ',null);

create table NhaCungCap
(
	MaNCC nvarchar(10) primary key not null,
	TenNCC nvarchar(50) null,
	diachi nvarchar(100) null,
	Lienhe nvarchar(100) null
)
--insert into NhaCungCap values (N'NCC00',N'Petmart',N'Sơn Đông - Lập thạch - Vĩnh Phúc',N'09375944');

create table HoaDonNhap
(
	MaHDN nvarchar(50) primary key not null,
	MaNV nvarchar(10) null,
	MaNCC nvarchar(10) null,
	NgayNhap date null,
	TongTienNhap nvarchar(max) null,
   /*constraint fk_HoadonNhap_MaNV foreign key(MaMV)
		references NhanVien(MaNV)*/
   /*constraint fk_HoadonNhap_MaNCC foreign key(MaNCC)
		references NhaCungCap(MaNCC)*/
)


create table CTHoaDonNhap
(
	MaHDN nvarchar(50) not null,
	MaSP nvarchar(10) not null,
	SoLuongNhap bigint null,
	ThanhTien nvarchar(max) null,
   /*constraint fk_CTHDN_MaHDN foreign key(MaHDN)
		references  Hoadonnhap(MaHDN)*/
   /*constraint fk_CTHDB_MaSP foreign key(MaSP)
		references SanPham(MaSP)*/
)

create table HoaDonBan
(
	MaHDB nvarchar(50) primary key,
	MaNV nvarchar(10) null,
	NgayBan date null,
	MaKh nvarchar(10) null,
	TongTienBan nvarchar(max) null,
   /*constraint fk_Hoadonban_MaNv foreign key(MaMV)
		references NhanVien(MaNV)*/
   /*constraint fk_Hoadonban_MaKh foreign key(MaKH)
		references KhachHang(MaKH)*/
)

create table CTHoaDonBan
(
	MaHDB nvarchar(50) not null,
	MaSP nvarchar(10) not null,
	SoLuongBan bigint null,
	KhuyenMai int null,
	ThanhTien nvarchar(max) null,
   /*constraint fk_CTHDB_MaHDB foreign key(MaHDB)
		references  Hoadonban(MaHDB)*/
   /*constraint fk_CTHDB_MaSP foreign key(MaSP)
		references SanPham(MaSP)*/
)


