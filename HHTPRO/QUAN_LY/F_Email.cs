using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HTTT.QUAN_LY
{
    public partial class F_Email : Form
    {
        public F_Email()
        {
            InitializeComponent();
        }
        private void btnGui_Click(object sender, EventArgs e)
        {
            try
            {
                string fromEmail = "hoaphan23072004@gmail.com"; // Giữ nguyên email của bạn
                string password = "howx ipry urzt idgh"; // Sử dụng mật khẩu ứng dụng bạn vừa tạo
                string subject = txtTieuDe.Text;
                string body = txtNoiDung.Text;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false; // Quan trọng: phải đặt false
                    smtp.Credentials = new NetworkCredential(fromEmail, password);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    // Kiểm tra nếu checkbox được tích
                    if (checkBox.Checked)
                    {
                        // Lấy danh sách email từ cơ sở dữ liệu
                        List<string> emailList = GetEmailListFromDatabase();

                        foreach (string toEmail in emailList)
                        {
                            if (!string.IsNullOrEmpty(toEmail))
                            {
                                // Tạo đối tượng MailMessage
                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress(fromEmail);
                                mail.To.Add(toEmail);
                                mail.Subject = subject;
                                mail.Body = body;
                                mail.IsBodyHtml = false; // Đặt true nếu bạn dùng HTML trong nội dung

                                // Gửi email
                                smtp.Send(mail);
                            }
                        }
                        MessageBox.Show("Email đã được gửi thành công tới tất cả khách hàng!");
                    }
                    else
                    {
                        string toEmail = txtEmail.Text;
                        if (!string.IsNullOrEmpty(toEmail)) 
                        {
                            MailMessage mail = new MailMessage();
                            mail.From = new MailAddress(fromEmail);
                            mail.To.Add(toEmail);
                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = false;

                            // Gửi email
                            smtp.Send(mail);
                            MessageBox.Show("Email đã được gửi thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Địa chỉ email không hợp lệ.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private List<string> GetEmailListFromDatabase()
        {
            List<string> emailList = new List<string>();

            // Kết nối đến cơ sở dữ liệu và lấy danh sách email
            using (var connection = new DatabaseConnection().GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Email FROM KhachHang", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Thêm email vào danh sách nếu không rỗng
                    string email = reader["Email"].ToString();
                    if (!string.IsNullOrEmpty(email))
                    {
                        emailList.Add(email);
                    }
                }
            }

            return emailList;
        }
    }
}
