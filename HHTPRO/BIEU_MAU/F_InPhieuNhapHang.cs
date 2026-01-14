using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTTT.FORM_IN
{
    public partial class F_InPhieuNhapHang : Form
    {
        private string maNhapHang;

        public F_InPhieuNhapHang(string maNhapHang)
        {
            InitializeComponent();
            this.maNhapHang = maNhapHang;
        }

        private void PhieuNhapHang_Load(object sender, EventArgs e)
        {
            DataTable dt = GetData(maNhapHang);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("PhieuNhapHang", dt)); // "DataSet1" là tên dataset trong RDLC
            this.reportViewer1.RefreshReport();
        }

        private DataTable GetData(string maNhapHang)
        {
            DataTable dt = new DataTable();

            using (var connection = new DatabaseConnection().GetConnection())
            {
                string query = "SELECT * FROM vw_PhieuNhapHang WHERE MaNhapHang = @MaNhapHang";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaNhapHang", maNhapHang);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    connection.Open();
                    adapter.Fill(dt);
                }
            }

            return dt;
        }
    }
}
