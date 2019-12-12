using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormularioWin
{
    public partial class Busqueda : Form
    {
        public Busqueda()
        {
            InitializeComponent();
        }

        private void Busqueda_Load(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-IQRAVC2\SQLEXPRESS01;Initial Catalog=dbProductos;Integrated Security=True");
            String query = "select * from Productos";
            SqlDataAdapter sda = new SqlDataAdapter(query, connection);

            connection.Open();

            DataSet dt = new DataSet();

            sda.Fill(dt, "Productos");

            dataGridView1.DataSource = dt;
            dataGridView1.DataMember = "Productos";

            // TODO: esta línea de código carga datos en la tabla 'dataSet1.Productos' Puede moverla o quitarla según sea necesario.
            this.productosTableAdapter.Fill(this.dataSet1.Productos);

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-IQRAVC2\SQLEXPRESS01;Initial Catalog=dbProductos;Integrated Security=True");
            String query = "select * from Productos where " + comboBox1.Text + " like '%" + textBox1.Text + "%'";
            SqlDataAdapter sda = new SqlDataAdapter(query, connection);

            connection.Open();

            DataSet dt = new DataSet();

            sda.Fill(dt, "Productos");

            dataGridView1.DataSource = dt;
            dataGridView1.DataMember = "Productos";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Menu form = new Menu();
            form.Show();
        }


    }
}
