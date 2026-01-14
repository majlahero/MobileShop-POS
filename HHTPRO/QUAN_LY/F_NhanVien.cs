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

namespace HTTT
{
    public partial class F_NhanVien : Form
    {
        public F_NhanVien()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            try
            {
                using (SqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    string query = "SELECT MaNhanVien, TenNhanVien, SoDienThoai, Email, DiaChi FROM NhanVien WHERE TrangThaiNhanVien = 1";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvEmployee.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhân viên: " + ex.Message);
            }
        }



        private void ClearFields()
        {
            txtEmployeeID.Clear();
            txtEmployeeName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtEmployeeName.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtEmployeeName.Text == "" || txtPhone.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên.");
                return;
            }

            try
            {
                using (SqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    conn.Open();

                    // Kiểm tra trùng mã nhân viên (nếu có nhập thủ công)
                    if (!string.IsNullOrEmpty(txtEmployeeID.Text))
                    {
                        string checkIdQuery = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien = @Ma";
                        SqlCommand checkIdCmd = new SqlCommand(checkIdQuery, conn);
                        checkIdCmd.Parameters.AddWithValue("@Ma", txtEmployeeID.Text);
                        int idExists = (int)checkIdCmd.ExecuteScalar();

                        if (idExists > 0)
                        {
                            MessageBox.Show("Mã nhân viên đã tồn tại. Vui lòng chọn mã khác hoặc để trống.");
                            return;
                        }
                    }

                    // Kiểm tra trùng số điện thoại
                    string checkPhoneQuery = "SELECT COUNT(*) FROM NhanVien WHERE SoDienThoai = @SDT";
                    SqlCommand checkPhoneCmd = new SqlCommand(checkPhoneQuery, conn);
                    checkPhoneCmd.Parameters.AddWithValue("@SDT", txtPhone.Text);
                    int phoneExists = (int)checkPhoneCmd.ExecuteScalar();

                    if (phoneExists > 0)
                    {
                        MessageBox.Show("Số điện thoại đã tồn tại. Vui lòng nhập số khác.");
                        return;
                    }

                    // Thêm nhân viên
                    string query;
                    SqlCommand cmd;

                    // Nếu có nhập ID
                    if (!string.IsNullOrEmpty(txtEmployeeID.Text))
                    {
                        query = "INSERT INTO NhanVien (MaNhanVien, TenNhanVien, SoDienThoai, Email, DiaChi) VALUES (@Ma, @Ten, @SDT, @Email, @DiaChi)";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Ma", txtEmployeeID.Text);
                    }
                    else
                    {
                        query = "INSERT INTO NhanVien (TenNhanVien, SoDienThoai, Email, DiaChi) VALUES (@Ten, @SDT, @Email, @DiaChi)";
                        cmd = new SqlCommand(query, conn);
                    }

                    cmd.Parameters.AddWithValue("@Ten", txtEmployeeName.Text);
                    cmd.Parameters.AddWithValue("@SDT", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtAddress.Text);

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    LoadEmployees();
                    MessageBox.Show("Thêm nhân viên thành công.");
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message);
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmployeeID.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa.");
                return;
            }

            try
            {
                using (SqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    string query = "UPDATE NhanVien SET TenNhanVien=@Ten, SoDienThoai=@SDT, Email=@Email, DiaChi=@DiaChi WHERE MaNhanVien=@Ma";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Ma", txtEmployeeID.Text);
                    cmd.Parameters.AddWithValue("@Ten", txtEmployeeName.Text);
                    cmd.Parameters.AddWithValue("@SDT", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtAddress.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    LoadEmployees();
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công.");
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật nhân viên: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmployeeID.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xoá", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xoá nhân viên này?", "Xác nhận xoá",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new DatabaseConnection().GetConnection())
                    {
                        conn.Open();

                        using (SqlTransaction transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                string updateEmployeeQuery = @"
                                UPDATE NhanVien 
                                SET TrangThaiNhanVien = 0 
                                WHERE MaNhanVien = @MaNhanVien";

                                using (SqlCommand updateEmployeeCmd = new SqlCommand(updateEmployeeQuery, conn, transaction))
                                {
                                    updateEmployeeCmd.Parameters.AddWithValue("@MaNhanVien", txtEmployeeID.Text);
                                    int employeeUpdated = updateEmployeeCmd.ExecuteNonQuery();

                                    if (employeeUpdated == 0)
                                    {
                                        throw new Exception("Không tìm thấy nhân viên để cập nhật");
                                    }
                                }

                                // Commit transaction nếu mọi thứ thành công
                                transaction.Commit();

                                // Làm mới danh sách và thông báo
                                LoadEmployees();
                                MessageBox.Show("Đã vô hiệu hóa nhân viên và xóa tài khoản thành công",
                                              "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearFields();
                            }
                            catch (Exception ex)
                            {
                                // Rollback nếu có lỗi
                                transaction.Rollback();
                                throw; // Ném lại exception để xử lý ở catch bên ngoài
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"Lỗi SQL khi xử lý nhân viên: {sqlEx.Message}",
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xử lý nhân viên: {ex.Message}",
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvEmployee.Rows[e.RowIndex];
                txtEmployeeID.Text = row.Cells["MaNhanVien"].Value.ToString();
                txtEmployeeName.Text = row.Cells["TenNhanVien"].Value.ToString();
                txtPhone.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtAddress.Text = row.Cells["DiaChi"].Value.ToString();
            }
        }
        private void dgvEmployee_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvEmployee.Rows[e.RowIndex];
                txtEmployeeID.Text = row.Cells["MaNhanVien"].Value.ToString();
                txtEmployeeName.Text = row.Cells["TenNhanVien"].Value.ToString();
                txtPhone.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtAddress.Text = row.Cells["DiaChi"].Value.ToString();
            }
        }

        private void ManageEmployee_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            F_QLDangNhap loginHistory = new F_QLDangNhap();
            loginHistory.ShowDialog();
        }
    }
}