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
using System.Collections;

namespace QuanLyDoanVien
{
    public partial class ChiDoan : Form
    {
        SqlConnection conn;
        string query;
        SqlDataReader data = null;
        SqlCommand cmd = null;
        public ChiDoan()
        {
            InitializeComponent();
            ConnectionDB connectionDB = new ConnectionDB();
            conn = connectionDB.ConnectDB();
            getData();
            enabledButton();
        }

        void enabledButton()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLamMoi.Enabled = true;
            txtMaChiDoan.Enabled = true;
        }
        void disableButton()
        {
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLamMoi.Enabled = true;
            txtMaChiDoan.Enabled = false;
        }

        void resetTxt()
        {
            txtMaChiDoan.Text = "";
            txtTenChiDoan.Text = "";
            txtNgayThanhLap.Text = "";
            txtKhuVuc.Text = "";
            txtSoDoanVien.Text = "";
        }
        void getData()
        {
            try
            {
                List<chidoan> lstChiDoan = new List<chidoan>();
                conn.Open();
                query = "SELECT * FROM ChiDoan";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    chidoan objChiDoan = new chidoan();
                    objChiDoan.MaChiDoan = (string)data["MaChiDoan"];
                    objChiDoan.TenChiDoan = (string)data["TenChiDoan"];
                    objChiDoan.NgayThanhLap = (string)data["NgayThanhLap"];
                    objChiDoan.KhuVuc = (string)data["KhuVuc"];
                    objChiDoan.SoDoanVien = (int)data["SoDoanVien"];
                    lstChiDoan.Add(objChiDoan);
                }
                dgvChiDoan.DataSource = lstChiDoan;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        int checkIssetDoanVien(string MaChiDoan)
        {
            try
            {
                conn.Open();
                query = $"SELECT COUNT(*) FROM ChiDoan WHERE MaChiDoan = N'{MaChiDoan}'";
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaChiDoan.Text == "" || txtTenChiDoan.Text == "" || txtNgayThanhLap.Text == "" || txtKhuVuc.Text == "")
                {
                    MessageBox.Show("Vui Nhập Đủ Thông Chi Đoàn!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                char ch = '-';
                int soNgayThanhLap = txtNgayThanhLap.Text.Split('-').Length;
                int soGachNgang = txtNgayThanhLap.Text.Count(c => c == ch);

                if (soNgayThanhLap != 3 || soGachNgang != 2)
                {
                    MessageBox.Show("Ngày Thành Lập Nhập Theo Định Dạng: ngày-tháng-năm!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                if (checkIssetDoanVien(txtMaChiDoan.Text) == 1)
                {
                    MessageBox.Show("Mã Chi Đoàn Đã Tồn Tại!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (checkIssetDoanVien(txtMaChiDoan.Text) == -1)
                {
                    MessageBox.Show("Có Lỗi Khi Thêm Chi Đoàn!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    conn.Open();
                    query = $"INSERT INTO ChiDoan(MaChiDoan,TenChiDoan,NgayThanhLap,KhuVuc,SoDoanVien) VALUES (N'{txtMaChiDoan.Text}', N'{txtTenChiDoan.Text}', '{txtNgayThanhLap.Text}', N'{txtKhuVuc.Text}', {txtSoDoanVien.Text})";
                    cmd = new SqlCommand(query, conn);
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Thêm Chi Đoàn Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getData();
                        resetTxt();
                    }
                    else
                    {
                        MessageBox.Show("Thêm Chi Đoàn Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvChiDoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dgvChiDoan.CurrentCell.RowIndex;
            txtMaChiDoan.Text = dgvChiDoan.Rows[rowindex].Cells[0].Value.ToString();
            txtTenChiDoan.Text = dgvChiDoan.Rows[rowindex].Cells[1].Value.ToString();
            txtNgayThanhLap.Text = dgvChiDoan.Rows[rowindex].Cells[2].Value.ToString();
            txtKhuVuc.Text = dgvChiDoan.Rows[rowindex].Cells[3].Value.ToString();
            txtSoDoanVien.Text = dgvChiDoan.Rows[rowindex].Cells[4].Value.ToString();
            disableButton();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaChiDoan.Text == "" || txtTenChiDoan.Text == "" || txtNgayThanhLap.Text == "" || txtKhuVuc.Text == "")
            {
                MessageBox.Show("Vui Nhập Đủ Thông Chi Đoàn!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            char ch = '-';
            int soNgayThanhLap = txtNgayThanhLap.Text.Split('-').Length;
            int soGachNgang = txtNgayThanhLap.Text.Count(c => c == ch);

            if (soNgayThanhLap != 3 || soGachNgang != 2)
            {
                MessageBox.Show("Ngày Thành Lập Nhập Theo Định Dạng: ngày-tháng-năm!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                conn.Open();
                query = $"UPDATE ChiDoan SET TenChiDoan=N'{txtTenChiDoan.Text}',NgayThanhLap='{txtNgayThanhLap.Text}',KhuVuc=N'{txtKhuVuc.Text}', SoDoanVien={txtSoDoanVien.Text} WHERE MaChiDoan = N'{txtMaChiDoan.Text}'";
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
                    MessageBox.Show("Cập Nhật Chi Đoàn Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            enabledButton();
            getData();
            resetTxt();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaChiDoan.Text == "")
            {
                MessageBox.Show("Vui Nhập Chọn Chi Đoàn Cần Xóa!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult delete = MessageBox.Show("Bạn Thực Sự Muốn Xóa Chi Đoàn Này?", "Thông Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (delete == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    query = $"DELETE ChiDoan WHERE MaChiDoan = N'{txtMaChiDoan.Text}'";
                    cmd = new SqlCommand(query, conn);
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getData();
                        resetTxt();
                    }
                    else
                    {
                        MessageBox.Show("Xóa Chi Đoàn Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        void searchData(string TenChiDoan)
        {
            try
            {
                List<chidoan> lstChiDoan = new List<chidoan>();
                conn.Open();
                query = $"SELECT * FROM ChiDoan WHERE TenChiDoan LIKE N'%{TenChiDoan}%'";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    chidoan objChiDoan = new chidoan();
                    objChiDoan.MaChiDoan = (string)data["MaChiDoan"];
                    objChiDoan.TenChiDoan = (string)data["TenChiDoan"];
                    objChiDoan.NgayThanhLap = (string)data["NgayThanhLap"];
                    objChiDoan.KhuVuc = (string)data["KhuVuc"];
                    objChiDoan.SoDoanVien = (int)data["SoDoanVien"];
                    lstChiDoan.Add(objChiDoan);
                }
                dgvChiDoan.DataSource = lstChiDoan;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTenChiDoan2.Text == "")
            {
                MessageBox.Show("Vui Nhập Đủ Thông Tin Tìm Kiếm!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            searchData(txtTenChiDoan2.Text);
        }
    }
}
