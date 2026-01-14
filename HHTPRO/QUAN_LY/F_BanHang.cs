//using HTTT.FORM_IN;
using HTTT.BIEU_MAU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HTTT.QUAN_LY
{
    public partial class F_BanHang : Form
    {
        public F_BanHang()
        {
            InitializeComponent();
        }

        private void Form_banhang_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MaNhanVien.Ma))
            {
                LoadNhanVienInfo();
            }
            else
            {
                MessageBox.Show("Mã nhân viên không hợp lệ.");
            }

            SetupDataGridView();
        }

        private void LoadNhanVienInfo()
        {
            using (var connection = new DatabaseConnection().GetConnection())
            {
                string query = "SELECT TenNhanVien, DiaChi, SoDienThoai, Email FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaNhanVien", MaNhanVien.Ma);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtTenNhanVien.Text = reader["TenNhanVien"].ToString();
                                txtDiaChiNV.Text = reader["DiaChi"].ToString();
                                txtSDTNV.Text = reader["SoDienThoai"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy thông tin nhân viên.");
                            }
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
                    }
                }
            }
        }

        private void TimKiemSanPham()
        {
            string tenSanPham = txtTimKiemSP.Text.Trim();
            lstSanPham.Items.Clear();

            if (!string.IsNullOrEmpty(tenSanPham))
            {
                using (var connection = new DatabaseConnection().GetConnection())
                {
                    string query = "SELECT TenSanPham FROM SanPham WHERE TenSanPham LIKE @TenSanPham AND TrangThai = 'Còn bán' AND SoLuong > 0";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TenSanPham", "%" + tenSanPham + "%");

                        try
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string tenSP = reader["TenSanPham"].ToString();
                                    lstSanPham.Items.Add(tenSP);
                                }
                            }

                            lstSanPham.Visible = lstSanPham.Items.Count > 0;
                        }
                        catch (SqlException sqlEx)
                        {
                            MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                lstSanPham.Visible = false;
            }
        }

        private void btnTimKiemSP_Click(object sender, EventArgs e)
        {
            TimKiemSanPham();
        }

        private void txtTimKiemSP_TextChanged(object sender, EventArgs e)
        {
            TimKiemSanPham();
        }

        private SanPham GetSanPhamByTen(string tenSanPham)
        {
            SanPham sanPham = null;

            using (var connection = new DatabaseConnection().GetConnection())
            {
                string query = "SELECT MaSanPham, TenSanPham, Gia FROM SanPham WHERE TenSanPham = @TenSanPham";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenSanPham", tenSanPham);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                sanPham = new SanPham
                                {
                                    MaSanPham = Convert.ToInt32(reader["MaSanPham"]),
                                    TenSanPham = reader["TenSanPham"].ToString(),
                                    Gia = Convert.ToDecimal(reader["Gia"])
                                };
                            }
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi lấy thông tin sản phẩm: " + ex.Message);
                    }
                }
            }

            return sanPham;
        }

        private void SetupDataGridView()
        {
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add("STT", "STT");
            dataGridView.Columns["STT"].Width = 50;
            dataGridView.Columns["STT"].ReadOnly = true; // Không cho phép sửa

            dataGridView.Columns.Add("TenSanPham", "Tên sản phẩm");
            dataGridView.Columns["TenSanPham"].Width = 400;
            dataGridView.Columns["TenSanPham"].ReadOnly = true; // Không cho phép sửa

            dataGridView.Columns.Add("GiaBan", "Giá bán");
            dataGridView.Columns["GiaBan"].Width = 120;
            dataGridView.Columns["GiaBan"].ReadOnly = true; // Không cho phép sửa

            dataGridView.Columns.Add("SoLuong", "Số lượng");
            dataGridView.Columns["SoLuong"].Width = 100; // Có thể sửa

            dataGridView.Columns.Add("ThanhTien", "Thành tiền");
            dataGridView.Columns["ThanhTien"].Width = 130;
            dataGridView.Columns["ThanhTien"].ReadOnly = true; // Không cho phép sửa

            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.Name = "Hành động";
            btnColumn.HeaderText = "Hành động";
            btnColumn.Text = "Xóa";
            btnColumn.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(btnColumn);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView.CellValueChanged += dataGridView_CellValueChanged;
            dataGridView.CellValidating += dataGridView_CellValidating;
        }
        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["SoLuong"].Index)
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out int soLuong) || soLuong < 0)
                {
                    e.Cancel = true;
                    return;
                }

                // Lấy tên sản phẩm từ ô tương ứng
                string tenSanPham = dataGridView.Rows[e.RowIndex].Cells["TenSanPham"].Value?.ToString();

                if (!string.IsNullOrEmpty(tenSanPham))
                {
                    // Kiểm tra số lượng trong cơ sở dữ liệu
                    int soLuongTrongDB = GetSoLuongSanPham(tenSanPham); // Hàm lấy số lượng từ database

                    if (soLuong > soLuongTrongDB)
                    {
                        MessageBox.Show($"Số lượng không được lớn hơn {soLuongTrongDB} cho sản phẩm {tenSanPham}.");
                        e.Cancel = true; // Hủy thao tác nhập
                    }
                }
            }
        }

        // Hàm để lấy số lượng sản phẩm từ database
        private int GetSoLuongSanPham(string tenSanPham)
        {
            using (var connection = new DatabaseConnection().GetConnection())
            {
                string query = "SELECT SoLuong FROM SanPham WHERE TenSanPham = @TenSanPham";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenSanPham", tenSanPham);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["SoLuong"].Index && e.RowIndex >= 0)
            {
                var row = dataGridView.Rows[e.RowIndex];
                int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                decimal giaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value);

                if (soLuong == 0)
                {
                    row.Cells["SoLuong"].Value = 1; 
                    soLuong = 1; 
                }

                row.Cells["ThanhTien"].Value = soLuong * giaBan;
                UpdateTongTien(); 
            }
        }

        private void UpdateThanhTien(DataGridViewRow row)
        {
            int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
            decimal giaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value);
            row.Cells["ThanhTien"].Value = soLuong * giaBan;
            UpdateTongTien();
        }
        private void btnTimKiemKH_Click(object sender, EventArgs e)
        {
            string sdt = txtTimKiemKH.Text.Trim();

            using (var connection = new DatabaseConnection().GetConnection())
            {
                connection.Open();
                string query = "SELECT TenKhachHang, DiaChi, SoDienThoai FROM KhachHang WHERE SoDienThoai = @sdt";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@sdt", sdt);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtTenKH.Text = reader["TenKhachHang"].ToString();
                            txtDiaChiKH.Text = reader["DiaChi"].ToString();
                            txtSDTKH.Text = reader["SoDienThoai"].ToString();
                        }
                        else
                        {
                            txtTenKH.Clear();
                            txtDiaChiKH.Clear();
                            txtSDTKH.Clear();
                            MessageBox.Show("Không tìm thấy khách hàng với số điện thoại này.");
                        }
                    }
                }
            }
        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["Hành động"].Index && e.RowIndex >= 0)
            {
                dataGridView.Rows.RemoveAt(e.RowIndex);
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    dataGridView.Rows[i].Cells["STT"].Value = i + 1;
                }
                UpdateTongTien();
            }
        }
        private void lstSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSanPham.SelectedItem is string tenSanPham)
            {
                var sanPham = GetSanPhamByTen(tenSanPham);

                if (sanPham != null)
                {
                    bool productExists = false;

                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (row.Cells["TenSanPham"].Value.ToString() == sanPham.TenSanPham)
                        {
                            int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                            row.Cells["SoLuong"].Value = soLuong + 1;
                            UpdateThanhTien(row);
                            productExists = true;
                            break;
                        }
                    }

                    if (!productExists)
                    {
                        int rowIndex = dataGridView.Rows.Add();
                        var row = dataGridView.Rows[rowIndex];
                        row.Cells["STT"].Value = rowIndex + 1;
                        row.Cells["TenSanPham"].Value = sanPham.TenSanPham;
                        row.Cells["GiaBan"].Value = sanPham.Gia;
                        row.Cells["SoLuong"].Value = 1;
                        row.Cells["ThanhTien"].Value = sanPham.Gia;
                    }

                    UpdateTongTien();
                    txtTimKiemSP.Clear();
                }
            }
        }

        private decimal GetSoTienGiam(string maKhuyenMai)
        {
            decimal soTienGiam = 0;

            using (var connection = new DatabaseConnection().GetConnection())
            {
                connection.Open();
                string query = "SELECT SoTienGiam FROM KhuyenMai WHERE MaKhuyenMai = @maKhuyenMai";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@maKhuyenMai", maKhuyenMai);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        soTienGiam = Convert.ToDecimal(result);
                    }
                }
            }

            return soTienGiam;
        }

        private void UpdateTongTien()
        {
            decimal tongTien = 0;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells["ThanhTien"].Value != null)
                {
                    tongTien += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                }
            }

            string maKhuyenMai = txtKhuyenMai.Text.Trim();
            decimal soTienGiam = GetSoTienGiam(maKhuyenMai);
            tongTien -= soTienGiam;

            if (tongTien < 0) tongTien = 0;

            btnTongTien.Text = $"{tongTien}";
        }


        private void btnApDung_Click(object sender, EventArgs e)
        {
            string maKhuyenMai = txtKhuyenMai.Text.Trim();
            decimal soTienGiam = GetSoTienGiam(maKhuyenMai);

            if (soTienGiam > 0)
            {
                MessageBox.Show($"Áp dụng mã khuyến mãi thành công! Giảm: {soTienGiam}");
                UpdateTongTien();
            }
            else
            {
                MessageBox.Show("Mã không hợp lệ!!");
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (dataGridView.Rows.Count == 0 || string.IsNullOrEmpty(cmboxThanhToan.SelectedItem?.ToString()))
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin đơn hàng và hình thức thanh toán!");
                return;
            }

            // Lấy thông tin thanh toán
            var paymentInfo = new
            {
                MaNhanVien = MaNhanVien.Ma,
                MaKhuyenMai = string.IsNullOrEmpty(txtKhuyenMai.Text) ? null : txtKhuyenMai.Text.Trim(),
                ThanhToan = cmboxThanhToan.SelectedItem.ToString(),
                MaKhachHang = GetKhachHangMaByPhone(txtSDTKH.Text.Trim())
            };

            // Validate mã khuyến mãi
            if (paymentInfo.MaKhuyenMai != null && !IsKhuyenMaiValid(paymentInfo.MaKhuyenMai))
            {
                MessageBox.Show("Mã khuyến mãi không hợp lệ!");
                return;
            }

            try
            {
                using (var connection = new DatabaseConnection().GetConnection())
                {
                    connection.Open();

                    // 1. Tạo đơn hàng
                    string maDonHang = CreateOrder(connection, paymentInfo);

                    if (string.IsNullOrEmpty(maDonHang))
                    {
                        MessageBox.Show("Lỗi khi tạo đơn hàng");
                        return;
                    }

                    // 2. Thêm chi tiết đơn hàng và cập nhật tồn kho
                    if (!ProcessOrderItems(connection, maDonHang))
                    {
                        // Nếu thêm chi tiết thất bại, xóa đơn hàng vừa tạo
                        DeleteOrder(connection, maDonHang);
                        MessageBox.Show("Lỗi khi thêm chi tiết đơn hàng");
                        return;
                    }

                    // 3. Hiển thị hóa đơn nếu thành công
                    new F_InHoaDon(maDonHang).Show();
                    dataGridView.Rows.Clear();
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi SQL: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo đơn hàng: {ex.Message}");
            }
        }

        private string CreateOrder(SqlConnection connection, dynamic paymentInfo)
        {
            string query = @"INSERT INTO DonHang (MaNhanVien, MaKhachHang, MaKhuyenMai, ThanhToan) 
                     OUTPUT INSERTED.MaDonHang 
                     VALUES (@MaNhanVien, @MaKhachHang, @MaKhuyenMai, @ThanhToan)";

            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.CommandTimeout = 30;
                cmd.Parameters.AddWithValue("@MaNhanVien", paymentInfo.MaNhanVien);
                cmd.Parameters.AddWithValue("@MaKhachHang", paymentInfo.MaKhachHang ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaKhuyenMai", paymentInfo.MaKhuyenMai ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ThanhToan", paymentInfo.ThanhToan);

                return cmd.ExecuteScalar()?.ToString();
            }
        }

        private bool ProcessOrderItems(SqlConnection connection, string maDonHang)
        {
            // Tạo DataTable để batch insert
            DataTable orderItems = new DataTable();
            orderItems.Columns.Add("MaDonHang", typeof(int));
            orderItems.Columns.Add("MaSanPham", typeof(int));
            orderItems.Columns.Add("SoLuong", typeof(int));

            // Danh sách sản phẩm cần cập nhật số lượng
            var productsToUpdate = new Dictionary<int, int>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells["TenSanPham"].Value == null) continue;

                string tenSanPham = row.Cells["TenSanPham"].Value.ToString();
                int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                int maSanPham = GetSanPhamMaByTen(tenSanPham);

                orderItems.Rows.Add(Convert.ToInt32(maDonHang), maSanPham, soLuong);
                productsToUpdate[maSanPham] = soLuong;
            }

            // Thêm chi tiết đơn hàng bằng BulkCopy
            using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, null))
            {
                bulkCopy.DestinationTableName = "ChiTietDonHang";
                bulkCopy.BatchSize = 100;
                bulkCopy.BulkCopyTimeout = 30;

                bulkCopy.ColumnMappings.Add("MaDonHang", "MaDonHang");
                bulkCopy.ColumnMappings.Add("MaSanPham", "MaSanPham");
                bulkCopy.ColumnMappings.Add("SoLuong", "SoLuong");

                bulkCopy.WriteToServer(orderItems);
            }

            // Cập nhật số lượng tồn kho
            foreach (var product in productsToUpdate)
            {
                string updateQuery = "UPDATE SanPham SET SoLuong = SoLuong - @SoLuong WHERE MaSanPham = @MaSanPham";

                using (var cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@SoLuong", product.Value);
                    cmd.Parameters.AddWithValue("@MaSanPham", product.Key);
                    cmd.ExecuteNonQuery();
                }
            }

            return true;
        }

        private void DeleteOrder(SqlConnection connection, string maDonHang)
        {
            try
            {
                string query = "DELETE FROM DonHang WHERE MaDonHang = @MaDonHang";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { /* Xử lý lỗi nếu cần */ }
        }


        private bool IsKhuyenMaiValid(string maKhuyenMai)
        {
            using (var connection = new DatabaseConnection().GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM KhuyenMai WHERE MaKhuyenMai = @MaKhuyenMai";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKhuyenMai", maKhuyenMai);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private int? GetKhachHangMaByPhone(string soDienThoai)
        {
            int? maKhachHang = null; 

            using (var connection = new DatabaseConnection().GetConnection())
            {
                string query = "SELECT MaKhachHang FROM KhachHang WHERE SoDienThoai = @SoDienThoai";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        maKhachHang = Convert.ToInt32(result);
                    }
                }
            }

            return maKhachHang;
        }

        private int GetSanPhamMaByTen(string tenSanPham)
        {
            int maSanPham = 0;

            using (var connection = new DatabaseConnection().GetConnection())
            {
                string query = "SELECT MaSanPham FROM SanPham WHERE TenSanPham = @TenSanPham";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenSanPham", tenSanPham);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        maSanPham = Convert.ToInt32(result);
                    }
                }
            }

            return maSanPham;
        }
    }


    public class SanPham
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public decimal Gia { get; set; }
    }
}