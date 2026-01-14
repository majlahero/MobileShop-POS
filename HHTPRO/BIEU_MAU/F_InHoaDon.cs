using System;
using System.Data;
using System.Data.SqlClient; // Thêm namespace này
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Diagnostics.Internal;

namespace HTTT.BIEU_MAU
{
    public partial class F_InHoaDon : Form
    {
        private string maDonHang;

        public F_InHoaDon(string maDonHang) 
        {
            InitializeComponent();
            this.maDonHang = maDonHang;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = GetData(maDonHang); 

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HoaDon", dt)); // "DataSet1" là tên dataset trong RDLC
            this.reportViewer1.RefreshReport();
        }

        private DataTable GetData(string maDonHang)
        {
            DataTable dt = new DataTable();

            using (var connection = new DatabaseConnection().GetConnection())
            {
                string query = "SELECT * FROM vw_HoaDon WHERE MaDonHang = @MaDonHang";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    connection.Open();
                    adapter.Fill(dt); 
                }
            }

            return dt;
        }
    }
}