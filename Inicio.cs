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
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-IQRAVC2\SQLEXPRESS01;Initial Catalog=dbProductos;Integrated Security=True");
            connection.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Usuarios Where Nombre='" + textBox1.Text + "' and Contraseña = '" + textBox2.Text + "'" , connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            
            //filas+columnas para detectar que exista algún usuario
            if(dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();

                Menu form = new Menu();
                form.Show();
            }
            else
            {
                MessageBox.Show("Por favor, revisa el usuario o la contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dataSet1.Usuarios' Puede moverla o quitarla según sea necesario.
            this.usuariosTableAdapter.Fill(this.dataSet1.Usuarios);

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
