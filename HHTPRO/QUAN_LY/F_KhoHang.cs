using HTTT;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CuaHangDiDong
{
    public partial class Form_KhoHang : Form
    {
        private DataTable khoHangTable;
        private int selectedMaSP = -1;

        public Form_KhoHang()
        {
            InitializeComponent();
            InitKhoHangTable();
            LoadProductsFromDatabase();
            dataGridViewKho.SelectionChanged += DataGridViewKho_SelectionChanged;
        }

        private void InitKhoHangTable()
        {
            khoHangTable = new DataTable();
            khoHangTable.Columns.Add("Mã SP", typeof(int));
            khoHangTable.Columns.Add("Tên SP");
            khoHangTable.Columns.Add("Số lượng tồn", typeof(int));
            khoHangTable.Columns.Add("Đơn giá", typeof(decimal));
            khoHangTable.Columns.Add("Trạng thái");
            dataGridViewKho.DataSource = khoHangTable;
        }

        private void LoadProductsFromDatabase()
        {
            var dt = new DataTable();
            using (SqlConnection conn = new DatabaseConnection().GetConnection())
            {
                using (SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT MaSanPham, TenSanPham, Gia, TrangThai FROM SanPham", conn))
                {
                    da.Fill(dt);
                }
            }
            khoHangTable.Clear();
            foreach (DataRow r in dt.Rows)
            {
                int ma = Convert.ToInt32(r["MaSanPham"]);
                khoHangTable.Rows.Add(
                    ma,
                    r["TenSanPham"].ToString(),
                    GetCurrentStock(ma),
                    Convert.ToDecimal(r["Gia"]),
                    r["TrangThai"].ToString()
                );
            }
            selectedMaSP = -1;
            if (dataGridViewKho.Rows.Count > 0)
                dataGridViewKho.ClearSelection(); 
        }

        private int GetCurrentStock(int maSP)
        {
            int inQty = 0, outQty = 0;
            using (SqlConnection conn = new DatabaseConnection().GetConnection())
            {
                conn.Open();
                using (SqlCommand cmdIn = new SqlCommand(
                    "SELECT ISNULL(SUM(SoLuong),0) FROM ChiTietPhieuNhapHang WHERE MaSanPham=@ma", conn))
                {
                    cmdIn.Parameters.AddWithValue("@ma", maSP);
                    inQty = Convert.ToInt32(cmdIn.ExecuteScalar());
                }
                using (SqlCommand cmdOut = new SqlCommand(
                    "SELECT ISNULL(SUM(SoLuong),0) FROM ChiTietDonHang WHERE MaSanPham=@ma", conn))
                {
                    cmdOut.Parameters.AddWithValue("@ma", maSP);
                    outQty = Convert.ToInt32(cmdOut.ExecuteScalar());
                }
            }
            return inQty - outQty;
        }

        private void DataGridViewKho_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewKho.SelectedRows.Count == 0)
            {
                selectedMaSP = -1;
                return;
            }

            var row = dataGridViewKho.SelectedRows[0];
            if (row != null)
            {
                selectedMaSP = Convert.ToInt32(row.Cells["Mã SP"].Value);
                textBox_tenSP.Text = row.Cells["Tên SP"].Value.ToString();
                textBox_donGia.Text = row.Cells["Đơn giá"].Value.ToString();
                labelSoLuong.Text = "Số lượng tồn hiện tại: " + GetCurrentStock(selectedMaSP);
            }
        }

        private void ButtonThemSP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_tenSP.Text) ||
                !decimal.TryParse(textBox_donGia.Text, out decimal gia))
            {
                MessageBox.Show("Nhập thiếu hoặc sai định dạng.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new DatabaseConnection().GetConnection())
            {
                conn.Open();

                bool exists = false;
                using (SqlCommand chk = new SqlCommand(
                    "SELECT COUNT(*) FROM SanPham WHERE TenSanPham = @ten", conn))
                {
                    chk.Parameters.AddWithValue("@ten", textBox_tenSP.Text.Trim());
                    exists = ((int)chk.ExecuteScalar() > 0);
                }

                if (!exists)
                {
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO SanPham(TenSanPham, SoLuong, Gia, TrangThai) VALUES(@ten, 0, @gia, N'Còn bán'); SELECT SCOPE_IDENTITY();", conn))
                    {
                        cmd.Parameters.AddWithValue("@ten", textBox_tenSP.Text.Trim());
                        cmd.Parameters.AddWithValue("@gia", gia);
                        int newMaSP = Convert.ToInt32(cmd.ExecuteScalar());

                        MessageBox.Show("Thêm sản phẩm mới thành công! Mã SP: " + newMaSP,
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    using (SqlCommand updateCmd = new SqlCommand(
                        "UPDATE SanPham SET Gia = @gia, TrangThai = N'Còn bán' WHERE TenSanPham = @ten", conn))
                    {
                        updateCmd.Parameters.AddWithValue("@ten", textBox_tenSP.Text.Trim());
                        updateCmd.Parameters.AddWithValue("@gia", gia);
                        updateCmd.ExecuteNonQuery();

                        MessageBox.Show("Sản phẩm đã tồn tại. Thông tin đã được cập nhật.",
                            "Cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            LoadProductsFromDatabase();
        }

        private void ButtonStopSP_Click(object sender, EventArgs e)
        {
            if (selectedMaSP < 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm trước.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new DatabaseConnection().GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE SanPham SET TrangThai=N'Ngừng kinh doanh' WHERE MaSanPham=@ma", conn))
                {
                    cmd.Parameters.AddWithValue("@ma", selectedMaSP);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadProductsFromDatabase();
        }
    }
}
