using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace HTTT.FORM_IN
{
    public partial class F_InPhieuHen : Form
    {
        private int maTraHang;

        // Truyền mã trả hàng từ ngoài vào
        public F_InPhieuHen(int maTraHang)
        {
            InitializeComponent();
            this.maTraHang = maTraHang;
        }

        private void F_InPhieuHen_Load(object sender, EventArgs e)
        {
            DataTable dt = GetData(maTraHang);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("PhieuHen", dt)); // "PhieuHen" là tên dataset trong RDLC
            this.reportViewer1.RefreshReport();
        }

        private DataTable GetData(int maTraHang)
        {
            DataTable dt = new DataTable();

            using (var connection = new DatabaseConnection().GetConnection())
            {
                string query = "SELECT * FROM vw_PhieuHen WHERE MaTraHang = @MaTraHang";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaTraHang", maTraHang);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    connection.Open();
                    adapter.Fill(dt);
                }
            }

            return dt;
        }
    }
}