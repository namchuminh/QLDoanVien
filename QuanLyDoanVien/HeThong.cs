﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDoanVien
{
    public partial class HeThong : Form
    {
        public HeThong()
        {
            InitializeComponent();
            loadform(new DoanVien());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void loadform(object Form)
        {
            if (this.panel_main.Controls.Count > 0)
            {
                this.panel_main.Controls.RemoveAt(0);
            }
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panel_main.Controls.Add(f);
            this.panel_main.Tag = f;
            f.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadform(new DoanVien());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadform(new ChiDoan());
        }
    }
}
