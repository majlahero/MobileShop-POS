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
    public partial class F_QLDangNhap : Form
    {
        public F_QLDangNhap()
        {
            InitializeComponent();
        }

        private void LoadLoginHistory()
        {
            try
            {
                using (SqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    string query = @"
                SELECT 
                    lsdn.MaNhanVien,
                    nv.TenNhanVien,
                    lsdn.ThoiGianDangNhap,
                    lsdn.ThoiGianDangXuat,
                    CASE 
                        WHEN lsdn.ThoiGianDangXuat IS NOT NULL THEN 
                            DATEDIFF(SECOND, lsdn.ThoiGianDangNhap, lsdn.ThoiGianDangXuat)
                        ELSE NULL
                    END AS TongThoiGianGiay
                FROM 
                    LichSuDangNhap lsdn
                LEFT JOIN 
                    NhanVien nv ON lsdn.MaNhanVien = nv.MaNhanVien";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Thêm cột tổng thời gian đã định dạng (giờ:phút:giây)
                    dt.Columns.Add("TongThoiGianDangNhap", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["TongThoiGianGiay"] != DBNull.Value)
                        {
                            int seconds = Convert.ToInt32(row["TongThoiGianGiay"]);
                            TimeSpan ts = TimeSpan.FromSeconds(seconds);
                            row["TongThoiGianDangNhap"] = ts.ToString(@"hh\:mm\:ss");
                        }
                        else
                        {
                            row["TongThoiGianDangNhap"] = "Chưa đăng xuất";
                        }
                    }

                    // Bạn có thể giữ lại cột TongThoiGianGiay nếu muốn debug
                    // hoặc ẩn nó nếu chỉ cần hiển thị đã định dạng
                    dt.Columns.Remove("TongThoiGianGiay");

                    dgvLoginHistoy.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử đăng nhập: " + ex.Message);
            }
        }

        private void LoadEmployeesToComboBox()
        {
            try
            {
                using (SqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    string query = "SELECT MaNhanVien, TenNhanVien FROM NhanVien WHERE TrangThaiNhanVien = 1";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    Dictionary<string, string> employees = new Dictionary<string, string>();
                    while (reader.Read())
                    {
                        string ma = reader["MaNhanVien"].ToString();
                        string ten = reader["TenNhanVien"].ToString();
                        employees.Add(ma, ten);
                    }

                    cbEmployee.DataSource = new BindingSource(employees, null);
                    cbEmployee.DisplayMember = "Value"; // Hiển thị TenNhanVien
                    cbEmployee.ValueMember = "Key";     // Giá trị là MaNhanVien

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message);
            }
        }

        private void btCheck_Click(object sender, EventArgs e)
        {
            if (cbEmployee.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên.");
                return;
            }

            string maNhanVien = ((KeyValuePair<string, string>)cbEmployee.SelectedItem).Key;
            DateTime selectedDate = dtpDate.Value.Date;

            try
            {
                using (SqlConnection conn = new DatabaseConnection().GetConnection())
                {
                    string query = @"
                    SELECT 
                        SUM(DATEDIFF(SECOND, ThoiGianDangNhap, ThoiGianDangXuat)) AS TongThoiGianGiay
                    FROM 
                        LichSuDangNhap
                    WHERE 
                        MaNhanVien = @MaNhanVien 
                        AND CAST(ThoiGianDangNhap AS DATE) = @Ngay";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmd.Parameters.AddWithValue("@Ngay", selectedDate);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    conn.Close();

                    if (result != DBNull.Value && result != null)
                    {
                        int totalSeconds = Convert.ToInt32(result);
                        TimeSpan ts = TimeSpan.FromSeconds(totalSeconds);
                        lbInform.Text = $"Nhân viên đã đăng nhập {ts.Hours} giờ {ts.Minutes} phút {ts.Seconds} giây trong ngày {selectedDate:dd/MM/yyyy}.";
                    }
                    else
                    {
                        lbInform.Text = $"Nhân viên không có lượt đăng nhập nào trong ngày {selectedDate:dd/MM/yyyy}.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra thời gian đăng nhập: " + ex.Message);
            }
        }

        private void F_QLDangNhap_Load(object sender, EventArgs e)
        {
            LoadEmployeesToComboBox();
            LoadLoginHistory();
        }
    }
}
