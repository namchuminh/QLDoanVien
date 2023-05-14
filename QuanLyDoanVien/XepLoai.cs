using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLyDoanVien.Models;

namespace QuanLyDoanVien
{
    public partial class XepLoai : Form
    {
        SqlConnection conn;
        string query;
        SqlDataReader data = null;
        SqlCommand cmd = null;
        public XepLoai()
        {
            InitializeComponent();
            ConnectionDB connectionDB = new ConnectionDB();
            conn = connectionDB.ConnectDB();
            getData();
            getChiDoan();
            enabledButton();
        }

        void enabledButton()
        {
            btnXepLoai.Enabled = true;
            btnSua.Enabled = false;
            btnLamMoi.Enabled = true;
            cbMaChiDoan.Enabled = true;
            cbTenDoanVien.Enabled = true;
        }
        void disableButton()
        {
            btnXepLoai.Enabled = false;
            btnSua.Enabled = true;
            btnLamMoi.Enabled = true;
            cbMaChiDoan.Enabled = false;
            cbTenDoanVien.Enabled = false;
        }

        void getData()
        {
            try
            {
                List<xeploai> lstXepLoai = new List<xeploai>();
                conn.Open();
                query = "SELECT DoanVien.*, XepLoai.* FROM DoanVien, XepLoai WHERE DoanVien.MaDoanVien = XepLoai.MaDoanVien";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    xeploai xeploai = new xeploai();
                    xeploai.MaDoanVien = (string)data["MaDoanVien"];
                    xeploai.TenDoanVien = (string)data["TenDoanVien"];
                    xeploai.NgaySinh = (string)data["NgaySinh"];
                    xeploai.ChiDoan = (string)data["ChiDoan"];
                    xeploai.XepLoai = (string)data["XepLoai"];
                    xeploai.ThoiGian = (string)data["ThoiGian"];
                    xeploai.NhanXet = (string)data["NhanXet"];
                    lstXepLoai.Add(xeploai);
                }
                dgvXepLoai.DataSource = lstXepLoai;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

            cbThang2.Text = "Tất Cả";
        }

