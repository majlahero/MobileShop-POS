using HTTT.FORM_IN;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HTTT.FORM_IN
{
    public partial class F_DanhSachPhieuNhap : Form
    {
        public F_DanhSachPhieuNhap()
        {
            InitializeComponent();
            this.Load += F_DanhSachPhieuNhap_Load; // Gắn event Form_Load
        }

        private void F_DanhSachPhieuNhap_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadData();
        }

        private void SetupDataGridView()
        {
            dataGridView.Columns.Clear();
            dataGridView.AutoGenerateColumns = false;
            dataGridView.AllowUserToAddRows = false;

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 50,
                ReadOnly = true
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaNhapHang",
                HeaderText = "Mã Nhập Hàng",
                Width = 150,
                ReadOnly = true
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayNhap",
                HeaderText = "Ngày Nhập",
                Width = 200,
                ReadOnly = true
            });

            dataGridView.CellDoubleClick -= dataGridView_CellDoubleClick; // Xóa nếu có cũ
            dataGridView.CellDoubleClick += dataGridView_CellDoubleClick;
        }

        private void LoadData()
        {
            dataGridView.Rows.Clear(); // Xóa dòng cũ

            using (var connection = new DatabaseConnection().GetConnection())
            {
                connection.Open();
                string query = "SELECT MaNhapHang, NgayNhap FROM PhieuNhapHang ORDER BY MaNhapHang DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int stt = 1;
                        while (reader.Read())
                        {
                            int maNhapHang = reader.GetInt32(0);
                            DateTime ngayNhap = reader.GetDateTime(1);

                            dataGridView.Rows.Add(stt++, maNhapHang, ngayNhap.ToString("dd/MM/yyyy HH:mm"));
                        }
                    }
                }
            }
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maNhapHang = dataGridView.Rows[e.RowIndex].Cells["MaNhapHang"].Value.ToString();

                var form = new F_InPhieuNhapHang(maNhapHang);
                form.ShowDialog();
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

            if (startDate > endDate)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc!");
                return;
            }

            dataGridView.Rows.Clear();

            try
            {
                using (var connection = new DatabaseConnection().GetConnection())
                {
                    connection.Open();
                    string query = "SELECT MaNhapHang, NgayNhap FROM PhieuNhapHang " +
                                    "WHERE NgayNhap BETWEEN @StartDate AND @EndDate " +
                                    "ORDER BY NgayNhap DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int stt = 1;
                            while (reader.Read())
                            {
                                int maNhapHang = reader.GetInt32(0);
                                DateTime ngayNhap = reader.GetDateTime(1);

                                dataGridView.Rows.Add(stt++, maNhapHang, ngayNhap.ToString("dd/MM/yyyy HH:mm"));
                            }
                        }
                    }
                }

                MessageBox.Show($"Đã lọc {dataGridView.Rows.Count} phiếu nhập hàng từ {startDate:dd/MM/yyyy} đến {endDate:dd/MM/yyyy}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message);
            }
        }
    }
}
