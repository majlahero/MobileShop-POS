using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
namespace HTTT
{
    public partial class F_ThongKe : Form
    {
        public F_ThongKe()
        {
            InitializeComponent();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtpFromDate.Value.Date;
            DateTime toDate = dtpToDate.Value.Date;

            using (SqlConnection conn = new DatabaseConnection().GetConnection())
            {
                conn.Open();

                string querySummary = @"
                SELECT 
                    ISNULL(SUM(TongTien), 0) AS TongDoanhThu,
                    ISNULL(SUM(SoLuong), 0) AS TongSoSanPham,
                    COUNT(DISTINCT MaDonHang) AS SoDonHang
                FROM vw_HoaDon
                WHERE NgayDatHang BETWEEN @FromDate AND @ToDate";

                SqlCommand cmdSummary = new SqlCommand(querySummary, conn);
                cmdSummary.Parameters.AddWithValue("@FromDate", fromDate);
                cmdSummary.Parameters.AddWithValue("@ToDate", toDate);

                using (SqlDataReader reader = cmdSummary.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblTongDoanhThu.Text = "Tổng doanh thu: " + reader["TongDoanhThu"].ToString() + " VNĐ";
                        lblTongSanPham.Text = "Tổng sản phẩm bán: " + reader["TongSoSanPham"].ToString();
                        lblSoDonHang.Text = "Số đơn hàng: " + reader["SoDonHang"].ToString() + " đơn";
                    }
                }

                // 2. Doanh thu theo sản phẩm từ VIEW
                string queryDetail = @"
                SELECT 
                    TenSanPham, 
                    SUM(SoLuong) AS SoLuongBan,
                    SUM(TongTien) AS DoanhThu
                FROM vw_HoaDon
                WHERE NgayDatHang BETWEEN @FromDate AND @ToDate
                GROUP BY TenSanPham
                ORDER BY DoanhThu DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(queryDetail, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@FromDate", fromDate);
                adapter.SelectCommand.Parameters.AddWithValue("@ToDate", toDate);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvThongKe.DataSource = dt;
            }
        }

        private void FormAnalysis_Load(object sender, EventArgs e)
        {
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear - 10; year <= currentYear + 1; year++)
            {
                cbNam.Items.Add(year);
            }
            cbNam.SelectedItem = currentYear;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (cbNam.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn năm cần thống kê!");
                return;
            }

            int selectedYear = Convert.ToInt32(cbNam.SelectedItem);

            using (SqlConnection conn = new DatabaseConnection().GetConnection())
            {
                conn.Open();

                string query = @"
                SELECT 
                    MONTH(NgayDatHang) AS Thang,
                    SUM(TongTien) AS DoanhThu
                FROM vw_HoaDon
                WHERE YEAR(NgayDatHang) = @Year
                GROUP BY MONTH(NgayDatHang)
                ORDER BY Thang";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Year", selectedYear);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Xóa dữ liệu cũ
                chartDoanhThuThang.Series.Clear();
                chartDoanhThuThang.Titles.Clear();

                // Tạo Series mới
                var series = new System.Windows.Forms.DataVisualization.Charting.Series("Doanh thu");
                series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                // Thêm dữ liệu vào chart
                for (int i = 1; i <= 12; i++)
                {
                    var row = dt.AsEnumerable().FirstOrDefault(r => r.Field<int>("Thang") == i);
                    decimal doanhThu = row != null ? row.Field<decimal>("DoanhThu") : 0;

                    series.Points.AddXY(" " + i, doanhThu);

                    if (doanhThu > 0)
                    {
                        series.Points[series.Points.Count - 1].Label = doanhThu.ToString("N0") + " VNĐ";
                    }
                    else
                    {
                        series.Points[series.Points.Count - 1].Label = "";
                    }
                }

                chartDoanhThuThang.Series.Add(series);
                chartDoanhThuThang.Titles.Add("Doanh thu theo từng tháng - Năm " + selectedYear);
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvThongKe.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Lưu báo cáo doanh thu";
            saveFileDialog.FileName = "BaoCaoDoanhThu.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Báo cáo doanh thu");

                        int row = 1;

                        worksheet.Cell(row, 1).Value = "BÁO CÁO DOANH THU CỬA HÀNG KINH DOANH ĐIỆN THOẠI";
                        worksheet.Range(row, 1, row, 3).Merge().AddToNamed("Titles");
                        worksheet.Row(row).Style.Font.SetBold().Font.FontSize = 14;
                        row += 2;

                        worksheet.Cell(row, 1).Value = "Thống kê từ ngày:";
                        worksheet.Cell(row, 2).Value = dtpFromDate.Value.ToShortDateString();
                        row++;
                        worksheet.Cell(row, 1).Value = "Đến ngày:";
                        worksheet.Cell(row, 2).Value = dtpToDate.Value.ToShortDateString();
                        row++;

                        worksheet.Cell(row, 1).Value = lblTongDoanhThu.Text;
                        row++;
                        worksheet.Cell(row, 1).Value = lblTongSanPham.Text;
                        row++;
                        worksheet.Cell(row, 1).Value = lblSoDonHang.Text;
                        row += 2;

                        DataTable dt = (DataTable)dgvThongKe.DataSource;
                        worksheet.Cell(row, 1).InsertTable(dt, "ChiTietSanPham", true);

                        worksheet.Columns().AdjustToContents();

                        workbook.SaveAs(saveFileDialog.FileName);
                    }

                    MessageBox.Show("Xuất file Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}