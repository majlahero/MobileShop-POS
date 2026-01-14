using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HTTT
{
    public partial class F_NhapHang : Form
    {
        public F_NhapHang()
        {
            InitializeComponent();
            LoadComboBoxHang();
        }

        public class ChiTietPhieuNhap
        {
            public int MaSanPham { get; set; }
            public string TenSanPham { get; set; }
            public int SoLuong { get; set; }
            public decimal GiaNhap { get; set; }
            public decimal ThanhTien => SoLuong * GiaNhap;
        }

        private List<ChiTietPhieuNhap> danhSachNhapTam = new List<ChiTietPhieuNhap>();

        private void HienThiDanhSachTam()
        {
            var dt = new DataTable();
            dt.Columns.Add("MaSanPham", typeof(int));
            dt.Columns.Add("TenSanPham", typeof(string));
            dt.Columns.Add("SoLuong", typeof(int));
            dt.Columns.Add("GiaNhap", typeof(decimal));
            dt.Columns.Add("ThanhTien", typeof(decimal));

            foreach (var item in danhSachNhapTam)
            {
                dt.Rows.Add(item.MaSanPham, item.TenSanPham, item.SoLuong, item.GiaNhap, item.ThanhTien);
            }

            // Tính tổng
            if (dt.Rows.Count > 0)
            {
                decimal tong = danhSachNhapTam.Sum(x => x.ThanhTien);
                DataRow rowTong = dt.NewRow();
                rowTong["TenSanPham"] = "TỔNG CỘNG";
                rowTong["ThanhTien"] = tong;
                dt.Rows.Add(rowTong);
            }

            dataGridViewNhapHang.DataSource = dt;

            // Format dòng tổng
            int lastRow = dataGridViewNhapHang.Rows.Count - 1;
            if (lastRow >= 0)
            {
                var row = dataGridViewNhapHang.Rows[lastRow];
                row.ReadOnly = true;
                row.DefaultCellStyle.BackColor = Color.LightYellow;
                row.DefaultCellStyle.Font = new Font(dataGridViewNhapHang.Font, FontStyle.Bold);
            }
        }

        // Load dữ liệu phiếu nhập từ CSDL
        private void LoadData()
        {
            var dtNhap = new DataTable();
            using (SqlConnection conn = new DatabaseConnection().GetConnection())
            {
                conn.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT 
                        pn.MaNhapHang,
                        pn.NhaCungCap,
                        pn.NgayNhap,
                        ctpn.MaSanPham,
                        ctpn.SoLuong,
                        ctpn.GiaNhap,
                        (ctpn.SoLuong * ctpn.GiaNhap) AS ThanhTien
                    FROM PhieuNhapHang pn
                    JOIN ChiTietPhieuNhapHang ctpn
                      ON pn.MaNhapHang = ctpn.MaNhapHang
                    ORDER BY pn.MaNhapHang, ctpn.MaChiTietNhap
                ", conn))
                {
                    da.Fill(dtNhap);
                }
            }

            // Thêm cột "GhiChu" để chứa chữ "TỔNG CỘNG"
            if (!dtNhap.Columns.Contains("GhiChu"))
                dtNhap.Columns.Add("GhiChu", typeof(string));

            // Tính tổng tiền
            if (dtNhap.Rows.Count > 0)
            {
                decimal tongTien = dtNhap.AsEnumerable().Sum(r => r.Field<decimal>("ThanhTien"));
                DataRow rowTong = dtNhap.NewRow();
                rowTong["GhiChu"] = "TỔNG CỘNG";
                rowTong["ThanhTien"] = tongTien;
                dtNhap.Rows.Add(rowTong);
            }

            // Gán vào DataGridView
            dataGridViewNhapHang.DataSource = dtNhap;
            dataGridViewNhapHang.AllowUserToAddRows = false;

            // Định dạng dòng tổng
            int lastRow = dataGridViewNhapHang.Rows.Count - 1;
            if (lastRow >= 0)
            {
                var row = dataGridViewNhapHang.Rows[lastRow];
                row.ReadOnly = true;
                row.DefaultCellStyle.BackColor = Color.LightYellow;
                row.DefaultCellStyle.Font = new Font(dataGridViewNhapHang.Font, FontStyle.Bold);
            }

            // Cố định vị trí cột GhiChu đầu tiên (nếu cần)
            dataGridViewNhapHang.Columns["GhiChu"].DisplayIndex = 0;
        }

        // Load ComboBox hàng hóa
        private void LoadComboBoxHang()
        {
            using (SqlConnection conn = new DatabaseConnection().GetConnection())
            {
                conn.Open();
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT MaSanPham, TenSanPham FROM SanPham WHERE TrangThai = N'Còn bán'", conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox_tenHang.DataSource = dt;
                    comboBox_tenHang.DisplayMember = "TenSanPham";
                    comboBox_tenHang.ValueMember = "MaSanPham";
                    comboBox_tenHang.SelectedIndex = -1;
                }
            }
        }

        // Tính thành tiền
        private void CalcThanhTien(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_soLuong.Text, out int sl) && decimal.TryParse(textBox_donGia.Text, out decimal dg))
            {
                textBox_thanhTien.Text = (sl * dg).ToString("N2");
            }
            else
            {
                textBox_thanhTien.Text = "0";
            }
        }

        // Thêm sản phẩm vào danh sách tạm
        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (comboBox_tenHang.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(textBox_soLuong.Text) ||
                string.IsNullOrWhiteSpace(textBox_donGia.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm.");
                return;
            }

            var sp = new ChiTietPhieuNhap
            {
                MaSanPham = (int)comboBox_tenHang.SelectedValue,
                TenSanPham = comboBox_tenHang.Text,
                SoLuong = int.Parse(textBox_soLuong.Text),
                GiaNhap = decimal.Parse(textBox_donGia.Text)
            };

            danhSachNhapTam.Add(sp);
            HienThiDanhSachTam();

            // Xóa form nhập
            comboBox_tenHang.SelectedIndex = -1;
            textBox_soLuong.Clear();
            textBox_donGia.Clear();
            textBox_thanhTien.Clear();
        }

        // Xác nhận lưu phiếu nhập
        private void buttonVerify_Click(object sender, EventArgs e)
        {
            if (danhSachNhapTam.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào để lưu.");
                return;
            }

            int maNhapMoi;
            using (SqlConnection conn = new DatabaseConnection().GetConnection())
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert vào PhieuNhapHang
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = @"
                                INSERT INTO PhieuNhapHang (NhaCungCap, NgayNhap)
                                VALUES (@ncc, @ngay);
                                SELECT SCOPE_IDENTITY();";
                            cmd.Parameters.AddWithValue("@ncc", textBox_nhaCungCap.Text.Trim());
                            cmd.Parameters.AddWithValue("@ngay", dateTimePicker_ngayDat.Value);
                            maNhapMoi = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Insert từng sản phẩm
                        foreach (var sp in danhSachNhapTam)
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.Transaction = tran;
                                cmd.CommandText = @"
                                    INSERT INTO ChiTietPhieuNhapHang
                                      (MaNhapHang, MaSanPham, SoLuong, GiaNhap)
                                    VALUES
                                      (@man, @masp, @sl, @gia);";
                                cmd.Parameters.AddWithValue("@man", maNhapMoi);
                                cmd.Parameters.AddWithValue("@masp", sp.MaSanPham);
                                cmd.Parameters.AddWithValue("@sl", sp.SoLuong);
                                cmd.Parameters.AddWithValue("@gia", sp.GiaNhap);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
                        return;
                    }
                }
            }

            // Cập nhật lại lưới
            LoadData();
            danhSachNhapTam.Clear();
            HienThiDanhSachTam();

            MessageBox.Show("Đã lưu phiếu nhập.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear form chung
            textBox_nhaCungCap.Clear();
            dateTimePicker_ngayDat.Value = DateTime.Now;
        }
    }
}
