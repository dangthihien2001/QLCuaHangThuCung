using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyCuaHangThuCung.Module
{
    class DataAccess
    {
        //b1 tạo một kết nối 
        string connectString = "Data Source=DANGHIEN\\SQLEXPRESS;Initial Catalog=ShopThuCungBTL;Integrated Security=True";
        SqlConnection sqlconnect = null;

        void OpenConnect()
        {
            sqlconnect = new SqlConnection(connectString);  // truyền chuỗi kết nối vào connection mới khởi tạo
            if (sqlconnect.State != ConnectionState.Open)  //kiểm tra trạng thái 
            {
                sqlconnect.Open();
            }
        }

        void CloseConnect()
        {
            if (sqlconnect.State != ConnectionState.Closed)
            {
                sqlconnect.Close();
                sqlconnect.Dispose();

            }
        }
        // phương thức thực hiện cập nhập dữ liệu , update, insert, delete
        public void UpdateData(String sql)
        {
            SqlCommand sqlcomn = new SqlCommand();
            OpenConnect();
            sqlcomn.Connection = sqlconnect;  //đối tượng connection
            sqlcomn.CommandText = sql;//lệnh sql hay nội thủ tục
            sqlcomn.ExecuteNonQuery();  //phương thức dùng thực thi các phát biểu như insert, update,..
            CloseConnect();  //đóng kết nối 
            sqlcomn.Dispose();  //hủy đối tượng 
        }

        //phương thức thực hiện câu lệnh select trả về một dataTable 
        public DataTable DataReader(String sqlSelect)
        {
            DataTable dtResult = new DataTable();  //khởi tạo một datatable 
            OpenConnect();
            SqlDataAdapter sqlData = new SqlDataAdapter(sqlSelect, sqlconnect);  //lấy dữ liệu về cho ứng dụng 
            sqlData.Fill(dtResult);  //thực thi câu lệnh selectCommand và đổ dữ liệu vào đối tượng datatable
            sqlData.Dispose();
            CloseConnect();
            return dtResult;
        }

    }
}
