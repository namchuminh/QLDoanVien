using QuanLyDoanVien.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDoanVien
{
    public partial class DangNhap : Form
    {
        SqlConnection conn;
        string query;
        SqlCommand cmd = null;
        public DangNhap()
        {
            InitializeComponent();
            ConnectionDB connectionDB = new ConnectionDB();
            conn = connectionDB.ConnectDB();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                query = $"SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan = '{txtTaiKhoan.Text}' AND MatKhau = '{txtMatKhau.Text}'";
                cmd = new SqlCommand(query, conn);
                int kq = (int)cmd.ExecuteScalar();
                if (kq == 1)
                {
                    this.Hide();
                    HeThong heThong = new HeThong();
                    heThong.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Đăng Nhập Thất Bại! Kiểm Tra Lại Tài Khoản Mật Khẩu!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
