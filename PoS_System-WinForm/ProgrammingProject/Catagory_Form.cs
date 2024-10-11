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
    public partial class Catagory_Form : Form
    {
        DBConnection dBCon = new DBConnection();
        public Catagory_Form()
        {
            InitializeComponent();
        }

        private void getTable()
        {
            string selectQuerry = "SELECT * FROM Catagory";
            SqlCommand command = new SqlCommand(selectQuerry, dBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DataGridView_catagory.DataSource = table;
        }

        private void Catagory_Form_Load(object sender, EventArgs e)
        {
            getTable();
        }

        private void clear()
        {
            textBox_id.Clear();
            textBox_name.Clear();
            textBox_description.Clear();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_id.Text == "" || textBox_name.Text == "" || textBox_description.Text == "")
                {
                    MessageBox.Show("Missing Information", "Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string insertQuery = "INSERT INTO Catagory VALUES(" + textBox_id.Text + ", '" + textBox_name.Text + "','" + textBox_description.Text + "')";

                    SqlCommand command = new SqlCommand(insertQuery, dBCon.GetCon());

                    dBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Catagory Added Successfully", "Add information");
                    dBCon.CloseCon();
                    getTable();
                    clear();
                }
            } 
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try {
                if (textBox_id.Text == "" || textBox_name.Text == "" || textBox_description.Text == "")
                {
                    MessageBox.Show("Missing Information", "Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string updateQuery = "UPDATE Catagory SET Cat_name = '" + textBox_name.Text + "', Cat_des = '" + textBox_description.Text + "' WHERE Cat_id = '" + textBox_id.Text + "'";

                    SqlCommand command = new SqlCommand(updateQuery, dBCon.GetCon());

                    dBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Catagory Updated Successfully", "Update Information");
                    dBCon.CloseCon();
                    getTable();
                    clear();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_id.Text == "" || textBox_name.Text == "" || textBox_description.Text == "")
                {
                    MessageBox.Show("Missing Information", "Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string deleteQuery = "DELETE FROM Catagory WHERE Cat_id = " + textBox_id.Text + " ";

                    SqlCommand command = new SqlCommand(deleteQuery, dBCon.GetCon());

                    dBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Catagory Deleted Successfully", "Delete Information");
                    dBCon.CloseCon();
                    getTable();
                    clear();
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGridView_catagory_Click(object sender, EventArgs e)
        {
            textBox_id.Text = DataGridView_catagory.SelectedRows[0].Cells[0].Value.ToString();
            textBox_name.Text = DataGridView_catagory.SelectedRows[0].Cells[1].Value.ToString();
            textBox_description.Text = DataGridView_catagory.SelectedRows[0].Cells[2].Value.ToString();
        } 

        private void button_logout_MouseEnter(object sender, EventArgs e)
        {
            button_logout.ForeColor = Color.Red;
        }

        private void button_logout_MouseLeave(object sender, EventArgs e)
        {
            button_logout.ForeColor = ColorTranslator.FromHtml("#F46B35");
        }

        private void button_logout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void button_product_Click(object sender, EventArgs e)
        {
            Product_Form productForm = new Product_Form();
            productForm.Show();
            this.Hide();
        }

        private void button_seller_Click(object sender, EventArgs e)
        {
            Seller_Form sellerForm = new Seller_Form();
            sellerForm.Show();
            this.Hide();
        }

        private void button_selling_Click(object sender, EventArgs e)
        {
            Selling_Form selling_Form = new Selling_Form();
            selling_Form.Show();
            this.Hide();
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

        private void button_add_MouseEnter(object sender, EventArgs e)
        {
            button_add.ForeColor = ColorTranslator.FromHtml("#F46B35");
        }

        private void button_add_MouseLeave(object sender, EventArgs e)
        {
            button_add.ForeColor = Color.White;
        }

        private void button_update_MouseEnter(object sender, EventArgs e)
        {
            button_update.ForeColor = ColorTranslator.FromHtml("#F46B35");
        }

        private void button_update_MouseLeave(object sender, EventArgs e)
        {
            button_update.ForeColor = Color.White;
        }

        private void button_delete_MouseEnter(object sender, EventArgs e)
        {
            button_delete.ForeColor = ColorTranslator.FromHtml("#F46B35");
        }

        private void button_delete_MouseLeave(object sender, EventArgs e)
        {
            button_delete.ForeColor = Color.White;
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

        private void pictureBox_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
