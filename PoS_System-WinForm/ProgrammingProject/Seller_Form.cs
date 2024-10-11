using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProgrammingProject
{
    public partial class Seller_Form : Form
    {

        DBConnection dBCon = new DBConnection();

        public Seller_Form()
        {
            InitializeComponent();
        }

        private void getTable()
        {
            string selectQuerry = "SELECT * FROM Seller";
            SqlCommand command = new SqlCommand(selectQuerry, dBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView_seller.DataSource = table;
        }

        private void Seller_Form_Load(object sender, EventArgs e)
        {
            getTable();
        }

        private void clear()
        {
            textBox_id.Clear();
            textBox_name.Clear();
            textBox_age.Clear();
            textBox_phone.Clear();
            textBox_pass.Clear();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_id.Text == "" || textBox_name.Text == "" || textBox_age.Text == "" || textBox_phone.Text == "" || textBox_pass.Text == "")
                {
                    MessageBox.Show("Missing Information", "Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string insertQuery = "INSERT INTO Seller VALUES(" + textBox_id.Text + ", '" + textBox_name.Text + "','" + textBox_age.Text + "', '" + textBox_phone.Text + "', '" + textBox_pass.Text + "')";

                    SqlCommand command = new SqlCommand(insertQuery, dBCon.GetCon());

                    dBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Seller Added Successfully", "Add information");
                    dBCon.CloseCon();
                    getTable();
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_id.Text == "" || textBox_name.Text == "" || textBox_age.Text == "" || textBox_phone.Text == "" || textBox_pass.Text == "")
                {
                    MessageBox.Show("Missing Information", "Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string updateQuery = "UPDATE Seller SET Seller_name = '" + textBox_name.Text + "', Seller_age = '" + textBox_age.Text + "', Seller_phone = '"+textBox_phone.Text+"', Seller_pass = '"+textBox_pass.Text+"' WHERE Seller_id = '" + textBox_id.Text + "'";

                    SqlCommand command = new SqlCommand(updateQuery, dBCon.GetCon());

                    dBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Seller Updated Successfully", "Update Information");
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
                if (textBox_id.Text == "" || textBox_name.Text == "" || textBox_age.Text == "" || textBox_phone.Text == "" || textBox_pass.Text == "")
                {
                    MessageBox.Show("Missing Information", "Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if ((MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    { string deleteQuery = "DELETE FROM Seller WHERE Seller_id = " + textBox_id.Text + " ";

                        SqlCommand command = new SqlCommand(deleteQuery, dBCon.GetCon());

                        dBCon.OpenCon();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Seller Deleted Successfully", "Delete Information");
                        dBCon.CloseCon();
                        getTable();
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView_seller_Click(object sender, EventArgs e)
        {
            textBox_id.Text = dataGridView_seller.SelectedRows[0].Cells[0].Value.ToString();
            textBox_name.Text = dataGridView_seller.SelectedRows[0].Cells[1].Value.ToString();
            textBox_age.Text = dataGridView_seller.SelectedRows[0].Cells[2].Value.ToString();
            textBox_phone.Text = dataGridView_seller.SelectedRows[0].Cells[3].Value.ToString();
            textBox_pass.Text = dataGridView_seller.SelectedRows[0].Cells[4].Value.ToString();
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

        private void button_products_Click(object sender, EventArgs e)
        {
            Product_Form products = new Product_Form();
            products.Show();
            this.Hide();
        }

        private void button_catagory_Click(object sender, EventArgs e)
        {
            Catagory_Form catagoryForm = new Catagory_Form();
            catagoryForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
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
