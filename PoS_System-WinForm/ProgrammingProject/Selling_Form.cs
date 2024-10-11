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
using DVGPrinter;

namespace ProgrammingProject
{
    public partial class Selling_Form : Form
    {

        DGVPrinter printer = new DGVPrinter();
        DBConnection dBCon = new DBConnection();

        public Selling_Form()
        {
            InitializeComponent();
        }

        private void getCatagory()
        {
            string selectQuerry = "SELECT * FROM Catagory";
            SqlCommand command = new SqlCommand(selectQuerry, dBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            comboBox_catagory.DataSource = table;
            comboBox_catagory.ValueMember = "Cat_name";
        }

        private void getTable()
        {
            string selectQuerry = "SELECT Prod_name, Prod_price FROM Products";
            SqlCommand command = new SqlCommand(selectQuerry, dBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView_product.DataSource = table;
        }

        private void getBillTable()
        {
            string selectQuerry = "SELECT * FROM Bill";
            SqlCommand command = new SqlCommand(selectQuerry, dBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView_sellList.DataSource = table;
        }

        private void Selling_Form_Load(object sender, EventArgs e)
        {
            label_seller.Text = LoginForm.sellerName;
            label_date.Text = DateTime.Today.ToShortDateString();
            getCatagory();
            getTable();
            getBillTable();
        }

        private void dataGridView_product_Click(object sender, EventArgs e)
        {
            textBox_name.Text = dataGridView_product.SelectedRows[0].Cells[0].Value.ToString();
            textBox_price.Text = dataGridView_product.SelectedRows[0].Cells[1].Value.ToString();
        }


        int grandTotal = 0, n = 0;

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = "INSERT INTO Bill VALUES(" + textBox_id.Text + ", '" + label_seller.Text + "', '" + label_date.Text + "', " + grandTotal.ToString() + ")";

                SqlCommand command = new SqlCommand(insertQuery, dBCon.GetCon());

                dBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Order Added Successfully", "Order information");
                dBCon.CloseCon();
                getBillTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            printer.Title = "Order Bill";
            printer.SubTitle = string.Format("Date : {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Ruhuna Solutions";
            printer.FooterSpacing = 10;
            printer.printDocument.DefaultPageSettings.Landscape = false;
            printer.PrintDataGridView(dataGridView_sellList);
        }

        private void button_logout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            getTable();
        }

        private void comboBox_catagory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectQuerry = "SELECT Prod_name, Prod_price FROM Products WHERE Prod_cat = '" + comboBox_catagory.SelectedValue.ToString() + "'";
            SqlCommand command = new SqlCommand(selectQuerry, dBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView_product.DataSource = table;
        }

        private void button_addOrder_Click(object sender, EventArgs e)
        {
            if (textBox_name.Text == "" || textBox_quantity.Text == "")
            {
                MessageBox.Show("Missing Information", "Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int total = Convert.ToInt32(textBox_price.Text) * Convert.ToInt32(textBox_quantity.Text);
                DataGridViewRow addRow = new DataGridViewRow();
                addRow.CreateCells(dataGridView_order);
                addRow.Cells[0].Value = ++n;
                addRow.Cells[1].Value = textBox_name.Text;
                addRow.Cells[2].Value = textBox_price.Text;
                addRow.Cells[3].Value = textBox_quantity.Text;
                addRow.Cells[4].Value = total;
                dataGridView_order.Rows.Add(addRow);
                grandTotal += total;
                label_amount.Text = "Rs. " + grandTotal;
            }
        }

        private void button_logout_MouseEnter(object sender, EventArgs e)
        {
            button_logout.ForeColor = Color.Red;
        }

        private void button_logout_MouseLeave(object sender, EventArgs e)
        {
            string hexColor = "#F46B35";
            Color foregroundColor = ColorTranslator.FromHtml(hexColor);
            button_logout.ForeColor = foregroundColor;
        }

        private void button_refresh_MouseEnter(object sender, EventArgs e)
        {
            button_refresh.ForeColor = ColorTranslator.FromHtml("#F46B35");
        }
        private void button_refresh_MouseLeave(object sender, EventArgs e)
        {
            button_refresh.ForeColor = Color.White;
        }

        private void button_addOrder_MouseEnter(object sender, EventArgs e)
        {
            button_addOrder.ForeColor = ColorTranslator.FromHtml("#F46B35");
        }

        private void button_addOrder_MouseLeave(object sender, EventArgs e)
        {
            button_addOrder.ForeColor = Color.White;
        }
        private void button_add_MouseEnter(object sender, EventArgs e)
        {
            button_add.ForeColor = ColorTranslator.FromHtml("#F46B35");
        }

        private void button_add_MouseLeave(object sender, EventArgs e)
        {
            button_add.ForeColor = Color.White;
        }

        private void pictureBox_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
    }
}
