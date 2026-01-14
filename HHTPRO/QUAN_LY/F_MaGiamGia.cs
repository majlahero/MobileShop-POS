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

namespace HTTT.QUAN_LY
{
    public partial class F_MaGiamGia : Form
    {
        public F_MaGiamGia()
        {
            InitializeComponent();
            LoadKhuyenMaiData();
        }

        private void LoadKhuyenMaiData()
        {
            try
            {
                using (var connection = new DatabaseConnection().GetConnection())
                {
                    connection.Open();
                    string query = "SELECT MaKhuyenMai, SoTienGiam, SoLanSuDung FROM KhuyenMai";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView.DataSource = dt;
                    dataGridView.Columns["MaKhuyenMai"].HeaderText = "Mã Giảm Giá";
                    dataGridView.Columns["SoTienGiam"].HeaderText = "Giá Trị Giảm";
                    dataGridView.Columns["SoLanSuDung"].HeaderText = "Số Lần Sử Dụng";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaGiamGia.Text) ||
                string.IsNullOrWhiteSpace(txtGiaTri.Text) ||
                string.IsNullOrWhiteSpace(txtSoLanSuDung.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (!decimal.TryParse(txtGiaTri.Text, out decimal soTienGiam) ||
                !int.TryParse(txtSoLanSuDung.Text, out int soLanSuDung))
            {
                MessageBox.Show("Giá trị giảm và số lần sử dụng phải là số!");
                return;
            }

            try
            {
                using (var connection = new DatabaseConnection().GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO KhuyenMai (MaKhuyenMai, SoTienGiam, SoLanSuDung) " +
                                   "VALUES (@MaKhuyenMai, @SoTienGiam, @SoLanSuDung)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@MaKhuyenMai", txtMaGiamGia.Text.Trim());
                        cmd.Parameters.AddWithValue("@SoTienGiam", soTienGiam);
                        cmd.Parameters.AddWithValue("@SoLanSuDung", soLanSuDung);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Thêm khuyến mãi thành công!");
                            LoadKhuyenMaiData();
                            ClearFields();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Violation of primary key
                {
                    MessageBox.Show("Mã khuyến mãi đã tồn tại!");
                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm khuyến mãi: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // Sửa khuyến mãi
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi cần sửa!");
                return;
            }

            if (!decimal.TryParse(txtGiaTri.Text, out decimal soTienGiam) ||
                !int.TryParse(txtSoLanSuDung.Text, out int soLanSuDung))
            {
                MessageBox.Show("Giá trị giảm và số lần sử dụng phải là số!");
                return;
            }

            try
            {
                using (var connection = new DatabaseConnection().GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE KhuyenMai SET SoTienGiam = @SoTienGiam, SoLanSuDung = @SoLanSuDung " +
                                   "WHERE MaKhuyenMai = @MaKhuyenMai";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@MaKhuyenMai", txtMaGiamGia.Text.Trim());
                        cmd.Parameters.AddWithValue("@SoTienGiam", soTienGiam);
                        cmd.Parameters.AddWithValue("@SoLanSuDung", soLanSuDung);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Cập nhật khuyến mãi thành công!");
                            LoadKhuyenMaiData();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy khuyến mãi để cập nhật!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật khuyến mãi: " + ex.Message);
            }
        }

        // Xóa khuyến mãi
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khuyến mãi này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                using (var connection = new DatabaseConnection().GetConnection())
                {
                    connection.Open();
                    string query = "DELETE FROM KhuyenMai WHERE MaKhuyenMai = @MaKhuyenMai";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@MaKhuyenMai", txtMaGiamGia.Text.Trim());

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Xóa khuyến mãi thành công!");
                            LoadKhuyenMaiData();
                            ClearFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy khuyến mãi để xóa!");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Foreign key constraint
                {
                    MessageBox.Show("Không thể xóa vì khuyến mãi đã được sử dụng trong đơn hàng!");
                }
                else
                {
                    MessageBox.Show("Lỗi khi xóa khuyến mãi: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtMaGiamGia.Clear();
            txtGiaTri.Clear();
            txtSoLanSuDung.Clear();
        }

        // Gọi LoadKhuyenMaiData khi form load
        private void F_KhuyenMai_Load(object sender, EventArgs e)
        {
            LoadKhuyenMaiData();
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView.SelectedRows[0];
                txtMaGiamGia.Text = row.Cells["MaKhuyenMai"].Value.ToString();
                txtGiaTri.Text = row.Cells["SoTienGiam"].Value.ToString();
                txtSoLanSuDung.Text = row.Cells["SoLanSuDung"].Value.ToString();
            }
        }
    }
}
