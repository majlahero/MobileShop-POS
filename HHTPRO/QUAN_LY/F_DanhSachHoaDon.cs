using HTTT.BIEU_MAU;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HTTT.QUAN_LY
{
    public partial class F_DanhSachHoaDon : Form
    {
        public F_DanhSachHoaDon()
        {
            InitializeComponent();
            SetupDataGridView(); 
        }
        private void SetupDataGridView()
        {
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add("STT", "STT");
            dataGridView.Columns["STT"].Width = 50;
            dataGridView.Columns["STT"].ReadOnly = true; 

            dataGridView.Columns.Add("MaHoaDon", "Mã Hóa Đơn");
            dataGridView.Columns["MaHoaDon"].Width = 150;
            dataGridView.Columns["MaHoaDon"].ReadOnly = true; 

            dataGridView.Columns.Add("ThoiGian", "Thời Gian");
            dataGridView.Columns["ThoiGian"].Width = 200;
            dataGridView.Columns["ThoiGian"].ReadOnly = true; 

            dataGridView.CellDoubleClick += dataGridView_CellDoubleClick;
        }

        private void LoadData()
        {
            dataGridView.Rows.Clear();
            int stt = 1; // Luôn bắt đầu từ 1

            using (var connection = new DatabaseConnection().GetConnection())
            {
                connection.Open();
                string query = "SELECT MaDonHang, NgayDatHang FROM DonHang";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView.Rows.Add(stt++, reader.GetInt32(0), reader.GetDateTime(1));
                        }
                    }
                }
            }
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maHoaDon = dataGridView.Rows[e.RowIndex].Cells["MaHoaDon"].Value.ToString();

                F_InHoaDon fHoaDon = new F_InHoaDon(maHoaDon);
                fHoaDon.ShowDialog();
            }
        }

        private void F_DanhSachHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
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
                    string query = "SELECT MaDonHang, NgayDatHang FROM DonHang " +
                                  "WHERE NgayDatHang BETWEEN @StartDate AND @EndDate " +
                                  "ORDER BY NgayDatHang";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int maDonHang = reader.GetInt32(0);
                                DateTime ngayDatHang = reader.GetDateTime(1);

                                dataGridView.Rows.Add(dataGridView.Rows.Count + 1, maDonHang, ngayDatHang);
                            }
                        }
                    }
                }

                MessageBox.Show($"Đã lọc {dataGridView.Rows.Count} đơn hàng từ {startDate.ToShortDateString()} đến {endDate.ToShortDateString()}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message);
            }
        }
    }
}