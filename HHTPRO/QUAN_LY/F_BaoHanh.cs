using HTTT.FORM_IN;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace HTTT.QUAN_LY
{
    public partial class F_BaoHanh : Form
    {
        private DatabaseConnection dbConnection;

        private Form previousForm;

        // Constructor nhận form trước đó
        public F_BaoHanh(Form previousForm)
        {
            InitializeComponent();
            this.previousForm = previousForm;
            dbConnection = new DatabaseConnection();
        }

        public F_BaoHanh()
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
        }

        private void F_BaoHanh_Load(object sender, EventArgs e)
        {
            LoadInvoiceData();
            //LoadProductData();
            //LoadCustomerData();
            dtpAppointmentDate.Value = DateTime.Now.AddDays(7);
            nudWarrantyPeriod.Value = 12;
            dgvAppointmentInfo.AutoGenerateColumns = true;
            dgvAppointmentInfo.AllowUserToAddRows = false;
            dgvAppointmentInfo.ReadOnly = true;
            dgvAppointmentInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cboInvoiceId.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboInvoiceId.AutoCompleteSource = AutoCompleteSource.ListItems;
        }


        private void LoadInvoiceData()
        {
            try
            {
                cboInvoiceId.Items.Clear(); // Xóa các item cũ

                using (var connection = dbConnection.GetConnection())
                {
                    connection.Open();

                    string query = @"
                SELECT MaDonHang 
                FROM DonHang
                ORDER BY MaDonHang DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string maDonHang = reader["MaDonHang"].ToString();
                            cboInvoiceId.Items.Add(maDonHang);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void btnCheckWarranty_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = dbConnection.GetConnection())
                {
                    connection.Open();

                    string query;
                    SqlCommand command;

                    if (string.IsNullOrWhiteSpace(txtWarrantyId.Text))
                    {
                        // Trường hợp không nhập mã -> hiển thị tất cả phiếu bảo hành
                        query = @"SELECT pb.MaBaoHanh,
                         sp.MaSanPham AS [Mã sản phẩm],
                         sp.TenSanPham AS [Tên sản phẩm],
                         FORMAT(pb.NgayMua, 'dd/MM/yyyy') AS [Ngày mua],
                         FORMAT(pb.NgayHetHan, 'dd/MM/yyyy') AS [Ngày hết hạn],
                         dh.MaDonHang AS [Mã hóa đơn],
                         kh.TenKhachHang AS [Khách hàng],
                         kh.SoDienThoai AS [Điện thoại]
                  FROM PhieuBaoHanh pb
                  JOIN DonHang dh ON pb.MaDonHang = dh.MaDonHang
                  JOIN KhachHang kh ON dh.MaKhachHang = kh.MaKhachHang
                  JOIN SanPham sp ON pb.TenSanPham = sp.TenSanPham
                  ORDER BY pb.MaBaoHanh DESC";
                        command = new SqlCommand(query, connection);
                    }
                    else
                    {
                        // Trường hợp có nhập mã -> tìm theo mã
                        query = @"SELECT pb.MaBaoHanh,
                         sp.MaSanPham AS [Mã sản phẩm],
                         sp.TenSanPham AS [Tên sản phẩm],
                         FORMAT(pb.NgayMua, 'dd/MM/yyyy') AS [Ngày mua],
                         FORMAT(pb.NgayHetHan, 'dd/MM/yyyy') AS [Ngày hết hạn],
                         dh.MaDonHang AS [Mã hóa đơn],
                         kh.TenKhachHang AS [Khách hàng],
                         kh.SoDienThoai AS [Điện thoại]
                  FROM PhieuBaoHanh pb
                  JOIN DonHang dh ON pb.MaDonHang = dh.MaDonHang
                  JOIN KhachHang kh ON dh.MaKhachHang = kh.MaKhachHang
                  JOIN SanPham sp ON pb.TenSanPham = sp.TenSanPham
                  WHERE pb.MaBaoHanh = @MaBaoHanh";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@MaBaoHanh", txtWarrantyId.Text.Trim());
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dgvWarrantyInfo.DataSource = dt;
                        dgvWarrantyInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phiếu bảo hành", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvWarrantyInfo.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnSaveAppointment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppProductId.Text) ||
                string.IsNullOrEmpty(txtAppWarrantyId.Text) ||
                string.IsNullOrEmpty(txtAppCustomerInfo.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = dbConnection.GetConnection())
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM PhieuTraHang WHERE MaBaoHanh = @MaBaoHanh";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@MaBaoHanh", txtAppWarrantyId.Text);

                    int existingCount = (int)checkCommand.ExecuteScalar();
                    if (existingCount > 0)
                    {
                        MessageBox.Show("Mã bảo hành này đã có phiếu hẹn trả hàng!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    string insertQuery = @"INSERT INTO PhieuTraHang 
                           (MaBaoHanh, NgayNhan, NgayTra)
                           OUTPUT INSERTED.MaTraHang
                           VALUES (@MaBaoHanh, @NgayNhan, @NgayTra)";

                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@MaBaoHanh", txtAppWarrantyId.Text);
                    insertCommand.Parameters.AddWithValue("@NgayNhan", DateTime.Now);
                    insertCommand.Parameters.AddWithValue("@NgayTra", dtpAppointmentDate.Value);

                    int maPhieuHen = Convert.ToInt32(insertCommand.ExecuteScalar());

                    if (maPhieuHen > 0)
                    {
                        MessageBox.Show("Đã tạo phiếu hẹn trả hàng thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        F_InPhieuHen frm = new F_InPhieuHen(maPhieuHen);
                        frm.Show();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu hẹn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // 1. Method cho KIỂM TRA PHIẾU HẸN TRẢ HÀNG (trong tab Kiểm tra phiếu hẹn)
        private void btnCheckAppointment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppointmentId.Text))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu hẹn", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = dbConnection.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT pt.MaTraHang AS [Mã phiếu hẹn], 
                    FORMAT(pt.NgayNhan, 'dd/MM/yyyy') AS [Ngày nhận],
                    FORMAT(pt.NgayTra, 'dd/MM/yyyy') AS [Ngày hẹn trả],
                    pt.TrangThai AS [Trạng thái],
                    pb.TenSanPham AS [Tên sản phẩm], 
                    pb.MaBaoHanh AS [Mã bảo hành]
                    FROM PhieuTraHang pt
                    JOIN PhieuBaoHanh pb ON pt.MaBaoHanh = pb.MaBaoHanh
                    WHERE pt.MaTraHang = @MaTraHang";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaTraHang", txtAppointmentId.Text);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy mã phiếu hẹn tương ứng.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvAppointmentInfo.DataSource = null; // Xóa kết quả cũ nếu có
                    }
                    else
                    {
                        dgvAppointmentInfo.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckReturn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReturnAppointmentId.Text))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu hẹn trả", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = dbConnection.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT 
                            pt.MaTraHang AS [Mã phiếu trả],
                            pb.TenSanPham AS [Sản phẩm],
                            pt.TrangThai AS [Trạng thái],
                            FORMAT(pt.NgayNhan, 'dd/MM/yyyy') AS [Ngày nhận],
                            FORMAT(pt.NgayTra, 'dd/MM/yyyy') AS [Ngày hẹn trả]
                            FROM PhieuTraHang pt
                            JOIN PhieuBaoHanh pb ON pt.MaBaoHanh = pb.MaBaoHanh
                            WHERE pt.MaTraHang = @MaTraHang";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaTraHang", txtReturnAppointmentId.Text);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvReturnInfo.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirmReturn_Click(object sender, EventArgs e)
        {
            if (dgvReturnInfo.Rows.Count == 0 || dgvReturnInfo.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng kiểm tra thông tin phiếu hẹn trước",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maTraHang = Convert.ToInt32(dgvReturnInfo.CurrentRow.Cells["Mã phiếu trả"].Value);
            string trangThai = dgvReturnInfo.CurrentRow.Cells["Trạng thái"].Value.ToString();

            if (trangThai.Equals("Đã trả hàng", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Sản phẩm này đã được trả rồi!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Xác nhận đã trả sản phẩm cho khách hàng?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var connection = dbConnection.GetConnection())
                    {
                        connection.Open();

                        // Kiểm tra lại trạng thái (đề phòng dữ liệu bị thay đổi giữa lúc kiểm tra và xác nhận)
                        string checkQuery = "SELECT TrangThai FROM PhieuTraHang WHERE MaTraHang = @MaTraHang";
                        SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                        checkCommand.Parameters.AddWithValue("@MaTraHang", maTraHang);
                        string currentStatus = checkCommand.ExecuteScalar()?.ToString();

                        if (currentStatus != null && currentStatus.Equals("Đã trả hàng", StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show("Sản phẩm này đã được trả rồi!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // Cập nhật trạng thái
                        string updateQuery = @"UPDATE PhieuTraHang 
                                    SET TrangThai = N'Đã trả hàng'
                                    WHERE MaTraHang = @MaTraHang";

                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@MaTraHang", maTraHang);

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Trả hàng thành công!",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Làm mới dữ liệu hiển thị
                            btnCheckReturn_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy phiếu trả hàng để cập nhật",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xác nhận trả hàng: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveWarranty_Click(object sender, EventArgs e)
        {
            if (cboProductInfo.SelectedIndex == -1 ||
                cboCustomerInfo.SelectedIndex == -1 ||
                cboInvoiceId.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string productName = cboProductInfo.SelectedItem.ToString();
                int customerId = GetIdFromComboBoxItem(cboCustomerInfo.SelectedItem.ToString());
                string maDonHang = cboInvoiceId.SelectedItem.ToString();

                using (var connection = dbConnection.GetConnection())
                {
                    connection.Open();

                    // 1. Kiểm tra phiếu bảo hành đã tồn tại
                    string checkWarrantyQuery = @"
SELECT COUNT(*) 
FROM vw_PhieuBaoHanh 
WHERE MaDonHang = @MaDonHang AND TenSanPham = @TenSanPham";

                    SqlCommand checkWarrantyCommand = new SqlCommand(checkWarrantyQuery, connection);
                    checkWarrantyCommand.Parameters.AddWithValue("@MaDonHang", maDonHang);
                    checkWarrantyCommand.Parameters.AddWithValue("@TenSanPham", productName);

                    int warrantyExists = Convert.ToInt32(checkWarrantyCommand.ExecuteScalar());

                    if (warrantyExists > 0)
                    {
                        MessageBox.Show("Sản phẩm này đã có phiếu bảo hành trong đơn hàng này", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 2. Kiểm tra sản phẩm có trong đơn hàng
                    string checkProductQuery = @"
SELECT COUNT(*) 
FROM ChiTietDonHang ct
JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
WHERE ct.MaDonHang = @MaDonHang AND sp.TenSanPham = @TenSanPham";

                    SqlCommand checkProductCommand = new SqlCommand(checkProductQuery, connection);
                    checkProductCommand.Parameters.AddWithValue("@MaDonHang", maDonHang);
                    checkProductCommand.Parameters.AddWithValue("@TenSanPham", productName);

                    int productExists = Convert.ToInt32(checkProductCommand.ExecuteScalar());

                    if (productExists == 0)
                    {
                        MessageBox.Show("Sản phẩm không có trong đơn hàng này", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 3. Tạo phiếu bảo hành và lấy MaBaoHanh vừa tạo
                    string warrantyQuery = @"
INSERT INTO PhieuBaoHanh 
(TenSanPham, MaDonHang, NgayMua, NgayHetHan)
OUTPUT INSERTED.MaBaoHanh
SELECT 
    sp.TenSanPham,
    @MaDonHang,
    dh.NgayDatHang,
    DATEADD(MONTH, @WarrantyPeriod, dh.NgayDatHang)
FROM DonHang dh
JOIN ChiTietDonHang ct ON dh.MaDonHang = ct.MaDonHang
JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
WHERE dh.MaDonHang = @MaDonHang AND sp.TenSanPham = @TenSanPham";

                    SqlCommand warrantyCommand = new SqlCommand(warrantyQuery, connection);
                    warrantyCommand.Parameters.AddWithValue("@MaDonHang", maDonHang);
                    warrantyCommand.Parameters.AddWithValue("@TenSanPham", productName);
                    warrantyCommand.Parameters.AddWithValue("@WarrantyPeriod", nudWarrantyPeriod.Value);

                    // Thực thi và lấy MaBaoHanh vừa tạo
                    int maBaoHanh = Convert.ToInt32(warrantyCommand.ExecuteScalar());

                    if (maBaoHanh > 0)
                    {
                        MessageBox.Show("Đã tạo phiếu bảo hành thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Reset form
                        cboProductInfo.SelectedIndex = -1;
                        cboCustomerInfo.SelectedIndex = -1;
                        cboInvoiceId.SelectedIndex = -1;
                        nudWarrantyPeriod.Value = 12;

                        // Làm mới danh sách hóa đơn
                        LoadInvoiceData();

                        // Mở form in phiếu bảo hành với mã vừa tạo
                        F_InBaoHanh frm = new F_InBaoHanh(maBaoHanh);
                        frm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Không thể tạo phiếu bảo hành", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo phiếu bảo hành: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private int GetIdFromComboBoxItem(string item)
        {
            // Lấy ID từ chuỗi combo box (ví dụ: "Laptop Dell - Mã: 1" -> trả về 1)
            string[] parts = item.Split(new[] { "Mã:" }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 1)
            {
                return int.Parse(parts[1].Trim());
            }
            return -1;
        }

        private void cboInvoiceId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInvoiceId.SelectedItem == null) return;

            string selectedMaDonHang = cboInvoiceId.SelectedItem.ToString();
            LoadCustomerByInvoiceId(selectedMaDonHang);
            LoadProductsByInvoiceId(selectedMaDonHang);
        }
        private void LoadCustomerByInvoiceId(string maDonHang)
        {
            try
            {
                using (var connection = dbConnection.GetConnection())
                {
                    connection.Open();
                    string query = @"
                SELECT kh.MaKhachHang, kh.TenKhachHang, kh.SoDienThoai
                FROM DonHang dh
                JOIN KhachHang kh ON dh.MaKhachHang = kh.MaKhachHang
                WHERE dh.MaDonHang = @MaDonHang";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaDonHang", maDonHang);

                    SqlDataReader reader = command.ExecuteReader();
                    cboCustomerInfo.Items.Clear();

                    if (reader.Read())
                    {
                        string display = $"{reader["TenKhachHang"]} - ĐT: {reader["SoDienThoai"]} - Mã: {reader["MaKhachHang"]}";
                        cboCustomerInfo.Items.Add(display);
                        cboCustomerInfo.SelectedIndex = 0; // tự động chọn
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin khách hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadProductsByInvoiceId(string maDonHang)
        {
            try
            {
                using (var connection = dbConnection.GetConnection())
                {
                    connection.Open();
                    string query = @"
                SELECT sp.TenSanPham
                FROM ChiTietDonHang ct
                JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
                WHERE ct.MaDonHang = @MaDonHang";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaDonHang", maDonHang);

                    SqlDataReader reader = command.ExecuteReader();
                    cboProductInfo.Items.Clear();

                    while (reader.Read())
                    {
                        cboProductInfo.Items.Add(reader["TenSanPham"].ToString());
                    }

                    if (cboProductInfo.Items.Count > 0)
                        cboProductInfo.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải sản phẩm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtAppWarrantyId_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvWarrantyInfo_DoubleClick(object sender, EventArgs e)
        {
            if (dgvWarrantyInfo.CurrentRow != null && dgvWarrantyInfo.CurrentRow.Index >= 0)
            {
                try
                {
                    int maBaoHanh = Convert.ToInt32(dgvWarrantyInfo.CurrentRow.Cells["MaBaoHanh"].Value);

                    F_InBaoHanh frm = new F_InBaoHanh(maBaoHanh);

                    frm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể mở thông tin bảo hành: " + ex.Message);
                }
            }
        }

        private void txtAppWarrantyId_TabIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppWarrantyId.Text))
            {
                
                return;
            }

            try
            {
                using (var connection = dbConnection.GetConnection())
                {
                    connection.Open();

                    string query = @"
                    SELECT 
                        kh.TenKhachHang,
                        sp.MaSanPham,
                        sp.TenSanPham
                    FROM PhieuBaoHanh pb
                    JOIN DonHang dh ON pb.MaDonHang = dh.MaDonHang
                    JOIN KhachHang kh ON dh.MaKhachHang = kh.MaKhachHang
                    JOIN ChiTietDonHang ct ON dh.MaDonHang = ct.MaDonHang
                    JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
                    WHERE pb.MaBaoHanh = @MaBaoHanh AND sp.TenSanPham = pb.TenSanPham";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaBaoHanh", txtAppWarrantyId.Text);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtAppCustomerInfo.Text = reader["TenKhachHang"].ToString();
                        txtAppProductId.Text = reader["MaSanPham"].ToString();
                        txtProductName.Text = reader["TenSanPham"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin khách hàng hoặc sản phẩm với mã bảo hành đã nhập.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAppCustomerInfo.Clear();
                        txtAppProductId.Clear();
                        txtProductName.Clear();
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}