        void getChiDoan()
        {
            try
            {
                conn.Open();
                query = "SELECT * FROM ChiDoan";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                cbMaChiDoan.Items.Clear();
                cbMaChiDoan2.Items.Clear();
                while (data.Read())
                {
                    cbMaChiDoan.Items.Add(data["MaChiDoan"].ToString());
                    cbMaChiDoan2.Items.Add(data["MaChiDoan"].ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void cbMaChiDoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                query = $"SELECT * FROM DoanVien WHERE ChiDoan = N'{cbMaChiDoan.Text}'";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                cbTenDoanVien.Items.Clear();
                while (data.Read())
                {
                    cbTenDoanVien.Items.Add(data["MaDoanVien"].ToString() + "-" +data["TenDoanVien"].ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        int checkIssetDoanVien(string MaDoanVien, string Thang)
        {
            try
            {
                conn.Open();
                query = $"SELECT COUNT(*) FROM XepLoai WHERE MaDoanVien = '{MaDoanVien}' AND ThoiGian = '{Thang}'";
                cmd = new SqlCommand(query, conn);
                int result = (int)cmd.ExecuteScalar();
                conn.Close();
                return result;
            }
            catch (Exception)
            {
                return -1;
            }

        }
        private void btnXepLoai_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbMaChiDoan.Text == "" || cbTenDoanVien.Text == "" || cbXepLoai.Text == "" || cbThang.Text == "" || txtNhanXet.Text == "")
                {
                    MessageBox.Show("Vui Nhập Đủ Thông Tin Cho Đoàn Viên Cần Xếp Loại!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (checkIssetDoanVien(cbTenDoanVien.Text.Split('-')[0].ToString(), cbThang.Text) == 1)
                {
                    MessageBox.Show("Đoàn Viên " + cbTenDoanVien.Text.Split('-')[1].ToString() + " Đã Được Xếp Loại Trong Tháng Này!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (checkIssetDoanVien(cbTenDoanVien.Text.Split('-')[0].ToString(), cbThang.Text) == -1)
                {
                    MessageBox.Show("Có Lỗi Khi Xếp Loại Đoàn Viên!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    string MaDoanVien = cbTenDoanVien.Text.Split('-')[0].ToString();
                    string TenDoanVien = cbTenDoanVien.Text.Split('-')[1].ToString();
                    conn.Open();
                    query = $"INSERT INTO XepLoai(MaDoanVien,MaChiDoan,XepLoai,ThoiGian,NhanXet) VALUES (N'{MaDoanVien}', N'{cbMaChiDoan.Text}', N'{cbXepLoai.Text}', N'{cbThang.Text}', N'{txtNhanXet.Text}')";
                    cmd = new SqlCommand(query, conn);
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show($"Xếp Loại Cho Đoàn Viên {TenDoanVien} Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("Xếp Loại Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvXepLoai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dgvXepLoai.CurrentCell.RowIndex;
            cbMaChiDoan.Text = dgvXepLoai.Rows[rowindex].Cells[3].Value.ToString();

            cbTenDoanVien.Items.Clear();
            cbTenDoanVien.Items.Add(dgvXepLoai.Rows[rowindex].Cells[0].Value.ToString()+"-"+dgvXepLoai.Rows[rowindex].Cells[1].Value.ToString());
            cbTenDoanVien.Text = dgvXepLoai.Rows[rowindex].Cells[0].Value.ToString() + "-" + dgvXepLoai.Rows[rowindex].Cells[1].Value.ToString();

            cbXepLoai.Text = dgvXepLoai.Rows[rowindex].Cells[4].Value.ToString();
            cbThang.Text = dgvXepLoai.Rows[rowindex].Cells[5].Value.ToString();
            txtNhanXet.Text = dgvXepLoai.Rows[rowindex].Cells[6].Value.ToString();

            cbThang.Enabled = false;
            disableButton();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (cbXepLoai.Text == "" || txtNhanXet.Text == "")
            {
                MessageBox.Show("Vui Nhập Đủ Thông Tin Xếp Loại Cho Đoàn Viên!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string MaDoanVien = cbTenDoanVien.Text.Split('-')[0].ToString();
                conn.Open();
                query = $"UPDATE XepLoai SET XepLoai=N'{cbXepLoai.Text}',NhanXet=N'{txtNhanXet.Text}' WHERE MaDoanVien = N'{MaDoanVien}' AND ThoiGian = N'{cbThang.Text}'";
                cmd = new SqlCommand(query, conn);
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                if (result == 1)
                {
                    MessageBox.Show("Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getData();
                }
                else
                {
                    MessageBox.Show("Xếp Loại Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            getData();
            getChiDoan();
            enabledButton();
            cbTenDoanVien.Items.Clear();
            cbThang.Enabled = true;
            txtNhanXet.Text = "";

            cbXepLoai.Items.Clear();
            cbXepLoai.Items.Add("Giỏi");
            cbXepLoai.Items.Add("Khá");
            cbXepLoai.Items.Add("Trung Bình");
            cbXepLoai.Items.Add("Yếu");
            cbXepLoai.Items.Add("Kém");

            cbThang.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                cbThang.Items.Add("Tháng "+i);
            }
            
        }

        private void cbThang2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbThang2.Text == "Tất Cả")
            {
                getData();
            }
            else
            {
                try
                {
                    List<xeploai> lstXepLoai = new List<xeploai>();
                    conn.Open();
                    query = $"SELECT DoanVien.*, XepLoai.* FROM DoanVien, XepLoai WHERE DoanVien.MaDoanVien = XepLoai.MaDoanVien AND XepLoai.ThoiGian = N'{cbThang2.Text}'";
                    cmd = new SqlCommand(query, conn);
                    data = cmd.ExecuteReader();
                    while (data.Read())
                    {
                        xeploai xeploai = new xeploai();
                        xeploai.MaDoanVien = (string)data["MaDoanVien"];
                        xeploai.TenDoanVien = (string)data["TenDoanVien"];
                        xeploai.NgaySinh = (string)data["NgaySinh"];
                        xeploai.ChiDoan = (string)data["ChiDoan"];
                        xeploai.XepLoai = (string)data["XepLoai"];
                        xeploai.ThoiGian = (string)data["ThoiGian"];
                        xeploai.NhanXet = (string)data["NhanXet"];
                        lstXepLoai.Add(xeploai);
                    }
                    dgvXepLoai.DataSource = lstXepLoai;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cbMaChiDoan2.Text == "" || cbXepLoai2.Text == "")
            {
                MessageBox.Show("Vui Chọn Đủ Thông Tin Tìm Kiếm Xếp Loại Của Đoàn Viên!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                List<xeploai> lstXepLoai = new List<xeploai>();
                conn.Open();
                query = $"SELECT DoanVien.*, XepLoai.* FROM DoanVien, XepLoai WHERE DoanVien.MaDoanVien = XepLoai.MaDoanVien AND XepLoai.MaChiDoan = N'{cbMaChiDoan2.Text}' AND XepLoai.XepLoai = N'{cbXepLoai2.Text}'";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    xeploai xeploai = new xeploai();
                    xeploai.MaDoanVien = (string)data["MaDoanVien"];
                    xeploai.TenDoanVien = (string)data["TenDoanVien"];
                    xeploai.NgaySinh = (string)data["NgaySinh"];
                    xeploai.ChiDoan = (string)data["ChiDoan"];
                    xeploai.XepLoai = (string)data["XepLoai"];
                    xeploai.ThoiGian = (string)data["ThoiGian"];
                    xeploai.NhanXet = (string)data["NhanXet"];
                    lstXepLoai.Add(xeploai);
                }
                dgvXepLoai.DataSource = lstXepLoai;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
