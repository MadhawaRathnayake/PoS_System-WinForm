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
using System.Runtime.InteropServices;

namespace ProgrammingProject
{
    public partial class LoginForm : Form
    {
        DBConnection dBConn = new DBConnection();
        public static string sellerName;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox_role.Items.Add("Admin");
            comboBox_role.Items.Add("Seller");
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if (textBox_username.Text == "" || textBox_password.Text == "")
            {
                MessageBox.Show("Please Enter User Name and Password", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else {
                if (comboBox_role.SelectedIndex > -1)
                {

                    if (comboBox_role.SelectedItem.ToString() == "Admin") {
                        if (textBox_username.Text == "admin" && textBox_password.Text == "admin")
                        {
                            Product_Form productForm = new Product_Form();
                            productForm.Show();
                            this.Hide();
                        } else
                        {
                            MessageBox.Show("Wrong ID or Password", "Please check your ID and Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    } else
                    {
                        string selectQuery = "SELECT * FROM Seller  WHERE Seller_name = '" + textBox_username.Text + "' AND Seller_pass = '" + textBox_password.Text + "'";
                        SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, dBConn.GetCon());
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            sellerName = textBox_username.Text;
                            Selling_Form sellingForm = new Selling_Form();
                            sellingForm.Show();
                            this.Hide();
                        } else
                        {
                            MessageBox.Show("Wrong User Name or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                } else
                {
                    MessageBox.Show("Please Select a Role", "Selections Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }



        private void pictureBox_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button_login_MouseHover(object sender, EventArgs e)
        {
            string hexColor = "#2A466F";
            Color backgroundColor = ColorTranslator.FromHtml(hexColor);
            pictureBox2.BackColor = backgroundColor;
        }

        private void button_login_MouseLeave(object sender, EventArgs e)
        {
            string hexColor = "#233B5D";
            Color backgroundColor = ColorTranslator.FromHtml(hexColor);
            pictureBox2.BackColor = backgroundColor;
        }

        private void pictureBox_minimize_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_minimize.Image = Properties.Resources.MinimizeHover;
        }

        private void pictureBox_minimize_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_minimize.Image = Properties.Resources.Minimize;
        }

        private void pictureBox_exit_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_exit.Image = Properties.Resources.ExitHover;
        }

        private void pictureBox_exit_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_exit.Image = Properties.Resources.Exit;
        }

        //Draggable header

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("user32.dll")]

        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]

        public static extern int SendMessage(IntPtr hwnd, int Msg, int wParam, int lParam);

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

		private void comboBox_role_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
