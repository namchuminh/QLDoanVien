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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyDoanVien
{
    public partial class DoanVien : Form
    {
        SqlConnection conn;
        string query;
        SqlDataReader data = null;
        SqlCommand cmd = null;
        string mdvImage = "";
        public DoanVien()
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
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLamMoi.Enabled = true;
            btnChonAnh.Enabled = false;
            txtMaDoanVien.Enabled = true;
        }
        void disableButton()
        {
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLamMoi.Enabled = true;
            btnChonAnh.Enabled = true;
            txtMaDoanVien.Enabled = false;
        }

        void getChiDoan()
        {
            try
            {
                conn.Open();
                query = "SELECT * FROM ChiDoan";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                cbChiDoan.Items.Clear();
                cbChiDoan2.Items.Clear();
                while (data.Read())
                {
                    cbChiDoan.Items.Add(data["MaChiDoan"].ToString());
                    cbChiDoan2.Items.Add(data["MaChiDoan"].ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        void getData()
        {
            try
            {
                List<doanvien> lstDoanVien = new List<doanvien>();
                conn.Open();
                query = "SELECT * FROM DoanVien";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    doanvien doanvien = new doanvien();
                    doanvien.MaDoanVien = (string)data["MaDoanVien"];
                    doanvien.TenDoanVien = (string)data["TenDoanVien"];
                    doanvien.NgaySinh = (string)data["NgaySinh"];
                    doanvien.NgayVaoDoan = (string)data["NgayVaoDoan"];
                    doanvien.ChucVu = (string)data["ChucVu"];
                    doanvien.ChiDoan = (string)data["ChiDoan"];
                    lstDoanVien.Add(doanvien);
                }
                dgvDoanVien.DataSource = lstDoanVien;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        int checkIssetDoanVien(string MaDoanVien)
        {
            try
            {
                conn.Open();
                query = $"SELECT COUNT(*) FROM DoanVien WHERE MaDoanVien = '{MaDoanVien}'";
                cmd = new SqlCommand(query, conn);
                int result = (int)cmd.ExecuteScalar();
                conn.Close();
                return result;
            }catch(Exception ex)
            {
                return -1;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaDoanVien.Text == "" || txtTenDoanVien.Text == "" || txtNgaySinh.Text == "" || txtNgayVaoDoan.Text == "")
                {
                    MessageBox.Show("Vui Nhập Đủ Thông Tin Đoàn Viên!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cbChiDoan.Text == "")
                {
                    MessageBox.Show("Vui Lòng Chọn Chi Đoàn!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cbChucVu.Text == "")
                {
                    MessageBox.Show("Vui Lòng Chọn Chức Vụ!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                char ch = '-';
                int soNgaySinh = txtNgaySinh.Text.Split('-').Length;
                int soGachNgang = txtNgaySinh.Text.Count(c => c == ch);

                int soNgayVaoDoan = txtNgayVaoDoan.Text.Split('-').Length;
                int soGachNgangNgayVaoDoan = txtNgayVaoDoan.Text.Count(c => c == ch);

                if (soNgaySinh != 3 || soGachNgang != 2)
                {
                    MessageBox.Show("Ngày Sinh Nhập Theo Định Dạng: ngày-tháng-năm!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (soNgayVaoDoan != 3 || soGachNgangNgayVaoDoan != 2)
                {
                    MessageBox.Show("Ngày Vào Đoàn Nhập Theo Định Dạng: ngày-tháng-năm!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if(checkIssetDoanVien(txtMaDoanVien.Text) == 1)
                {
                    MessageBox.Show("Mã Đoàn Viên Đã Tồn Tại!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }else if (checkIssetDoanVien(txtMaDoanVien.Text) == -1)
                {
                    MessageBox.Show("Có Lỗi Khi Thêm Đoàn Viên", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    conn.Open();
                    query = $"INSERT INTO DoanVien(MaDoanVien,TenDoanVien,NgaySinh,NgayVaoDoan,ChucVu,ChiDoan) VALUES ('{txtMaDoanVien.Text}', N'{txtTenDoanVien.Text}', '{txtNgaySinh.Text}', '{txtNgayVaoDoan.Text}', N'{cbChucVu.Text}', N'{cbChiDoan.Text}')";
                    cmd = new SqlCommand(query, conn);
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Thêm Đoàn Viên Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show($"Vui Lòng Thêm Ảnh Cho Đoàn Viên: {txtTenDoanVien.Text}", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mdvImage = txtMaDoanVien.Text;
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("Thêm Đoàn Viên Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (mdvImage == "")
            {
                MessageBox.Show("Vui Lòng Chọn Đoàn Viên Hoặc Thêm Đoàn Viên Trước!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Gán đường dẫn của file được chọn cho một biến  
                string filePath = openFileDialog1.FileName;

                // Lấy đường dẫn thư mục của ứng dụng
                string appPath = Application.StartupPath;

                Random random = new Random();
                int randomNumber = random.Next(1000000,999999999);

                // Thiết lập tên mới cho file ảnh
                string newFileName = mdvImage + randomNumber.ToString() + ".jpg";

                string imgDir = appPath + "\\Image\\" + newFileName;
                // Sao chép tập tin ảnh vào thư mục của ứng dụng
                File.Copy(filePath, imgDir);

                conn.Open();
                query = $"UPDATE DoanVien SET Anh = '{newFileName}' WHERE MaDoanVien = '{mdvImage}'";
                cmd = new SqlCommand(query, conn);
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                if (result == 1)
                {
                    MessageBox.Show("Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Set đường dẫn tới file ảnh
                    imgDoanVien.ImageLocation = imgDir;

                    // Hiển thị ảnh trong PictureBox
                    imgDoanVien.SizeMode = PictureBoxSizeMode.StretchImage;
                    imgDoanVien.Load();
                    getData();
                }
                else
                {
                    MessageBox.Show("Cập Nhật Ảnh Cho Đoàn Viên Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void dgvDoanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dgvDoanVien.CurrentCell.RowIndex;
            txtMaDoanVien.Text = dgvDoanVien.Rows[rowindex].Cells[0].Value.ToString();
            txtTenDoanVien.Text = dgvDoanVien.Rows[rowindex].Cells[1].Value.ToString();
            txtNgaySinh.Text = dgvDoanVien.Rows[rowindex].Cells[2].Value.ToString().Split(' ')[0].ToString();
            txtNgayVaoDoan.Text = dgvDoanVien.Rows[rowindex].Cells[3].Value.ToString();
            cbChucVu.Text = dgvDoanVien.Rows[rowindex].Cells[4].Value.ToString();
            cbChiDoan.Text = dgvDoanVien.Rows[rowindex].Cells[5].Value.ToString();

            mdvImage = dgvDoanVien.Rows[rowindex].Cells[0].Value.ToString();

            conn.Open();
            query = $"SELECT * FROM DoanVien WHERE MaDoanVien = '{mdvImage}'";
            cmd = new SqlCommand(query, conn);
            data = cmd.ExecuteReader();

            string appPath = Application.StartupPath;

            while (data.Read())
            {
                // Set đường dẫn tới file ảnh
                imgDoanVien.ImageLocation = appPath + "\\Image\\" + (string)data["Anh"];

                // Hiển thị ảnh trong PictureBox
                imgDoanVien.SizeMode = PictureBoxSizeMode.StretchImage;
                imgDoanVien.Load();
            }
            conn.Close();

            disableButton();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtMaDoanVien.Text == "")
            {
                MessageBox.Show("Vui Nhập Chọn Đoàn Viên Cần Xóa!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult delete = MessageBox.Show("Bạn Thực Sự Muốn Xóa Đoàn Viên Này?", "Thông Báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (delete == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    query = $"DELETE DoanVien WHERE MaDoanVien = '{txtMaDoanVien.Text}';";
                    cmd = new SqlCommand(query, conn);
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getData();
                        resetTxt();
                        if (imgDoanVien.Image != null)
                        {
                            imgDoanVien.Image.Dispose();
                            imgDoanVien.Image = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Xóa Đoàn Viên Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        void resetTxt()
        {
            txtMaDoanVien.Text = "";
            txtTenDoanVien.Text = "";
            txtNgaySinh.Text = "";
            txtNgayVaoDoan.Text = "";
            txtTenDoanVien2.Text = "";
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            enabledButton();
            getData();
            resetTxt();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaDoanVien.Text == "")
            {
                MessageBox.Show("Vui Nhập Chọn Đoàn Viên Cần Sửa Thông Tin!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenDoanVien.Text == "" || txtNgaySinh.Text == "" || txtNgayVaoDoan.Text == "" || cbChucVu.Text == "" || cbChiDoan.Text == "")
            {
                MessageBox.Show("Vui Nhập Đủ Thông Tin Cập Nhật!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            char ch = '-';
            int soNgaySinh = txtNgaySinh.Text.Split('-').Length;
            int soGachNgang = txtNgaySinh.Text.Count(c => c == ch);

            int soNgayVaoDoan = txtNgayVaoDoan.Text.Split('-').Length;
            int soGachNgangNgayVaoDoan = txtNgayVaoDoan.Text.Count(c => c == ch);

            if (soNgaySinh != 3 || soGachNgang != 2)
            {
                MessageBox.Show("Ngày Sinh Nhập Theo Định Dạng: ngày-tháng-năm!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (soNgayVaoDoan != 3 || soGachNgangNgayVaoDoan != 2)
            {
                MessageBox.Show("Ngày Vào Đoàn Nhập Theo Định Dạng: ngày-tháng-năm!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                conn.Open();
                query = $"UPDATE DoanVien SET TenDoanVien=N'{txtTenDoanVien.Text}',NgaySinh='{txtNgaySinh.Text}',NgayVaoDoan='{txtNgayVaoDoan.Text}', ChucVu=N'{cbChucVu.Text}', ChiDoan=N'{cbChiDoan.Text}' WHERE MaDoanVien = '{txtMaDoanVien.Text}'";
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
                    MessageBox.Show("Cập Nhật Đoàn Viên Không Thành Công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        void searchData(string ChiDoan, string TenDoanVien)
        {
            try
            {
                List<doanvien> lstDoanVien = new List<doanvien>();
                conn.Open();
                query = $"SELECT * FROM DoanVien WHERE TenDoanVien LIKE '%{TenDoanVien}%' AND ChiDoan = N'{ChiDoan}'";
                cmd = new SqlCommand(query, conn);
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    doanvien doanvien = new doanvien();
                    doanvien.MaDoanVien = (string)data["MaDoanVien"];
                    doanvien.TenDoanVien = (string)data["TenDoanVien"];
                    doanvien.NgaySinh = (string)data["NgaySinh"];
                    doanvien.NgayVaoDoan = (string)data["NgayVaoDoan"];
                    doanvien.ChucVu = (string)data["ChucVu"];
                    doanvien.ChiDoan = (string)data["ChiDoan"];
                    lstDoanVien.Add(doanvien);
                }
                dgvDoanVien.DataSource = lstDoanVien;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            if (txtTenDoanVien2.Text == "" && cbChiDoan2.Text == "")
            {
                MessageBox.Show("Vui Lòng Chọn Chi Đoàn Và Tên Của Đoàn Viên Cần Tìm!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbChiDoan2.Text == "")
            {
                MessageBox.Show("Vui Lòng Chọn Chi Đoàn Cần Tìm!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtTenDoanVien2.Text == "")
            {
                MessageBox.Show("Vui Lòng Chọn Tên Đoàn Viên Cần Tìm!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            searchData(cbChiDoan2.Text, txtTenDoanVien2.Text);
        }
    }
}
