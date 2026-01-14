using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace HTTT.FORM_IN
{
    public partial class F_InBaoHanh : Form
    {
        private int maBaoHanh;

        // Truyền mã bảo hành từ ngoài vào
        public F_InBaoHanh(int maBaoHanh)
        {
            InitializeComponent();
            this.maBaoHanh = maBaoHanh;
        }

        private void F_BaoHanh_Load(object sender, EventArgs e)
        {
            DataTable dt = GetData(maBaoHanh);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("BaoHanh", dt));
            this.reportViewer1.RefreshReport();
        }

        private DataTable GetData(int maBaoHanh)
        {
            DataTable dt = new DataTable();

            using (var connection = new DatabaseConnection().GetConnection())
            {
                string query = "SELECT * FROM vw_PhieuBaoHanh WHERE MaBaoHanh = @MaBaoHanh";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaBaoHanh", maBaoHanh);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    connection.Open();
                    adapter.Fill(dt);
                }
            }

            return dt;
        }
    }
}
