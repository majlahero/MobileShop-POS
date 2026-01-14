using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HTTT
{
    public partial class F_KhachHang : Form
    {
        public F_KhachHang()
        {
            InitializeComponent();
            LoadCustomers();
        }


        private void LoadCustomers()
        {
            try
            {
                using (SqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    string query = "SELECT * FROM KhachHang";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvCustomer.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new DatabaseConnection().GetConnection())
            {
                string query = @"INSERT INTO KhachHang (TenKhachHang, SoDienThoai, DiaChi, Email) 
                        VALUES (@TenKhachHang, @SoDienThoai, @DiaChi, @Email)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenKhachHang", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@SoDienThoai", txtPhone.Text);
                cmd.Parameters.AddWithValue("@DiaChi", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text); // Thêm Email

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                LoadCustomers();
                MessageBox.Show("Thêm khách hàng thành công!");
                ClearFields();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerID.Text)) return;

            using (SqlConnection conn = new DatabaseConnection().GetConnection())
            {
                string query = @"UPDATE KhachHang 
                        SET TenKhachHang = @TenKhachHang, 
                            SoDienThoai = @SoDienThoai, 
                            DiaChi = @DiaChi,
                            Email = @Email
                        WHERE MaKhachHang = @MaKhachHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKhachHang", txtCustomerID.Text);
                cmd.Parameters.AddWithValue("@TenKhachHang", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@SoDienThoai", txtPhone.Text);
                cmd.Parameters.AddWithValue("@DiaChi", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text); // Thêm Email

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                LoadCustomers();
                MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Không thay đổi vì không liên quan đến Email
            if (string.IsNullOrEmpty(txtCustomerID.Text)) return;

            using (SqlConnection conn = new DatabaseConnection().GetConnection())
            {
                string query = "DELETE FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKhachHang", txtCustomerID.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                LoadCustomers();
                MessageBox.Show("Xóa khách hàng thành công!");
                ClearFields();
            }
        }



        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];
                txtCustomerID.Text = row.Cells["MaKhachHang"].Value.ToString();
                txtCustomerName.Text = row.Cells["TenKhachHang"].Value.ToString();
                txtPhone.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtAddress.Text = row.Cells["DiaChi"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? ""; // Thêm Email
            }
        }

        private void dgvCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];
                txtCustomerID.Text = row.Cells["MaKhachHang"].Value.ToString();
                txtCustomerName.Text = row.Cells["TenKhachHang"].Value.ToString();
                txtPhone.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtAddress.Text = row.Cells["DiaChi"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? ""; // Thêm Email
            }
        }

        private void ClearFields()
        {
            txtCustomerID.Clear();
            txtCustomerName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtEmail.Clear(); // Thêm Email
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
