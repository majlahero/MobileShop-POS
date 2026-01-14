using HHTPRO;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HTTT.QUAN_LY
{
    public partial class F_DangNhap : Form
    {
        public F_DangNhap()
        {
            InitializeComponent();
            txtMatKhau.PasswordChar = '*';
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string matKhau = txtMatKhau.Text;
            string tenTaiKhoan = txtTenDangnhap.Text;

            using (var connection = new DatabaseConnection().GetConnection())
            {
                if (connection != null)
                {
                    string query = "SELECT VaiTro FROM TaiKhoan WHERE MaNhanVien = @MaNhanVien AND MatKhau = @MatKhau";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNhanVien", tenTaiKhoan);
                        command.Parameters.AddWithValue("@MatKhau", matKhau);

                        try
                        {
                            connection.Open();
                            var vaiTroObj = command.ExecuteScalar();

                            if (vaiTroObj != null)
                            {
                                MaNhanVien.Ma = tenTaiKhoan;

                                // Chuyển đổi kết quả sang kiểu bool
                                bool isQuanLy = false;
                                if (vaiTroObj is bool)
                                {
                                    isQuanLy = (bool)vaiTroObj;
                                }
                                else if (vaiTroObj is int)
                                {
                                    isQuanLy = Convert.ToInt32(vaiTroObj) == 1;
                                }

                                // Mở form tương ứng với vai trò
                                if (isQuanLy)
                                {
                                    DashBoard_QL menuQL = new DashBoard_QL();
                                    menuQL.Show();
                                }
                                else
                                {
                                    DashBoard_NhanVien menuNV = new DashBoard_NhanVien();
                                    menuNV.Show();
                                }

                                GhiLichSuDangNhap(MaNhanVien.Ma);
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng!");
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi thực hiện truy vấn: " + ex.Message);
                        }
                    }
                }
            }
        }
        private void GhiLichSuDangNhap(string maNhanVien)
        {
            string query = "INSERT INTO LichSuDangNhap (MaNhanVien, ThoiGianDangNhap) VALUES (@MaNhanVien, @ThoiGianDangNhap)";

            using (var connection = new DatabaseConnection().GetConnection())
            {
                connection.Open(); 

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    command.Parameters.AddWithValue("@ThoiGianDangNhap", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}