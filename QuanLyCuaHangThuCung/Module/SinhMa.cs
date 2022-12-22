using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace QuanLyCuaHangThuCung.Module
{
    class SinhMa
    {
        DataAccess database = new DataAccess();
        public string SinhMaTD(string TenBang, string Mabatdau, string TruongMa)
        {
            int id = 0;
            bool dung = false;
            string ma = "";
            DataTable dm = new DataTable();
            while (dung == false)
            {
                dm = database.DataReader("Select * from " + TenBang + " where " + TruongMa + " = '" + Mabatdau + id.ToString() + "'");
                if (dm.Rows.Count == 0)
                {
                    dung = true;
                }
                else
                {
                    id++;
                    dung = false;
                }
            }
            ma = Mabatdau + id.ToString();
            return ma;
        }



        //gán dữ liệu vào combobox
        public void FillCombo(ComboBox Combo, DataTable dtTable, string displayMember, string valueMember)
        {
            Combo.DataSource = dtTable;
            Combo.DisplayMember = displayMember;
            Combo.ValueMember = valueMember;
        }
    }
}